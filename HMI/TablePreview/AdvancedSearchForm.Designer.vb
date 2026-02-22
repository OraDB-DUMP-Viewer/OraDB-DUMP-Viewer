<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvancedSearchForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリアするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        mainPanel = New TableLayoutPanel()
        scrollPanel = New Panel()
        flowLayoutPanel = New FlowLayoutPanel()
        buttonPanel = New Panel()
        buttonAdd = New Button()
        buttonClear = New Button()
        buttonSearch = New Button()
        buttonCancel = New Button()
        templateConditionRow = New SearchConditionRow(New List(Of String)())
        templateLogicalPanel = New Panel()
        mainPanel.SuspendLayout()
        scrollPanel.SuspendLayout()
        buttonPanel.SuspendLayout()
        SuspendLayout()
        ' 
        ' mainPanel
        ' 
        mainPanel.ColumnCount = 1
        mainPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        mainPanel.Controls.Add(scrollPanel, 0, 0)
        mainPanel.Controls.Add(buttonPanel, 0, 1)
        mainPanel.Dock = DockStyle.Fill
        mainPanel.Location = New Point(0, 0)
        mainPanel.Margin = New Padding(5, 6, 5, 6)
        mainPanel.Name = "mainPanel"
        mainPanel.RowCount = 2
        mainPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        mainPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 115F))
        mainPanel.Size = New Size(1500, 1154)
        mainPanel.TabIndex = 0
        ' 
        ' scrollPanel
        ' 
        scrollPanel.AutoScroll = True
        scrollPanel.Controls.Add(flowLayoutPanel)
        scrollPanel.Dock = DockStyle.Fill
        scrollPanel.Location = New Point(5, 6)
        scrollPanel.Margin = New Padding(5, 6, 5, 6)
        scrollPanel.Name = "scrollPanel"
        scrollPanel.Size = New Size(1490, 1027)
        scrollPanel.TabIndex = 0
        ' 
        ' flowLayoutPanel
        ' 
        flowLayoutPanel.AutoSize = True
        flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink
        flowLayoutPanel.BackColor = SystemColors.Control
        flowLayoutPanel.FlowDirection = FlowDirection.TopDown
        flowLayoutPanel.Location = New Point(17, 19)
        flowLayoutPanel.Margin = New Padding(5, 6, 5, 6)
        flowLayoutPanel.Name = "flowLayoutPanel"
        flowLayoutPanel.Padding = New Padding(17, 19, 17, 19)
        flowLayoutPanel.Size = New Size(34, 38)
        flowLayoutPanel.TabIndex = 0
        ' 
        ' buttonPanel
        ' 
        buttonPanel.Controls.Add(buttonSearch)
        buttonPanel.Controls.Add(buttonCancel)
        buttonPanel.Controls.Add(buttonClear)
        buttonPanel.Controls.Add(buttonAdd)
        buttonPanel.Dock = DockStyle.Fill
        buttonPanel.Location = New Point(5, 1045)
        buttonPanel.Margin = New Padding(5, 6, 5, 6)
        buttonPanel.Name = "buttonPanel"
        buttonPanel.Size = New Size(1490, 103)
        buttonPanel.TabIndex = 1
        ' 
        ' buttonAdd
        ' 
        buttonAdd.Location = New Point(200, 19)
        buttonAdd.Margin = New Padding(5, 6, 5, 6)
        buttonAdd.Name = "buttonAdd"
        buttonAdd.Size = New Size(167, 77)
        buttonAdd.TabIndex = 0
        buttonAdd.Text = "条件を追加"
        buttonAdd.UseVisualStyleBackColor = True
        ' 
        ' buttonClear
        ' 
        buttonClear.Location = New Point(383, 19)
        buttonClear.Margin = New Padding(5, 6, 5, 6)
        buttonClear.Name = "buttonClear"
        buttonClear.Size = New Size(167, 77)
        buttonClear.TabIndex = 1
        buttonClear.Text = "クリア"
        buttonClear.UseVisualStyleBackColor = True
        ' 
        ' buttonSearch
        ' 
        buttonSearch.BackColor = SystemColors.Highlight
        buttonSearch.DialogResult = DialogResult.OK
        buttonSearch.ForeColor = Color.White
        buttonSearch.Location = New Point(17, 19)
        buttonSearch.Margin = New Padding(5, 6, 5, 6)
        buttonSearch.Name = "buttonSearch"
        buttonSearch.Size = New Size(167, 77)
        buttonSearch.TabIndex = 2
        buttonSearch.Text = "検索"
        buttonSearch.UseVisualStyleBackColor = False
        ' 
        ' buttonCancel
        ' 
        buttonCancel.DialogResult = DialogResult.Cancel
        buttonCancel.Location = New Point(567, 19)
        buttonCancel.Margin = New Padding(5, 6, 5, 6)
        buttonCancel.Name = "buttonCancel"
        buttonCancel.Size = New Size(167, 77)
        buttonCancel.TabIndex = 3
        buttonCancel.Text = "キャンセル"
        buttonCancel.UseVisualStyleBackColor = True
        ' 
        ' templateConditionRow
        ' 
        templateConditionRow.AutoSize = True
        templateConditionRow.BackColor = SystemColors.Control
        templateConditionRow.Location = New Point(0, 0)
        templateConditionRow.Margin = New Padding(5, 6, 5, 6)
        templateConditionRow.Name = "templateConditionRow"
        templateConditionRow.Padding = New Padding(8, 10, 8, 10)
        templateConditionRow.Size = New Size(1200, 77)
        templateConditionRow.TabIndex = 0
        templateConditionRow.Visible = False
        ' 
        ' templateLogicalPanel
        ' 
        templateLogicalPanel.AutoSize = True
        templateLogicalPanel.BackColor = SystemColors.Control
        templateLogicalPanel.Location = New Point(0, 0)
        templateLogicalPanel.Margin = New Padding(5, 6, 5, 6)
        templateLogicalPanel.Name = "templateLogicalPanel"
        templateLogicalPanel.Size = New Size(0, 0)
        templateLogicalPanel.TabIndex = 0
        templateLogicalPanel.Visible = False
        ' 
        ' AdvancedSearchForm
        ' 
        AcceptButton = buttonSearch
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = buttonCancel
        ClientSize = New Size(1500, 1154)
        Controls.Add(mainPanel)
        Controls.Add(templateConditionRow)
        Controls.Add(templateLogicalPanel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AdvancedSearchForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "高度な検索"
        mainPanel.ResumeLayout(False)
        scrollPanel.ResumeLayout(False)
        scrollPanel.PerformLayout()
        buttonPanel.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents mainPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents scrollPanel As System.Windows.Forms.Panel
    Friend WithEvents flowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents buttonPanel As System.Windows.Forms.Panel
    Friend WithEvents buttonAdd As System.Windows.Forms.Button
    Friend WithEvents buttonClear As System.Windows.Forms.Button
    Friend WithEvents buttonSearch As System.Windows.Forms.Button
    Friend WithEvents buttonCancel As System.Windows.Forms.Button
    Friend WithEvents templateConditionRow As SearchConditionRow
    Friend WithEvents templateLogicalPanel As System.Windows.Forms.Panel

End Class
