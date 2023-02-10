
Partial Class Reports_summary_tracking_cliente2
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ReportViewer1.LocalReport.Refresh()
    End Sub
End Class
