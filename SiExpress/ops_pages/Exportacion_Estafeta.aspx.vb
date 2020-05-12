
Partial Class ops_pages_Exportacion_Estafeta
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('[id*=DropDownAgente]').multiselect({includeSelectAllOption: true,nonSelectedText: 'No hay elementos seleccionados.',nSelectedText: 'seleccionados',allSelectedText: 'Todos seleccionados',selectAllText: ' Seleccionar todos'}); });", True)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim enviosExportar As New List(Of FrecuenciaCotizadorExport)
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim checkRow As CheckBox = row.FindControl("chkEnvio")
                If checkRow IsNot Nothing AndAlso checkRow.Checked Then
                    Dim envioExportar As New FrecuenciaCotizadorExport()

                    If Not String.IsNullOrEmpty(row.Cells(10).Text) And Not String.IsNullOrEmpty(row.Cells(6).Text) Then
                        With envioExportar
                            .CPDestinatario = row.Cells(10).Text
                            .CPRemitente = row.Cells(6).Text
                            .IdEnvio = CInt(row.Cells(1).Text)
                        End With
                        enviosExportar.Add(envioExportar)
                    End If

                End If
            End If
        Next

        Dim estafetaWrapper As New EstafetaWrapper()

        estafetaWrapper.FrecuenciaCotizador(enviosExportar)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub DropDownAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgente.SelectedIndexChanged
        GridView1.DataBind()

    End Sub
End Class
