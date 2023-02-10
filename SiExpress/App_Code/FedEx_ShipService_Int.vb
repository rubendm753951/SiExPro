Option Strict On
Imports Microsoft.VisualBasic
Imports ObjCliente
Imports ObjDestinatario
Imports ObjEnvio
'Imports WebReference
Imports ShipService
Imports System
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

' Sample code to call the FedEx International Ground Shipping Web Service
' Tested with Microsoft Visual Studio 2005 Professional Edition

'Module ShipWebServiceClient
Public Class FedEx_ShipService_Int
    Inherits System.Web.UI.Page
    Function CreateShipmentRequest(ByVal isCodShipment As Boolean, ByVal cliente As ObjCliente, ByVal destinatario As ObjDestinatario, ByVal envio As ObjEnvio) As ProcessShipmentRequest
        ' Build a ProcessShipmentRequest
        Dim request As ProcessShipmentRequest = New ProcessShipmentRequest()
        '
        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        'request.WebAuthenticationDetail.UserCredential.Key = "BFkUHfE41T5qeVwi " 'Test
        'request.WebAuthenticationDetail.UserCredential.Password = "as43kggXo6FOSpGKx8FDrBhsa" 'Test
        request.WebAuthenticationDetail.UserCredential.Key = "08QU2L0ukbUrEayj" 'Production
        request.WebAuthenticationDetail.UserCredential.Password = "q8eZrHUQeLGKhDIFNwBhNuSXU" 'Production

        '
        request.ClientDetail = New ClientDetail()
        'request.ClientDetail.AccountNumber = "510087623" ' test
        'request.ClientDetail.MeterNumber = "118508675" ' test
        request.ClientDetail.AccountNumber = "410930404" ' Production
        request.ClientDetail.MeterNumber = "102602679" ' Production

        '
        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = "***Express International Shipment v9 Request using VB.NET***" ' The client will get the same value back in the response
        '
        request.Version = New VersionId() ' WSDL version information, value is automatically set from wsdl        
        '
        SetShipmentDetails(request, envio)
        '
        SetSender(request, cliente)
        '
        SetRecipient(request, destinatario)
        '
        SetPayment(request)
        '
        SetLabelDetails(request, envio)
        '
        SetInternationalDetails(request, envio)
        '
        SetIndividualPackageLineItems(request, envio)
        '
        If (isCodShipment) Then
            SetCOD(request)
        End If
        Return request
    End Function

    Sub SetShipmentDetails(ByRef request As ProcessShipmentRequest, ByRef envio As ObjEnvio)

        Dim today As DayOfWeek = DateTime.Now.DayOfWeek
        Dim PickupDate As Date
        If today = DayOfWeek.Sunday Then
            PickupDate = DateTime.Now.AddDays(1)
        ElseIf today = DayOfWeek.Saturday Then
            PickupDate = DateTime.Now.AddDays(2)
        Else
            PickupDate = DateTime.Now
        End If
        envio.fecha_rec = PickupDate

        envio.FedExTipo = CType(envio.FedExTipo, Integer) - 2

        request.RequestedShipment = New RequestedShipment()
        request.RequestedShipment.ShipTimestamp = PickupDate 'CDate(envio.fecha_rec) ' Ship date and time
        request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP
        request.RequestedShipment.ServiceType = CType(envio.FedExTipo, ServiceType) 'ServiceType.INTERNATIONAL_PRIORITY 'ServiceType.FEDEX_GROUND ' Service types are STANDARD_OVERNIGHT, PRIORITY_OVERNIGHT, FEDEX_GROUND ...
        request.RequestedShipment.PackagingType = CType(envio.FedExPkg, PackagingType) 'PackagingType.FEDEX_ENVELOPE '
        '
        request.RequestedShipment.RateRequestTypes = New RateRequestType(0) {RateRequestType.ACCOUNT} ' Rate types requested LIST, MULTIWEIGHT, ...
        request.RequestedShipment.PackageCount = "1"
        request.RequestedShipment.PackageDetail = RequestedPackageDetailType.INDIVIDUAL_PACKAGES
        request.RequestedShipment.PackageDetailSpecified = True

    End Sub

    Sub SetSender(ByRef request As ProcessShipmentRequest, ByVal cliente As ObjCliente)
        request.RequestedShipment.Shipper = New Party() ' Sender information
        request.RequestedShipment.Shipper.Contact = New Contact()
        request.RequestedShipment.Shipper.Contact.PersonName = cliente.nombre.ToString '"Sender Name"
        request.RequestedShipment.Shipper.Contact.CompanyName = cliente.empresa.ToString  '"Sender Company Name"
        request.RequestedShipment.Shipper.Contact.PhoneNumber = cliente.telefono.ToString  '"0805522713"
        request.RequestedShipment.Shipper.Address = New Address()
        request.RequestedShipment.Shipper.Address.StreetLines = New String(0) {cliente.direccion.ToString} '{"Address Line 1"}
        request.RequestedShipment.Shipper.Address.City = cliente.ciudad.ToString '"Irving"
        request.RequestedShipment.Shipper.Address.StateOrProvinceCode = cliente.estadoprovincia.ToString '"TX"
        request.RequestedShipment.Shipper.Address.PostalCode = cliente.codigo_postal.ToString '"75063"
        request.RequestedShipment.Shipper.Address.CountryCode = cliente.codigo_pais.ToString ''"US"

    End Sub

    Sub SetRecipient(ByRef request As ProcessShipmentRequest, ByVal destinatario As ObjDestinatario)
        ' TINS is optional(Tax Payer Identification)
        'request.RequestedShipment.Recipient.Tins = New TaxpayerIdentification(0) {New TaxpayerIdentification()}
        'request.RequestedShipment.Recipient.Tins(0) = New TaxpayerIdentification()
        'request.RequestedShipment.Recipient.Tins(0).TinType = TinType.BUSINESS_NATIONAL
        'request.RequestedShipment.Recipient.Tins(0).Number = "XXX" ' Replace "XXX" with the TIN number
        '
        request.RequestedShipment.Recipient = New Party() ' Recipient information
        request.RequestedShipment.Recipient.Contact = New Contact()
        request.RequestedShipment.Recipient.Contact.PersonName = destinatario.nombre.ToString  '"Recipient Name"
        request.RequestedShipment.Recipient.Contact.CompanyName = destinatario.empresa.ToString  '"Recipient Company Name"
        request.RequestedShipment.Recipient.Contact.PhoneNumber = destinatario.telefono.ToString  '"9012637906"
        '
        request.RequestedShipment.Recipient.Address = New Address()
        request.RequestedShipment.Recipient.Address.StreetLines = New String(0) {destinatario.direccion.ToString} '{"Address Line 1"}
        request.RequestedShipment.Recipient.Address.City = destinatario.ciudad.ToString  '"Richmond"
        request.RequestedShipment.Recipient.Address.StateOrProvinceCode = destinatario.estadoprovincia.ToString  '"BC"
        request.RequestedShipment.Recipient.Address.PostalCode = destinatario.codigo_postal.ToString  '"V7C4V4"
        request.RequestedShipment.Recipient.Address.CountryCode = destinatario.codigo_pais.ToString  '"CA"
        request.RequestedShipment.Recipient.Address.Residential = True

    End Sub

    Sub SetPayment(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.ShippingChargesPayment = New Payment()
        request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER
        request.RequestedShipment.ShippingChargesPayment.Payor = New Payor()
        'request.RequestedShipment.ShippingChargesPayment.Payor.AccountNumber = "510087623" ' Test
        request.RequestedShipment.ShippingChargesPayment.Payor.AccountNumber = "410930404" ' Production
        request.RequestedShipment.ShippingChargesPayment.Payor.CountryCode = "GT"
    End Sub

    Sub SetLabelDetails(ByRef request As ProcessShipmentRequest, ByVal envio As ObjEnvio)
        request.RequestedShipment.LabelSpecification = New LabelSpecification() ' Label specification
        request.RequestedShipment.LabelSpecification.ImageType = ShippingDocumentImageType.PDF ' ShippingDocumentImageType.ZPLII
        request.RequestedShipment.LabelSpecification.ImageTypeSpecified = True

        '****** FOR PRINTING LABELS IN ZEBRA PRINTER
        If envio.impresion_FedEx = "termica" Then
            request.RequestedShipment.LabelSpecification.LabelStockType = LabelStockType.PAPER_4X6
            request.RequestedShipment.LabelSpecification.LabelStockTypeSpecified = True
        End If
        '************************************************

        request.RequestedShipment.LabelSpecification.LabelFormatType = LabelFormatType.COMMON2D  '.COMMON2D  ' COMMON2D, LABEL_DATA_ONLY
    End Sub

    Sub SetInternationalDetails(ByRef request As ProcessShipmentRequest, ByVal envio As ObjEnvio)
        request.RequestedShipment.CustomsClearanceDetail = New CustomsClearanceDetail() ' International details
        request.RequestedShipment.CustomsClearanceDetail.DutiesPayment = New Payment()
        request.RequestedShipment.CustomsClearanceDetail.DutiesPayment.PaymentType = PaymentType.SENDER
        request.RequestedShipment.CustomsClearanceDetail.DutiesPayment.Payor = New Payor()
        'request.RequestedShipment.CustomsClearanceDetail.DutiesPayment.Payor.AccountNumber = "510087623" ' Test
        request.RequestedShipment.CustomsClearanceDetail.DutiesPayment.Payor.AccountNumber = "410930404" ' Production
        request.RequestedShipment.CustomsClearanceDetail.DutiesPayment.Payor.CountryCode = "GT" '"CA"
        request.RequestedShipment.CustomsClearanceDetail.DocumentContent = InternationalDocumentContentType.NON_DOCUMENTS
        '
        request.RequestedShipment.CustomsClearanceDetail.CustomsValue = New Money()
        request.RequestedShipment.CustomsClearanceDetail.CustomsValue.Amount = CDec(envio.valor_aduana)  '100D
        request.RequestedShipment.CustomsClearanceDetail.CustomsValue.Currency = "USD"
        '
        'SetCommodityDetails(request)

        request.RequestedShipment.CustomsClearanceDetail.Commodities = New Commodity(0) {New Commodity()} ' Commodity details
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).NumberOfPieces = "1"
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).Description = envio.contenido.ToString   '"books"
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).CountryOfManufacture = "GT" '"us"

        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).Weight = New Weight()
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).Weight.Value = CType(envio.peso, Decimal) '1D
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).Weight.Units = WeightUnits.KG '.lb

        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).Quantity = "1"
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).QuantityUnits = "EA"

        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).UnitPrice = New Money()
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).UnitPrice.Amount = 1D
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).UnitPrice.Currency = "USD"

        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).CustomsValue = New Money()
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).CustomsValue.Amount = CDec(envio.valor_aduana) '100D
        request.RequestedShipment.CustomsClearanceDetail.Commodities(0).CustomsValue.Currency = "USD"

        '********** TESTING GENERATION OF COMMERCIAL INVOICE ************
        'request.RequestedShipment.ShippingDocumentSpecification = New ShippingDocumentSpecification
        'request.RequestedShipment.ShippingDocumentSpecification.ShippingDocumentTypes(0) = RequestedShippingDocumentType.COMMERCIAL_INVOICE
        'request.RequestedShipment.ShippingDocumentSpecification.CommercialInvoiceDetail = New CommercialInvoiceDetail

        '********** TESTING GENERATION OF COMMERCIAL INVOICE ************

        'request.RequestedShipment.SpecialServicesRequested.EtdDetail = New EtdDetail()
        'request.RequestedShipment.SpecialServicesRequested.EtdDetail


        'request.RequestedShipment.InternationalDetail.CommercialInvoice.Comments(0) = "sd"
        'To be used for Shipments that fall under AES Compliance
        'request.RequestedShipment.InternationalDetail.ExportDetail = New ExportDetail() 'EEI details
        'request.RequestedShipment.InternationalDetail.ExportDetail.ExportComplianceStatement = "AESX20091127123456"
    End Sub

    Sub SetIndividualPackageLineItems(ByRef request As ProcessShipmentRequest, ByVal envio As ObjEnvio)
        request.RequestedShipment.RequestedPackageLineItems = New RequestedPackageLineItem(0) {New RequestedPackageLineItem()}
        request.RequestedShipment.RequestedPackageLineItems(0).SequenceNumber = "1" ' package sequence number
        '
        request.RequestedShipment.RequestedPackageLineItems(0).Weight = New Weight() ' Package weight information
        request.RequestedShipment.RequestedPackageLineItems(0).Weight.Value = CType(envio.peso, Decimal)  '50D
        request.RequestedShipment.RequestedPackageLineItems(0).Weight.Units = WeightUnits.KG 'LB
        ' package dimensions
        request.RequestedShipment.RequestedPackageLineItems(0).Dimensions = New Dimensions()
        request.RequestedShipment.RequestedPackageLineItems(0).Dimensions.Length = CInt(envio.largo).ToString  '"12"
        request.RequestedShipment.RequestedPackageLineItems(0).Dimensions.Width = CInt(envio.ancho).ToString  '"13"
        request.RequestedShipment.RequestedPackageLineItems(0).Dimensions.Height = CInt(envio.alto).ToString  '"14"
        request.RequestedShipment.RequestedPackageLineItems(0).Dimensions.Units = LinearUnits.CM '.IN
        '
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences = New CustomerReference(2) {New CustomerReference(), New CustomerReference(), New CustomerReference()} ' Reference details
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(0).CustomerReferenceType = CustomerReferenceType.CUSTOMER_REFERENCE
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(0).Value = envio.referencia.ToString  '"GR4567892"
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(1).CustomerReferenceType = CustomerReferenceType.INVOICE_NUMBER
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(1).Value = "" '"INV4567892"
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(2).CustomerReferenceType = CustomerReferenceType.P_O_NUMBER
        request.RequestedShipment.RequestedPackageLineItems(0).CustomerReferences(2).Value = "" '"PO4567892"
    End Sub

    Sub SetCOD(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.SpecialServicesRequested = New ShipmentSpecialServicesRequested()
        request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = New ShipmentSpecialServiceType(0) {ShipmentSpecialServiceType.COD}
        '
        request.RequestedShipment.SpecialServicesRequested.CodDetail = New CodDetail()
        request.RequestedShipment.SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.ANY ' ANY, CASH, GUARANTEED_FUNDS
        '
        request.RequestedShipment.RequestedPackageLineItems(0).SpecialServicesRequested = New PackageSpecialServicesRequested()
        'request.RequestedShipment.RequestedPackageLineItems(0).SpecialServicesRequested.SpecialServiceTypes = New PackageSpecialServiceType(0) {PackageSpecialServiceType.APPOINTMENT_DELIVERY} ' Special Services types COD, HOLD_AT_LOCATION, RESIDENTIAL DELIVERY, ...
        'request.RequestedShipment.RequestedPackageLineItems(0).SpecialServicesRequested.CodCollectionAmount = New Money()
        'request.RequestedShipment.RequestedPackageLineItems(0).SpecialServicesRequested.CodCollectionAmount.Amount = 150
        'request.RequestedShipment.RequestedPackageLineItems(0).SpecialServicesRequested.CodCollectionAmount.Currency = "CAD"
    End Sub

    Function Main(ByRef cliente As ObjCliente, ByRef destinatario As ObjDestinatario, ByRef envio As ObjEnvio) As Byte()
        'look for errors
        'test(cliente, destinatario, envio)
        ' Set this to true to process a COD shipment and print a COD return Label
        Dim isCodShipment As Boolean = False 'True
        Dim request As ProcessShipmentRequest = CreateShipmentRequest(isCodShipment, cliente, destinatario, envio)
        '
        ' Log the xml request
        ' Uncomment this piece of code to log the web service request. The request will be logged in 'access.log' file under bin folder
        ' Dim tm As System.DateTime
        ' Dim requestSerializer As New Serialization.XmlSerializer(GetType(ProcessShipmentRequest))
        ' Dim file1 As FileInfo = New FileInfo("..\\access.log")
        ' Dim sWriter As StreamWriter = file1.AppendText()
        ' tm = Now
        ' sWriter.WriteLine("{0} - Request:", tm)
        ' requestSerializer.Serialize(sWriter, request)
        ' sWriter.WriteLine()
        ' sWriter.Close()
        '
        Dim service As ShipService.ShipService = New ShipService.ShipService() ' Initialize the service
        '
        'Try
        ' Call the ship web service passing in a ProcessShipmentRequest and returning a ProcessShipmentReply
        Dim reply As ProcessShipmentReply = service.processShipment(request)
        '
        ' Log the xml reply
        ' Uncomment this piece of code to log the web service reply. The reply will be logged in 'access.log' file under bin folder
        ' Dim replySerializer As New Serialization.XmlSerializer(GetType(ProcessShipmentReply))
        ' Dim rWriter As StreamWriter = file1.AppendText()
        ' tm = Now
        ' rWriter.WriteLine()
        ' rWriter.WriteLine("{0} Reply:", tm)
        ' replySerializer.Serialize(rWriter, reply)
        ' rWriter.WriteLine()
        ' rWriter.Close()
        '
        If ((Not reply.HighestSeverity = NotificationSeverityType.ERROR) And (Not reply.HighestSeverity = NotificationSeverityType.FAILURE)) Then ' check if the call was successful
            'ShowReply(reply, isCodShipment)
            Dim etiqueta As Byte()
            Dim packageDetail As CompletedPackageDetail = reply.CompletedShipmentDetail.CompletedPackageDetails(0)
            etiqueta = packageDetail.Label.Parts(0).Image

            'Get FedEx ID Tracking
            envio.FedExRef = packageDetail.TrackingIds(0).TrackingNumber
            'Update tracking nuber in d_envios table
            actualiza_referencia(envio.FedExRef.ToString, CInt(envio.id_envio))

            Return etiqueta
        Else
            Dim noti As String = ""
            For Each notification As Notification In reply.Notifications
                'Console.WriteLine(notification.Message)
                noti = noti + notification.Message & vbCrLf
            Next
            Dim etiqueta As Byte()
            Dim utf As New System.Text.UTF7Encoding()
            etiqueta = utf.GetBytes(noti)
            Return etiqueta
        End If

        'Catch e As SoapException
        '    Console.WriteLine(e.Detail.InnerText)
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        ''Console.WriteLine("Press any key to quit !")
        'Console.ReadKey()
    End Function
    Sub test(ByRef cliente As ObjCliente, ByRef destinatario As ObjDestinatario, ByVal envio As ObjEnvio)
        'Try
        '    Dim isCodShipment As Boolean = False 'True
        '    Dim request As ProcessShipmentRequest = CreateShipmentRequest(isCodShipment, cliente, destinatario, envio)
        '    Dim service As ShipService = New WebReference.ShipService() ' Initialize the service
        '    Dim reply As ProcessShipmentReply = service.processShipment(request)

        '    If ((Not reply.HighestSeverity = NotificationSeverityType.ERROR) And (Not reply.HighestSeverity = NotificationSeverityType.FAILURE)) Then ' check if the call was successful
        '        'MsgBox("todo bien llamando al ShipWebservice FedEx")
        '    Else
        '        Dim noti As String = ""
        '        For Each notification As Notification In reply.Notifications
        '            noti = noti + notification.Message & vbCrLf
        '        Next
        '        MsgBox(noti)
        '    End If
        'Catch e As SoapException
        '    MsgBox(e.Detail.InnerText)
        'Catch e As Exception
        '    MsgBox(e.Message)
        'End Try
    End Sub
    Sub ShowReply(ByRef reply As ProcessShipmentReply, ByVal isCodShipment As Boolean)
        For Each notification As Notification In reply.Notifications
            Console.WriteLine(notification.Message)
        Next
        Console.WriteLine("--- Package details ---")
        Console.WriteLine()
        ' Details for each package
        For Each packageDetail As CompletedPackageDetail In reply.CompletedShipmentDetail.CompletedPackageDetails
            'ShowTrackingDetails(packageDetail.TrackingIds)
            'ShowPackageRateDetails(packageDetail.PackageRating.PackageRateDetails)
            'ShowBarcodeDetails(packageDetail.Barcodes)
            'ShowShipmentLabels(reply.CompletedShipmentDetail, packageDetail, isCodShipment)
            Dim etiqueta As Byte()
            etiqueta = packageDetail.Label.Parts(0).Image
            If (packageDetail.Label.Parts(0).Image IsNot Nothing) Then
                ' Save outbound shipping label 
                Dim FileName As String = "D:\Hosting\5920298\html\labels\" + packageDetail.TrackingIds(0).TrackingNumber + ".pdf"
                SaveLabel(FileName, packageDetail.Label.Parts(0).Image)
            End If
        Next
        ShowPackageRouteDetails(reply.CompletedShipmentDetail.RoutingDetail)
    End Sub

    Sub ShowTrackingDetails(ByRef TrackingIds() As TrackingId)
        ' Tracking information for each package
        Console.WriteLine("Tracking details")
        If (TrackingIds IsNot Nothing) Then
            For Each trackingId As TrackingId In TrackingIds
                Console.WriteLine("Tracking # {0} Form ID {1}", trackingId.TrackingNumber, trackingId.FormId)
            Next
        End If
    End Sub

    Sub ShowPackageRateDetails(ByRef PackageRateDetails() As PackageRateDetail)
        For Each ratedPackage As PackageRateDetail In PackageRateDetails
            Console.WriteLine()
            Console.WriteLine("Rate details")
            If (ratedPackage.BillingWeight IsNot Nothing) Then
                Console.WriteLine("Billing weight {0} {1}", ratedPackage.BillingWeight.Value, ratedPackage.BillingWeight.Units)
            End If

            If (ratedPackage.BaseCharge IsNot Nothing) Then
                Console.WriteLine("Base charge {0} {1}", ratedPackage.BaseCharge.Amount, ratedPackage.BaseCharge.Currency)
            End If

            If (ratedPackage.NetCharge IsNot Nothing) Then
                Console.WriteLine("Net charge {0} {1}", ratedPackage.NetCharge.Amount, ratedPackage.NetCharge.Currency)
            End If

            If (ratedPackage.Surcharges IsNot Nothing) Then
                ' Individual surcharge for each package
                For Each surcharge As Surcharge In ratedPackage.Surcharges
                    Console.WriteLine("{0} surcharge {1} {2}", surcharge.SurchargeType, surcharge.Amount.Amount, surcharge.Amount.Currency)
                Next
            End If

            If (ratedPackage.TotalSurcharges IsNot Nothing) Then
                Console.WriteLine("Total surcharge {0} {1}", ratedPackage.TotalSurcharges.Amount, ratedPackage.TotalSurcharges.Currency)
            End If
        Next
    End Sub

    Sub ShowBarcodeDetails(ByRef Barcodes As PackageBarcodes)
        ' Barcode information for each package
        Console.WriteLine()
        Console.WriteLine("Barcode details")
        If (Barcodes IsNot Nothing) Then
            If (Barcodes.StringBarcodes IsNot Nothing) Then
                For i As Integer = 0 To Barcodes.StringBarcodes.Length - 1
                    Console.WriteLine("String barcode {0} Type {1}", Barcodes.StringBarcodes(i).Value, Barcodes.StringBarcodes(i).Type)
                Next
            End If

            If (Barcodes.BinaryBarcodes IsNot Nothing) Then
                For i As Integer = 0 To Barcodes.BinaryBarcodes.Length - 1
                    Console.WriteLine("Binary barcode Type {0}", Barcodes.BinaryBarcodes(i).Type)
                Next
            End If
        End If
    End Sub

    Sub ShowShipmentLabels(ByRef CompletedShipmentDetail As CompletedShipmentDetail, ByRef packageDetail As CompletedPackageDetail, ByVal isCodShipment As Boolean)
        If (packageDetail.Label.Parts(0).Image IsNot Nothing) Then
            ' Save outbound shipping label
            Dim FileName As String = "D:\Hosting\5920298\html\labels\" + packageDetail.TrackingIds(0).TrackingNumber + ".pdf"
            SaveLabel(FileName, packageDetail.Label.Parts(0).Image)
            'Dim FileName As String = "c:\" + packageDetail.TrackingIds(0).TrackingNumber + ".pdf"
            'SaveLabel(FileName, packageDetail.Label.Parts(0).Image)
            '' Save COD Return label
            'If (isCodShipment) Then
            '    FileName = "c:\" + CompletedShipmentDetail.CompletedPackageDetails(0).TrackingIds(0).TrackingNumber + "CR.pdf"
            '    SaveLabel(FileName, CompletedShipmentDetail.CompletedPackageDetails(0).CodReturnDetail.Label.Parts(0).Image)
            'End If
        End If
    End Sub

    Sub ShowPackageRouteDetails(ByRef RoutingDetail As ShipmentRoutingDetail)
        Console.WriteLine()
        Console.WriteLine("Routing details")
        Console.WriteLine("URSA prefix {0} suffix {1}", RoutingDetail.UrsaPrefixCode, RoutingDetail.UrsaSuffixCode)
        Console.WriteLine("Service commitment {0} Airport ID {1}", RoutingDetail.DestinationLocationId, RoutingDetail.AirportId)

        If (RoutingDetail.DeliveryDaySpecified) Then
            Console.WriteLine("Delivery day " + RoutingDetail.DeliveryDay.ToString())
        End If
        If (RoutingDetail.DeliveryDateSpecified) Then
            Console.WriteLine("Delivery date " + RoutingDetail.DeliveryDate.ToShortDateString())
        End If
        Console.WriteLine("Transit time " + RoutingDetail.TransitTime.ToString())
    End Sub

    Sub SaveLabel(ByRef LabelFileName As String, ByRef LabelBuffer() As Byte)
        ' Save label buffer to file
        Dim LabelFile As FileStream = New FileStream(LabelFileName, FileMode.Create)
        LabelFile.Write(LabelBuffer, 0, LabelBuffer.Length)
        LabelFile.Close()
        ' Display label in Acrobat
        DisplayLabel(LabelFileName)
    End Sub

    Sub DisplayLabel(ByRef LabelFileName As String)
        Dim info As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo(LabelFileName)
        info.UseShellExecute = True
        info.Verb = "open"
        System.Diagnostics.Process.Start(info)
    End Sub
    Sub actualiza_referencia(ByVal referencia As String, ByVal id_envio As Integer)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        'ejecuta SP para Insertar nuevo destinatario
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_udate_referencia_export"

        'Parámetros de entrada par actualizar
        Dim parm1 As Data.Common.DbParameter = CType(cmd.CreateParameter(), Data.Common.DbParameter)
        parm1.ParameterName = "@referencia"
        parm1.Value = referencia
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = CType(cmd.CreateParameter(), Data.Common.DbParameter)
        parm2.ParameterName = "@id_envio"
        parm2.Value = id_envio
        cmd.Parameters.Add(parm2)

  
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub
End Class

