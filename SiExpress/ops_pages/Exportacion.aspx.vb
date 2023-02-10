Imports ShipService
Imports System.Globalization
Partial Class ops_pages_Exportacion
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cultures() As CultureInfo = CultureInfo.GetCultures(CultureTypes.NeutralCultures)

        If Today.DayOfWeek = DayOfWeek.Saturday Then
            'txtFecha.Text = DateTime.Now.AddDays(2).ToString("d", CultureInfo.CreateSpecificCulture("en-US"))
            txtFecha.Text = Format(DateTime.Now.AddDays(2), "dd-MMM-yyyy")
        ElseIf Today.DayOfWeek = DayOfWeek.Sunday Then
            'txtFecha.Text = DateTime.Now.AddDays(1).ToString("d", CultureInfo.CreateSpecificCulture("en-US"))
            txtFecha.Text = Format(DateTime.Now.AddDays(1), "dd-MMM-yyyy")
        Else
            'txtFecha.Text = DateTime.Now.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))
            txtFecha.Text = Format(DateTime.Now, "dd-MMM-yyyy")
        End If

    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DropDownList1.SelectedValue = 10 Then  'FedEx Provider
            Dim envio_fedex As Integer
            envio_fedex = DropDownShipType.SelectedValue

            Dim envio_fedex_pkg As Integer
            envio_fedex_pkg = DropDownPkg.SelectedValue

            'If DropDownList1.SelectedIndex = 0 Then
            'Genera Guía FedEX en otra ventana y desactiva botón
            Dim impresion As String = ""

            If cbLasser.Checked = True Then
                impresion = "lasser"
                Dim sJScript3 As String = "<script language=""Javascript"">" & _
                " window.open('guia_fedex.aspx?id_envio=" & TextBox2.Text & "&envio_fedex=" & envio_fedex & "&envio_fedex_pkg=" & envio_fedex_pkg & "&impresion=" & impresion & "','','width=800,height=500, toolbar=1, Scrollbars=1')" & _
                "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)
            Else
                impresion = "termica"
                Dim sJScript3 As String = "<script language=""Javascript"">" & _
                " window.open('guia_fedex.aspx?id_envio=" & TextBox2.Text & "&envio_fedex=" & envio_fedex & "&envio_fedex_pkg=" & envio_fedex_pkg & "&impresion=" & impresion & "','','width=384,height=576, toolbar=1, Scrollbars=1')" & _
                "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)
            End If

            'Response.Write(sJScript2)
            'ElseIf DropDownList1.SelectedIndex = 1 Then
            'Dim USPS_tools As New USPS_WebTools
            'Dim Bytes As Byte()
            'Bytes = USPS_tools.DeliveryConfirmationLabel("X")

            'Response.ContentType = "image/GIF" '"Application/pdf"
            'Response.BinaryWrite(Bytes)
            'Response.End()


            ' Dim l As String = TextBox3.Text
            'Dim sb As New System.Text.StringBuilder
            'For i As Integer = 0 To l.Length - 1
            '    If l.Length > 0 Then sb.Append(l(i).Trim)
            'Next
            'Dim b() As Byte = Convert.FromBase64String(l)
            'Dim ms As New System.IO.MemoryStream()
            'ms.Write(b, 0, b.Length)
            'End If
            TextBox2.DataBind()
        ElseIf DropDownList1.SelectedValue = 20 Then  'USPS Provider

            Dim api As String
            If cbSignature.Checked = True Then
                api = "SignatureConfirmation"
            Else
                api = "DeliveryConfirmation"
            End If

            Dim sJScript3 As String = "<script language=""Javascript"">" & _
            " window.open('guia_usps.aspx?id_envio=" & TextBox2.Text & "&api=" & api & "','','width=660,height=880, toolbar=1, Scrollbars=1')" & _
            "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)
            End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim USPS_tools As New USPS_WebTools
        respuesta.Text = USPS_tools.AddressValidateRequest(address1.Text, address2.Text, city.Text, state.Text, zip5.Text, zip4.Text)

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim envio As New ObjEnvio
        Dim cliente As New ObjCliente
        Dim destinatario As New ObjDestinatario

        'Dim envio_fedex As New ServiceType  'RateRequest.RequestedShipment
        'Select Case DropDownShipType.SelectedValue
        '    Case 0
        '        envio_fedex = ServiceType.INTERNATIONAL_ECONOMY
        '    Case 1
        '        envio_fedex = ServiceType.INTERNATIONAL_PRIORITY
        '    Case 2
        '        envio_fedex = ServiceType.INTERNATIONAL_FIRST
        'End Select

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Datos_Envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = TextBox2.Text
        cmd.Parameters.Add(parm1)

        connection.Open()
        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        If reader.HasRows Then
            'Dim datos1 As String, datos2 As String, datos3 As String
            reader.Read()
            'Datos del agente
            'datos1 = "Nombre del Agente: " & reader.GetString(3) & vbCrLf
            'datos1 = datos1 & "Ciuad del Agente: " & reader.GetString(6) & vbCrLf
            'datos1 = datos1 & "País del Agente: " & reader.GetString(4) & vbCrLf
            'datos1 = datos1 & "Dirección del Agente: " & reader.GetString(5) & vbCrLf
            'datos1 = datos1 & "Código de Agente: " & reader.GetInt32(1).ToString & vbCrLf
            'datos1 = datos1 & "Límite de Crédito del Agente: " & Format(reader.GetValue(8), "#.##").ToString & vbCrLf
            'datos1 = datos1 & "--------------------------------------------------" & vbCrLf

            'datos2 = "Envío Número: " & reader.GetInt32(0) & vbCrLf
            'datos2 = datos2 & "SubProducto: " & reader.GetString(9) & vbCrLf
            'datos2 = datos2 & "Tarifa: " & Format(reader.GetValue(10), "#.##").ToString & vbCrLf
            envio.valor_seguro = reader.GetValue(11)
            'datos2 = datos2 & "Descuento: " & Format(reader.GetValue(12), "#.##").ToString & vbCrLf
            'datos2 = datos2 & "Total: " & Format(reader.GetValue(13), "#.##").ToString & vbCrLf
            'datos2 = datos2 & "Moneda: " & reader.GetValue(7) & vbCrLf
            'datos2 = datos2 & "Comisión por envío: " & Format(reader.GetValue(14), "#.##").ToString & vbCrLf
            'datos2 = datos2 & "Comisión Porcentaje: " & Format(reader.GetValue(15), "#.##").ToString & vbCrLf
            'datos2 = datos2 & "Zona (Ruta Destino): " & reader.GetString(16) & vbCrLf
            'datos2 = datos2 & "Tipo de Tarifa: " & reader.GetValue(17).ToString & vbCrLf
            envio.largo = reader.GetValue(32)
            envio.ancho = reader.GetValue(33)
            envio.alto = reader.GetValue(34)
            envio.peso = reader.GetValue(35)
            'datos2 = datos2 & "Peso Volumetrico: " & reader.GetValue(36) & vbCrLf
            envio.referencia = reader.GetString(37)
            'datos2 = datos2 & "--------------------------------------------------" & vbCrLf
            envio.fecha_rec = CDate(txtFecha.Text)

            'Remitente y Destinatario
            'datos3 = "Remitente: " & reader.GetString(18) & vbCrLf
            'datos3 = datos3 & "Empresa: " & reader.GetString(19) & vbCrLf
            'datos3 = datos3 & "Dirección: " & reader.GetString(21) & vbCrLf
            'datos3 = datos3 & "Teléfono: " & reader.GetString(20) & vbCrLf
            cliente.direccion = reader.GetString(19)
            cliente.ciudad = reader.GetString(22)
            cliente.estadoprovincia = reader.GetString(23)
            cliente.codigo_pais = reader.GetString(24)
            cliente.codigo_postal = reader.GetString(38)
            'datos3 = datos3 & "--------------------------------------------------" & vbCrLf
            'datos3 = datos3 & "Destinatario: " & reader.GetString(25) & vbCrLf
            'datos3 = datos3 & "Empresa: " & reader.GetString(26) & vbCrLf
            destinatario.direccion = reader.GetString(28)
            'datos3 = datos3 & "Teléfono: " & reader.GetString(27) & vbCrLf
            destinatario.ciudad = reader.GetString(29)
            destinatario.estadoprovincia = reader.GetString(30)
            destinatario.codigo_pais = reader.GetString(31)
            destinatario.codigo_postal = reader.GetString(39)
            'TextBox3.Text = datos1 & datos2 & datos3
            'datos3 = datos3 & "--------------------------------------------------" & vbCrLf
        Else
            TextBox3.Text = "No existen datos para el envío " & TextBox2.Text

        End If
        connection.Close()

        envio.FedExTipo = DropDownShipType.SelectedValue
        envio.FedExPkg = DropDownPkg.SelectedValue

        Dim FedExRate As New FedEx_RateRequest
        TextBox3.Text = FedExRate.Main(envio, cliente, destinatario)

    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Datos_Envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = TextBox2.Text
        cmd.Parameters.Add(parm1)

        connection.Open()
        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        If reader.HasRows Then
            Dim datos1 As String, datos2 As String, datos3 As String
            reader.Read()
            'Datos del agente
            datos1 = "Nombre del Agente: " & reader.GetString(3) & vbCrLf
            datos1 = datos1 & "Ciuad del Agente: " & reader.GetString(6) & vbCrLf
            datos1 = datos1 & "País del Agente: " & reader.GetString(4) & vbCrLf
            datos1 = datos1 & "Dirección del Agente: " & reader.GetString(5) & vbCrLf
            datos1 = datos1 & "Código de Agente: " & reader.GetInt32(1).ToString & vbCrLf
            datos1 = datos1 & "Límite de Crédito del Agente: " & Format(reader.GetValue(8), "#.##").ToString & vbCrLf
            datos1 = datos1 & "--------------------------------------------------" & vbCrLf

            datos2 = "Envío Número: " & reader.GetInt32(0) & vbCrLf
            datos2 = datos2 & "SubProducto: " & reader.GetString(9) & vbCrLf
            datos2 = datos2 & "Tarifa: " & Format(reader.GetValue(10), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Seguro: " & Format(reader.GetValue(11), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Descuento: " & Format(reader.GetValue(12), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Valor Aduana: " & Format(reader.GetValue(41), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Total: " & Format(reader.GetValue(13), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Moneda: " & reader.GetValue(7) & vbCrLf
            datos2 = datos2 & "Comisión por envío: " & Format(reader.GetValue(14), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Comisión Porcentaje: " & Format(reader.GetValue(15), "#.##").ToString & vbCrLf
            datos2 = datos2 & "Zona (Ruta Destino): " & reader.GetString(16) & vbCrLf
            datos2 = datos2 & "Tipo de Tarifa: " & reader.GetValue(17).ToString & vbCrLf
            datos2 = datos2 & "Largo: " & reader.GetValue(32).ToString & vbCrLf
            datos2 = datos2 & "Ancho: " & reader.GetValue(33).ToString & vbCrLf
            datos2 = datos2 & "Alto: " & reader.GetValue(34).ToString & vbCrLf
            datos2 = datos2 & "Peso: " & reader.GetValue(35) & vbCrLf
            datos2 = datos2 & "Peso Volumetrico: " & reader.GetValue(36) & vbCrLf
            datos2 = datos2 & "Referencia1: " & reader.GetString(37) & vbCrLf
            datos2 = datos2 & "--------------------------------------------------" & vbCrLf


            'Remitente y Destinatario
            datos3 = "Remitente: " & reader.GetString(18) & vbCrLf
            datos3 = datos3 & "Empresa: " & reader.GetString(19) & vbCrLf
            datos3 = datos3 & "Dirección: " & reader.GetString(21) & vbCrLf
            datos3 = datos3 & "Teléfono: " & reader.GetString(20) & vbCrLf
            datos3 = datos3 & "Dirección: " & reader.GetString(19) & vbCrLf
            datos3 = datos3 & "Ciudad: " & reader.GetString(22) & vbCrLf
            datos3 = datos3 & "Estado: " & reader.GetString(23) & vbCrLf
            datos3 = datos3 & "País: " & reader.GetString(24) & vbCrLf
            datos3 = datos3 & "CP: " & reader.GetString(38) & vbCrLf
            datos3 = datos3 & "--------------------------------------------------" & vbCrLf
            datos3 = datos3 & "Destinatario: " & reader.GetString(25) & vbCrLf
            datos3 = datos3 & "Empresa: " & reader.GetString(26) & vbCrLf
            datos3 = datos3 & "Dirección: " & reader.GetString(28) & vbCrLf
            datos3 = datos3 & "Teléfono: " & reader.GetString(27) & vbCrLf
            datos3 = datos3 & "Ciudad: " & reader.GetString(29) & vbCrLf
            datos3 = datos3 & "Estado: " & reader.GetString(30) & vbCrLf
            datos3 = datos3 & "País: " & reader.GetString(31) & vbCrLf
            datos3 = datos3 & "CP: " & reader.GetString(39) & vbCrLf
            TextBox3.Text = datos1 & datos2 & datos3
            datos3 = datos3 & "--------------------------------------------------" & vbCrLf
        Else
            TextBox3.Text = "No existen datos para el envío " & TextBox2.Text

        End If
        connection.Close()
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Datos_Envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = TextBox2.Text
        cmd.Parameters.Add(parm1)
        connection.Open()

        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        Dim ReferenciaFedEx As String
        If reader.HasRows Then
            reader.Read()
            ReferenciaFedEx = reader.GetString(42)
            connection.Close()
            If ReferenciaFedEx <> "" And IsNothing(ReferenciaFedEx) = False Then
                Dim BorrarEnvio As New FedEx_DeleteShipment
                TextBox3.Text = BorrarEnvio.Main(ReferenciaFedEx)
            End If
        Else
            TextBox3.Text = "No existen datos de exportación del envío " & TextBox4.Text
        End If
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click

        Dim sJScript3 As String = "<script language=""Javascript"">" & _
        " window.open('guia_usps.aspx','','width=660,height=880, toolbar=1, Scrollbars=1')" & _
        "</script>"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)

    End Sub

    Protected Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim usps_tool As New USPS_WebTools
        respuesta.Text = usps_tool.USPS_tracking("") '  USPS_tools.AddressValidateRequest(address1.Text, address2.Text, city.Text, state.Text, zip5.Text, zip4.Text)

        Dim usps_tool_live As New USPS_WebTools
        respuesta0.Text = usps_tool.USPS_tracking_live("") '  USPS_tools.AddressValidateRequest(address1.Text, address2.Text, city.Text, state.Text, zip5.Text, zip4.Text)

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = 20 Then 'USPS
            'TextBox2.Enabled = False
            TextBox4.Enabled = False
            Button3.Enabled = False
            'Button4.Enabled = False
            Button5.Enabled = False
            DropDownPkg.Enabled = False
            cbTermica.Enabled = False
            cbLasser.Enabled = False
            'DropDownShipType.Enabled = False
        Else  'FedEx
            TextBox2.Enabled = True
            TextBox4.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            DropDownPkg.Enabled = True
            DropDownShipType.Enabled = True
            cbTermica.Enabled = True
            cbLasser.Enabled = True
        End If
        'MsgBox(DropDownList1.SelectedValue)
        TextBox2.DataBind()
        DropDownShipType.DataBind()
    End Sub
End Class
