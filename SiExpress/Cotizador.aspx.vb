
Partial Class Cotizador
    Inherits System.Web.UI.Page

    Protected Sub btnCotizar_Click(sender As Object, e As EventArgs) Handles btnCotizar.Click
        lblMensaje.Text = ""
        txtCosto.Text = "0"

        If Valida() Then
            Dim MyConnection As ConnectionStringSettings
            Dim peso As Double = 0
            Dim pesoVol As Double = 0
            Dim tipo As Integer = CType(IIf(rbPaquete.Checked = True, 1, 2), Integer)

            pesoVol = (CType(txtAlto.Text, Double) * CType(txtAncho.Text, Double) * CType(txtLargo.Text, Double)) / 5000
            peso = CType(txtPeso.Text, Double)

            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_select_precio"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_ciudad"
            parm1.Value = DropDownCiudad.SelectedValue
            cmd.Parameters.Add(parm1)

            Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            parm2.ParameterName = "@peso_vol"
            parm2.Value = IIf(peso > pesoVol, peso, pesoVol)
            cmd.Parameters.Add(parm2)

            Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
            parm3.ParameterName = "@tipo"
            parm3.Value = tipo
            cmd.Parameters.Add(parm3)

            connection.Open()

            Dim costo = cmd.ExecuteScalar()

            If costo IsNot Nothing Then
                txtCosto.Text = costo.ToString()
            Else
                lblMensaje.Text = "El peso excede el limite permito."
            End If
            connection.Close()
        End If
    End Sub

    Private Function Valida() As Boolean
        Dim isValid As Boolean = True
        Dim mensaje As New StringBuilder

        If Not IsNumeric(txtPeso.Text) Then
            mensaje.Append("Peso de la caja debe ser numerico." + Chr(10) + Chr(13))
            isValid = False
        End If

        If Not IsNumeric(txtAlto.Text) Then
            mensaje.Append("Alto de la caja debe ser numerico." + Chr(10) + Chr(13))
            isValid = False
        End If

        If Not IsNumeric(txtLargo.Text) Then
            mensaje.Append("Largo de la caja debe ser numerico." + Chr(10) + Chr(13))
            isValid = False
        End If

        If Not IsNumeric(txtAncho.Text) Then
            mensaje.Append("Alto de la caja debe ser numerico." + Chr(10) + Chr(13))
            isValid = False
        End If

        lblMensaje.Text = mensaje.ToString()
        Return isValid
    End Function

    Protected Sub DropDownOrigen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownOrigen.SelectedIndexChanged
        If DropDownOrigen.SelectedValue <> 52 Then
            rbPaquete.Enabled = True
            rbSobre.Enabled = True
        Else
            rbPaquete.Checked = True
            rbPaquete.Enabled = False
            rbSobre.Enabled = False
        End If

        SqlDSEstados.DataBind()
        DropDownDestino.DataBind()

        SqlDataCiudades.DataBind()
        DropDownCiudad.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If DropDownOrigen.SelectedValue <> 52 Then
            rbPaquete.Enabled = True
            rbSobre.Enabled = True
        Else
            rbPaquete.Checked = True
            rbPaquete.Enabled = False
            rbSobre.Enabled = False
        End If
    End Sub

    Protected Sub btLimpiar_Click(sender As Object, e As EventArgs) Handles btLimpiar.Click
        txtPeso.Text = "0"
        txtAlto.Text = "0"
        txtLargo.Text = "0"
        txtAncho.Text = "0"
        txtCosto.Text = "0"
    End Sub
End Class
