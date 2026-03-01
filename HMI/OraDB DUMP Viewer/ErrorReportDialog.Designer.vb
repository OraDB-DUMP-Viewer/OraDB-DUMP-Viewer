<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErrorReportDialog
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        tblMain = New TableLayoutPanel()
        lblTitleCaption = New Label()
        txtTitle = New TextBox()
        lblDescCaption = New Label()
        txtDescription = New TextBox()
        lblContactCaption = New Label()
        txtContact = New TextBox()
        lblContactHint = New Label()
        lblSysInfoHeader = New Label()
        lblVersionCaption = New Label()
        lblVersionValue = New Label()
        lblOSCaption = New Label()
        lblOSValue = New Label()
        lblDotNetCaption = New Label()
        lblDotNetValue = New Label()
        pnlButtons = New Panel()
        lnkIssue = New LinkLabel()
        lblStatus = New Label()
        prgSubmit = New ProgressBar()
        btnCancel = New Button()
        btnSubmit = New Button()
        tblMain.SuspendLayout()
        pnlButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblMain
        ' 
        tblMain.ColumnCount = 2
        tblMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 143F))
        tblMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblMain.Controls.Add(lblTitleCaption, 0, 0)
        tblMain.Controls.Add(txtTitle, 1, 0)
        tblMain.Controls.Add(lblDescCaption, 0, 1)
        tblMain.Controls.Add(txtDescription, 1, 1)
        tblMain.Controls.Add(lblContactCaption, 0, 2)
        tblMain.Controls.Add(txtContact, 1, 2)
        tblMain.Controls.Add(lblContactHint, 1, 3)
        tblMain.Controls.Add(lblSysInfoHeader, 0, 4)
        tblMain.Controls.Add(lblVersionCaption, 0, 5)
        tblMain.Controls.Add(lblVersionValue, 1, 5)
        tblMain.Controls.Add(lblOSCaption, 0, 6)
        tblMain.Controls.Add(lblOSValue, 1, 6)
        tblMain.Controls.Add(lblDotNetCaption, 0, 7)
        tblMain.Controls.Add(lblDotNetValue, 1, 7)
        tblMain.Dock = DockStyle.Fill
        tblMain.Location = New Point(0, 0)
        tblMain.Margin = New Padding(4, 5, 4, 5)
        tblMain.Name = "tblMain"
        tblMain.Padding = New Padding(17, 20, 17, 7)
        tblMain.RowCount = 8
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 33F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 47F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tblMain.Size = New Size(743, 566)
        tblMain.TabIndex = 0
        ' 
        ' lblTitleCaption
        ' 
        lblTitleCaption.Dock = DockStyle.Fill
        lblTitleCaption.Location = New Point(21, 20)
        lblTitleCaption.Margin = New Padding(4, 0, 4, 0)
        lblTitleCaption.Name = "lblTitleCaption"
        lblTitleCaption.Size = New Size(135, 50)
        lblTitleCaption.TabIndex = 0
        lblTitleCaption.Text = "件名:"
        lblTitleCaption.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtTitle
        ' 
        txtTitle.Dock = DockStyle.Fill
        txtTitle.Location = New Point(164, 25)
        txtTitle.Margin = New Padding(4, 5, 4, 5)
        txtTitle.MaxLength = 100
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(558, 31)
        txtTitle.TabIndex = 1
        ' 
        ' lblDescCaption
        ' 
        lblDescCaption.Location = New Point(21, 70)
        lblDescCaption.Margin = New Padding(4, 0, 4, 0)
        lblDescCaption.Name = "lblDescCaption"
        lblDescCaption.Padding = New Padding(0, 7, 0, 0)
        lblDescCaption.Size = New Size(134, 38)
        lblDescCaption.TabIndex = 2
        lblDescCaption.Text = "内容:"
        ' 
        ' txtDescription
        ' 
        txtDescription.Dock = DockStyle.Fill
        txtDescription.Location = New Point(164, 75)
        txtDescription.Margin = New Padding(4, 5, 4, 5)
        txtDescription.MaxLength = 5000
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Size = New Size(558, 229)
        txtDescription.TabIndex = 3
        ' 
        ' lblContactCaption
        ' 
        lblContactCaption.Dock = DockStyle.Fill
        lblContactCaption.Location = New Point(21, 309)
        lblContactCaption.Margin = New Padding(4, 0, 4, 0)
        lblContactCaption.Name = "lblContactCaption"
        lblContactCaption.Size = New Size(135, 50)
        lblContactCaption.TabIndex = 4
        lblContactCaption.Text = "連絡先:"
        lblContactCaption.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtContact
        ' 
        txtContact.Dock = DockStyle.Fill
        txtContact.Location = New Point(164, 314)
        txtContact.Margin = New Padding(4, 5, 4, 5)
        txtContact.MaxLength = 254
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(558, 31)
        txtContact.TabIndex = 5
        ' 
        ' lblContactHint
        ' 
        lblContactHint.Dock = DockStyle.Fill
        lblContactHint.ForeColor = Color.Gray
        lblContactHint.Location = New Point(164, 359)
        lblContactHint.Margin = New Padding(4, 0, 4, 0)
        lblContactHint.Name = "lblContactHint"
        lblContactHint.Size = New Size(558, 33)
        lblContactHint.TabIndex = 6
        lblContactHint.Text = "(任意) メールアドレスなど、返信先をご記入ください"
        ' 
        ' lblSysInfoHeader
        ' 
        tblMain.SetColumnSpan(lblSysInfoHeader, 2)
        lblSysInfoHeader.Dock = DockStyle.Fill
        lblSysInfoHeader.ForeColor = Color.Gray
        lblSysInfoHeader.Location = New Point(21, 392)
        lblSysInfoHeader.Margin = New Padding(4, 0, 4, 0)
        lblSysInfoHeader.Name = "lblSysInfoHeader"
        lblSysInfoHeader.Size = New Size(701, 47)
        lblSysInfoHeader.TabIndex = 7
        lblSysInfoHeader.Text = "── 以下の環境情報が自動的に送信されます ──"
        lblSysInfoHeader.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblVersionCaption
        ' 
        lblVersionCaption.Dock = DockStyle.Fill
        lblVersionCaption.ForeColor = Color.Gray
        lblVersionCaption.Location = New Point(21, 439)
        lblVersionCaption.Margin = New Padding(4, 0, 4, 0)
        lblVersionCaption.Name = "lblVersionCaption"
        lblVersionCaption.Size = New Size(135, 40)
        lblVersionCaption.TabIndex = 8
        lblVersionCaption.Text = "バージョン:"
        lblVersionCaption.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblVersionValue
        ' 
        lblVersionValue.Dock = DockStyle.Fill
        lblVersionValue.ForeColor = Color.Gray
        lblVersionValue.Location = New Point(164, 439)
        lblVersionValue.Margin = New Padding(4, 0, 4, 0)
        lblVersionValue.Name = "lblVersionValue"
        lblVersionValue.Size = New Size(558, 40)
        lblVersionValue.TabIndex = 9
        lblVersionValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblOSCaption
        ' 
        lblOSCaption.Dock = DockStyle.Fill
        lblOSCaption.ForeColor = Color.Gray
        lblOSCaption.Location = New Point(21, 479)
        lblOSCaption.Margin = New Padding(4, 0, 4, 0)
        lblOSCaption.Name = "lblOSCaption"
        lblOSCaption.Size = New Size(135, 40)
        lblOSCaption.TabIndex = 10
        lblOSCaption.Text = "OS:"
        lblOSCaption.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblOSValue
        ' 
        lblOSValue.Dock = DockStyle.Fill
        lblOSValue.ForeColor = Color.Gray
        lblOSValue.Location = New Point(164, 479)
        lblOSValue.Margin = New Padding(4, 0, 4, 0)
        lblOSValue.Name = "lblOSValue"
        lblOSValue.Size = New Size(558, 40)
        lblOSValue.TabIndex = 11
        lblOSValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDotNetCaption
        ' 
        lblDotNetCaption.Dock = DockStyle.Fill
        lblDotNetCaption.ForeColor = Color.Gray
        lblDotNetCaption.Location = New Point(21, 519)
        lblDotNetCaption.Margin = New Padding(4, 0, 4, 0)
        lblDotNetCaption.Name = "lblDotNetCaption"
        lblDotNetCaption.Size = New Size(135, 40)
        lblDotNetCaption.TabIndex = 12
        lblDotNetCaption.Text = ".NET:"
        lblDotNetCaption.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDotNetValue
        ' 
        lblDotNetValue.Dock = DockStyle.Fill
        lblDotNetValue.ForeColor = Color.Gray
        lblDotNetValue.Location = New Point(164, 519)
        lblDotNetValue.Margin = New Padding(4, 0, 4, 0)
        lblDotNetValue.Name = "lblDotNetValue"
        lblDotNetValue.Size = New Size(558, 40)
        lblDotNetValue.TabIndex = 13
        lblDotNetValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlButtons
        ' 
        pnlButtons.Controls.Add(lnkIssue)
        pnlButtons.Controls.Add(lblStatus)
        pnlButtons.Controls.Add(prgSubmit)
        pnlButtons.Controls.Add(btnCancel)
        pnlButtons.Controls.Add(btnSubmit)
        pnlButtons.Dock = DockStyle.Bottom
        pnlButtons.Location = New Point(0, 566)
        pnlButtons.Margin = New Padding(4, 5, 4, 5)
        pnlButtons.Name = "pnlButtons"
        pnlButtons.Padding = New Padding(17, 7, 17, 13)
        pnlButtons.Size = New Size(743, 167)
        pnlButtons.TabIndex = 1
        ' 
        ' lnkIssue
        ' 
        lnkIssue.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lnkIssue.Location = New Point(457, 113)
        lnkIssue.Margin = New Padding(4, 0, 4, 0)
        lnkIssue.Name = "lnkIssue"
        lnkIssue.Size = New Size(266, 33)
        lnkIssue.TabIndex = 4
        lnkIssue.TextAlign = ContentAlignment.MiddleRight
        lnkIssue.Visible = False
        ' 
        ' lblStatus
        ' 
        lblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblStatus.Location = New Point(21, 113)
        lblStatus.Margin = New Padding(4, 0, 4, 0)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(557, 33)
        lblStatus.TabIndex = 3
        lblStatus.Visible = False
        ' 
        ' prgSubmit
        ' 
        prgSubmit.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        prgSubmit.Location = New Point(21, 73)
        prgSubmit.Margin = New Padding(4, 5, 4, 5)
        prgSubmit.Name = "prgSubmit"
        prgSubmit.Size = New Size(700, 30)
        prgSubmit.Style = ProgressBarStyle.Marquee
        prgSubmit.TabIndex = 2
        prgSubmit.Visible = False
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.Location = New Point(469, 12)
        btnCancel.Margin = New Padding(4, 5, 4, 5)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(123, 50)
        btnCancel.TabIndex = 1
        btnCancel.Text = "キャンセル"
        ' 
        ' btnSubmit
        ' 
        btnSubmit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSubmit.Location = New Point(600, 12)
        btnSubmit.Margin = New Padding(4, 5, 4, 5)
        btnSubmit.Name = "btnSubmit"
        btnSubmit.Size = New Size(123, 50)
        btnSubmit.TabIndex = 0
        btnSubmit.Text = "送信"
        ' 
        ' ErrorReportDialog
        ' 
        AcceptButton = btnSubmit
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(743, 733)
        Controls.Add(tblMain)
        Controls.Add(pnlButtons)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ErrorReportDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "エラー報告"
        tblMain.ResumeLayout(False)
        tblMain.PerformLayout()
        pnlButtons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents lblTitleCaption As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblDescCaption As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents lblContactCaption As Label
    Friend WithEvents txtContact As TextBox
    Friend WithEvents lblContactHint As Label
    Friend WithEvents lblSysInfoHeader As Label
    Friend WithEvents lblVersionCaption As Label
    Friend WithEvents lblVersionValue As Label
    Friend WithEvents lblOSCaption As Label
    Friend WithEvents lblOSValue As Label
    Friend WithEvents lblDotNetCaption As Label
    Friend WithEvents lblDotNetValue As Label
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents prgSubmit As ProgressBar
    Friend WithEvents lblStatus As Label
    Friend WithEvents lnkIssue As LinkLabel

End Class
