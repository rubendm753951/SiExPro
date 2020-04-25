
Partial Class ops_pages_devolucion
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim devolucion As New seguimiento_envios
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = devolucion.valida_referencia(txtEnvio.Text)

            If TxtObs.Text <> "" And Not IsNothing(TxtObs.Text) And IsNumeric(id_envio) Then
                Dim mensaje As String = ""
                Dim permitido As Boolean = True
                devolucion.valida_flujo(id_envio, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
                If permitido Then
                    devolucion.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, TxtObs.Text, Session("id_usuario"))
                    Label2.Text = "Envío " + id_envio.ToString + " El envío está ahora en proces de devolución " + Date.Today.ToString
                Else
                    Label2.Text = "Envío " + id_envio.ToString + ", " + mensaje
                End If
            Else
                Button2.Visible = True
                txtMsg.Visible = True
                txtMsg.Text = "El número de envío no es válido o falta un comentario"
            End If
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        txtMsg.Visible = False
        Button2.Visible = False
    End Sub
End Class
