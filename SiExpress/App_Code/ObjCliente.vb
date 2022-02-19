Imports Microsoft.VisualBasic

Public Class ObjCliente
    Private id_destinatario, _id_pais As Integer, _nombre As String, _apellidos As String, _empresa As String, _calle As String, _noexterior As Integer, _nointerior As String, _direccion2 As String
    Private _colonia As String, _ciudad As String, _municipio As String, _estadoprovincia As String, _telefono As String, _codigo_postal As String, _email As String, _codigo_pais As String, _direccion As String
    Private _rfc As String, _registro_tributario As String, _residencia_fiscal As String
    Property id_pais() As Integer
        Get
            Return _id_pais
        End Get
        Set(ByVal value As Integer)
            _id_pais = value
        End Set
    End Property
    Property codigo_pais() As String
        Get
            Return _codigo_pais
        End Get
        Set(ByVal value As String)
            _codigo_pais = value
        End Set
    End Property
    Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property
    Public Property apellidos() As String
        Get
            Return _apellidos
        End Get
        Set(ByVal value As String)
            _apellidos = value
        End Set
    End Property
    Public Property empresa() As String
        Get
            Return _empresa
        End Get
        Set(ByVal value As String)
            _empresa = value
        End Set
    End Property
    Public Property calle() As String
        Get
            Return _calle
        End Get
        Set(ByVal value As String)
            _calle = value
        End Set
    End Property
    Public Property noexterior() As Integer
        Get
            Return _noexterior
        End Get
        Set(ByVal value As Integer)
            _noexterior = value
        End Set
    End Property
    Public Property nointerior() As String
        Get
            Return _nointerior
        End Get
        Set(ByVal value As String)
            _nointerior = value
        End Set
    End Property
    Public Property direccion2() As String
        Get
            Return _direccion2
        End Get
        Set(ByVal value As String)
            _direccion2 = value
        End Set
    End Property
    Public Property colonia() As String
        Get
            Return _colonia
        End Get
        Set(ByVal value As String)
            _colonia = value
        End Set
    End Property
    Public Property ciudad() As String
        Get
            Return _ciudad
        End Get
        Set(ByVal value As String)
            _ciudad = value
        End Set
    End Property
    Public Property municipio() As String
        Get
            Return _municipio
        End Get
        Set(ByVal value As String)
            _municipio = value
        End Set
    End Property
    Public Property estadoprovincia() As String
        Get
            Return _estadoprovincia
        End Get
        Set(ByVal value As String)
            _estadoprovincia = value
        End Set
    End Property
    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property
    Public Property codigo_postal() As String
        Get
            Return _codigo_postal
        End Get
        Set(ByVal value As String)
            _codigo_postal = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property direccion() As String
        Get
            Return _direccion
        End Get
        Set(ByVal value As String)
            _direccion = value
        End Set
    End Property
    Public Property rfc() As String
        Get
            Return _rfc
        End Get
        Set(ByVal value As String)
            _rfc = value
        End Set
    End Property
    Public Property registro_tributario() As String
        Get
            Return _registro_tributario
        End Get
        Set(ByVal value As String)
            _registro_tributario = value
        End Set
    End Property
    Public Property residencia_fiscal() As String
        Get
            Return _residencia_fiscal
        End Get
        Set(ByVal value As String)
            _residencia_fiscal = value
        End Set
    End Property
End Class
