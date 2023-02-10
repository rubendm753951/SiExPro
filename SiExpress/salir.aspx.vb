
Partial Class salir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("user_name") = Nothing
        Session("id_usuario") = Nothing
        Session("id_perfil") = Nothing
        Response.Redirect("~/admin_pages/admin.aspx")
    End Sub
End Class
