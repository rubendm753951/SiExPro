
Imports System.Data

Partial Class ops_pages_Exportacion_Estafeta
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType(), "AutoCompleteDropDowns", "$(function () {  $('[id*=DropDownAgente]').multiselect({includeSelectAllOption: true,nonSelectedText: 'No hay elementos seleccionados.',nSelectedText: 'seleccionados',allSelectedText: 'Todos seleccionados',selectAllText: ' Seleccionar todos'}); });", True)
    End Sub

    Protected Sub btnCotizar_Click(sender As Object, e As EventArgs) Handles btnCotizar.Click
        Dim enviosExportar As New List(Of FrecuenciaCotizadorExport)
        Dim estafetaWrapper As New EstafetaWrapper()

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

                        Dim respuestaFrecuenciaCotizador As Estafeta.Frecuenciacotizador.Respuesta() = estafetaWrapper.FrecuenciaCotizadorSingle(envioExportar)

                        If respuestaFrecuenciaCotizador.Length > 0 Then
                            If respuestaFrecuenciaCotizador(0).MensajeError <> "" Then

                                row.Cells(12).Text = respuestaFrecuenciaCotizador(0).MensajeError
                            Else
                                Dim ddlTiposServicio As DropDownList = CType(row.FindControl("DropDownTipoServicio"), DropDownList)
                                Dim dsTipoServicio As DataList = CType(row.FindControl("dlTipoServicio"), DataList)

                                For Each tipoServicio As Estafeta.Frecuenciacotizador.TipoServicio In respuestaFrecuenciaCotizador(0).TipoServicio
                                    Dim ddItem As New ListItem()
                                    With ddItem
                                        .Text = tipoServicio.DescripcionServicio
                                        .Value = tipoServicio.DescripcionServicio
                                    End With
                                    ddItem.Attributes.Add("data-TarifaBase", tipoServicio.TarifaBase.ToString())
                                    ddItem.Attributes.Add("data-TipoEnvioRes", tipoServicio.TipoEnvioRes.ToString())
                                    ddItem.Attributes.Add("data-AplicaCotizacion", tipoServicio.AplicaCotizacion)
                                    ddItem.Attributes.Add("data-CCTarifaBase", tipoServicio.CCTarifaBase.ToString())
                                    ddItem.Attributes.Add("data-SobrePeso", tipoServicio.SobrePeso.ToString())
                                    ddItem.Attributes.Add("data-CCSobrePeso", tipoServicio.CCSobrePeso.ToString())
                                    ddItem.Attributes.Add("data-CostoTotal", tipoServicio.CostoTotal.ToString())
                                    ddItem.Attributes.Add("data-Peso", tipoServicio.Peso.ToString())
                                    ddItem.Attributes.Add("data-AplicaServicio", tipoServicio.AplicaServicio)

                                    ddlTiposServicio.Items.Add(ddItem)
                                Next
                                Dim sessionTipoServico = row.Cells(1).Text
                                Session(sessionTipoServico) = respuestaFrecuenciaCotizador

                                ddlTiposServicio.DataSource = respuestaFrecuenciaCotizador(0).TipoServicio
                                ddlTiposServicio.DataTextField = "DescripcionServicio"
                                ddlTiposServicio.DataValueField = "DescripcionServicio"

                                dsTipoServicio.DataSource = respuestaFrecuenciaCotizador(0).TipoServicio
                                dsTipoServicio.DataBind()

                                Dim ts = respuestaFrecuenciaCotizador(0).TipoServicio.FirstOrDefault(Function(x) x.DescripcionServicio = ddlTiposServicio.SelectedValue)

                                row.Cells(12).Text = "CargosExtra:" + ts.CargosExtra.ToString() + vbCrLf +
                                                    "TarifaBase:" + ts.TarifaBase.ToString() + vbCrLf +
                                                    "AplicaCotizacion:" + ts.AplicaCotizacion + vbCrLf +
                                                    "CCTarifaBase:" + ts.CCTarifaBase.ToString() + vbCrLf +
                                                    "SobrePeso:" + ts.SobrePeso.ToString() + vbCrLf +
                                                    "CCSobrePeso:" + ts.CCSobrePeso.ToString() + vbCrLf +
                                                    "Peso:" + ts.Peso.ToString() + vbCrLf +
                                                    "AplicaServicio:" + ts.AplicaServicio + vbCrLf +
                                                    "CostoTotal:" + ts.CostoTotal.ToString()

                                'ddlTiposServicio.DataBind()

                            End If
                        End If

                    End If

                End If
            End If
        Next

        'GridView1.DataBind()

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim enviosExportar As New List(Of FrecuenciaCotizadorExport)
        Dim estafetaWrapper As New EstafetaWrapper()

        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim checkRow As CheckBox = row.FindControl("chkEnvio")
                If checkRow IsNot Nothing AndAlso checkRow.Checked Then
                    Dim dv As DataView = Me.EnviosAExportar.Select(DataSourceSelectArguments.Empty)
                    Dim dsRow = dv(row.RowIndex)

                    Dim ddlTiposServicio As DropDownList = CType(row.FindControl("DropDownTipoServicio"), DropDownList)
                    If Session(row.Cells(1).Text) IsNot Nothing Then
                        Dim respuestaFrecuenciaCotizador = CType(Session(row.Cells(1).Text), Estafeta.Frecuenciacotizador.Respuesta())
                        Dim sessionTipoServico = respuestaFrecuenciaCotizador(0).TipoServicio
                        Dim tipoServicio = sessionTipoServico.FirstOrDefault(Function(x) x.DescripcionServicio = ddlTiposServicio.SelectedValue)
                        Dim respuestaLabel As String = estafetaWrapper.Label(dsRow, tipoServicio, respuestaFrecuenciaCotizador)

                        row.Cells(12).Text = respuestaLabel
                        If respuestaLabel = "Envio Exportado" Then
                            Dim hp As HyperLink = New HyperLink()
                            hp.Text = "Ver Etiqueta"
                            hp.NavigateUrl = "~\Reports\EstafetaLabel.aspx?id_envio=" + dsRow("id_envio").ToString()
                            hp.Target = "_blank"
                            row.Cells(13).Controls.Add(hp)

                            checkRow.Checked = False
                            checkRow.Visible = False

                        End If

                        estafetaWrapper.Tracking()
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub DropDownTipoServicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grow As GridViewRow = DirectCast(DirectCast(sender, Control).NamingContainer, GridViewRow)

        Dim ddlTiposServicio As DropDownList
        ddlTiposServicio = CType(sender, DropDownList)
        Dim row As GridViewRow = ddlTiposServicio.Parent.Parent

        If Session(row.Cells(1).Text) IsNot Nothing Then
            Dim respuestaFrecuenciaCotizador = CType(Session(row.Cells(1).Text), Estafeta.Frecuenciacotizador.Respuesta())
            Dim sessionTipoServico = respuestaFrecuenciaCotizador(0).TipoServicio
            Dim tipoServicio = sessionTipoServico.FirstOrDefault(Function(x) x.DescripcionServicio = ddlTiposServicio.SelectedValue)

            row.Cells(12).Text = "CargosExtra:" + tipoServicio.CargosExtra.ToString() + vbCrLf +
                        "TarifaBase:" + tipoServicio.TarifaBase.ToString() + vbCrLf +
                        "AplicaCotizacion:" + tipoServicio.AplicaCotizacion + vbCrLf +
                        "CCTarifaBase:" + tipoServicio.CCTarifaBase.ToString() + vbCrLf +
                        "SobrePeso:" + tipoServicio.SobrePeso.ToString() + vbCrLf +
                        "CCSobrePeso:" + tipoServicio.CCSobrePeso.ToString() + vbCrLf +
                        "Peso:" + tipoServicio.Peso.ToString() + vbCrLf +
                        "AplicaServicio:" + tipoServicio.AplicaServicio + vbCrLf +
                        "CostoTotal:" + tipoServicio.CostoTotal.ToString()
        End If

    End Sub

    'Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    If (e.Row.RowType = DataControlRowType.DataRow) Then

    '        'Find the DropDownList in the Row.
    '        Dim ddlCountries As DropDownList = CType(e.Row.FindControl("ddlCountries"), DropDownList)
    '        ddlCountries.DataSource = GetData("SELECT DISTINCT Country FROM Customers")
    '        ddlCountries.DataTextField = "Country"
    '        ddlCountries.DataValueField = "Country"
    '        ddlCountries.DataBind()

    '        'Add Default Item in the DropDownList.
    '        ddlCountries.Items.Insert(0, New ListItem("Please select"))

    '        'Select the Country of Customer in DropDownList.
    '        Dim country As String = CType(e.Row.FindControl("lblCountry"), Label).Text
    '        ddlCountries.Items.FindByValue(country).Selected = True
    '    End If
    'End Sub

    Private Sub SetDataRowValue(ByVal grid As GridView, ByVal columnName As String, ByVal newVal As String, ByVal rowIndex As Integer)

        ' Set the value of a column in the last row of a DataGrid.
        Dim table As DataTable = CType(grid.DataSource, DataTable)
        Dim row As DataRow = table.Rows(rowIndex)
        Dim column As DataColumn = table.Columns(columnName)
        row(column) = newVal
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub DropDownAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgente.SelectedIndexChanged
        GridView1.DataBind()

    End Sub
End Class
