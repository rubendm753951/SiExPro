Imports Microsoft.VisualBasic
Imports Estafeta


Public Class EstafetaWrapper

    Public Function FrecuenciaCotizador(enviosExportar As List(Of FrecuenciaCotizadorExport)) As String

        Dim estafetaService As New Frecuenciacotizador.Service()
        Dim estafetaUser = GetEstafetaUser(1)
        Dim tipoEnvio = New Estafeta.Frecuenciacotizador.TipoEnvio()
        'Dim cpOrigen = New String() {"45016"}
        'Dim cpDestino = New String() {"58060"}

        tipoEnvio.EsPaquete = True
        tipoEnvio.Alto = 2
        tipoEnvio.Ancho = 2
        tipoEnvio.Largo = 2
        tipoEnvio.Peso = 1

        For Each envioExportar As FrecuenciaCotizadorExport In enviosExportar
            Dim cpOrigen = New String() {envioExportar.CPRemitente}
            Dim cpDestino = New String() {envioExportar.CPDestinatario}

            Dim frecuenciaCotizadorResponse = estafetaService.FrecuenciaCotizador(estafetaUser.UserId, estafetaUser.UserName, estafetaUser.Password, False, True, tipoEnvio, cpOrigen, cpDestino)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonString = serializer.Serialize(frecuenciaCotizadorResponse)

        Next


        Return ""
    End Function

    Public Function Label() As String
        Dim estafetaService As New Estafeta.Label.EstafetaLabelService()
        Dim estafetaUser = GetEstafetaUser(3)

        Dim origen As New Estafeta.Label.OriginInfo()
        With origen
            .address1 = "origen addr1"
            .address2 = "origen Addr2"
            .city = "Ciudad"
            .contactName = "Contact"
            .corporateName = "Corporate"
            .customerNumber = "1234567"
            .neighborhood = "neighborhood"
            .phoneNumber = "1111111"
            .cellPhone = "0447777777777"
            .state = "Mexico"
            .zipCode = "01000"
        End With

        Dim destino As New Estafeta.Label.DestinationInfo()
        With destino
            .address1 = "Destino addr1"
            .address2 = "Destino Addr2"
            .city = "Ciudad"
            .contactName = "Cliente"
            .corporateName = "Corporate"
            .customerNumber = "1234568"
            .neighborhood = "neighborhood"
            .phoneNumber = "1111111"
            .phoneNumber = "1111111"
            .cellPhone = "0447777777777"
            .state = "Mexico"
            .zipCode = "01000"
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
            .reference = "Referencia"
            .weight = 1
            .numberOfLabels = 1
            .originZipCodeForRouting = "62250"
            .serviceTypeId = "70"
            .officeNum = "130"
            .returnDocument = True
            .serviceTypeIdDocRet = "50"
            .effectiveDate = "20200504"
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

        Return ""
    End Function

    Public Function Tracking() As String
        Dim estafetaService As New Estafeta.Tracking.Service()
        Dim estafetaUser = GetEstafetaUser(2)
        Dim searchType As New Estafeta.Tracking.SearchType()

        searchType.type = "L"

        Dim waybillList = New Estafeta.Tracking.WaybillList()
        waybillList.waybills = New String() {"8055241528464720099314"}
        waybillList.waybillType = "L"

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
            .includeSignature = True
            .includeCustomerInfo = True
            .historyConfiguration = historyConfiguration
            .filterType = filterType
        End With

        estafetaService.Timeout = 200000
        Dim queryResult = estafetaService.ExecuteQuery(estafetaUser.UserId.ToString, estafetaUser.UserName, estafetaUser.Password, searchType, searchConfiguration)

        Return ""
    End Function

    Public Function GetEstafetaUser(servicio As Integer) As EstafetaUser
        Dim estafetaUser As New EstafetaUser()
        Select Case servicio
            Case 1
                With estafetaUser
                    .UserId = 1
                    .UserName = "AdminUser"
                    .Password = ",1,B(vVi"
                End With
            Case 2
                With estafetaUser
                    .UserId = 25
                    .UserName = "Usuario1"
                    .Password = "1GCvGIu$"
                End With
            Case 3
                With estafetaUser
                    .UserId = 28
                    .UserName = "prueba1"
                    .Password = "lAbeL_K_11"
                    .CustomerNumber = "0000000"
                End With
        End Select


        Return estafetaUser
    End Function


End Class
