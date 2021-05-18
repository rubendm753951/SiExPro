Imports System.Configuration
Imports System.Net
Imports TrackService

Public Class FedEx_TrackService
    Public shipmentInfo As String = ""
    Public Function Track_FedEx(ByVal tracking_reference As String) As ArrayList
        Dim T As ArrayList = New ArrayList()

        Dim Tracking_Number As String
        Dim Tracking_ID As String
        Dim Event_Desc As String = ""
        Dim City As String = ""
        Dim State As String = ""

        Dim request As TrackRequest = New TrackRequest() 'Build a track request object
        Dim service As TrackService.TrackService = New TrackService.TrackService() ' Initialize the service

        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        request.WebAuthenticationDetail.UserCredential.Key = ConfigurationManager.AppSettings("FedEx.Key")
        request.WebAuthenticationDetail.UserCredential.Password = ConfigurationManager.AppSettings("FedEx.Password")

        request.ClientDetail = New ClientDetail()
        request.ClientDetail.AccountNumber = ConfigurationManager.AppSettings("FedEx.AccountNumber")
        request.ClientDetail.MeterNumber = ConfigurationManager.AppSettings("FedEx.MeterNumber")

        request.TransactionDetail = New TransactionDetail()

        request.Version = New VersionId()

        request.SelectionDetails = New TrackSelectionDetail() {New TrackSelectionDetail()}

        request.SelectionDetails(0).PackageIdentifier = New TrackPackageIdentifier() 'Tracking information
        request.TransactionDetail.CustomerTransactionId = "***Track v4 Request using VB.NET***"
        request.SelectionDetails(0).PackageIdentifier.Value = tracking_reference '"794420271872" 'tracking_reference
        request.SelectionDetails(0).PackageIdentifier.Type = TrackIdentifierType.TRACKING_NUMBER_OR_DOORTAG

        request.ProcessingOptions = New TrackRequestProcessingOptionType() {New TrackRequestProcessingOptionType()}
        request.ProcessingOptions(0) = TrackRequestProcessingOptionType.INCLUDE_DETAILED_SCANS

        ' This is the call to the web service passing in a request object and returning a reply object
        ServicePointManager.Expect100Continue = True
        ServicePointManager.DefaultConnectionLimit = 9999
        ServicePointManager.SecurityProtocol = 3072

        Dim reply As TrackReply = service.track(request)

        Dim log As String = ConfigurationManager.AppSettings("Estafeta.LogRequestResponse")
        If log = "true" Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonRequest = serializer.Serialize(request)
            Dim jsonResonse = serializer.Serialize(reply)

            DaspackDALC.LogEstafetaRequestResponse("FedexTracking", jsonRequest, jsonResonse, 0, 2)
        End If


        If ((Not reply.HighestSeverity = NotificationSeverityType.ERROR) And (Not reply.HighestSeverity = NotificationSeverityType.FAILURE)) Then 'check if the call was successful

            If (reply.CompletedTrackDetails IsNot Nothing) Then
                For Each completedTrackDetail As CompletedTrackDetail In reply.CompletedTrackDetails
                    For Each detail As TrackDetail In completedTrackDetail.TrackDetails 'Track details for each package
                        Dim shipmentName As String = ""
                        Dim shipmentAddress As String = ""
                        Dim Track_Desc As String = ""
                        Dim Time_Stamp As String = ""

                        If detail.Shipper IsNot Nothing Then
                            shipmentName = detail.Shipper.PersonName
                        End If

                        If detail.ShipperAddress IsNot Nothing Then
                            shipmentAddress = detail.ShipperAddress.City + " " + detail.ShipperAddress.CountryName + " " + detail.ShipperAddress.CountryCode
                        End If

                        If shipmentName <> "" Or shipmentAddress <> "" Then
                            shipmentInfo = detail.ShipTimestamp.ToShortDateString() + " " + shipmentName + " " + shipmentAddress
                        Else
                            shipmentInfo = ""
                        End If

                        Tracking_Number = detail.TrackingNumber
                        Tracking_ID = detail.TrackingNumberUniqueIdentifier
                        If detail.StatusDetail IsNot Nothing Then
                            Track_Desc = detail.StatusDetail.Description + " - " + detail.StatusDetail.Code
                        End If
                        If detail.Events IsNot Nothing Then
                            For Each events As TrackEvent In detail.Events
                                If (events.TimestampSpecified) Then
                                    Time_Stamp = events.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")
                                End If
                                If (Not events.EventDescription Is Nothing) Then
                                    Event_Desc = events.EventDescription
                                End If
                                If (Not events.Address Is Nothing) Then
                                    If (Not events.Address.City Is Nothing) Then
                                        City = events.Address.City
                                    End If
                                    If (Not events.Address.StateOrProvinceCode Is Nothing) Then
                                        State = events.Address.StateOrProvinceCode
                                    End If
                                End If
                                T.Add(New Track_Record(Tracking_Number, Tracking_ID, Track_Desc, Time_Stamp, Event_Desc, City, State))
                            Next
                        End If
                    Next
                Next
            End If
        Else
            Dim Notification_txt As String = ""
            Dim errorLog = New ErrorLog
            For Each notification As Notification In reply.Notifications
                Notification_txt = Notification_txt & notification.Message
            Next

            errorLog.ErrorLog("Track_FedEx", Notification_txt, "FedEx", "", "")
        End If
        If T.Count = 0 Then
            T.Add(New Track_Record("", "", "", "1900-01-01", "", "", ""))
        End If
        Return T
    End Function
End Class
