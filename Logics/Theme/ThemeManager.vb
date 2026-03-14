Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

''' <summary>
''' アプリケーションのテーマ管理モジュール。
''' LocaleManagerパターンを踏襲し、OS追従 + 手動切替をサポートする。
''' </summary>
Public Module ThemeManager

#Region "Win32 API (タイトルバーダークモード)"
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Private Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr,
                                   X As Integer, Y As Integer, cx As Integer, cy As Integer,
                                   uFlags As UInteger) As Boolean
    End Function

    Private Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20
    Private Const SWP_NOMOVE As UInteger = &H2
    Private Const SWP_NOSIZE As UInteger = &H1
    Private Const SWP_NOZORDER As UInteger = &H4
    Private Const SWP_FRAMECHANGED As UInteger = &H20

    ''' <summary>フォームのタイトルバーにダーク/ライトモードを適用する</summary>
    Public Sub ApplyTitleBarTheme(frm As Form, isDark As Boolean)
        If Not frm.IsHandleCreated Then Return
        Try
            Dim value As Integer = If(isDark, 1, 0)
            DwmSetWindowAttribute(frm.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, value, 4)
            ' 非クライアント領域（タイトルバー）を即座に再描画
            SetWindowPos(frm.Handle, IntPtr.Zero, 0, 0, 0, 0,
                         SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOZORDER Or SWP_FRAMECHANGED)
        Catch
            ' Windows 10 旧バージョン等でサポートされない場合は無視
        End Try
    End Sub
#End Region

#Region "カラーパレット"
    ' ダークテーマ (VS Code風)
    Public ReadOnly DarkFormBackColor As Color = ColorTranslator.FromHtml("#1E1E1E")
    Public ReadOnly DarkControlBackColor As Color = ColorTranslator.FromHtml("#252526")
    Public ReadOnly DarkInputBackColor As Color = ColorTranslator.FromHtml("#3C3C3C")
    Public ReadOnly DarkForeColor As Color = ColorTranslator.FromHtml("#D4D4D4")
    Public ReadOnly DarkAccentColor As Color = ColorTranslator.FromHtml("#007ACC")
    Public ReadOnly DarkBorderColor As Color = ColorTranslator.FromHtml("#3F3F46")
    Public ReadOnly DarkMenuBackColor As Color = ColorTranslator.FromHtml("#2D2D30")
    Public ReadOnly DarkStatusBarBackColor As Color = ColorTranslator.FromHtml("#007ACC")
    Public ReadOnly DarkGridHeaderBackColor As Color = ColorTranslator.FromHtml("#333333")
    Public ReadOnly DarkGridLineColor As Color = ColorTranslator.FromHtml("#2D2D30")
    Public ReadOnly DarkButtonBackColor As Color = ColorTranslator.FromHtml("#4A4A4D")
    Public ReadOnly DarkDisabledForeColor As Color = ColorTranslator.FromHtml("#808080")
#End Region

#Region "テーマ状態"
    Private _currentTheme As AppTheme = AppTheme.System
    Private _osListenerRegistered As Boolean = False

    ''' <summary>現在のテーマ設定</summary>
    Public Property CurrentTheme As AppTheme
        Get
            Return _currentTheme
        End Get
        Set(value As AppTheme)
            _currentTheme = value
        End Set
    End Property

    ''' <summary>現在の実効テーマがダークかどうか</summary>
    Public Function IsDark() As Boolean
        Select Case _currentTheme
            Case AppTheme.Dark
                Return True
            Case AppTheme.Light
                Return False
            Case Else ' System
                Return DetectOsDarkMode()
        End Select
    End Function
#End Region

#Region "初期化"
    ''' <summary>
    ''' アプリ起動時に呼び出す。保存済みテーマ設定をロードする。
    ''' </summary>
    Public Sub InitializeTheme()
        Try
            _currentTheme = CType(My.Settings.AppTheme, AppTheme)
        Catch
            _currentTheme = AppTheme.System
        End Try
        RegisterOsListener()
    End Sub

    ''' <summary>
    ''' テーマを切り替え、全フォームに適用する。
    ''' </summary>
    Public Sub SetTheme(theme As AppTheme)
        _currentTheme = theme
        My.Settings.AppTheme = CInt(theme)
        My.Settings.Save()
        BroadcastThemeChange()
    End Sub
#End Region

#Region "OS設定検知"
    ''' <summary>
    ''' WindowsレジストリからOSのダークモード設定を検出する。
    ''' </summary>
    Public Function DetectOsDarkMode() As Boolean
        Try
            Using key = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                If key IsNot Nothing Then
                    Dim value = key.GetValue("AppsUseLightTheme")
                    If value IsNot Nothing Then
                        Return CInt(value) = 0
                    End If
                End If
            End Using
        Catch
            ' レジストリ読み取り失敗時はライトテーマ
        End Try
        Return False
    End Function

    Private Sub RegisterOsListener()
        If _osListenerRegistered Then Return
        AddHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged
        _osListenerRegistered = True
    End Sub

    ''' <summary>アプリ終了時に呼び出す。</summary>
    Public Sub UnregisterOsListener()
        If Not _osListenerRegistered Then Return
        RemoveHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged
        _osListenerRegistered = False
    End Sub

    Private Sub OnUserPreferenceChanged(sender As Object, e As UserPreferenceChangedEventArgs)
        If e.Category = UserPreferenceCategory.General AndAlso _currentTheme = AppTheme.System Then
            BroadcastThemeChange()
        End If
    End Sub
#End Region

#Region "テーマ適用"
    ''' <summary>開いている全フォームにテーマを適用する。</summary>
    Public Sub BroadcastThemeChange()
        Dim isDarkMode = IsDark()
        ' Application.OpenForms は列挙中に変更される可能性があるためコピーを作成
        Dim forms = New List(Of Form)()
        For Each frm As Form In Application.OpenForms
            forms.Add(frm)
        Next
        For Each frm In forms
            If frm.IsDisposed Then Continue For
            Dim themeable = TryCast(frm, IThemeable)
            If themeable IsNot Nothing Then
                themeable.ApplyTheme(isDarkMode)
            End If
        Next
    End Sub

    ''' <summary>単一フォームにテーマを適用する。</summary>
    Public Sub ApplyToForm(frm As Form)
        Dim themeable = TryCast(frm, IThemeable)
        If themeable IsNot Nothing Then
            themeable.ApplyTheme(IsDark())
        End If
    End Sub

    ''' <summary>
    ''' コントロールツリーを再帰的に走査し、型に応じたテーマカラーを適用する。
    ''' </summary>
    Public Sub ApplyToControl(ctrl As Control, isDark As Boolean)
        If ctrl Is Nothing Then Return

        ' ToolStrip系はスキップ（ApplyToToolStripで別途処理する）
        If TypeOf ctrl Is ToolStrip Then Return

        ' フォームレベルでレイアウトを一時停止してちらつきを防止
        Dim isTopLevel = TypeOf ctrl Is Form
        If isTopLevel Then
            ctrl.SuspendLayout()
        End If

        ' 型別にスタイルを設定
        Select Case True
            Case TypeOf ctrl Is Form
                ctrl.BackColor = If(isDark, DarkFormBackColor, SystemColors.Control)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)
                ' タイトルバーのダーク/ライト切替
                ApplyTitleBarTheme(DirectCast(ctrl, Form), isDark)

            Case TypeOf ctrl Is DataGridView
                ApplyToDataGridView(DirectCast(ctrl, DataGridView), isDark)

            Case TypeOf ctrl Is TreeView
                Dim tv = DirectCast(ctrl, TreeView)
                tv.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                tv.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is TextBox
                ctrl.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is RichTextBox
                ctrl.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is ListBox
                ctrl.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is ComboBox
                Dim cmb = DirectCast(ctrl, ComboBox)
                cmb.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                cmb.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)
                cmb.FlatStyle = If(isDark, FlatStyle.Flat, FlatStyle.Standard)

            Case TypeOf ctrl Is NumericUpDown
                ctrl.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is Button
                Dim btn = DirectCast(ctrl, Button)
                ' 初回のみ元スタイルを保存（ライト/ダーク問わず）
                If Not _originalButtonStyles.ContainsKey(btn) Then
                    _originalButtonStyles(btn) = (btn.BackColor, btn.ForeColor, btn.FlatStyle)
                End If
                Dim orig = _originalButtonStyles(btn)
                ' アクセントボタン判定: UseVisualStyleBackColor=False かつカスタム色を持つボタン
                Dim isAccentButton = (Not btn.UseVisualStyleBackColor) AndAlso
                                     orig.FlatStyle <> FlatStyle.Flat
                If isDark Then
                    btn.FlatStyle = FlatStyle.Flat
                    btn.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#606060")
                    btn.FlatAppearance.BorderSize = 1
                    If isAccentButton Then
                        btn.BackColor = DarkAccentColor
                        btn.ForeColor = Color.White
                    Else
                        btn.BackColor = DarkButtonBackColor
                        btn.ForeColor = DarkForeColor
                    End If
                Else
                    ' ライトモード: 元スタイルを復元
                    btn.BackColor = orig.BackColor
                    btn.ForeColor = orig.ForeColor
                    btn.FlatStyle = orig.FlatStyle
                    btn.UseVisualStyleBackColor = Not isAccentButton
                End If

            Case TypeOf ctrl Is CheckBox
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            Case TypeOf ctrl Is RadioButton
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            Case TypeOf ctrl Is Label
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            Case TypeOf ctrl Is GroupBox
                ctrl.BackColor = If(isDark, DarkControlBackColor, SystemColors.Control)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            Case TypeOf ctrl Is Panel
                ctrl.BackColor = If(isDark, DarkFormBackColor, SystemColors.Control)

            Case TypeOf ctrl Is SplitContainer
                ctrl.BackColor = If(isDark, DarkFormBackColor, SystemColors.Control)

            Case TypeOf ctrl Is TabControl
                ctrl.BackColor = If(isDark, DarkControlBackColor, SystemColors.Control)

            Case TypeOf ctrl Is TabPage
                ctrl.BackColor = If(isDark, DarkControlBackColor, SystemColors.Control)
                ctrl.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            Case TypeOf ctrl Is ListView
                Dim lv = DirectCast(ctrl, ListView)
                lv.BackColor = If(isDark, DarkInputBackColor, SystemColors.Window)
                lv.ForeColor = If(isDark, DarkForeColor, SystemColors.WindowText)

            Case TypeOf ctrl Is TableLayoutPanel
                ctrl.BackColor = If(isDark, DarkFormBackColor, SystemColors.Control)

            Case TypeOf ctrl Is LinkLabel
                Dim ll = DirectCast(ctrl, LinkLabel)
                ll.LinkColor = If(isDark, ColorTranslator.FromHtml("#4FC1FF"), SystemColors.HotTrack)
                ll.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)
        End Select

        ' 子コントロールを再帰的に処理（コレクション変更対策でコピー）
        Dim children(ctrl.Controls.Count - 1) As Control
        ctrl.Controls.CopyTo(children, 0)
        For Each child As Control In children
            ApplyToControl(child, isDark)
        Next

        ' フォームレベルでレイアウトを再開
        If isTopLevel Then
            ctrl.ResumeLayout(True)
        End If
    End Sub

    ''' <summary>DataGridViewにテーマを適用する。</summary>
    Private Sub ApplyToDataGridView(dgv As DataGridView, isDark As Boolean)
        If isDark Then
            dgv.BackgroundColor = DarkControlBackColor
            dgv.GridColor = DarkGridLineColor
            dgv.DefaultCellStyle.BackColor = DarkFormBackColor
            dgv.DefaultCellStyle.ForeColor = DarkForeColor
            dgv.DefaultCellStyle.SelectionBackColor = DarkAccentColor
            dgv.DefaultCellStyle.SelectionForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkGridHeaderBackColor
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = DarkForeColor
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = DarkGridHeaderBackColor
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = DarkForeColor
            dgv.RowHeadersDefaultCellStyle.BackColor = DarkGridHeaderBackColor
            dgv.RowHeadersDefaultCellStyle.ForeColor = DarkForeColor
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = DarkGridHeaderBackColor
            dgv.RowHeadersDefaultCellStyle.SelectionForeColor = DarkForeColor
            dgv.EnableHeadersVisualStyles = False
            dgv.AlternatingRowsDefaultCellStyle.BackColor = DarkControlBackColor
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = DarkForeColor
        Else
            dgv.BackgroundColor = SystemColors.Window
            dgv.GridColor = SystemColors.ControlDark
            dgv.DefaultCellStyle.BackColor = SystemColors.Window
            dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText
            dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
            dgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            dgv.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control
            dgv.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight
            dgv.RowHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            dgv.EnableHeadersVisualStyles = True
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Empty
        End If
    End Sub
#End Region

#Region "ToolStrip/MenuStrip/StatusStrip"
    Private _darkRenderer As DarkToolStripRenderer
    Private ReadOnly _defaultRenderer As New ToolStripProfessionalRenderer()

    ''' <summary>元画像を保存するマップ (復元用)</summary>
    Private ReadOnly _originalImages As New Dictionary(Of ToolStripItem, Image)()

    ''' <summary>元のボタンスタイルを保存するマップ (復元用)</summary>
    Private ReadOnly _originalButtonStyles As New Dictionary(Of Button, (BackColor As Color, ForeColor As Color, FlatStyle As FlatStyle))()

    ''' <summary>
    ''' MenuStrip/ToolStrip/StatusStrip にテーマを適用する。
    ''' </summary>
    Public Sub ApplyToToolStrip(strip As ToolStrip, isDark As Boolean)
        If isDark Then
            If _darkRenderer Is Nothing Then _darkRenderer = New DarkToolStripRenderer()
            strip.Renderer = _darkRenderer
            ' BackColor も明示的に設定（レンダラーだけでは反映されない箇所がある）
            If TypeOf strip Is StatusStrip Then
                strip.BackColor = DarkStatusBarBackColor
            Else
                strip.BackColor = DarkMenuBackColor
            End If
            strip.ForeColor = DarkForeColor
        Else
            strip.RenderMode = ToolStripRenderMode.ManagerRenderMode
            strip.BackColor = SystemColors.Control
            strip.ForeColor = SystemColors.ControlText
        End If

        ' ToolStripItem のテキスト色を明示的に設定
        ApplyToolStripItemColors(strip.Items, isDark)

        ' アイコン画像の反転/復元
        InvertToolStripImages(strip.Items, isDark)

        ' 強制再描画
        strip.Invalidate()
        strip.Update()
    End Sub

    ''' <summary>
    ''' ToolStripItem のテキスト色を再帰的に設定する。
    ''' </summary>
    Private Sub ApplyToolStripItemColors(items As ToolStripItemCollection, isDark As Boolean)
        ' コレクション変更による列挙エラーを防ぐためコピーを作成
        Dim itemsCopy(items.Count - 1) As ToolStripItem
        items.CopyTo(itemsCopy, 0)

        For Each item As ToolStripItem In itemsCopy
            item.ForeColor = If(isDark, DarkForeColor, SystemColors.ControlText)

            ' ドロップダウンメニューの子アイテムも再帰処理
            Dim menuItem = TryCast(item, ToolStripMenuItem)
            If menuItem IsNot Nothing AndAlso menuItem.HasDropDownItems Then
                ' ドロップダウンメニュー自体にもレンダラーを設定
                If isDark Then
                    menuItem.DropDown.BackColor = DarkMenuBackColor
                Else
                    menuItem.DropDown.BackColor = SystemColors.Menu
                End If
                ApplyToolStripItemColors(menuItem.DropDownItems, isDark)
            End If
        Next
    End Sub

    ''' <summary>
    ''' ToolStripItem のアイコン画像を再帰的に反転（ダーク）/ 復元（ライト）する。
    ''' </summary>
    Private Sub InvertToolStripImages(items As ToolStripItemCollection, isDark As Boolean)
        ' コレクション変更による列挙エラーを防ぐためコピーを作成
        Dim itemsCopy(items.Count - 1) As ToolStripItem
        items.CopyTo(itemsCopy, 0)

        For Each item As ToolStripItem In itemsCopy
            If item.Image IsNot Nothing Then
                If isDark Then
                    If Not _originalImages.ContainsKey(item) Then
                        _originalImages(item) = item.Image
                    End If
                    item.Image = AdjustIconForDarkTheme(_originalImages(item))
                Else
                    If _originalImages.ContainsKey(item) Then
                        item.Image = _originalImages(item)
                        _originalImages.Remove(item)
                    End If
                End If
            End If

            ' ドロップダウンメニューの子アイテムも再帰処理
            Dim menuItem = TryCast(item, ToolStripMenuItem)
            If menuItem IsNot Nothing AndAlso menuItem.HasDropDownItems Then
                InvertToolStripImages(menuItem.DropDownItems, isDark)
            End If
        Next
    End Sub

    ''' <summary>
    ''' ダークテーマ用にアイコンを調整する。
    ''' 黒い輪郭線・暗いグレーをライトグレーに変換し、
    ''' 色付きピクセル（緑・赤・青等）は元の色を保持する。
    ''' </summary>
    Private Function AdjustIconForDarkTheme(src As Image) As Image
        Dim bmp As New Bitmap(src)
        For y As Integer = 0 To bmp.Height - 1
            For x As Integer = 0 To bmp.Width - 1
                Dim c = bmp.GetPixel(x, y)
                If c.A < 10 Then Continue For ' 透明ピクセルはスキップ

                Dim maxC As Integer = Math.Max(c.R, Math.Max(c.G, c.B))
                Dim minC As Integer = Math.Min(c.R, Math.Min(c.G, c.B))
                Dim saturation As Double = If(maxC = 0, 0, (maxC - minC) / CDbl(maxC))
                Dim brightness As Double = maxC / 255.0

                If brightness < 0.4 AndAlso saturation < 0.3 Then
                    ' 暗い無彩色ピクセル（黒い輪郭線等）→ ライトグレーに
                    Dim newVal As Integer = CInt(190 + brightness * 65)
                    bmp.SetPixel(x, y, Color.FromArgb(c.A, newVal, newVal, newVal))
                ElseIf brightness < 0.5 AndAlso saturation >= 0.3 Then
                    ' 暗い有彩色ピクセル → 少し明るくして色を保持
                    Dim factor As Double = 1.6
                    Dim r = Math.Min(255, CInt(c.R * factor))
                    Dim g = Math.Min(255, CInt(c.G * factor))
                    Dim b = Math.Min(255, CInt(c.B * factor))
                    bmp.SetPixel(x, y, Color.FromArgb(c.A, r, g, b))
                End If
                ' 明るいピクセル・色付きピクセルはそのまま
            Next
        Next
        Return bmp
    End Function
#End Region

End Module
