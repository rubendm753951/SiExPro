Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Drawing


Public Class USPS_WebTools

    'Base URL for USPS Address and Zip Code validation API.
    Private Const BaseURL As String = "http://testing.shippingapis.com/ShippingAPITest.dll"

    'Web client instance.
    Private wsClient As New WebClient()

    'User ID obtained from USPS.
    Public USPS_UserID As String = "294TABSA5545"

    Private Function GetDataFromSite(ByVal USPS_Request As String) As String
        Dim strResponse As String = ""
        Dim ResponseData() As Byte

        'Send the request to USPS.
        ResponseData = wsClient.DownloadData(USPS_Request)

        'Convert byte stream to string data.
        For Each oItem As Byte In ResponseData
            strResponse += Chr(oItem)
        Next oItem

        GetDataFromSite = strResponse

    End Function
    'Private Function GetDataFromSiteBytes(ByVal InComingData As String) As Byte()
    'Dim USPS_Request As String
    'USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=DeliveryConfirmationV3&XML=<DeliveryConfirmationV3.0Request USERID="294TABSA5545" ><Option>1</Option><ImageParameters /><FromName>John Smith</FromName><FromFirm /><FromAddress1 /><FromAddress2>475 L'Enfant Plaza, SW</FromAddress2><FromCity>Washington</FromCity><FromState>DC</FromState><FromZip5>20260</FromZip5><FromZip4 /><ToName>Joe Customer</ToName><ToFirm /><ToAddress1>STE 201</ToAddress1><ToAddress2>6060 PRIMACY PKWY</ToAddress2><ToCity>MEMPHIS</ToCity><ToState>TN</ToState><ToZip5 /><ToZip4 /><WeightInOunces>2</WeightInOunces><ServiceType>Priority</ServiceType><POZipCode /><ImageType>TIF</ImageType><LabelDate /></DeliveryConfirmationV3.0Request>"

    'Send the request to USPS.
    'GetDataFromSiteBytes = wsClient.DownloadData(USPS_Request)

    'End Function

    '-------------------------------------------------
    ' This function provides an interface to the USPS
    ' WebTools Address Validation API.
    '-------------------------------------------------
    Function AddressValidateRequest(ByVal Address1 As String, _
                                    ByVal Address2 As String, _
                                    ByVal City As String, _
                                    ByVal State As String, _
                                    ByVal Zip5 As String, _
                                    ByVal Zip4 As String) As String
        'http://production.shippingapis.com/ShippingAPITest.dll?API=Verify
        '  &XML=<AddressValidateRequest USERID="xxxxxxx"><Address ID="0"><Address1></Address1>
        '  <Address2>6406 Ivy Lane</Address2><City>Greenbelt</City><State>MD</State>
        '  <Zip5></Zip5><Zip4></Zip4></Address></AddressValidateRequest>

        Dim strUSPS As String
        strUSPS = BaseURL & "?API=Verify&XML=<AddressValidateRequest USERID=""" + USPS_UserID + """> "
        strUSPS = strUSPS & "<Address ID=""0"">"
        strUSPS = strUSPS & "<Address1>" & Address1 & "</Address1>"
        strUSPS = strUSPS & "<Address2>" & Address2 & "</Address2>"
        strUSPS = strUSPS & "<City>" & City & "</City>"
        strUSPS = strUSPS & "<State>" & State & "</State>"
        strUSPS = strUSPS & "<Zip5>" & Zip5 & "</Zip5>"
        strUSPS = strUSPS & "<Zip4>" & Zip4 & "</Zip4>"
        strUSPS = strUSPS & "</Address></AddressValidateRequest>"

        AddressValidateRequest = GetDataFromSite(strUSPS)

    End Function
    Function DeliveryConfirmationLabel(ByVal id_envio As Integer, ByVal api As String) As Byte()
        'Posteriorimente construiremos la cadena con variables provenientes de la aplicación
        Dim datos_cliente As New ObjCliente
        Dim datos_destinatario As New ObjDestinatario
        Dim datos_envio As New ObjEnvio

        datos_envio.id_envio = id_envio

        Dim seguimiento As New seguimiento_envios
        seguimiento.consulta_envio(datos_envio, datos_cliente, datos_destinatario)
        Dim zip5 As String, zip4 As String
        zip5 = Left(datos_destinatario.codigo_postal, 5)
        If Len(datos_destinatario.codigo_postal) = 10 Then
            zip4 = Right(datos_destinatario.codigo_postal, 4)
        Else
            zip4 = ""
        End If
        'Dim peso_ounces As String = (datos_envio.peso / 0.45359237 * 16).ToString
        Dim peso_ounces As String = CInt(datos_envio.peso).ToString
        Dim USPS_Request As String

        Select Case api
            Case "DeliveryConfirmation"
                USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=DeliveryConfirmationV3&XML=<DeliveryConfirmationV3.0Request USERID=""" + "294TABSA5545" + """><Option>1</Option><ImageParameters /><FromName>" & _
                datos_cliente.nombre + " " + datos_cliente.apellidos + "</FromName><FromFirm /><FromAddress1 /><FromAddress2>" + "7311 NW 12th ST" + "</FromAddress2><FromCity>" + "Miami" + "</FromCity><FromState>" + "FL" + "</FromState><FromZip5>" & _
                "33126" + "</FromZip5><FromZip4 /><ToName>" + datos_destinatario.nombre + " " + datos_destinatario.apellidos + "</ToName><ToFirm /><ToAddress1 /><ToAddress2>" + datos_destinatario.direccion + "</ToAddress2><ToCity>" + datos_destinatario.ciudad + "</ToCity><ToState>" & _
                datos_destinatario.estadoprovincia + "</ToState><ToZip5>" + zip5 + "</ToZip5><ToZip4>" + zip4 + "</ToZip4><WeightInOunces>" + peso_ounces + "</WeightInOunces><ServiceType>" + "Priority" + "</ServiceType><POZipCode /><ImageType>" + "PDF" + "</ImageType><LabelDate /></DeliveryConfirmationV3.0Request>"
            Case "SignatureConfirmation"
                USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=SignatureConfirmationV3&XML=<SignatureConfirmationV3.0Request USERID=""" + "294TABSA5545" + """><Option>1</Option><ImageParameters></ImageParameters><FromName>" + datos_cliente.nombre + " " + datos_cliente.apellidos + "</FromName>" & _
                "<FromFirm></FromFirm><FromAddress1></FromAddress1><FromAddress2>" + "7311 NW 12th ST" + "</FromAddress2><FromCity>" + "Miami" + "</FromCity><FromState>" + "FL" + "</FromState><FromZip5>" + "33126" + "</FromZip5><FromZip4></FromZip4><ToName>" + datos_destinatario.nombre + " " + datos_destinatario.apellidos + "</ToName>" & _
                "<ToFirm></ToFirm><ToAddress1></ToAddress1><ToAddress2>" + datos_destinatario.direccion + "</ToAddress2><ToCity>" + datos_destinatario.ciudad + "</ToCity><ToState>" + datos_destinatario.estadoprovincia + "</ToState><ToZip5>" + zip5 + "</ToZip5><ToZip4>" + zip4 + "</ToZip4>" & _
                "<WeightInOunces>" + peso_ounces + "</WeightInOunces><ServiceType>Priority</ServiceType><WaiverOfSignature></WaiverOfSignature><SeparateReceiptPage></SeparateReceiptPage><POZipCode></POZipCode><ImageType>PDF</ImageType><LabelDate></LabelDate><CustomerRefNo>" + datos_envio.referencia + "</CustomerRefNo><AddressServiceRequested></AddressServiceRequested><SenderName></SenderName>" & _
                "<SenderEMail></SenderEMail><RecipientName></RecipientName><RecipientEMail></RecipientEMail></SignatureConfirmationV3.0Request>"
            Case "OpenDistributePriority"
                USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=OpenDistributePriority&XML=<OpenDistributePriorityRequest USERID=""" + "294TABSA5545" + """><PermitNumber>1</PermitNumber><PermitIssuingPOZip5>21718</PermitIssuingPOZip5><FromName>M. Hurd</FromName><FromFirm>Hewlett-Packard</FromFirm><FromAddress1>Room 150</FromAddress1><FromAddress2>6600 Rockledge</FromAddress2><FromCity>Bethesda</FromCity><FromState>MD</FromState><FromZip5>21718</FromZip5><FromZip4/><POZipCode>21838</POZipCode><ToFacilityName>Fairfax Post Office</ToFacilityName><ToFacilityAddress1/><ToFacilityAddress2>10660 Page Ave</ToFacilityAddress2><ToFacilityCity>Fairfax</ToFacilityCity><ToFacilityState>VA</ToFacilityState><ToFacilityZip5>22030</ToFacilityZip5><ToFacilityZip4/><FacilityType>DDU</FacilityType><MailClassEnclosed>Other</MailClassEnclosed><MailClassOther>Free Samples</MailClassOther><WeightInPounds>22</WeightInPounds><WeightInOunces>10</WeightInOunces><ImageType>PDF</ImageType><SeparateReceiptPage>false</SeparateReceiptPage><AllowNonCleansedFacilityAddr>false</AllowNonCleansedFacilityAddr></OpenDistributePriorityRequest>"
            Case "Rate"
                USPS_Request = "http://production.shippingapis.com/ShippingAPI.dll?API=RateV4&XML=<RateV4Request USERID=""" + "294TABSA5545" + """><Revision>2</Revision><Package ID=""" + "1ST" + """><Service>FIRST CLASS</Service><FirstClassMailType>LETTER</FirstClassMailType>" & _
                "<ZipOrigination>33126</ZipOrigination><ZipDestination>" + zip5 + "</ZipDestination><Pounds>0</Pounds><Ounces>" + peso_ounces + "</Ounces><Container/><Size>REGULAR</Size><Width>15</Width><Length>30</Length><Height>15</Height><Girth>55</Girth><Value>1000</Value><SpecialServices><SpecialService>1</SpecialService></SpecialServices>" & _
                "<Machinable>true</Machinable></Package></RateV4Request>"
            Case Else
                USPS_Request = ""
        End Select

        'USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=DeliveryConfirmationV3&XML=<DeliveryConfirmationV3.0Request USERID=""" + "294TABSA5545" + """><Option>1</Option><ImageParameters /><FromName>RUBEN DOMINGUEZ </FromName><FromFirm /><FromAddress1 /><FromAddress2>7311 NW 12th ST</FromAddress2><FromCity>Miami</FromCity><FromState>FL</FromState><FromZip5>33126</FromZip5><FromZip4 /><ToName>MARG HELGENBERGER </ToName><ToFirm /><ToAddress1 /><ToAddress2>7800 Beverly Blvd</ToAddress2><ToCity>Los Angeles</ToCity><ToState>CA</ToState><ToZip5>90036</ToZip5><ToZip4>2112</ToZip4><WeightInOunces>0.0141745</WeightInOunces><ServiceType>Priority</ServiceType><POZipCode /><ImageType>TIF</ImageType><LabelDate /></DeliveryConfirmationV3.0Request>"
        'USPS_Request = "https://secure.shippingapis.com/ShippingAPI.dll?API=DeliveryConfirmationV3&XML=<DeliveryConfirmationV3.0Request USERID=""" + "294TABSA5545" + """ ><Option>1</Option><ImageParameters /><FromName>Ruben Dominguez</FromName><FromFirm /><FromAddress1 /><FromAddress2>475 L'Enfant Plaza, SW</FromAddress2><FromCity>Washington</FromCity><FromState>DC</FromState><FromZip5>20260</FromZip5><FromZip4 /><ToName>Norma Orozco</ToName><ToFirm /><ToAddress1>STE 201</ToAddress1><ToAddress2>6060 PRIMACY PKWY</ToAddress2><ToCity>MEMPHIS</ToCity><ToState>TN</ToState><ToZip5 /><ToZip4 /><WeightInOunces>2</WeightInOunces><ServiceType>Priority</ServiceType><POZipCode /><ImageType>TIF</ImageType><LabelDate /></DeliveryConfirmationV3.0Request>"
        'MsgBox(USPS_Request)

        DeliveryConfirmationLabel = wsClient.DownloadData(USPS_Request)
    End Function
    Function USPS_tracking(ByVal track_request As String) As String

        track_request = "http://production.shippingapis.com/ShippingAPITest.dll?API=TrackV2&XML=<TrackRequest USERID=""" + "294TABSA5545" + """><TrackID ID=""" + "EJ958088695US" + """></TrackID></TrackRequest>"
        USPS_tracking = wsClient.DownloadString(track_request) ''wsClient.DownloadData(USPS_Request)

    End Function
    Function USPS_tracking_live(ByVal track_request As String) As String
        track_request = "https://secure.shippingapis.com/ShippingAPI.dll?API=TrackV2&XML=<TrackRequest USERID=""" + "294TABSA5545" + """><TrackID ID=""" + "EJ958088694US" + """></TrackID></TrackRequest>"
        USPS_tracking_live = wsClient.DownloadString(track_request) ''wsClient.DownloadData(USPS_Request)
    End Function
    Function Base64ToImage(ByVal base64String As String) As Image
        '// Convert Base64 String to byte[]
        'byte[] imageBytes = Convert.FromBase64String(base64String);
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        'MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        Dim ms As New MemoryStream(imageBytes)
        '// Convert byte[] to Image
        Dim imagen As Image
        imagen = Image.FromStream(ms)

        Return imagen
    End Function
    Public Sub actualiza_referencia(ByVal referencia As String, ByVal id_envio As Integer)
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

        Dim parm3 As Data.Common.DbParameter = CType(cmd.CreateParameter(), Data.Common.DbParameter)
        parm3.ParameterName = "@proveedor"
        parm3.Value = 20 ' This USPS module
        cmd.Parameters.Add(parm3)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

End Class
