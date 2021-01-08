Option Strict On
Option Explicit On

Imports System
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Reflection
Imports Microsoft.SqlServer.Server

Public Class DaspackDataContext
    Inherits System.Data.Linq.DataContext

#Region "Extensibility Method Definitions"
    Partial Private Sub OnCreated()
    End Sub
#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(System.Configuration.ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString").ConnectionString)
        OnCreated()
    End Sub

    <FunctionAttribute(Name:="dbo.sp_select_templates")>
    Public Function GetTemplates() As ISingleResult(Of Templates)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo))
            Return CType(result.ReturnValue, ISingleResult(Of Templates))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_campos_obligatorios")>
    Public Function GetCamposObligatorios() As ISingleResult(Of camposObligatorios)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo))
            Return CType(result.ReturnValue, ISingleResult(Of camposObligatorios))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_templates_columnas")>
    Public Function GetTemplateColumns(<Parameter(Name:="@id_template", DbType:="int")> ByVal id_template As Integer) As ISingleResult(Of templateFields)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_template)

            Return CType(result.ReturnValue, ISingleResult(Of templateFields))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Insert_Template")>
    Public Function InsTemplate(<Parameter(Name:="@Nombre", DbType:="Nvarchar(50)")> ByVal nombre As String) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), nombre)

            Return CType(result.ReturnValue, Integer)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Insert_Template_Columnas")>
    Public Function InsTemplateColumnas(<Parameter(Name:="@IdTemplate", DbType:="Nvarchar(50)")> ByVal IdTemplate As Integer,
                                        <Parameter(Name:="@IdColumaArchivo", DbType:="Nvarchar(50)")> ByVal IdColumaArchivo As Integer,
                                        <Parameter(Name:="@IdCampoObligatorio", DbType:="Nvarchar(50)")> ByVal IdCampoObligatorio As Integer) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), IdTemplate, IdColumaArchivo, IdCampoObligatorio)

            Return 0
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Insert_LibroDirecciones")>
    Public Function InsAddBook(<Parameter(Name:="@Nombre", DbType:="Nvarchar(50)")> ByVal nombre As String,
                               <Parameter(Name:="@idUsuario", DbType:="int")> ByVal idUsuario As Integer) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), nombre, idUsuario)

            Return CType(result.ReturnValue, Integer)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Insert_LibroDirecciones_Archivo")>
    Public Function InsAddBookDatosArchivo(<Parameter(Name:="@Contenedor", DbType:="Nvarchar(200)")> ByVal contenedor As String,
                                            <Parameter(Name:="@Inventario", DbType:="Nvarchar(200)")> ByVal inventario As String,
                                            <Parameter(Name:="@Destinatario", DbType:="Nvarchar(200)")> ByVal destinatario As String,
                                            <Parameter(Name:="@Ciudad", DbType:="Nvarchar(200)")> ByVal ciudad As String,
                                            <Parameter(Name:="@Estado", DbType:="Nvarchar(200)")> ByVal estado As String,
                                            <Parameter(Name:="@Direccion", DbType:="Nvarchar(200)")> ByVal direccion As String,
                                            <Parameter(Name:="@CP", DbType:="Nvarchar(200)")> ByVal cp As String,
                                            <Parameter(Name:="@Telefono", DbType:="Nvarchar(200)")> ByVal telefono As String,
                                            <Parameter(Name:="@Servicio", DbType:="Nvarchar(200)")> ByVal servicio As String,
                                            <Parameter(Name:="@Cobranza", DbType:="Nvarchar(200)")> ByVal cobranza As String,
                                            <Parameter(Name:="@Transporte", DbType:="Nvarchar(200)")> ByVal transporte As String,
                                            <Parameter(Name:="@Guia", DbType:="Nvarchar(200)")> ByVal guia As String,
                                            <Parameter(Name:="@Observaciones", DbType:="Nvarchar(200)")> ByVal observaciones As String) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), contenedor, inventario, destinatario, ciudad, estado, direccion, cp, telefono, servicio, cobranza, transporte, guia, observaciones)

            Return CType(result.ReturnValue, Integer)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Insert_Libro_Clientes")>
    Public Function InsAddBookClientes(<Parameter(Name:="@IdLibro", DbType:="int")> ByVal idLibro As Integer,
                                        <Parameter(Name:="@IdCliente", DbType:="int")> ByVal idCliente As Integer,
                                        <Parameter(Name:="@IdLibroArchivo", DbType:="int")> ByVal idLibroArchivo As Integer) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), idLibro, idCliente, idLibroArchivo)
            Return 0
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_libro_clientes")>
    Public Function GetAddBookClientes(<Parameter(Name:="@id_libro", DbType:="int")> ByVal id_libro As Integer) As ISingleResult(Of addBookClientes)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_libro)

            Return CType(result.ReturnValue, ISingleResult(Of addBookClientes))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_libro_archivo")>
    Public Function GetAddBookArchivo(<Parameter(Name:="@id_libro", DbType:="int")> ByVal id_libro As Integer) As ISingleResult(Of addressBook)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_libro)

            Return CType(result.ReturnValue, ISingleResult(Of addressBook))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_tarifas_agencia")>
    Public Function GetTarifasAgencia(<Parameter(Name:="@id_agencia", DbType:="int")> ByVal id_agencia As Integer) As ISingleResult(Of TarifasAgencia)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_agencia)

            Return CType(result.ReturnValue, ISingleResult(Of TarifasAgencia))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_libro_direcciones")>
    Public Function getAddBook(<Parameter(Name:="@idUsuario", DbType:="int")> ByVal id_usuario As Integer) As ISingleResult(Of addBook)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_usuario)
            Return CType(result.ReturnValue, ISingleResult(Of addBook))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_privilegio_modulo")>
    Public Function GetPrivilegiosModulo(<Parameter(Name:="@idModulo", DbType:="int")> ByVal idModulo As Integer,
                                          <Parameter(Name:="@idUsuario", DbType:="int")> ByVal idUsuario As Integer) As ISingleResult(Of PrivilegioModulo)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), idModulo, idUsuario)
            Return CType(result.ReturnValue, ISingleResult(Of PrivilegioModulo))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_ciudades_por_nombreestado")>
    Public Function GetCitiesByStateName(<Parameter(Name:="@nombre", DbType:="varchar(22)")> ByVal nombre As String) As ISingleResult(Of Ciudades)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), nombre)
            Return CType(result.ReturnValue, ISingleResult(Of Ciudades))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_modulo_por_descripcion")>
    Public Function GetModuloPorDescripcion(<Parameter(Name:="@descripcion", DbType:="nvarchar(150)")> ByVal descripcion As String) As ISingleResult(Of Modulo)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), descripcion)
            Return CType(result.ReturnValue, ISingleResult(Of Modulo))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_insert_envio_comentario")>
    Public Function InsComentario(<Parameter(Name:="idEnvio", DbType:="int")> ByVal idEnvio As Integer,
                                  <Parameter(Name:="idUsuario", DbType:="int")> ByVal idUsuario As Integer,
                                  <Parameter(Name:="comentario", DbType:="nvarchar(1000)")> ByVal comentario As String) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), idEnvio, idUsuario, comentario)

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_usuario_empresa")>
    Public Function GetUsuarioEmpresa(<Parameter(Name:="@id_usuario", DbType:="int")> ByVal id_usuario As Integer) As ISingleResult(Of Empresa)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_usuario)
            Return CType(result.ReturnValue, ISingleResult(Of Empresa))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_adeudo_empresa")>
    Public Function GetAdeudoEmpresa(<Parameter(Name:="@idEmpresa", DbType:="int")> ByVal idEmpresa As Integer) As ISingleResult(Of AdeudoEmpresa)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), idEmpresa)
            Return CType(result.ReturnValue, ISingleResult(Of AdeudoEmpresa))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_update_adeudo_empresa")>
    Public Function UpdateAdeudoEmpresa(<Parameter(Name:="idFactura", DbType:="int")> ByVal idFactura As Integer,
                                  <Parameter(Name:="idUsuario", DbType:="int")> ByVal idUsuario As Integer,
                                  <Parameter(Name:="referencia", DbType:="nvarchar(250)")> ByVal referencia As String,
                                  <Parameter(Name:="comentarios", DbType:="nvarchar(250)")> ByVal comentarios As String,
                                  <Parameter(Name:="facturado", DbType:="bit")> ByVal facturado As Boolean,
                                  <Parameter(Name:="deposito", DbType:="money")> ByVal deposito As Decimal) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), idFactura, idUsuario, referencia, comentarios, facturado, deposito)

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_select_datos_usuario")>
    Public Function GetDatosUsuario(<Parameter(Name:="@id_usuario", DbType:="int")> ByVal id_usuario As Integer) As ISingleResult(Of Usuario)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_usuario)
            Return CType(result.ReturnValue, ISingleResult(Of Usuario))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <FunctionAttribute(Name:="dbo.sp_Select_Datos_Envio_Mult_estafeta")>
    Public Function ImagenesEnviosEstafeta(<Parameter(Name:="@id_envio1", DbType:="int")> ByVal id_envio1 As Integer,
                                    <Parameter(Name:="@id_envio2", DbType:="int")> ByVal id_envio2 As Integer,
                                    <Parameter(Name:="@id_agente", DbType:="int")> ByVal id_agente As Integer) As ISingleResult(Of ImagenesEnviosEstafeta)
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), id_envio1, id_envio2, id_agente)
            Return CType(result.ReturnValue, ISingleResult(Of ImagenesEnviosEstafeta))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Partial Public Class Usuario
    Public Property id_usuario As Integer
    Public Property nombre As String
    Public Property id_agencia As Integer
    Public Property email As String
    Public Property login As String
