
Imports System.IO

Partial Class Reports_PrintFedexLabel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id_envio As Integer
        Dim chtLoginsByMonthStream As MemoryStream = New MemoryStream()

        Dim Width As Double = Double.Parse(4) * 72
        Dim Height As Double = Double.Parse(6) * 72

        Try
            If Integer.TryParse(Request.QueryString(0), id_envio) Then
                Dim estafetaLabel = DaspackDALC.EstafetaLabel(id_envio)
                If estafetaLabel IsNot Nothing Then
                    chtLoginsByMonthStream.Position = 0

                    'Read the entire stream
                    chtLoginsByMonthStream.Read(estafetaLabel.labelPDF, 0, estafetaLabel.labelPDF.Length)

                    'Put the byte array into the new datarow in the appropriate column
                    Dim image As New Image
                    Dim base64String As String = Convert.ToBase64String(estafetaLabel.labelPDF, 0, estafetaLabel.labelPDF.Length)
                    image.Width = CType(Width, Unit)
                    image.Height = CType(Height, Unit)

                    image.ImageUrl = "data:image/png;base64," & base64String

                    image.Visible = True


                    FedexLabelPanel.Controls.Add(image)

                    'PrinterHelper.PrintWebControl(FedexLabelPanel)

                End If
            End If

        Catch ex As Exception
            Response.Write("Ocurrio un error, favor de contactar al administrador." + ex.Message + " - " + ex.Source)
        End Try
    End Sub
End Class
