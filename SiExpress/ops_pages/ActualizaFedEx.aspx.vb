
Partial Class ops_pages_ActualizaFedEx
    Inherits BasePage

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim control As New seguimiento_envios
            'validar si el código proporcionado es refeencia o guía interna
            control.actualiza_FedEx(txtEnvio.Text, txtReferencia.Text)
            Label2.Text = "El envío se actualizó con éxito"
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub
End Class
