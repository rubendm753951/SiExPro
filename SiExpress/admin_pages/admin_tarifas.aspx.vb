
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

End Class


