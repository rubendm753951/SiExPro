
Partial Class admin_pages_admin_promotores
    Inherits BasePage
    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Button2.Visible = False
        txtMsg.Visible = False

        GridView1.DataBind()
    End Sub
    Protected Sub BtnInsertPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnInsertPromo.Click
        Try
            Button2.Visible = False
            txtMsg.Visible = False

            Dim crear_promotor As New admin_catalogos
            crear_promotor.insertar_promotores(TextBox1.Text, DropDownList1.SelectedValue)
            GridView1.DataBind()
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try

    End Sub

End Class
