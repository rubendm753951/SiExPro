
Partial Class ops_pages_asigna_envio
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SqlDataSource1.DataBind()
        SqlDataSource2.DataBind()
        GridView1.DataBind()
    End Sub
    Protected Sub Asignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Asignar.Click
        Try

            Button2.Visible = False
            txtMsg.Visible = False
            txtMsg.Text = ""
            Dim asignacion As New seguimiento_envios

            'validar si el código proporcionado es refeencia o guía interna
            Dim id_envio As Integer = asignacion.valida_referencia(TxtBox_id_envio.Text)

            Dim mensaje As String = ""
            Dim permitido As Boolean = True
            asignacion.valida_flujo(id_envio, Me.AppRelativeVirtualPath.ToString, mensaje, permitido)
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
                asignacion.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, "", id_usuario, DropDownIdPromo.SelectedItem.ToString)
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
    Protected Sub DropDownIdPromo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownIdPromo.SelectedIndexChanged
        GridView1.DataBind()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub

    Protected Sub TxtBox_id_envio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtBox_id_envio.TextChanged
        'RaiseEvent Asignar_Click()
        'MsgBox("Hola")
        'Dim a As New ops_pages_asigna_envio
        'a.Asignar_Click(sender, e)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim sJScript2 As String = "<script language=""Javascript"">" & _
" window.open('Reporte_manif_asign.aspx?id_promotor=" & DropDownIdPromo.SelectedValue & "&fecha=" & Format(Date.Today(), "yyyy/MM/dd") & "','','width=800,height=500, toolbar=1, Scrollbars=1')" & _
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript2, False)

    End Sub
End Class
