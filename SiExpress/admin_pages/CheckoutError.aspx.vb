
Partial Class admin_pages_CheckoutError
    Inherits BasePage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))

        Try
            Dim errorCode As String = Request.QueryString("ErrorCode")
            Dim desc1 As String = Request.QueryString("Desc")
            Dim desc2 As String = Request.QueryString("Desc2")

            Dim errorLog = New ErrorLog
            errorLog.ErrorLog("Paypal_CheckoutError", errorCode + " - " + desc1 + ", " + desc2, "", usuarioId.ToString, "")
        Catch ex As Exception
            Dim errorLog = New ErrorLog
            errorLog.ErrorLog("Paypal_CheckoutError", ex.Message, ex.Source, usuarioId.ToString, "")
        End Try
    End Sub
End Class
