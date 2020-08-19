Imports Microsoft.VisualBasic
Imports Estafeta
Imports System.Net

Public Class EstafetaWrapper

    Public Function FrecuenciaCotizador(enviosExportar As List(Of FrecuenciaCotizadorExport)) As String

        Dim estafetaService As New Frecuenciacotizador.Service()
        Dim tipoEnvio = New Estafeta.Frecuenciacotizador.TipoEnvio()

        tipoEnvio.EsPaquete = True
        tipoEnvio.Alto = 2
        tipoEnvio.Ancho = 2
        tipoEnvio.Largo = 2
        tipoEnvio.Peso = 1

        For Each envioExportar As FrecuenciaCotizadorExport In enviosExportar
            Dim cpOrigen = New String() {envioExportar.CPRemitente}
            Dim cpDestino = New String() {envioExportar.CPDestinatario}
            Dim estafetaUser = GetEstafetaUser(1, envioExportar.CPDestinatario)

            Dim frecuenciaCotizadorResponse = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonString = serializer.Serialize(frecuenciaCotizadorResponse)

        Next

        Return ""
    End Function

    Public Function FrecuenciaCotizadorSingle(envioExportar As FrecuenciaCotizadorExport) As Estafeta.Frecuenciacotizador.Respuesta()

        Dim estafetaService As New Frecuenciacotizador.Service()
        Dim tipoEnvio = New Estafeta.Frecuenciacotizador.TipoEnvio()

        tipoEnvio.EsPaquete = True
        tipoEnvio.Alto = 2
        tipoEnvio.Ancho = 2
        tipoEnvio.Largo = 2
        tipoEnvio.Peso = 1


        Dim cpOrigen = New String() {envioExportar.CPRemitente}
        Dim cpDestino = New String() {envioExportar.CPDestinatario}
        Dim estafetaUser = GetEstafetaUser(1, envioExportar.CPDestinatario)

        Return estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)

    End Function

    Public Function Label(envio As System.Data.DataRowView, tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio, respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta()) As String
        Dim estafetaService As New Estafeta.Label.EstafetaLabelService()
        Dim estafetaUser = GetEstafetaUser(3, envio("cp_dest").ToString().Trim())

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
            .returnDocument = True
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

        If response.globalResult.resultCode = 0 Then
            DaspackDALC.InsEstafetaLabel(envio("id_envio"), response)
            DaspackDALC.InsFrecuanciaCotizador(envio("id_envio"), respuestaFrecuenciaCotizador, tipoServicio)
            Return "Envio Exportado"
        Else
            Return response.globalResult.resultDescription
        End If

    End Function

    Public Function Tracking(ByVal trackingId As String) As List(Of EstafetaTrackingStep)
        Dim estafetaService As New Estafeta.Tracking.Service()
        Dim estafetaUser = GetEstafetaUser(2, "")
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
            .includeReturnDocumentData = True
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

        Dim trackHistory = New List(Of EstafetaTrackingStep)
        Dim newStep = New EstafetaTrackingStep()
        If queryResult IsNot Nothing Then
            If queryResult.errorCode = "0" Then
                If queryResult.trackingData IsNot Nothing Then
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

    Public Function GetEstafetaUser(servicio As Integer, zip_code As String) As EstafetaUser
        Dim estafetaUser As New EstafetaUser()

        If zip_code <> "" Then
            Dim cobertura = DaspackDALC.FindZipCode(zip_code)
            If cobertura IsNot Nothing Then
                Dim estadozona = DaspackDALC.FindGombarState(cobertura.siglas_plaza)
                If estadozona IsNot Nothing Then
                    Dim usuarioZona = DaspackDALC.UserZone(estadozona.zona)
                    Dim UserId As String = ConfigurationManager.AppSettings("Estafeta.Usuario" + usuarioZona.usuario_estafeta.ToString() + ".UserId")
                    Dim UserName As String = ConfigurationManager.AppSettings("Estafeta.Usuario" + usuarioZona.usuario_estafeta.ToString() + ".UserName")
                    Dim Password As String = ConfigurationManager.AppSettings("Estafeta.Usuario" + usuarioZona.usuario_estafeta.ToString() + ".Password")
                    Dim CustomerNumber As String = ConfigurationManager.AppSettings("Estafeta.Usuario" + usuarioZona.usuario_estafeta.ToString() + ".CustomerNumber")

                    Select Case servicio
                        Case 1
                            With estafetaUser
                                .UserId = 1
                                .UserName = "AdminUser"
                                .Password = ",1,B(vVi"
                            End With
                        Case 2

                        Case 3
                            With estafetaUser
                                .UserId = 28
                                .UserName = "prueba1"
                                .Password = "lAbeL_K_11"
                                .CustomerNumber = "0000000"
                            End With
                    End Select
                End If
            End If
        Else
            With estafetaUser
                .UserId = 25
                .UserName = "Usuario1"
                .Password = "1GCvGIu$"
            End With
        End If




        Return estafetaUser
    End Function


End Class
