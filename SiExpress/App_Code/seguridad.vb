Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System

Public Class seguridad
    Inherits System.Web.UI.Page
    Private _id_modulo As Integer, _user_name As String, _autorizado As Boolean, _id_usuario As Integer, _id_perfil As Integer, _page_name As String, _id_oficina As Integer
    Public Property id_modulo() As Integer
        Get
            Return _id_modulo
        End Get
        Set(ByVal value As Integer)
            _id_modulo = value
        End Set
    End Property
    Public Property user_name() As String
        Get
            Return _user_name
        End Get
        Set(ByVal value As String)
            _user_name = value
        End Set
    End Property
    Public Property id_usuario() As Integer
        Get
            Return _id_usuario
        End Get
        Set(ByVal value As Integer)
            _id_usuario = value
        End Set
    End Property
    Public Property page_name() As String
        Get
            Return _page_name
        End Get
        Set(ByVal value As String)
            _page_name = value
        End Set
    End Property
    Public Property id_perfil() As Integer
        Get
            Return _id_perfil
        End Get
        Set(ByVal value As Integer)
            _id_perfil = value
        End Set
    End Property
    Public Property id_oficina() As Integer
        Get
            Return _id_oficina
        End Get
        Set(ByVal value As Integer)
            _id_oficina = value
        End Set
    End Property

    Public Function validar_usuario(ByVal user_name As String, ByVal pagina As String) As Boolean
        Dim autorizado As Boolean

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        'ejecuta SP para validar seguridad
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_ValidaUsuarios"

        'Parámetros para validar el usuario
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@user_name"
        parm1.Value = user_name
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = ("@page_name")
        parm2.Value = pagina
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@autorizado"
        parm3.Size = 10
        cmd.Parameters.Add(parm3)
        parm3.Direction = Data.ParameterDirection.Output


        connection.Open()
        cmd.ExecuteNonQuery()
        autorizado = parm3.Value 'Parametros de salida para leer el resultado de la validación.
        connection.Close()

        Me.id_modulo = id_modulo
        Return autorizado

    End Function
    Public Function login_usuario(ByVal user_name As String, ByVal password As String) As Boolean
        Dim autorizado As Boolean

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString

        'ejecuta SP para validar seguridad
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_login"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@user_name"
        parm1.Value = user_name
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@password"
        parm2.Value = password
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@result"
        parm3.Size = 10
        cmd.Parameters.Add(parm3)
        parm3.Direction = Data.ParameterDirection.Output

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@id_usuario"
        parm4.Size = 10
        cmd.Parameters.Add(parm4)
        parm4.Direction = Data.ParameterDirection.Output

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@id_perfil"
        parm5.Size = 10
        cmd.Parameters.Add(parm5)
        parm5.Direction = Data.ParameterDirection.Output

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@id_oficina"
        parm6.Size = 10
        cmd.Parameters.Add(parm6)
        parm6.Direction = Data.ParameterDirection.Output


        connection.Open()
        cmd.ExecuteNonQuery()
        autorizado = parm3.Value 'Parametros de salida para leer el resultado de la validación.
        connection.Close()

        Me.user_name = user_name
        If parm4.Value <> 0 Then
            Me.id_usuario = parm4.Value
            Me.id_perfil = parm5.Value
            Me.id_oficina = parm6.Value
        End If

        Return autorizado

    End Function
End Class
