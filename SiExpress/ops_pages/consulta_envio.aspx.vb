Imports System.Xml.Serialization
Imports System.IO
Imports System.Data
Imports System.Activities.Expressions
Imports FedEx_TrackService
Partial Class ops_pages_consulta_envio
    Inherits BasePage

    Private Shared _idModulo As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString.Count > 0 Then
            Dim idEnvio As String = Request.QueryString(0)
            If idEnvio IsNot Nothing AndAlso idEnvio <> "" Then
                TextBox1.Text = idEnvio
                Button1_Click(Me, EventArgs.Empty)
            End If
        End If

        '****************************************************************
        Dim modulo As Modulo = DaspackDALC.GetModuloPorDescripcion(Me.AppRelativeVirtualPath.ToString)
        If modulo IsNot Nothing Then
            _idModulo = modulo.IdModulo
        End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        
        Dim control As New seguimiento_envios
        'validar si el código proporcionado es refeencia o guía interna
        Dim id_envio As Integer = control.valida_referencia(TextBox1.Text)


        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Datos_Envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_usuario"
        parm2.Value = usuarioId
        cmd.Parameters.Add(parm2)

        connection.Open()

        Dim tracking_number As String = "0"
        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        If reader.HasRows Then
            'Button2.Enabled = True
            Dim datos2 As String
            reader.Read()
            'Datos del agente
            'datos1 = "Nombre del Agente: " & reader.GetString(3) & vbCrLf
            'datos1 = datos1 & "Ciuad del Agente: " & reader.GetString(6) & vbCrLf
            'datos1 = datos1 & "País del Agente: " & reader.GetString(4) & vbCrLf
            'datos1 = datos1 & "Dirección del Agente: " & reader.GetString(5) & vbCrLf
            'datos1 = datos1 & "Código de Agente: " & reader.GetInt32(1).ToString & vbCrLf
            'datos1 = datos1 & "Límite de Crédito del Agente: " & Format(reader.GetValue(8), "#.##").ToString & vbCrLf
            tracking_number = reader.GetString(42).Trim()
            txtIdAgente.Text = reader.GetInt32(1).ToString

            datos2 = "Envío Número: " & reader.GetInt32(0) & vbCrLf
            datos2 = datos2 & "SubProducto: " & reader.GetString(9) & vbCrLf
            datos2 = datos2 & "Tarifa: " & Format(reader.GetValue(10), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Seguro: " & Format(reader.GetValue(11), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Descuento: " & Format(reader.GetValue(12), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Total: " & Format(reader.GetValue(13), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Moneda: " & reader.GetValue(7) & vbCrLf
            datos2 = datos2 & "Comisión por envío: " & Format(reader.GetValue(14), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Comisión Porcentaje: " & Format(reader.GetValue(15), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Valor declarado: " & Format(reader.GetValue(41), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Zona (Ruta Destino): " & reader.GetString(16) & vbCrLf
            datos2 = datos2 & "Tipo de Tarifa: " & reader.GetValue(17).ToString & vbCrLf
            datos2 = datos2 & "Largo: " & reader.GetValue(32).ToString & vbCrLf
            datos2 = datos2 & "Ancho: " & reader.GetValue(33).ToString & vbCrLf
            datos2 = datos2 & "Alto: " & reader.GetValue(34).ToString & vbCrLf
            datos2 = datos2 & "Peso: " & reader.GetValue(35) & vbCrLf
            datos2 = datos2 & "Peso Volumetrico: " & reader.GetValue(36) & vbCrLf
            datos2 = datos2 & "Contenedor: " & reader.GetValue(44) & vbCrLf
            datos2 = datos2 & "Inventario: " & reader.GetString(37)
            datos2 = datos2 & "Referencia Exportación: " & reader.GetString(42)
            TextBox2.Text = datos2

            'TextBox3.Text = datos2
            ''Remitente y Destinatario
            'datos3 = "Remitente: " & reader.GetString(18) & vbCrLf
            'datos3 = datos3 & "Empresa: " & reader.GetString(19) & vbCrLf
            'datos3 = datos3 & "Dirección: " & reader.GetString(21) & vbCrLf
            'datos3 = datos3 & "Teléfono: " & reader.GetString(20) & vbCrLf
            'datos3 = datos3 & "Dirección: " & reader.GetString(19) & vbCrLf
            'datos3 = datos3 & "Ciudad: " & reader.GetString(22) & vbCrLf
            'datos3 = datos3 & "Estado: " & reader.GetString(23) & vbCrLf
            'datos3 = datos3 & "Zip Code: " & reader.GetString(37) & vbCrLf
            'datos3 = datos3 & "País: " & reader.GetString(24) & vbCrLf
            'datos3 = datos3 & "Destinatario: " & reader.GetString(25) & vbCrLf
            'datos3 = datos3 & "Empresa: " & reader.GetString(26) & vbCrLf
            'datos3 = datos3 & "Dirección: " & reader.GetString(28) & vbCrLf
            'datos3 = datos3 & "Teléfono: " & reader.GetString(27) & vbCrLf
            'datos3 = datos3 & "Ciudad: " & reader.GetString(29) & vbCrLf
            'datos3 = datos3 & "Estado: " & reader.GetString(30) & vbCrLf
            'datos3 = datos3 & "Zip Code: " & reader.GetString(38) & vbCrLf
            'datos3 = datos3 & "País: " & reader.GetString(31)
            'TextBox4.Text = datos3
            TextBox1.Text = id_envio.ToString
            GridView1.DataBind()
        Else
            TextBox2.Text = "No existen datos para el envío " & TextBox1.Text
        End If
        connection.Close()


        Dim estafetaWrapper As New EstafetaWrapper()
        Dim estafetaTracking = estafetaWrapper.Tracking(tracking_number)

        If estafetaTracking IsNot Nothing Then
            'GridView2.DataSourceID = Nothing
            GridView2.DataSource = estafetaTracking
            GridView2.DataBind()
        End If

        TextBox1.Focus()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text > "" And IsNumeric(TextBox1.Text) Then

            Dim sjscript2 As String = "<script language=""javascript"">" & _
            " window.open('guia_mult.aspx?id_envio1=" & TextBox1.Text & "&id_envio2=" & TextBox1.Text & "&id_agente=" & txtIdAgente.Text & "','','width=600,height=800, toolbar=1, scrollbars=1')" & _
            "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
        End If
        Button2.Enabled = False
        CheckBox1.Checked = False
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
    End Sub
    Protected Sub GridView3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.SelectedIndexChanged
    End Sub
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Protected Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim control As New seguimiento_envios
        Dim id_envio As Integer = control.valida_referencia(TextBox1.Text)

        If (DaspackDALC.GetModuloPrivilegio(_idModulo, usuarioId, TipoPrivilegio.Escribe) = True) Then
            DaspackDALC.InsComenatrio(id_envio, usuarioId, txtComentario.Text)
            GridView3.DataBind()
            txtComentario.Text = ""
        Else
            TextBox2.Text = "El usuario no tiene permisos para insertar comenatrios."
        End If
    End Sub

    Protected Sub GridView3_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView3.RowEditing
        Dim row As GridViewRow = GridView3.Rows(e.NewEditIndex)
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        'Dim idUsrCom As Integer = 0
        Dim idUsrRes As Integer = 0
        If (DaspackDALC.GetModuloPrivilegio(_idModulo, usuarioId, TipoPrivilegio.Escribe) = True) Then
            'If row.Cells(4).Text <> "" Then
            '    idUsrCom = CType(row.Cells(4).Text, Integer)
            'End If

            If row.Cells(6).Text <> "" Then
                idUsrRes = CType(row.Cells(6).Text, Integer)
            End If

            'Dim comentario As BoundField = CType(GridView3.Columns(5), BoundField)
            'If idUsrCom <> 0 And idUsrCom <> usuarioId Then
            '    row.Cells(5).Text = ""
            '    comentario.ReadOnly = True
            'End If

            Dim respuesta As BoundField = CType(GridView3.Columns(7), BoundField)
            If idUsrRes <> 0 And idUsrRes <> usuarioId Then
                row.Cells(7).Text = ""
                respuesta.ReadOnly = True
            End If
        Else
            GridView3.EditIndex = -1
            e.Cancel = True
        End If

    End Sub

    Protected Sub GridView3_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        If (DaspackDALC.GetModuloPrivilegio(_idModulo, usuarioId, TipoPrivilegio.Borra) = False) Then
            e.Cancel = True
        End If
    End Sub
End Class
