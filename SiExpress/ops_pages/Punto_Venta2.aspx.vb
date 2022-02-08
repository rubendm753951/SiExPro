Imports System.Data
Imports System.Web.Services
Imports System.Data.OleDb
Imports System.Data.Entity
Imports SiExProData
Imports System.Security.Cryptography

Partial Class Punto_Venta
    Inherits BasePage
    Public Shared Paso As String = ""
    Private Shared _idModulo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim modulo As Modulo = DaspackDALC.GetModuloPorDescripcion(Me.AppRelativeVirtualPath.ToString)

        If modulo IsNot Nothing Then
            _idModulo = modulo.IdModulo
        End If

        If DropDownColonia.Items.Count > 0 Then
            TxtCol2.Visible = False
            DropDownColonia.Visible = True
        Else
            TxtCol2.Visible = True
            DropDownColonia.Visible = False
        End If

        If Not IsPostBack Then
            contenidosDesc.Visible = False
            contenidosCampos.Visible = False
            contenidosGrid.Visible = False
            tipopaquete.Visible = False
        End If

        '        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('input[id$=btnCheckOut]').click(function () { $('input[id$=btnAceptar]').removeAttr('disabled'); });  $('input[id$=btnAceptar]').click(function () { $('input[id$=btnAceptar]').attr('disabled', 'disabled'); }); });", True)

        '****************************************************************

    End Sub

    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        Me.ModalPopupExtender2.Show()
    End Sub

    Protected Sub TxtCP2_TextChanged(sender As Object, e As EventArgs)
        Dim cp = DirectCast(sender, TextBox).Text
        GetColonias(cp)
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If TxtCP2.Text <> "" And TxtCP2.Text.Length >= 5 Then
            GetColonias(TxtCP2.Text)
        End If
    End Sub

    Private Sub DropDownColonia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownColonia.SelectedIndexChanged

    End Sub

    Private Sub GetColonias(cp As String)
        Dim sepomexResult = DaspackDALC.GetSearchZipCode(cp)
        If sepomexResult IsNot Nothing And sepomexResult.Count > 0 Then
            TxtCol2.Visible = False
            DropDownColonia.Visible = True
            DropDownColonia.DataSource = sepomexResult
            DropDownColonia.DataBind()

            'DropDownPais2.SelectedValue = 52
            txtCiudad2.Visible = True
            'txtEdo2.DataBind()
            txtEdo2.SelectedValue = sepomexResult(0).estado_codigo
            txtCiudad2.Text = sepomexResult(0).d_ciudad
            TxtMpio2.Text = sepomexResult(0).D_mnpio
        Else
            TxtCol2.Visible = True
            DropDownColonia.Visible = False
        End If
    End Sub

    Protected Sub DropDownProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownProduct.SelectedIndexChanged
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_SelectTarifas_por_Agente"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_agencia"
            parm1.Value = DropDownAgentes.SelectedValue
            cmd.Parameters.Add(parm1)

            'Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            'parm2.ParameterName = "@id_tipo"
            'parm2.Value = 1
            'cmd.Parameters.Add(parm2)

            'Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
            'parm3.ParameterName = "@activado"
            'parm3.Value = True
            'cmd.Parameters.Add(parm3)

            connection.Open()
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                Do While reader.GetInt32(0) <> DropDownProduct.SelectedValue
                    reader.Read()
                Loop
                TxtTarifa.value = Format(reader.GetValue(6), "$#,##0.00;($#,##0.00);$0.00")
                Session("dimension_peso") = reader.GetString(12)
                lblPeso.Text = "Peso (" & Session("dimension_peso") & ")"
            End If
            connection.Close()
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub
    Protected Sub btnCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOut.Click
        Try
            Dim Mensaje As String = "" 'para devolver resultado de validación de mensajes
            Dim Datos_Dest As New ObjDestinatario
            Dim datos_envio As New ObjEnvio

            Dim Crear_Envio As New Insertar_Envios
            Dim coloniaDesc = ""
            Label2.Text = ""

            If TxtCol2.Visible = False Then
                coloniaDesc = DropDownColonia.SelectedItem.Text
            Else
                coloniaDesc = TxtCol2.Text
            End If
            'Insetar nuevo destinataro
            Dim id_destinatario As Integer
            Datos_Dest.id_pais = DropDownPais2.SelectedValue
            Datos_Dest.nombre = TxtNombre2.Text
            Datos_Dest.apellidos = txtApellidos2.Text
            Datos_Dest.empresa = txtEmpresa2.Text
            Datos_Dest.calle = txtCalle2.Text
            Datos_Dest.noexterior = 0 ' Estamos pasando la dirección completa en el campo de calle.
            Datos_Dest.nointerior = Nothing
            Datos_Dest.direccion2 = Nothing
            Datos_Dest.colonia = coloniaDesc
            Datos_Dest.ciudad = txtCiudad2.Text
            Datos_Dest.municipio = TxtMpio2.Text
            Datos_Dest.estadoprovincia = txtEdo2.Text
            Datos_Dest.telefono = txtTelefono2.Text
            Datos_Dest.email = txtEmail2.Text
            If Not IsNothing(TxtCP2.Text) And Len(TxtCP2.Text) = 5 Then
                Datos_Dest.codigo_postal = TxtCP2.Text
            Else
                Datos_Dest.codigo_postal = Crear_Envio.valida_cp_mx(Datos_Dest.ciudad, txtEdo2.SelectedItem.Text)
            End If

            Mensaje = Crear_Envio.valida_datos_dest(Datos_Dest)
            If Mensaje = "OK" Then
                id_destinatario = Crear_Envio.crea_destinatario(Datos_Dest)
                Session("id_destinatario") = id_destinatario
            Else
                Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                ModalPopupExtender3.Show()
                Exit Sub
            End If

            Dim cajas_count As Integer = 0
            Dim envios(CInt(TxtCajas.Text) - 1) As Integer

            If Not guia_por_caja.Checked Then
                ReDim envios(0)
            End If
            Dim db As New SiExProEntities

            Do While cajas_count < envios.Length()

                'Insertar el Envío
                Dim id_envio As Integer
                datos_envio.id_agente = DropDownAgentes.Text 'it's an argument calling the method
                datos_envio.precio = TxtTarifa.Value
                datos_envio.valor_seguro = TxtSeguro.Text
                datos_envio.id_tarifa_agencia = DropDownProduct.SelectedValue

                If TxtPromo.Text <> "" And IsNumeric(TxtPromo.Text) Then
                    datos_envio.id_codigo_promocion = TxtPromo.Text
                Else
                    datos_envio.id_codigo_promocion = Nothing
                End If

                If TxtAduana.Text <> "" And IsNumeric(TxtAduana.Text) Then
                    datos_envio.valor_aduana = TxtAduana.Text
                Else
                    datos_envio.valor_aduana = Nothing
                End If

                datos_envio.total_envio = datos_envio.precio + datos_envio.valor_seguro
                datos_envio.fecha = DateTime.Now.ToString
                datos_envio.instrucciones_entrega = TxtInstEntrega.Text
                datos_envio.observaciones = Nothing   'future reference 
                datos_envio.id_usuario = Session("id_usuario")
                datos_envio.id_ruta = 0
                datos_envio.id_destinatario = id_destinatario
                datos_envio.id_cliente = 0
                datos_envio.largo = txtLargo.Text
                datos_envio.ancho = txtAncho.Text
                datos_envio.alto = txtAlto.Text
                datos_envio.peso = txtPeso.Text
                datos_envio.referencia = TxtRef.Text
                datos_envio.contenido = DropDownContenidos.Text
                datos_envio.dimension_peso = Session("dimension_peso")
                datos_envio.contenedores = TxtCajas.Text

                'PreRegistor del Envío
                Mensaje = Crear_Envio.valida_preregistro(datos_envio)
                If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then

                Else
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                    ModalPopupExtender3.Show()
                    Mensaje = ""
                    Exit Sub
                End If

                envios(cajas_count) = id_envio
                cajas_count = cajas_count + 1
            Loop

            Dim id_agencia As Integer = datos_envio.id_agente

            Dim estafetaWrapper As New EstafetaWrapper()
            Dim envioExportar As New FrecuenciaCotizadorExport()
            With envioExportar
                .CPDestinatario = Datos_Dest.codigo_postal
                .CPRemitente = ""
                .IdEnvio = 0
                .EsPaquete = True
                .Alto = txtAlto.Text
                .Largo = txtLargo.Text
                .Ancho = txtAncho.Text
                .Peso = txtPeso.Text
            End With

            Dim seguimiento As New seguimiento_envios
            Dim area_extendida_express_saver As Decimal = 0
            Dim area_extendida_standard_overnight As Decimal = 0
            Dim pesoVol As Decimal = (envioExportar.Alto * envioExportar.Ancho * envioExportar.Largo) / 5000
            Dim pesoVolumetrico = IIf(envioExportar.Peso > pesoVol, envioExportar.Peso, pesoVol)
            Dim estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, id_agencia, pesoVolumetrico, area_extendida_express_saver, area_extendida_standard_overnight, envioExportar.Peso, 0, 0)

            rbTerrestre.Checked = False
            rbDiaSiguiente.Checked = False
            rbLtl.Checked = False
            rbFedexExpress.Checked = False
            rbFedexStandard.Checked = False
            rbPaqueteExpressEconomic.Checked = False
            rbPaqueteExpressNextDay.Checked = False

            rbTerrestre.Visible = False
            rbDiaSiguiente.Visible = False
            rbLtl.Visible = False
            rbFedexExpress.Visible = False
            rbFedexStandard.Visible = False
            rbPaqueteExpressEconomic.Visible = False
            rbPaqueteExpressNextDay.Visible = False

            brTerrestre.Visible = False
            brDiaSiguiente.Visible = False
            brLtl.Visible = False
            brFedexExpress.Visible = False
            brFedexStandard.Visible = False
            brPaqueteExpressEconomic.Visible = False
            brPaqueteExpressNextDay.Visible = False

            Dim proveedor = DropDownProveedores.SelectedValue

            '************ FEDEX  **************
            If proveedor = 10 Then
                If datos_envio.largo = 0 Or datos_envio.alto = 0 Or datos_envio.peso = 0 Or datos_envio.ancho = 0 Then
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> Valores de dimensiones y peso no pueden ser cero"
                    ModalPopupExtender3.Show()
                    Exit Sub
                End If

                If (estafetaPrecios.StandardOvernightUser > 0 Or estafetaPrecios.ExpressSaverUser > 0) Then
                        Dim shipmentRequest As New ShipmentRequestDto()
                        With shipmentRequest
                            .AgentId = id_agencia
                            .ShipmentId = 1
                            .AccountId = 1
                            .CarrierId = 2
                        .ClientId = ConfigurationManager.AppSettings("PaqueteExpress.ClientId")
                        .Content = datos_envio.contenido
                        .DeliveryInstructions = datos_envio.instrucciones_entrega
                        .FedexExpressSaver = estafetaPrecios.ExpressSaverUser > 0
                        .FedexStandardOvernight = estafetaPrecios.StandardOvernightUser > 0
                        .Insurance = datos_envio.valor_seguro
                        .MultipleLabels = envios.Length() > 1
                        .PackageCount = envios.Length()
                        .ProductId = datos_envio.id_tarifa_agencia
                        .PromoId = datos_envio.id_codigo_promocion
                        .Reference = datos_envio.referencia
                        .Length = datos_envio.largo
                        .Height = datos_envio.alto
                        .Weight = datos_envio.peso
                        .Width = datos_envio.ancho
                    End With

                    Dim destinatary As New DestinataryDto()
                    With destinatary
                        .Address = Datos_Dest.direccion
                        .City = Datos_Dest.ciudad
                        .Company = Datos_Dest.empresa
                        .CountryCode = "MX"
                        .CountryId = 52
                        .DestinataryId = id_destinatario
                        .DownTown = Datos_Dest.colonia
                        .Email = Datos_Dest.email
                        .LastName = Datos_Dest.apellidos
                        .Name = Datos_Dest.nombre
                        .NoExt = Datos_Dest.noexterior
                        .NoInt = Datos_Dest.nointerior
                        .PhoneNumber = Datos_Dest.telefono
                        .State = Datos_Dest.estadoprovincia
                        .Street = Datos_Dest.calle
                        .Town = Datos_Dest.municipio
                        .ZipCode = Datos_Dest.codigo_postal
                    End With

                    Dim fedexShipRequest As New ShipRequestDto()
                    With fedexShipRequest
                        .Destinatary = destinatary
                        .ShipmentRequest = shipmentRequest
                    End With

                    Dim fedexResponse = DaspackDALC.FedexRate(fedexShipRequest)

                    If fedexResponse.Success AndAlso fedexResponse.Data IsNot Nothing Then
                        area_extendida_standard_overnight = fedexResponse.Data.StandardOvernight.Amount
                        area_extendida_express_saver = fedexResponse.Data.ExpressSaver.Amount
                    End If

                    estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, id_agencia, pesoVolumetrico, area_extendida_express_saver, area_extendida_standard_overnight, envioExportar.Peso, 0, 0)

                    If fedexResponse.Data.ExpressSaver.Supported = False Then
                        estafetaPrecios.ExpressSaverUser = 0
                    End If

                    If fedexResponse.Data.StandardOvernight.Supported = False Then
                        estafetaPrecios.StandardOvernightUser = 0
                    End If
                End If

                If estafetaPrecios.ExpressSaverUser > 0 And estafetaPrecios.ExpressSaverAmount > 0 Then
                    fedexExpress.Value = estafetaPrecios.ExpressSaverAmount
                    rbFedexExpress.Text = " Fedex Express Saver: " & FormatCurrency(estafetaPrecios.ExpressSaverAmount.ToString(), 2)
                    rbFedexExpress.Visible = True
                    brFedexExpress.Visible = True
                End If

                If estafetaPrecios.StandardOvernightUser > 0 And estafetaPrecios.StandardOvernightAmount > 0 Then
                    fedexStandard.Value = estafetaPrecios.StandardOvernightAmount
                    rbFedexStandard.Text = " Fedex Standar Overnight: " & FormatCurrency(estafetaPrecios.StandardOvernightAmount.ToString(), 2)
                    rbFedexStandard.Visible = True
                    brFedexStandard.Visible = True
                End If
            End If

            rbCosto.Text = "DraftLogistic: " & FormatCurrency(estafetaPrecios.Gombar.ToString(), 2)
            rbCosto.Checked = True
            brCosto.Visible = True
            lblOcurre.Visible = estafetaPrecios.Ocurre

            '************ ESTAFETA  **************
            If proveedor = 30 Then
                If datos_envio.largo = 0 Or datos_envio.alto = 0 Or datos_envio.peso = 0 Or datos_envio.ancho = 0 Then
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> Valores de dimensiones y peso no pueden ser cero"
                    ModalPopupExtender3.Show()
                    Exit Sub
                End If
                Dim respuestaFrecuenciaCotizador As FrecuenciaCotizadorRespuesta = estafetaWrapper.FrecuenciaCotizadorSingle(envioExportar, estafetaPrecios)

                If respuestaFrecuenciaCotizador.Respuesta IsNot Nothing AndAlso respuestaFrecuenciaCotizador.Respuesta.Length > 0 Then
                    If respuestaFrecuenciaCotizador.Respuesta(0).MensajeError <> "" Then
                        Label2.Text = respuestaFrecuenciaCotizador.Respuesta(0).MensajeError
                        ModalPopupExtender3.Show()
                        Mensaje = ""
                        Exit Sub
                    Else
                        Dim sGUID As String
                        sGUID = System.Guid.NewGuid.ToString()
                        estafetaTipoServicio.Value = sGUID
                        Session(sGUID) = respuestaFrecuenciaCotizador

                        For Each tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio In respuestaFrecuenciaCotizador.Respuesta(0).TipoServicio
                            If tipoServicio.DescripcionServicio = "Terrestre" And estafetaPrecios.Terrestre > 0 Then
                                estafetaTerrestre.Value = estafetaPrecios.Terrestre
                                rbTerrestre.Text = " Terrestre: " & FormatCurrency(estafetaPrecios.Terrestre.ToString(), 2)
                                rbTerrestre.Visible = True
                                brTerrestre.Visible = True
                            End If

                            If tipoServicio.DescripcionServicio = "Dia Sig." And estafetaPrecios.DiaSiguiente > 0 Then
                                estafetaDiaSig.Value = estafetaPrecios.DiaSiguiente
                                rbDiaSiguiente.Text = " Dia Siguiente: " & FormatCurrency(estafetaPrecios.DiaSiguiente.ToString(), 2)
                                rbDiaSiguiente.Visible = True
                                brDiaSiguiente.Visible = True
                            End If

                            If tipoServicio.DescripcionServicio = "LTL" And estafetaPrecios.Ltl > 0 Then
                                estafetaLtl.Value = estafetaPrecios.Ltl
                                rbLtl.Text = " Tarimas: " & FormatCurrency(estafetaPrecios.Ltl.ToString(), 2)
                                rbLtl.Visible = True
                                brLtl.Visible = True
                            End If
                        Next

                    End If
                End If
            End If

            '************ PAQUETE EXPRESS  **************
            If proveedor = 40 Then
                If (estafetaPrecios.PaqueteExpressEconomic > 0 Or estafetaPrecios.PaqueteExpressNextDay > 0) Then
                    Dim shipmentRequest As New ShipmentRequestDto()
                    With shipmentRequest
                        .AgentId = id_agencia
                        .ShipmentId = 1
                        .AccountId = estafetaPrecios.UserAccountPe
                        .CarrierId = 2
                        .ClientId = ConfigurationManager.AppSettings("PaqueteExpress.ClientId")
                        .Content = datos_envio.contenido
                        .DeliveryInstructions = datos_envio.instrucciones_entrega
                        .FedexExpressSaver = estafetaPrecios.ExpressSaverUser > 0
                        .FedexStandardOvernight = estafetaPrecios.StandardOvernightUser > 0
                        .Insurance = datos_envio.valor_seguro
                        .MultipleLabels = envios.Length() > 1
                        .PackageCount = envios.Length()
                        .ProductId = datos_envio.id_tarifa_agencia
                        .PromoId = datos_envio.id_codigo_promocion
                        .Reference = datos_envio.referencia
                        .Length = datos_envio.largo
                        .Height = datos_envio.alto
                        .Weight = datos_envio.peso
                        .Width = datos_envio.ancho
                        .IsOcurre = IIf(chkOcurre.Checked, 1, 0)
                    End With

                    Dim dtgridview As DataTable = TryCast(ViewState("Data"), DataTable)
                    Dim valorTotalDeclarado As Double = 0
                    Dim paqueteExpressEconomic As Decimal = 0
                    Dim paqueteExpressNextDay As Decimal = 0

                    If dtgridview IsNot Nothing Then
                        Dim shipmentItems As New List(Of ShipmentRequestItemDto)

                        For Each row As DataRow In dtgridview.Rows
                            Dim shipmentItem As New ShipmentRequestItemDto()
                            With shipmentItem
                                .Content = row("Contenido")
                                .Height = row("Alto")
                                .Length = row("Largo")
                                .Quantity = row("Cantidad")
                                .Weight = row("Peso")
                                .Width = row("Ancho")
                                .ShpCode = row("TipoClave")
                                .Insurance = row("Seguro")
                                .SatService = row("ServicioSAT")
                                .SatServiceDesc = row("ServicioSATDesc")
                            End With

                            valorTotalDeclarado = valorTotalDeclarado + row("Seguro")
                            shipmentItems.Add(shipmentItem)
                        Next row
                        shipmentRequest.ShipmentItems = shipmentItems
                    End If
                    shipmentRequest.TotlDeclVlue = valorTotalDeclarado
                    hdnValorTotalDeclarado.Value = valorTotalDeclarado

                    Dim destinatary As New DestinataryDto()
                    With destinatary
                        .Address = Datos_Dest.direccion
                        .City = Datos_Dest.ciudad
                        .Company = Datos_Dest.empresa
                        .CountryCode = "MX"
                        .CountryId = 52
                        .DestinataryId = id_destinatario
                        .DownTown = Datos_Dest.colonia
                        .Email = IIf(String.IsNullOrWhiteSpace(Datos_Dest.email), "embarqueahora@hotmail.com", Datos_Dest.email)
                        .LastName = Datos_Dest.apellidos
                        .Name = Datos_Dest.nombre
                        .NoExt = Datos_Dest.noexterior
                        .NoInt = Datos_Dest.nointerior
                        .PhoneNumber = Datos_Dest.telefono
                        .State = Datos_Dest.estadoprovincia
                        .Street = Datos_Dest.calle
                        .Town = Datos_Dest.municipio
                        .ZipCode = Datos_Dest.codigo_postal
                    End With

                    Dim paqueteExpressQuoteRequest As New ShipRequestDto()
                    With paqueteExpressQuoteRequest
                        .Destinatary = destinatary
                        .ShipmentRequest = shipmentRequest
                    End With

                    Dim paqueteExpressResponse = DaspackDALC.PaqueteExpressQuote(paqueteExpressQuoteRequest)

                    If paqueteExpressResponse.Success Then
                        If paqueteExpressResponse.Data IsNot Nothing AndAlso paqueteExpressResponse.Data.Quotations.Count > 0 Then
                            Dim economico = paqueteExpressResponse.Data.Quotations.FirstOrDefault(Function(x) x.ServiceType = "ST")

                            If estafetaPrecios.PaqueteExpressEconomic > 0 And economico IsNot Nothing Then
                                Dim areaExtendida = economico.OtherServices.otherServices.FirstOrDefault(Function(x) x.Id = "EXT-1")
                                Dim valorAreaExtendida As Double = 0

                                If areaExtendida IsNot Nothing Then
                                    valorAreaExtendida = areaExtendida.Amt + areaExtendida.AmtTax
                                End If

                                If dtgridview IsNot Nothing Then
                                    For Each row As DataRow In dtgridview.Rows
                                        pesoVol = (row("Alto") * row("Ancho") * row("Largo")) / 5000
                                        estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, id_agencia, pesoVol, area_extendida_express_saver, area_extendida_standard_overnight, row("Peso"), valorAreaExtendida, valorTotalDeclarado)

                                        paqueteExpressEconomic = paqueteExpressEconomic + (estafetaPrecios.PaqueteExpressEconomic * row("Cantidad"))
                                    Next row
                                    estafetaPrecios.PaqueteExpressEconomic = paqueteExpressEconomic
                                End If

                                hdnValorAreaExtendida.Value = valorAreaExtendida
                                hdnPaqueteExpressEconomic.Value = estafetaPrecios.PaqueteExpressEconomic
                                rbPaqueteExpressEconomic.Text = " Paquete Express Economico: " & FormatCurrency(estafetaPrecios.PaqueteExpressEconomic.ToString(), 2)
                                rbPaqueteExpressEconomic.Visible = True
                                brPaqueteExpressEconomic.Visible = True
                            End If

                            Dim diaSiguiente = paqueteExpressResponse.Data.Quotations.FirstOrDefault(Function(x) x.ServiceType = "DS")
                            If estafetaPrecios.PaqueteExpressNextDay > 0 And diaSiguiente IsNot Nothing Then
                                Dim areaExtendida = diaSiguiente.OtherServices.otherServices.FirstOrDefault(Function(x) x.Id = "EXT-1")
                                Dim valorAreaExtendida As Double = 0

                                If areaExtendida IsNot Nothing Then
                                    valorAreaExtendida = areaExtendida.Amt
                                End If

                                If dtgridview IsNot Nothing Then
                                    For Each row As DataRow In dtgridview.Rows
                                        pesoVol = (row("Alto") * row("Ancho") * row("Largo")) / 5000
                                        estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, id_agencia, pesoVol, area_extendida_express_saver, area_extendida_standard_overnight, row("Peso"), valorAreaExtendida, valorTotalDeclarado)
                                        paqueteExpressNextDay = paqueteExpressNextDay + (estafetaPrecios.PaqueteExpressNextDay * row("Cantidad"))
                                    Next row
                                    estafetaPrecios.PaqueteExpressNextDay = paqueteExpressNextDay
                                End If

                                hdnPaqueteExpressNextDay.Value = estafetaPrecios.PaqueteExpressNextDay
                                hdnValorAreaExtendida.Value = valorAreaExtendida
                                rbPaqueteExpressNextDay.Text = " Paquete Express Dia Siguiente: " & FormatCurrency(estafetaPrecios.PaqueteExpressNextDay.ToString(), 2)
                                rbPaqueteExpressNextDay.Visible = True
                                brPaqueteExpressNextDay.Visible = True
                            End If
                        Else
                            Label2.Text = "Servicio no disponible"
                            ModalPopupExtender3.Show()
                        End If

                    Else
                        Label2.Text = "Ocurrió un error, por favor revise los datos -->" + paqueteExpressResponse.ErrorMessage
                        ModalPopupExtender3.Show()
                    End If
                End If
            End If

            If Label2.Text = "" Then
                ModalPopupExtender7.Show()
            End If
        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If Panel3.Enabled = False Then
        '    Panel3.Enabled = True
        'End If
        GridView2.DataBind()
        ModalPopupExtender2.Show()
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Try
            If Not (GridView2.SelectedIndex = 0 And GridView2.PageIndex = 0) Then
                'PopupControlExtender.GetProxyForCurrentPopup(Me.Page).Commit(GridView1.SelectedValue)
                Dim row As GridViewRow = GridView2.SelectedRow
                'MsgBox(row.Cells(2).Text + " " + row.Cells(3).Text)
                TxtNombre2.Text = row.Cells(2).Text
                txtApellidos2.Text = row.Cells(3).Text
                txtEmpresa2.Text = row.Cells(4).Text
                txtCalle2.Text = row.Cells(5).Text
                'TxtNoExt2.Text = row.Cells(5).Text
                'TxtNoInt2.Text = row.Cells(6).Text

                txtEdo2.SelectedValue = row.Cells(7).Text
                txtCiudad2.Text = row.Cells(6).Text

                'txtMpio2.Text = row.Cells(9).Text
                txtTelefono2.Text = row.Cells(8).Text
                txtEmail2.Text = row.Cells(9).Text
                TxtCP2.Text = row.Cells(10).Text

                Session("id_destinatario") = row.Cells(1).Text
                TxtBuscaDest.Text = ""
                GridView2.DataBind()
                'Panel3.Enabled = False
            Else
                'Panel3.Enabled = True
                TxtNombre2.Focus()
                TxtBuscaDest.Text = ""
                GridView2.DataBind()
                Session("id_destinatario") = 0
            End If
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub Inserta_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal id_envio_imp As Integer = 0, Optional ByVal genera_guia As Boolean = True) Handles btnAceptar.Click
        Try
            Dim Mensaje As String = "" 'para devolver resultado de validación de mensajes
            Dim datos_cliente As New ObjCliente
            Dim Datos_Dest As New ObjDestinatario
            Dim datos_envio As New ObjEnvio
            Dim Crear_Envio As New Insertar_Envios

            Dim coloniaDesc = ""

            If TxtCol2.Visible = False Then
                coloniaDesc = DropDownColonia.SelectedItem.Text
            Else
                coloniaDesc = TxtCol2.Text
            End If
            'Insetar nuevo destinataro
            Dim id_destinatario As Integer
            Datos_Dest.id_pais = DropDownPais2.SelectedValue
            Datos_Dest.nombre = TxtNombre2.Text
            Datos_Dest.apellidos = txtApellidos2.Text
            Datos_Dest.empresa = txtEmpresa2.Text
            Datos_Dest.calle = txtCalle2.Text
            Datos_Dest.noexterior = 0 ' Estamos pasando la dirección completa en el campo de calle.
            Datos_Dest.nointerior = Nothing
            Datos_Dest.direccion2 = Nothing
            Datos_Dest.colonia = coloniaDesc
            Datos_Dest.ciudad = txtCiudad2.Text

            Datos_Dest.municipio = TxtMpio2.Text
            Datos_Dest.estadoprovincia = txtEdo2.Text
            Datos_Dest.telefono = txtTelefono2.Text
            Datos_Dest.email = txtEmail2.Text

            If Not IsNothing(TxtCP2.Text) And Len(TxtCP2.Text) = 5 Then
                Datos_Dest.codigo_postal = TxtCP2.Text
            Else
                Datos_Dest.codigo_postal = Crear_Envio.valida_cp_mx(Datos_Dest.ciudad, txtEdo2.SelectedItem.Text)
            End If

            Mensaje = Crear_Envio.valida_datos_dest(Datos_Dest)
            If Mensaje = "OK" Then
                id_destinatario = Crear_Envio.crea_destinatario(Datos_Dest)
                Session("id_destinatario") = id_destinatario
            Else
                Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                ModalPopupExtender3.Show()
                Exit Sub
            End If

            Dim destinatary As New DestinataryDto()
            With destinatary
                .Address = Datos_Dest.direccion
                .City = Datos_Dest.ciudad
                .Company = Datos_Dest.empresa
                .CountryCode = "MX"
                .CountryId = 52
                .DestinataryId = id_destinatario
                .DownTown = Datos_Dest.colonia
                .Email = Datos_Dest.email
                .LastName = Datos_Dest.apellidos
                .Name = Datos_Dest.nombre
                .NoExt = Datos_Dest.noexterior
                .NoInt = Datos_Dest.nointerior
                .PhoneNumber = Datos_Dest.telefono
                .State = Datos_Dest.estadoprovincia
                .Street = Datos_Dest.calle
                .Town = Datos_Dest.municipio
                .ZipCode = Datos_Dest.codigo_postal
            End With

            Dim cajas_count As Integer = 0
            Dim envios(CInt(TxtCajas.Text) - 1) As Integer

            If Not guia_por_caja.Checked Then
                ReDim envios(0)
            End If

            Dim estafetaWrapper As New EstafetaWrapper()
            Dim respuestaFrecuenciaCotizador = CType(Session(estafetaTipoServicio.Value), FrecuenciaCotizadorRespuesta)
            Dim sessionTipoServico As Estafeta.Frecuenciacotizador.TipoServicio()
            If respuestaFrecuenciaCotizador IsNot Nothing AndAlso respuestaFrecuenciaCotizador.Respuesta IsNot Nothing Then
                sessionTipoServico = respuestaFrecuenciaCotizador.Respuesta(0).TipoServicio
            End If

            Dim tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio = New Estafeta.Frecuenciacotizador.TipoServicio()
            Dim envioEstafeta As Boolean = False
            Dim cuentaServicio As New EstafetaCuentaServicio
            Dim id_cliente As Integer = 0
            Dim db As New SiExProEntities
            Dim agente = db.C_AGENCIAS.FirstOrDefault(Function(x) x.id_agencia = DropDownAgentes.Text)

            Dim seguimiento As New seguimiento_envios
            Dim envioExportar As New FrecuenciaCotizadorExport()
            With envioExportar
                .CPDestinatario = Datos_Dest.codigo_postal
                .CPRemitente = ""
                .IdEnvio = 0
                .EsPaquete = True
                .Alto = txtAlto.Text
                .Largo = txtLargo.Text
                .Ancho = txtAncho.Text
                .Peso = txtPeso.Text
            End With

            Dim shipmentRequest As New ShipmentRequestDto()
            Dim fedexShipRequest As New ShipRequestDto()

            Dim area_extendida_express_saver As Decimal = 0
            Dim area_extendida_standard_overnight As Decimal = 0
            Dim pesoVol As Decimal = (envioExportar.Alto * envioExportar.Ancho * envioExportar.Largo) / 5000
            Dim pesoVolumetrico = IIf(envioExportar.Peso > pesoVol, envioExportar.Peso, pesoVol)
            Dim estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, DropDownAgentes.Text, pesoVolumetrico, area_extendida_express_saver, area_extendida_standard_overnight, envioExportar.Peso, 0, 0)

            Do While cajas_count < envios.Length()
                Dim cliente As Cliente = Nothing
                If respuestaFrecuenciaCotizador IsNot Nothing AndAlso respuestaFrecuenciaCotizador.Respuesta IsNot Nothing Then
                    cuentaServicio = respuestaFrecuenciaCotizador.CuentaServicios.Where(Function(x) x.Seleccionada = True).FirstOrDefault()
                End If

                Dim valor_envio = estafetaPrecios.Gombar
                Dim total_envio As Decimal = estafetaPrecios.Gombar

                If rbTerrestre.Checked Then
                    tipoServicio = sessionTipoServico.FirstOrDefault(Function(x) x.DescripcionServicio = "Terrestre")
                    valor_envio = tipoServicio.CostoTotal
                    envioEstafeta = True
                    datos_envio.observaciones = "Terrestre"
                    cuentaServicio = respuestaFrecuenciaCotizador.CuentaServicios.FirstOrDefault(Function(x) x.Servicio = "Terrestre")
                    total_envio = estafetaPrecios.Terrestre
                    id_cliente = cuentaServicio.Cliente.id_cliente
                    cliente = cuentaServicio.Cliente
                End If

                If rbDiaSiguiente.Checked Then
                    tipoServicio = sessionTipoServico.FirstOrDefault(Function(x) x.DescripcionServicio = "Dia Sig.")
                    valor_envio = tipoServicio.CostoTotal
                    datos_envio.observaciones = "Dia Sig."
                    envioEstafeta = True
                    cuentaServicio = respuestaFrecuenciaCotizador.CuentaServicios.FirstOrDefault(Function(x) x.Servicio = "Dia Sig.")
                    total_envio = estafetaPrecios.DiaSiguiente
                    id_cliente = cuentaServicio.Cliente.id_cliente
                    cliente = cuentaServicio.Cliente
                End If

                If rbLtl.Checked Then
                    tipoServicio = sessionTipoServico.FirstOrDefault(Function(x) x.DescripcionServicio = "LTL")
                    valor_envio = tipoServicio.CostoTotal
                    datos_envio.observaciones = "LTL"
                    envioEstafeta = True
                    cuentaServicio = respuestaFrecuenciaCotizador.CuentaServicios.FirstOrDefault(Function(x) x.Servicio = "LTL")
                    total_envio = estafetaPrecios.Ltl
                    id_cliente = cuentaServicio.Cliente.id_cliente
                    cliente = cuentaServicio.Cliente
                End If

                If rbFedexExpress.Checked Or rbFedexStandard.Checked Then
                    With shipmentRequest
                        .AgentId = agente.id_agencia
                        .ShipmentId = 1
                        .AccountId = 1
                        .CarrierId = 2
                        .Content = datos_envio.contenido
                        .DeliveryInstructions = datos_envio.instrucciones_entrega
                        .FedexExpressSaver = rbFedexExpress.Checked
                        .FedexStandardOvernight = rbFedexStandard.Checked
                        .Insurance = datos_envio.valor_seguro
                        .MultipleLabels = envios.Length() > 1
                        .PackageCount = envios.Length()
                        .ProductId = DropDownProduct.SelectedValue
                        .PromoId = datos_envio.id_codigo_promocion
                        .Reference = TxtRef.Text
                        .Length = txtLargo.Text
                        .Height = txtAlto.Text
                        .Weight = txtPeso.Text
                        .Width = txtAncho.Text
                    End With

                    If rbFedexExpress.Checked Then
                        cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.ExpressSaverUser)
                        shipmentRequest.AccountId = estafetaPrecios.ExpressSaverUser
                        shipmentRequest.ClientId = cliente.id_cliente
                    End If

                    If rbFedexStandard.Checked Then
                        cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.StandardOvernightUser)
                        shipmentRequest.AccountId = estafetaPrecios.StandardOvernightUser
                        shipmentRequest.ClientId = cliente.id_cliente
                    End If

                    With fedexShipRequest
                        .Destinatary = destinatary
                        .ShipmentRequest = shipmentRequest
                    End With

                    Dim fedexRateResponse = DaspackDALC.FedexRate(fedexShipRequest)
                    If fedexRateResponse.Success AndAlso fedexRateResponse.Data IsNot Nothing Then
                        area_extendida_standard_overnight = fedexRateResponse.Data.StandardOvernight.Amount
                        area_extendida_express_saver = fedexRateResponse.Data.ExpressSaver.Amount
                    End If

                    estafetaPrecios = seguimiento.costo_estafeta_gombar(Datos_Dest.codigo_postal, DropDownAgentes.Text, pesoVolumetrico, area_extendida_express_saver, area_extendida_standard_overnight, envioExportar.Peso, 0, 0)

                    If rbFedexExpress.Checked Then
                        valor_envio = estafetaPrecios.ExpressSaverAmount
                        datos_envio.observaciones = "FedexExpressSaver"
                        envioEstafeta = False
                        total_envio = estafetaPrecios.ExpressSaverAmount
                        cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.ExpressSaverUser)
                        shipmentRequest.AccountId = estafetaPrecios.ExpressSaverUser
                        shipmentRequest.ClientId = cliente.id_cliente
                        datos_envio.id_cliente = cliente.id_cliente
                        id_cliente = cliente.id_cliente
                    End If

                    If rbFedexStandard.Checked Then
                        valor_envio = estafetaPrecios.StandardOvernightAmount
                        datos_envio.observaciones = "FedexStandardOvernight"
                        envioEstafeta = False
                        total_envio = estafetaPrecios.StandardOvernightAmount
                        cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.StandardOvernightUser)
                        shipmentRequest.AccountId = estafetaPrecios.StandardOvernightUser
                        shipmentRequest.ClientId = cliente.id_cliente
                        datos_envio.id_cliente = cliente.id_cliente
                        id_cliente = cliente.id_cliente
                    End If
                End If

                If (estafetaPrecios.PaqueteExpressEconomic > 0 Or estafetaPrecios.PaqueteExpressNextDay > 0) Then
                    If rbPaqueteExpressEconomic.Checked Or rbPaqueteExpressNextDay.Checked Then
                        shipmentRequest = New ShipmentRequestDto()

                        With shipmentRequest
                            .AgentId = agente.id_agencia
                            .ShipmentId = 1
                            .CarrierId = 2
                            .Content = datos_envio.contenido
                            .DeliveryInstructions = datos_envio.instrucciones_entrega
                            .FedexExpressSaver = estafetaPrecios.ExpressSaverUser > 0
                            .FedexStandardOvernight = estafetaPrecios.StandardOvernightUser > 0
                            .Insurance = datos_envio.valor_seguro
                            .MultipleLabels = envios.Length() > 1
                            .PackageCount = envios.Length()
                            .ProductId = datos_envio.id_tarifa_agencia
                            .PromoId = datos_envio.id_codigo_promocion
                            .Reference = datos_envio.referencia
                            .Length = datos_envio.largo
                            .Height = datos_envio.alto
                            .Weight = datos_envio.peso
                            .Width = datos_envio.ancho
                            .IsOcurre = IIf(chkOcurre.Checked, 1, 0)
                        End With

                        If rbPaqueteExpressEconomic.Checked Then
                            cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.UserAccountPe)
                            shipmentRequest.ClientId = cliente.id_cliente
                            datos_envio.id_cliente = cliente.id_cliente
                            id_cliente = cliente.id_cliente
                            shipmentRequest.AccountId = estafetaPrecios.UserAccountPe
                        End If

                        If rbPaqueteExpressNextDay.Checked Then
                            cliente = db.C_CLIENTES.FirstOrDefault(Function(x) x.NIT = estafetaPrecios.UserAccountPe)
                            shipmentRequest.ClientId = cliente.id_cliente
                            datos_envio.id_cliente = cliente.id_cliente
                            id_cliente = cliente.id_cliente
                            shipmentRequest.AccountId = estafetaPrecios.UserAccountPe
                        End If

                        Dim dtgridview As DataTable = TryCast(ViewState("Data"), DataTable)
                        If dtgridview IsNot Nothing Then
                            Dim shipmentItems As New List(Of ShipmentRequestItemDto)
                            Dim paqueteExpressEconomic As Decimal = 0
                            Dim paqueteExpressNextDay As Decimal = 0

                            For Each row As DataRow In dtgridview.Rows
                                Dim shipmentItem As New ShipmentRequestItemDto()
                                With shipmentItem
                                    .Content = row("Contenido")
                                    .Height = row("Alto")
                                    .Length = row("Largo")
                                    .Quantity = row("Cantidad")
                                    .Weight = row("Peso")
                                    .Width = row("Ancho")
                                    .ShpCode = row("TipoClave")
                                    .ShpCodeDesc = row("Tipo")
                                    .Insurance = row("Seguro")
                                    .SatService = row("ServicioSAT")
                                    .SatServiceDesc = row("ServicioSATDesc")
                                End With

                                shipmentItems.Add(shipmentItem)
                            Next row
                            shipmentRequest.TotlDeclVlue = hdnValorTotalDeclarado.Value
                            shipmentRequest.ShipmentItems = shipmentItems
                        End If

                        destinatary = New DestinataryDto()
                        With destinatary
                            .Address = Datos_Dest.direccion
                            .City = Datos_Dest.ciudad
                            .Company = Datos_Dest.empresa
                            .CountryCode = "MX"
                            .CountryId = 52
                            .DestinataryId = id_destinatario
                            .DownTown = Datos_Dest.colonia
                            .Email = Datos_Dest.email
                            .LastName = Datos_Dest.apellidos
                            .Name = Datos_Dest.nombre
                            .NoExt = Datos_Dest.noexterior
                            .NoInt = Datos_Dest.nointerior
                            .PhoneNumber = Datos_Dest.telefono
                            .State = Datos_Dest.estadoprovincia
                            .Street = Datos_Dest.calle
                            .Town = Datos_Dest.municipio
                            .ZipCode = Datos_Dest.codigo_postal
                        End With

                        Dim paqueteExpressQuoteRequest As New ShipRequestDto()
                        With paqueteExpressQuoteRequest
                            .Destinatary = destinatary
                            .ShipmentRequest = shipmentRequest
                        End With

                        If rbPaqueteExpressEconomic.Checked Then
                            valor_envio = hdnPaqueteExpressEconomic.Value
                            datos_envio.observaciones = "PaqueteExpressEconomic"
                            envioEstafeta = False
                            total_envio = hdnPaqueteExpressEconomic.Value
                        End If

                        If rbPaqueteExpressNextDay.Checked Then
                            valor_envio = hdnPaqueteExpressNextDay.Value
                            datos_envio.observaciones = "PaqueteExpressNextDay"
                            envioEstafeta = False
                            total_envio = hdnPaqueteExpressNextDay.Value
                        End If
                    End If
                End If

                If cliente IsNot Nothing And (rbTerrestre.Checked Or rbDiaSiguiente.Checked Or rbLtl.Checked) Then
                    datos_cliente.id_pais = cliente.id_pais
                    datos_cliente.nombre = cliente.nombre
                    datos_cliente.apellidos = cliente.apellidos
                    datos_cliente.empresa = cliente.empresa
                    datos_cliente.calle = cliente.calle
                    datos_cliente.noexterior = cliente.noexterior
                    datos_cliente.nointerior = cliente.nointerior
                    datos_cliente.direccion2 = cliente.direccion2
                    datos_cliente.colonia = cliente.colonia
                    datos_cliente.ciudad = cliente.ciudad
                    datos_cliente.ciudad = cliente.ciudad
                    datos_cliente.municipio = cliente.municipio
                    datos_cliente.estadoprovincia = cliente.estadoprovincia
                    datos_cliente.telefono = cliente.telefono
                    datos_cliente.email = cliente.email
                    datos_cliente.codigo_postal = cliente.codigo_postal
                End If

                If rbCosto.Checked Then
                    Dim clienteGombar = DaspackDALC.GetGombarSender(4)
                    id_cliente = clienteGombar.id_cliente
                    datos_envio.observaciones = "DraftLogistics"
                End If

                'Insertar el Envío
                Dim id_envio As Integer

                datos_envio.id_agente = DropDownAgentes.Text 'it's an argument calling the method
                datos_envio.precio = valor_envio
                datos_envio.valor_seguro = TxtSeguro.Text
                datos_envio.id_tarifa_agencia = DropDownProduct.SelectedValue

                If TxtPromo.Text <> "" And IsNumeric(TxtPromo.Text) Then
                    datos_envio.id_codigo_promocion = TxtPromo.Text
                Else
                    datos_envio.id_codigo_promocion = Nothing
                End If

                If TxtAduana.Text <> "" And IsNumeric(TxtAduana.Text) Then
                    datos_envio.valor_aduana = TxtAduana.Text
                Else
                    datos_envio.valor_aduana = Nothing
                End If

                datos_envio.total_envio = total_envio + datos_envio.valor_seguro
                datos_envio.fecha = DateTime.Now.ToString
                datos_envio.instrucciones_entrega = TxtInstEntrega.Text
                datos_envio.id_usuario = Session("id_usuario")
                datos_envio.id_ruta = 0
                datos_envio.id_destinatario = id_destinatario
                datos_envio.id_cliente = id_cliente
                datos_envio.largo = txtLargo.Text
                datos_envio.ancho = txtAncho.Text
                datos_envio.alto = txtAlto.Text
                datos_envio.peso = txtPeso.Text
                datos_envio.referencia = TxtRef.Text
                datos_envio.contenido = DropDownContenidos.Text
                datos_envio.dimension_peso = Session("dimension_peso")
                datos_envio.contenedores = TxtCajas.Text

                'PreRegistor del Envío
                Mensaje = Crear_Envio.valida_preregistro(datos_envio)
                If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                    id_envio = Crear_Envio.PreRegistro_Envios(DropDownAgentes.Text, datos_envio, id_envio_imp)
                Else
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                    ModalPopupExtender3.Show()
                    Mensaje = ""
                    Exit Sub
                End If

                'Registro de Envíos (Detalles)
                Crear_Envio.Detalle_Envios(id_envio, datos_envio, 0, TxtObservaciones.Text)
                TextBox2.Text = id_envio.ToString

                Guia.DataBind()

                'Insertar SobreCargos
                Crear_Envio.inserta_SobreCargos(id_envio)

                'Inserta seguimiento
                Dim ins_seguimiento As New seguimiento_envios
                ins_seguimiento.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, "", Session("id_usuario"))

                envios(cajas_count) = id_envio
                datos_envio.id_envio = id_envio
                cajas_count = cajas_count + 1
                Label1.Text = "Útimo envío-> " & id_envio.ToString & " fue creado con exito"
            Loop

            If envioEstafeta = True Then
                Dim respuestaLabel As String = estafetaWrapper.Label(datos_envio, datos_cliente, Datos_Dest, tipoServicio, respuestaFrecuenciaCotizador.Respuesta, cuentaServicio.Cuenta, envios, ddlTipoImpresion.SelectedValue)

                If agente.guia_estafeta = True And respuestaLabel = "Envio Exportado" Then
                    Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('../Reports/EstafetaLabel.aspx?id_envio=" & envios(cajas_count - 1).ToString & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
                Else
                    If agente.guia_estafeta = True Then
                        Label2.Text = "Ocurrió un error, por favor revise los datos ---> Error al crear etiqueta " + respuestaLabel
                        ModalPopupExtender3.Show()
                    Else

                        Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('guia_individual.aspx?id_envio1=" & envios(0).ToString & "&id_envio2=" & envios(cajas_count - 1).ToString & "&id_agente=" & datos_envio.id_agente & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
                    End If
                End If
            Else
                If rbFedexStandard.Checked Or rbFedexExpress.Checked Then
                    With shipmentRequest
                        .AgentId = agente.id_agencia
                        .ShipmentId = envios(0)
                        .CarrierId = 2
                        .Content = datos_envio.contenido
                        .DeliveryInstructions = datos_envio.instrucciones_entrega
                        .FedexExpressSaver = rbFedexExpress.Checked
                        .FedexStandardOvernight = rbFedexStandard.Checked
                        .Insurance = datos_envio.valor_seguro
                        .MultipleLabels = envios.Length() > 1
                        .PackageCount = envios.Length()
                        .ProductId = DropDownProduct.SelectedValue
                        .PromoId = datos_envio.id_codigo_promocion
                        .Reference = TxtRef.Text
                        .Length = txtLargo.Text
                        .Height = txtAlto.Text
                        .Weight = txtPeso.Text
                        .Width = txtAncho.Text
                        .PaperType = ddlTipoImpresion.SelectedValue
                        .ClientId = id_cliente
                    End With
                    fedexShipRequest.ShipmentRequest = shipmentRequest
                    fedexShipRequest.Destinatary = destinatary

                    Dim fedexResponse = DaspackDALC.FedexShipment(fedexShipRequest)
                    If fedexResponse.Success Then
                        Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('../Reports/PrintFedexLabel.aspx?id_envio=" & envios(0).ToString & "&tipo_impresion=" & ddlTipoImpresion.SelectedValue & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                        'Dim sjscript2 As String = "<script language=""javascript"">" &
                        '" window.open('../Reports/PrintFedexLabel.aspx?id_envio=" & envios(0).ToString & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        '"</script>"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
                    Else
                        Label2.Text = "Ocurrió un error, por favor revise los datos ---> Error al crear etiqueta " + fedexResponse.ErrorMessage
                        ModalPopupExtender3.Show()
                    End If
                Else
                    If rbPaqueteExpressEconomic.Checked Or rbPaqueteExpressNextDay.Checked Then
                        With shipmentRequest
                            .AgentId = agente.id_agencia
                            .ShipmentId = envios(0)
                            .CarrierId = 2
                            .Content = datos_envio.contenido
                            .DeliveryInstructions = datos_envio.instrucciones_entrega
                            .FedexExpressSaver = rbFedexExpress.Checked
                            .FedexStandardOvernight = rbFedexStandard.Checked
                            .Insurance = datos_envio.valor_seguro
                            .MultipleLabels = envios.Length() > 1
                            .PackageCount = envios.Length()
                            .ProductId = DropDownProduct.SelectedValue
                            .PromoId = datos_envio.id_codigo_promocion
                            .Reference = TxtRef.Text
                            .Length = txtLargo.Text
                            .Height = txtAlto.Text
                            .Weight = txtPeso.Text
                            .Width = txtAncho.Text
                            .TypeSrvcId = IIf(rbPaqueteExpressEconomic.Checked, "STD-T", "SEG-DS")
                            .PaperType = ddlTipoImpresion.SelectedValue
                            .ClientId = id_cliente
                            .IsOcurre = IIf(chkOcurre.Checked, 1, 0)
                        End With
                        fedexShipRequest.ShipmentRequest = shipmentRequest
                        fedexShipRequest.Destinatary = destinatary

                        Dim peResponse = DaspackDALC.PaqueteExpressShip(fedexShipRequest)
                        If peResponse.Success Then
                            If String.IsNullOrEmpty(peResponse.ErrorMessage) Then
                                Dim dtgridview As DataTable = TryCast(ViewState("Data"), DataTable)
                                If dtgridview IsNot Nothing Then
                                    DaspackDALC.AddPaqueteExpressTipoPaquetes(dtgridview, envios(0))
                                End If

                                Dim impresion = ""
                                If ddlTipoImpresion.SelectedValue = 2 Then
                                    impresion = "&measure=4x6"
                                End If

                                Dim sjscript2 As String = "<script language=""javascript"">" &
                                " window.open('" & ConfigurationManager.AppSettings("PaqueteExpress.Label") & peResponse.Data & impresion & "', '_blank')" &
                                "</script>"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)

                                ViewState("Data") = Nothing
                                BindGridView()
                            Else
                                Label2.Text = "Ocurrió un error, por favor revise los datos -->" + peResponse.ErrorMessage
                                ModalPopupExtender3.Show()
                            End If
                        Else
                            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + peResponse.ErrorMessage
                            ModalPopupExtender3.Show()
                        End If

                    Else
                        Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('guia_individual.aspx?id_envio1=" & envios(0).ToString & "&id_envio2=" & envios(cajas_count - 1).ToString & "&id_agente=" & datos_envio.id_agente & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
                    End If
                End If
            End If

            Dim Ctrl As Control
            For Each Ctrl In Panel3.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Text = ""
                End If
            Next
            For Each Ctrl In Panel4.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    If txt.ID = "txtLargo" Or txt.ID = "txtAncho" Or txt.ID = "txtAlto" Or txt.ID = "txtPeso" Then
                        txt.Text = 0
                    ElseIf txt.ID <> "TxtTarifa" Then
                        txt.Text = ""
                    End If
                End If
                If (Ctrl.GetType() Is GetType(DropDownList)) Then
                    Dim cbobx As DropDownList = CType(Ctrl, DropDownList)
                    'MsgBox(cbobx.ID.ToString)
                    If cbobx.ID <> "DropDownAgentes" And cbobx.ID <> "DropDownProduct" Then
                        cbobx.SelectedIndex = -1
                    Else
                    End If
                End If
            Next

            Session("id_cliente") = 0
            id_cliente = 0
            Session("id_destinatario") = 0
            id_destinatario = 0

            TxtCajas.Text = "1"
            TxtSeguro.Text = "0"
            txtPeso.Text = "1"
            guia_por_caja.Checked = False


            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString
            connection.Open()

            Dim cmd2 As Data.IDbCommand = connection.CreateCommand()
            cmd2.CommandType = Data.CommandType.StoredProcedure
            cmd2.CommandText = "sp_Select_Envios_Pendientes_Uso"

            Dim parm2 As Data.Common.DbParameter = cmd2.CreateParameter()
            parm2.ParameterName = "@id_agencia"
            parm2.Value = DropDownAgentes.SelectedValue
            cmd2.Parameters.Add(parm2)

            Dim reader2 As Data.SqlClient.SqlDataReader = cmd2.ExecuteReader()
            If reader2.HasRows Then
                reader2.Read()
                EnviosAsignados.Text = Format(reader2.GetValue(0), "#,##;(#,##);0")
            End If
            reader2.Close()
            connection.Close()


        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub btnDatosUltimoEnvio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDatosUltimoEnvio.Click
        Dim id_usuario As Integer = Session("id_usuario")
        Dim mensaje As String = ""
        Try
            Dim envio As Envio = DaspackDALC.GetUltimoEnvio(id_usuario)
            If envio IsNot Nothing Then
                Dim tarifaAgencia As TarifaAgencia = DaspackDALC.GetTarifaAgencia(envio.id_tarifa_agencia)
                If tarifaAgencia IsNot Nothing Then
                    DropDownAgentes.SelectedValue = tarifaAgencia.id_agencia
                    DropDownProduct.DataBind()
                    DropDownProduct.SelectedValue = tarifaAgencia.id_tarifa_agencia
                End If

                With envio
                    TxtTarifa.value = .precio
                    TxtSeguro.Text = .valor_seguro
                    TxtPromo.Text = .id_codigo_promocion
                    TxtAduana.Text = .valor_aduana
                    TxtInstEntrega.Text = .instrucciones_entrega

                    txtLargo.Text = .largo
                    txtAncho.Text = .ancho
                    txtAlto.Text = .alto
                    txtPeso.Text = .peso
                    TxtRef.Text = .Referencia1
                    DropDownContenidos.SelectedValue = .id_contenido
                    TxtCajas.Text = .contenedores
                End With

                Dim destinatario As Destinatario = DaspackDALC.GetDatosDestinatario(envio.id_envio)
                If destinatario IsNot Nothing Then
                    TxtCol2.Visible = True
                    DropDownColonia.Visible = False

                    With destinatario
                        DropDownPais2.SelectedValue = .id_pais
                        txtCiudad2.Visible = True
                        txtEdo2.DataBind()
                        TxtNombre2.Text = .nombre
                        txtApellidos2.Text = .apellidos
                        txtEmpresa2.Text = .empresa
                        txtCalle2.Text = .calle
                        TxtCol2.Text = .colonia
                        txtEdo2.SelectedValue = .estadoprovincia
                        txtCiudad2.Text = .ciudad
                        TxtMpio2.Text = .municipio
                        txtTelefono2.Text = .telefono
                        txtEmail2.Text = .email
                        TxtCP2.Text = .codigo_postal
                    End With
                End If
            Else
                mensaje = "El usuario no tiene envios creados."
            End If

        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + mensaje
            Dim errorLog = New ErrorLog
            errorLog.LogError(HttpContext.Current.Request.Url.AbsoluteUri + " - " + Me.GetType().Name + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, ex.Source, id_usuario.ToString())
        End Try

    End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function createMasiveIndicium(ByVal requestSender As ObjCliente, ByVal requestEnvio As ObjEnvio, ByVal idAddBook As Integer) As createMasiveResponse
        Dim response As New createMasiveResponse
        Dim Mensaje As String
        Dim id_cliente As Integer
        Dim Crear_Envio As New Insertar_Envios
        Dim add = (New With {.Message = String.Empty, .id_dest = Integer.MinValue, .benAddError = Integer.Parse("0")})
        Dim BenefMessages As New ArrayList
        Dim shipments As New ArrayList
        Dim id_envio As Integer
        Dim db As New DaspackDataContext
        Try
            response.SendMessages = ""
            response.remAddError = 0

            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim dimensionPeso As String = CType(HttpContext.Current.Session("dimension_peso"), String)
            Dim appRelativeVirtualPath As String = CType(HttpContext.Current.Session("AppRelativeVirtualPath"), String)

            Mensaje = Crear_Envio.valida_datos_cliente(requestSender)
            If Mensaje = "OK" Then
                id_cliente = Crear_Envio.crea_cliente(requestSender)
                HttpContext.Current.Session.Add("id_cliente", id_cliente)
            Else
                response.SendMessages = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                Return response
            End If

            Dim addBook As IEnumerable(Of addressBook) = db.GetAddBookArchivo(idAddBook)
            Dim addBookClientes As List(Of addressBook) = New List(Of addressBook)(addBook.ToList())

            Dim tarifasAgencia As IEnumerable(Of TarifasAgencia) = db.GetTarifasAgencia(requestEnvio.id_agente)
            Dim tarifas As List(Of TarifasAgencia) = New List(Of TarifasAgencia)(tarifasAgencia.ToList())

            Dim result As IEnumerable(Of addBookClientes) = db.GetAddBookClientes(idAddBook)
            Dim requestBeneficiary = New ArrayList(result.ToArray)

            For Each Benef In requestBeneficiary
                Try
                    add.id_dest = Benef.id_cliente

                    requestEnvio.id_cliente = id_cliente
                    requestEnvio.id_destinatario = Benef.id_cliente
                    requestEnvio.dimension_peso = dimensionPeso
                    requestEnvio.fecha = DateTime.Now.ToString

                    Dim ab As addressBook = addBookClientes.FirstOrDefault(Function(a) a.id_destinatario = Benef.id_cliente.ToString())

                    requestEnvio.observaciones = ab.Observaciones
                    requestEnvio.contenedores = ab.Contenedor
                    requestEnvio.referencia = ab.Inventario

                    If ab.Transporte IsNot Nothing AndAlso ab.Transporte.ToUpper().Contains("FEDEX") Then
                        requestEnvio.FedExRef = ab.Guia
                    Else
                        requestEnvio.FedExRef = "0"
                        requestEnvio.observaciones = requestEnvio.observaciones + "Transporte: " + ab.Transporte + " - Guia: " + ab.Guia
                    End If

                    If ab.Servicio.ToUpper().Contains("DOM") Then
                        requestEnvio.id_tarifa_agencia = tarifas.FirstOrDefault(Function(t) t.id_tarifa = 3).id_tarifa_agencia
                    Else
                        requestEnvio.id_tarifa_agencia = tarifas.FirstOrDefault(Function(t) t.id_tarifa = 5).id_tarifa_agencia
                    End If

                    Dim cobranza As Decimal = 0
                    If IsNumeric(ab.Cobranza) Then
                        cobranza = CType(ab.Cobranza, Decimal)
                    End If

                    If ab.Cobranza IsNot Nothing Then
                        requestEnvio.valor_seguro = requestEnvio.valor_seguro + cobranza
                    End If

                    requestEnvio.id_usuario = usuarioId
                    requestEnvio.id_ruta = Nothing

                    'PreRegistor del Envío
                    Mensaje = Crear_Envio.valida_preregistro(requestEnvio)
                    If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                        id_envio = Crear_Envio.PreRegistro_Envios(requestEnvio.id_agente, requestEnvio, 0)
                    Else
                        Throw New Exception("Ocurrió un error, por favor revise los datos ---> " + Mensaje)
                    End If

                    'Registro de Envíos (Detalles)
                    Crear_Envio.Detalle_Envios(id_envio, requestEnvio, 0, "")
                    'Insertar SobreCargos
                    Crear_Envio.inserta_SobreCargos(id_envio)
                    'Inserta seguimiento
                    Dim ins_seguimiento As New seguimiento_envios
                    ins_seguimiento.insertar_seguimiento(id_envio, appRelativeVirtualPath, "", usuarioId)

                    shipments.Add(id_envio)
                Catch ex As Exception
                    add.Message = ex.Message
                    BenefMessages.Add(add)
                End Try
            Next

            response.shipments = shipments
            response.BenefMessages = BenefMessages
            Return response
        Catch ex As Exception
            response.shipments = New ArrayList()

            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Label2.Text = ""
        'Button3.Visible = False
        'Panel2.Enabled = True
        'Panel3.Enabled = True
    End Sub

    Protected Sub DropDownAgentes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgentes.SelectedIndexChanged
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_Select_datos_agente"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_agencia"
            parm1.Value = DropDownAgentes.SelectedValue
            cmd.Parameters.Add(parm1)

            connection.Open()
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()

                DropDownPais2.SelectedValue = reader.GetInt32(1) ' For masupack
                txtCiudad2.Visible = True
            End If
            reader.Close()

            Dim cmd2 As Data.IDbCommand = connection.CreateCommand()
            cmd2.CommandType = Data.CommandType.StoredProcedure
            cmd2.CommandText = "sp_Select_Envios_Pendientes_Uso"

            Dim parm2 As Data.Common.DbParameter = cmd2.CreateParameter()
            parm2.ParameterName = "@id_agencia"
            parm2.Value = DropDownAgentes.SelectedValue
            cmd2.Parameters.Add(parm2)

            Dim reader2 As Data.SqlClient.SqlDataReader = cmd2.ExecuteReader()
            If reader2.HasRows Then
                reader2.Read()
                EnviosAsignados.Text = Format(reader2.GetValue(0), "#,##;(#,##);0")

            End If
            reader2.Close()
            connection.Close()
            TxtTarifa.value = 0

            Dim idAgente As Integer = 0
            Integer.TryParse(DropDownAgentes.SelectedValue, idAgente)
            Dim Crear_Envio As New Insertar_Envios
            If Crear_Envio.AgenteCOD(idAgente) Then
                TxtSeguro.Text = ConfigurationManager.AppSettings("ValorCOD")
            Else
                TxtSeguro.Text = "0"
            End If

        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Genera Guía FedEX en otra ventana y desactiva botón
        Button4.Enabled = False
        Dim sJScript3 As String = "<script language=""Javascript"">" &
" window.open('guia_fedex.aspx?id_envio=" & TextBox2.Text & "','','width=800,height=500, toolbar=1, Scrollbars=1')" &
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)

    End Sub
    Protected Sub BtnAddCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'TxtRemit.Text = ""
        'llama ventana de administración de clientes
        Button4.Enabled = False
        Dim sJScript3 As String = "<script language=""Javascript"">" &
" window.open('admin_clientes.aspx','','width=600,height=800, toolbar=1, Scrollbars=1')" &
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)

    End Sub
    Protected Sub TxtRemit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim valida_addr As FedEx_AddressValidation = New FedEx_AddressValidation
            valida_addr.Company = txtEmpresa2.Text
            valida_addr.Address_line = txtCalle2.Text
            valida_addr.City = txtCiudad2.Text
            valida_addr.StateProvince = txtEdo2.Text
            valida_addr.zip_code = IIf(TxtCP2.Text = "", 0, TxtCP2.Text)
            valida_addr.Country = DropDownPais2.SelectedItem.ToString

            Dim identity As Integer
            identity = valida_addr.addr_input

            Dim resut_valida As Integer
            resut_valida = valida_addr.addr_validation(identity)

            Dim datos_devueltos As New ObjAddressValidation
            Dim records As Integer
            records = valida_addr.GetData(identity, datos_devueltos)

            If records = 1 Then
                txtCalle2.Text = datos_devueltos.addrStreetline_r
                'txtCalle2.BackColor = Drawing.Color.Aqua
                'txtCiudad.Text = datos_devueltos.addrCity_r
                txtEdo2.Text = datos_devueltos.addrState_r
                TxtCP2.Text = datos_devueltos.addrZipCode_r
                'txtMessage.Text = datos_devueltos.addr_changes_r
                'If datos_devueltos.addr_country_code_r <> DropDownPais2.SelectedItem.ToString Then
                '    txtMessage.Text = "La dirección encontada corresponde al código de país " & datos_devueltos.addr_country_code_r
                'End If
            End If
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub OnAdd(ByVal sender As Object, ByVal e As EventArgs)
        Dim dtgridview As DataTable = TryCast(ViewState("Data"), DataTable)

        If dtgridview Is Nothing Then
            dtgridview = New DataTable()
            dtgridview.Columns.Add("Cantidad", GetType(Integer))
            dtgridview.Columns.Add("Contenido", GetType(String))
            dtgridview.Columns.Add("Ancho", GetType(Decimal))
            dtgridview.Columns.Add("Largo", GetType(Decimal))
            dtgridview.Columns.Add("Alto", GetType(Decimal))
            dtgridview.Columns.Add("Peso", GetType(Decimal))
            dtgridview.Columns.Add("TipoClave", GetType(String))
            dtgridview.Columns.Add("Tipo", GetType(String))
            dtgridview.Columns.Add("Seguro", GetType(String))
            dtgridview.Columns.Add("ServicioSAT", GetType(String))
            dtgridview.Columns.Add("ServicioSATDesc", GetType(String))
        End If
        Dim tipoPaqueteAgregado = False
        For Each row As DataRow In dtgridview.Rows
            If ddlTiposPaquete.SelectedValue = row("TipoClave") Then
                tipoPaqueteAgregado = True
            End If
        Next row

        If String.IsNullOrWhiteSpace(txtServicioSat.Text) Then
            Label2.Text = "Debe ingresa un Codigo de Servicio SAT"
            ModalPopupExtender3.Show()
        Else
            Dim codigoSat = DaspackDALC.BuscarCodigoSat(txtServicioSat.Text)
            If codigoSat Is Nothing Then
                Label2.Text = "El codigo ingresado no existe"
                ModalPopupExtender3.Show()
            Else
                Dim dr2 As DataRow = dtgridview.NewRow()
                dr2("Cantidad") = txtContCant.Text
                dr2("Contenido") = txtContCont.Text
                dr2("Ancho") = txtContAncho.Text
                dr2("Largo") = txtContLargo.Text
                dr2("Alto") = txtContAlto.Text
                dr2("Peso") = txtContPeso.Text
                dr2("TipoClave") = ddlTiposPaquete.SelectedValue
                dr2("Tipo") = ddlTiposPaquete.SelectedItem.Text
                dr2("Seguro") = txtPESeguro.Text
                dr2("ServicioSAT") = codigoSat.codigo_servicio_id
                dr2("ServicioSATDesc") = codigoSat.descripcion

                dtgridview.Rows.Add(dr2)
                ViewState("Data") = dtgridview
                BindGridView()

                txtContCant.Text = ""
                txtContCont.Text = ""
                txtContAncho.Text = ""
                txtContLargo.Text = ""
                txtContAlto.Text = ""
                txtContPeso.Text = ""
                txtPESeguro.Text = "0"
                txtServicioSat.Text = ""
            End If
        End If
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim dt As DataTable = CType(ViewState("Data"), DataTable)
        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        ' Delete from ViewState.
        dt.Rows(index).Delete()
        ViewState("Data") = dt
        If Not IsDBNull(GridView1.DataKeys(e.RowIndex).Value) Then
            'Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
            '' Delete from Database.
            'Dim constr As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
            'Using con As SqlConnection = New SqlConnection(constr)
            '    Dim query As String = "DELETE FROM Customers WHERE CustomerId = @Id"
            '    Using cmd As SqlCommand = New SqlCommand(query)
            '        cmd.Connection = con
            '        cmd.Parameters.AddWithValue("@Id", id)
            '        con.Open()
            '        cmd.ExecuteNonQuery()
            '        con.Close()
            '    End Using
            'End Using
        End If
        BindGridView()
    End Sub
    Private Sub BindGridView()
        GridView1.DataSource = TryCast(ViewState("Data"), DataTable)
        GridView1.DataBind()
    End Sub
    Private Sub PopulateData()
        Dim constr As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString

        Dim dtgridview As DataTable = TryCast(ViewState("Data"), DataTable)
        If dtgridview Is Nothing Then
            dtgridview = New DataTable()
            dtgridview.Columns.Add("Cantidad", GetType(Integer))
            dtgridview.Columns.Add("Contenido", GetType(String))
            dtgridview.Columns.Add("Ancho", GetType(Decimal))
            dtgridview.Columns.Add("Largo", GetType(Decimal))
            dtgridview.Columns.Add("Alto", GetType(Decimal))
            dtgridview.Columns.Add("Peso", GetType(Decimal))
            dtgridview.Columns.Add("TipoClave", GetType(String))
            dtgridview.Columns.Add("Tipo", GetType(String))
            dtgridview.Columns.Add("Seguro", GetType(String))
            dtgridview.Columns.Add("ServicioSAT", GetType(String))
            dtgridview.Columns.Add("ServicioSATDesc", GetType(String))

            ViewState("Data") = dtgridview
            BindGridView()
        End If
    End Sub

    <WebMethod()>
    Public Shared Function getTemplates() As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of Templates) = db.GetTemplates

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar templates -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    <WebMethod()>
    Public Shared Function getMandatoryFields() As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of camposObligatorios) = db.GetCamposObligatorios

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar campos obligatorios -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    <WebMethod()>
    Public Shared Function readFile(ByVal fileFullName As String, ByVal idTemplate As Integer, ByVal sheetName As String, ByVal rowHead As Integer) As readFileResponse
        Dim response As New readFileResponse
        Dim listField As New ArrayList
        Try
            listField = GetFileHeadColumnName(fileFullName, sheetName, rowHead)
            response.fileFields = listField

            If idTemplate > 0 Then
                Dim db As New DaspackDataContext

                Dim result As IEnumerable(Of templateFields) = db.GetTemplateColumns(idTemplate)
                Dim resultFilter = (From r In result Select r.id_columna_archivo.ToString() + ":" + r.id_campo_obligatorio.ToString()).ToList()

                response.matchTemplate = New ArrayList(resultFilter.ToArray)
            End If

            response.responseMessage = ""

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al leer archivo -->" + ex.Message.ToString + " - " + Paso
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Private Shared Function GetFileHeadColumnName(ByVal fileName As String, ByVal sheetName As String, ByVal rowHead As Integer, Optional ByRef excelDt As DataTable = Nothing) As ArrayList
        Dim listField As New ArrayList
        Try
            Dim dt As DataTable
            dt = ReadExcelFile(fileName, sheetName, rowHead)

            excelDt = dt
            Dim rowIndex = 0

            If rowHead = 1 Then
                Dim name(dt.Columns.Count - 1) As String
                Dim i As Integer = 0
                For Each column As DataColumn In dt.Columns
                    name(i) = column.ColumnName
                    i += 1
                Next
                listField = New ArrayList(name.ToArray)
            Else
                For Each dr As DataRow In dt.Rows
                    If rowIndex >= rowHead Then
                        listField = New ArrayList(dr.ItemArray)
                        Exit For
                    End If
                    rowIndex = rowIndex + 1
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
        Return listField
    End Function

    Private Shared Function ReadExcelFile(ByVal fileName As String, ByVal sheetName As String, ByVal headRow As Integer) As DataTable
        Dim oledbConn As New OleDbConnection
        Dim path As String = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings("fullPath") + fileName)

        If IO.Path.GetExtension(path) = ".xls" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        ElseIf IO.Path.GetExtension(path) = ".csv" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"";")
        ElseIf IO.Path.GetExtension(path) = ".xlsx" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';")
        End If

        Try
            ' Open connection
            oledbConn.Open()

            '' Create OleDbCommand object and select data from worksheet Sheet1
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [" + sheetName + "$]", oledbConn)
            ' Create new OleDbDataAdapter
            Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()

            oleda.SelectCommand = cmd

            '' Create a DataSet which will hold the data extracted from the worksheet.
            Dim ds As DataSet = New DataSet()

            ' Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds, "ImportedTable")

            Return ds.Tables(0)

        Catch ex As Exception
            Throw
        End Try
        oledbConn.Close()
    End Function

    <WebMethod()>
    Public Shared Function readFileContent(ByVal fileName As String, ByVal positions As String, ByVal sheetName As String, ByVal rowHead As Integer) As genericResponse
        Dim response As New genericResponse
        Dim listField As New ArrayList
        Dim pos() As String
        Dim match() As String
        Dim matches As String
        Dim rowCount As Integer
        Dim listHeadField As ArrayList
        Dim dt As New DataTable
        Try
            ReadExcelFile(fileName, sheetName, rowHead)
            listHeadField = GetFileHeadColumnName(fileName, sheetName, rowHead, dt)

            Dim DBposition As Integer
            Dim fieldFile As String
            response.responseMessage = ""

            pos = positions.Split("|")
            rowCount = 1
            For Each dr As DataRow In dt.Rows
                Dim addBook As New addressBook
                addBook.Observaciones = ""
                addBook.Message = ""
                For Each matches In pos
                    addBook.id = rowCount

                    match = matches.Split(":")
                    DBposition = CInt(match(1))

                    fieldFile = dr.ItemArray(CInt(match(0)) - 1).ToString()
                    Select Case DBposition
                        Case 1
                            If fieldFile.Trim <> "" Then
                                addBook.Contenedor = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Contenedor Vacio, "
                            End If
                        Case 2
                            If fieldFile.Trim <> "" Then
                                addBook.Inventario = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Inventario Vacio, "
                            End If
                        Case 3
                            If fieldFile.Trim <> "" Then
                                addBook.Destinatario = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Destinatario Vacio, "
                            End If
                        Case 4
                            If fieldFile.Trim <> "" Then
                                addBook.Ciudad = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Ciudad Vacio, "
                            End If
                        Case 5
                            If fieldFile.Trim <> "" Then
                                addBook.Estado = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Estado Vacio, "
                            End If
                        Case 6
                            addBook.Direccion = fieldFile.Trim
                        Case 7
                            addBook.codigo_postal = fieldFile.Trim
                        Case 8
                            If fieldFile.Trim <> "" Then
                                addBook.Telefono = fieldFile.Trim
                            Else
                                addBook.Message = addBook.Message + "Telefono Vacio, "
                            End If
                        Case 9
                            If fieldFile.Trim <> "" Then
                                addBook.Servicio = fieldFile.Trim()
                            Else
                                addBook.Message = addBook.Message + "Servicio Vacio, "
                            End If
                        Case 10
                            If fieldFile.Trim <> "" Then
                                addBook.Cobranza = fieldFile.Trim()
                            Else
                                addBook.Message = addBook.Message + "Cobranza Vacio, "
                            End If
                        Case 11
                            If fieldFile.Trim <> "" Then
                                addBook.Transporte = fieldFile.Trim()
                            Else
                                addBook.Message = addBook.Message + "Transporte Vacio, "
                            End If
                        Case 12
                            If fieldFile.Trim <> "" Then
                                addBook.Guia = fieldFile.Trim()
                            Else
                                addBook.Message = addBook.Message + "Guia Vacio, "
                            End If
                        Case 13
                            addBook.Observaciones = addBook.Observaciones + ", " + listHeadField(CInt(match(0)) - 1).ToString() + ":" + fieldFile.Trim()
                    End Select
                Next

                If (rowCount >= 1) Then
                    listField.Add(addBook)
                    If addBook.Message <> "" Then
                        response.responseMessage = "Algunos registros del archivo contienen campos vacios."
                    End If
                End If
                rowCount = rowCount + 1
            Next
            response.responseSuccess = 1
            response.responseArray = New ArrayList(listField.ToArray)
            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al leer contenido de archivo -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try

    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function saveAddressBook(ByVal templateName As String, ByVal positions As String, ByVal addBookName As String, ByVal beneficiaries() As addressBook, ByVal pais As Integer, ByVal fileName As String) As genericResponse
        Dim response As New genericResponse
        Dim pos(), match(), matches, Mensaje As String
        Dim templateID, mandatoryField, columnFile, idAddBook As Integer
        Dim id_destinatario As Integer
        Dim idAddBookArchivo As Integer
        Try
            Dim db As New DaspackDataContext
            Dim Crear_Envio As New Insertar_Envios

            If templateName <> "" Then
                templateID = db.InsTemplate(templateName)

                pos = positions.Split("|")

                For Each matches In pos
                    match = matches.Split(":")
                    mandatoryField = CInt(match(1))
                    columnFile = CInt(match(0))
                    db.InsTemplateColumnas(templateID, columnFile, mandatoryField)
                Next
            End If

            If (beneficiaries.Length > 0) Then
                Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
                If addBookName = "" Then
                    addBookName = "LIBRO_DEFAULT_" + usuarioId.ToString()
                End If

                idAddBook = db.InsAddBook(addBookName, usuarioId)

                For Each beneficiary As addressBook In beneficiaries
                    Mensaje = Crear_Envio.valida_datos_libro_direcciones(beneficiary)

                    If beneficiary.Destinatario Is Nothing Then
                        beneficiary.Destinatario = "Desconocido"
                    End If

                    If beneficiary.Telefono Is Nothing Then
                        beneficiary.Telefono = "000000000"
                    End If

                    If beneficiary.codigo_postal Is Nothing Then
                        beneficiary.codigo_postal = "45000"
                    End If

                    If beneficiary.Servicio Is Nothing Then
                        beneficiary.Servicio = "DOM"
                    End If

                    If Mensaje <> "OK" Then
                        response.responseMessage = "Ocurrió un error, por favor revise los datos ---> " + beneficiary.Destinatario + " - " + Mensaje
                        response.responseSuccess = 0
                        Return response
                    End If

                Next

                For Each beneficiary As addressBook In beneficiaries
                    Dim destinatario As New ObjDestinatario
                    beneficiary.Estado = Regex.Replace(beneficiary.Estado, "[^\w\.@-]", "").Trim()

                    destinatario.id_pais = pais
                    destinatario.nombre = beneficiary.Destinatario
                    destinatario.direccion = beneficiary.Direccion
                    destinatario.ciudad = beneficiary.Ciudad
                    destinatario.codigo_postal = beneficiary.codigo_postal
                    destinatario.estadoprovincia = beneficiary.Estado
                    destinatario.telefono = beneficiary.Telefono
                    destinatario.apellidos = ""
                    destinatario.calle = ""
                    destinatario.codigo_pais = ""
                    destinatario.colonia = ""
                    destinatario.direccion2 = ""
                    destinatario.email = ""
                    destinatario.empresa = ""
                    destinatario.municipio = ""
                    destinatario.noexterior = 0
                    destinatario.nointerior = ""

                    id_destinatario = Crear_Envio.crea_destinatario(destinatario)

                    idAddBookArchivo = db.InsAddBookDatosArchivo(beneficiary.Contenedor, beneficiary.Inventario, beneficiary.Destinatario, beneficiary.Ciudad, beneficiary.Estado, beneficiary.Direccion, beneficiary.codigo_postal, beneficiary.Telefono, beneficiary.Servicio, beneficiary.Cobranza, beneficiary.Transporte, beneficiary.Guia, beneficiary.Observaciones)

                    db.InsAddBookClientes(idAddBook, id_destinatario, idAddBookArchivo)
                Next
            End If

            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseSuccess = 0
            response.responseMessage = "Ocurrió un error al salvar template y libro de direcciones -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try

    End Function

    <WebMethod()>
    Public Shared Function getAddBookClientes(ByVal idLibro As Integer) As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of addBookClientes) = db.GetAddBookClientes(idLibro)

            response.responseMessage = ""
            response.responseSuccess = 1
            response.responseArray = New ArrayList(result.ToArray)

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar clientes de libro -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Private Shared Sub deleteFile(ByVal fullPath As String)
        If System.IO.File.Exists(ConfigurationManager.AppSettings("fullPath") + fullPath) Then
            System.IO.File.Delete(ConfigurationManager.AppSettings("fullPath") + fullPath)
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function getAddBook() As genericResponse
        Dim response As New genericResponse
        Try
            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim db As New DaspackDataContext
            Dim result As IEnumerable(Of addBook) = db.getAddBook(usuarioId)

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar libros de direcciones -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function


    Protected Sub DropDownProveedores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownProveedores.SelectedIndexChanged
        Dim proveedor = DropDownProveedores.SelectedValue

        txtContCant.Text = ""
        txtContCont.Text = ""
        txtContAncho.Text = ""
        txtContLargo.Text = ""
        txtContAlto.Text = ""
        txtContPeso.Text = ""
        txtPESeguro.Text = "0"
        txtServicioSat.Text = ""

        ViewState("Data") = Nothing
        BindGridView()

        If proveedor = 40 Then
            contenidosDesc.Visible = True
            contenidosCampos.Visible = True
            contenidosGrid.Visible = True
            tipopaquete.Visible = True
            txtLargo.Text = 1
            txtAncho.Text = 1
            txtAlto.Text = 1
            txtPeso.Text = 1
        Else
            contenidosDesc.Visible = False
            contenidosCampos.Visible = False
            contenidosGrid.Visible = False
            tipopaquete.Visible = False
        End If
    End Sub

    Private Sub DropDownProduct_DataBound(sender As Object, e As EventArgs) Handles DropDownProduct.DataBound

        If DropDownProduct.Items.Count() > 1 Then
            DropDownProduct.SelectedIndex = 1
        End If
    End Sub


End Class


