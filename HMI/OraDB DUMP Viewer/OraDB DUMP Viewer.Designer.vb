<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OraDB_DUMP_Viewer
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OraDB_DUMP_Viewer))
        MenuStrip = New MenuStrip()
        ファイルFToolStripMenuItem = New ToolStripMenuItem()
        開くToolStripMenuItem = New ToolStripMenuItem()
        ワークスペースToolStripMenuItem = New ToolStripMenuItem()
        ダンプファイルDToolStripMenuItem = New ToolStripMenuItem()
        閉じるCToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        ワークスペースの保存SToolStripMenuItem = New ToolStripMenuItem()
        名前を付けてワークスペースを保存AToolStripMenuItem = New ToolStripMenuItem()
        ワークスペースを閉じるLToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        ダンプファイルの作成GToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        最近使ったワークスペースWToolStripMenuItem = New ToolStripMenuItem()
        最近使ったダンプファイルDToolStripMenuItem = New ToolStripMenuItem()
        終了XToolStripMenuItem = New ToolStripMenuItem()
        編集EToolStripMenuItem = New ToolStripMenuItem()
        元に戻すUToolStripMenuItem = New ToolStripMenuItem()
        やり直しRToolStripMenuItem = New ToolStripMenuItem()
        コピーCToolStripMenuItem = New ToolStripSeparator()
        切り取りTToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripMenuItem()
        貼り付けPToolStripMenuItem = New ToolStripMenuItem()
        すべて選択AToolStripMenuItem = New ToolStripSeparator()
        すべて選択AToolStripMenuItem1 = New ToolStripMenuItem()
        表示VToolStripMenuItem = New ToolStripMenuItem()
        ツールバーTToolStripMenuItem = New ToolStripMenuItem()
        エクスポートToolStripMenuItem = New ToolStripMenuItem()
        ステータスバーSToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        最新の情報に更新RToolStripMenuItem = New ToolStripMenuItem()
        オブジェクトOToolStripMenuItem = New ToolStripMenuItem()
        開くToolStripMenuItem1 = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        すべてのフィルタをクリアFToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        削除DToolStripMenuItem = New ToolStripMenuItem()
        更新の取り消しCToolStripMenuItem = New ToolStripMenuItem()
        プロパティPToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        エクスポートToolStripMenuItem1 = New ToolStripMenuItem()
        スクリプトWSToolStripMenuItem = New ToolStripMenuItem()
        データDToolStripMenuItem = New ToolStripMenuItem()
        レポート出力RToolStripMenuItem = New ToolStripMenuItem()
        オブジェクト一覧ToolStripMenuItem = New ToolStripMenuItem()
        テーブル定義ToolStripMenuItem = New ToolStripMenuItem()
        ツールTToolStripMenuItem = New ToolStripMenuItem()
        ファイルの取り出しFToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator8 = New ToolStripSeparator()
        レポート定義DToolStripMenuItem = New ToolStripMenuItem()
        オプションOToolStripMenuItem = New ToolStripMenuItem()
        ウィンドウWToolStripMenuItem = New ToolStripMenuItem()
        重ねて表示CToolStripMenuItem = New ToolStripMenuItem()
        並べて表示TToolStripMenuItem = New ToolStripMenuItem()
        アイコンの整列IToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator13 = New ToolStripSeparator()
        ヘルプHToolStripMenuItem = New ToolStripMenuItem()
        MokujiToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        エラー報告RToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator10 = New ToolStripSeparator()
        ライセンス認証ToolStripMenuItem = New ToolStripMenuItem()
        バージョン情報AToolStripMenuItem = New ToolStripMenuItem()
        StatusStrip = New StatusStrip()
        ToolStripProgressBar = New ToolStripProgressBar()
        ToolStripStatusLabel = New ToolStripStatusLabel()
        ToolExport = New ToolStrip()
        ToolStripButton2 = New ToolStripButton()
        ToolStripButton1 = New ToolStripButton()
        ToolStripSeparator11 = New ToolStripSeparator()
        ToolStripButton5 = New ToolStripButton()
        ToolStripButton6 = New ToolStripButton()
        ToolStripSeparator12 = New ToolStripSeparator()
        ToolStripButton7 = New ToolStripButton()
        ToolStripButton8 = New ToolStripButton()
        ToolStripButton10 = New ToolStripButton()
        ToolStripButton9 = New ToolStripButton()
        ToolStripButton3 = New ToolStripButton()
        MenuStrip.SuspendLayout()
        StatusStrip.SuspendLayout()
        ToolExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip
        ' 
        MenuStrip.ImageScalingSize = New Size(24, 24)
        MenuStrip.Items.AddRange(New ToolStripItem() {ファイルFToolStripMenuItem, 編集EToolStripMenuItem, 表示VToolStripMenuItem, オブジェクトOToolStripMenuItem, ツールTToolStripMenuItem, ウィンドウWToolStripMenuItem, ヘルプHToolStripMenuItem})
        MenuStrip.Location = New Point(0, 0)
        MenuStrip.MdiWindowListItem = ウィンドウWToolStripMenuItem
        MenuStrip.Name = "MenuStrip"
        MenuStrip.Size = New Size(1607, 33)
        MenuStrip.TabIndex = 0
        MenuStrip.Text = "MenuStrip1"
        ' 
        ' ファイルFToolStripMenuItem
        ' 
        ファイルFToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {開くToolStripMenuItem, 閉じるCToolStripMenuItem, ToolStripSeparator1, ワークスペースの保存SToolStripMenuItem, 名前を付けてワークスペースを保存AToolStripMenuItem, ワークスペースを閉じるLToolStripMenuItem, ToolStripSeparator2, ダンプファイルの作成GToolStripMenuItem, ToolStripSeparator3, 最近使ったワークスペースWToolStripMenuItem, 最近使ったダンプファイルDToolStripMenuItem, 終了XToolStripMenuItem})
        ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem"
        ファイルFToolStripMenuItem.Size = New Size(98, 29)
        ファイルFToolStripMenuItem.Text = "ファイル(F)"
        ' 
        ' 開くToolStripMenuItem
        ' 
        開くToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ワークスペースToolStripMenuItem, ダンプファイルDToolStripMenuItem})
        開くToolStripMenuItem.Name = "開くToolStripMenuItem"
        開くToolStripMenuItem.Size = New Size(387, 34)
        開くToolStripMenuItem.Text = "開く(&O)"
        ' 
        ' ワークスペースToolStripMenuItem
        ' 
        ワークスペースToolStripMenuItem.Enabled = False
        ワークスペースToolStripMenuItem.Name = "ワークスペースToolStripMenuItem"
        ワークスペースToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Shift Or Keys.O
        ワークスペースToolStripMenuItem.Size = New Size(360, 34)
        ワークスペースToolStripMenuItem.Text = "ワークスペース(&W)..."
        ' 
        ' ダンプファイルDToolStripMenuItem
        ' 
        ダンプファイルDToolStripMenuItem.Name = "ダンプファイルDToolStripMenuItem"
        ダンプファイルDToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.O
        ダンプファイルDToolStripMenuItem.Size = New Size(360, 34)
        ダンプファイルDToolStripMenuItem.Text = "ダンプファイル(&D)..."
        ' 
        ' 閉じるCToolStripMenuItem
        ' 
        閉じるCToolStripMenuItem.Name = "閉じるCToolStripMenuItem"
        閉じるCToolStripMenuItem.Size = New Size(387, 34)
        閉じるCToolStripMenuItem.Text = "閉じる(&C)"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(384, 6)
        ' 
        ' ワークスペースの保存SToolStripMenuItem
        ' 
        ワークスペースの保存SToolStripMenuItem.Enabled = False
        ワークスペースの保存SToolStripMenuItem.Name = "ワークスペースの保存SToolStripMenuItem"
        ワークスペースの保存SToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        ワークスペースの保存SToolStripMenuItem.Size = New Size(387, 34)
        ワークスペースの保存SToolStripMenuItem.Text = "ワークスペースの保存(&S)"
        ' 
        ' 名前を付けてワークスペースを保存AToolStripMenuItem
        ' 
        名前を付けてワークスペースを保存AToolStripMenuItem.Enabled = False
        名前を付けてワークスペースを保存AToolStripMenuItem.Name = "名前を付けてワークスペースを保存AToolStripMenuItem"
        名前を付けてワークスペースを保存AToolStripMenuItem.Size = New Size(387, 34)
        名前を付けてワークスペースを保存AToolStripMenuItem.Text = "名前を付けてワークスペースを保存(&A)..."
        ' 
        ' ワークスペースを閉じるLToolStripMenuItem
        ' 
        ワークスペースを閉じるLToolStripMenuItem.Enabled = False
        ワークスペースを閉じるLToolStripMenuItem.Name = "ワークスペースを閉じるLToolStripMenuItem"
        ワークスペースを閉じるLToolStripMenuItem.Size = New Size(387, 34)
        ワークスペースを閉じるLToolStripMenuItem.Text = "ワークスペースを閉じる(&L)"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(384, 6)
        ' 
        ' ダンプファイルの作成GToolStripMenuItem
        ' 
        ダンプファイルの作成GToolStripMenuItem.Enabled = False
        ダンプファイルの作成GToolStripMenuItem.Name = "ダンプファイルの作成GToolStripMenuItem"
        ダンプファイルの作成GToolStripMenuItem.ShowShortcutKeys = False
        ダンプファイルの作成GToolStripMenuItem.Size = New Size(387, 34)
        ダンプファイルの作成GToolStripMenuItem.Text = "ダンプファイルの作成(&G)..."
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(384, 6)
        ' 
        ' 最近使ったワークスペースWToolStripMenuItem
        ' 
        最近使ったワークスペースWToolStripMenuItem.Enabled = False
        最近使ったワークスペースWToolStripMenuItem.Name = "最近使ったワークスペースWToolStripMenuItem"
        最近使ったワークスペースWToolStripMenuItem.Size = New Size(387, 34)
        最近使ったワークスペースWToolStripMenuItem.Text = "最近使ったワークスペース(&W)"
        ' 
        ' 最近使ったダンプファイルDToolStripMenuItem
        ' 
        最近使ったダンプファイルDToolStripMenuItem.Enabled = False
        最近使ったダンプファイルDToolStripMenuItem.Name = "最近使ったダンプファイルDToolStripMenuItem"
        最近使ったダンプファイルDToolStripMenuItem.Size = New Size(387, 34)
        最近使ったダンプファイルDToolStripMenuItem.Text = "最近使ったダンプファイル(&D)"
        ' 
        ' 終了XToolStripMenuItem
        ' 
        終了XToolStripMenuItem.Name = "終了XToolStripMenuItem"
        終了XToolStripMenuItem.Size = New Size(387, 34)
        終了XToolStripMenuItem.Text = "終了(&X)"
        ' 
        ' 編集EToolStripMenuItem
        ' 
        編集EToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {元に戻すUToolStripMenuItem, やり直しRToolStripMenuItem, コピーCToolStripMenuItem, 切り取りTToolStripMenuItem, ToolStripMenuItem1, 貼り付けPToolStripMenuItem, すべて選択AToolStripMenuItem, すべて選択AToolStripMenuItem1})
        編集EToolStripMenuItem.Name = "編集EToolStripMenuItem"
        編集EToolStripMenuItem.Size = New Size(83, 29)
        編集EToolStripMenuItem.Text = "編集(E)"
        ' 
        ' 元に戻すUToolStripMenuItem
        ' 
        元に戻すUToolStripMenuItem.Enabled = False
        元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem"
        元に戻すUToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
        元に戻すUToolStripMenuItem.Size = New Size(278, 34)
        元に戻すUToolStripMenuItem.Text = "元に戻す(&U)"
        ' 
        ' やり直しRToolStripMenuItem
        ' 
        やり直しRToolStripMenuItem.Enabled = False
        やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem"
        やり直しRToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.Y
        やり直しRToolStripMenuItem.Size = New Size(278, 34)
        やり直しRToolStripMenuItem.Text = "やり直し(&R)"
        ' 
        ' コピーCToolStripMenuItem
        ' 
        コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem"
        コピーCToolStripMenuItem.Size = New Size(275, 6)
        ' 
        ' 切り取りTToolStripMenuItem
        ' 
        切り取りTToolStripMenuItem.Enabled = False
        切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem"
        切り取りTToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.X
        切り取りTToolStripMenuItem.Size = New Size(278, 34)
        切り取りTToolStripMenuItem.Text = "切り取り(&T)"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Enabled = False
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.ShortcutKeys = Keys.Control Or Keys.C
        ToolStripMenuItem1.Size = New Size(278, 34)
        ToolStripMenuItem1.Text = "コピー(&C)"
        ' 
        ' 貼り付けPToolStripMenuItem
        ' 
        貼り付けPToolStripMenuItem.Enabled = False
        貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem"
        貼り付けPToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.V
        貼り付けPToolStripMenuItem.Size = New Size(278, 34)
        貼り付けPToolStripMenuItem.Text = "貼り付け(&P)"
        ' 
        ' すべて選択AToolStripMenuItem
        ' 
        すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem"
        すべて選択AToolStripMenuItem.Size = New Size(275, 6)
        ' 
        ' すべて選択AToolStripMenuItem1
        ' 
        すべて選択AToolStripMenuItem1.Enabled = False
        すべて選択AToolStripMenuItem1.Name = "すべて選択AToolStripMenuItem1"
        すべて選択AToolStripMenuItem1.ShortcutKeys = Keys.Control Or Keys.A
        すべて選択AToolStripMenuItem1.Size = New Size(278, 34)
        すべて選択AToolStripMenuItem1.Text = "すべて選択(&A)"
        ' 
        ' 表示VToolStripMenuItem
        ' 
        表示VToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ツールバーTToolStripMenuItem, ステータスバーSToolStripMenuItem, ToolStripSeparator4, 最新の情報に更新RToolStripMenuItem})
        表示VToolStripMenuItem.Name = "表示VToolStripMenuItem"
        表示VToolStripMenuItem.Size = New Size(85, 29)
        表示VToolStripMenuItem.Text = "表示(&V)"
        ' 
        ' ツールバーTToolStripMenuItem
        ' 
        ツールバーTToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {エクスポートToolStripMenuItem})
        ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem"
        ツールバーTToolStripMenuItem.Size = New Size(303, 34)
        ツールバーTToolStripMenuItem.Text = "ツールバー(&T)"
        ' 
        ' エクスポートToolStripMenuItem
        ' 
        エクスポートToolStripMenuItem.Checked = True
        エクスポートToolStripMenuItem.CheckState = CheckState.Checked
        エクスポートToolStripMenuItem.Name = "エクスポートToolStripMenuItem"
        エクスポートToolStripMenuItem.Size = New Size(270, 34)
        エクスポートToolStripMenuItem.Text = "エクスポート"
        ' 
        ' ステータスバーSToolStripMenuItem
        ' 
        ステータスバーSToolStripMenuItem.Checked = True
        ステータスバーSToolStripMenuItem.CheckState = CheckState.Checked
        ステータスバーSToolStripMenuItem.Name = "ステータスバーSToolStripMenuItem"
        ステータスバーSToolStripMenuItem.Size = New Size(303, 34)
        ステータスバーSToolStripMenuItem.Text = "ステータスバー(&S)"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(300, 6)
        ' 
        ' 最新の情報に更新RToolStripMenuItem
        ' 
        最新の情報に更新RToolStripMenuItem.Name = "最新の情報に更新RToolStripMenuItem"
        最新の情報に更新RToolStripMenuItem.ShortcutKeys = Keys.F5
        最新の情報に更新RToolStripMenuItem.Size = New Size(303, 34)
        最新の情報に更新RToolStripMenuItem.Text = "最新の情報に更新(&R)"
        ' 
        ' オブジェクトOToolStripMenuItem
        ' 
        オブジェクトOToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {開くToolStripMenuItem1, ToolStripSeparator5, すべてのフィルタをクリアFToolStripMenuItem, ToolStripSeparator6, 削除DToolStripMenuItem, 更新の取り消しCToolStripMenuItem, プロパティPToolStripMenuItem, ToolStripSeparator7, エクスポートToolStripMenuItem1, レポート出力RToolStripMenuItem})
        オブジェクトOToolStripMenuItem.Name = "オブジェクトOToolStripMenuItem"
        オブジェクトOToolStripMenuItem.Size = New Size(131, 29)
        オブジェクトOToolStripMenuItem.Text = "オブジェクト(&O)"
        ' 
        ' 開くToolStripMenuItem1
        ' 
        開くToolStripMenuItem1.Name = "開くToolStripMenuItem1"
        開くToolStripMenuItem1.Size = New Size(298, 34)
        開くToolStripMenuItem1.Text = "開く(&O)"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(295, 6)
        ' 
        ' すべてのフィルタをクリアFToolStripMenuItem
        ' 
        すべてのフィルタをクリアFToolStripMenuItem.Name = "すべてのフィルタをクリアFToolStripMenuItem"
        すべてのフィルタをクリアFToolStripMenuItem.Size = New Size(298, 34)
        すべてのフィルタをクリアFToolStripMenuItem.Text = "すべてのフィルタをクリア(&F)"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(295, 6)
        ' 
        ' 削除DToolStripMenuItem
        ' 
        削除DToolStripMenuItem.Name = "削除DToolStripMenuItem"
        削除DToolStripMenuItem.ShortcutKeys = Keys.Delete
        削除DToolStripMenuItem.Size = New Size(298, 34)
        削除DToolStripMenuItem.Text = "削除(&D)"
        ' 
        ' 更新の取り消しCToolStripMenuItem
        ' 
        更新の取り消しCToolStripMenuItem.Name = "更新の取り消しCToolStripMenuItem"
        更新の取り消しCToolStripMenuItem.Size = New Size(298, 34)
        更新の取り消しCToolStripMenuItem.Text = "更新の取り消し(&C)"
        ' 
        ' プロパティPToolStripMenuItem
        ' 
        プロパティPToolStripMenuItem.Name = "プロパティPToolStripMenuItem"
        プロパティPToolStripMenuItem.Size = New Size(298, 34)
        プロパティPToolStripMenuItem.Text = "プロパティ(&P)"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(295, 6)
        ' 
        ' エクスポートToolStripMenuItem1
        ' 
        エクスポートToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {スクリプトWSToolStripMenuItem, データDToolStripMenuItem})
        エクスポートToolStripMenuItem1.Name = "エクスポートToolStripMenuItem1"
        エクスポートToolStripMenuItem1.Size = New Size(298, 34)
        エクスポートToolStripMenuItem1.Text = "エクスポート(&E)"
        ' 
        ' スクリプトWSToolStripMenuItem
        ' 
        スクリプトWSToolStripMenuItem.Name = "スクリプトWSToolStripMenuItem"
        スクリプトWSToolStripMenuItem.Size = New Size(201, 34)
        スクリプトWSToolStripMenuItem.Text = "スクリプト(&S)"
        ' 
        ' データDToolStripMenuItem
        ' 
        データDToolStripMenuItem.Name = "データDToolStripMenuItem"
        データDToolStripMenuItem.Size = New Size(201, 34)
        データDToolStripMenuItem.Text = "データ(&D)"
        ' 
        ' レポート出力RToolStripMenuItem
        ' 
        レポート出力RToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {オブジェクト一覧ToolStripMenuItem, テーブル定義ToolStripMenuItem})
        レポート出力RToolStripMenuItem.Name = "レポート出力RToolStripMenuItem"
        レポート出力RToolStripMenuItem.Size = New Size(298, 34)
        レポート出力RToolStripMenuItem.Text = "レポート出力(&R)"
        ' 
        ' オブジェクト一覧ToolStripMenuItem
        ' 
        オブジェクト一覧ToolStripMenuItem.Name = "オブジェクト一覧ToolStripMenuItem"
        オブジェクト一覧ToolStripMenuItem.Size = New Size(229, 34)
        オブジェクト一覧ToolStripMenuItem.Text = "オブジェクト一覧"
        ' 
        ' テーブル定義ToolStripMenuItem
        ' 
        テーブル定義ToolStripMenuItem.Name = "テーブル定義ToolStripMenuItem"
        テーブル定義ToolStripMenuItem.Size = New Size(229, 34)
        テーブル定義ToolStripMenuItem.Text = "テーブル定義"
        ' 
        ' ツールTToolStripMenuItem
        ' 
        ツールTToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ファイルの取り出しFToolStripMenuItem, ToolStripSeparator8, レポート定義DToolStripMenuItem, オプションOToolStripMenuItem})
        ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem"
        ツールTToolStripMenuItem.Size = New Size(87, 29)
        ツールTToolStripMenuItem.Text = "ツール(&T)"
        ' 
        ' ファイルの取り出しFToolStripMenuItem
        ' 
        ファイルの取り出しFToolStripMenuItem.Name = "ファイルの取り出しFToolStripMenuItem"
        ファイルの取り出しFToolStripMenuItem.Size = New Size(260, 34)
        ファイルの取り出しFToolStripMenuItem.Text = "ファイルの取り出し(&F)"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(257, 6)
        ' 
        ' レポート定義DToolStripMenuItem
        ' 
        レポート定義DToolStripMenuItem.Name = "レポート定義DToolStripMenuItem"
        レポート定義DToolStripMenuItem.Size = New Size(260, 34)
        レポート定義DToolStripMenuItem.Text = "レポート定義(&D)"
        ' 
        ' オプションOToolStripMenuItem
        ' 
        オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem"
        オプションOToolStripMenuItem.Size = New Size(260, 34)
        オプションOToolStripMenuItem.Text = "オプション(&O)"
        ' 
        ' ウィンドウWToolStripMenuItem
        ' 
        ウィンドウWToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {重ねて表示CToolStripMenuItem, 並べて表示TToolStripMenuItem, アイコンの整列IToolStripMenuItem, ToolStripSeparator13})
        ウィンドウWToolStripMenuItem.Name = "ウィンドウWToolStripMenuItem"
        ウィンドウWToolStripMenuItem.Size = New Size(118, 29)
        ウィンドウWToolStripMenuItem.Text = "ウィンドウ(&W)"
        ' 
        ' 重ねて表示CToolStripMenuItem
        ' 
        重ねて表示CToolStripMenuItem.Name = "重ねて表示CToolStripMenuItem"
        重ねて表示CToolStripMenuItem.Size = New Size(233, 34)
        重ねて表示CToolStripMenuItem.Text = "重ねて表示(&C)"
        ' 
        ' 並べて表示TToolStripMenuItem
        ' 
        並べて表示TToolStripMenuItem.Name = "並べて表示TToolStripMenuItem"
        並べて表示TToolStripMenuItem.Size = New Size(233, 34)
        並べて表示TToolStripMenuItem.Text = "並べて表示($T)"
        ' 
        ' アイコンの整列IToolStripMenuItem
        ' 
        アイコンの整列IToolStripMenuItem.Name = "アイコンの整列IToolStripMenuItem"
        アイコンの整列IToolStripMenuItem.Size = New Size(233, 34)
        アイコンの整列IToolStripMenuItem.Text = "アイコンの整列(&I)"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(230, 6)
        ' 
        ' ヘルプHToolStripMenuItem
        ' 
        ヘルプHToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MokujiToolStripMenuItem, ToolStripSeparator9, エラー報告RToolStripMenuItem, ToolStripSeparator10, ライセンス認証ToolStripMenuItem, バージョン情報AToolStripMenuItem})
        ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem"
        ヘルプHToolStripMenuItem.Size = New Size(95, 29)
        ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)"
        ' 
        ' MokujiToolStripMenuItem
        ' 
        MokujiToolStripMenuItem.Name = "MokujiToolStripMenuItem"
        MokujiToolStripMenuItem.Size = New Size(238, 34)
        MokujiToolStripMenuItem.Text = "目次(&I)"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(235, 6)
        ' 
        ' エラー報告RToolStripMenuItem
        ' 
        エラー報告RToolStripMenuItem.Name = "エラー報告RToolStripMenuItem"
        エラー報告RToolStripMenuItem.Size = New Size(238, 34)
        エラー報告RToolStripMenuItem.Text = "エラー報告(&R)"
        ' 
        ' ToolStripSeparator10
        ' 
        ToolStripSeparator10.Name = "ToolStripSeparator10"
        ToolStripSeparator10.Size = New Size(235, 6)
        ' 
        ' ライセンス認証ToolStripMenuItem
        ' 
        ライセンス認証ToolStripMenuItem.Name = "ライセンス認証ToolStripMenuItem"
        ライセンス認証ToolStripMenuItem.Size = New Size(238, 34)
        ライセンス認証ToolStripMenuItem.Text = "ライセンス認証(&L)"
        ' 
        ' バージョン情報AToolStripMenuItem
        ' 
        バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem"
        バージョン情報AToolStripMenuItem.Size = New Size(238, 34)
        バージョン情報AToolStripMenuItem.Text = "バージョン情報(&A)"
        ' 
        ' StatusStrip
        ' 
        StatusStrip.ImageScalingSize = New Size(24, 24)
        StatusStrip.Items.AddRange(New ToolStripItem() {ToolStripProgressBar, ToolStripStatusLabel})
        StatusStrip.Location = New Point(0, 1027)
        StatusStrip.Name = "StatusStrip"
        StatusStrip.Size = New Size(1607, 32)
        StatusStrip.TabIndex = 1
        StatusStrip.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBar
        ' 
        ToolStripProgressBar.Name = "ToolStripProgressBar"
        ToolStripProgressBar.Size = New Size(100, 24)
        ' 
        ' ToolStripStatusLabel
        ' 
        ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        ToolStripStatusLabel.Size = New Size(175, 25)
        ToolStripStatusLabel.Text = "OraDB DUMP Viewer"
        ' 
        ' ToolExport
        ' 
        ToolExport.ImageScalingSize = New Size(24, 24)
        ToolExport.Items.AddRange(New ToolStripItem() {ToolStripButton2, ToolStripButton1, ToolStripSeparator11, ToolStripButton5, ToolStripButton6, ToolStripSeparator12, ToolStripButton7, ToolStripButton8, ToolStripButton10, ToolStripButton9, ToolStripButton3})
        ToolExport.Location = New Point(0, 33)
        ToolExport.Name = "ToolExport"
        ToolExport.Size = New Size(1607, 33)
        ToolExport.TabIndex = 4
        ToolExport.Text = "ToolStrip2"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), Image)
        ToolStripButton2.ImageTransparentColor = Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(34, 28)
        ToolStripButton2.Text = "プロパティ"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), Image)
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(34, 28)
        ToolStripButton1.Text = "削除"
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(6, 33)
        ' 
        ' ToolStripButton5
        ' 
        ToolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), Image)
        ToolStripButton5.ImageTransparentColor = Color.Magenta
        ToolStripButton5.Name = "ToolStripButton5"
        ToolStripButton5.Size = New Size(34, 28)
        ToolStripButton5.Text = "スクリプト出力"
        ' 
        ' ToolStripButton6
        ' 
        ToolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), Image)
        ToolStripButton6.ImageTransparentColor = Color.Magenta
        ToolStripButton6.Name = "ToolStripButton6"
        ToolStripButton6.Size = New Size(34, 28)
        ToolStripButton6.Text = "データ出力"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(6, 33)
        ' 
        ' ToolStripButton7
        ' 
        ToolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), Image)
        ToolStripButton7.ImageTransparentColor = Color.Magenta
        ToolStripButton7.Name = "ToolStripButton7"
        ToolStripButton7.Size = New Size(34, 28)
        ToolStripButton7.Text = "Excel形式で出力"
        ' 
        ' ToolStripButton8
        ' 
        ToolStripButton8.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), Image)
        ToolStripButton8.ImageTransparentColor = Color.Magenta
        ToolStripButton8.Name = "ToolStripButton8"
        ToolStripButton8.Size = New Size(34, 28)
        ToolStripButton8.Text = "Access形式で出力"
        ' 
        ' ToolStripButton10
        ' 
        ToolStripButton10.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), Image)
        ToolStripButton10.ImageTransparentColor = Color.Magenta
        ToolStripButton10.Name = "ToolStripButton10"
        ToolStripButton10.Size = New Size(34, 28)
        ToolStripButton10.Text = "テキスト形式で出力"
        ' 
        ' ToolStripButton9
        ' 
        ToolStripButton9.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), Image)
        ToolStripButton9.ImageTransparentColor = Color.Magenta
        ToolStripButton9.Name = "ToolStripButton9"
        ToolStripButton9.Size = New Size(34, 28)
        ToolStripButton9.Text = "SQL Serverへ出力"
        ' 
        ' ToolStripButton3
        ' 
        ToolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), Image)
        ToolStripButton3.ImageTransparentColor = Color.Magenta
        ToolStripButton3.Name = "ToolStripButton3"
        ToolStripButton3.Size = New Size(34, 28)
        ToolStripButton3.Text = "ODBCで出力"
        ' 
        ' OraDB_DUMP_Viewer
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1607, 1059)
        Controls.Add(ToolExport)
        Controls.Add(StatusStrip)
        Controls.Add(MenuStrip)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip
        Name = "OraDB_DUMP_Viewer"
        Text = "OraDB DUMP Viewer"
        MenuStrip.ResumeLayout(False)
        MenuStrip.PerformLayout()
        StatusStrip.ResumeLayout(False)
        StatusStrip.PerformLayout()
        ToolExport.ResumeLayout(False)
        ToolExport.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip As MenuStrip
    Friend WithEvents ファイルFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 開くToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ワークスペースToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ダンプファイルDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 閉じるCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ワークスペースの保存SToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 名前を付けてワークスペースを保存AToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ワークスペースを閉じるLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ダンプファイルの作成GToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents 最近使ったワークスペースWToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 最近使ったダンプファイルDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 終了XToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 編集EToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 元に戻すUToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents やり直しRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 切り取りTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents コピーCToolStripMenuItem As ToolStripSeparator
    Friend WithEvents 貼り付けPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents すべて選択AToolStripMenuItem As ToolStripSeparator
    Friend WithEvents すべて選択AToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 表示VToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ツールバーTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents エクスポートToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ステータスバーSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents 最新の情報に更新RToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents オブジェクトOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 開くToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents すべてのフィルタをクリアFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents 削除DToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 更新の取り消しCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents プロパティPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents エクスポートToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents スクリプトWSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents データDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents レポート出力RToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents オブジェクト一覧ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents テーブル定義ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ツールTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ファイルの取り出しFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents レポート定義DToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents オプションOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ウィンドウWToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 重ねて表示CToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 並べて表示TToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents アイコンの整列IToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ヘルプHToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MokujiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents エラー報告RToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents バージョン情報AToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabel As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar As ToolStripProgressBar
    Friend WithEvents ライセンス認証ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolEdit As ToolStrip
    Friend WithEvents ToolExport As ToolStrip
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
End Class