End Class

Partial Public Class AdeudoEmpresa
    Public Property idFactura As Integer
    Public Property id_usuario As Integer
    Public Property id_empresa As Integer
    Public Property TarifaPorEnvio As Decimal
    Public Property Referencia As String
    Public Property Comentarios As String
    Public Property Fecha As Date?
    Public Property NumeroEnvios As Integer
    Public Property Mes As Integer
    Public Property Anio As Integer
    Public Property Facturado As Boolean
    Public Property Mensualidad As Decimal
    Public Property Deposito As Decimal
End Class

Partial Public Class Empresa
    Public Property id_empresa As Integer
    Public Property nombre As String
    Public Property direccion As String
    Public Property telefono As String
    Public Property ciudad As String
    Public Property estado As String
    Public Property id_pais As Integer
    Public Property fecha_alta As Date
    Public Property logo As Byte()
    Public Property alto As Integer
    Public Property ancho As Integer
End Class

Partial Public Class Ciudades
    Private _id_ciudad As Int32
    Private _ciudad As String

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub

    Public Property id_ciudad() As Integer
        Get
            Return _id_ciudad
        End Get
        Set(ByVal value As Integer)
            _id_ciudad = value
        End Set
    End Property


    Public Property Ciudad() As String
        Get
            Return _ciudad
        End Get
        Set(ByVal value As String)
            _ciudad = value
        End Set
    End Property
