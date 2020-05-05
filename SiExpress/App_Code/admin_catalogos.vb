Imports Microsoft.VisualBasic

Public Class admin_catalogos
    Public Function insertar_agente(ByRef agente As ObjAgente) As Integer
        Dim nuevo_agente As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_InsertAgencias"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_corporativo"
        parm1.Value = agente.id_corporativo
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_pais"
        parm2.Value = agente.id_pais
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@nombre"
        parm3.Value = agente.nombre
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@direccion"
        parm4.Value = agente.direccion
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@estado_provincia"
        parm5.Value = agente.provincia
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@ciudad"
        parm6.Value = agente.ciudad
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@fecha_alta"
        parm7.Value = agente.fecha_alta
        cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@fecha_termino"
        parm8.Value = agente.fecha_termino
        cmd.Parameters.Add(parm8)

        Dim parm9 As Data.Common.DbParameter = cmd.CreateParameter()
        parm9.ParameterName = "@id_moneda"
        parm9.Value = agente.id_moneda
        cmd.Parameters.Add(parm9)

        Dim parm10 As Data.Common.DbParameter = cmd.CreateParameter()
        parm10.ParameterName = "@limite_credito"
        parm10.Value = agente.limite_de_credito
        cmd.Parameters.Add(parm10)

        Dim parm11 As Data.Common.DbParameter = cmd.CreateParameter()
        parm11.ParameterName = "@NIT"
        parm11.Value = agente.NIT
        cmd.Parameters.Add(parm11)

        Dim parm12 As Data.Common.DbParameter = cmd.CreateParameter()
        parm12.ParameterName = "@telefono"
        parm12.Value = agente.telefono
        cmd.Parameters.Add(parm12)

        Dim parm21 As Data.Common.DbParameter = cmd.CreateParameter()
        parm21.ParameterName = "@requiere_asignacion"
        parm21.Value = agente.requiere_asignacion
        cmd.Parameters.Add(parm21)

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@factor"
        parm14.Value = agente.Factor
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@esquema_por_factor"
        parm15.Value = agente.EsquemaPorFactor
        cmd.Parameters.Add(parm15)

        Dim parm13 As Data.Common.DbParameter = cmd.CreateParameter()
        parm13.ParameterName = "@id_agencia"
        parm13.Size = 20
        cmd.Parameters.Add(parm13)
        parm13.Direction = Data.ParameterDirection.Output

        connection.Open()
        cmd.ExecuteNonQuery()
        nuevo_agente = parm13.Value 'Parametros de salida para leer el agente qu se acaba de crear
        agente.id_agente = parm13.Value
        connection.Close()

        Return nuevo_agente
    End Function
    Public Sub insetar_tarifas(agente As ObjAgente)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_InsertTarifas_Default_Agente"

        Dim parm14 As Data.Common.DbParameter = cmd.CreateParameter()
        parm14.ParameterName = "@id_tipo"
        parm14.Value = agente.id_tarifa_tipo
        cmd.Parameters.Add(parm14)

        Dim parm15 As Data.Common.DbParameter = cmd.CreateParameter()
        parm15.ParameterName = "@id_agencia"
        parm15.Value = agente.id_agente
        cmd.Parameters.Add(parm15)

        Dim parm16 As Data.Common.DbParameter = cmd.CreateParameter()
        parm16.ParameterName = "@factor"
        parm16.Value = agente.factor_tarifa
        cmd.Parameters.Add(parm16)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub
    Public Sub insertar_comisiones(agente As ObjAgente)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_InsertComm_Default_Agente"

        Dim parm17 As Data.Common.DbParameter = cmd.CreateParameter()
        parm17.ParameterName = "@id_agencia"
        parm17.Value = agente.id_agente
        cmd.Parameters.Add(parm17)

        Dim parm18 As Data.Common.DbParameter = cmd.CreateParameter()
        parm18.ParameterName = "@valor_moneda"
        parm18.Value = agente.comision_moneda
        cmd.Parameters.Add(parm18)

        Dim parm19 As Data.Common.DbParameter = cmd.CreateParameter()
        parm19.ParameterName = "@valor_porcent"
        parm19.Value = agente.comision_porcent
        cmd.Parameters.Add(parm19)

        Dim parm20 As Data.Common.DbParameter = cmd.CreateParameter()
        parm20.ParameterName = "@id_tipo"
        parm20.Value = agente.id_tarifa_tipo
        cmd.Parameters.Add(parm20)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
    Public Sub insertar_tarifas_sobrepeso(agente As ObjAgente)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_InsertTarifas_sobrepeso_Default_Agente"

        Dim parm22 As Data.Common.DbParameter = cmd.CreateParameter()
        parm22.ParameterName = "@id_agencia"
        parm22.Value = agente.id_agente
        cmd.Parameters.Add(parm22)

        Dim parm23 As Data.Common.DbParameter = cmd.CreateParameter()
        parm23.ParameterName = "@id_moneda"
        parm23.Value = agente.id_moneda
        cmd.Parameters.Add(parm23)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
    Public Sub insertar_promotores(nombre As String, id_oficina As Integer)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_promotores"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_oficina"
        parm1.Value = id_oficina
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@nombre"
        parm2.Value = nombre
        cmd.Parameters.Add(parm2)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
    Public Sub insertar_vehiculos(id_oficina As Integer, placas As String, serie As String, modelo As String, fecha_compra As Date)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_Insert_vehiculo"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_oficina"
        parm1.Value = id_oficina
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@placas"
        parm2.Value = placas
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@serie"
        parm3.Value = serie
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@modelo"
        parm4.Value = modelo
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@fecha_compra"
        parm5.Value = fecha_compra
        cmd.Parameters.Add(parm5)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub

    Public Sub insertar_mantenimiento_vehiculo(id_vehiculo As Integer, fecha As Date, id_tipo_servicio As Integer, kilometraje As Integer, costorefacciones As Decimal, costomanodeobra As Decimal)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_mantenimiento_vehiculo"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_vehiculo"
        parm1.Value = id_vehiculo
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@fecha"
        parm2.Value = fecha
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@id_tipo_servicio"
        parm3.Value = id_tipo_servicio
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@kilometraje"
        parm4.Value = kilometraje
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@costorefacciones"
        parm5.Value = costorefacciones
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@costomanodeobra"
        parm6.Value = costomanodeobra
        cmd.Parameters.Add(parm6)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub

    Public Sub insertar_operacion_vehiculos(id_vehiculo As Integer, id_oficina As Integer, id_promotor As Integer, kmInicial As Integer, kmFinal As Integer, costoCombustible As Decimal, cantidadLts As Integer, fecha As Date)

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_operacion_vehiculo"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id_vehiculo"
        parm1.Value = id_vehiculo
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@id_promotor"
        parm2.Value = id_promotor
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@km_inicial"
        parm3.Value = kmInicial
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@km_final"
        parm4.Value = kmFinal
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@costo_combustible"
        parm5.Value = costoCombustible
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@cantidad_lts"
        parm6.Value = cantidadLts
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@fecha"
        parm7.Value = fecha
        cmd.Parameters.Add(parm7)

        Dim parm8 As Data.Common.DbParameter = cmd.CreateParameter()
        parm8.ParameterName = "@id_oficina"
        parm8.Value = id_oficina
        cmd.Parameters.Add(parm8)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

    End Sub
End Class
