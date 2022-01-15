
Imports System.IO

Partial Class Reports_PrintFedexLabel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id_envio As Integer
        Dim tipo_impresion As Integer = 2
        Dim chtLoginsByMonthStream As MemoryStream = New MemoryStream()

        Try
            If Integer.TryParse(Request.QueryString(0), id_envio) Then
                Integer.TryParse(Request.QueryString(1), tipo_impresion)

                Dim Width As Double = Double.Parse(5.3) * 72
                Dim Height As Double = Double.Parse(8.2) * 72

                If (tipo_impresion = 1) Then
                    Width = Double.Parse(11.3) * 72
                    Height = Double.Parse(15) * 72
                End If

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
