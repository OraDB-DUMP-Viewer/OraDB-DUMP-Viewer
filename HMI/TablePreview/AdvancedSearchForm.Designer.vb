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
        components = New System.ComponentModel.Container()
        Me.mainPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.scrollPanel = New System.Windows.Forms.Panel()
        Me.flowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.buttonPanel = New System.Windows.Forms.Panel()
        Me.buttonAdd = New System.Windows.Forms.Button()
        Me.buttonClear = New System.Windows.Forms.Button()
        Me.buttonSearch = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.templateConditionRow = New SearchConditionRow(New List(Of String)())
        Me.templateLogicalPanel = New System.Windows.Forms.Panel()
        Me.mainPanel.SuspendLayout()
        Me.scrollPanel.SuspendLayout()
        Me.buttonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainPanel
        '
        Me.mainPanel.ColumnCount = 1
        Me.mainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainPanel.Controls.Add(Me.scrollPanel, 0, 0)
        Me.mainPanel.Controls.Add(Me.buttonPanel, 0, 1)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(0, 0)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.RowCount = 2
        Me.mainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.mainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.mainPanel.Size = New System.Drawing.Size(900, 600)
        Me.mainPanel.TabIndex = 0
        '
        'scrollPanel
        '
        Me.scrollPanel.AutoScroll = True
        Me.scrollPanel.Controls.Add(Me.flowLayoutPanel)
        Me.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scrollPanel.Location = New System.Drawing.Point(3, 3)
        Me.scrollPanel.Name = "scrollPanel"
        Me.scrollPanel.Size = New System.Drawing.Size(894, 534)
        Me.scrollPanel.TabIndex = 0
        '
        'flowLayoutPanel
        '
        Me.flowLayoutPanel.AutoSize = True
        Me.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flowLayoutPanel.BackColor = System.Drawing.SystemColors.Control
        Me.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flowLayoutPanel.Location = New System.Drawing.Point(10, 10)
        Me.flowLayoutPanel.Name = "flowLayoutPanel"
        Me.flowLayoutPanel.Padding = New System.Windows.Forms.Padding(10)
        Me.flowLayoutPanel.Size = New System.Drawing.Size(0, 20)
        Me.flowLayoutPanel.TabIndex = 0
        '
        'buttonPanel
        '
        Me.buttonPanel.Controls.Add(Me.buttonAdd)
        Me.buttonPanel.Controls.Add(Me.buttonClear)
        Me.buttonPanel.Controls.Add(Me.buttonSearch)
        Me.buttonPanel.Controls.Add(Me.buttonCancel)
        Me.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.buttonPanel.Location = New System.Drawing.Point(3, 543)
        Me.buttonPanel.Name = "buttonPanel"
        Me.buttonPanel.Size = New System.Drawing.Size(894, 54)
        Me.buttonPanel.TabIndex = 1
        '
        'buttonAdd
        '
        Me.buttonAdd.Location = New System.Drawing.Point(10, 10)
        Me.buttonAdd.Name = "buttonAdd"
        Me.buttonAdd.Size = New System.Drawing.Size(100, 40)
        Me.buttonAdd.TabIndex = 0
        Me.buttonAdd.Text = "条件を追加"
        Me.buttonAdd.UseVisualStyleBackColor = True
        '
        'buttonClear
        '
        Me.buttonClear.Location = New System.Drawing.Point(120, 10)
        Me.buttonClear.Name = "buttonClear"
        Me.buttonClear.Size = New System.Drawing.Size(100, 40)
        Me.buttonClear.TabIndex = 1
        Me.buttonClear.Text = "クリア"
        Me.buttonClear.UseVisualStyleBackColor = True
        '
        'buttonSearch
        '
        Me.buttonSearch.BackColor = System.Drawing.SystemColors.Highlight
        Me.buttonSearch.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.buttonSearch.ForeColor = System.Drawing.Color.White
        Me.buttonSearch.Location = New System.Drawing.Point(230, 10)
        Me.buttonSearch.Name = "buttonSearch"
        Me.buttonSearch.Size = New System.Drawing.Size(100, 40)
        Me.buttonSearch.TabIndex = 2
        Me.buttonSearch.Text = "検索"
        Me.buttonSearch.UseVisualStyleBackColor = False
        '
        'buttonCancel
        '
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(340, 10)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(100, 40)
        Me.buttonCancel.TabIndex = 3
        Me.buttonCancel.Text = "キャンセル"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'templateConditionRow
        '
        Me.templateConditionRow.AutoSize = True
        Me.templateConditionRow.BackColor = System.Drawing.SystemColors.Control
        Me.templateConditionRow.Height = 40
        Me.templateConditionRow.Name = "templateConditionRow"
        Me.templateConditionRow.Padding = New System.Windows.Forms.Padding(5)
        Me.templateConditionRow.Size = New System.Drawing.Size(720, 40)
        Me.templateConditionRow.TabIndex = 0
        Me.templateConditionRow.Visible = False
        '
        'templateLogicalPanel
        '
        Me.templateLogicalPanel.AutoSize = True
        Me.templateLogicalPanel.BackColor = System.Drawing.SystemColors.Control
        Me.templateLogicalPanel.Height = 40
        Me.templateLogicalPanel.Name = "templateLogicalPanel"
        Me.templateLogicalPanel.Size = New System.Drawing.Size(0, 0)
        Me.templateLogicalPanel.TabIndex = 0
        Me.templateLogicalPanel.Visible = False
        '
        'AdvancedSearchForm
        '
        Me.AcceptButton = Me.buttonSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(900, 600)
        Me.Controls.Add(Me.mainPanel)
        Me.Controls.Add(Me.templateConditionRow)
        Me.Controls.Add(Me.templateLogicalPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdvancedSearchForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "高度な検索"
        Me.mainPanel.ResumeLayout(False)
        Me.scrollPanel.ResumeLayout(False)
        Me.scrollPanel.PerformLayout()
        Me.buttonPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

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
