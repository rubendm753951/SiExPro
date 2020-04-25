
Partial Class Reports_Report_Manager
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '*************Bloque para validar al usuario ********************
        Dim validador As New seguridad

        Dim controlitem As Control
        For Each controlitem In Panel1.Controls
            If (controlitem.GetType().ToString().Equals("System.Web.UI.WebControls.HyperLink")) Then
                If Not validador.validar_usuario(Session("user_name"), DirectCast(controlitem, System.Web.UI.WebControls.HyperLink).NavigateUrl) Then
                    controlitem.Visible = False
                End If
            End If
            ' perform desired processing on each item.
        Next controlitem

    End Sub
End Class
