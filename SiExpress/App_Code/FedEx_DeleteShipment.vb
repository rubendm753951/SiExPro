Imports Microsoft.VisualBasic
Imports DeleteShipment
Imports System.Web.Services.Protocols
Imports System.IO

Public Class FedEx_DeleteShipment
    Public Function Main(ByVal TrackingNumber As String) As String
        Dim request As DeleteShipmentRequest = CreateDeleteShipmentRequest(TrackingNumber)
        '
        ' Log the xml request
        ' Uncomment this piece of code to log the web service request. The request will be logged in 'access.log' file under bin folder
        ' Dim tm As System.DateTime
        ' Dim requestSerializer As New Serialization.XmlSerializer(GetType(DeleteShipmentRequest))
        ' Dim file1 As FileInfo = New FileInfo("..\\access.log")
        ' Dim sWriter As StreamWriter = file1.AppendText()
        ' tm = Now
        ' sWriter.WriteLine("{0} - Request:", tm)
        ' requestSerializer.Serialize(sWriter, request)
        ' sWriter.WriteLine()
        ' sWriter.Close()
        '
        Dim service As DeleteShipment.ShipService = New DeleteShipment.ShipService() ' Initialize the service
        '
        'Try
        ' Call to the ship web service passing in a DeleteShipmentRequest and returning a ShipmentReply
        Dim reply As ShipmentReply = service.deleteShipment(request)
        '
        ' Log the xml reply
        ' Uncomment this piece of code to log the web service reply. The reply will be logged in 'access.log' file under bin folder
        ' Dim replySerializer As New Serialization.XmlSerializer(GetType(ShipmentReply))
        ' Dim rWriter As StreamWriter = file1.AppendText()
        ' tm = Now
        ' rWriter.WriteLine()
        ' rWriter.WriteLine("{0} Reply:", tm)
        ' replySerializer.Serialize(rWriter, reply)
        ' rWriter.WriteLine()
        ' rWriter.Close()
        '
        If (reply.HighestSeverity = NotificationSeverityType.SUCCESS) Then
            Return ("El envío " & TrackingNumber & " Se ha eliminado correctamente en la base de datos de FedEx")
        Else
            Dim nota As String : nota = ""
            For Each notification As Notification In reply.Notifications
                nota = (notification.Message).ToString & vbCrLf
            Next
            Return nota
        End If
        'Catch e As SoapException
        '    Console.WriteLine(e.Detail.InnerText)
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Console.WriteLine("Press any key to quit !")
        'Console.ReadKey()
    End Function

    Function CreateDeleteShipmentRequest(ByVal TrackingNumber As String) As DeleteShipmentRequest
        ' Build a DeleteShipmentRequest
        Dim request As DeleteShipmentRequest = New DeleteShipmentRequest()
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
        request.TransactionDetail.CustomerTransactionId = "***Delete Shipment v9 Request using VB.NET***" ' The client will get the same value back in the response
        '
        request.Version = New VersionId() ' WSDL version information, value is automatically set from wsdl
        '
        request.TrackingId = New TrackingId()
        request.TrackingId.TrackingIdType = TrackingIdType.EXPRESS ' Replace with desired tracking id type
        request.TrackingId.TrackingIdTypeSpecified = True
        request.TrackingId.TrackingNumber = TrackingNumber ' Replace "XXX" with the tracking number to delete
        '
        request.DeletionControl = DeletionControlType.DELETE_ALL_PACKAGES
        Return request
    End Function


End Class
