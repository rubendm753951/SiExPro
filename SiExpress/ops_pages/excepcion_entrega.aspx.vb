
Partial Class ops_pages_excepcion_entrega
    Inherits BasePage
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim excepcion As New seguimiento_envios
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = excepcion.valida_referencia(txtEnvio.Text)

            Dim mensaje As String = ""
            Dim permitido As Boolean = True
            excepcion.valida_flujo(id_envio, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
            If permitido Then
                Dim obs As String
                obs = ListExceptions.SelectedItem.Text & " (" & txtObs.Text & ")"
                excepcion.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, obs, Session("id_usuario"))
                Label2.Text = "Envío " + id_envio.ToString + " Excepción registrada con éxito en " + Date.Today.ToString
                txtEnvio.Text = ""
                txtEnvio.Focus()
            Else
                Label2.Text = "Envío " + id_envio.ToString + ", " + mensaje
            End If
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub
End Class
