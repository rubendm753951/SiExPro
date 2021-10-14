
Imports DataAccess.HelpersT
Imports Entities

Partial Class admin_pages_CheckoutConfirmation
    Inherits BasePageNoLogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.Session("id_usuario") IsNot Nothing Then
            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            If usuarioId > 0 Then
                If Not IsPostBack Then
                    If (Session("userCheckoutCompleted").ToString <> "true") Then
                        Session("userCheckoutCompleted") = String.Empty
                        Response.Redirect("CheckoutError.aspx?" + "Desc=Unvalidated%20Checkout.")
                    End If

                    Dim payPalCaller As PayPalApiCaller = New PayPalApiCaller

                    Dim retMsg As String = ""
                    Dim token As String = ""
                    Dim finalPaymentAmount As String = ""
                    Dim PayerID As String = ""
                    Dim Decoder As NVPCodec = New NVPCodec()

                    token = Session("token").ToString()
                    PayerID = Session("payerId").ToString()
                    finalPaymentAmount = Session("payment_amt").ToString()

                    Dim referencia As New StringBuilder
                    referencia.Append("Token: " + token)
                    referencia.Append(" PayerID: " + PayerID)
                    referencia.Append(" Monto: " + finalPaymentAmount)


                    Dim ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, PayerID, Decoder, retMsg)

                    If ret Then
                        Dim PaymentConfirmation As String = Decoder("PAYMENTINFO_0_TRANSACTIONID").ToString()
                        TransactionId.Text = PaymentConfirmation

                        referencia.Append(" PaymentConfirmation: " + PaymentConfirmation)

                        Dim idEmpresa As Integer = CType(Session("id_empresa"), Integer)
                        Dim adeudoEmpresasList As List(Of AdeudoEmpresa) = DaspackDALC.GetAdeudos(idEmpresa)
                        Dim pago As Decimal = CType(finalPaymentAmount, Decimal)
                        Dim facturas As New StringBuilder

                        For Each adeudoEmpresa As AdeudoEmpresa In adeudoEmpresasList
                            Dim adeudoMes As Decimal = adeudoEmpresa.Mensualidad - adeudoEmpresa.Deposito + (adeudoEmpresa.NumeroEnvios * adeudoEmpresa.TarifaPorEnvio)

                            If pago > 0 AndAlso pago >= adeudoMes Then
                                DaspackDALC.UpdateAdeudoEmpresa(adeudoEmpresa.idFactura, usuarioId, referencia.ToString(), "Abono realizado por medio de PayPal.", True, adeudoMes + adeudoEmpresa.Deposito)

                                pago = pago - adeudoMes

                                facturas.Append(adeudoEmpresa.idFactura.ToString() + ", ")
                            ElseIf pago > 0 AndAlso pago < adeudoMes Then
                                DaspackDALC.UpdateAdeudoEmpresa(adeudoEmpresa.idFactura, usuarioId, referencia.ToString(), "Abono parcial realizado por medio de PayPal.", False, pago + adeudoEmpresa.Deposito)

                                pago = 0

                                facturas.Append(adeudoEmpresa.idFactura.ToString() + " (Parcial), ")
                            End If
                        Next

                        Dim usuario As Usuario = DaspackDALC.GetDatosUsuario(usuarioId)
                        If usuario IsNot Nothing Then
                            Dim mailSender As New MailSender
                            mailSender.EmailType = 2
                            mailSender.RecipientAddress = usuario.email
                            mailSender.RecipientName = usuario.nombre
                            mailSender.BodyComplement = vbNewLine + usuario.nombre + vbNewLine +
                                 vbNewLine + "Usuario: " + usuario.login +
                                 vbNewLine + "Email: " + usuario.email +
                                 vbNewLine + "Monto: " + String.Format("{0}", finalPaymentAmount) +
                                 vbNewLine + "Referencia Paypal: " + PaymentConfirmation +
                                 vbNewLine + "Factura: " + facturas.ToString()

                            mailSender.SendMail()
                        End If
                    Else
                        Response.Redirect("CheckoutError.aspx?" + retMsg)
                    End If

                End If
            Else
                Response.Redirect("CheckoutError.aspx?ErrorCode=1000&Desc=La sesion ha caducado, usuario no registrado.")
            End If
        Else
            Response.Redirect("CheckoutError.aspx?ErrorCode=1000&Desc=La sesion ha caducado, usuario no registrado.")
        End If
    End Sub

    Protected Sub Continue_Click(sender As Object, e As EventArgs)
        Response.Redirect("admin_myaccount.aspx?saldo=1")
    End Sub
End Class
