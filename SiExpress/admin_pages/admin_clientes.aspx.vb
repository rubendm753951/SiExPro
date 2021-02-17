
Partial Class admin_pages_admin_clientes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('[id*=DropDownAgente]').multiselect({includeSelectAllOption: true,nonSelectedText: 'No hay elementos seleccionados.',nSelectedText: 'seleccionados',allSelectedText: 'Todos seleccionados',selectAllText: ' Seleccionar todos'}); });", True)
    End Sub
End Class
