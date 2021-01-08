Imports System.IO

Partial Class Reports_EstafetaLabel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id_envio As Integer

        Try
            If Integer.TryParse(Request.QueryString(0), id_envio) Then
                Dim estafetaLabel = DaspackDALC.EstafetaLabel(id_envio)
                If estafetaLabel IsNot Nothing Then
                    Dim base64String As String = Convert.ToBase64String(estafetaLabel.labelPDF, 0, estafetaLabel.labelPDF.Length)

                    If base64String = "" Then
                        Response.Write("Etiqueta no disponible.")
                    Else
                        Response.Buffer = True
                        Response.Clear()
                        Response.ClearContent()
                        Response.ClearHeaders()
                        Response.ContentType = "application/pdf"
                        Response.BinaryWrite(System.Convert.FromBase64String(base64String))
                        Response.End()
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write("Ocurrio un error, favor de contactar al administrador." + ex.Message + " - " + ex.Source)
        End Try
    End Sub
End Class
