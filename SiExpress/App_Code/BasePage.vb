Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports SiExProData

Public MustInherit Class BasePage
    Inherits System.Web.UI.Page

    Public Shared IdModulo As Integer = 0
    Public Shared PuedeLeer As Boolean = False
    Public Shared PuedeModificar As Boolean = False
    Public Shared PuedeBorrar As Boolean = False
    Public Shared PuedeGuardar As Boolean = False
    Public LogoImageBase As Byte()

    Private Sub BasePage_Load(sender As Object, e As EventArgs) Handles Me.Load
        '*************Bloque para validar al usuario ********************
        If Not Session("user_name") = Nothing Then
            Dim validador As New seguridad
            If Not validador.validar_usuario(Session("user_name"), Me.AppRelativeVirtualPath.ToString) Then
                Response.Redirect("~/no_access.aspx")
            End If
        Else
            Response.Redirect("~/no_session.aspx")
        End If
        '****************************************************************

        Dim empresa As Empresa
        If Session("empresa") IsNot Nothing Then
            Dim logoImage As Image = CType(Master.FindControl("Image1"), Image)
            Dim lblAdeudo As Label = CType(Master.FindControl("lblAdeudo"), Label)
            logoImage.Height = 280
            logoImage.Width = 70
            Try
                empresa = CType(Session("empresa"), Empresa)

                Dim adeudo As Decimal = DaspackDALC.GetAdeudoEmpresaTotal(empresa.id_empresa)

                If adeudo > 0 Then
                    lblAdeudo.Visible = True
                    lblAdeudo.Text = "Tiene un adeudo pendiente, da clic aqui para ver el detalle."
                Else
                    lblAdeudo.Text = ""
                    lblAdeudo.Visible = False
                End If

                If empresa.logo IsNot Nothing Then
                    LogoImageBase = empresa.logo
                    Dim base64String As String = Convert.ToBase64String(empresa.logo, 0, empresa.logo.Length)

                    logoImage.ImageUrl = "data:image/jpeg;base64," + base64String
                    logoImage.Height = empresa.alto
                    logoImage.Width = empresa.ancho
                Else
                    logoImage.ImageUrl = "~/Images/451logoUnavailable.png"
                End If
            Catch ex As Exception
                logoImage.ImageUrl = "~/Images/451logoUnavailable.png"
            End Try
        Else
            Response.Redirect("~/no_access.aspx")
        End If
        dim idPerfil as Integer = Session("id_perfil") 
        dim idOficina as Integer = Session("id_oficina") 
        If Not Me.AppRelativeVirtualPath.ToString.Contains("EnviosPendientes") and  idPerfil = 3 and Not Me.AppRelativeVirtualPath.ToString.Contains("consulta_envio") Then
            Dim dbContext As New SiExProEntities
            Dim envioComentarioResult as SelectEnviosComentariosPendientesResult = dbContext.sp_Select_envios_comentarios_pendientes(idOficina).FirstOrDefault()
            If envioComentarioResult IsNot Nothing Then
                Response.Redirect("~/ops_pages/EnviosPendientes.aspx")
            End If
        End If

    End Sub



    Public Shared Function IsUserLogged() As Integer
        Dim usuarioId As Integer = 0
        If HttpContext.Current.Session("id_usuario") IsNot Nothing Then
            usuarioId = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            If (usuarioId <> Nothing AndAlso usuarioId <> 0) Then
                Return usuarioId
            End If
            Return usuarioId
        End If
        Return usuarioId
    End Function
End Class
