
Partial Class admin_pages_modif_cancel
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim entrega As New seguimiento_envios
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = entrega.valida_referencia(txtEnvio.Text)

            If TxtObs.Text <> "" And Not IsNothing(TxtObs.Text) And IsNumeric(id_envio) Then
                Dim mensaje As String = ""
                Dim permitido As Boolean = True
                entrega.valida_flujo(id_envio, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
                If permitido Then 'mensaje = "ok" Or mensaje = "Este envio ya ha sido entregado" Then
                    entrega.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, TxtObs.Text, Session("id_usuario"))
                    Label2.Text = "Envío " + id_envio.ToString + " confirmado de entrega con éxito en " + Date.Today.ToString
                Else
                    Label2.Text = "Envío " + id_envio.ToString + ", " + mensaje
                End If
            Else
                Button2.Visible = True
                txtMsg.Visible = True
                txtMsg.Text = "El número de envío no es válido o falta nombre de quien recibe"
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
