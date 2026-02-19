Imports System.IO
Imports System.Text

Public Class Oracle_DUMP_Viewer

    ''' <summary>
    ''' フォームロードイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Oracle_DUMP_Viewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初期化処理をここに記述

        'ステータスラベルのテキストをリセットする
        COMMON.ReSet_StatusLavel()
        'プログレスバーをリセットする
        COMMON.ResetProgressBar()

        '起動時ライセンスチェック（RSA署名方式）
        Try
            Dim appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OracleDUMPViewer")
            Dim statusPath = Path.Combine(appData, "license.status")

            If Not File.Exists(statusPath) Then
                Dim res As DialogResult = MessageBox.Show("ライセンスが見つかりません。今すぐライセンス認証しますか？", "ライセンス未登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If res = DialogResult.Yes Then
                    MenuStripLogics.ライセンス認証ToolStripMenuItem()
                End If
                Return
            End If

            Dim product As String = String.Empty
            Dim expiryDate As DateTime
            Dim holder As String = String.Empty
            Dim errMsg As String = String.Empty
            If Not LICENSE.VerifyLicenseFile(statusPath, product, expiryDate, holder, errMsg) Then
                MessageBox.Show("ライセンス検証に失敗しました: " & errMsg & vbCrLf & "今すぐライセンス認証しますか？", "ライセンス無効", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                MenuStripLogics.ライセンス認証ToolStripMenuItem()
            End If
        Catch ex As Exception
            MessageBox.Show("起動時ライセンスチェック中にエラーが発生しました: " & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
            Return
        End Try

    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「ダンプファイル(D)」クリックイベント
    ''' クリックされると、ダンプファイルのパスを取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ダンプファイルDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ダンプファイルDToolStripMenuItem.Click
        Dim filePath As String = String.Empty

        'ステータスラベルのテキストを更新する
        COMMON.Set_StatusLavel("ダンプファイルのパスを選択してください...")

        'ダンプファイルのパスを取得する
        filePath = MenuStripLogics.ダンプファイルDToolStripMenuItem()

        If filePath = String.Empty Then
            'キャンセルされた場合、ステータスラベルのテキストをリセットする
            COMMON.ReSet_StatusLavel()
            Return
        End If

        Dim childForm As New Workspace(filePath, "")
        childForm.MdiParent = Me   ' 親フォームを指定
        childForm.Show()

        'ステータスラベルのテキストをリセットする
        COMMON.ReSet_StatusLavel()
    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「ステータスバー(S)」クリックイベント
    ''' クリックされると、ステータスバーの表示/非表示を切り替える
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ステータスバーSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ステータスバーSToolStripMenuItem.Click
        'ステータスバーの表示/非表示を切り替える
        MenuStripLogics.ステータスバーSToolStripMenuItem()
    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「エクスポート」クリックイベント
    ''' クリックされると、エクスポートツールバーの表示/非表示を切り替える
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub エクスポートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles エクスポートToolStripMenuItem.Click
        'エクスポートツールバーの表示/非表示を切り替える
        ToolExport.Visible = Not ToolExport.Visible
        エクスポートToolStripMenuItem.Checked = ToolExport.Visible
    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「ライセンス認証(L)」クリックイベント
    ''' クリックされると、ヘルプを表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ライセンス認証ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ライセンス認証ToolStripMenuItem.Click

        ' ライセンス認証ロジックを呼び出し
        MenuStripLogics.ライセンス認証ToolStripMenuItem()

    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「重ねて表示(C)」クリックイベント
    ''' クリックされると、MDI子ウィンドウを重ねて表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 重ねて表示CToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重ねて表示CToolStripMenuItem.Click
        'MDI子ウィンドウを重ねて表示する
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「並べて表示(T)」クリックイベント
    ''' クリックされると、MDI子ウィンドウを並べて表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 並べて表示TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 並べて表示TToolStripMenuItem.Click
        'MDI子ウィンドウを並べて表示する
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    ''' <summary>
    ''' ToolStripMenuItem「アイコンの整列(I)」クリックイベント
    ''' クリックされると、最小化されたMDI子ウィンドウのアイコンを整列する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub アイコンの整列IToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles アイコンの整列IToolStripMenuItem.Click
        'MDI子ウィンドウのアイコンを整列する
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub 終了XToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 終了XToolStripMenuItem.Click
        'アプリケーションを終了する
        Application.Exit()
    End Sub
End Class