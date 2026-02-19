Public Class Workspace
    Inherits Form

#Region "フィールド・コンストラクタ"
    Private DumpFilePath As String
    Private WorkspacePath As String

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
        AnalyzeLogic.AnalyzeDumpFile(DumpFilePath)

        'テストデータを追加する
        AddTestDataToTreeDBList()
    End Sub
#End Region

#Region "テストデータ追加処理"
    Private Sub AddTestDataToTreeDBList()
        'ルートノードを追加
        Dim rootNode As TreeNode = treeDBList.Nodes.Add("Database")

        'スキーマノードを追加
        Dim schemaNode1 As TreeNode = rootNode.Nodes.Add("SCHEMA1")
        Dim schemaNode2 As TreeNode = rootNode.Nodes.Add("SCHEMA2")

        'テーブルノードをSCHEMA1に追加
        schemaNode1.Nodes.Add("EMP_TABLE")
        schemaNode1.Nodes.Add("DEPT_TABLE")
        schemaNode1.Nodes.Add("SALARY_TABLE")

        'テーブルノードをSCHEMA2に追加
        schemaNode2.Nodes.Add("USERS_TABLE")
        schemaNode2.Nodes.Add("PRODUCTS_TABLE")
        schemaNode2.Nodes.Add("ORDERS_TABLE")

        'ルートノードを展開
        rootNode.Expand()
    End Sub
#End Region

End Class