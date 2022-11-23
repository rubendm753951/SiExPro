
Partial Class ops_pages_modificacion_envios
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim mensaje As String = String.Empty
        If usuarioId > 0 Then
            If Not String.IsNullOrEmpty(txtEnvio.Text) Then
                Dim numeroEnvio = 0
                Integer.TryParse(txtEnvio.Text, numeroEnvio)

                If numeroEnvio <= 0 Then
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> Envio invalido"
                    ModalPopupExtender3.Show()
                    Exit Sub
                End If

                If Not String.IsNullOrEmpty(txtTotalEnvio.Text) Then
                    Dim totalEnvio As Double = 0
                    Double.TryParse(txtTotalEnvio.Text, totalEnvio)

                    If totalEnvio <= 0 Then
                        Label2.Text = "Ocurrió un error, por favor revise los datos ---> Total del envio debe ser mayor a cero"
                        ModalPopupExtender3.Show()
                        Exit Sub
                    End If

                    Dim respuestaTotalEnvio = DaspackDALC.ModificacionTotalEnvio(numeroEnvio, totalEnvio, txtComentarios.Text, usuarioId)
                    If respuestaTotalEnvio = True Then
                        mensaje = "Total Envio."
                    Else
                        mensaje = "Total envio no pudo ser actualizado."
                    End If
                End If

                If Not String.IsNullOrEmpty(txtReferencia.Text) Then
                    Dim respuestaReferenciaFedex = DaspackDALC.ModificacionReferenciaFedex(numeroEnvio, txtReferencia.Text, txtComentarios.Text, usuarioId)
                    If respuestaReferenciaFedex = True Then
                        mensaje = mensaje + " Referencia Fedex."
                    Else
                        mensaje = mensaje + " Referencia no pudo ser actualizada."
                    End If
                End If

                Dim respuesta = DaspackDALC.ModificacionEnvioProveedor(numeroEnvio, txtComentarios.Text, DropDownProveedores.SelectedValue, usuarioId)
                If respuesta = True Then
                    mensaje = mensaje + " Proveedor Envio actualizado."
                    txtComentarios.Text = ""
                    txtEnvio.Text = ""
                    txtTotalEnvio.Text = ""
                    txtReferencia.Text = ""
                Else
                    mensaje = mensaje + " Proveedor envio no pudo ser actualizado."
                End If
            End If
        Else
            mensaje = "Usuario Invalido"
        End If
        Label2.Text = mensaje
        ModalPopupExtender3.Show()
        Exit Sub
    End Sub
End Class
