Imports System.Collections.Generic

''' <summary>
''' 単一の検索条件行UI
''' </summary>
Public Class SearchConditionRow
    Inherits Panel

    Public LogicalComboBox As ComboBox

    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property ShowDeleteButton As Boolean
        Get
            Return btnDelete.Visible
        End Get
        Set(value As Boolean)
            btnDelete.Visible = value
        End Set
    End Property

    Public ReadOnly Property DeleteButton As Button
        Get
            Return btnDelete
        End Get
    End Property

    Public Sub New(columnNames As List(Of String))
        InitializeComponent()

        ' 列名をコンボボックスに設定
        For Each colName In columnNames
            columnCombo.Items.Add(colName)
        Next
        If columnCombo.Items.Count > 0 Then
            columnCombo.SelectedIndex = 0
        End If

        ' イベントハンドラーを追加
        AddHandler columnCombo.SelectedIndexChanged, AddressOf ColumnCombo_SelectedIndexChanged
    End Sub

    Private Sub ColumnCombo_SelectedIndexChanged(sender As Object, e As EventArgs)
        valueTextBox.Clear()
    End Sub

    Public Function IsValid() As Boolean
        Return columnCombo.SelectedIndex >= 0 AndAlso
               operatorCombo.SelectedIndex >= 0 AndAlso
               (IsNullOperator() OrElse Not String.IsNullOrEmpty(valueTextBox.Text.Trim()))
    End Function

    Private Function IsNullOperator() As Boolean
        Return operatorCombo.SelectedIndex = 10 OrElse operatorCombo.SelectedIndex = 11
    End Function

    Public Function GetCondition() As SearchCondition.SearchConditionItem
        Dim opType As SearchCondition.OperatorType
        Select Case operatorCombo.SelectedIndex
            Case 0 ' 含む
                opType = SearchCondition.OperatorType.Contains
            Case 1 ' 含まない
                opType = SearchCondition.OperatorType.NotContains
            Case 2 ' 等しい
                opType = SearchCondition.OperatorType.Equals
            Case 3 ' 等しくない
                opType = SearchCondition.OperatorType.NotEquals
            Case 4 ' で始まる
                opType = SearchCondition.OperatorType.StartsWith
            Case 5 ' で終わる
                opType = SearchCondition.OperatorType.EndsWith
            Case 6 ' >
                opType = SearchCondition.OperatorType.GreaterThan
            Case 7 ' <
                opType = SearchCondition.OperatorType.LessThan
            Case 8 ' >=
                opType = SearchCondition.OperatorType.GreaterThanOrEqual
            Case 9 ' <=
                opType = SearchCondition.OperatorType.LessThanOrEqual
            Case 10 ' Null
                opType = SearchCondition.OperatorType.IsNull
            Case 11 ' Not Null
                opType = SearchCondition.OperatorType.IsNotNull
            Case Else
                opType = SearchCondition.OperatorType.Contains
        End Select

        Return New SearchCondition.SearchConditionItem(
            columnCombo.SelectedItem.ToString(),
            opType,
            valueTextBox.Text.Trim(),
            caseSensitiveCheckBox.Checked
        )
    End Function

    Public Function GetLogicalOperator() As SearchCondition.LogicalOperatorType
        If LogicalComboBox IsNot Nothing Then
            If LogicalComboBox.SelectedIndex = 0 Then
                Return SearchCondition.LogicalOperatorType.And
            Else
                Return SearchCondition.LogicalOperatorType.Or
            End If
        End If
        Return SearchCondition.LogicalOperatorType.And
    End Function
End Class
