Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.Json

''' <summary>
''' 公開鍵暗号方式によるライセンス検証ロジック
''' </summary>
Public Class LICENSE
    ''' <summary>
    ''' 公開鍵（PEM形式 or XML形式）
    ''' </summary>
    Public Const PublicKeyXml As String = "<RSAKeyValue><Modulus>vCjZ1HeSgqUO5oWANZhprgAlPN0YzBF2rCU9pvQ1mgWC61vcEHxZ2nrjxhu5Uptx3Unkeexa7Rt+Dz1Bxs+q+2nNr8dFVvoyDk+j/gzVNfBK1OBOW7lkxrO2tMvXjoBnHda1CQqn2RHeaxHprWsLDBKGnsPvTnVoNBuAyX7AadVF0xt9jQf1ZnTbTPjqNOeaQE8T3QcAfa64girV6ED/u/2cpp3N1CyshWKVwDKsBYYj7w3/JmOeetq4/eC2uuPZJa1SgQj0zNMPZx+az2wKlFMU5tJT63udSl9nmKvmEr57XCScYpL4BWbKV48P+nmXNg/NAbesE9rZZgQrmINGmw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"

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

End Class
