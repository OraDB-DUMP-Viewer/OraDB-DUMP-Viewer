Imports System.Collections.Generic

Public Class Workspace
    Inherits Form

#Region "フィールド・コンストラクタ"
    Private DumpFilePath As String
    Private WorkspacePath As String
    ''' <summary>テーブル一覧メタデータ (スキーマ名→(テーブル名, 行数, データオフセット)リスト)</summary>
    Private _tableList As New Dictionary(Of String, List(Of Tuple(Of String, Long, Long)))
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
        ' フェーズ1: テーブル一覧のみ取得（高速・メモリ軽量）
        Dim tables = AnalyzeLogic.ListTables(DumpFilePath)

        ' テーブル一覧をスキーマ別に整理
        _tableList.Clear()
        For Each t In tables
            Dim schema = t.Item1
            Dim tableName = t.Item2
            Dim rowCount = t.Item4
            Dim dataOffset = t.Item5
            If Not _tableList.ContainsKey(schema) Then
                _tableList(schema) = New List(Of Tuple(Of String, Long, Long))
            End If
            _tableList(schema).Add(Tuple.Create(tableName, rowCount, dataOffset))
        Next

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

        If _tableList.ContainsKey(schemaName) Then
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
    ''' <summary>
    ''' テーブルダブルクリック時: オンデマンドで選択テーブルのみ解析
    ''' DUMPファイル全体をスキャンするが、行データは選択テーブルのみメモリに蓄積
    ''' </summary>
    Private Sub LstTableList_DoubleClick(sender As Object, e As EventArgs)
        If lstTableList.SelectedItems.Count = 0 Then
            MessageBox.Show("テーブルが選択されていません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim selectedItem = lstTableList.SelectedItems(0)
            Dim tableName As String = selectedItem.Text

            If String.IsNullOrEmpty(_currentSchema) Then
                MessageBox.Show("スキーマが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' テーブルのデータオフセットを取得（高速シーク用）
            Dim dataOffset As Long = 0
            If _tableList.ContainsKey(_currentSchema) Then
                Dim entry = _tableList(_currentSchema).Find(Function(x) x.Item1 = tableName)
                If entry IsNot Nothing Then
                    dataOffset = entry.Item3
                End If
            End If

            ' フェーズ2: 選択テーブルのみ解析（dataOffset>0ならDDL位置に高速シーク）
            Dim tableData = AnalyzeLogic.AnalyzeTable(DumpFilePath, _currentSchema, tableName, dataOffset)

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

        If _tableList Is Nothing OrElse _tableList.Count = 0 Then
            MessageBox.Show("データベースのスキーマが見つかりません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            ' ルートノードを作成
            Dim rootNode As TreeNode = treeDBList.Nodes.Add(System.IO.Path.GetFileNameWithoutExtension(DumpFilePath))

            ' スキーマノードを作成
            For Each schemaKvp In _tableList
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
                If _tableList.ContainsKey(firstSchemaName) Then
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

        If Not _tableList.ContainsKey(schemaName) Then
            MessageBox.Show($"スキーマ '{schemaName}' が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim tables = _tableList(schemaName)

            If tables Is Nothing OrElse tables.Count = 0 Then
                Return
            End If

            For Each tableInfo In tables
                Dim tableName = tableInfo.Item1
                Dim rowCount = tableInfo.Item2
                ' ListViewItemを作成
                Dim item As New ListViewItem(tableName)
                item.SubItems.Add(schemaName)       ' 所有者
                item.SubItems.Add("TABLE")          ' 種類
                item.SubItems.Add(rowCount.ToString("#,0"))  ' 行数

                lstTableList.Items.Add(item)
            Next

        Catch ex As Exception
            MessageBox.Show($"テーブル一覧の表示に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class