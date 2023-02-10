
Partial Class admin_pages_modif_cancel
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not txtObs.Text = "" And Not IsNothing(txtObs.Text) And IsNumeric(txtEnvio.Text) Then
                Dim cancela As New seguimiento_envios
                Dim mensaje As String = ""
                Dim permitido As Boolean = True
                cancela.valida_flujo(txtEnvio.Text, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
                If permitido Then
                    cancela.insertar_seguimiento(txtEnvio.Text, Me.AppRelativeVirtualPath.ToString, txtObs.Text, Session("id_usuario"))
                    Label2.Text = "El envío " + txtEnvio.Text + " fue cancelado con éxito"
                    txtEnvio.Text = ""
                    txtEnvio.Focus()
                Else
                    Label2.Text = "Envío " + txtEnvio.Text + ", " + mensaje
                End If
            Else
                Button2.Visible = True
                txtMsg.Visible = True
                txtMsg.Text = "El número de envío no es válido o faltan las observaciones"
            End If
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub
End Class
