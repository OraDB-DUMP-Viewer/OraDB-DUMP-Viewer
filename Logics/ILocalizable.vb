''' <summary>
''' ローカライズ可能なフォームが実装するインターフェース。
''' ApplyLocalization() を実装し、InitializeComponent() 直後に呼び出すこと。
''' </summary>
Public Interface ILocalizable
    Sub ApplyLocalization()
End Interface
