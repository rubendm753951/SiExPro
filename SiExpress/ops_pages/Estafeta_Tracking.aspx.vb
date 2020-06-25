
Imports SiExProData

Partial Class ops_pages_Estafeta_Tracking
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "", True)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not String.IsNullOrWhiteSpace(txtEnvio.Text) And Not String.IsNullOrEmpty(txtEnvio.Text) Then
            Dim envio As Envio = DaspackDALC.GetDatosEnvio(txtEnvio.Text)
            If envio IsNot Nothing Then
                Dim trackingId As String = envio.Referencia_FedEx.Trim()
                Dim estafetaWrapper As New EstafetaWrapper()
                Dim estafetaTracking = estafetaWrapper.Tracking(trackingId)
                GridView1.DataSource = estafetaTracking
                GridView1.DataBind()
            End If

        End If


    End Sub
End Class
