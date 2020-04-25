
Partial Class admin_usuarios
    Inherits BasePage

    Protected Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassword.TextChanged

    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        SqlDataSource3.DataBind()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If (TxtNombre.Text > "" And Not TxtNombre.Text Is Nothing _
                And (Txtusuario.Text > "" And Not Txtusuario.Text Is Nothing) _
                And (TxtPassword.Text > "" And Not TxtPassword.Text Is Nothing)) Then

                Dim MyConnection As ConnectionStringSettings
                MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
                Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
                connection.ConnectionString = MyConnection.ConnectionString

                'ejecuta SP para insertar el nuevo usuario
                Dim cmd As Data.IDbCommand = connection.CreateCommand()
                cmd.CommandType = Data.CommandType.StoredProcedure
                cmd.CommandText = "dbo.sp_insert_usuarios"

                Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
                parm1.ParameterName = "@nombre"
                parm1.Value = TxtNombre.Text
                cmd.Parameters.Add(parm1)

                Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
                parm2.ParameterName = "@login"
                parm2.Value = Txtusuario.Text
                cmd.Parameters.Add(parm2)

                Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
                parm3.ParameterName = "@password"
                parm3.Value = TxtPassword.Text
                cmd.Parameters.Add(parm3)

                Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
                parm4.ParameterName = "@id_oficina"
                parm4.Value = DropDownList3.SelectedValue
                cmd.Parameters.Add(parm4)

                Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
                parm5.ParameterName = "@id_perfil"
                parm5.Value = DropDownList6.SelectedValue
                cmd.Parameters.Add(parm5)

                connection.Open()
                cmd.ExecuteNonQuery()
                connection.Close()
                GridView1.DataBind()
            End If

        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            TxtError.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            TxtError.Visible = True
            Button3.Visible = True
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            'ejecuta SP para insertar el nuevo usuario
            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "sp_insert_agentes_por_usuario"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_usuario"
            parm1.Value = Session("id_usuario_selected") 'GridView1.SelectedIndex
            cmd.Parameters.Add(parm1)

            Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            parm2.ParameterName = "@id_agente"
            parm2.Value = DropDownList1.SelectedValue
            cmd.Parameters.Add(parm2)


            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
            GridView2.DataBind()
        Catch ex As Exception
            TxtError.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            TxtError.Visible = True
            Button3.Visible = True
        End Try

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        Session("usuario_selected") = row.Cells(2).Text
        Session("id_usuario_selected") = row.Cells(1).Text
        TextBox1.Text = "Agentes y privilegios de " & Session("usuario_selected")
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        TxtError.Visible = False
        Button3.Visible = False
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            'ejecuta SP para insertar el nuevo usuario
            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "sp_insert_usuarios_modulos"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_usuario"
            parm1.Value = Session("id_usuario_selected") 'GridView1.SelectedIndex
            cmd.Parameters.Add(parm1)

            Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            parm2.ParameterName = "@id_modulo"
            parm2.Value = DropDownList2.SelectedValue
            cmd.Parameters.Add(parm2)


            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
            GridView3.DataBind()
        Catch ex As Exception
            TxtError.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            TxtError.Visible = True
            Button3.Visible = True
        End Try
    End Sub
End Class
