Imports System.Data.Entity
Imports Microsoft.VisualBasic
Imports SiExProData

Public Class DaspackDALC

    Public Shared Function GetModuloPrivilegio(ByVal idModulo As Integer, ByVal idUsuario As Integer, ByVal privilegio As Integer) As Boolean
        Dim db As New DaspackDataContext
        Dim tienePermiso As Boolean = False
        Return True
        Dim userPrivilegesList As IEnumerable(Of PrivilegioModulo) = db.GetPrivilegiosModulo(idModulo, idUsuario)
        Dim usuarioPrivilegios As List(Of PrivilegioModulo) = New List(Of PrivilegioModulo)(userPrivilegesList.ToList())
        Dim usuarioPrivilegio As PrivilegioModulo

        If usuarioPrivilegios IsNot Nothing Then
            usuarioPrivilegio = usuarioPrivilegios.FirstOrDefault()
        End If


        If usuarioPrivilegio IsNot Nothing Then
            Select Case privilegio
                Case TipoPrivilegio.Lee
                    If (usuarioPrivilegio.PuedeLeer = 1) Then
                        tienePermiso = True
                    End If
                Case TipoPrivilegio.Borra
                    If (usuarioPrivilegio.PuedeBorrar = 1) Then
                        tienePermiso = True
                    End If
                Case TipoPrivilegio.Escribe
                    If (usuarioPrivilegio.PuedeEscribir = 1) Then
                        tienePermiso = True
                    End If
            End Select
        End If

        Return tienePermiso
    End Function

    Public Shared Function GetModuloPorDescripcion(ByVal descripcion As String) As Modulo
        Dim db As New DaspackDataContext
        Dim modulo As Modulo = Nothing
        Dim modulosList As IEnumerable(Of Modulo) = db.GetModuloPorDescripcion(descripcion)
        
        Dim modulos As List(Of Modulo) = New List(Of Modulo)(modulosList.ToList())

        If modulos IsNot Nothing Then
            modulo = modulos.FirstOrDefault()
        End If

        Return modulo

    End Function

    Public Shared Function InsComenatrio(ByVal idEnvio As Integer, ByVal idUsuario As Integer, ByVal comentario As String) As Boolean
        Dim db As New DaspackDataContext
        db.InsComentario(idEnvio, idUsuario, comentario)
        Return True

    End Function

    Public Shared Function GetUsuarioEmpresa(ByVal id_usuario As Integer) As Empresa
        Dim db As New DaspackDataContext
        Dim empresa As Empresa = Nothing
        Dim empresasList As IEnumerable(Of Empresa) = db.GetUsuarioEmpresa(id_usuario)

        Dim empresas As List(Of Empresa) = New List(Of Empresa)(empresasList.ToList())

        If empresas IsNot Nothing Then
            empresa = empresas.FirstOrDefault()
        End If

        Return empresa

    End Function

    Public Shared Function GetAdeudoEmpresaTotal(ByVal id_empresa As Integer) As Decimal
        Dim db As New DaspackDataContext
        Dim adeudo As Decimal = 0
        Dim adeudoEmpresa As IEnumerable(Of AdeudoEmpresa) = db.GetAdeudoEmpresa(id_empresa)

        Dim adeudoEmpresasList As List(Of AdeudoEmpresa) = New List(Of AdeudoEmpresa)(adeudoEmpresa.ToList())

        If adeudoEmpresasList IsNot Nothing Then
            adeudo = adeudoEmpresasList.Sum(Function(x) x.Mensualidad - x.Deposito + (x.NumeroEnvios * x.TarifaPorEnvio))
        End If

        Return adeudo

    End Function

    Public Shared Function GetAdeudos(ByVal id_empresa As Integer) As List(Of AdeudoEmpresa)
        Dim db As New DaspackDataContext
        Dim adeudoEmpresa As IEnumerable(Of AdeudoEmpresa) = db.GetAdeudoEmpresa(id_empresa)

        Dim adeudoEmpresasList As List(Of AdeudoEmpresa) = New List(Of AdeudoEmpresa)(adeudoEmpresa.ToList())

        Return adeudoEmpresasList

    End Function

    Public Shared Function UpdateAdeudoEmpresa(idFactura As Integer, idUsuario As Integer, referencia As String, comentarios As String, facturado As Boolean, deposito As Decimal) As Boolean
        Dim db As New DaspackDataContext
        db.UpdateAdeudoEmpresa(idFactura, idUsuario, referencia, comentarios, facturado, deposito)

        Return True

    End Function

    Public Shared Function GetDatosUsuario(ByVal id_usuario As Integer) As Usuario
        Dim db As New DaspackDataContext

        Dim usuario As Usuario = db.GetDatosUsuario(id_usuario).FirstOrDefault()

        Return usuario

    End Function

    public Shared Function GetUltimoEnvio(ByVal id_usuario As integer ) As Envio
        Dim dbContext As New SiExProEntities
        Dim envio As Envio = dbContext.D_ENVIOS.OrderByDescending(Function(x) x.id_envio).FirstOrDefault(Function(x) x.id_usuario = id_usuario)

        Return envio
    End Function

    public Shared Function GetDatosEnvio(ByVal idEnvio As integer ) As Envio
        Dim dbContext As New SiExProEntities
        Dim envio As Envio = dbContext.D_ENVIOS.FirstOrDefault(Function(x) x.id_envio = idEnvio)

        Return envio
    End Function

    public Shared Function GetDatosCliente(id_envio as integer) As Cliente
        Dim dbContext As New SiExProEntities
        Dim cliente As Cliente

        cliente = CType((from ed in dbContext.D_ENVIOS_DATOS.Where(Function(x) x.id_envio = id_envio)
            join c In dbContext.C_CLIENTES 
                On c.id_cliente Equals ed.id_cliente
            Select c).FirstOrDefault(), Cliente)

        return cliente

    End Function

    public Shared Function GetDatosDestinatario(id_envio as integer) As Destinatario
        Dim dbContext As New SiExProEntities
        Dim destinatario as Destinatario

        destinatario = CType((from ed in dbContext.D_ENVIOS_DATOS.Where(Function(x) x.id_envio = id_envio)
            join c In dbContext.C_DESTINATARIOS
                On c.id_destinatario Equals ed.id_destinatario
            Select c).FirstOrDefault(), Destinatario)

        Return destinatario
    End Function

    public Shared Function GetTarifaAgencia(id_tarifa_agencia As integer) As TarifaAgencia
        Dim dbContext As New SiExProEntities
        Dim tarifaAgencias as TarifaAgencia = dbContext.D_TARIFAS_AGENCIA.FirstOrDefault(Function(x) x.id_tarifa_agencia = id_tarifa_agencia) 

        Return tarifaAgencias
    End Function

End Class
