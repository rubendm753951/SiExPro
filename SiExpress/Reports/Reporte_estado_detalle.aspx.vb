
Imports Microsoft.Reporting.WebForms

Partial Class Reports_estado_envio_detalle
    Inherits BasePage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ReportViewer1.LocalReport.EnableExternalImages = True
            Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
            ReportViewer1.LocalReport.SetParameters(parameter)
            ReportViewer1.LocalReport.Refresh()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ReportViewer1.LocalReport.Refresh()
    End Sub
End Class
