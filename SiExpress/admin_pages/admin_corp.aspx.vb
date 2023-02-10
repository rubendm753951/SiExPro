
Partial Class admin_corp
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_InsertCorp"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@nombre"
        parm1.Value = TxtBoxNombre.Text
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@direccion"
        parm2.Value = TxtBoxDir.Text
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@ciudad"
        parm3.Value = TxtBoxCD.Text
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@estado_provincia"
        parm4.Value = TxtBoxEdoProv.Text
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@telefono"
        parm5.Value = TxtBoxTel.Text
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@NIT"
        parm6.Value = TxtBoxNIT.Text
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@id_pais"
        parm7.Value = DropDownPais.SelectedValue
        cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@id_moneda"
        parm8.Value = DropDownMoneda.SelectedValue
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@limite_credito"
        parm9.Value = TxtBoxLimCred.Text
        cmd.Parameters.Add(parm9)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
        GridView1.DataBind()


    End Sub

    Protected Sub sqlDataSource1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SelectCorp.Updating
        For x As Integer = 0 To e.Command.Parameters.Count - 1
            Trace.Write(e.Command.Parameters(x).ParameterName)
            Trace.Write(e.Command.Parameters(x).Value)
        Next

    End Sub

End Class
