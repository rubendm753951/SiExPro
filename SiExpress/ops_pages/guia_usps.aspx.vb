Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Xml
Imports System.Web.Services.Protocols

Partial Class ops_pages_guia_usps
    Inherits BasePageNoLogin

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim usps_label As New USPS_WebTools
            Dim bytes As Byte()
            bytes = usps_label.DeliveryConfirmationLabel(Request.QueryString("id_envio"), Request.QueryString("api"))
            Dim download As String = Encoding.ASCII.GetString(bytes)

            'MsgBox(download)

            Dim stream As StringReader
            stream = New StringReader(download)
            Dim reader As XmlTextReader = New XmlTextReader(stream)

            Dim fallo As Boolean = True
            Do While (reader.Read())
                If reader.Name Like "*ConfirmationNumber" Then
                    usps_label.actualiza_referencia(reader.ReadElementContentAsString, CInt(Request.QueryString("id_envio")))
                End If
                If reader.Name Like "*ConfirmationLabel" Then
                    bytes = Convert.FromBase64String(reader.ReadElementContentAsString)
                    'MsgBox(reader.ReadElementContentAsString)
                    fallo = False
                    'Actualiza Referecia USPS
                    Exit Do
                    'TextBox1.Text = reader.ReadElementContentAsString
                End If
                'textbox1.text = textbox1.text + reader.name & vbcrlf
            Loop
            If fallo Then
                Dim stream2 As StringReader
                stream2 = New StringReader(download)
                Dim reader2 As XmlTextReader = New XmlTextReader(stream2)
                Do While (reader2.Read())
                    ' do some work here on the data.
                    If reader2.Name = "Description" Then
                        Response.Write("Error USPS API: " & reader2.ReadElementContentAsString)
                        Exit Sub
                        'TextBox1.Text = reader.ReadElementContentAsString
                    End If
                    'textbox1.text = textbox1.text + reader.name & vbcrlf
                Loop
            End If

            '************ Enviar la etiqueta a un archivo ******************************
            'Dim estado As String = reader.ReadState
            'MsgBox(estado)
            'bytes = Convert.FromBase64String(TextBox1.Text)

            'Dim outFile As System.IO.FileStream
            'outFile = New System.IO.FileStream("C:\Users\Ruben\Downloads\label.gif", _
            '                                System.IO.FileMode.Create, _
            '                                System.IO.FileAccess.Write)
            'outFile.Write(bytes, 0, bytes.Length - 1)
            'outFile.Close()
            '************ **************************************************************

            '************  Para sacar la imagen PDF en el browser  ******************
            Dim utf As New System.Text.UTF7Encoding()
            Dim txt As String = utf.GetString(bytes)
            If Not Left(txt, 4) = "%PDF" And Not Left(txt, 4) = "^XA^" Then
                'MsgBox(txt)
                Dim respuesta As String
                respuesta = "El envío no se pudo enviar al sistema del USPS, este devolvió el siguiente mensaje:" & vbCrLf & txt & vbCrLf & "Consulte a un administrador"
                Response.Write(respuesta)
            Else
                'Actualizar Referencia FedEx (dentro del metodo main)
                Response.ContentType = "Application/pdf"
                'Response.ContentType = "Image/gif"
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            '************************************************************************
  
            '************  Para sacar la imagen TIF en el browser  ******************
            'Dim ms As MemoryStream = New MemoryStream(bytes, 0, bytes.Length)
            ''// Convert byte[] to Image
            'ms.Write(bytes, 0, bytes.Length)
            'Dim image As Image = image.FromStream(ms, True, True)
            'image.GetThumbnailImage(660, 880, Nothing, IntPtr.Zero).Save(Response.OutputStream, ImageFormat.Gif)
            '**************************************************************************

        Catch ex As Exception
            Response.Write("Ocurrió un error (Tabsa app), contacte a un administrador ---> " + ex.Message.ToString)
        End Try
    End Sub
End Class
