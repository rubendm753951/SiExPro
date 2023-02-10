Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("user_name") Is Nothing Then
            Label1.Text = "Usuario: " & Session("user_name").ToString
            If Session("id_perfil") = 2 Then
                SiteMapDataSource1.StartingNodeUrl = "~/ops_pages/cliente_corporativo.aspx"
                'SiteMapDataSource1.StartingNodeUrl = "ops_pages/operacion.aspx"
            End If
        End If
    End Sub
End Class

