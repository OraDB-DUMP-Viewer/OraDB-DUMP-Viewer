Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class MenuStripLogics
    Public Shared Function ダンプファイルDToolStripMenuItem() As String
        Try
            'OpenFileDialogのインスタンスを作成
            Using OpenFileDialog As New OpenFileDialog
                OpenFileDialog.Title = "Oracle DUMPファイルを選択してください"
                OpenFileDialog.Filter = "Oracleダンプファイル (*.dmp)|*.dmp|すべてのファイル (*.*)|*.*"
                OpenFileDialog.FilterIndex = 1
                OpenFileDialog.RestoreDirectory = True
                OpenFileDialog.CheckFileExists = True
                OpenFileDialog.CheckPathExists = True

                '初期フォルダをデスクトップに設定
                OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

                'ダイアログを表示
                If OpenFileDialog.ShowDialog() = DialogResult.OK Then
                    Dim dumpFilePath As String = OpenFileDialog.FileName

                    'ファイルパスを返す
                    Return dumpFilePath

                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("ファイル選択中にエラーが発生しました: " & ex.Message,
                            "エラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

        'キャンセルまたはエラー時は空文字を返す
        Return String.Empty
    End Function

    Public Shared Sub ステータスバーSToolStripMenuItem()
        'ステータスバーの表示・非表示を切り替える
        If Oracle_DUMP_Viewer.ToolStripStatusLabel.Visible = True Then
            Oracle_DUMP_Viewer.ToolStripStatusLabel.Visible = False
            Oracle_DUMP_Viewer.ステータスバーSToolStripMenuItem.Checked = False
        Else
            Oracle_DUMP_Viewer.ToolStripStatusLabel.Visible = True
            Oracle_DUMP_Viewer.ステータスバーSToolStripMenuItem.Checked = True
        End If
    End Sub

    ' Use COMMON for shared license logic

    

    Public Shared Sub ライセンス認証ToolStripMenuItem()
        Try
            Using OpenFileDialog As New OpenFileDialog
                OpenFileDialog.Title = "ライセンスファイル（*.lic.json）を選択してください"
                OpenFileDialog.Filter = "ライセンスファイル (*.lic.json)|*.lic.json|すべてのファイル (*.*)|*.*"
                OpenFileDialog.FilterIndex = 1
                OpenFileDialog.RestoreDirectory = True
                OpenFileDialog.CheckFileExists = True
                OpenFileDialog.CheckPathExists = True
                OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

                If OpenFileDialog.ShowDialog() = DialogResult.OK Then
                    Dim licPath As String = OpenFileDialog.FileName
                    Dim product As String = String.Empty
                    Dim expiryDate As DateTime
                    Dim holder As String = String.Empty
                    Dim errMsg As String = String.Empty
                    If Not LICENSE.VerifyLicenseFile(licPath, product, expiryDate, holder, errMsg) Then
                        MessageBox.Show(errMsg, "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                    ' 認証成功時はAppDataにそのまま保存
                    Dim appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OracleDUMPViewer")
                    If Not Directory.Exists(appData) Then Directory.CreateDirectory(appData)
                    Dim statusPath = Path.Combine(appData, "license.status")
                    File.Copy(licPath, statusPath, True)
                    MessageBox.Show("ライセンス認証に成功しました。", "認証成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("ライセンス認証中にエラーが発生しました: " & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
