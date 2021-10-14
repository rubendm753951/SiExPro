
Imports Microsoft.Reporting.WebForms

Partial Class Reports_Envios_procesados_sin_movimiento
    Inherits BasePageNoLogin

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not Me.IsPostBack Then
            Dim logoImage As Image = CType(Master.FindControl("Image1"), Image)

            ReportViewer1.LocalReport.EnableExternalImages = True
            Dim imagePath As String = logoImage.ImageUrl
            Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
            ReportViewer1.LocalReport.SetParameters(parameter)
            ReportViewer1.LocalReport.Refresh()
        End If
    End Sub
End Class
