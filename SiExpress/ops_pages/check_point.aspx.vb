
Partial Class ops_pages_check_point
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim control As New seguimiento_envios
            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = control.valida_referencia(txtEnvio.Text)

            If Not IsNothing(id_envio) And id_envio.ToString <> "" And IsNumeric(id_envio) Then
                Dim mensaje As String = ""
                Dim permitido As Boolean = True
                control.valida_flujo(id_envio, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
                If permitido Then
                    control.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, txtObs.Text, Session("id_usuario"))
                    Label2.Text = "Envío " + id_envio.ToString + " controlado con éxito por " + Session("user_name").ToString
                    txtEnvio.Text = ""
                    txtEnvio.Focus()
                Else
                    Label2.Text = "Envío " + id_envio.ToString + ", " + mensaje
                End If
            Else
                Button2.Visible = True
                txtMsg.Visible = True
                txtMsg.Text = "El número de envío no es válido"
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
