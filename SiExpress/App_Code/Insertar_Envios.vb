Imports System.Activities.Expressions
Imports Microsoft.VisualBasic
Imports ObjDestinatario, ObjCliente, ObjEnvio
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System

Public Class Insertar_Envios
    Inherits System.Web.UI.Page
    Public Function preasignar_envios(ByVal id_agente As Integer, ByVal id_envio_inicial As Integer, ByVal id_envio_final As Integer) As Integer
        Dim mensaje As Integer = 0, existentes As Integer
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        'ejecuta SP para Insertar nuevo destinatario
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_guias_preasignadas"

        'Parámetros de entrada par insertar
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio_inicial"
        parm1.Value = id_envio_inicial
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_envio_final"
        parm2.Value = id_envio_final
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_agencia"
        parm3.Value = id_agente
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@err"
        parm4.Size = 10
        parm4.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm4)
        'Valida datos de entrasa

        'ejecuta SP para validar si el rango esta disponible
        Dim cmd2 As Data.IDbCommand = connection.CreateCommand()
        cmd2.CommandType = Data.CommandType.StoredProcedure
        cmd2.CommandText = "dbo.sp_busca_guias_asignadas"

        'Parámetros de entrada para validar que no hay guias usadas en el rango
        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@id_envio_inicial"
        parm5.Value = id_envio_inicial
        cmd2.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@id_envio_final"
        parm6.Value = id_envio_final
        cmd2.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@id_agente"
        parm7.Value = id_agente
        cmd2.Parameters.Add(parm7)

        connection.Open()
        Dim reader As Data.SqlClient.SqlDataReader = cmd2.ExecuteReader()
        reader.Read()
        existentes = reader.GetInt32(0)
        connection.Close()
        If id_envio_final < id_envio_inicial Or id_envio_final - id_envio_inicial > 1000 Then
            mensaje = 1000 '"El rango es incorrecto o mayor de 1000 envíos" 
        ElseIf existentes > 0 Then
            mensaje = 1001 '"Alguna(as) de las guías del rango ya han sido asignadas"
        Else
            mensaje = 9000 '"Validación correcta"
            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
        End If
        Return mensaje

    End Function
    Public Function crea_destinatario(ByVal dest_datos As ObjDestinatario) As Integer
        Dim id_destinatario As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        'ejecuta SP para Insertar nuevo destinatario
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_Destinatario"

        'Parámetros de entrada
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_pais"
        parm1.Value = dest_datos.id_pais
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@nombre"
        parm2.Value = dest_datos.nombre
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@apellidos"
        parm3.Value = dest_datos.apellidos
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@empresa"
        parm4.Value = dest_datos.empresa
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@calle"
        parm5.Value = dest_datos.calle
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@noexterior"
        parm6.Value = dest_datos.noexterior
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@nointerior"
        parm7.Value = dest_datos.nointerior
        cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@direccion2"
        parm8.Value = dest_datos.direccion2
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@colonia"
        parm9.Value = dest_datos.colonia
        cmd.Parameters.Add(parm9)

        Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
        parm10.ParameterName = "@ciudad"
        parm10.Value = dest_datos.ciudad
        cmd.Parameters.Add(parm10)

        Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
        parm11.ParameterName = "@municipio"
        parm11.Value = dest_datos.municipio
        cmd.Parameters.Add(parm11)

        Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
        parm12.ParameterName = "@estadoprovincia"
        parm12.Value = dest_datos.estadoprovincia
        cmd.Parameters.Add(parm12)

        Dim parm13 As Data.Common.DbParameter = cmd.CreateParameter()
        parm13.ParameterName = "@telefono"
        parm13.Value = dest_datos.telefono
        cmd.Parameters.Add(parm13)

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@codigo_postal"
        parm14.Value = dest_datos.codigo_postal
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@email"
        parm15.Value = dest_datos.email
        cmd.Parameters.Add(parm15)

        Dim parm16 As Data.Common.DbParameter = cmd.CreateParameter()
        parm16.ParameterName = "@id_destinatario"
        parm16.Size = 10
        parm16.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm16)

        connection.Open()
        cmd.ExecuteNonQuery()
        id_destinatario = parm16.Value
        connection.Close()

        Return id_destinatario
        Session("id_destinatario") = id_destinatario

    End Function
    Public Function crea_cliente(ByVal cliente_datos As ObjCliente) As Integer
        Dim id_cliente As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        'ejecuta SP para Insertar nuevo destinatario
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_Cliente"

        'Parámetros de entrada
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_pais"
        parm1.Value = cliente_datos.id_pais
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@nombre"
        parm2.Value = cliente_datos.nombre
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@apellidos"
        parm3.Value = cliente_datos.apellidos
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@empresa"
        parm4.Value = cliente_datos.empresa
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@calle"
        parm5.Value = cliente_datos.calle
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@noexterior"
        parm6.Value = cliente_datos.noexterior
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@nointerior"
        parm7.Value = cliente_datos.nointerior
        cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@direccion2"
        parm8.Value = cliente_datos.direccion2
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@colonia"
        parm9.Value = cliente_datos.colonia
        cmd.Parameters.Add(parm9)

        Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
        parm10.ParameterName = "@ciudad"
        parm10.Value = cliente_datos.ciudad
        cmd.Parameters.Add(parm10)

        Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
        parm11.ParameterName = "@municipio"
        parm11.Value = cliente_datos.municipio
        cmd.Parameters.Add(parm11)

        Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
        parm12.ParameterName = "@estadoprovincia"
        parm12.Value = cliente_datos.estadoprovincia
        cmd.Parameters.Add(parm12)

        Dim parm13 As Data.Common.DbParameter = cmd.CreateParameter()
        parm13.ParameterName = "@telefono"
        parm13.Value = cliente_datos.telefono
        cmd.Parameters.Add(parm13)

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@codigo_postal"
        parm14.Value = cliente_datos.codigo_postal
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@email"
        parm15.Value = cliente_datos.email
        cmd.Parameters.Add(parm15)

        Dim parm16 As Data.Common.DbParameter = cmd.CreateParameter()
        parm16.ParameterName = "@id_cliente"
        parm16.Size = 10
        parm16.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm16)

        connection.Open()
        cmd.ExecuteNonQuery()
        id_cliente = parm16.Value
        connection.Close()

        Return id_cliente
        Session("id_cliente") = id_cliente
    End Function

    Public Function Update_PreRegistro_Envios(ByVal id_agente As Integer, ByVal datos_envio As ObjEnvio, ByVal id_envio_imp As Integer) As Integer

        'Recuperar el ID de la tarifa
        Dim id_envio As Integer
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Update_Envios"

        'Parámetros de entrada --El agente pasa como argumento de lallamda del método
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_agente"
        parm1.Value = id_agente 'datos_envio.id_agente
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@precio"
        parm2.Value = datos_envio.precio
        cmd.Parameters.Add(parm2)

        Dim parm13 As Data.Common.DbParameter = cmd.CreateParameter()
        parm13.ParameterName = "@valor_seguro"
        parm13.Value = datos_envio.valor_seguro
        cmd.Parameters.Add(parm13)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_tarifa_agencia"
        parm3.Value = datos_envio.id_tarifa_agencia  'datos_envio.id_tarifa_agencia (Pendiente!!!!!!) En caso que necesitemos el id_tarifa
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@id_codigo_promocion"
        parm4.Value = datos_envio.id_codigo_promocion
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@total_envio"
        parm5.Value = datos_envio.total_envio
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@fecha"
        parm6.Value = datos_envio.fecha
        cmd.Parameters.Add(parm6)

        'Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter() --NO Aplica en este método
        'parm7.ParameterName = "@fecha_corte"
        'parm7.Value = datos_envio.fecha_corte
        'cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@instrucciones_entrega"
        parm8.Value = datos_envio.instrucciones_entrega
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@observaciones"
        parm9.Value = datos_envio.observaciones
        cmd.Parameters.Add(parm9)

        Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
        parm10.ParameterName = "@id_usuario"
        parm10.Value = datos_envio.id_usuario
        cmd.Parameters.Add(parm10)

        Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
        parm11.ParameterName = "@id_ruta"
        parm11.Value = datos_envio.id_ruta
        cmd.Parameters.Add(parm11)

        Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
        parm12.ParameterName = "@id_envio"
        parm12.Value = datos_envio.id_envio
        cmd.Parameters.Add(parm12)

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@largo"
        parm14.Value = datos_envio.largo
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@ancho"
        parm15.Value = datos_envio.ancho
        cmd.Parameters.Add(parm15)

        Dim parm16 As Data.Common.DbParameter = cmd.CreateParameter()
        parm16.ParameterName = "@alto"
        parm16.Value = datos_envio.alto
        cmd.Parameters.Add(parm16)

        Dim parm17 As Data.Common.DbParameter = cmd.CreateParameter()
        parm17.ParameterName = "@peso"
        parm17.Value = datos_envio.peso
        cmd.Parameters.Add(parm17)

        Dim parm18 As Data.Common.DbParameter = cmd.CreateParameter()
        parm18.ParameterName = "@id_contenido"
        parm18.Value = datos_envio.contenido
        cmd.Parameters.Add(parm18)

        Dim parm19 As Data.Common.DbParameter = cmd.CreateParameter()
        parm19.ParameterName = "@referencia1"
        parm19.Value = datos_envio.referencia
        cmd.Parameters.Add(parm19)

        Dim parm20 As Data.Common.DbParameter = cmd.CreateParameter()
        parm20.ParameterName = "@valor_aduana"
        parm20.Value = datos_envio.valor_aduana
        cmd.Parameters.Add(parm20)

        Dim parm21 As Data.Common.DbParameter = cmd.CreateParameter()
        parm21.ParameterName = "@contenedores"
        parm21.Value = datos_envio.contenedores
        cmd.Parameters.Add(parm21)

        Dim parm22 As Data.Common.DbParameter = cmd.CreateParameter()
        parm22.ParameterName = "@id_envio_imp"
        parm22.Value = id_envio_imp
        cmd.Parameters.Add(parm22)

        Dim parm23 As Data.Common.DbParameter = cmd.CreateParameter()
        parm23.ParameterName = "@ref_fedex"
        parm23.Value = datos_envio.FedExRef
        cmd.Parameters.Add(parm23)


        connection.Open()
        cmd.ExecuteNonQuery()
        id_envio = parm12.Value
        connection.Close()

        Return id_envio

    End Function

    Public Function PreRegistro_Envios(ByVal id_agente As Integer, ByVal datos_envio As ObjEnvio, ByVal id_envio_imp As Integer) As Integer

        'Recuperar el ID de la tarifa
        Dim id_envio As Integer
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_Envios"

        'Parámetros de entrada --El agente pasa como argumento de lallamda del método
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_agente"
        parm1.Value = id_agente 'datos_envio.id_agente
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@precio"
        parm2.Value = datos_envio.precio
        cmd.Parameters.Add(parm2)

        Dim parm13 As Data.Common.DbParameter = cmd.CreateParameter()
        parm13.ParameterName = "@valor_seguro"
        parm13.Value = datos_envio.valor_seguro
        cmd.Parameters.Add(parm13)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_tarifa_agencia"
        parm3.Value = datos_envio.id_tarifa_agencia  'datos_envio.id_tarifa_agencia (Pendiente!!!!!!) En caso que necesitemos el id_tarifa
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@id_codigo_promocion"
        parm4.Value = datos_envio.id_codigo_promocion
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@total_envio"
        parm5.Value = datos_envio.total_envio
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@fecha"
        parm6.Value = datos_envio.fecha
        cmd.Parameters.Add(parm6)

        'Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter() --NO Aplica en este método
        'parm7.ParameterName = "@fecha_corte"
        'parm7.Value = datos_envio.fecha_corte
        'cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@instrucciones_entrega"
        parm8.Value = datos_envio.instrucciones_entrega
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@observaciones"
        parm9.Value = datos_envio.observaciones
        cmd.Parameters.Add(parm9)

        Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
        parm10.ParameterName = "@id_usuario"
        parm10.Value = datos_envio.id_usuario
        cmd.Parameters.Add(parm10)

        Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
        parm11.ParameterName = "@id_ruta"
        parm11.Value = datos_envio.id_ruta
        cmd.Parameters.Add(parm11)

        Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
        parm12.ParameterName = "@id_envio"
        parm12.Size = 10
        parm12.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm12)

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@largo"
        parm14.Value = datos_envio.largo
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@ancho"
        parm15.Value = datos_envio.ancho
        cmd.Parameters.Add(parm15)

        Dim parm16 As Data.Common.DbParameter = cmd.CreateParameter()
        parm16.ParameterName = "@alto"
        parm16.Value = datos_envio.alto
        cmd.Parameters.Add(parm16)

        Dim parm17 As Data.Common.DbParameter = cmd.CreateParameter()
        parm17.ParameterName = "@peso"
        parm17.Value = datos_envio.peso
        cmd.Parameters.Add(parm17)

        Dim parm18 As Data.Common.DbParameter = cmd.CreateParameter()
        parm18.ParameterName = "@id_contenido"
        parm18.Value = datos_envio.contenido
        cmd.Parameters.Add(parm18)

        Dim parm19 As Data.Common.DbParameter = cmd.CreateParameter()
        parm19.ParameterName = "@referencia1"
        parm19.Value = datos_envio.referencia
        cmd.Parameters.Add(parm19)

        Dim parm20 As Data.Common.DbParameter = cmd.CreateParameter()
        parm20.ParameterName = "@valor_aduana"
        parm20.Value = datos_envio.valor_aduana
        cmd.Parameters.Add(parm20)

        Dim parm21 As Data.Common.DbParameter = cmd.CreateParameter()
        parm21.ParameterName = "@contenedores"
        parm21.Value = datos_envio.contenedores
        cmd.Parameters.Add(parm21)

        Dim parm22 As Data.Common.DbParameter = cmd.CreateParameter()
        parm22.ParameterName = "@id_envio_imp"
        parm22.Value = id_envio_imp
        cmd.Parameters.Add(parm22)

        Dim parm23 As Data.Common.DbParameter = cmd.CreateParameter()
        parm23.ParameterName = "@ref_fedex"
        parm23.Value = datos_envio.FedExRef
        cmd.Parameters.Add(parm23)


        connection.Open()
        cmd.ExecuteNonQuery()
        id_envio = parm12.Value
        connection.Close()

        Return id_envio

    End Function
    Sub Detalle_Envios(ByVal id_envio As Integer, ByVal datos_envio As ObjEnvio, id_contenido As Integer, observaciones As String)
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_Envios_Datos"

        'Parámetros de entrada 
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio 'datos_envio.id_agente
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_cliente"
        parm2.Value = datos_envio.id_cliente
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_destinatario"
        parm3.Value = datos_envio.id_destinatario
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@id_contenido"
        parm4.Value = id_contenido
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@observaciones"
        parm5.Value = observaciones
        cmd.Parameters.Add(parm5)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub
    Sub inserta_SobreCargos(ByVal id_envio As Integer)
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_sobrecargos_envios"

        'Parámetros de entrada 
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_envio"
        parm1.Value = id_envio
        cmd.Parameters.Add(parm1)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
    Public Function valida_preregistro(ByVal datos_envio As ObjEnvio) As String
        Dim idAgente As Integer = CType(datos_envio.id_agente, Integer)
        Dim esAgenteCod As Boolean = AgenteCOD(idAgente)
        Dim valorCOD As Integer = 0
        Integer.TryParse(ConfigurationManager.AppSettings("ValorCOD"), valorCOD)

        If datos_envio.precio Is Nothing Then
            datos_envio.precio = 0
        End If

        If datos_envio.id_agente = 0 Or datos_envio.id_agente Is Nothing Then
            Return "Seleccione un gente"
        ElseIf datos_envio.id_tarifa_agencia = 0 Or datos_envio.id_tarifa_agencia Is Nothing Then
            Return "EL producto es inválido"
        'ElseIf datos_envio.precio = 0 And Not esAgenteCod Then
        '    Return "La tarifa es incorrecta, seleccione un subprodcuto"
        ElseIf datos_envio.largo Is Nothing Or datos_envio.ancho Is Nothing _
            Or datos_envio.alto Is Nothing Or datos_envio.peso Is Nothing Then
            Return "Las dimensiones del paquete son incorrectas"
        ElseIf datos_envio.dimension_peso = "oz" And datos_envio.peso < 1 Then
            Return "El peso es en Onzas  debe ser al menos 1.0"
        ElseIf esAgenteCod And datos_envio.valor_seguro < valorCOD Then
            Return "Valor COD no puede ser menor que " + valorCOD.ToString() + "."
        Else
            Return "OK"
        End If
    End Function

    Public Function AgenteCOD(ByVal idAgente As Integer) As Boolean
        Dim agentes As String = ConfigurationManager.AppSettings("AgentsCOD")
        Dim cod As Boolean = False
        If agentes IsNot Nothing AndAlso agentes <> "" Then
            Dim arrayAgentes = agentes.Split(",")

            For Each agente As String In arrayAgentes
                Dim a As Integer = 0
                Integer.TryParse(agente, a)

                If a = idAgente Then
                    cod = True
                    Exit For
                End If
            Next
        End If

        Return cod
    End Function
    Public Function valida_datos_cliente(ByVal datosCliente As ObjCliente) As String
        If datosCliente.nombre Is Nothing Or Len(datosCliente.nombre) < 3 _
          Or datosCliente.apellidos Is Nothing Or Len(datosCliente.apellidos) < 3 Then
            Return "El nombre o apellido del cliente están incompletos"
        ElseIf datosCliente.calle Is Nothing Or Len(datosCliente.calle) < 3 Then
            Return "La dirección del cliente es incorrrecta o falta"
        ElseIf Not IsNumeric(datosCliente.noexterior) Then
            Return "Falta el número de la calle del cliente"
        ElseIf datosCliente.ciudad Is Nothing Or Len(datosCliente.ciudad) < 3 Then
            Return "La ciudad del cliente es incorrecta o falta"
        ElseIf datosCliente.estadoprovincia Is Nothing Or Len(datosCliente.estadoprovincia) < 2 Then
            Return "Estado o provincia del cliente esincorrecta o falta"
        ElseIf datosCliente.codigo_postal Is Nothing Or Len(datosCliente.codigo_postal) < 5 Then
            Return "El código postal del cliente es incorrecto"
        Else
            Return "OK"
        End If
    End Function
    Public Function valida_datos_dest(ByVal datosDest As ObjDestinatario) As String
        If datosDest.nombre Is Nothing Or Len(datosDest.nombre) < 3 Then
            'Or datosDest.apellidos Is Nothing Or Len(datosDest.apellidos) < 3 Then
            Return "El nombre del destinatario están incompletos"
        ElseIf datosDest.calle Is Nothing Or Len(datosDest.calle) < 3 Then
            Return "La dirección del destinatario es incorrecto o falta"
        ElseIf Not IsNumeric(datosDest.noexterior) Then
            Return "Falta el número de la calle del destinatario"
        ElseIf datosDest.ciudad Is Nothing Or Len(datosDest.ciudad) < 3 Then
            Return "La ciudad del destinatario es incorrecta o falta"
        ElseIf datosDest.estadoprovincia Is Nothing Or Len(datosDest.estadoprovincia) < 2 Then
            Return "Estado o provincia del destinatario esincorrecta o falta"
        ElseIf datosDest.codigo_postal Is Nothing Or Len(datosDest.codigo_postal) < 4 Then
            Return "El código postal del destinatario es incorrecto"
        Else
            Return "OK"
        End If
    End Function

    Public Function valida_datos_libro_direcciones(ByVal datosDest As addressBook) As String
        'If datosDest.Destinatario Is Nothing Or Len(datosDest.Destinatario) < 3 Then
        '    Return "El nombre del destinatario están incompletos"
        'Else
        If datosDest.Contenedor Is Nothing Or Len(datosDest.Contenedor) < 2 Then
            Return "Numero de contenedor incorrecto"
        ElseIf datosDest.Inventario Is Nothing Or Len(datosDest.Inventario) < 2 Then
            Return "Numero de iventario incorrecto"
        ElseIf datosDest.Ciudad Is Nothing Or Len(datosDest.Ciudad) < 3 Then
            Return "La ciudad del destinatario es incorrecta o falta"
        ElseIf datosDest.Estado Is Nothing Or Len(datosDest.Estado) < 2 Then
            Return "Estado o provincia del destinatario esincorrecta o falta"
        Else
            Return "OK"
        End If
    End Function

    Public Function valida_cp_mx(ByVal ciudad As String, ByVal estado As String) As String
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        'ejecuta SP para Insertar nuevo destinatario
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_busca_cp_mx"

        'Parámetros de entrada
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@ciudad"
        parm1.Value = ciudad
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@estado"
        parm2.Value = estado
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@cp"
        parm3.Size = 100
        parm3.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm3)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

        Return parm3.Value

    End Function
    Sub Insertar_tmp(ByVal campos() As String, ByVal id_usuario As Integer, ByRef mensaje As String)
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString
            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "sp_insert_datos_temporales"

            'Parámetros de entrada 
            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "campo1"
            parm1.Value = campos(0)
            cmd.Parameters.Add(parm1)

            Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            parm2.ParameterName = "campo2"
            parm2.Value = campos(1)
            cmd.Parameters.Add(parm2)

            Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
            parm3.ParameterName = "campo3"
            parm3.Value = campos(2)
            cmd.Parameters.Add(parm3)

            Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
            parm4.ParameterName = "campo4"
            parm4.Value = campos(3)
            cmd.Parameters.Add(parm4)

            Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
            parm5.ParameterName = "campo5"
            parm5.Value = campos(4)
            cmd.Parameters.Add(parm5)

            Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
            parm6.ParameterName = "campo6"
            parm6.Value = campos(5)
            cmd.Parameters.Add(parm6)

            Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
            parm7.ParameterName = "campo7"
            parm7.Value = campos(6)
            cmd.Parameters.Add(parm7)

            Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
            parm8.ParameterName = "campo8"
            parm8.Value = campos(7)
            cmd.Parameters.Add(parm8)

            Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
            parm9.ParameterName = "campo9"
            parm9.Value = campos(8)
            cmd.Parameters.Add(parm9)

            Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
            parm10.ParameterName = "campo10"
            parm10.Value = campos(9)
            cmd.Parameters.Add(parm10)

            Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
            parm11.ParameterName = "id_usuario"
            parm11.Value = id_usuario
            cmd.Parameters.Add(parm11)

            Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
            parm12.ParameterName = "campo11"
            parm12.Value = campos(10)
            cmd.Parameters.Add(parm12)

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
            mensaje = "ok"

        Catch ex As Exception
            mensaje = "Ocurrió un error al insertar los datos -->" + ex.Message.ToString
        End Try
    End Sub
    Sub borrar_tmp(ByVal id As Integer, ByRef mensaje_borrar As String)
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString
            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "sp_delete_tmp_importar"

            'Parámetros de entrada 
            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "id"
            parm1.Value = id
            cmd.Parameters.Add(parm1)

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
            mensaje_borrar = "ok"

        Catch ex As Exception
            mensaje_borrar = "Ocurrió un error al insertar los datos -->" + ex.Message.ToString
        End Try


    End Sub
End Class
