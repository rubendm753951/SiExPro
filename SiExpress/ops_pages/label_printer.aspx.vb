
Partial Class ops_pages_label_printer
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            TxtMsg.Text = ""
            If TextBox1.Text > "" And IsNumeric(TextBox1.Text) And TextBox2.Text > "" And IsNumeric(TextBox2.Text) Then
                Dim sjscript2 As String = "<script language=""javascript"">" & _
            " window.open('guia_mult.aspx?id_envio1=" & TextBox1.Text & "&id_envio2=" & TextBox2.Text & "&id_agente=" & DropDownAgente.SelectedValue & "','','width=600,height=800, toolbar=1, scrollbars=1')" & _
            "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
            Else
                TxtMsg.Text = "Sólo se aceptan valores numéricos"
            End If
        Catch ex As Exception
            TxtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub

    Protected Sub DropDownAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgente.SelectedIndexChanged
    End Sub
End Class
