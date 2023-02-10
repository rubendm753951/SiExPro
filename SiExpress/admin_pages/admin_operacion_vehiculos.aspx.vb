
Partial Class admin_pages_admi_operacion_vehiculos
    Inherits BasePage
    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            Button2.Visible = False
            txtMsg.Visible = False

            ValidaDatos()

            Dim crear_operacion_vehiculo As New admin_catalogos
            crear_operacion_vehiculo.insertar_operacion_vehiculos(dropDownVehiculo.SelectedValue, dropDownOficina.SelectedValue, dropDownPromotor.SelectedValue, CType(txtKmIncial.Text, Integer), CType(txtKmFinal.Text, Integer), CType(txtCostoCombustible.Text, Decimal), CType(txtCantidadLts.Text, Integer), CType(txtFecha.Text, Date))
            GridView1.DataBind()
        Catch ex As Exception
            Button2.Visible = True
            txtMsg.Visible = True
            txtMsg.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
        End Try
    End Sub

    Private Sub ValidaDatos()
        Dim fecha As Date
        If Not Date.TryParse(txtFecha.Text, fecha) Then
            Throw New Exception("Fecha invalida")
        End If

        Dim integerValue As Integer
        If Not Integer.TryParse(txtKmIncial.Text, integerValue) Then
            Throw New Exception("Kilometraje inicial debe ser numerico.")
        End If

        If Not Integer.TryParse(txtKmFinal.Text, integerValue) Then
            Throw New Exception("Kilometraje final debe ser numerico.")
        End If

        If Not Integer.TryParse(txtCantidadLts.Text, integerValue) Then
            Throw New Exception("Cantidad Lts debe ser numerico.")
        End If

        Dim decimalValue As Integer
        If Not Decimal.TryParse(txtCostoCombustible.Text, decimalValue) Then
            Throw New Exception("Costo combustible debe ser numerico.")
        End If
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        txtFecha.Text = Calendar1.SelectedDate.ToShortDateString()
        Calendar1.Visible = False
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub txtFechaCompra_TextChanged(sender As Object, e As EventArgs) Handles txtFecha.TextChanged
        Calendar1.SelectedDate = Convert.ToDateTime(txtFecha.Text)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtMsg.Text = ""
        Button2.Visible = False
        txtMsg.Visible = False
    End Sub
End Class
