
Imports System.Net
Imports Microsoft.SqlServer.Server

Public Class MailSender
    Public Const SMTPSERVER As String = "Email.SmtpServer"
    Public Const PORT As String = "Email.Port"
    Public Const ENABLESSL As String = "Email.EnableSSL"
    Public Const USERNAME As String = "Email.UserName"
    Public Const PASSWORD As String = "Email.Password"
    Public Const DISPLAYNAME As String = "Email.DisplayName"

    Public Property RecipientAddress As String
    Public Property RecipientName As String
    Public Property Attachments As List(Of Mail.Attachment) = New List(Of Mail.Attachment)
    Public Property EmailType As Integer
    Public Property BodyComplement As String

    Public Function SendMail() As String

        Dim smtpClient As Mail.SmtpClient = New Mail.SmtpClient()
        Dim message As Mail.MailMessage = ComposeMail()
        Dim result As String = "Success"

        SetSmtpConfig(smtpClient)

        Try
            smtpClient.Send(message)
        Catch ex As Exception
            Dim errorLog = New ErrorLog
            errorLog.Log("MailSender_SendMail", ex, ex.Source, "0")
        End Try

        Return result

    End Function

    Public Sub AddAttatchment(path As String)

        If IO.File.Exists(path) Then _Attachments.Add(New Mail.Attachment(path))

    End Sub

    Private Function ComposeMail() As Mail.MailMessage
        Dim message As New Mail.MailMessage
        Try
            message.Body = "Pago a través de PayPal" + " " + _BodyComplement
            message.Subject = ConfigurationManager.AppSettings("Email.Environment") + " Abono PayPal"


            Dim bccEmail As String = ConfigurationManager.AppSettings("Email.BCCMail")
            Dim bccNombre As String = ConfigurationManager.AppSettings("Email.BCCNombre")

            If _RecipientAddress <> "" Then
                message.To.Add(New Mail.MailAddress(_RecipientAddress, _RecipientName))
            Else
                message.To.Add(New Mail.MailAddress(bccEmail, bccNombre))
            End If

            message.Bcc.Add(New Mail.MailAddress(bccEmail, bccNombre))
            message.From = New Mail.MailAddress(ConfigurationManager.AppSettings(USERNAME),
                                                ConfigurationManager.AppSettings(DISPLAYNAME))

            For Each attachment As Mail.Attachment In _Attachments
                message.Attachments.Add(attachment)
            Next
        Catch ex As Exception
            Dim errorLog = New ErrorLog
            errorLog.Log("MailSender_ComposeMail", ex, ex.Source, "0")
            Return message
        End Try

        Return message

    End Function

    Private Sub SetSmtpConfig(ByRef smtpClient As Mail.SmtpClient)

        With smtpClient
            .Host = ConfigurationManager.AppSettings(SMTPSERVER)
            .EnableSsl = CBool(ConfigurationManager.AppSettings(ENABLESSL))
            .Port = CInt(ConfigurationManager.AppSettings(PORT))
            .Credentials = New NetworkCredential With {
              .UserName = ConfigurationManager.AppSettings(USERNAME),
              .Password = ConfigurationManager.AppSettings(PASSWORD)
            }
        End With

    End Sub
End Class
