Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions
Imports System.Security.Policy
Imports Microsoft.Reporting.WebForms

Partial Class ops_pages_guia
    Inherits BasePageNoLogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim deviceInfo As String
        Dim bytes As Byte()
        Dim lr As New Microsoft.Reporting.WebForms.LocalReport

        Dim Permissions As New PermissionSet(PermissionState.None)
        Permissions.AddPermission(New FileIOPermission(PermissionState.Unrestricted))
        Permissions.AddPermission(New SecurityPermission(SecurityPermissionFlag.Execution))
        lr.SetBasePermissionsForSandboxAppDomain(Permissions)

        Guia.DataBind()
        lr.EnableExternalImages = True
        Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
        lr.SetParameters(parameter)
        lr.ReportPath = Server.MapPath("~/Reports/label_masupack.rdlc")

        lr.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet2_sp_Select_Datos_Envio", Guia()))
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
