''' <summary>
''' テーブルプロパティダイアログ
''' 選択されたテーブルのメタ情報（スキーマ名、テーブル名、カラム一覧、行数）を表示する
''' </summary>
Partial Public Class TablePropertyDialog
    Implements ILocalizable
    Implements IThemeable

    Public Sub New(schemaName As String, tableName As String, columnNames As String(), columnTypes As String(), rowCount As Long)
        InitializeComponent()
        ApplyLocalization()

        ' タイトル
        Me.Text = Loc.SF("TableProp_FormTitle", schemaName, tableName)

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

        ' テーマ適用
        ThemeManager.ApplyToForm(Me)
    End Sub

#Region "ローカライズ"
    Public Sub ApplyLocalization() Implements ILocalizable.ApplyLocalization
        Me.Text = Loc.S("TableProp_FormTitleBase")
        lblSchemaCaption.Text = Loc.S("TableProp_SchemaLabel")
        lblTableCaption.Text = Loc.S("TableProp_TableNameLabel")
        lblColumnCountCaption.Text = Loc.S("TableProp_ColumnCountLabel")
        lblRowCountCaption.Text = Loc.S("TableProp_RowCountLabel")
        lblColumnsCaption.Text = Loc.S("TableProp_ColumnListLabel")
        colName.Text = Loc.S("TableProp_ColumnNameHeader")
        colType.Text = Loc.S("TableProp_TypeHeader")
        btnClose.Text = Loc.S("Button_Close")
    End Sub
#End Region

#Region "テーマ"
    Public Sub ApplyTheme(isDark As Boolean) Implements IThemeable.ApplyTheme
        ThemeManager.ApplyToControl(Me, isDark)
    End Sub
#End Region

End Class
