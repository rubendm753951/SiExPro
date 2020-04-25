
Partial Class admin_pages_admin_adeudo_empresa
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim empresa As Empresa
        If Session("empresa") IsNot Nothing Then
            If Not IsPostBack Then

                Try
                    empresa = CType(Session("empresa"), Empresa)

                    Dim adeudo As Decimal = DaspackDALC.GetAdeudoEmpresaTotal(empresa.id_empresa)

                    If adeudo > 0 Then
                        txtTotal.Text = adeudo.ToString("0.00")
                    Else
                        txtTotal.Text = "0.00"
                    End If

                Catch ex As Exception

                End Try
            End If
        Else
            Response.Redirect("~/no_access.aspx")
        End If
    End Sub

    Protected Sub CheckoutImageBtn_Click(sender As Object, e As ImageClickEventArgs) Handles CheckoutImageBtn.Click
        Dim payPalCaller As PayPalApiCaller = New PayPalApiCaller()

        Session("payment_amt") = txtTotal.Text
        Session("ProductName") = "Saldo cliente: " + Session("user_name")
        Session("UnitPrice") = txtTotal.Text

        If txtTotal.Text <> "0" AndAlso IsNumeric(txtTotal.Text) Then

            Dim amt As String = txtTotal.Text
            Dim retMsg As String = ""
            Dim token As String = ""
            Dim ret As Boolean = payPalCaller.ShortcutExpressCheckout(amt, token, retMsg)

            If ret Then
                Session("token") = token
                Response.Redirect(retMsg)

            Else
                Response.Redirect("CheckoutError.aspx?" + retMsg)

            End If

        Else
            Response.Redirect("CheckoutError.aspx?ErrorCode=AmtMissing")

        End If
    End Sub
End Class
