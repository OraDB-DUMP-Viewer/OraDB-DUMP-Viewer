Imports System.Collections.Generic

Public Class Workspace
    Inherits Form

#Region "フィールド・コンストラクタ"
    Private DumpFilePath As String
    Private WorkspacePath As String
    Private _allTableData As Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
    Private _currentSchema As String = String.Empty

    ' 引数ありコンストラクタ
    Public Sub New(value1 As String, value2 As String)
        InitializeComponent()
        DumpFilePath = value1
        WorkspacePath = value2
    End Sub
#End Region

#Region "イベント処理"
    Private Sub Workspace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ダンプファイルの解析を実行する
        _allTableData = AnalyzeLogic.AnalyzeDumpFile(DumpFilePath)

        'TreeViewにスキーマ一覧を追加
        PopulateSchemaTree()

        ' TreeView のスキーマノードをクリック時のイベント設定
        AddHandler treeDBList.NodeMouseClick, AddressOf TreeDBList_NodeMouseClick

        ' ListViewのダブルクリック時のイベント設定
        AddHandler lstTableList.DoubleClick, AddressOf LstTableList_DoubleClick
    End Sub

    ''' <summary>
    ''' TreeView のスキーマノードがクリックされた時のイベント
    ''' 選択されたスキーマのテーブルをListViewに表示する
    ''' </summary>
    Private Sub TreeDBList_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)
        Dim node = e.Node

        ' ルートノード（Database）の場合はスキップ
        If node.Parent Is Nothing Then
            lstTableList.Items.Clear()
            _currentSchema = String.Empty
            Return
        End If

        ' スキーマノードの場合 - 括弧を削除してスキーマ名を抽出
        Dim schemaName As String = ExtractSchemaName(node.Text)

        If _allTableData.ContainsKey(schemaName) Then
            _currentSchema = schemaName
            DisplayTablesForSchema(schemaName)
        End If
    End Sub

    ''' <summary>
    ''' ノードテキストからスキーマ名を抽出する
    ''' 「スキーマ名 (テーブル数)」形式から「スキーマ名」を取得
    ''' </summary>
    Private Function ExtractSchemaName(nodeText As String) As String
        Dim parts = nodeText.Split("("c)
        If parts.Length > 0 Then
            Return parts(0).Trim()
        End If
        Return nodeText
    End Function

    ''' <summary>
    ''' ListViewのアイテムがダブルクリックされた時のイベント
    ''' 選択されたテーブルをTablePreviewで表示
    ''' </summary>
    Private Sub LstTableList_DoubleClick(sender As Object, e As EventArgs)
        If lstTableList.SelectedItems.Count = 0 Then
            MessageBox.Show("テーブルが選択されていません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim selectedItem = lstTableList.SelectedItems(0)
            Dim tableName As String = selectedItem.Text

            If String.IsNullOrEmpty(_currentSchema) OrElse Not _allTableData.ContainsKey(_currentSchema) Then
                MessageBox.Show("スキーマが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not _allTableData(_currentSchema).ContainsKey(tableName) Then
                MessageBox.Show($"テーブル '{tableName}' が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim tableData = _allTableData(_currentSchema)(tableName)

            If tableData Is Nothing OrElse tableData.Count = 0 Then
                MessageBox.Show("テーブルにデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 列名を取得
            Dim columnNames As New List(Of String)(tableData(0).Keys)

            ' TablePreview を表示
            TablePreviewLogic.DisplayTableData(Me.MdiParent, tableData, columnNames,
                                               $"{_currentSchema}.{tableName}")

        Catch ex As Exception
            MessageBox.Show($"テーブル表示エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "データ表示処理"
    ''' <summary>
    ''' TreeViewにスキーマ一覧のみを表示
    ''' </summary>
    Private Sub PopulateSchemaTree()
        treeDBList.Nodes.Clear()

        If _allTableData Is Nothing OrElse _allTableData.Count = 0 Then
            MessageBox.Show("データベースのスキーマが見つかりません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            ' ルートノードを作成
            Dim rootNode As TreeNode = treeDBList.Nodes.Add(System.IO.Path.GetFileNameWithoutExtension(DumpFilePath))

            ' スキーマノードを作成
            For Each schemaKvp In _allTableData
                Dim schemaName As String = schemaKvp.Key
                Dim tableCount As Integer = schemaKvp.Value.Count

                ' スキーマノードを作成（テーブル数を表示）
                Dim schemaNode As TreeNode = rootNode.Nodes.Add($"{schemaName} ({tableCount})")
            Next

            ' ルートノードを展開
            rootNode.Expand()

            ' 最初のスキーマを自動選択
            If rootNode.Nodes.Count > 0 Then
                treeDBList.SelectedNode = rootNode.Nodes(0)
                Dim firstSchemaName As String = ExtractSchemaName(rootNode.Nodes(0).Text)
                If _allTableData.ContainsKey(firstSchemaName) Then
                    _currentSchema = firstSchemaName
                    DisplayTablesForSchema(firstSchemaName)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show($"スキーマツリーの作成に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 指定されたスキーマのテーブル一覧をListViewに表示
    ''' </summary>
    Private Sub DisplayTablesForSchema(schemaName As String)
        lstTableList.Items.Clear()

        If String.IsNullOrEmpty(schemaName) Then
            Return
        End If

        If Not _allTableData.ContainsKey(schemaName) Then
            MessageBox.Show($"スキーマ '{schemaName}' が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim tables = _allTableData(schemaName)

            If tables Is Nothing OrElse tables.Count = 0 Then
                ' テーブルがない場合も表示
                Return
            End If

            For Each tableKvp In tables
                Dim tableName As String = tableKvp.Key
                Dim tableData = tableKvp.Value
                Dim rowCount As Integer = If(tableData IsNot Nothing, tableData.Count, 0)

                ' ListViewItemを作成
                Dim item As New ListViewItem(tableName)
                item.SubItems.Add(schemaName)       ' 所有者
                item.SubItems.Add("TABLE")          ' 種類
                item.SubItems.Add(rowCount.ToString())  ' 行数

                lstTableList.Items.Add(item)
            Next

        Catch ex As Exception
            MessageBox.Show($"テーブル一覧の表示に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class