Imports System.IO
Imports System.Text
Imports System.Collections.Generic

''' <summary>
''' Oracle DUMPファイル解析ロジック
''' OraDB_DumpParser.dll (C言語ネイティブDLL) を呼び出して解析する
''' </summary>
Public Class AnalyzeLogic

    ''' <summary>
    ''' ダンプファイルを解析する
    ''' </summary>
    ''' <param name="filePath">ダンプファイルのパス</param>
    ''' <returns>解析結果のデータ構造（スキーマ→テーブル→データ）</returns>
    Public Shared Function AnalyzeDumpFile(filePath As String) As Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
        Try
            ValidateFilePath(filePath)

            ' プログレスバーをマーキースタイルに設定
            COMMON.setProgressBarMarquee()

            Dim sw As New Diagnostics.Stopwatch()
            sw.Start()

            ' 進捗コールバック: UIスレッドでプログレスバーを更新
            Dim progressAction As Action(Of Long, String) =
                Sub(rowsProcessed As Long, currentTable As String)
                    Try
                        Dim elapsed = sw.Elapsed
                        Dim msg = $"{rowsProcessed:#,0}行処理済み | {currentTable} | 経過: {elapsed.TotalSeconds:F0}s"
                        COMMON.Set_StatusLavel(msg)
                    Catch
                        ' UI更新エラーは無視
                    End Try
                End Sub

            ' DLLを使ってダンプファイルを解析
            Dim result = OraDB_NativeParser.ParseDump(filePath, progressAction)

            sw.Stop()

            ' プログレスバーをリセット
            COMMON.ResetProgressBar()

            ' 完了メッセージ
            Dim totalRows As Long = 0
            Dim totalTables As Integer = 0
            For Each schema In result
                For Each table In schema.Value
                    totalTables += 1
                    totalRows += table.Value.Count
                Next
            Next
            COMMON.Set_StatusLavel($"解析完了: {totalTables}テーブル, {totalRows:#,0}行 ({sw.Elapsed.TotalSeconds:F1}秒)")

            Return result

        Catch ex As DllNotFoundException
            MessageBox.Show($"解析DLLが見つかりません: {ex.Message}" & vbCrLf &
                           "OraDB_DumpParser.dll が実行ファイルと同じフォルダにあることを確認してください。",
                           "DLLエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            COMMON.ResetProgressBar()
            Return New Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))()

        Catch ex As Exception
            MessageBox.Show($"ダンプファイル解析中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            COMMON.ResetProgressBar()
            Return New Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))()
        End Try
    End Function

    ''' <summary>
    ''' ファイルパスの妥当性をチェック
    ''' </summary>
    Private Shared Sub ValidateFilePath(filePath As String)
        If String.IsNullOrEmpty(filePath) Then
            Throw New ArgumentException("ファイルパスが指定されていません。")
        End If

        If Not File.Exists(filePath) Then
            Throw New FileNotFoundException($"ファイルが見つかりません: {filePath}")
        End If

        If Not filePath.EndsWith(".dmp", StringComparison.OrdinalIgnoreCase) Then
            Throw New ArgumentException("ファイルは.dmp形式である必要があります。")
        End If
    End Sub

End Class
