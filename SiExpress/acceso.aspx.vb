
Partial Class acceso
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '**********  FOR TESTING PURPOSE ONLY, ENABLE CODE BELOW WHEN READY *************
        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Session("user_name") = "admin"
        'Session("id_usuario") = 1
        'Session("id_perfil") = 1
        'Response.Redirect("~/admin_pages/admin.aspx")
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '**********  FOR TESTING PURPOSE ONLY, ENABLE CODE BELOW WHEN READY *************
        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        Dim user_login As New seguridad
        Dim respuesta As Boolean
        respuesta = user_login.login_usuario(TxtBoxUser_Name.Text, TextBoxPwd.Text)
        If IsNothing(respuesta) Or respuesta = False Then
            Response.Redirect("acceso_denegado.aspx")
            ' Dim sJScript2 As String = "<script language=""Javascript"">" & _
            '" window.open('acceso_denegado.aspx','','width=800,height=500, toolbar=1, Scrollbars=1')" & _
            ' "</script>"

        Else
            Session("user_name") = TxtBoxUser_Name.Text
            'Session("password") = TextBoxPwd.Text
            Session("id_usuario") = user_login.id_usuario
            Session("id_perfil") = user_login.id_perfil
            Session("id_oficina") = user_login.id_oficina

            Dim empresa As Empresa = DaspackDALC.GetUsuarioEmpresa(user_login.id_usuario)
            If empresa IsNot Nothing Then
                Session("empresa") = empresa
                Session("id_empresa") = empresa.id_empresa

                Response.Redirect("~/admin_pages/admin.aspx")
            Else
                Response.Redirect("~/no_access.aspx")
            End If


        End If
    End Sub
End Class