End Class

Partial Public Class Modulo
    Private _idModulo As Int32
    Private _descripcion As String
    Private _nombre As String

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub

    Public Property IdModulo() As Integer
        Get
            Return _idModulo
        End Get
        Set(ByVal value As Integer)
            _idModulo = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property
End Class

Partial Public Class PrivilegioModulo
    Private _puedeLeer As Byte
    Private _puedeEscribir As Byte
    Private _puedeBorrar As Byte

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub
    Public Property PuedeLeer() As Byte
        Get
            Return _puedeLeer
        End Get
        Set(ByVal value As Byte)
            _puedeLeer = value
        End Set
    End Property

    Public Property PuedeEscribir() As Byte
        Get
            Return _puedeEscribir
        End Get
        Set(ByVal value As Byte)
            _puedeEscribir = value
        End Set
    End Property

    Public Property PuedeBorrar() As Byte
        Get
            Return _puedeBorrar
        End Get
        Set(ByVal value As Byte)
            _puedeBorrar = value
        End Set
    End Property
End Class

Partial Public Class TarifasAgencia
    Private _idTarifaAgencia As Int32
    Private _IdTarifa As Int32

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub

    Public Property id_tarifa_agencia() As Int32
        Get
            Return _idTarifaAgencia
        End Get
        Set(ByVal value As Int32)
            _idTarifaAgencia = value
        End Set
    End Property

    Public Property id_tarifa() As Int32
        Get
            Return _IdTarifa
        End Get
        Set(ByVal value As Int32)
            _IdTarifa = value
        End Set
    End Property

End Class

