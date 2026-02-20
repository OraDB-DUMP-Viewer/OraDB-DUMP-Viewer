Imports System.Collections.Generic

Public Class AdvancedSearchForm

    Private _columnNames As List(Of String)
    Private _searchCondition As SearchCondition.ComplexSearchCondition
    Private _conditionRows As New List(Of SearchConditionRow)()

    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property SearchConditionResult As SearchCondition.ComplexSearchCondition
        Get
            Return _searchCondition
        End Get
        Set(value As SearchCondition.ComplexSearchCondition)
            _searchCondition = value
        End Set
    End Property

    Public Sub New(columnNames As List(Of String))
        InitializeComponent()
        _columnNames = New List(Of String)(columnNames)
        _searchCondition = New SearchCondition.ComplexSearchCondition()

        ' テンプレートをコントロールから削除
        Me.Controls.Remove(templateConditionRow)
        Me.Controls.Remove(templateLogicalPanel)
    End Sub

    Private Sub AdvancedSearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler buttonAdd.Click, AddressOf ButtonAdd_Click
        AddHandler buttonClear.Click, AddressOf ButtonClear_Click
        AddHandler buttonSearch.Click, AddressOf ButtonSearch_Click
        AddHandler MyBase.FormClosing, AddressOf AdvancedSearchForm_FormClosing

        ' 初期条件を1つ追加
        AddSearchConditionRow()
    End Sub

    Private Sub AddSearchConditionRow()
        Dim row As SearchConditionRow
        Dim rowIndex = _conditionRows.Count

        If rowIndex = 0 Then
            ' 最初の行はテンプレートから作成
            row = CreateConditionRowFromTemplate()
        Else
            ' 2番目以降はコピーで作成
            row = CloneConditionRow(templateConditionRow)
        End If

        If rowIndex > 0 Then
            ' AND/OR選択用パネルを追加
            Dim logicalPanel = CloneLogicalPanel()
            row.LogicalComboBox = CType(logicalPanel.Controls(1), ComboBox)
            flowLayoutPanel.Controls.Add(logicalPanel)
        End If

        ' 削除ボタンを表示
        row.ShowDeleteButton = rowIndex > 0
        AddHandler row.DeleteButton.Click, Sub(s, e) RemoveSearchConditionRow(row)

        flowLayoutPanel.Controls.Add(row)
        _conditionRows.Add(row)
    End Sub

    Private Function CreateConditionRowFromTemplate() As SearchConditionRow
        ' テンプレートから新しい行を作成
        Dim row As New SearchConditionRow(_columnNames)
        Return row
    End Function

    Private Function CloneConditionRow(original As SearchConditionRow) As SearchConditionRow
        ' 新しい行を作成
        Dim row As New SearchConditionRow(_columnNames)
        ' 各コントロールの状態をコピー（必要に応じて）
        Return row
    End Function

    Private Function CloneLogicalPanel() As Panel
        Dim logicalPanel As New Panel()
        logicalPanel.Height = 40
        logicalPanel.AutoSize = True
        logicalPanel.BackColor = SystemColors.Control

        Dim logicalLabel As New Label()
        logicalLabel.Text = "条件:"
        logicalLabel.Location = New Point(10, 8)
        logicalLabel.Width = 60

        Dim logicalCombo As New ComboBox()
        logicalCombo.Items.Add("AND")
        logicalCombo.Items.Add("OR")
        logicalCombo.SelectedIndex = 0
        logicalCombo.Location = New Point(70, 5)
        logicalCombo.Width = 100
        logicalCombo.DropDownStyle = ComboBoxStyle.DropDownList

        logicalPanel.Controls.Add(logicalLabel)
        logicalPanel.Controls.Add(logicalCombo)

        Return logicalPanel
    End Function

    Private Sub RemoveSearchConditionRow(row As SearchConditionRow)
        If _conditionRows.Count <= 1 Then
            MessageBox.Show("最低1つの条件は必要です。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim index = _conditionRows.IndexOf(row)
        _conditionRows.Remove(row)

        ' UIから削除
        flowLayoutPanel.Controls.Remove(row)

        ' AND/ORパネルも削除
        If index < flowLayoutPanel.Controls.Count Then
            Dim controlToRemove = flowLayoutPanel.Controls(index)
            If controlToRemove.GetType() = GetType(Panel) Then
                flowLayoutPanel.Controls.Remove(controlToRemove)
            End If
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs)
        AddSearchConditionRow()
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs)
        _conditionRows.Clear()
        flowLayoutPanel.Controls.Clear()
        _searchCondition = New SearchCondition.ComplexSearchCondition()
        AddSearchConditionRow()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs)
        ' 入力を検証
        For Each row In _conditionRows
            If Not row.IsValid() Then
                MessageBox.Show("すべての条件を正しく入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        ' 複合検索条件を構築
        _searchCondition = New SearchCondition.ComplexSearchCondition()

        For i = 0 To _conditionRows.Count - 1
            Dim condition = _conditionRows(i).GetCondition()
            _searchCondition.Conditions.Add(condition)

            If i > 0 Then
                Dim logicalOp = _conditionRows(i).GetLogicalOperator()
                _searchCondition.LogicalOperators.Add(logicalOp)
            End If
        Next
    End Sub

    Private Sub AdvancedSearchForm_FormClosing(sender As Object, e As FormClosingEventArgs)
        If Me.DialogResult = DialogResult.OK Then
            ButtonSearch_Click(Nothing, Nothing)
        End If
    End Sub

End Class
