Imports Microsoft.VisualBasic
Public Class ObjAgente
    Private _id_agente As Integer, _id_corporativo As Integer, _id_pais As Integer, _nombre As String, _direccion As String, _estado As String
    Private _provincia As String, _ciudad As String, _fecha_alta As DateTime, _fecha_termino As DateTime, _id_moneda As Integer
    Private _limite_de_credito As Decimal, _NIT As String, _telefono As String, _requiere_asignacion As Boolean, _id_tarifa_tipo As Integer
    Private _id_comision_tipo As Integer, _factor_tarifa As Decimal, _comision_moneda As Decimal, _comision_porcent As Decimal
    Private _factor As Decimal, _esquema_por_factor As Boolean, _costo_adicional As Decimal, _guia_estafeta As Boolean

    Public Property id_agente() As Integer
        Get
            Return _id_agente
        End Get
        Set(ByVal value As Integer)
            _id_agente = value
        End Set
    End Property
    Public Property id_corporativo() As Integer
        Get
            Return _id_corporativo
        End Get
        Set(ByVal value As Integer)
            _id_corporativo = value
        End Set
    End Property
    Public Property id_pais() As Integer
        Get
            Return _id_pais
        End Get
        Set(ByVal value As Integer)
            _id_pais = value
        End Set
    End Property
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
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
    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal value As String)
            _estado = value
        End Set
    End Property
    Public Property provincia() As String
        Get
            Return _provincia
        End Get
        Set(ByVal value As String)
            _provincia = value
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
    Public Property fecha_alta() As DateTime
        Get
            Return _fecha_alta
        End Get
        Set(ByVal value As DateTime)
            _fecha_alta = value
        End Set
    End Property
    Public Property fecha_termino() As DateTime
        Get
            Return _fecha_termino
        End Get
        Set(ByVal value As DateTime)
            _fecha_termino = value
        End Set
    End Property
    Public Property id_moneda() As Integer
        Get
            Return _id_moneda
        End Get
        Set(ByVal value As Integer)
            _id_moneda = value
        End Set
    End Property
    Public Property limite_de_credito() As Decimal
        Get
            Return _limite_de_credito
        End Get
        Set(ByVal value As Decimal)
            _limite_de_credito = value
        End Set
    End Property
    Public Property NIT() As String
        Get
            Return _NIT
        End Get
        Set(ByVal value As String)
            _NIT = value
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
    Public Property requiere_asignacion() As Boolean
        Get
            Return _requiere_asignacion
        End Get
        Set(ByVal value As Boolean)
            _requiere_asignacion = value
        End Set
    End Property
    Public Property id_tarifa_tipo() As Integer
        Get
            Return _id_tarifa_tipo
        End Get
        Set(ByVal value As Integer)
            _id_tarifa_tipo = value
        End Set
    End Property
    Public Property id_comsion_tipo() As Integer
        Get
            Return _id_comision_tipo
        End Get
        Set(ByVal value As Integer)
            _id_comision_tipo = value
        End Set
    End Property
    Public Property factor_tarifa() As Decimal
        Get
            Return _factor_tarifa
        End Get
        Set(ByVal value As Decimal)
            _factor_tarifa = value
        End Set
    End Property
    Public Property comision_moneda() As Decimal
        Get
            Return _comision_moneda
        End Get
        Set(ByVal value As Decimal)
            _comision_moneda = value
        End Set
    End Property
    Public Property comision_porcent() As Decimal
        Get
            Return _comision_moneda
        End Get
        Set(ByVal value As Decimal)
            _comision_moneda = value
        End Set
    End Property
    
    Public Property EsquemaPorFactor() As Boolean
        Get
            Return _esquema_por_factor
        End Get
        Set
            _esquema_por_factor = value
        End Set
    End Property

    Public Property Factor() As Decimal
        Get
            Return _factor
        End Get
        Set
            _factor = value
        End Set
    End Property

    Public Property CostoAdicional() As Decimal
        Get
            Return _costo_adicional
        End Get
        Set
            _costo_adicional = Value
        End Set
    End Property

    Public Property guia_estafeta() As Boolean
        Get
            Return _guia_estafeta
        End Get
        Set(ByVal value As Boolean)
            _guia_estafeta = value
        End Set
    End Property

End Class




