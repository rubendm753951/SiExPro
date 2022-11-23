Imports Microsoft.VisualBasic

Public MustInherit Class BasePageNoLogin
    Inherits System.Web.UI.Page

    Public Shared IdModulo As Integer = 0
    Public Shared PuedeLeer As Boolean = False
    Public Shared PuedeModificar As Boolean = False
    Public Shared PuedeBorrar As Boolean = False
    Public Shared PuedeGuardar As Boolean = False
    Public LogoImageBase As Byte()

    Private Sub BasePage_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim empresa As Empresa
        Dim logoImage As Image = CType(Master.FindControl("Image1"), Image)
            logoImage.Height = 280
            logoImage.Width = 70
        Try
            empresa = DaspackDALC.GetUsuarioEmpresa(1)

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
