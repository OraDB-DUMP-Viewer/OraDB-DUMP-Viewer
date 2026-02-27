<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TablePropertyDialog
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
        tblMain = New TableLayoutPanel()
        lblSchemaCaption = New Label()
        lblSchemaValue = New Label()
        lblTableCaption = New Label()
        lblTableValue = New Label()
        lblColumnCountCaption = New Label()
        lblColumnCountValue = New Label()
        lblRowCountCaption = New Label()
        lblRowCountValue = New Label()
        lblColumnsCaption = New Label()
        lstColumns = New ListView()
        colNo = New ColumnHeader()
        colName = New ColumnHeader()
        colType = New ColumnHeader()
        btnClose = New Button()
        tblMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblMain
        ' 
        tblMain.ColumnCount = 2
        tblMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        tblMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblMain.Controls.Add(lblSchemaCaption, 0, 0)
        tblMain.Controls.Add(lblSchemaValue, 1, 0)
        tblMain.Controls.Add(lblTableCaption, 0, 1)
        tblMain.Controls.Add(lblTableValue, 1, 1)
        tblMain.Controls.Add(lblColumnCountCaption, 0, 2)
        tblMain.Controls.Add(lblColumnCountValue, 1, 2)
        tblMain.Controls.Add(lblRowCountCaption, 0, 3)
        tblMain.Controls.Add(lblRowCountValue, 1, 3)
        tblMain.Controls.Add(lblColumnsCaption, 0, 4)
        tblMain.Controls.Add(lstColumns, 1, 4)
        tblMain.Dock = DockStyle.Fill
        tblMain.Location = New Point(0, 0)
        tblMain.Name = "tblMain"
        tblMain.Padding = New Padding(12)
        tblMain.RowCount = 5
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblMain.Size = New Size(600, 415)
        tblMain.TabIndex = 0
        ' 
        ' lblSchemaCaption
        ' 
        lblSchemaCaption.Dock = DockStyle.Fill
        lblSchemaCaption.Location = New Point(15, 12)
        lblSchemaCaption.Name = "lblSchemaCaption"
        lblSchemaCaption.Size = New Size(114, 30)
        lblSchemaCaption.TabIndex = 0
        lblSchemaCaption.Text = "スキーマ:"
        lblSchemaCaption.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSchemaValue
        ' 
        lblSchemaValue.Dock = DockStyle.Fill
        lblSchemaValue.Font = New Font("Yu Gothic UI", 9F, FontStyle.Bold)
        lblSchemaValue.Location = New Point(135, 12)
        lblSchemaValue.Name = "lblSchemaValue"
        lblSchemaValue.Size = New Size(450, 30)
        lblSchemaValue.TabIndex = 1
        lblSchemaValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTableCaption
        ' 
        lblTableCaption.Dock = DockStyle.Fill
        lblTableCaption.Location = New Point(15, 42)
        lblTableCaption.Name = "lblTableCaption"
        lblTableCaption.Size = New Size(114, 30)
        lblTableCaption.TabIndex = 2
        lblTableCaption.Text = "テーブル名:"
        lblTableCaption.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblTableValue
        ' 
        lblTableValue.Dock = DockStyle.Fill
        lblTableValue.Font = New Font("Yu Gothic UI", 9F, FontStyle.Bold)
        lblTableValue.Location = New Point(135, 42)
        lblTableValue.Name = "lblTableValue"
        lblTableValue.Size = New Size(450, 30)
        lblTableValue.TabIndex = 3
        lblTableValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblColumnCountCaption
        ' 
        lblColumnCountCaption.Dock = DockStyle.Fill
        lblColumnCountCaption.Location = New Point(15, 72)
        lblColumnCountCaption.Name = "lblColumnCountCaption"
        lblColumnCountCaption.Size = New Size(114, 30)
        lblColumnCountCaption.TabIndex = 4
        lblColumnCountCaption.Text = "カラム数:"
        lblColumnCountCaption.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblColumnCountValue
        ' 
        lblColumnCountValue.Dock = DockStyle.Fill
        lblColumnCountValue.Font = New Font("Yu Gothic UI", 9F, FontStyle.Bold)
        lblColumnCountValue.Location = New Point(135, 72)
        lblColumnCountValue.Name = "lblColumnCountValue"
        lblColumnCountValue.Size = New Size(450, 30)
        lblColumnCountValue.TabIndex = 5
        lblColumnCountValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblRowCountCaption
        ' 
        lblRowCountCaption.Dock = DockStyle.Fill
        lblRowCountCaption.Location = New Point(15, 102)
        lblRowCountCaption.Name = "lblRowCountCaption"
        lblRowCountCaption.Size = New Size(114, 30)
        lblRowCountCaption.TabIndex = 6
        lblRowCountCaption.Text = "行数:"
        lblRowCountCaption.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblRowCountValue
        ' 
        lblRowCountValue.Dock = DockStyle.Fill
        lblRowCountValue.Font = New Font("Yu Gothic UI", 9F, FontStyle.Bold)
        lblRowCountValue.Location = New Point(135, 102)
        lblRowCountValue.Name = "lblRowCountValue"
        lblRowCountValue.Size = New Size(450, 30)
        lblRowCountValue.TabIndex = 7
        lblRowCountValue.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblColumnsCaption
        ' 
        lblColumnsCaption.Dock = DockStyle.Fill
        lblColumnsCaption.Location = New Point(15, 132)
        lblColumnsCaption.Name = "lblColumnsCaption"
        lblColumnsCaption.Size = New Size(114, 271)
        lblColumnsCaption.TabIndex = 8
        lblColumnsCaption.Text = "カラム一覧:"
        lblColumnsCaption.TextAlign = ContentAlignment.TopRight
        ' 
        ' lstColumns
        ' 
        lstColumns.Columns.AddRange(New ColumnHeader() {colNo, colName, colType})
        lstColumns.Dock = DockStyle.Fill
        lstColumns.FullRowSelect = True
        lstColumns.GridLines = True
        lstColumns.Location = New Point(135, 135)
        lstColumns.Name = "lstColumns"
        lstColumns.Size = New Size(450, 265)
        lstColumns.TabIndex = 9
        lstColumns.UseCompatibleStateImageBehavior = False
        lstColumns.View = View.Details
        ' 
        ' colNo
        ' 
        colNo.Text = "#"
        colNo.Width = 40
        ' 
        ' colName
        ' 
        colName.Text = "カラム名"
        colName.Width = 200
        ' 
        ' colType
        ' 
        colType.Text = "型"
        colType.Width = 150
        ' 
        ' btnClose
        ' 
        btnClose.DialogResult = DialogResult.OK
        btnClose.Dock = DockStyle.Bottom
        btnClose.Location = New Point(0, 415)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(600, 35)
        btnClose.TabIndex = 1
        btnClose.Text = "閉じる"
        ' 
        ' TablePropertyDialog
        ' 
        AcceptButton = btnClose
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(600, 450)
        Controls.Add(tblMain)
        Controls.Add(btnClose)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "TablePropertyDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "テーブルプロパティ"
        tblMain.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents lblSchemaCaption As Label
    Friend WithEvents lblSchemaValue As Label
    Friend WithEvents lblTableCaption As Label
    Friend WithEvents lblTableValue As Label
    Friend WithEvents lblColumnCountCaption As Label
    Friend WithEvents lblColumnCountValue As Label
    Friend WithEvents lblRowCountCaption As Label
    Friend WithEvents lblRowCountValue As Label
    Friend WithEvents lblColumnsCaption As Label
    Friend WithEvents lstColumns As ListView
    Friend WithEvents colNo As ColumnHeader
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents colType As ColumnHeader
    Friend WithEvents btnClose As Button

End Class
