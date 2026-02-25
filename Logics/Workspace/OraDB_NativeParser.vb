Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
''' OraDB_DumpParser.dll P/Invoke ラッパー
''' C言語ネイティブDLLとの連携を担当
''' </summary>
Public Class OraDB_NativeParser

#Region "定数"
    ' DLL名
    Private Const DLL_NAME As String = "OraDB_DumpParser.dll"

    ' Return codes
    Public Const ODV_OK As Integer = 0
    Public Const ODV_ERROR As Integer = -1
    Public Const ODV_ERROR_CANCELLED As Integer = -200

    ' Dump types
    Public Const DUMP_UNKNOWN As Integer = -1
    Public Const DUMP_EXPDP As Integer = 0
    Public Const DUMP_EXPDP_COMPRESS As Integer = 1
    Public Const DUMP_EXP As Integer = 10
    Public Const DUMP_EXP_DIRECT As Integer = 11
#End Region

#Region "コールバックデリゲート"
    ''' <summary>
    ''' 行データ配送コールバック (1行ごとに呼ばれる)
    ''' </summary>
    <UnmanagedFunctionPointer(CallingConvention.StdCall)>
    Public Delegate Sub RowCallback(
        schema As IntPtr,
        table As IntPtr,
        colCount As Integer,
        colNames As IntPtr,
        colValues As IntPtr,
        userData As IntPtr
    )

    ''' <summary>
    ''' 進捗通知コールバック (500行ごとに呼ばれる)
    ''' </summary>
    <UnmanagedFunctionPointer(CallingConvention.StdCall)>
    Public Delegate Sub ProgressCallback(
        rowsProcessed As Long,
        currentTable As IntPtr,
        userData As IntPtr
    )

    ''' <summary>
    ''' テーブル発見コールバック (テーブルごとに呼ばれる)
    ''' </summary>
    <UnmanagedFunctionPointer(CallingConvention.StdCall)>
    Public Delegate Sub TableCallback(
        schema As IntPtr,
        table As IntPtr,
        colCount As Integer,
        colNames As IntPtr,
        colTypes As IntPtr,
        rowCount As Long,
        userData As IntPtr
    )
#End Region

