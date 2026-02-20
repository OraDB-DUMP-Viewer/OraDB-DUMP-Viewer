Imports System.IO
Imports System.Collections.Generic

Public Class TablePreview

#Region "フィールド・コンストラクタ"
    Private _tableData As List(Of Dictionary(Of String, Object))
    Private _filteredData As List(Of Dictionary(Of String, Object))
    Private _columnNames As List(Of String)
    Private _currentPage As Integer = 1
    Private _pageSize As Integer = 100
    Private _totalRows As Integer = 0
    Private _isInitializing As Boolean = True

    Public Sub New(tableData As List(Of Dictionary(Of String, Object)), columnNames As List(Of String), tableName As String)
        InitializeComponent()
        _tableData = tableData
        _columnNames = columnNames
        _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
        _totalRows = _tableData.Count
        Me.Text = $"テーブルデータプレビュー - {tableName}"
    End Sub
#End Region

#Region "イベント処理"
    Private Sub TablePreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isInitializing = True

        ' 列名をコンボボックスに設定
        For Each colName In _columnNames
            comboBoxColumns.Items.Add(colName)
        Next
        If comboBoxColumns.Items.Count > 0 Then
            comboBoxColumns.SelectedIndex = 0
        End If

        ' ページサイズの初期値を設定
        numericUpDownPageSize.Value = _pageSize

        ' DataGridViewをセットアップ
        SetupDataGridView()

        ' 初期データを表示
        UpdateDataDisplay()

        _isInitializing = False
    End Sub

    Private Sub buttonSearch_Click(sender As Object, e As EventArgs) Handles buttonSearch.Click
        _currentPage = 1
        FilterData()
        UpdateDataDisplay()
    End Sub

    Private Sub buttonReset_Click(sender As Object, e As EventArgs) Handles buttonReset.Click
        _currentPage = 1
        _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
        textBoxSearchValue.Clear()
        comboBoxColumns.SelectedIndex = 0
        UpdateDataDisplay()
    End Sub

    Private Sub buttonPrev_Click(sender As Object, e As EventArgs) Handles buttonPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub buttonNext_Click(sender As Object, e As EventArgs) Handles buttonNext.Click
        Dim totalPages As Integer = Math.Ceiling(_filteredData.Count / _pageSize)
        If _currentPage < totalPages Then
            _currentPage += 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub numericUpDownPageSize_ValueChanged(sender As Object, e As EventArgs) Handles numericUpDownPageSize.ValueChanged
        If Not _isInitializing Then
            _pageSize = CInt(numericUpDownPageSize.Value)
            _currentPage = 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub textBoxSearchValue_KeyDown(sender As Object, e As KeyEventArgs) Handles textBoxSearchValue.KeyDown
        If e.KeyCode = Keys.Return Then
            buttonSearch.PerformClick()
            e.Handled = True
        End If
    End Sub
#End Region

#Region "データ処理"
    Private Sub SetupDataGridView()
        dataGridViewData.Columns.Clear()

        ' 列を追加
        For Each colName In _columnNames
            dataGridViewData.Columns.Add(colName, colName)
        Next

        ' 列の幅を自動調整
        For Each col As DataGridViewColumn In dataGridViewData.Columns
            col.Width = Math.Max(100, Math.Min(200, col.Width))
        Next
    End Sub

    Private Sub FilterData()
        Dim columnName As String = comboBoxColumns.SelectedItem?.ToString()
        Dim searchValue As String = textBoxSearchValue.Text.Trim()

        If String.IsNullOrEmpty(columnName) OrElse String.IsNullOrEmpty(searchValue) Then
            _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
            Return
        End If

        ' フィルタリング処理
        _filteredData = _tableData.Where(Function(row)
                                           If row.ContainsKey(columnName) Then
                                               Dim cellValue = row(columnName)
                                               If cellValue IsNot Nothing Then
                                                   Return cellValue.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                                               End If
                                           End If
                                           Return False
                                       End Function).ToList()
    End Sub

    Private Sub UpdateDataDisplay()
        Dim totalPages As Integer = Math.Ceiling(If(_filteredData.Count = 0, 1, _filteredData.Count / _pageSize))
        Dim startRow As Integer = (_currentPage - 1) * _pageSize
        Dim endRow As Integer = Math.Min(startRow + _pageSize, _filteredData.Count)

        ' DataGridViewをクリア
        dataGridViewData.Rows.Clear()

        ' 現在のページのデータをDataGridViewに追加
        For i As Integer = startRow To endRow - 1
            Dim row = _filteredData(i)
            Dim cells As New List(Of Object)

            For Each colName In _columnNames
                If row.ContainsKey(colName) Then
                    cells.Add(If(row(colName), String.Empty))
                Else
                    cells.Add(String.Empty)
                End If
            Next

            dataGridViewData.Rows.Add(cells.ToArray())
        Next

        ' UI要素を更新
        labelPageInfo.Text = $"ページ: {_currentPage}/{totalPages}"
        labelRowCount.Text = $"合計行数: {_tableData.Count} (表示行数: {_filteredData.Count})"
        buttonPrev.Enabled = _currentPage > 1
        buttonNext.Enabled = _currentPage < totalPages
    End Sub
#End Region

End Class