
Partial Class admin_pages_admin_tarifas_estafeta
    Inherits BasePage

    Private Shared _idModulo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim modulo As Modulo = DaspackDALC.GetModuloPorDescripcion(Me.AppRelativeVirtualPath.ToString)

        If modulo IsNot Nothing Then
            _idModulo = modulo.IdModulo
        End If
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('[id*=DropDownAgente]').multiselect({includeSelectAllOption: true,nonSelectedText: 'No hay elementos seleccionados.',nSelectedText: 'seleccionados',allSelectedText: 'Todos seleccionados',selectAllText: ' Seleccionar todos'}); });", True)
    End Sub

    Private Sub DropDownAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownAgente.SelectedIndexChanged
        GridView1.DataBind()
    End Sub
End Class
