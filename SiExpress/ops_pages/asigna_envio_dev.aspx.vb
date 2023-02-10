
Partial Class ops_pages_asigna_envio_dev
    Inherits BasePage

    Protected Sub Asignar_Click(sender As Object, e As System.EventArgs) Handles Asignar.Click
        'Crear dos páginas virtuales para poder asignar los status de asignación y devolución
        Dim pag_devuelve As String = "~/ops_Pages/devolucion_virtual.aspx"
        Dim pag_asigna As String = "~/ops_Pages/asigna_virtual.aspx"


        Dim devolucion As New seguimiento_envios
        'validar si el código proporcionado es refeencia o guía interna
        Dim id_envio As Integer = devolucion.valida_referencia(TxtBox_id_envio.Text)

            If TxtObs.Text <> "" And Not IsNothing(TxtObs.Text) And IsNumeric(id_envio) Then
            Dim mensaje As String = ""
            Dim permitido As Boolean = True
            devolucion.valida_flujo(id_envio, pag_devuelve, mensaje, permitido)
            If permitido Then
                devolucion.insertar_seguimiento(id_envio, pag_devuelve, TxtObs.Text, Session("id_usuario"))
                txtMsg.Text = "Envío " + id_envio.ToString + " El envío está ahora en proces de devolución " + Date.Today.ToString
            Else
                txtMsg.Text = "Envío " + id_envio.ToString + ", " + mensaje
            End If
        Else
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "El número de envío no es válido o falta un comentario"
        End If

        Try


            Button2.Visible = False
            txtMsg.Visible = False
            txtMsg.Text = ""
            Dim asignacion As New seguimiento_envios

            'validar si el código proporcionado es refeencia o guía interna
            id_envio = asignacion.valida_referencia(TxtBox_id_envio.Text)

            Dim mensaje As String = ""
            Dim permitido As Boolean = True
            asignacion.valida_flujo(id_envio, pag_asigna, mensaje, permitido)
            If permitido Then
                Dim mensaje_rec As String
                Dim id_usuario As Integer = Session("id_usuario")
                If IsNothing(id_usuario) Or id_usuario = 0 Then
                    Response.Redirect("~/no_session.aspx")
                End If
                mensaje_rec = asignacion.insertar_asignacion(id_envio, DropDownIdPromo.SelectedValue, id_usuario)
                If mensaje_rec <> "OK" Then
                    Button2.Visible = True
                    txtMsg.Visible = True
                    txtMsg.Text = "El envío " & id_envio.ToString & " ya está asignado al promotor en este día"
                    Exit Sub
                End If
                asignacion.insertar_seguimiento(id_envio, pag_asigna, "", id_usuario, DropDownIdPromo.SelectedItem.ToString)
                GridView1.DataBind()
                TxtBox_id_envio.Text = ""
                TxtBox_id_envio.Focus()
            Else
                Button2.Visible = True
                txtMsg.Visible = True
                txtMsg.Text = "Envío " + id_envio.ToString + ", " + mensaje
            End If

        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub


    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim sJScript2 As String = "<script language=""Javascript"">" & _
" window.open('Reporte_manif_asign.aspx?id_promotor=" & DropDownIdPromo.SelectedValue & "&fecha=" & Format(Date.Today(), "yyyy/MM/dd") & "','','width=800,height=500, toolbar=1, Scrollbars=1')" & _
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript2, False)
    End Sub
End Class