#Region "DLLインポート"
    ' セッション管理
    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_create_session(ByRef session As IntPtr) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_destroy_session(session As IntPtr) As Integer
    End Function

    ' 設定
    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)>
    Private Shared Function odv_set_dump_file(session As IntPtr, <MarshalAs(UnmanagedType.LPStr)> path As String) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_set_row_callback(session As IntPtr, cb As RowCallback, userData As IntPtr) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_set_progress_callback(session As IntPtr, cb As ProgressCallback, userData As IntPtr) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_set_table_callback(session As IntPtr, cb As TableCallback, userData As IntPtr) As Integer
    End Function

    ' 操作
    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_check_dump_kind(session As IntPtr, ByRef dumpType As Integer) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_list_tables(session As IntPtr) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_parse_dump(session As IntPtr) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Ansi)>
    Private Shared Function odv_export_csv(session As IntPtr,
        <MarshalAs(UnmanagedType.LPStr)> tableName As String,
        <MarshalAs(UnmanagedType.LPStr)> outputPath As String) As Integer
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_cancel(session As IntPtr) As Integer
    End Function

    ' ユーティリティ
    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_get_version() As IntPtr
    End Function

    <DllImport(DLL_NAME, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function odv_get_last_error(session As IntPtr) As IntPtr
    End Function
#End Region

#Region "ヘルパーメソッド"
    ''' <summary>
    ''' IntPtr (char*) からマネージド文字列に変換 (UTF-8)
    ''' </summary>
    Private Shared Function PtrToStringUTF8(ptr As IntPtr) As String
        If ptr = IntPtr.Zero Then Return String.Empty
        Return Marshal.PtrToStringUTF8(ptr)
    End Function

    ''' <summary>
    ''' char** 配列から String配列に変換
    ''' </summary>
    Private Shared Function PtrArrayToStrings(ptrArray As IntPtr, count As Integer) As String()
        Dim result(count - 1) As String
        For i As Integer = 0 To count - 1
            Dim strPtr As IntPtr = Marshal.ReadIntPtr(ptrArray, i * IntPtr.Size)
            result(i) = PtrToStringUTF8(strPtr)
        Next
        Return result
    End Function
#End Region

#Region "パースコンテキスト"
    ''' <summary>
    ''' DLL解析中の状態を保持するコンテキスト
    ''' GCHandle経由でコールバックに渡される
    ''' </summary>
    Public Class ParseContext
        Public AllData As New Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
        Public RowsProcessed As Long = 0
        Public CurrentTable As String = ""
        Public ProgressAction As Action(Of Long, String) = Nothing
        Public ColumnNamesCache As New Dictionary(Of String, String())

        ''' <summary>
        ''' テーブルのフルキー (schema.table)
        ''' </summary>
        Public Shared Function TableKey(schema As String, table As String) As String
            Return $"{schema}.{table}"
        End Function
    End Class
#End Region

#Region "公開API"
    ''' <summary>
    ''' DLLバージョンを取得
    ''' </summary>
    Public Shared Function GetVersion() As String
        Try
            Dim ptr = odv_get_version()
            Return PtrToStringUTF8(ptr)
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function

    ''' <summary>
    ''' ダンプファイルの種類を判定
    ''' </summary>
    Public Shared Function CheckDumpKind(filePath As String) As Integer
        Dim session As IntPtr = IntPtr.Zero
        Try
            Dim rc = odv_create_session(session)
            If rc <> ODV_OK Then Return DUMP_UNKNOWN

            rc = odv_set_dump_file(session, filePath)
            If rc <> ODV_OK Then Return DUMP_UNKNOWN

            Dim dumpType As Integer = DUMP_UNKNOWN
            rc = odv_check_dump_kind(session, dumpType)
            Return dumpType
        Catch
            Return DUMP_UNKNOWN
        Finally
            If session <> IntPtr.Zero Then
                odv_destroy_session(session)
            End If
        End Try
    End Function

    ''' <summary>
    ''' ダンプファイルを解析し、全テーブルデータを返す
    ''' </summary>
    ''' <param name="filePath">ダンプファイルパス</param>
    ''' <param name="progressAction">進捗コールバック (処理行数, 現在のテーブル名)</param>
    ''' <returns>スキーマ→テーブル→行リストの辞書</returns>
    Public Shared Function ParseDump(filePath As String, Optional progressAction As Action(Of Long, String) = Nothing) As Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
        Dim session As IntPtr = IntPtr.Zero
        Dim ctx As New ParseContext()
        ctx.ProgressAction = progressAction
        Dim gcHandle As GCHandle = GCHandle.Alloc(ctx)

        ' コールバックデリゲートをフィールドに保持（GC回収防止）
        Dim rowCb As New RowCallback(AddressOf OnRowCallback)
        Dim progCb As New ProgressCallback(AddressOf OnProgressCallback)

        Try
            Dim rc = odv_create_session(session)
            If rc <> ODV_OK Then
                Throw New Exception($"セッション作成に失敗しました (rc={rc})")
            End If

            rc = odv_set_dump_file(session, filePath)
            If rc <> ODV_OK Then
                Dim errMsg = PtrToStringUTF8(odv_get_last_error(session))
                Throw New Exception($"ダンプファイル設定エラー: {errMsg}")
            End If

            ' コールバック設定
            Dim userData As IntPtr = GCHandle.ToIntPtr(gcHandle)
            odv_set_row_callback(session, rowCb, userData)
            odv_set_progress_callback(session, progCb, userData)

            ' 解析実行
            rc = odv_parse_dump(session)
            If rc <> ODV_OK AndAlso rc <> ODV_ERROR_CANCELLED Then
                Dim errMsg = PtrToStringUTF8(odv_get_last_error(session))
                Throw New Exception($"ダンプ解析エラー: {errMsg}")
            End If

            Return ctx.AllData

        Finally
            If session <> IntPtr.Zero Then
                odv_destroy_session(session)
            End If
            If gcHandle.IsAllocated Then
                gcHandle.Free()
            End If
        End Try
    End Function

    ''' <summary>
    ''' テーブル一覧のみ取得（データは読み込まない）
    ''' </summary>
    Public Shared Function ListTables(filePath As String) As List(Of Tuple(Of String, String, Integer))
        Dim session As IntPtr = IntPtr.Zero
        Dim tables As New List(Of Tuple(Of String, String, Integer))
        Dim gcHandle As GCHandle = GCHandle.Alloc(tables)
        Dim tableCb As New TableCallback(AddressOf OnTableListCallback)

        Try
            Dim rc = odv_create_session(session)
            If rc <> ODV_OK Then Return tables

            rc = odv_set_dump_file(session, filePath)
            If rc <> ODV_OK Then Return tables

            Dim userData As IntPtr = GCHandle.ToIntPtr(gcHandle)
            odv_set_table_callback(session, tableCb, userData)

            odv_list_tables(session)
            Return tables

        Finally
            If session <> IntPtr.Zero Then
                odv_destroy_session(session)
            End If
            If gcHandle.IsAllocated Then
                gcHandle.Free()
            End If
        End Try
    End Function

    ''' <summary>
    ''' CSVエクスポート
    ''' </summary>
    Public Shared Function ExportCsv(filePath As String, tableName As String, outputPath As String) As Integer
        Dim session As IntPtr = IntPtr.Zero
        Try
            Dim rc = odv_create_session(session)
            If rc <> ODV_OK Then Return rc

            rc = odv_set_dump_file(session, filePath)
            If rc <> ODV_OK Then Return rc

            Return odv_export_csv(session, tableName, outputPath)

        Finally
            If session <> IntPtr.Zero Then
                odv_destroy_session(session)
            End If
        End Try
    End Function
#End Region

#Region "コールバック実装"
    ''' <summary>
    ''' 行データコールバック - C DLLから1行ごとに呼ばれる
    ''' </summary>
    Private Shared Sub OnRowCallback(schemaPtr As IntPtr, tablePtr As IntPtr,
                                     colCount As Integer, colNamesPtr As IntPtr,
                                     colValuesPtr As IntPtr, userData As IntPtr)
        Try
            Dim gcHandle As GCHandle = GCHandle.FromIntPtr(userData)
            Dim ctx = DirectCast(gcHandle.Target, ParseContext)

            Dim schema = PtrToStringUTF8(schemaPtr)
            Dim table = PtrToStringUTF8(tablePtr)
            Dim colNames = PtrArrayToStrings(colNamesPtr, colCount)
            Dim colValues = PtrArrayToStrings(colValuesPtr, colCount)

            ' スキーマ辞書を確保
            If Not ctx.AllData.ContainsKey(schema) Then
                ctx.AllData(schema) = New Dictionary(Of String, List(Of Dictionary(Of String, Object)))
            End If

            ' テーブル行リストを確保
            If Not ctx.AllData(schema).ContainsKey(table) Then
                ctx.AllData(schema)(table) = New List(Of Dictionary(Of String, Object))
            End If

            ' 行データを辞書に変換して追加
            Dim row As New Dictionary(Of String, Object)
            For i As Integer = 0 To colCount - 1
                Dim colName = If(colNames(i), $"COL_{i}")
                Dim colValue As Object = If(String.IsNullOrEmpty(colValues(i)), DBNull.Value, colValues(i))
                If Not row.ContainsKey(colName) Then
                    row(colName) = colValue
                End If
            Next

            ctx.AllData(schema)(table).Add(row)
            ctx.RowsProcessed += 1

        Catch
            ' コールバック中の例外は握りつぶす（DLL側に伝播させない）
        End Try
    End Sub

    ''' <summary>
    ''' 進捗コールバック - 500行ごとに呼ばれる
    ''' </summary>
    Private Shared Sub OnProgressCallback(rowsProcessed As Long, currentTablePtr As IntPtr, userData As IntPtr)
        Try
            Dim gcHandle As GCHandle = GCHandle.FromIntPtr(userData)
            Dim ctx = DirectCast(gcHandle.Target, ParseContext)
            Dim currentTable = PtrToStringUTF8(currentTablePtr)

            ctx.CurrentTable = currentTable

            ctx.ProgressAction?.Invoke(rowsProcessed, currentTable)

        Catch
            ' コールバック中の例外は握りつぶす
        End Try
    End Sub

    ''' <summary>
    ''' テーブル一覧コールバック
    ''' </summary>
    Private Shared Sub OnTableListCallback(schemaPtr As IntPtr, tablePtr As IntPtr,
                                           colCount As Integer, colNamesPtr As IntPtr,
                                           colTypesPtr As IntPtr, rowCount As Long,
                                           userData As IntPtr)
        Try
            Dim gcHandle As GCHandle = GCHandle.FromIntPtr(userData)
            Dim tables = DirectCast(gcHandle.Target, List(Of Tuple(Of String, String, Integer)))

            Dim schema = PtrToStringUTF8(schemaPtr)
            Dim table = PtrToStringUTF8(tablePtr)

            tables.Add(Tuple.Create(schema, table, colCount))

        Catch
            ' コールバック中の例外は握りつぶす
        End Try
    End Sub
#End Region

End Class
