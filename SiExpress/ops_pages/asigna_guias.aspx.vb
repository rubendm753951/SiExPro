
Partial Class ops_pages_asigna_guias
    Inherits BasePage

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged

        Label7.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label10.Text = ""
        Label11.Text = ""
        Label12.Text = ""

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Asignaciones_no_usadas"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_agencia"
        parm1.Value = DropDownList1.SelectedValue
        cmd.Parameters.Add(parm1)

        connection.Open()
        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        If reader.HasRows Then
            'Dim guia_inicial_usada As Integer, guia_final_usada As Integer, total_guias_usadas As Integer
            'Dim guia_inicial_libre As Integer, guia_final_libre As Integer, total_guias_libre As Integer
            reader.Read()
            Label7.Text = reader.GetInt32(4)
            Label8.Text = reader.GetInt32(5)
            Label9.Text = reader.GetInt32(6)
            Label10.Text = reader.GetInt32(1)
            Label11.Text = reader.GetInt32(2)
            Label12.Text = reader.GetInt32(3)
        End If
        connection.Close()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mensaje_rec As Integer
        Dim asignacion As New Insertar_Envios
        mensaje_rec = asignacion.preasignar_envios(DropDownList1.SelectedValue, TextBox1.Text, TextBox2.Text)
        Label13.Text = mensaje_rec
    End Sub
End Class
