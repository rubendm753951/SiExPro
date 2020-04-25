Imports Microsoft.Reporting.WebForms

Partial Class Reports_Reporte__manif_asign
    Inherits BasePageNoLogin

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Asignacion.DataBind()

        'Lanzar reporte de MAnifiesto de asignacion
        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim deviceInfo As String
        Dim bytes As Byte()
        Dim lr As New Microsoft.Reporting.WebForms.LocalReport

        lr.ReportPath = Server.MapPath("~/Reports/Reporte_manif_asign.rdlc")
        lr.EnableExternalImages = True
        Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
        lr.SetParameters(parameter)
        lr.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1_sp_select_envios_asignados_promotor", Asignacion()))
        deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"
        bytes = lr.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

        'Set the appropriate ContentType.
        Response.ContentType = "Application/pdf"
        'Write the file directly to the HTTP output stream.
        'Response.WriteFile(archivo_guia)
        Response.BinaryWrite(bytes)
        Response.End()
    End Sub
End Class
