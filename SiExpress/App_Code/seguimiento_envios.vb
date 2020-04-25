Imports Microsoft.VisualBasic
Imports ObjDestinatario, ObjCliente, ObjEnvio
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System

Public Class seguimiento_envios
    Inherits System.Web.UI.Page
    Sub insertar_seguimiento(ByVal id_envio As Integer, ByVal modulo As String, ByVal observaciones As String, ByVal id_usuario As Integer, Optional ByVal promotor As String = "")
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_Seguimiento"

        If promotor > "" Then
            observaciones = promotor
        End If

        'Par�metros de entrada 
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@modulo"
        parm2.Value = modulo
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_usuario"
        parm3.Value = id_usuario
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@observaciones"
        parm4.Value = observaciones
        cmd.Parameters.Add(parm4)


        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
    Sub consulta_envio(ByRef datos_envio As ObjEnvio, ByRef datos_cliente As ObjCliente, ByRef datos_destinatario As ObjDestinatario)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Select_Datos_Envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = datos_envio.id_envio  'Request.QueryString("id_envio")
        cmd.Parameters.Add(parm1)

        connection.Open()
        Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
        'Dim datos_cliente As New ObjCliente
        'Dim datos_destinatario As New ObjDestinatario
        'Dim datos_envio As New ObjEnvio

        If reader.HasRows Then
            reader.Read()
            'Datos del cliente
            datos_cliente.nombre = reader.GetString(18)
            datos_cliente.empresa = reader.GetString(19)
            datos_cliente.direccion = reader.GetString(21)
            datos_cliente.codigo_postal = reader.GetString(38)
            datos_cliente.ciudad = reader.GetString(22)
            datos_cliente.estadoprovincia = reader.GetString(23)
            datos_cliente.codigo_pais = reader.GetString(24)
            datos_cliente.telefono = reader.GetString(20)
            'datos del destinatario
            datos_destinatario.nombre = reader.GetString(25)
            datos_destinatario.empresa = reader.GetString(26)
            datos_destinatario.direccion = reader.GetString(28)
            datos_destinatario.codigo_postal = reader.GetString(39)
            datos_destinatario.ciudad = reader.GetString(29)
            datos_destinatario.estadoprovincia = reader.GetString(30)
            datos_destinatario.codigo_pais = reader.GetString(31)
            datos_destinatario.telefono = reader.GetString(27)
            'datos env�o
            datos_envio.largo = reader.GetValue(32)
            datos_envio.ancho = reader.GetValue(33)
            datos_envio.alto = reader.GetValue(34)
            datos_envio.peso = reader.GetValue(35)
            datos_envio.contenido = reader.GetString(40)
            datos_envio.referencia = reader.GetString(37)
            datos_envio.valor_aduana = reader.GetValue(41)
            datos_envio.id_tarifa_agencia = reader.GetValue(51)

        End If
        connection.Close()

        'datos_envio.FedExTipo = Request.QueryString("envio_fedex")
        'datos_envio.id_envio = Request.QueryString("id_envio")
        'datos_envio.FedExPkg = Request.QueryString("envio_fedex_pkg")
        'datos_envio.impresion_FedEx = Request.QueryString("impresion")

        'Dim Ship_FedEx As New FedEx_ShipService_Int
        'Dim Bytes As Byte()
        'Bytes = Ship_FedEx.Main(datos_cliente, datos_destinatario, datos_envio)
        'Dim utf As New System.Text.UTF7Encoding()
        'Dim txt As String = utf.GetString(Bytes)
        'If Not Left(txt, 4) = "%PDF" And Not Left(txt, 4) = "^XA^" Then
        '    'MsgBox(txt)
        '    Dim respuesta As String
        '    respuesta = "El env�o no se pudo enviar al sistema de FedEx, este devolvi� el siguiente mensaje:" & vbCrLf & txt & vbCrLf & "Consulte a un adminstrador"
        '    Response.Write(respuesta)
        'Else
        '    'Actualizar Referencia FedEx (dentro del metodo main)
        '    Response.ContentType = "Application/pdf"
        '    'Response.ContentType = "Image/gif"
        '    Response.BinaryWrite(Bytes)
        '    Response.End()
        'End If
    End Sub
    Public Function insertar_asignacion(ByVal id_envio As Integer, ByVal id_promotor As Integer, ByVal id_usuario As Integer) As String
        Dim salida As String
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_insert_asignacion_entrega"

        'Par�metros de entrada 
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_promotor"
        parm2.Value = id_promotor
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_usuario"
        parm3.Value = id_usuario
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@respuesta"
        parm4.Size = 50
        cmd.Parameters.Add(parm4)
        parm4.Direction = Data.ParameterDirection.Output

        connection.Open()
        cmd.ExecuteNonQuery()
        salida = parm4.Value
        connection.Close()
        Return salida
    End Function
    'Public Function valida_flujo(ByVal id_envio As Integer, ByVal modulo As String) As String
    '    'definir temporalmenete en esta fucni�n las reglas para cambio de status
    '    Dim current_status As Integer = 0
    '    Dim message As String
    '    Dim MyConnection As ConnectionStringSettings
    '    MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
    '    Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
    '    connection.ConnectionString = MyConnection.ConnectionString

    '    Dim cmd As Data.IDbCommand = connection.CreateCommand()
    '    cmd.CommandType = Data.CommandType.StoredProcedure
    '    cmd.CommandText = "dbo.sp_Select_Datos_Envio"

    '    Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
    '    parm1.ParameterName = "@id_envio"
    '    parm1.Value = id_envio  'Request.QueryString("id_envio")
    '    cmd.Parameters.Add(parm1)

    '    connection.Open()
    '    Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()

    '    If reader.HasRows Then
    '        reader.Read()
    '        current_status = reader.GetValue(44)
    '    End If

    '    connection.Close()

    '    Select Case current_status
    '        Case 400
    '            message = "Este envio ya ha sido entregado"
    '        Case 500
    '            message = "Este envio ha sido cancelado anteriormente"
    '        Case Else
    '            message = "ok"
    '    End Select
    '    Return message
    'End Function
    Public Function valida_referencia(ByVal envio As String) As Integer
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_select_envio_referencia"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@envio"
            parm1.Value = envio  'Request.QueryString("id_envio")
            cmd.Parameters.Add(parm1)

            Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            parm2.ParameterName = "@id_envio"
            parm2.Size = 100
            parm2.Direction = Data.ParameterDirection.Output
            cmd.Parameters.Add(parm2)

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()

            Return parm2.Value
        Catch ex As Exception
            Return 0
        End Try

    End Function
    Sub valida_flujo(ByVal id_envio As Integer, ByVal modulo As String, ByRef mensaje As String, ByRef permitido As Boolean)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_valida_flujo"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio  'Request.QueryString("id_envio")
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@modulo"
        parm2.Value = modulo  'Request.QueryString("id_envio")
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@permitido"
        parm3.Size = 10
        parm3.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@mensaje"
        parm4.Size = 100
        parm4.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm4)

        connection.Open()
        cmd.ExecuteNonQuery()

        permitido = parm3.Value
        mensaje = parm4.Value

        connection.Close()
    End Sub
    Sub elimina_modifica(ByVal id_envio As Integer, ByVal id_usuario As Integer, ByVal movimiento As String, ByRef mensaje As String)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_change_status_envio"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_usuario"
        parm2.Value = id_usuario
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@movimiento"
        parm3.Value = movimiento
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@respuesta"
        parm4.Size = 200
        parm4.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm4)

        connection.Open()
        cmd.ExecuteNonQuery()

        mensaje = parm4.Value

        connection.Close()
    End Sub

    Sub actualiza_FedEx(ByVal id_envio As Integer, ByVal referencia_fedex As String)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_UpdateReferenciaFedeEx"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@ReferenciaFedEx"
        parm2.Value = referencia_fedex
        cmd.Parameters.Add(parm2)

        connection.Open()
        cmd.ExecuteNonQuery()

        connection.Close()
    End Sub
End Class
