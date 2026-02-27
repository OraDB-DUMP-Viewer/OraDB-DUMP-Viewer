''' <summary>
''' テーブルプロパティダイアログ
''' 選択されたテーブルのメタ情報（スキーマ名、テーブル名、カラム一覧、行数）を表示する
''' </summary>
Partial Public Class TablePropertyDialog

    Public Sub New(schemaName As String, tableName As String, columnNames As String(), columnTypes As String(), rowCount As Long)
        InitializeComponent()

        ' タイトル
        Me.Text = $"テーブルプロパティ - {schemaName}.{tableName}"

        ' メタ情報を設定
        lblSchemaValue.Text = schemaName
        lblTableValue.Text = tableName
        lblColumnCountValue.Text = If(columnNames IsNot Nothing, columnNames.Length.ToString(), "0")
        lblRowCountValue.Text = rowCount.ToString("#,0")

        ' カラム一覧を設定
        If columnNames IsNot Nothing Then
            For i As Integer = 0 To columnNames.Length - 1
                Dim item As New ListViewItem((i + 1).ToString())
                item.SubItems.Add(columnNames(i))
                ' 型情報があれば表示
                If columnTypes IsNot Nothing AndAlso i < columnTypes.Length Then
                    item.SubItems.Add(If(columnTypes(i), ""))
                Else
                    item.SubItems.Add("")
                End If
                lstColumns.Items.Add(item)
            Next
        End If
    End Sub

End Class
