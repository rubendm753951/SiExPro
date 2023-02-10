Imports System.IO
Imports System.Net
Imports Microsoft.VisualBasic

Public Class PayPalApiCaller
    Private Const CVV2 As String = "CVV2"

    Private pEndPointURL As String = ConfigurationManager.AppSettings("PayPal.pEndPointURL")
    Private host As String = ConfigurationManager.AppSettings("PayPal.host")

    Private pEndPointURL_SB As String = ConfigurationManager.AppSettings("PayPal.pEndPointURL_SB")
    Private host_SB As String = ConfigurationManager.AppSettings("PayPal.host_SB")

    Private Const USER As String = "USER"
    Private Const SIGNATURE As String = "SIGNATURE"
    Private Const PWD As String = "PWD"
    Private Const ACCT As String = "ACCT"
    Private Const SUBJECT As String = "SUBJECT"

    Public APIUsername As String = ConfigurationManager.AppSettings("PayPal.APIUsername")
    Private APIPassword As String = ConfigurationManager.AppSettings("PayPal.APIPassword")
    Private APISignature As String = ConfigurationManager.AppSettings("PayPal.APISignature")
    Private BNCode As String = ConfigurationManager.AppSettings("PayPal.BNCode")


    Private Const Timeout As Integer = 15000
    Private Shared ReadOnly SECURED_NVPS() As String = {ACCT, CVV2, SIGNATURE, PWD}

    Public Sub SetCredentials(Userid As String, Pwd As String, Signature As String)

        APIUsername = Userid
        APIPassword = Pwd
        APISignature = Signature

    End Sub

    Public Function ShortcutExpressCheckout(amt As String, ByRef token As String, ByRef retMsg As String) As Boolean

        pEndPointURL = pEndPointURL_SB
        host = host_SB

        Dim returnURL As String = ConfigurationManager.AppSettings("PayPal.Return")
        Dim cancelURL As String = ConfigurationManager.AppSettings("PayPal.Cancel")
        Dim encoder As NVPCodec = New NVPCodec

        encoder("METHOD") = "SetExpressCheckout"
        encoder("RETURNURL") = returnURL
        encoder("CANCELURL") = cancelURL
        encoder("BRANDNAME") = ConfigurationManager.AppSettings("PayPal.BRANDNAME")
        encoder("PAYMENTREQUEST_0_AMT") = amt
        encoder("PAYMENTREQUEST_0_ITEMAMT") = amt
        encoder("PAYMENTREQUEST_0_PAYMENTACTION") = "Sale"
        encoder("PAYMENTREQUEST_0_CURRENCYCODE") = "USD"

        encoder("L_PAYMENTREQUEST_0_NAME0") = HttpContext.Current.Session("ProductName").ToString()
        encoder("L_PAYMENTREQUEST_0_AMT0") = HttpContext.Current.Session("UnitPrice").ToString()
        encoder("L_PAYMENTREQUEST_0_QTY0") = "1"

        Dim pStrrequestforNvp As String = encoder.Encode()
        Dim pStresponsenvp As String = HttpCall(pStrrequestforNvp)

        Dim Decoder As NVPCodec = New NVPCodec()
        Decoder.Decode(pStresponsenvp)

        Dim strAck As String = Decoder("ACK").ToLower()
        If strAck IsNot Nothing AndAlso (strAck = "success" OrElse strAck = "successwithwarning") Then

            token = Decoder("TOKEN")
            Dim ECURL As String = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token
            retMsg = ECURL

            Return True

        Else

            retMsg = "ErrorCode=" + Decoder("L_ERRORCODE0") + "&" +
                "Desc=" + Decoder("L_SHORTMESSAGE0") + "&" +
                "Desc2=" + Decoder("L_LONGMESSAGE0")
            Return False
        End If

    End Function

    Public Function GetCheckoutDetails(token As String, ByRef PayerID As String, ByRef decoder As NVPCodec, ByRef retMsg As String) As Boolean

        pEndPointURL = pEndPointURL_SB

        Dim encoder As NVPCodec = New NVPCodec

        encoder("METHOD") = "GetExpressCheckoutDetails"
        encoder("TOKEN") = token

        Dim pStrrequestforNvp As String = encoder.Encode()
        Dim pStresponsenvp As String = HttpCall(pStrrequestforNvp)

        decoder = New NVPCodec
        decoder.Decode(pStresponsenvp)

        Dim strAck As String = decoder("ACK").ToLower()
        If strAck IsNot Nothing AndAlso (strAck = "success" OrElse strAck = "successwithwarning") Then

            PayerID = decoder("PAYERID")
            Return True

        Else

            retMsg = "ErrorCode=" + decoder("L_ERRORCODE0") + "&" +
                "Desc=" + decoder("L_SHORTMESSAGE0") + "&" +
                "Desc2=" + decoder("L_LONGMESSAGE0")

            Return False
        End If
    End Function

    Public Function DoCheckoutPayment(finalPaymentAmount As String, token As String, PayerID As String, ByRef decoder As NVPCodec, ByRef retMsg As String) As Boolean

        pEndPointURL = pEndPointURL_SB

        Dim Encoder As NVPCodec = New NVPCodec()
        Encoder("METHOD") = "DoExpressCheckoutPayment"
        Encoder("TOKEN") = token
        Encoder("PAYERID") = PayerID
        Encoder("PAYMENTREQUEST_0_AMT") = finalPaymentAmount
        Encoder("PAYMENTREQUEST_0_CURRENCYCODE") = "USD"
        Encoder("PAYMENTREQUEST_0_PAYMENTACTION") = "Sale"

        Dim pStrrequestforNvp As String = Encoder.Encode()
        Dim pStresponsenvp As String = HttpCall(pStrrequestforNvp)

        decoder = New NVPCodec()
        decoder.Decode(pStresponsenvp)

        Dim strAck As String = decoder("ACK").ToLower()

        If strAck IsNot Nothing AndAlso (strAck = "success" OrElse strAck = "successwithwarning") Then

            Return True

        Else

            retMsg = "ErrorCode=" + decoder("L_ERRORCODE0") + "&" +
                "Desc=" + decoder("L_SHORTMESSAGE0") + "&" +
                "Desc2=" + decoder("L_LONGMESSAGE0")

            Return False
        End If

    End Function

    Public Function HttpCall(NvpRequest As String) As String

        Dim url As String = pEndPointURL
        Dim strPost As String = NvpRequest + "&" + buildCredentialsNVPString()

        strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode)

        Dim objRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        objRequest.Timeout = Timeout
        objRequest.Method = "POST"
        objRequest.ContentLength = strPost.Length
        ServicePointManager.SecurityProtocol = 3072

        Try
            Using myWriter As StreamWriter = New StreamWriter(objRequest.GetRequestStream())

                myWriter.Write(strPost)
            End Using
        Catch e As Exception

        End Try

        Dim objResponse As HttpWebResponse = CType(objRequest.GetResponse(), HttpWebResponse)
        Dim result As String

        Using sr As StreamReader = New StreamReader(objResponse.GetResponseStream())
            result = sr.ReadToEnd()
        End Using

        Return result

    End Function

    Private Function buildCredentialsNVPString() As String

        Dim codec As NVPCodec = New NVPCodec

        If Not IsEmpty(APIUsername) Then codec(USER) = APIUsername

        If Not IsEmpty(APIPassword) Then codec(PWD) = APIPassword

        If Not IsEmpty(APISignature) Then codec(SIGNATURE) = APISignature

        codec(SUBJECT) = ""
        codec("VERSION") = "88.0"

        Return codec.Encode()

    End Function

    Public Shared Function IsEmpty(s As String) As Boolean

        Return s Is Nothing OrElse s.Trim() = String.Empty

    End Function