Partial Public Class Templates
    Private _id_template As Int32
    Private _nombre As String

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub

    <Column(Storage:="_id_template", DbType:="int NOT NULL", CanBeNull:=False)> _
    Public Property id_template() As Int32
        Get
            Return Me._id_template
        End Get
        Set(ByVal value As Int32)
            If ((Me._id_template = value) _
               = False) Then
                Me._id_template = value
            End If
        End Set
    End Property

    <Column(Storage:="_nombre", DbType:="NVarChar(50) NOT NULL", CanBeNull:=False)> _
    Public Property nombre() As String
        Get
            Return Me._nombre
        End Get
        Set(ByVal value As String)
            If ((Me._nombre = value) _
               = False) Then
                Me._nombre = value
            End If
        End Set
    End Property
End Class

Partial Public Class camposObligatorios
    Private _id_campo As Int32
    Private _nombre As String

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
    End Sub

    Public Property id_campo() As Int32
        Get
            Return _id_campo
        End Get
        Set(ByVal value As Int32)
            _id_campo = value
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
End Class

Partial Public Class templateFields
    Public Property id_columna_archivo As Integer
    Public Property id_campo_obligatorio As Integer
End Class

Partial Public Class ImagenesEnviosEstafeta
    Public Property id_envio As Integer
    Public Property labelPDF As Byte()
End Class

Partial Public Class addressBook

    Private _id As Integer
    Private _Contenedor As String
    Private _Inventario As String
    Private _Destinatario As String
    Private _Ciudad As String
    Private _Estado As String
    Private _Direccion As String
    Private _codigo_postal As String
    Private _Telefono As String
    Private _Servicio As String
    Private _Cobranza As String
    Private _Transporte As String
    Private _Guia As String
    Private _Observaciones As String
    Private _Message As String
    Private _id_destinatario As String
    
#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnCreated()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
        OnCreated()
    End Sub

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Contenedor() As String
        Get
            Return _Contenedor
        End Get
        Set(ByVal value As String)
            _Contenedor = value
        End Set
    End Property

    Public Property Inventario() As String
        Get
            Return _Inventario
        End Get
        Set(ByVal value As String)
            _Inventario = value
        End Set
    End Property

    Public Property Destinatario() As String
        Get
            Return _Destinatario
        End Get
        Set(ByVal value As String)
            _Destinatario = value
        End Set
    End Property

    Public Property Ciudad() As String
        Get
            Return _Ciudad
        End Get
        Set(ByVal value As String)
            _Ciudad = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return _Estado
        End Get
        Set(ByVal value As String)
            _Estado = value
        End Set
    End Property

    Public Property Direccion() As String
        Get
            Return _Direccion
        End Get
        Set(ByVal value As String)
            _Direccion = value
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

    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            _Telefono = value
        End Set
    End Property

    Public Property Servicio() As String
        Get
            Return _Servicio
        End Get
        Set(ByVal value As String)
            _Servicio = value
        End Set
    End Property

    Public Property Cobranza() As String
        Get
            Return _Cobranza
        End Get
        Set(ByVal value As String)
            _Cobranza = value
        End Set
    End Property

    Public Property Transporte() As String
        Get
            Return _Transporte
        End Get
        Set(ByVal value As String)
            _Transporte = value
        End Set
    End Property

    Public Property Guia() As String
        Get
            Return _Guia
        End Get
        Set(ByVal value As String)
            _Guia = value
        End Set
    End Property

    Public Property Observaciones() As String
        Get
            Return _Observaciones
        End Get
        Set(ByVal value As String)
            _Observaciones = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property

    Public Property id_destinatario() As String
        Get
            Return _id_destinatario
        End Get
        Set(ByVal value As String)
            _id_destinatario = value
        End Set
    End Property

End Class

Partial Public Class addBookClientes
    Public Property id_book As Int32
    Public Property id_cliente As Int32
    Public Property nombrecompleto As String
    Public Property direccioncompleta As String
    Public Property telefono As String
    Public Property nombres As String
    Public Property apellidos As String
    Public Property direccion As String
    Public Property estadoprovincia As String
    Public Property ciudad As String
    Public Property codigo_postal As String
End Class

Partial Public Class addBook
    Public Property id_book As Integer
    Public Property nombre As String
End Class

Public Enum TipoPrivilegio
    Lee = 1
    Escribe = 2
    Borra = 3
End Enum