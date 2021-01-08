
Partial Class admin_pages_admin_tarifas_estafeta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('[id*=DropDownAgente]').multiselect({includeSelectAllOption: true,nonSelectedText: 'No hay elementos seleccionados.',nSelectedText: 'seleccionados',allSelectedText: 'Todos seleccionados',selectAllText: ' Seleccionar todos'}); });", True)
    End Sub

    Private Sub DropDownAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownAgente.SelectedIndexChanged
        GridView1.DataBind()
    End Sub
End Class
