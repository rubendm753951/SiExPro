Imports System.Data
Imports System.Data.SqlClient
Imports SiExProData

Partial Class ops_pages_rastreo
    Inherits BasePageNoLogin

    Protected Sub BtnRastreo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRastreo.Click
        TextBox1.Visible = False
        Session("id_envio") = "0"
        GridView2.DataBind()

        Session("Envio1") = "0"
        Session("Envio2") = "0"
        Session("Envio3") = "0"
        Session("Envio4") = "0"
        Session("Envio5") = "0"
        Session("Envio6") = "0"
        Session("Envio7") = "0"
        Session("Envio8") = "0"
        Session("Envio9") = "0"
        Session("Envio10") = "0"
        Session("tipo_tracking") = 1
        TextBox1.Text = ""
        TextBox1.Visible = False

        If cbMasupack.Checked = True Then
            Session("tipo_tracking") = 1
        Else
            Session("tipo_tracking") = 2
        End If

        Dim envio As String
        Dim split As String() = TxtBoxEnvios.Text.Split(New [Char]() {CChar(vbLf)}) 'Char() {vbCrLfc}  '[Char]() {CChar(vbCrLf)}
        Dim i As Integer = 1
        Dim s As String
        For Each s In split
            If Session("tipo_tracking") = 1 And Not IsNumeric(Trim(s)) Then
                TextBox1.Text = "Lo números de guía no admiten otro caracter distito de número, intente con referencia cliente."
                TextBox1.Visible = True
            ElseIf s <> vbCrLf Then
                envio = "Envio" + i.ToString
                Session(envio) = s.Trim
            End If
            If i > 10 Then
                Exit For
            End If
            i = i + 1
        Next s
        GridView1.DataBind()
        If GridView1.Rows.Count = 0 Then
            TextBox1.Text = TextBox1.Text + vbCrLf + "No existen datos coincidentes de guías o referencias."
            TextBox1.Visible = True
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        Session("id_envio") = row.Cells(1).Text
        GridView2.DataBind()


        Dim envio As Envio = DaspackDALC.GetDatosEnvio(row.Cells(1).Text)
        If envio IsNot Nothing Then
            Dim trackingId As String = envio.Referencia_FedEx.Trim()
            Dim estafetaWrapper As New EstafetaWrapper()
            Dim estafetaTracking = estafetaWrapper.Tracking(trackingId)
            GridView3.DataSource = estafetaTracking
            GridView3.DataBind()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("id_envio") = "0"
        'Session("Envio1") = "0"
        'Session("Envio2") = "0"
        'Session("Envio3") = "0"
        'Session("Envio4") = "0"
        'Session("Envio5") = "0"
        'Session("Envio6") = "0"
        'Session("Envio7") = "0"
        'Session("Envio8") = "0"
        'Session("Envio9") = "0"
        'Session("Envio10") = "0"
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        'https://www.aspsnippets.com/Articles/Display-Binary-Image-from-database-in-ASPNet-GridView-control-using-C-and-VBNet.aspx
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRowView = CType(e.Row.DataItem, DataRowView)
            If dr("SignImage") IsNot System.DBNull.Value Then
                Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(dr("SignImage"), Byte()))
                CType(e.Row.FindControl("Image1"), Image).ImageUrl = imageUrl
            End If
        End If
    End Sub
End Class
