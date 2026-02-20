<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchConditionRow
    Inherits System.Windows.Forms.Panel

    'コンポーネントの一覧をクリアするために dispose をオーバーライドします。
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
        columnCombo = New System.Windows.Forms.ComboBox()
        operatorCombo = New System.Windows.Forms.ComboBox()
        valueTextBox = New System.Windows.Forms.TextBox()
        caseSensitiveCheckBox = New System.Windows.Forms.CheckBox()
        btnDelete = New System.Windows.Forms.Button()
        SuspendLayout()
        '
        'columnCombo
        '
        columnCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        columnCombo.FormattingEnabled = True
        columnCombo.Location = New System.Drawing.Point(10, 5)
        columnCombo.Name = "columnCombo"
        columnCombo.Size = New System.Drawing.Size(150, 20)
        columnCombo.TabIndex = 0
        '
        'operatorCombo
        '
        operatorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        operatorCombo.FormattingEnabled = True
        operatorCombo.Items.AddRange(New Object() {"含む", "含まない", "等しい", "等しくない", "で始まる", "で終わる", ">", "<", ">=", "<=", "Null", "Not Null"})
        operatorCombo.Location = New System.Drawing.Point(170, 5)
        operatorCombo.Name = "operatorCombo"
        operatorCombo.Size = New System.Drawing.Size(100, 20)
        operatorCombo.TabIndex = 1
        operatorCombo.SelectedIndex = 0
        '
        'valueTextBox
        '
        valueTextBox.Location = New System.Drawing.Point(280, 5)
        valueTextBox.Name = "valueTextBox"
        valueTextBox.Size = New System.Drawing.Size(200, 19)
        valueTextBox.TabIndex = 2
        '
        'caseSensitiveCheckBox
        '
        caseSensitiveCheckBox.AutoSize = True
        caseSensitiveCheckBox.Location = New System.Drawing.Point(490, 8)
        caseSensitiveCheckBox.Name = "caseSensitiveCheckBox"
        caseSensitiveCheckBox.Size = New System.Drawing.Size(126, 16)
        caseSensitiveCheckBox.TabIndex = 3
        caseSensitiveCheckBox.Text = "大文字小文字区別"
        caseSensitiveCheckBox.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        btnDelete.Location = New System.Drawing.Point(650, 5)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New System.Drawing.Size(60, 25)
        btnDelete.TabIndex = 4
        btnDelete.Text = "削除"
        btnDelete.UseVisualStyleBackColor = True
        btnDelete.Visible = False
        '
        'SearchConditionRow
        '
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(columnCombo)
        Me.Controls.Add(operatorCombo)
        Me.Controls.Add(valueTextBox)
        Me.Controls.Add(caseSensitiveCheckBox)
        Me.Controls.Add(btnDelete)
        Me.Height = 40
        Me.Name = "SearchConditionRow"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(720, 40)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents columnCombo As System.Windows.Forms.ComboBox
    Friend WithEvents operatorCombo As System.Windows.Forms.ComboBox
    Friend WithEvents valueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents caseSensitiveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button

End Class
