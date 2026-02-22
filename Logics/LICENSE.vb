Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.Json

''' <summary>
''' 公開鍵暗号方式によるライセンス検証ロジック
''' </summary>
Public Class LICENSE

#Region "公開鍵定義"
    ''' <summary>
    ''' 公開鍵（PEM形式 or XML形式）
    ''' </summary>
    Public Const PublicKeyXml As String = "<RSAKeyValue><Modulus>u7Phr86EDhhUuKAqZnOL/W4lkby6NIHhaOhCuqBAmEjm0Esna3GpEYIup1guwm69UWHEAf5wJSGgDfSrOYuP3agUKXl/uQFOXbg23aDidLaH9gf6uuqhBDtDozHlJaT0uc1YAfQEiD+7RKshqCZd8lwK6Z9fLZ9Ae+pFZsBavACI39UC8Kgc7bJthZbDBQlMbCTQP9XI0CBXo+X6D3D71DWNuLyD0V90IVG01lch19QSmjKCwWWwgy96D+0+5pV22FIcZwCljTyuI9DNpW9ZhMqXsSz3T73YIZxhEl7CQodgeIqstGYR4yZTqOD9hn29ACuX2Tp+N/pIZBgW+M5m5Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue> "
#End Region

#Region "ライセンス検証メイン処理"
    ''' <summary>
    ''' ライセンスファイル（JSON+署名）を検証し、情報を抽出
    ''' </summary>
    ''' <param name="licensePath">ライセンスファイルパス</param>
    ''' <param name="product">[out] 製品名</param>
    ''' <param name="expiryDate">[out] 有効期限</param>
    ''' <param name="holder">[out] 使用者名</param>
    ''' <param name="errMsg">[out] エラー内容</param>
    ''' <returns>有効な場合True</returns>
    Public Shared Function VerifyLicenseFile(licensePath As String, ByRef product As String, ByRef expiryDate As DateTime, ByRef holder As String, ByRef errMsg As String) As Boolean
        product = "" : expiryDate = Date.MinValue : holder = "" : errMsg = ""
        Try
            Dim jsonText = File.ReadAllText(licensePath, Encoding.UTF8)
            Dim doc = JsonDocument.Parse(jsonText).RootElement
            Dim dataB64 = doc.GetProperty("data").GetString()
            Dim sigB64 = doc.GetProperty("signature").GetString()
            Dim dataBytes = Convert.FromBase64String(dataB64)
            Dim sigBytes = Convert.FromBase64String(sigB64)
            ' 公開鍵で署名検証
            Using rsa As RSA = RSA.Create()
                rsa.FromXmlString(PublicKeyXml)
                If Not rsa.VerifyData(dataBytes, sigBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1) Then
                    errMsg = "署名検証に失敗" : Return False
                End If
            End Using
            ' data部をパース
            Dim dataJson = Encoding.UTF8.GetString(dataBytes)
            Dim dataObj = JsonDocument.Parse(dataJson).RootElement
            product = dataObj.GetProperty("Product").GetString()
            Dim expiryStr = dataObj.GetProperty("Expiry").GetString()
            If dataObj.TryGetProperty("Holder", Nothing) AndAlso dataObj.GetProperty("Holder").ValueKind <> JsonValueKind.Null Then
                holder = dataObj.GetProperty("Holder").GetString()
            Else
                holder = ""
            End If
            If Not DateTime.TryParse(expiryStr, expiryDate) Then errMsg = "有効期限パース失敗" : Return False
            If DateTime.Now.Date > expiryDate.Date Then errMsg = "有効期限切れ" : Return False
            Return True
        Catch ex As Exception
            errMsg = "検証エラー: " & ex.Message
            Return False
        End Try
    End Function
#End Region

#Region "ライセンス情報取得"
    ''' <summary>
    ''' 現在のライセンスファイルからHolder（使用者名）を取得
    ''' </summary>
    Public Shared Function GetLicenseHolder() As String
        Try
            Dim appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OracleDUMPViewer")
            Dim statusPath = Path.Combine(appData, "license.status")
            If Not File.Exists(statusPath) Then Return ""
            Dim product As String = ""
            Dim expiryDate As DateTime
            Dim holder As String = ""
            Dim errMsg As String = ""
            If LICENSE.VerifyLicenseFile(statusPath, product, expiryDate, holder, errMsg) Then
                Return holder
            End If
            Return ""
        Catch
            Return ""
        End Try
    End Function
#End Region

End Class
