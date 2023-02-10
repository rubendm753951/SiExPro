Imports Microsoft.Reporting.WebForms

Partial Public Class Reports_Reporte_contenedores
    Inherits BasePageNoLogin

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ReportViewer1.LocalReport.EnableExternalImages = True
            Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
            ReportViewer1.LocalReport.SetParameters(parameter)
            ReportViewer1.LocalReport.Refresh()
        End If
    End Sub
End Class