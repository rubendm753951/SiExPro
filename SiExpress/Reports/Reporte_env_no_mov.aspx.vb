Imports Microsoft.Reporting.WebForms
Partial Class Reports_Reporte_env_no_mov
    Inherits BasePage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Reportviewer1.LocalReport.EnableExternalImages = True
            Dim parameter As New ReportParameter("ImagePath", Convert.ToBase64String(LogoImageBase))
            Reportviewer1.LocalReport.SetParameters(parameter)
            Reportviewer1.LocalReport.Refresh()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ReportViewer1.LocalReport.Refresh()
    End Sub

End Class
