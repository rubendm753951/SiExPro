
Imports System.Data
Imports seguridad
Partial Class admin_tarifas
    Inherits BasePage
    Protected Sub DropDownPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownPais.SelectedIndexChanged
        DropDownCorp.DataBind()
        DropDownAgente.DataBind()
        GridView1.DataBind()

        Dim etiqueta As String, elementos As Integer
        etiqueta = ""
        elementos = DropDownAgente.SelectedIndex
        If elementos = -1 Then
            etiqueta = "Configuración de Tarifas para el agente: "
        Else
            etiqueta = "Configuración de Tarifas para el agente: " & DropDownAgente.SelectedItem.ToString()
        End If
        Label4.Text = etiqueta
    End Sub

    Protected Sub DropDownCorp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownCorp.SelectedIndexChanged
        DropDownAgente.DataBind()
        GridView1.DataBind()

        Dim etiqueta As String, elementos As Integer
        etiqueta = ""
        elementos = DropDownAgente.SelectedIndex
        If elementos = -1 Then
            etiqueta = "Configuración de Tarifas para el agente: "
        Else
            etiqueta = "Configuración de Tarifas para el agente: " & DropDownAgente.SelectedItem.ToString()
        End If
        Label4.Text = etiqueta
    End Sub

    Protected Sub DropDownAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgente.SelectedIndexChanged
        GridView1.DataBind()

        Dim etiqueta As String, elementos As Integer
        etiqueta = ""
        elementos = DropDownAgente.SelectedIndex
        If elementos = -1 Then
            etiqueta = "Configuración de Tarifas para el agente: "
        Else
            etiqueta = "Configuración de Tarifas para el agente: " & DropDownAgente.SelectedItem.ToString()
        End If
        Label4.Text = etiqueta

    End Sub
    Protected Sub sqlDataSource1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles Agente_Tarifas.Updating


    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Function ConvertSortDirectionToSql(ByVal sortDirection As SortDirection) As String
        Dim newSortDirection As String = String.Empty

        Select Case sortDirection
            Case SortDirection.Ascending
                newSortDirection = "ASC"
            Case SortDirection.Descending
                newSortDirection = "DESC"
        End Select

        Return newSortDirection
    End Function

    Protected Sub gridView_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dataTable As DataTable = TryCast(GridView1.DataSource, DataTable)

        If dataTable IsNot Nothing Then
            Dim dataView As DataView = New DataView(dataTable)
            dataView.Sort = e.SortExpression & " " & ConvertSortDirectionToSql(e.SortDirection)
            GridView1.DataSource = dataView
            GridView1.DataBind()
        End If
    End Sub


End Class


