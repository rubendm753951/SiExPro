Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Partial Class Reports_EstafetaLabelMultiple
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim chtLoginsByMonthStream As MemoryStream = New MemoryStream()
        Dim id_envio1 As Integer = Request.QueryString(0)
        Dim id_envio2 As Integer = Request.QueryString(1)
        Dim id_agente As Integer = Request.QueryString(2)
        Dim db As New DaspackDataContext
        Dim arrEnvios As ArrayList = New ArrayList()
        Dim Width As Double = Double.Parse(4) * 72
        Dim Height As Double = Double.Parse(6) * 72

        Try

            Dim result As IEnumerable(Of ImagenesEnviosEstafeta) = db.ImagenesEnviosEstafeta(id_envio1, id_envio2, id_agente)
            If result IsNot Nothing Then
                arrEnvios = New ArrayList((From i In result Select i).ToArray)
            End If

            Dim base64String As String = ""
            Dim relacion = New List(Of System.Guid)

            If arrEnvios.Count > 0 Then
                Dim count As Integer = 0
                Dim sb As New StringBuilder()
                sb.Append("<script type = 'text/javascript'>")
                For Each imgEnvio As ImagenesEnviosEstafeta In arrEnvios

                    Dim existeRelacion = relacion.FirstOrDefault(Function(x) x = imgEnvio.relacion)

                    If existeRelacion = Guid.Empty Then

                        If imgEnvio.relacion <> Guid.Empty Then
                            relacion.Add(imgEnvio.relacion)
                        End If

                        Dim url As String = "../reports/EstafetaLabel.aspx?id_envio=" + imgEnvio.id_envio.ToString()

                        sb.Append("window.open('")
                        sb.Append(url)
                        If count = 0 Then
                            sb.Append("', '_self');")
                        Else
                            sb.Append("', '_blank');")
                        End If

                        count = count + 1
                    End If
                Next
                sb.Append("</script>")
                ClientScript.RegisterStartupScript(Me.GetType(),
                              "script", sb.ToString())
            End If

        Catch ex As Exception
            Response.Write("Ocurrio un error, favor de contactar al administrador." + ex.Message + " - " + ex.Source)
        End Try
    End Sub

End Class
