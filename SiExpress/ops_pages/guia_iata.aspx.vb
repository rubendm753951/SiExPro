
Partial Class ops_pages_guia_iata
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        Try

        Dim empresa As Empresa
        Dim logoImageBase As Byte()
        If Session("empresa") IsNot Nothing Then
            Try
                empresa = CType(Session("empresa"), Empresa)
                If empresa.logo IsNot Nothing Then
                    logoImageBase = empresa.logo
                End If
            Catch ex As Exception

            End Try
        End If
        
        Dim archivo As String = "label_masupack_iata.rdlc"
        
        Guia.DataBind()
        'Crear Guia Tabsa
        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim deviceInfo As String
        Dim bytes As Byte()
        Dim lr As New Microsoft.Reporting.WebForms.LocalReport
        lr.EnableExternalImages = True

        lr.EnableExternalImages = True
        lr.ReportPath = Server.MapPath("~/Reports/" & archivo)
        lr.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet2_sp_Select_Datos_Envio_iata", Guia()))
        Dim parameter As New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Convert.ToBase64String(logoImageBase))
        lr.SetParameters(parameter)
        deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"
        bytes = lr.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

        Response.ContentType = "Application/pdf"
        Response.BinaryWrite(bytes)
        Response.End()
        'Catch ex As Exception
        '    TxtMsg.Text = "El número de guías supera el máximo de 100 " & vbCrLf & _
        '    "o no existen gías para el rango y agente indicados" & vbCrLf & _
        '    ex.Message.ToString
        'End Try

    End Sub
End Class
