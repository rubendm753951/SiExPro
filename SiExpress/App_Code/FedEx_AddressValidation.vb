Imports Microsoft.VisualBasic

Public Class FedEx_AddressValidation
    Private _id As Integer, _Country As String, _Company As String, _Address_line As String, _City As String, _StateProvince As String
    Private _zip_code As String, _Notification As String, _Changes As String, _SoapException As String, _Exception As String
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Public Property Country() As String
        Get
            Return _Country
        End Get
        Set(ByVal value As String)
            _Country = value
        End Set
    End Property
    Public Property Company() As String
        Get
            Return _Company
        End Get
        Set(ByVal value As String)
            _Company = value
        End Set
    End Property
    Public Property Address_line() As String
        Get
            Return _Address_line
        End Get
        Set(ByVal value As String)
            _Address_line = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return _City
        End Get
        Set(ByVal value As String)
            _City = value
        End Set
    End Property
    Public Property StateProvince() As String
        Get
            Return _StateProvince
        End Get
        Set(ByVal value As String)
            _StateProvince = value
        End Set
    End Property
    Public Property zip_code() As String
        Get
            Return _zip_code
        End Get
        Set(ByVal value As String)
            _zip_code = value
        End Set
    End Property
    Public Property Notification() As String
        Get
            Return _Notification
        End Get
        Set(ByVal value As String)
            _Notification = value
        End Set
    End Property
    Public Property Changes() As String
        Get
            Return _Changes
        End Get
        Set(ByVal value As String)
            _Changes = value
        End Set
    End Property
    Public Property SoapException() As String
        Get
            Return _SoapException
        End Get
        Set(ByVal value As String)
            _SoapException = value
        End Set
    End Property
    Public Property Exception() As String
        Get
            Return _Exception
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Function addr_input() As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "dbo.sp_insert_tmp_FedEX_addr_USA"

        Dim id_identity As Integer

        'Parámetros de entrada
        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@Country"
        parm1.Value = Me.Country
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@Company"
        parm2.Value = Me.Company
        cmd.Parameters.Add(parm2)

        Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
        parm3.ParameterName = "@Address_line"
        parm3.Value = Me.Address_line
        cmd.Parameters.Add(parm3)

        Dim parm4 As Data.Common.DbParameter = cmd.CreateParameter()
        parm4.ParameterName = "@City"
        parm4.Value = Me.City
        cmd.Parameters.Add(parm4)

        Dim parm5 As Data.Common.DbParameter = cmd.CreateParameter()
        parm5.ParameterName = "@StateProvince"
        parm5.Value = Me.StateProvince
        cmd.Parameters.Add(parm5)

        Dim parm6 As Data.Common.DbParameter = cmd.CreateParameter()
        parm6.ParameterName = "@zip_code"
        parm6.Value = Me.zip_code
        cmd.Parameters.Add(parm6)

        Dim parm7 As Data.Common.DbParameter = cmd.CreateParameter()
        parm7.ParameterName = "@id"
        parm7.Size = 10
        parm7.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm7)

        connection.Open()
        cmd.ExecuteReader() '.ExecuteNonQuery()
        id_identity = parm7.Value
        connection.Close()

        Return id_identity

    End Function

    Public Function addr_validation(ByVal id_identity As Integer) As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "Sp_FedEx_Address_Validation"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@identity"
        parm1.Value = id_identity
        cmd.Parameters.Add(parm1)

        Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
        parm2.ParameterName = "@result"
        parm2.Size = 10
        parm2.Direction = Data.ParameterDirection.Output
        cmd.Parameters.Add(parm2)

        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()

        Return parm2.Value

    End Function
    Public Function GetData(ByVal identity As Integer, ByRef datos_valid As ObjAddressValidation) As Integer

        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_select_tmp_FedEX_addr_USA_rs"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id"
        parm1.Value = identity
        cmd.Parameters.Add(parm1)

        Dim reader As Data.SqlClient.SqlDataReader

        connection.Open()
        reader = cmd.ExecuteReader()

        Dim registros As Integer
        If reader.HasRows Then
            reader.Read()
            If Not reader.IsDBNull(0) Then
                datos_valid.addr_country_code_r = reader.GetString(0)
            End If
            If Not reader.IsDBNull(1) Then
                datos_valid.addrCompany = reader.GetString(1)
            End If
            If Not reader.IsDBNull(2) Then
                datos_valid.addrStreetline_r = reader.GetString(2)
            End If
            If Not reader.IsDBNull(3) Then
                datos_valid.addrCity_r = reader.GetString(3)
            End If
            If Not reader.IsDBNull(4) Then
                datos_valid.addrState_r = reader.GetString(4)
            End If
            If Not reader.IsDBNull(5) Then
                datos_valid.addrZipCode_r = reader.GetString(5)
            End If
            If Not reader.IsDBNull(6) Then
                datos_valid.addr_changes_r = reader.GetString(6)
            End If
            If Not reader.IsDBNull(7) Then
                datos_valid.score_r = reader.GetValue(7)
            End If
            registros = 1
        Else
            registros = 0
        End If
        connection.Close()

        Return registros

    End Function
    Public Function get_notis(ByVal identity As Integer) As String
        Dim mensaje As String = ""
        Dim MyConnection As ConnectionStringSettings
        MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
        Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
        connection.ConnectionString = MyConnection.ConnectionString
        Dim cmd As Data.IDbCommand = connection.CreateCommand()
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.CommandText = "sp_select_tmp_FedEX_addr_USA_notis"

        Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
        parm1.ParameterName = "@id"
        parm1.Value = identity
        cmd.Parameters.Add(parm1)

        Dim reader As Data.SqlClient.SqlDataReader
        connection.Open()
        reader = cmd.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            mensaje = reader.GetString(0)
        Else
            mensaje = "No existen notificaciones"
        End If
        Return mensaje
    End Function

End Class
