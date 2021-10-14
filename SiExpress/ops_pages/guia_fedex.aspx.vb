Imports System.Web.Services.Protocols
Partial Class ops_pages_guia_fedex
    Inherits BasePageNoLogin

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Es necesario agregar acceso a esta página por omision a todo usuario con acceso a exportaciones
        '*************Bloque para validar al usuario ********************
        'If Not Session("user_name") = Nothing Then
        '    Dim validador As New seguridad
        '    If Not validador.validar_usuario(Session("user_name"), Me.AppRelativeVirtualPath.ToString) Then
        '        Response.Redirect("~/acceso_denegado.aspx")
        '    End If
        'Else
        '    Response.Redirect("~/no_session.aspx")
        'End If
        '****************************************************************

        Try
            'Dim MyConnection As ConnectionStringSettings
            'MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            'Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            'connection.ConnectionString = MyConnection.ConnectionString

            'Dim cmd As Data.IDbCommand = connection.CreateCommand()
            'cmd.CommandType = Data.CommandType.StoredProcedure
            'cmd.CommandText = "dbo.sp_Select_Datos_Envio"

            'Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            'parm1.ParameterName = "@id_envio"
            'parm1.Value = Request.QueryString("id_envio")
            'cmd.Parameters.Add(parm1)

            'connection.Open()
            'Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            Dim datos_cliente As New ObjCliente
            Dim datos_destinatario As New ObjDestinatario
            Dim datos_envio As New ObjEnvio

            'If reader.HasRows Then
            '    reader.Read()
            '    'Datos del cliente
            '    datos_cliente.nombre = reader.GetString(18)
            '    datos_cliente.empresa = reader.GetString(19)
            '    datos_cliente.direccion = reader.GetString(21)
            '    datos_cliente.codigo_postal = reader.GetString(38)
            '    datos_cliente.ciudad = reader.GetString(22)
            '    datos_cliente.estadoprovincia = reader.GetString(23)
            '    datos_cliente.codigo_pais = reader.GetString(24)
            '    datos_cliente.telefono = reader.GetString(20)
            '    'datos del destinatario
            '    datos_destinatario.nombre = reader.GetString(25)
            '    datos_destinatario.empresa = reader.GetString(26)
            '    datos_destinatario.direccion = reader.GetString(28)
            '    datos_destinatario.codigo_postal = reader.GetString(39)
            '    datos_destinatario.ciudad = reader.GetString(29)
            '    datos_destinatario.estadoprovincia = reader.GetString(30)
            '    datos_destinatario.codigo_pais = reader.GetString(31)
            '    datos_destinatario.telefono = reader.GetString(27)
            '    'datos envío
            '    datos_envio.largo = reader.GetValue(32)
            '    datos_envio.ancho = reader.GetValue(33)
            '    datos_envio.alto = reader.GetValue(34)
            '    datos_envio.peso = reader.GetValue(35)
            '    datos_envio.contenido = reader.GetString(40)
            '    datos_envio.referencia = reader.GetString(37)
            '    datos_envio.valor_aduana = reader.GetValue(41)

            'End If
            'connection.Close()

            datos_envio.FedExTipo = Request.QueryString("envio_fedex")
            datos_envio.id_envio = Request.QueryString("id_envio")
            datos_envio.FedExPkg = Request.QueryString("envio_fedex_pkg")
            datos_envio.impresion_FedEx = Request.QueryString("impresion")

            Dim seguimiento As New seguimiento_envios
            seguimiento.consulta_envio(datos_envio, datos_cliente, datos_destinatario)

            Dim Ship_FedEx As New FedEx_ShipService_Int
            Dim Bytes As Byte()
            Bytes = Ship_FedEx.Main(datos_cliente, datos_destinatario, datos_envio)
            Dim utf As New System.Text.UTF7Encoding()
            Dim txt As String = utf.GetString(Bytes)
            If Not Left(txt, 4) = "%PDF" And Not Left(txt, 4) = "^XA^" Then
                'MsgBox(txt)
                Dim respuesta As String
                respuesta = "El envío no se pudo enviar al sistema de FedEx, este devolvió el siguiente mensaje:" & vbCrLf & txt & vbCrLf & "Consulte a un adminstrador"
                Response.Write(respuesta)
            Else
                'Actualizar Referencia FedEx (dentro del metodo main)
                Response.ContentType = "Application/pdf"
                'Response.ContentType = "Image/gif"
                Response.BinaryWrite(Bytes)
                Response.End()
            End If

        Catch ex As SoapException
            MsgBox(ex.Detail.InnerText)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
