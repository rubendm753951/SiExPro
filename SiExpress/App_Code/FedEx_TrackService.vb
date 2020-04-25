Imports tracking
'Imports WebReference
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

' Sample code to call the FedEx Tracking Web Service
' Tested with Microsoft Visual Studio 2005 professional Edition

Public Class FedEx_TrackService
    Public Function Track_FedEx(ByVal tracking_reference As String) As System.IO.MemoryStream
        Dim T As Track_Records = New Track_Records()
        'T.Add(New Track_Record("", "", "", "", "", "", ""))

        Dim Tracking_Number As String
        Dim Tracking_ID As String
        Dim Track_Desc As String
        Dim Time_Stamp As Date
        Dim Event_Desc As String = ""
        Dim City As String = ""
        Dim State As String = ""

        Dim request As TrackRequest = New TrackRequest() 'Build a track request object
        Dim service As TrackService = New TrackService() ' Initialize the service

        '
        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        'request.WebAuthenticationDetail.UserCredential.Key = "BFkUHfE41T5qeVwi " 'Test
        'request.WebAuthenticationDetail.UserCredential.Password = "as43kggXo6FOSpGKx8FDrBhsa" 'Test
        request.WebAuthenticationDetail.UserCredential.Key = "08QU2L0ukbUrEayj" 'Production
        request.WebAuthenticationDetail.UserCredential.Password = "q8eZrHUQeLGKhDIFNwBhNuSXU" 'Production

        '
        request.ClientDetail = New ClientDetail()
        'request.ClientDetail.AccountNumber = "510087623" ' Test
        'request.ClientDetail.MeterNumber = "118508675 " ' Test
        request.ClientDetail.AccountNumber = "410930404" ' Production
        request.ClientDetail.MeterNumber = "102602679" ' Production

        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = "***Track v4 Request using VB.NET***" 'The client will get the same value back in the response
        request.Version = New VersionId()
        '
        request.PackageIdentifier = New TrackPackageIdentifier() 'Tracking information
        request.PackageIdentifier.Value = tracking_reference '"794420271872" 'tracking_reference
        request.PackageIdentifier.Type = TrackIdentifierType.TRACKING_NUMBER_OR_DOORTAG
        '
        request.IncludeDetailedScans = True
        request.IncludeDetailedScansSpecified = True
        '
        '
        'Try
        ' This is the call to the web service passing in a request object and returning a reply object

        Dim reply As TrackReply = service.track(request)
        If ((Not reply.HighestSeverity = NotificationSeverityType.ERROR) And (Not reply.HighestSeverity = NotificationSeverityType.FAILURE)) Then 'check if the call was successful
            'Console.WriteLine("Track Service Details ..........")
            'Console.WriteLine("")
            If (Not reply.TrackDetails Is Nothing) Then
                For Each detail As TrackDetail In reply.TrackDetails 'Track details for each package
                    'Console.WriteLine("Tracking Number " + detail.TrackingNumber)
                    Tracking_Number = detail.TrackingNumber
                    'Console.WriteLine("Tracking Number Unique Identifier " + detail.TrackingNumberUniqueIdentifier)
                    Tracking_ID = detail.TrackingNumberUniqueIdentifier
                    'Console.WriteLine("Track Description " + detail.StatusDescription)
                    Track_Desc = detail.StatusDescription
                    For Each events As TrackEvent In detail.Events
                        'Console.WriteLine("Events...")
                        'Console.WriteLine("")
                        If (events.TimestampSpecified) Then
                            'Console.WriteLine("Timestamp " + events.Timestamp)
                            Time_Stamp = events.Timestamp
                        End If
                        If (Not events.EventDescription Is Nothing) Then
                            'Console.WriteLine("Description -" + events.EventDescription)
                            Event_Desc = events.EventDescription
                        End If
                        If (Not events.Address Is Nothing) Then
                            If (Not events.Address.City Is Nothing) Then
                                'Console.WriteLine("City " + events.Address.City)
                                City = events.Address.City
                            End If
                            If (Not events.Address.StateOrProvinceCode Is Nothing) Then
                                'Console.WriteLine("StateOrProvinceCode " + events.Address.StateOrProvinceCode)
                                State = events.Address.StateOrProvinceCode
                            End If
                        End If
                        T.Add(New Track_Record(Tracking_Number, Tracking_ID, Track_Desc, Time_Stamp, Event_Desc, City, State))
                    Next
                Next
            End If
        Else
            Dim Notification_txt As String = ""
            For Each notification As Notification In reply.Notifications
                'Console.WriteLine(notification.Message)
                Notification_txt = Notification_txt & notification.Message
            Next
            'MsgBox(Notification_txt.ToString)
        End If


        Dim serializer As XmlSerializer = New XmlSerializer(GetType(Track_Records))
        Dim stream As MemoryStream = New MemoryStream()
        If T.Count = 0 Then
            T.Add(New Track_Record("", "", "", "1900-01-01", "", "", ""))
        End If
        serializer.Serialize(stream, T)

        Return stream

        'Catch se As SoapException
        '    'Console.WriteLine(se.Detail.ToString())
        '    MsgBox(se.Detail.ToString())
        'Catch e As Exception
        '    'Console.WriteLine(e.Message)
        'MsgBox(e.Message)
        'End Try
        'Console.WriteLine(" Press any key to quit !")
        'Console.ReadKey()





    End Function
End Class
