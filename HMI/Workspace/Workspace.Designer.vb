<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Workspace
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        pnlDBList = New Panel()
        treeDBList = New TreeView()
        splList = New Splitter()
        lstTableList = New ListView()
        plnTableSearch = New TableLayoutPanel()
        txtTableSearch = New TextBox()
        btnTableSearch = New Button()
        pnlDBList.SuspendLayout()
        plnTableSearch.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlDBList
        ' 
        pnlDBList.BackColor = SystemColors.Control
        pnlDBList.Controls.Add(treeDBList)
        pnlDBList.Dock = DockStyle.Left
        pnlDBList.Location = New Point(0, 0)
        pnlDBList.Name = "pnlDBList"
        pnlDBList.Size = New Size(300, 811)
        pnlDBList.TabIndex = 0
        ' 
        ' treeDBList
        ' 
        treeDBList.Dock = DockStyle.Fill
        treeDBList.Location = New Point(0, 0)
        treeDBList.Name = "treeDBList"
        treeDBList.Size = New Size(300, 811)
        treeDBList.TabIndex = 0
        ' 
        ' splList
        ' 
        splList.BackColor = SystemColors.ButtonShadow
        splList.Location = New Point(300, 0)
        splList.Name = "splList"
        splList.Size = New Size(15, 811)
        splList.TabIndex = 1
        splList.TabStop = False
        ' 
        ' lstTableList
        ' 
        lstTableList.Dock = DockStyle.Fill
        lstTableList.Location = New Point(315, 37)
        lstTableList.Name = "lstTableList"
        lstTableList.Size = New Size(835, 774)
        lstTableList.TabIndex = 2
        lstTableList.UseCompatibleStateImageBehavior = False
        ' 
        ' plnTableSearch
        ' 
        plnTableSearch.AutoSize = True
        plnTableSearch.ColumnCount = 2
        plnTableSearch.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        plnTableSearch.ColumnStyles.Add(New ColumnStyle())
        plnTableSearch.Controls.Add(txtTableSearch, 1, 0)
        plnTableSearch.Controls.Add(btnTableSearch, 0, 0)
        plnTableSearch.Dock = DockStyle.Top
        plnTableSearch.Location = New Point(315, 0)
        plnTableSearch.Name = "plnTableSearch"
        plnTableSearch.RowCount = 1
        plnTableSearch.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        plnTableSearch.Size = New Size(835, 37)
        plnTableSearch.TabIndex = 1
        ' 
        ' txtTableSearch
        ' 
        txtTableSearch.Dock = DockStyle.Fill
        txtTableSearch.Location = New Point(103, 3)
        txtTableSearch.Name = "txtTableSearch"
        txtTableSearch.Size = New Size(729, 31)
        txtTableSearch.TabIndex = 2
        ' 
        ' btnTableSearch
        ' 
        btnTableSearch.Dock = DockStyle.Fill
        btnTableSearch.Location = New Point(3, 3)
        btnTableSearch.Name = "btnTableSearch"
        btnTableSearch.Size = New Size(94, 31)
        btnTableSearch.TabIndex = 0
        btnTableSearch.Text = "検索"
        btnTableSearch.UseVisualStyleBackColor = True
        ' 
        ' Workspace
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1150, 811)
        Controls.Add(lstTableList)
        Controls.Add(plnTableSearch)
        Controls.Add(splList)
        Controls.Add(pnlDBList)
        Name = "Workspace"
        Text = "Workspace"
        pnlDBList.ResumeLayout(False)
        plnTableSearch.ResumeLayout(False)
        plnTableSearch.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlDBList As Panel
    Friend WithEvents splList As Splitter
    Friend WithEvents plnTableSearch As TableLayoutPanel
    Friend WithEvents btnTableSearch As Button
    Friend WithEvents txtTableSearch As TextBox
    Friend WithEvents treeDBList As TreeView
    Friend WithEvents lstTableList As ListView
End Class
