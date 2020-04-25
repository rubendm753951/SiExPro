
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("user_name") Is Nothing Then
            Label2.Text = "Usuario: " & Session("user_name").ToString
        End If
    End Sub
End Class

