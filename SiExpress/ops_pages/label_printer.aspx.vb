
Partial Class ops_pages_label_printer
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If DropDownAgente.SelectedValue = 0 Then
                TxtMsg.Text = "Seleccione un agente"
            Else
                TxtMsg.Text = ""
                If TextBox1.Text > "" And IsNumeric(TextBox1.Text) And TextBox2.Text > "" And IsNumeric(TextBox2.Text) Then

                    Dim minEnvio = TextBox1.Text
                    Dim maxEnvio = TextBox2.Text

                    If (Math.Abs(maxEnvio - minEnvio) > 5) Then
                        TxtMsg.Text = "No se aceptan mas de 5 Guias multiples"
                    Else
                        Dim minMaxEnvio = DaspackDALC.GetMinMaxEnvioMult(TextBox1.Text)

                        If minMaxEnvio IsNot Nothing And minMaxEnvio.Count > 0 Then
                            minEnvio = minMaxEnvio(0).id_envio
                        End If

                        minMaxEnvio = DaspackDALC.GetMinMaxEnvioMult(TextBox2.Text)

                        If minMaxEnvio IsNot Nothing And minMaxEnvio.Count > 0 Then
                            maxEnvio = minMaxEnvio(1).id_envio
                        End If


                        Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('guia_mult.aspx?id_envio1=" & minEnvio & "&id_envio2=" & maxEnvio & "&id_agente=" & DropDownAgente.SelectedValue & "','_blank','width=600,height=800, toolbar=1, scrollbars=1');" &
                        " window.open('EstafetaLabelMultiple.aspx?id_envio1=" & minEnvio & "&id_envio2=" & maxEnvio & "&id_agente=" & DropDownAgente.SelectedValue & "','_blank','width=600,height=800, toolbar=1, scrollbars=1');" &
                        "</script>"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
                    End If
                Else
                    TxtMsg.Text = "Sólo se aceptan valores numéricos"
                End If
            End If
        Catch ex As Exception
            TxtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub

    Protected Sub DropDownAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgente.SelectedIndexChanged
    End Sub
End Class
