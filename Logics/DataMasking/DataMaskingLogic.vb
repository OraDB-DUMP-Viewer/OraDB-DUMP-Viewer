''' <summary>
''' データマスキング処理を行うモジュール。
''' エクスポート前にデータのコピーを作成し、指定列の値を固定文字列に置換する。
''' 元データは変更しない（非破壊処理）。
''' </summary>
Public Module DataMaskingLogic

    ''' <summary>
    ''' マスク定義に基づいてデータにマスクを適用する。
    ''' 元のデータは変更せず、ディープコピーしたデータを返す。
    ''' </summary>
    ''' <param name="data">元データ（行のリスト、各行は列値の配列）</param>
    ''' <param name="columnNames">列名のリスト</param>
    ''' <param name="schema">スキーマ名</param>
    ''' <param name="tableName">テーブル名</param>
    ''' <param name="definition">マスク定義</param>
    ''' <returns>マスク適用済みのデータ（ディープコピー）。該当ルールがなければ元データをそのまま返す。</returns>
    Public Function ApplyMask(
        data As List(Of String()),
        columnNames As List(Of String),
        schema As String,
        tableName As String,
        definition As MaskingDefinition
    ) As List(Of String())

        If definition Is Nothing Then Return data

        Dim tableRule = definition.FindTableRule(schema, tableName)
        If tableRule Is Nothing OrElse tableRule.Columns.Count = 0 Then
            Return data
        End If

        ' マスク対象列のインデックスとマスク値のマッピングを構築
        Dim maskMap As New Dictionary(Of Integer, String)
        For Each colRule In tableRule.Columns
            Dim colIndex = -1
            For i As Integer = 0 To columnNames.Count - 1
                If String.Equals(columnNames(i), colRule.ColumnName, StringComparison.OrdinalIgnoreCase) Then
                    colIndex = i
                    Exit For
                End If
            Next
            If colIndex >= 0 Then
                Dim maskValue = colRule.MaskValue
                If String.IsNullOrEmpty(maskValue) Then
                    maskValue = definition.DefaultMaskValue
                End If
                maskMap(colIndex) = maskValue
            End If
        Next

        If maskMap.Count = 0 Then Return data

        ' ディープコピーを作成しながらマスクを適用
        Dim maskedData As New List(Of String())(data.Count)
        For Each row In data
            Dim newRow = CType(row.Clone(), String())
            For Each kvp In maskMap
                ' NULL値はNULLのまま保持
                If newRow(kvp.Key) IsNot Nothing Then
                    newRow(kvp.Key) = kvp.Value
                End If
            Next
            maskedData.Add(newRow)
        Next

        Return maskedData
    End Function

End Module
