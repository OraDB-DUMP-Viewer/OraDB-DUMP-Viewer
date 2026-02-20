Imports System.Collections.Generic

Public Class TablePreviewLogic

    ''' <summary>
    ''' テーブルデータをTablePreviewフォームで表示する
    ''' 親フォーム参照から新しいTablePreviewを作成して表示する
    ''' </summary>
    ''' <param name="parentForm">親となるMDIフォーム</param>
    ''' <param name="tableData">テーブルデータ（行ごとにDictionary&lt;列名, 値&gt;のリスト）</param>
    ''' <param name="columnNames">列名のリスト</param>
    ''' <param name="tableName">テーブル名</param>
    Public Shared Sub DisplayTableData(parentForm As Form, tableData As List(Of Dictionary(Of String, Object)), 
                                       columnNames As List(Of String), tableName As String)
        Try
            If tableData Is Nothing OrElse tableData.Count = 0 Then
                MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If columnNames Is Nothing OrElse columnNames.Count = 0 Then
                MessageBox.Show("列情報がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' TablePreviewフォームを作成
            Dim previewForm As New TablePreview(tableData, columnNames, tableName)
            previewForm.MdiParent = parentForm
            previewForm.Show()

        Catch ex As Exception
            MessageBox.Show("データ表示中にエラーが発生しました: " & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
