
Partial Class admin_pages_CheckoutReview
    Inherits BasePageNoLogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.Session("id_usuario") IsNot Nothing Then
            If Not IsPostBack Then

                Dim payPalCaller As PayPalApiCaller = New PayPalApiCaller

                Dim retMsg = ""
                Dim PayerID = ""
                Dim Decoder = New NVPCodec()
                Dim token = Session("token").ToString()

                Dim ret As Boolean = payPalCaller.GetCheckoutDetails(token, PayerID, Decoder, retMsg)
                If ret Then

                    Session("payerId") = PayerID

                    Dim myOrder As Object = New With
                    {
                    .OrderDate = Convert.ToDateTime(Decoder("TIMESTAMP")), .Username = User.Identity.Name,
                    .FirstName = Decoder("FIRSTNAME").ToString(), .LastName = Decoder("LASTNAME").ToString(),
                    .Address = Decoder("SHIPTOSTREET").ToString(), .City = Decoder("SHIPTOCITY").ToString(),
                    .State = Decoder("SHIPTOSTATE").ToString(), .PostalCode = Decoder("SHIPTOZIP").ToString(),
                    .Country = Decoder("SHIPTOCOUNTRYCODE").ToString(), .Email = Decoder("EMAIL").ToString(),
                    .Total = Convert.ToDecimal(Decoder("AMT").ToString())
                    }

                    Try

                        Dim paymentAmountOnCheckout As Decimal = Convert.ToDecimal(Session("payment_amt").ToString())
                        Dim paymentAmoutFromPayPal As Decimal = Convert.ToDecimal(Decoder("AMT").ToString())

                        If paymentAmountOnCheckout <> paymentAmoutFromPayPal Then
                            Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.")
                        End If

                    Catch ex As Exception
                        Response.Redirect("CheckoutError.aspx?" + "Desc=Error%20al%20comparar%20totales.")
                    End Try

                    Dim orderList As List(Of Object) = New List(Of Object)
                    orderList.Add(myOrder)
                    ShipInfo.DataSource = orderList
                    ShipInfo.DataBind()

                Else
                    Response.Redirect("CheckoutError.aspx?" + retMsg)

                End If

            End If
        Else
            Response.Redirect("CheckoutError.aspx?ErrorCode=1000&Desc=La sesion ha caducado, usuario no registrado.")
        End If

    End Sub

    Protected Sub CheckoutConfirm_Click(sender As Object, e As EventArgs)
        Session("userCheckoutCompleted") = "true"
        Response.Redirect("CheckoutConfirmation.aspx")
    End Sub
End Class
