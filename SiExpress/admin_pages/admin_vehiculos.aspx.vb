
Imports System.Data

Partial Class admin_pages_admin_vehiculos
    Inherits BasePage

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            Button2.Visible = False
            txtMsg.Visible = False

            ValidaDatos()

            Dim crear_vehiculo As New admin_catalogos
            crear_vehiculo.insertar_vehiculos(dropDownOficina.SelectedValue, txtPlacas.Text, txtSerie.Text, txtModelo.Text, CType(txtFechaCompra.Text, Date))
            GridView1.DataBind()
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try
    End Sub

    Private Sub ValidaDatos()
        Dim fechaCompra As Date
        If Not Date.TryParse(txtFechaCompra.Text, fechaCompra) Then
            Throw New Exception("Fecha de compra invalida")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtMsg.Text = ""
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        txtFechaCompra.Text = Calendar1.SelectedDate.ToShortDateString()
        Calendar1.Visible = False
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub txtFechaCompra_TextChanged(sender As Object, e As EventArgs) Handles txtFechaCompra.TextChanged
        Calendar1.SelectedDate = Convert.ToDateTime(txtFechaCompra.Text)
    End Sub
End Class
