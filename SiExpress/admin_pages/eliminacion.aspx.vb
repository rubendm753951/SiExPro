
Partial Class admin_pages_eliminacion
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim eliminacion As New seguimiento_envios
            Dim movimiento As String = "cambia_status"
            Dim mensaje As String = ""
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = eliminacion.valida_referencia(txtEnvio.Text)
            eliminacion.elimina_modifica(id_envio, Session("id_usuario"), movimiento, mensaje)

            'No existe vaidación en la eliminación
            Label2.Text = mensaje

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

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim eliminacion As New seguimiento_envios
            Dim movimiento As String = "eliminacion"
            Dim mensaje As String = ""
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = eliminacion.valida_referencia(txtEnvio.Text)
            eliminacion.elimina_modifica(id_envio, Session("id_usuario"), movimiento, mensaje)

            'No existe vaidación en la eliminación
            Label2.Text = mensaje
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub
End Class
