Imports Microsoft.VisualBasic
Imports Estafeta
Imports System.Net

Public Class EstafetaWrapper
    Public Const TERRESTRE = "Terrestre"
    Public Const DIA_SIGUIENTE = "Dia Sig."
    Public Const LTL = "LTL"

    Public Function FrecuenciaCotizadorSingle(envioExportar As FrecuenciaCotizadorExport, estafetaPrecios As EstafetaPrecio) As FrecuenciaCotizadorRespuesta
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
        Dim tipoServicios As List(Of Estafeta.Frecuenciacotizador.TipoServicio) = Nothing


        If estafetaPrecios.CuentaLtl > 0 Then
            Dim tmpEstafetaUserTarimas = GetEstafetaUser("FC", envioExportar.CPDestinatario, estafetaPrecios.CuentaLtl)

            cliente = DaspackDALC.GetGombarSender(tmpEstafetaUserTarimas.AccountId)
            cpOrigen = New String() {cliente.codigo_postal}

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = LTL
                .Cuenta = estafetaPrecios.CuentaLtl
                .Zona = estafetaPrecios.ZonaLtl
                .PesoVolumetrico = envioExportar.PesoVolumetrico
                .Ocurre = estafetaPrecios.Ocurre
            End With

            cuentaServicios.Add(cuentaServicio)

            respuestaFrecuenciaCotizador = estafetaService.FrecuenciaCotizador(tmpEstafetaUserTarimas.UserId, tmpEstafetaUserTarimas.UserName, tmpEstafetaUserTarimas.Password, False, True, tipoEnvio, cpOrigen, cpDestino)
            If log = "true" Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim estafetaRequest = New With {Key .idusuario = tmpEstafetaUserTarimas.UserId.ToString, .usuario = tmpEstafetaUserTarimas.UserName, .contra = tmpEstafetaUserTarimas.Password, .esFrecuencia = False, .esLista = True, .tipoEnvio = tipoEnvio, .datosOrigen = cpOrigen, .datosDestino = cpDestino}

                Dim jsonRequest = serializer.Serialize(estafetaRequest)
                Dim jsonResonse = serializer.Serialize(respuestaFrecuenciaCotizador)

                DaspackDALC.LogEstafetaRequestResponse("FrecuenciaCotizador", jsonRequest, jsonResonse, tmpEstafetaUserTarimas.AccountId, 1)
            End If

            If respuestaFrecuenciaCotizador.Length > 0 Then
                If respuestaFrecuenciaCotizador(0).MensajeError = "" Then
                    tipoServicios = respuestaFrecuenciaCotizador(0).TipoServicio.Where(Function(x) x.DescripcionServicio = LTL).ToList()
                End If
            End If
        End If

        If estafetaPrecios.Cuenta > 0 Then
            estafetaUser = GetEstafetaUser("FC", envioExportar.CPDestinatario, estafetaPrecios.Cuenta)
            cliente = DaspackDALC.GetGombarSender(estafetaUser.AccountId)
            cpOrigen = New String() {cliente.codigo_postal}

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = DIA_SIGUIENTE
                .Cuenta = estafetaPrecios.Cuenta
                .Zona = estafetaPrecios.Zona
                .PesoVolumetrico = envioExportar.PesoVolumetrico
                .Ocurre = estafetaPrecios.Ocurre
            End With

            cuentaServicios.Add(cuentaServicio)

            cuentaServicio = New EstafetaCuentaServicio()
            With cuentaServicio
                .Cliente = cliente
                .Servicio = TERRESTRE
                .Cuenta = estafetaPrecios.Cuenta
                .Zona = estafetaPrecios.Zona
                .PesoVolumetrico = envioExportar.PesoVolumetrico
                .Ocurre = estafetaPrecios.Ocurre
            End With

            cuentaServicios.Add(cuentaServicio)

            respuestaFrecuenciaCotizador = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)
            If log = "true" Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim estafetaRequest = New With {Key .idusuario = estafetaUser.UserId.ToString, .usuario = estafetaUser.UserName, .contra = estafetaUser.Password, .esFrecuencia = False, .esLista = True, .tipoEnvio = tipoEnvio, .datosOrigen = cpOrigen, .datosDestino = cpDestino}

                Dim jsonRequest = serializer.Serialize(estafetaRequest)
                Dim jsonResonse = serializer.Serialize(respuestaFrecuenciaCotizador)

                DaspackDALC.LogEstafetaRequestResponse("FrecuenciaCotizador", jsonRequest, jsonResonse, estafetaUser.AccountId, 1)
            End If

            If respuestaFrecuenciaCotizador.Length > 0 Then
                If respuestaFrecuenciaCotizador(0).MensajeError = "" Then
                    Dim ts = respuestaFrecuenciaCotizador(0).TipoServicio.Where(Function(x) x.DescripcionServicio = TERRESTRE Or x.DescripcionServicio = DIA_SIGUIENTE).ToList()
                    If tipoServicios IsNot Nothing Then
                        tipoServicios.AddRange(ts)
                    Else
                        tipoServicios = ts
                    End If
                End If
            End If
        End If

        If tipoServicios IsNot Nothing AndAlso tipoServicios.Count > 0 Then
            respuestaFrecuenciaCotizador(0).TipoServicio = tipoServicios.Select(Function(x) x).ToArray()
        End If

        frecuenciaCotizadorRespuesta.Respuesta = respuestaFrecuenciaCotizador
        frecuenciaCotizadorRespuesta.CuentaServicios = cuentaServicios

        Return frecuenciaCotizadorRespuesta

    End Function

    Public Function Label(envio As ObjEnvio, cliente As ObjCliente, destinatario As ObjDestinatario, tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio, respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta(), cuenta As Integer, envios() As Integer, tipoImpresion As Integer) As String
        Dim estafetaService As New Estafeta.Label.EstafetaLabelService()
        Dim estafetaUser = GetEstafetaUser("Label", destinatario.codigo_postal, cuenta)

        Dim origen As New Estafeta.Label.OriginInfo()
        With origen
            .address1 = cliente.calle
            .address2 = cliente.colonia
            .city = cliente.ciudad
            .contactName = cliente.nombre
            .corporateName = IIf(String.IsNullOrEmpty(cliente.empresa), "N/A", Strings.Left(cliente.empresa, 50))
            .customerNumber = "0000000"
            .neighborhood = cliente.municipio
            .phoneNumber = cliente.telefono
            .cellPhone = ""
            .state = cliente.estadoprovincia
            .zipCode = cliente.codigo_postal
        End With

        Dim direccion = destinatario.calle
        If destinatario.calle.Length > 30 Then
            direccion = destinatario.calle.Substring(0, 30)
        End If

        Dim nombre = destinatario.nombre
        If destinatario.nombre.Length > 30 Then
            nombre = destinatario.nombre.Substring(0, 30)
        End If

        Dim municipio = destinatario.municipio
        If destinatario.municipio.Length > 50 Then
            municipio = destinatario.municipio.Substring(0, 50)
        End If

        Dim colonia = destinatario.colonia
        If destinatario.colonia.Length > 30 Then
            colonia = destinatario.colonia.Substring(0, 30)
        End If

        Dim destino As New Estafeta.Label.DestinationInfo()
        With destino
            .address1 = direccion
            .address2 = colonia
            .city = destinatario.ciudad
            .contactName = nombre
            .corporateName = IIf(String.IsNullOrEmpty(destinatario.empresa), "N/A", Strings.Left(destinatario.empresa, 50))
            .customerNumber = "0000000"
            .neighborhood = municipio
            .phoneNumber = destinatario.telefono
            .cellPhone = ""
            .state = destinatario.estadoprovincia
            .zipCode = destinatario.codigo_postal
        End With


        Dim serviceTypeId = "70"
        'Dim paperType = 2

        If cuenta = 3 Then
            serviceTypeId = ConfigurationManager.AppSettings("Estafeta.Cuenta3.ServiceType")
        End If
        If tipoServicio.DescripcionServicio = "Dia Sig." Then
            serviceTypeId = "60"
            '   paperType = 1
        End If

        If tipoServicio.DescripcionServicio = "LTL" Then
            serviceTypeId = "L0"
        End If

        Dim pesoVol As Decimal = (envio.alto * envio.ancho * envio.largo) / 5000

        envio.peso = IIf(envio.peso > pesoVol, envio.peso, pesoVol)
        Dim today As DateTime = DateTime.Today
        Dim effectiveDate As DateTime = today.AddDays(5)
        Dim effectiveDateStr = effectiveDate.ToString("yyyyMMdd")


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
            .weight = Math.Round(envio.peso, 2)
            .numberOfLabels = envios.Length
            .originZipCodeForRouting = destinatario.codigo_postal
            .serviceTypeId = serviceTypeId
            .officeNum = ConfigurationManager.AppSettings("Estafeta.OfficeNum")
            .returnDocument = False
            .serviceTypeIdDocRet = "50"
            .effectiveDate = effectiveDateStr
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
            .paperType = tipoImpresion
            .labelDescriptionList = listArray.ToArray()
        End With

        Dim response = estafetaService.createLabel(estafetaLabelRequest)

        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")

        If log = "true" Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonRequest = serializer.Serialize(estafetaLabelRequest)
            Dim jsonResonse = serializer.Serialize(response)
            Dim imagenBase64 = ""
            If response.labelPDF IsNot Nothing Then
                imagenBase64 = Convert.ToBase64String(response.labelPDF, 0, response.labelPDF.Length)
            End If

            DaspackDALC.LogEstafetaRequestResponse("CreateLabel", jsonRequest, jsonResonse, estafetaUser.AccountId, 1, imagenBase64)
        End If

        Dim referenciasFedex() As String = response.labelResultList(0).resultDescription.Split("|")
        Dim message As String = ""
        Dim identificador = System.Guid.NewGuid

        For index = 0 To envios.Length - 1
            DaspackDALC.InsFrecuanciaCotizador(envios(index), respuestaFrecuenciaCotizador, tipoServicio)
            If response.globalResult.resultCode = 0 Then
                Dim referenciaFedex As String = ""

                If envios.Length = referenciasFedex.Length Then
                    referenciaFedex = referenciasFedex(index)
                Else
                    referenciaFedex = response.labelResultList(0).resultDescription
                End If

                DaspackDALC.InsEstafetaLabel(envios(index), response.labelPDF, referenciaFedex, response.globalResult.resultDescription, identificador)

                message = "Envio Exportado"
            Else
                Dim observaciones = IIf(response.globalResult.resultSpanishDescription <> "", response.globalResult.resultSpanishDescription, response.globalResult.resultDescription)

                If response.labelResultList.Count > 0 Then
                    observaciones = observaciones + " " + response.labelResultList(0).resultSpanishDescription
                End If

                Dim cancela As New seguimiento_envios
                cancela.insertar_seguimiento(envios(index), "~/admin_pages/modif_cancel.aspx", observaciones, envio.id_usuario)

                message = observaciones
            End If
        Next

        Return message

    End Function

    Public Function Tracking(ByVal trackingId As String) As List(Of EstafetaTrackingStep)
        Dim estafetaService As New Estafeta.Tracking.Service()
        Dim envio = DaspackDALC.GetDatosEnvioByReferenciaFedex(trackingId)
        Dim servicioSeleccionado = DaspackDALC.GetServicioSelecionado(envio.id_envio)
        Dim destinatario = DaspackDALC.GetDatosDestinatario(envio.id_envio)

        Dim estafetaUser = GetEstafetaUser("Tracking", destinatario.codigo_postal, Nothing)

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

            DaspackDALC.LogEstafetaRequestResponse("Tracking", jsonRequest, jsonResonse, estafetaUser.AccountId, 1)
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


    Public Function GetEstafetaUser(servicio As String, zip_code As String, cuenta As Integer) As EstafetaUser
        Dim estafetaUser As New EstafetaUser()

        With estafetaUser
            .UserId = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".UserId")
            .UserName = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".UserName")
            .Password = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".Password")
            .CustomerNumber = ConfigurationManager.AppSettings("Estafeta.Cuenta" & cuenta.ToString() & "." & servicio & ".CustomerNumber")
            .AccountId = cuenta
        End With

        Return estafetaUser
    End Function

End Class
