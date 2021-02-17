Imports Microsoft.VisualBasic
Imports Estafeta
Imports System.Net

Public Class EstafetaWrapper
    Public Const TERRESTRE = "Terrestre"
    Public Const DIA_SIGUIENTE = "Dia Sig."

    Public Function FrecuenciaCotizadorSingle(envioExportar As FrecuenciaCotizadorExport, ByRef clienteId As Integer) As FrecuenciaCotizadorRespuesta
        Dim estafetaService As New Frecuenciacotizador.Service()
        Dim tipoEnvio = New Estafeta.Frecuenciacotizador.TipoEnvio()
        Dim cliente As SiExProData.Cliente
        Dim cuentaServicios As New List(Of EstafetaCuentaServicio)
        Dim cuentaServicio As EstafetaCuentaServicio
        Dim frecuenciaCotizadorRespuesta As New FrecuenciaCotizadorRespuesta()

        tipoEnvio.EsPaquete = envioExportar.EsPaquete
        tipoEnvio.Alto = envioExportar.Alto
        tipoEnvio.Ancho = envioExportar.Ancho
        tipoEnvio.Largo = envioExportar.Largo
        tipoEnvio.Peso = envioExportar.Peso

        Dim pesoVol As Decimal = (envioExportar.Alto * envioExportar.Ancho * envioExportar.Largo) / 5000

        envioExportar.PesoVolumetrico = IIf(envioExportar.Peso > pesoVol, envioExportar.Peso, pesoVol)

        Dim cpOrigen = New String() {envioExportar.CPRemitente}
        Dim cpDestino = New String() {envioExportar.CPDestinatario}
        Dim estafetaUser As EstafetaUser = Nothing
        Dim respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta() = Nothing
        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")

        If envioExportar.PesoVolumetrico <= 10 Then
            estafetaUser = New EstafetaUser()
            Dim cuenta As String = 3
            With estafetaUser
                .UserId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta & ".FC" & ".UserId")
                .UserName = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta & ".FC" & ".UserName")
                .Password = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta & ".FC" & ".Password")
                .CustomerNumber = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta & ".FC" & ".CustomerNumber")
                .AccountId = cuenta
                .Zone = cuenta
            End With

            cliente = DaspackDALC.GetGombarSender(estafetaUser.AccountId)
            clienteId = cliente.id_cliente
            cpOrigen = New String() {cliente.codigo_postal}
            Dim tmpEstafetaUser = GetEstafetaUser("FC", envioExportar.CPDestinatario, tipoEnvio.Peso, DIA_SIGUIENTE, 0)

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = DIA_SIGUIENTE
                .Cuenta = estafetaUser.AccountId
                .Zona = tmpEstafetaUser.Zone
                .PesoVolumetrico = envioExportar.PesoVolumetrico
            End With

            cuentaServicios.Add(cuentaServicio)

            Dim tmpEstafetaUser1 = GetEstafetaUser("FC", envioExportar.CPDestinatario, tipoEnvio.Peso, TERRESTRE, 0)
            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = TERRESTRE
                .Cuenta = estafetaUser.AccountId
                .Zona = tmpEstafetaUser1.Zone
                .PesoVolumetrico = envioExportar.PesoVolumetrico
            End With

            estafetaUser.Zone = tmpEstafetaUser1.Zone

            cuentaServicios.Add(cuentaServicio)

            respuestaFrecuenciaCotizador = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)

            If log = "true" Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim estafetaRequest = New With {Key .idusuario = estafetaUser.UserId.ToString, .usuario = estafetaUser.UserName, .contra = estafetaUser.Password, .esFrecuencia = False, .esLista = True, .tipoEnvio = tipoEnvio, .datosOrigen = cpOrigen, .datosDestino = cpDestino}

                Dim jsonRequest = serializer.Serialize(estafetaRequest)
                Dim jsonResonse = serializer.Serialize(respuestaFrecuenciaCotizador)

                DaspackDALC.LogEstafetaRequestResponse("FrecuenciaCotizador", jsonRequest, jsonResonse, estafetaUser.AccountId)
            End If
        Else
            estafetaUser = GetEstafetaUser("FC", envioExportar.CPDestinatario, tipoEnvio.Peso, DIA_SIGUIENTE, 0)
            'clienteId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & estafetaUser.AccountId.ToString() & ".ClienteId")
            cliente = DaspackDALC.GetGombarSender(estafetaUser.AccountId)
            clienteId = cliente.id_cliente
            cpOrigen = New String() {cliente.codigo_postal}

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = DIA_SIGUIENTE
                .Cuenta = estafetaUser.AccountId
                .Zona = estafetaUser.Zone
                .PesoVolumetrico = envioExportar.PesoVolumetrico
            End With

            cuentaServicios.Add(cuentaServicio)

            respuestaFrecuenciaCotizador = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)
            If log = "true" Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim estafetaRequest = New With {Key .idusuario = estafetaUser.UserId.ToString, .usuario = estafetaUser.UserName, .contra = estafetaUser.Password, .esFrecuencia = False, .esLista = True, .tipoEnvio = tipoEnvio, .datosOrigen = cpOrigen, .datosDestino = cpDestino}

                Dim jsonRequest = serializer.Serialize(estafetaRequest)
                Dim jsonResonse = serializer.Serialize(respuestaFrecuenciaCotizador)

                DaspackDALC.LogEstafetaRequestResponse("FrecuenciaCotizador", jsonRequest, jsonResonse, estafetaUser.AccountId)
            End If

            Dim tipoServicios As List(Of Estafeta.Frecuenciacotizador.TipoServicio) = Nothing
            If respuestaFrecuenciaCotizador.Length > 0 Then
                If respuestaFrecuenciaCotizador(0).MensajeError = "" Then
                    tipoServicios = respuestaFrecuenciaCotizador(0).TipoServicio.Where(Function(x) x.DescripcionServicio = DIA_SIGUIENTE).ToList()
                End If
            End If

            estafetaUser = GetEstafetaUser("FC", envioExportar.CPDestinatario, tipoEnvio.Peso, TERRESTRE, 0)
            'clienteId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & estafetaUser.AccountId.ToString() & ".ClienteId")
            cliente = DaspackDALC.GetGombarSender(estafetaUser.AccountId)
            clienteId = cliente.id_cliente
            cpOrigen = New String() {cliente.codigo_postal}

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = TERRESTRE
                .Cuenta = estafetaUser.AccountId
                .Zona = estafetaUser.Zone
                .PesoVolumetrico = envioExportar.PesoVolumetrico
            End With

            cuentaServicios.Add(cuentaServicio)

            respuestaFrecuenciaCotizador = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)
            If log = "true" Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim estafetaRequest = New With {Key .idusuario = estafetaUser.UserId.ToString, .usuario = estafetaUser.UserName, .contra = estafetaUser.Password, .esFrecuencia = False, .esLista = True, .tipoEnvio = tipoEnvio, .datosOrigen = cpOrigen, .datosDestino = cpDestino}

                Dim jsonRequest = serializer.Serialize(estafetaRequest)
                Dim jsonResonse = serializer.Serialize(respuestaFrecuenciaCotizador)

                DaspackDALC.LogEstafetaRequestResponse("FrecuenciaCotizador", jsonRequest, jsonResonse, estafetaUser.AccountId)
            End If

            If respuestaFrecuenciaCotizador.Length > 0 Then
                If respuestaFrecuenciaCotizador(0).MensajeError = "" Then
                    Dim ts = respuestaFrecuenciaCotizador(0).TipoServicio.Where(Function(x) x.DescripcionServicio = TERRESTRE).ToList()
                    If tipoServicios IsNot Nothing Then
                        tipoServicios.AddRange(ts)
                    Else
                        tipoServicios = ts
                    End If
                End If
            End If

            respuestaFrecuenciaCotizador(0).TipoServicio = tipoServicios.Select(Function(x) x).ToArray()
        End If

        frecuenciaCotizadorRespuesta.Respuesta = respuestaFrecuenciaCotizador
        frecuenciaCotizadorRespuesta.CuentaServicios = cuentaServicios

        Return frecuenciaCotizadorRespuesta

    End Function

    Public Function Label(envio As System.Data.DataRowView, tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio, respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta()) As String
        Dim estafetaService As New Estafeta.Label.EstafetaLabelService()
        Dim envioEncontrado = DaspackDALC.GetDatosEnvio(envio("id_envio").ToString())
        Dim servicioSeleccionado = DaspackDALC.GetServicioSelecionado(envio("id_envio").ToString())
        Dim estafetaUser = GetEstafetaUser("Label", envio("cp_dest").ToString().Trim(), envioEncontrado.peso, servicioSeleccionado.DescripcionServicio, 0)

        Dim origen As New Estafeta.Label.OriginInfo()
        With origen
            .address1 = envio("calle_remit")
            .address2 = "N/A"
            .city = envio("ciudad_remit")
            .contactName = envio("nombre_remit")
            .corporateName = IIf(String.IsNullOrEmpty(envio("empresa_remit")), "N/A", Strings.Left(envio("empresa_remit"), 50))
            .customerNumber = "0000000"
            .neighborhood = "N/A"
            .phoneNumber = envio("tel_remit")
            .cellPhone = ""
            .state = envio("estadoprovincia_remit")
            .zipCode = envio("cp_remit")
        End With

        Dim destino As New Estafeta.Label.DestinationInfo()
        With destino
            .address1 = envio("calle_dest")
            .address2 = "N/A"
            .city = envio("ciudad_dest")
            .contactName = envio("nombre_dest")
            .corporateName = IIf(String.IsNullOrEmpty(envio("empresa_dest")), "N/A", Strings.Left(envio("empresa_dest"), 50))
            .customerNumber = "0000000"
            .neighborhood = "N/A"
            .phoneNumber = envio("tel_dest")
            .cellPhone = ""
            .state = envio("estadoprovincia_dest")
            .zipCode = envio("cp_dest")
        End With

        Dim descripcionLista As New Estafeta.Label.LabelDescriptionList()
        With descripcionLista
            .originInfo = origen
            .destinationInfo = destino
            .aditionalInfo = "Informacion adicional"
            .content = "Contenido"
            .costCenter = "CCtos"
            .deliveryToEstafetaOffice = False
            .destinationCountryId = "MX"
            'Tipo de envio 1=SOBRE 4=PAQUETE
            .parcelTypeId = 4
            .reference = envio("id_envio")
            .weight = 1
            .numberOfLabels = 1
            .originZipCodeForRouting = envio("cp_dest")
            .serviceTypeId = "70"
            .officeNum = "130"
            .returnDocument = False
            .serviceTypeIdDocRet = "50"
            .effectiveDate = "20200604"
            .contentDescription = "Descripcion del contenido del paquete"
        End With

        Dim listArray As New List(Of Label.LabelDescriptionList)
        listArray.Add(descripcionLista)

        Dim estafetaLabelRequest As New Estafeta.Label.EstafetaLabelRequest()
        With estafetaLabelRequest
            .customerNumber = estafetaUser.CustomerNumber
            .login = estafetaUser.UserName
            .password = estafetaUser.Password
            .suscriberId = estafetaUser.UserId
            .quadrant = 0
            .paperType = 2
            .labelDescriptionList = listArray.ToArray()
        End With

        Dim response = estafetaService.createLabel(estafetaLabelRequest)
        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")
        If log = "true" Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonRequest = serializer.Serialize(estafetaLabelRequest)
            Dim jsonResonse = serializer.Serialize(response)
            Dim imagenBase64 = Convert.ToBase64String(response.labelPDF, 0, response.labelPDF.Length)

            DaspackDALC.LogEstafetaRequestResponse("CreateLabel", jsonRequest, jsonResonse, estafetaUser.AccountId, imagenBase64)
        End If

        If response.globalResult.resultCode = 0 Then
            DaspackDALC.InsEstafetaLabel(envio("id_envio"), response)
            DaspackDALC.InsFrecuanciaCotizador(envio("id_envio"), respuestaFrecuenciaCotizador, tipoServicio)
            Return "Envio Exportado"
        Else
            Return response.globalResult.resultDescription
        End If

    End Function

    Public Function Label(envio As ObjEnvio, cliente As ObjCliente, destinatario As ObjDestinatario, tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio, respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta(), cuenta As Integer) As String
        Dim estafetaService As New Estafeta.Label.EstafetaLabelService()
        Dim estafetaUser = GetEstafetaUser("Label", destinatario.codigo_postal, envio.peso, tipoServicio.DescripcionServicio, cuenta)

        Dim origen As New Estafeta.Label.OriginInfo()
        With origen
            .address1 = cliente.calle
            .address2 = "N/A"
            .city = cliente.ciudad
            .contactName = cliente.nombre
            .corporateName = IIf(String.IsNullOrEmpty(cliente.empresa), "N/A", Strings.Left(cliente.empresa, 50))
            .customerNumber = "0000000"
            .neighborhood = "N/A"
            .phoneNumber = cliente.telefono
            .cellPhone = ""
            .state = cliente.estadoprovincia
            .zipCode = cliente.codigo_postal
        End With

        Dim destino As New Estafeta.Label.DestinationInfo()
        With destino
            .address1 = destinatario.calle
            .address2 = "N/A"
            .city = destinatario.ciudad
            .contactName = destinatario.nombre
            .corporateName = IIf(String.IsNullOrEmpty(destinatario.empresa), "N/A", Strings.Left(destinatario.empresa, 50))
            .customerNumber = "0000000"
            .neighborhood = "N/A"
            .phoneNumber = destinatario.telefono
            .cellPhone = ""
            .state = destinatario.estadoprovincia
            .zipCode = destinatario.codigo_postal
        End With

        Dim descripcionLista As New Estafeta.Label.LabelDescriptionList()
        With descripcionLista
            .originInfo = origen
            .destinationInfo = destino
            .aditionalInfo = "Informacion adicional"
            .content = "Contenido"
            .costCenter = "CCtos"
            .deliveryToEstafetaOffice = False
            .destinationCountryId = "MX"
            'Tipo de envio 1=SOBRE 4=PAQUETE
            .parcelTypeId = 4
            .reference = envio.id_envio
            .weight = 1
            .numberOfLabels = 1
            .originZipCodeForRouting = destinatario.codigo_postal
            .serviceTypeId = "70"
            .officeNum = "130"
            .returnDocument = False
            .serviceTypeIdDocRet = "50"
            .effectiveDate = "20200604"
            .contentDescription = "Descripcion del contenido del paquete"
        End With

        Dim listArray As New List(Of Label.LabelDescriptionList)
        listArray.Add(descripcionLista)

        Dim estafetaLabelRequest As New Estafeta.Label.EstafetaLabelRequest()
        With estafetaLabelRequest
            .customerNumber = estafetaUser.CustomerNumber
            .login = estafetaUser.UserName
            .password = estafetaUser.Password
            .suscriberId = estafetaUser.UserId
            .quadrant = 0
            .paperType = 2
            .labelDescriptionList = listArray.ToArray()
        End With

        Dim response = estafetaService.createLabel(estafetaLabelRequest)

        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")

        If log = "true" Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonRequest = serializer.Serialize(estafetaLabelRequest)
            Dim jsonResonse = serializer.Serialize(response)
            Dim imagenBase64 = Convert.ToBase64String(response.labelPDF, 0, response.labelPDF.Length)

            DaspackDALC.LogEstafetaRequestResponse("CreateLabel", jsonRequest, jsonResonse, estafetaUser.AccountId, imagenBase64)
        End If

        If response.globalResult.resultCode = 0 Then
            DaspackDALC.InsEstafetaLabel(envio.id_envio, response)
            DaspackDALC.InsFrecuanciaCotizador(envio.id_envio, respuestaFrecuenciaCotizador, tipoServicio)
            Return "Envio Exportado"
        Else
            Return response.globalResult.resultDescription
        End If

    End Function

    Public Function Tracking(ByVal trackingId As String) As List(Of EstafetaTrackingStep)
        Dim estafetaService As New Estafeta.Tracking.Service()
        Dim envio = DaspackDALC.GetDatosEnvioByReferenciaFedex(trackingId)
        Dim servicioSeleccionado = DaspackDALC.GetServicioSelecionado(envio.id_envio)
        Dim destinatario = DaspackDALC.GetDatosDestinatario(envio.id_envio)

        Dim estafetaUser = GetEstafetaUser("Tracking", destinatario.codigo_postal, envio.peso, servicioSeleccionado.DescripcionServicio, 0)

        Dim searchType As New Estafeta.Tracking.SearchType()

        searchType.type = "L"

        Dim waybillList = New Estafeta.Tracking.WaybillList()
        waybillList.waybills = New String() {trackingId}
        waybillList.waybillType = "G"

        searchType.waybillList = waybillList

        Dim searchConfiguration As New Estafeta.Tracking.SearchConfiguration()
        Dim historyConfiguration As New Estafeta.Tracking.HistoryConfiguration()

        With historyConfiguration
            .includeHistory = True
            .historyType = "ALL"
        End With

        Dim filterType As New Estafeta.Tracking.Filter()
        With filterType
            .filterInformation = False
        End With


        With searchConfiguration
            .includeDimensions = True
            .includeWaybillReplaceData = True
            .includeReturnDocumentData = False
            .includeMultipleServiceData = False
            .includeInternationalData = False
            .includeSignature = False
            .includeCustomerInfo = True
            .historyConfiguration = historyConfiguration
            .filterType = filterType
        End With

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        estafetaService.Timeout = 200000
        Dim queryResult = estafetaService.ExecuteQuery(estafetaUser.UserId.ToString, estafetaUser.UserName, estafetaUser.Password, searchType, searchConfiguration)

        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")
        If log = "true" Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim estafetaRequest = New With {Key .subscriberId = estafetaUser.UserId.ToString, .login = estafetaUser.UserName, .password = estafetaUser.Password, searchType, searchConfiguration}

            Dim jsonRequest = serializer.Serialize(estafetaRequest)
            Dim jsonResonse = serializer.Serialize(queryResult)

            DaspackDALC.LogEstafetaRequestResponse("Tracking", jsonRequest, jsonResonse, estafetaUser.AccountId)
        End If

        Dim trackHistory = New List(Of EstafetaTrackingStep)
        Dim newStep = New EstafetaTrackingStep()
        If queryResult IsNot Nothing Then
            If queryResult.errorCode = "0" Then
                If queryResult.trackingData IsNot Nothing And queryResult.trackingData.Length > 0 Then
                    If queryResult.trackingData(0).deliveryData IsNot Nothing Then
                        With newStep
                            .EventTime = queryResult.trackingData(0).deliveryData.deliveryDateTime
                            .EventDescription = "Entregado - " + queryResult.trackingData(0).deliveryData.receiverName + " " + queryResult.trackingData(0).deliveryData.destinationName
                        End With
                        trackHistory.Add(newStep)
                    End If

                    For Each historyStep As Estafeta.Tracking.History In queryResult.trackingData(0).history
                        newStep = New EstafetaTrackingStep()
                        With newStep
                            .EventTime = historyStep.eventDateTime
                            .EventDescription = historyStep.eventDescriptionSPA
                            .EventException = historyStep.exceptionCodeDetails
                            .EventPlace = historyStep.eventPlaceName
                        End With
                        trackHistory.Add(newStep)
                    Next
                Else
                    With newStep
                        .EventDescription = "Aun no hay registros de este envio"
                    End With
                    trackHistory.Add(newStep)
                End If
            Else
                With newStep
                    .EventDescription = queryResult.errorCodeDescriptionSPA
                End With
                trackHistory.Add(newStep)
            End If
        Else
            With newStep
                .EventDescription = "Envio No Encontrado"
            End With
            trackHistory.Add(newStep)
        End If

        Return trackHistory
    End Function

    Public Function GetNumeroCuenta(servicio As String, zip_code As String, peso As Double, servicioEstafeta As String) As Integer
        Dim numeroCuenta As Integer = 0

        Dim cobertura = DaspackDALC.FindZipCode(zip_code)
        If cobertura IsNot Nothing Then
            Dim estadozona = DaspackDALC.FindGombarState(cobertura.siglas_plaza)
            If estadozona IsNot Nothing Then
                Dim cuentaZona = DaspackDALC.UserZone(estadozona.zona)
                If cuentaZona IsNot Nothing Then
                    numeroCuenta = cuentaZona.usuario_estafeta
                End If
            End If
        End If

        Return numeroCuenta
    End Function

    Public Function GetEstafetaUser(servicio As String, zip_code As String, peso As Double, servicioEstafeta As String, cuenta As Integer) As EstafetaUser
        Dim estafetaUser As New EstafetaUser()
        Dim UserId As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim CustomerNumber As String = ""

        If cuenta > 0 Then
            With estafetaUser
                .UserId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".UserId")
                .UserName = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".UserName")
                .Password = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".Password")
                .CustomerNumber = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".CustomerNumber")
                .AccountId = cuenta
                .Zone = 0
            End With
        Else
            Dim cobertura = DaspackDALC.FindZipCode(zip_code)
            If cobertura IsNot Nothing Then
                Dim estadozona = DaspackDALC.FindGombarState(cobertura.siglas_plaza)
                If estadozona IsNot Nothing Then
                    Dim cuentaZona = DaspackDALC.UserZone(estadozona.zona)
                    If cuentaZona IsNot Nothing Then
                        With estafetaUser
                            .UserId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuentaZona.usuario_estafeta.ToString() & "." & servicio & ".UserId")
                            .UserName = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuentaZona.usuario_estafeta.ToString() & "." & servicio & ".UserName")
                            .Password = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuentaZona.usuario_estafeta.ToString() & "." & servicio & ".Password")
                            .CustomerNumber = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuentaZona.usuario_estafeta.ToString() & "." & servicio & ".CustomerNumber")
                            .AccountId = cuentaZona.usuario_estafeta
                            .Zone = estadozona.zona
                        End With
                    End If
                End If
            End If
        End If

        Return estafetaUser
    End Function

End Class
