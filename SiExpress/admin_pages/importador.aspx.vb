Imports System.IO
Imports System.Data

Partial Class importador
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim stream As FileStream = File.Open("C:\Users\Ruben\importar.xls", FileMode.Open, FileAccess.Read)

        Dim arrSentences() As String ' Create a String Array
        Dim arrFields() As String
        Dim importar As New Insertar_Envios
        Dim mensaje As String = ""
        Dim salida As String = ""
        Dim id_usuario As Integer = 1 'Session("id_usuario")

        TextBox1.Text = Replace(TextBox1.Text, vbCr, vbLf)
        TextBox1.Text = Replace(TextBox1.Text, vbLf & vbLf, vbLf)
        arrSentences = Split(Trim(TextBox1.Text), vbLf)
        For i = 0 To arrSentences.Length - 1
            'MsgBox(arrSentences(i))
            arrFields = Split(Trim(arrSentences(i)), vbTab)
            importar.Insertar_tmp(arrFields, id_usuario, mensaje)
            salida = salida & "Importar Línea " & i & " Mensaje = " & mensaje & vbCrLf
        Next i
        TextBox1.Text = salida
        DSDatosImportar.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub DSDatosImportar_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles DSDatosImportar.Deleting
        'For x As Integer = 0 To e.Command.Parameters.Count - 1
        '    Trace.Write(e.Command.Parameters(x).ParameterName)
        '    Trace.Write(e.Command.Parameters(x).Value)
        'Next
    End Sub
End Class

