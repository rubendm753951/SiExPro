
Partial Class admin_pages_admin_mantenimiento_vehiculos
    Inherits BasePage

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            Button2.Visible = False
            txtMsg.Visible = False

            ValidaDatos()

            Dim crear_mtto_vehiculo As New admin_catalogos
            crear_mtto_vehiculo.insertar_mantenimiento_vehiculo(dropDownVehiculo.SelectedValue, CType(txtFechaMtto.Text, Date), DropDownTipoServicio.SelectedValue, CType(txtKilometraje.Text, Integer), CType(txtCostoRefacciones.Text, Decimal), CType(txtCostoManoObra.Text, Decimal))
            GridView1.DataBind()
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try
    End Sub

    Private Sub ValidaDatos()
        Dim fecha As Date
        If Not Date.TryParse(txtFechaMtto.Text, fecha) Then
            Throw New Exception("Fecha invalida")
        End If

        Dim kilometraje As Integer
        If Not Integer.TryParse(txtKilometraje.Text, kilometraje) Then
            Throw New Exception("Kilometraje debe ser numerico.")
        End If

        Dim costoRefacciones As Decimal
        If Not Decimal.TryParse(txtCostoRefacciones.Text, costoRefacciones) Then
            Throw New Exception("Costo refacciones debe ser numerico.")
        End If

        Dim costoManoDeObra As Decimal
        If Not Decimal.TryParse(txtCostoManoObra.Text, costoManoDeObra) Then
            Throw New Exception("Costo mano de obra debe ser numerico.")
        End If
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        txtFechaMtto.Text = Calendar1.SelectedDate.ToShortDateString()
        Calendar1.Visible = False
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub txtFechaCompra_TextChanged(sender As Object, e As EventArgs) Handles txtFechaMtto.TextChanged
        Calendar1.SelectedDate = Convert.ToDateTime(txtFechaMtto.Text)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtMsg.Text = ""
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub
End Class
