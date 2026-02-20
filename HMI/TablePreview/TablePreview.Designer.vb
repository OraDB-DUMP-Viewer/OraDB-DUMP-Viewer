<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TablePreview
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
        components = New ComponentModel.Container()
        dataGridViewData = New DataGridView()
        panelSearch = New Panel()
        buttonSearch = New Button()
        comboBoxColumns = New ComboBox()
        textBoxSearchValue = New TextBox()
        labelSearch = New Label()
        labelColumns = New Label()
        labelRowCount = New Label()
        buttonReset = New Button()
        buttonAdvancedSearch = New Button()
        panelPaging = New Panel()
        labelPageInfo = New Label()
        buttonNext = New Button()
        buttonPrev = New Button()
        numericUpDownPageSize = New NumericUpDown()
        labelPageSize = New Label()
        CType(dataGridViewData, ComponentModel.ISupportInitialize).BeginInit()
        CType(numericUpDownPageSize, ComponentModel.ISupportInitialize).BeginInit()
        panelSearch.SuspendLayout()
        panelPaging.SuspendLayout()
        SuspendLayout()
        dataGridViewData.AllowUserToAddRows = False
        dataGridViewData.AllowUserToDeleteRows = False
        dataGridViewData.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dataGridViewData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGridViewData.Location = New Point(12, 100)
        dataGridViewData.Name = "dataGridViewData"
        dataGridViewData.ReadOnly = True
        dataGridViewData.Size = New Size(776, 280)
        dataGridViewData.TabIndex = 0
        panelSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        panelSearch.Controls.Add(labelSearch)
        panelSearch.Controls.Add(textBoxSearchValue)
        panelSearch.Controls.Add(labelColumns)
        panelSearch.Controls.Add(comboBoxColumns)
        panelSearch.Controls.Add(buttonSearch)
        panelSearch.Controls.Add(buttonReset)
        panelSearch.Controls.Add(buttonAdvancedSearch)
        panelSearch.Controls.Add(labelRowCount)
        panelSearch.Location = New Point(12, 12)
        panelSearch.Name = "panelSearch"
        panelSearch.Size = New Size(776, 82)
        panelSearch.TabIndex = 1
        labelSearch.AutoSize = True
        labelSearch.Location = New Point(3, 47)
        labelSearch.Name = "labelSearch"
        labelSearch.Size = New Size(71, 12)
        labelSearch.TabIndex = 0
        labelSearch.Text = "検索値:"
        textBoxSearchValue.Location = New Point(80, 44)
        textBoxSearchValue.Name = "textBoxSearchValue"
        textBoxSearchValue.Size = New Size(150, 19)
        textBoxSearchValue.TabIndex = 1
        labelColumns.AutoSize = True
        labelColumns.Location = New Point(3, 15)
        labelColumns.Name = "labelColumns"
        labelColumns.Size = New Size(71, 12)
        labelColumns.TabIndex = 2
        labelColumns.Text = "列名:"
        comboBoxColumns.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxColumns.FormattingEnabled = True
        comboBoxColumns.Location = New Point(80, 12)
        comboBoxColumns.Name = "comboBoxColumns"
        comboBoxColumns.Size = New Size(150, 20)
        comboBoxColumns.TabIndex = 3
        buttonSearch.Location = New Point(236, 44)
        buttonSearch.Name = "buttonSearch"
        buttonSearch.Size = New Size(75, 23)
        buttonSearch.TabIndex = 4
        buttonSearch.Text = "検索"
        buttonSearch.UseVisualStyleBackColor = True
        buttonReset.Location = New Point(236, 12)
        buttonReset.Name = "buttonReset"
        buttonReset.Size = New Size(75, 23)
        buttonReset.TabIndex = 5
        buttonReset.Text = "リセット"
        buttonReset.UseVisualStyleBackColor = True
        buttonAdvancedSearch.Location = New Point(317, 12)
        buttonAdvancedSearch.Name = "buttonAdvancedSearch"
        buttonAdvancedSearch.Size = New Size(100, 23)
        buttonAdvancedSearch.TabIndex = 6
        buttonAdvancedSearch.Text = "高度な検索"
        buttonAdvancedSearch.UseVisualStyleBackColor = True
        labelRowCount.AutoSize = True
        labelRowCount.Location = New Point(425, 15)
        labelRowCount.Name = "labelRowCount"
        labelRowCount.Size = New Size(143, 12)
        labelRowCount.TabIndex = 7
        labelRowCount.Text = "合計行数: 0 (表示行数: 0)"
        panelPaging.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        panelPaging.Controls.Add(labelPageSize)
        panelPaging.Controls.Add(numericUpDownPageSize)
        panelPaging.Controls.Add(buttonPrev)
        panelPaging.Controls.Add(buttonNext)
        panelPaging.Controls.Add(labelPageInfo)
        panelPaging.Location = New Point(12, 386)
        panelPaging.Name = "panelPaging"
        panelPaging.Size = New Size(776, 42)
        panelPaging.TabIndex = 2
        labelPageInfo.AutoSize = True
        labelPageInfo.Location = New Point(3, 12)
        labelPageInfo.Name = "labelPageInfo"
        labelPageInfo.Size = New Size(71, 12)
        labelPageInfo.TabIndex = 0
        labelPageInfo.Text = "ページ: 1/1"
        buttonPrev.Location = New Point(150, 9)
        buttonPrev.Name = "buttonPrev"
        buttonPrev.Size = New Size(75, 23)
        buttonPrev.TabIndex = 1
        buttonPrev.Text = "< 前へ"
        buttonPrev.UseVisualStyleBackColor = True
        buttonNext.Location = New Point(231, 9)
        buttonNext.Name = "buttonNext"
        buttonNext.Size = New Size(75, 23)
        buttonNext.TabIndex = 2
        buttonNext.Text = "次へ >"
        buttonNext.UseVisualStyleBackColor = True
        numericUpDownPageSize.Location = New Point(381, 11)
        numericUpDownPageSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numericUpDownPageSize.Name = "numericUpDownPageSize"
        numericUpDownPageSize.Size = New Size(60, 19)
        numericUpDownPageSize.TabIndex = 3
        numericUpDownPageSize.Value = New Decimal(New Integer() {100, 0, 0, 0})
        labelPageSize.AutoSize = True
        labelPageSize.Location = New Point(318, 13)
        labelPageSize.Name = "labelPageSize"
        labelPageSize.Size = New Size(59, 12)
        labelPageSize.TabIndex = 4
        labelPageSize.Text = "1ページ:"
        AutoScaleDimensions = New SizeF(6.0F, 12.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 440)
        Controls.Add(dataGridViewData)
        Controls.Add(panelSearch)
        Controls.Add(panelPaging)
        Name = "TablePreview"
        Text = "テーブルデータプレビュー"
        WindowState = FormWindowState.Maximized
        CType(dataGridViewData, ComponentModel.ISupportInitialize).EndInit()
        CType(numericUpDownPageSize, ComponentModel.ISupportInitialize).EndInit()
        panelSearch.ResumeLayout(False)
        panelSearch.PerformLayout()
        panelPaging.ResumeLayout(False)
        panelPaging.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents dataGridViewData As System.Windows.Forms.DataGridView
    Friend WithEvents panelSearch As System.Windows.Forms.Panel
    Friend WithEvents buttonSearch As System.Windows.Forms.Button
    Friend WithEvents comboBoxColumns As System.Windows.Forms.ComboBox
    Friend WithEvents textBoxSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents labelSearch As System.Windows.Forms.Label
    Friend WithEvents labelColumns As System.Windows.Forms.Label
    Friend WithEvents labelRowCount As System.Windows.Forms.Label
    Friend WithEvents buttonReset As System.Windows.Forms.Button
    Friend WithEvents buttonAdvancedSearch As System.Windows.Forms.Button
    Friend WithEvents panelPaging As System.Windows.Forms.Panel
    Friend WithEvents labelPageInfo As System.Windows.Forms.Label
    Friend WithEvents buttonNext As System.Windows.Forms.Button
    Friend WithEvents buttonPrev As System.Windows.Forms.Button
    Friend WithEvents numericUpDownPageSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents labelPageSize As System.Windows.Forms.Label
End Class