End Class

Public NotInheritable Class NVPCodec

    Inherits NameValueCollection

    Private Const AMPERSAND As String = "&"
    Private Const _EQUALS As String = "="
    Private Shared ReadOnly AMPERSAND_CHAR_ARRAY As Char() = AMPERSAND.ToCharArray()
    Private Shared ReadOnly EQUALS_CHAR_ARRAY As Char() = _EQUALS.ToCharArray()

    Public Function Encode() As String

        Dim sb As StringBuilder = New StringBuilder
        Dim firstPair As Boolean = True

        For Each kv As String In AllKeys

            Dim name As String = HttpUtility.UrlEncode(kv)
            Dim value As String = HttpUtility.UrlEncode(Me(kv))
            If Not firstPair Then sb.Append(AMPERSAND)

            sb.Append(name).Append(_EQUALS).Append(value)
            firstPair = False
        Next

        Return sb.ToString()
    End Function

    Public Sub Decode(nvpstring As String)

        Clear()
        For Each nvp As String In nvpstring.Split(AMPERSAND_CHAR_ARRAY)

            Dim tokens() As String = nvp.Split(EQUALS_CHAR_ARRAY)
            If (tokens.Length >= 2) Then
                Dim name As String = HttpUtility.UrlDecode(tokens(0))
                Dim value As String = HttpUtility.UrlDecode(tokens(1))
                Add(name, value)
            End If
        Next

    End Sub

    Public Sub AddItem(name As String, value As String, index As Integer)
        Add(GetArrayName(index, name), value)
    End Sub

    Public Sub RemoveItem(arrayName As String, index As Integer)
        Me.Remove(GetArrayName(index, arrayName))
    End Sub

    Default Public Overloads Property Item(name As String, index As String) As String
        Get
            Return GetArrayName(index, name)
        End Get
        Set(value As String)
            Me(GetArrayName(index, name)) = value
        End Set
    End Property

    Private Shared Function GetArrayName(index As Integer, name As String) As String

        If index < 0 Then
            Throw New ArgumentOutOfRangeException("index", "index cannot be negative : " + index)
        End If

        Return name + index

    End Function

End Class