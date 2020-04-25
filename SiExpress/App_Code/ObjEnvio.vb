Imports Microsoft.VisualBasic
Public Class ObjEnvio
    Private _id_agente As Integer, _precio As Decimal, _valor_seguro As Decimal, _id_tarifa_agencia As Integer, _id_codigo_promocion As Integer, _total_envio As Decimal, _fecha As DateTime
    Private _fecha_corte As Date, _instrucciones_entrega As String, _observaciones As String, _id_usuario As Integer, _id_ruta As Integer, _referencia As String, _valor_aduana As Decimal
    Private _id_cliente As Integer, _id_destinatario As Integer, _contenido As String, _observaciones_detalle As String, _largo As Decimal, _ancho As Decimal, _alto As Decimal, _peso As Decimal, _fecha_rec As DateTime
    Private _FedExTipo As Integer, _FedExPkg As Integer, _FedExRef As String, _id_envio As Integer, _impresion_FedEx As String, _id_subproducto As Integer, _USPS_service As String, _dimension_peso As String, _contenedores As String
    Public Property id_agente() As Object
        Get
            Return _id_agente
        End Get
        Set(ByVal value As Object)
            _id_agente = value
        End Set
    End Property
    Public Property precio() As Object
        Get
            Return _precio
        End Get
        Set(ByVal value As Object)
            _precio = value
        End Set
    End Property
    Public Property valor_seguro() As Decimal
        Get
            Return _valor_seguro
        End Get
        Set(ByVal value As Decimal)
            _valor_seguro = value
        End Set
    End Property
    Public Property id_tarifa_agencia() As Object
        Get
            Return _id_tarifa_agencia
        End Get
        Set(ByVal value As Object)
            _id_tarifa_agencia = value
        End Set
    End Property
    Public Property id_codigo_promocion() As Integer
        Get
            Return _id_codigo_promocion
        End Get
        Set(ByVal value As Integer)
            _id_codigo_promocion = value
        End Set
    End Property
    Public Property total_envio() As Decimal
        Get
            Return _total_envio
        End Get
        Set(ByVal value As Decimal)
            _total_envio = value
        End Set
    End Property
    Public Property fecha() As DateTime
        Get
            Return _fecha
        End Get
        Set(ByVal value As DateTime)
            _fecha = value
        End Set
    End Property
    Public Property fecha_corte() As DateTime
        Get
            Return _fecha_corte
        End Get
        Set(ByVal value As DateTime)
            _fecha_corte = value
        End Set
    End Property
    Public Property instrucciones_entrega() As String
        Get
            Return _instrucciones_entrega
        End Get
        Set(ByVal value As String)
            _instrucciones_entrega = value
        End Set
    End Property
    Public Property observaciones() As String
        Get
            Return _observaciones
        End Get
        Set(ByVal value As String)
            _observaciones = value
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
    Public Property id_ruta() As Integer
        Get
            Return _id_ruta
        End Get
        Set(ByVal value As Integer)
            _id_ruta = value
        End Set
    End Property

    Public Property id_cliente() As Integer
        Get
            Return _id_cliente
        End Get
        Set(ByVal value As Integer)
            _id_cliente = value
        End Set
    End Property
    Public Property id_destinatario() As Integer
        Get
            Return _id_destinatario
        End Get
        Set(ByVal value As Integer)
            _id_destinatario = value
        End Set
    End Property
    Public Property contenido() As String
        Get
            Return _contenido
        End Get
        Set(ByVal value As String)
            _contenido = value
        End Set
    End Property

    Public Property observaciones_detalle() As String
        Get
            Return _observaciones_detalle
        End Get
        Set(ByVal value As String)
            _observaciones_detalle = value
        End Set
    End Property
    Public Property largo() As Object
        Get
            Return _largo
        End Get
        Set(ByVal value As Object)
            _largo = value
        End Set
    End Property
    Public Property ancho() As Object
        Get
            Return _ancho
        End Get
        Set(ByVal value As Object)
            _ancho = value
        End Set
    End Property
    Public Property alto() As Object
        Get
            Return _alto
        End Get
        Set(ByVal value As Object)
            _alto = value
        End Set
    End Property
    Public Property peso() As Object
        Get
            Return _peso
        End Get
        Set(ByVal value As Object)
            _peso = value
        End Set
    End Property
    Public Property referencia() As String
        Get
            Return _referencia
        End Get
        Set(ByVal value As String)
            _referencia = value
        End Set
    End Property
    Public Property valor_aduana() As Decimal
        Get
            Return _valor_aduana
        End Get
        Set(ByVal value As Decimal)
            _valor_aduana = value
        End Set
    End Property
    Public Property fecha_rec() As DateTime
        Get
            Return _fecha_rec
        End Get
        Set(ByVal value As DateTime)
            _fecha_rec = value
        End Set
    End Property
    Public Property FedExTipo() As Integer
        Get
            Return _FedExTipo
        End Get
        Set(ByVal value As Integer)
            _FedExTipo = value
        End Set
    End Property
    Public Property FedExPkg() As Integer
        Get
            Return (_FedExPkg)
        End Get
        Set(ByVal value As Integer)
            _FedExPkg = value
        End Set
    End Property
    Public Property FedExRef() As String
        Get
            Return _FedExRef
        End Get
        Set(ByVal value As String)
            _FedExRef = value
        End Set
    End Property
    Public Property id_envio() As Integer
        Get
            Return _id_envio
        End Get
        Set(ByVal value As Integer)
            _id_envio = value
        End Set
    End Property
    Public Property impresion_FedEx() As String
        Get
            Return _impresion_FedEx
        End Get
        Set(ByVal value As String)
            _impresion_FedEx = value
        End Set
    End Property
    Public Property id_subproducto() As Integer
        Get
            Return _id_subproducto
        End Get
        Set(ByVal value As Integer)
            _id_subproducto = value
        End Set
    End Property
    Public Property USPS_service() As String
        Get
            Return _USPS_service
        End Get
        Set(ByVal value As String)
            _USPS_service = value
        End Set
    End Property
    Public Property dimension_peso() As String
        Get
            Return _dimension_peso
        End Get
        Set(ByVal value As String)
            _dimension_peso = value
        End Set
    End Property
    Public Property contenedores() As String
        Get
            Return _contenedores
        End Get
        Set(ByVal value As String)
            _contenedores = value
        End Set
    End Property
End Class






