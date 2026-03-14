<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MaskingConfigDialog
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblFilePath = New Label()
        txtFilePath = New TextBox()
        btnOpen = New Button()
        btnNew = New Button()
        btnSave = New Button()
        lblDescription = New Label()
        txtDescription = New TextBox()
        lblDefaultMask = New Label()
        txtDefaultMask = New TextBox()
        grpTables = New GroupBox()
        lstTables = New ListBox()
        grpColumns = New GroupBox()
        dgvColumns = New DataGridView()
        colCheck = New DataGridViewCheckBoxColumn()
        colColumnName = New DataGridViewTextBoxColumn()
        colColumnType = New DataGridViewTextBoxColumn()
        colMaskValue = New DataGridViewTextBoxColumn()
        btnOK = New Button()
        btnCancel = New Button()
        splitMain = New SplitContainer()

        grpTables.SuspendLayout()
        grpColumns.SuspendLayout()
        CType(dgvColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        splitMain.Panel1.SuspendLayout()
        splitMain.Panel2.SuspendLayout()
        splitMain.SuspendLayout()
        SuspendLayout()

        ' lblFilePath
        lblFilePath.AutoSize = True
        lblFilePath.Location = New Point(15, 15)
        lblFilePath.Name = "lblFilePath"
        lblFilePath.Text = "定義ファイル:"

        ' txtFilePath
        txtFilePath.Location = New Point(100, 12)
        txtFilePath.Name = "txtFilePath"
        txtFilePath.Size = New Size(380, 23)
        txtFilePath.ReadOnly = True
        txtFilePath.BackColor = SystemColors.Window

        ' btnOpen
        btnOpen.Location = New Point(485, 11)
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(60, 25)
        btnOpen.Text = "開く"
        btnOpen.UseVisualStyleBackColor = True

        ' btnNew
        btnNew.Location = New Point(550, 11)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(60, 25)
        btnNew.Text = "新規"
        btnNew.UseVisualStyleBackColor = True

        ' btnSave
        btnSave.Location = New Point(615, 11)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(60, 25)
        btnSave.Text = "保存"
        btnSave.UseVisualStyleBackColor = True

        ' lblDescription
        lblDescription.AutoSize = True
        lblDescription.Location = New Point(15, 45)
        lblDescription.Name = "lblDescription"
        lblDescription.Text = "説明:"

        ' txtDescription
        txtDescription.Location = New Point(100, 42)
        txtDescription.Name = "txtDescription"
        txtDescription.Size = New Size(380, 23)

        ' lblDefaultMask
        lblDefaultMask.AutoSize = True
        lblDefaultMask.Location = New Point(500, 45)
        lblDefaultMask.Name = "lblDefaultMask"
        lblDefaultMask.Text = "デフォルトマスク値:"

        ' txtDefaultMask
        txtDefaultMask.Location = New Point(625, 42)
        txtDefaultMask.Name = "txtDefaultMask"
        txtDefaultMask.Size = New Size(50, 23)
        txtDefaultMask.Text = "***"

        ' splitMain
        splitMain.Location = New Point(15, 75)
        splitMain.Name = "splitMain"
        splitMain.Size = New Size(660, 370)
        splitMain.SplitterDistance = 220
        splitMain.FixedPanel = FixedPanel.Panel1

        ' grpTables
        grpTables.Controls.Add(lstTables)
        grpTables.Dock = DockStyle.Fill
        grpTables.Name = "grpTables"
        grpTables.Text = "テーブル一覧"

        ' lstTables
        lstTables.Dock = DockStyle.Fill
        lstTables.Name = "lstTables"
        lstTables.IntegralHeight = False

        ' grpColumns
        grpColumns.Controls.Add(dgvColumns)
        grpColumns.Dock = DockStyle.Fill
        grpColumns.Name = "grpColumns"
        grpColumns.Text = "列マスク設定"

        ' dgvColumns
        dgvColumns.AllowUserToAddRows = False
        dgvColumns.AllowUserToDeleteRows = False
        dgvColumns.AllowUserToResizeRows = False
        dgvColumns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvColumns.Columns.AddRange(New DataGridViewColumn() {colCheck, colColumnName, colColumnType, colMaskValue})
        dgvColumns.Dock = DockStyle.Fill
        dgvColumns.Name = "dgvColumns"
        dgvColumns.RowHeadersVisible = False
        dgvColumns.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' colCheck
        colCheck.HeaderText = "マスク"
        colCheck.Name = "colCheck"
        colCheck.Width = 50

        ' colColumnName
        colColumnName.HeaderText = "列名"
        colColumnName.Name = "colColumnName"
        colColumnName.ReadOnly = True
        colColumnName.Width = 130

        ' colColumnType
        colColumnType.HeaderText = "型"
        colColumnType.Name = "colColumnType"
        colColumnType.ReadOnly = True
        colColumnType.Width = 100

        ' colMaskValue
        colMaskValue.HeaderText = "マスク値"
        colMaskValue.Name = "colMaskValue"
        colMaskValue.Width = 130

        ' splitMain panels
        splitMain.Panel1.Controls.Add(grpTables)
        splitMain.Panel2.Controls.Add(grpColumns)

        ' btnOK
        btnOK.Location = New Point(510, 455)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(80, 28)
        btnOK.Text = "OK"
        btnOK.UseVisualStyleBackColor = True
        btnOK.DialogResult = DialogResult.OK

        ' btnCancel
        btnCancel.Location = New Point(595, 455)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(80, 28)
        btnCancel.Text = "キャンセル"
        btnCancel.UseVisualStyleBackColor = True
        btnCancel.DialogResult = DialogResult.Cancel

        ' MaskingConfigDialog
        AcceptButton = btnOK
        CancelButton = btnCancel
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(690, 495)
        Controls.Add(lblFilePath)
        Controls.Add(txtFilePath)
        Controls.Add(btnOpen)
        Controls.Add(btnNew)
        Controls.Add(btnSave)
        Controls.Add(lblDescription)
        Controls.Add(txtDescription)
        Controls.Add(lblDefaultMask)
        Controls.Add(txtDefaultMask)
        Controls.Add(splitMain)
        Controls.Add(btnOK)
        Controls.Add(btnCancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "MaskingConfigDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "データマスキング設定"

        grpTables.ResumeLayout(False)
        grpColumns.ResumeLayout(False)
        CType(dgvColumns, System.ComponentModel.ISupportInitialize).EndInit()
        splitMain.Panel1.ResumeLayout(False)
        splitMain.Panel2.ResumeLayout(False)
        CType(splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        splitMain.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblFilePath As Label
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents lblDescription As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents lblDefaultMask As Label
    Friend WithEvents txtDefaultMask As TextBox
    Friend WithEvents grpTables As GroupBox
    Friend WithEvents lstTables As ListBox
    Friend WithEvents grpColumns As GroupBox
    Friend WithEvents dgvColumns As DataGridView
    Friend WithEvents colCheck As DataGridViewCheckBoxColumn
    Friend WithEvents colColumnName As DataGridViewTextBoxColumn
    Friend WithEvents colColumnType As DataGridViewTextBoxColumn
    Friend WithEvents colMaskValue As DataGridViewTextBoxColumn
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents splitMain As SplitContainer

End Class
