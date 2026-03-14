Imports System.IO

''' <summary>
''' データマスキング定義を設定するダイアログ。
''' テーブル一覧から列を選択し、マスク値を指定する。
''' 定義は .odmask ファイルとして保存・読み込みが可能。
''' </summary>
Public Class MaskingConfigDialog
    Implements ILocalizable

    ''' <summary>現在編集中のマスク定義</summary>
    Private _definition As New MaskingDefinition()

    ''' <summary>現在の定義ファイルパス</summary>
    Private _currentFilePath As String = String.Empty

    ''' <summary>テーブルごとの列名 (キー: "schema.table")</summary>
    Private ReadOnly _columnNamesMap As Dictionary(Of String, String())

    ''' <summary>テーブルごとの列型 (キー: "schema.table")</summary>
    Private ReadOnly _columnTypesMap As Dictionary(Of String, String())

    ''' <summary>テーブル一覧 (キー: schema, 値: テーブル名リスト)</summary>
    Private ReadOnly _tableList As Dictionary(Of String, List(Of String))

    ''' <summary>現在選択中のテーブルキー ("schema.table")</summary>
    Private _selectedTableKey As String = String.Empty

    ''' <summary>UI更新中のフラグ（イベントの再帰防止）</summary>
    Private _isUpdating As Boolean = False

    ''' <summary>ダイアログで確定されたマスク定義を取得する</summary>
    Public ReadOnly Property ResultDefinition As MaskingDefinition
        Get
            Return _definition
        End Get
    End Property

    ''' <summary>ダイアログで確定された定義ファイルパスを取得する</summary>
    Public ReadOnly Property ResultFilePath As String
        Get
            Return _currentFilePath
        End Get
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="columnNamesMap">テーブルごとの列名マップ</param>
    ''' <param name="columnTypesMap">テーブルごとの列型マップ</param>
    ''' <param name="existingDefinitionPath">既存のマスク定義ファイルパス（空ならば新規）</param>
    Public Sub New(
        columnNamesMap As Dictionary(Of String, String()),
        columnTypesMap As Dictionary(Of String, String()),
        Optional existingDefinitionPath As String = "")

        InitializeComponent()

        _columnNamesMap = If(columnNamesMap, New Dictionary(Of String, String()))
        _columnTypesMap = If(columnTypesMap, New Dictionary(Of String, String()))

        ' テーブル一覧を構築
        _tableList = New Dictionary(Of String, List(Of String))
        For Each key In _columnNamesMap.Keys
            Dim parts = key.Split("."c)
            If parts.Length = 2 Then
                Dim schema = parts(0)
                Dim tableName = parts(1)
                If Not _tableList.ContainsKey(schema) Then
                    _tableList(schema) = New List(Of String)
                End If
                _tableList(schema).Add(tableName)
            End If
        Next

        ' 既存定義ファイルがあれば読み込む
        If Not String.IsNullOrEmpty(existingDefinitionPath) AndAlso File.Exists(existingDefinitionPath) Then
            Try
                _definition = MaskingDefinition.Load(existingDefinitionPath)
                _currentFilePath = existingDefinitionPath
            Catch
                _definition = New MaskingDefinition()
            End Try
        End If

        ApplyLocalization()
        PopulateTableList()
        UpdateUIFromDefinition()
    End Sub

    ''' <summary>
    ''' テーブル一覧を左パネルのListBoxに表示する
    ''' </summary>
    Private Sub PopulateTableList()
        lstTables.Items.Clear()
        For Each schemaKvp In _tableList.OrderBy(Function(x) x.Key)
            For Each tableName In schemaKvp.Value.OrderBy(Function(x) x)
                lstTables.Items.Add($"{schemaKvp.Key}.{tableName}")
            Next
        Next
    End Sub

    ''' <summary>
    ''' 現在の定義の内容をUIに反映する
    ''' </summary>
    Private Sub UpdateUIFromDefinition()
        _isUpdating = True
        txtFilePath.Text = _currentFilePath
        txtDescription.Text = _definition.Description
        txtDefaultMask.Text = _definition.DefaultMaskValue
        _isUpdating = False
    End Sub

    ''' <summary>
    ''' 現在のUIの内容を定義に反映する
    ''' </summary>
    Private Sub UpdateDefinitionFromUI()
        _definition.Description = txtDescription.Text
        _definition.DefaultMaskValue = txtDefaultMask.Text
    End Sub

    ''' <summary>
    ''' テーブル選択時に列マスク設定を表示する
    ''' </summary>
    Private Sub lstTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTables.SelectedIndexChanged
        ' 現在の列設定を保存してから切り替え
        SaveCurrentColumnSettings()

        If lstTables.SelectedIndex < 0 Then
            dgvColumns.Rows.Clear()
            _selectedTableKey = String.Empty
            Return
        End If

        _selectedTableKey = lstTables.SelectedItem.ToString()
        DisplayColumnsForTable(_selectedTableKey)
    End Sub

    ''' <summary>
    ''' 指定テーブルの列情報をDataGridViewに表示する
    ''' </summary>
    Private Sub DisplayColumnsForTable(tableKey As String)
        _isUpdating = True
        dgvColumns.Rows.Clear()

        If Not _columnNamesMap.ContainsKey(tableKey) Then
            _isUpdating = False
            Return
        End If

        Dim colNames = _columnNamesMap(tableKey)
        Dim colTypes As String() = Nothing
        If _columnTypesMap.ContainsKey(tableKey) Then
            colTypes = _columnTypesMap(tableKey)
        End If

        ' 既存のマスクルールを取得
        Dim parts = tableKey.Split("."c)
        Dim tableRule As TableMaskingRule = Nothing
        If parts.Length = 2 Then
            tableRule = _definition.FindTableRule(parts(0), parts(1))
        End If

        For i As Integer = 0 To colNames.Length - 1
            Dim colName = colNames(i)
            Dim colType = If(colTypes IsNot Nothing AndAlso i < colTypes.Length, colTypes(i), "")

            ' 既存ルールでマスク対象かチェック
            Dim isChecked = False
            Dim maskValue = ""
            If tableRule IsNot Nothing Then
                Dim colRule = tableRule.Columns.FirstOrDefault(
                    Function(c) String.Equals(c.ColumnName, colName, StringComparison.OrdinalIgnoreCase))
                If colRule IsNot Nothing Then
                    isChecked = True
                    maskValue = colRule.MaskValue
                End If
            End If

            dgvColumns.Rows.Add(isChecked, colName, colType, maskValue)
        Next

        _isUpdating = False
    End Sub

    ''' <summary>
    ''' 現在表示中の列設定を定義に保存する
    ''' </summary>
    Private Sub SaveCurrentColumnSettings()
        If String.IsNullOrEmpty(_selectedTableKey) Then Return

        Dim parts = _selectedTableKey.Split("."c)
        If parts.Length <> 2 Then Return

        Dim schema = parts(0)
        Dim tableName = parts(1)

        ' 既存のテーブルルールを削除
        Dim existingRule = _definition.FindTableRule(schema, tableName)
        If existingRule IsNot Nothing Then
            _definition.Tables.Remove(existingRule)
        End If

        ' チェックされた列からルールを構築
        Dim columnRules As New List(Of ColumnMaskingRule)
        For Each row As DataGridViewRow In dgvColumns.Rows
            Dim isChecked = CBool(If(row.Cells(0).Value, False))
            If isChecked Then
                Dim colName = CStr(If(row.Cells(1).Value, ""))
                Dim maskValue = CStr(If(row.Cells(3).Value, ""))
                columnRules.Add(New ColumnMaskingRule() With {
                    .ColumnName = colName,
                    .MaskValue = maskValue
                })
            End If
        Next

        ' マスク列がある場合のみテーブルルールを追加
        If columnRules.Count > 0 Then
            _definition.Tables.Add(New TableMaskingRule() With {
                .Schema = schema,
                .TableName = tableName,
                .Columns = columnRules
            })
        End If
    End Sub

    ''' <summary>
    ''' 「開く」ボタン: 既存の.odmaskファイルを読み込む
    ''' </summary>
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Using dlg As New OpenFileDialog()
            dlg.Filter = Loc.S("Masking_FileFilter")
            dlg.Title = Loc.S("Masking_OpenTitle")
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    _definition = MaskingDefinition.Load(dlg.FileName)
                    _currentFilePath = dlg.FileName
                    UpdateUIFromDefinition()
                    ' 選択中のテーブルの列設定を再表示
                    If lstTables.SelectedIndex >= 0 Then
                        _selectedTableKey = lstTables.SelectedItem.ToString()
                        DisplayColumnsForTable(_selectedTableKey)
                    End If
                Catch ex As Exception
                    MessageBox.Show(
                        Loc.SF("Masking_LoadError", ex.Message),
                        Loc.S("Masking_ErrorTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 「新規」ボタン: 空の定義を作成する
    ''' </summary>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        _definition = New MaskingDefinition()
        _currentFilePath = String.Empty
        UpdateUIFromDefinition()
        ' 列設定を再表示
        If lstTables.SelectedIndex >= 0 Then
            _selectedTableKey = lstTables.SelectedItem.ToString()
            DisplayColumnsForTable(_selectedTableKey)
        End If
    End Sub

    ''' <summary>
    ''' 「保存」ボタン: 定義を.odmaskファイルに保存する
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' 現在表示中の列設定を保存
        SaveCurrentColumnSettings()
        UpdateDefinitionFromUI()

        Using dlg As New SaveFileDialog()
            dlg.Filter = Loc.S("Masking_FileFilter")
            dlg.Title = Loc.S("Masking_SaveTitle")
            If Not String.IsNullOrEmpty(_currentFilePath) Then
                dlg.FileName = Path.GetFileName(_currentFilePath)
                dlg.InitialDirectory = Path.GetDirectoryName(_currentFilePath)
            End If
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    _definition.Save(dlg.FileName)
                    _currentFilePath = dlg.FileName
                    txtFilePath.Text = _currentFilePath
                    MessageBox.Show(
                        Loc.S("Masking_SaveSuccess"),
                        Loc.S("Masking_InfoTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(
                        Loc.SF("Masking_SaveError", ex.Message),
                        Loc.S("Masking_ErrorTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' OKボタン: 現在の設定を確定して閉じる
    ''' </summary>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SaveCurrentColumnSettings()
        UpdateDefinitionFromUI()
    End Sub

#Region "ローカライズ"
    Public Sub ApplyLocalization() Implements ILocalizable.ApplyLocalization
        Me.Text = Loc.S("Masking_FormTitle")
        lblFilePath.Text = Loc.S("Masking_FilePath")
        btnOpen.Text = Loc.S("Masking_Open")
        btnNew.Text = Loc.S("Masking_New")
        btnSave.Text = Loc.S("Masking_Save")
        lblDescription.Text = Loc.S("Masking_Description")
        lblDefaultMask.Text = Loc.S("Masking_DefaultMaskValue")
        grpTables.Text = Loc.S("Masking_TableList")
        grpColumns.Text = Loc.S("Masking_ColumnSettings")
        colCheck.HeaderText = Loc.S("Masking_ColCheck")
        colColumnName.HeaderText = Loc.S("Masking_ColName")
        colColumnType.HeaderText = Loc.S("Masking_ColType")
        colMaskValue.HeaderText = Loc.S("Masking_ColMaskValue")
        btnOK.Text = Loc.S("Button_OK")
        btnCancel.Text = Loc.S("Button_Cancel")
    End Sub
#End Region

End Class
