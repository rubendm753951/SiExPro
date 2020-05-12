Imports Microsoft.VisualBasic

Public Class FrecuenciaCotizadorResponse

    Public Property TipoEnvio As Tipoenvio
    Public Property TipoServicio() As Tiposervicio
    Public Property Colonias As Colonias
    Public Property ModalidadEntrega As Modalidadentrega
    Public Property DiasEntrega As Diasentrega
    Public Property CostoReexpedicion As String
    Public Property ExistenteSiglaOri As String
    Public Property ExistenteSiglaDes As String
    Public Property Destino As Destino
    Public Property Origen As Origen
    Public Property _Error As String
    Public Property MensajeError As String
    Public Property CodigoPosOri As String

End Class

Public Class Tipoenvio
    Public Property EsPaquete As Boolean
    Public Property Largo As Integer
    Public Property Peso As Integer
    Public Property Alto As Integer
    Public Property Ancho As Integer
End Class

Public Class Colonias
    Public Property Colonias() As String
End Class

Public Class Modalidadentrega
    Public Property OcurreForzoso As String
    Public Property Frecuencia As String
End Class

Public Class Diasentrega
    Public Property Lunes As String
    Public Property Martes As String
    Public Property Miercoles As String
    Public Property Jueves As String
    Public Property Viernes As String
    Public Property Sabado As String
    Public Property Domingo As String
End Class

Public Class Destino
    Public Property CpDestino As String
    Public Property Plaza1 As String
    Public Property Municipio As String
    Public Property Estado As String
End Class

Public Class Origen
    Public Property CodigoPosOri As String
    Public Property PlazaOri As String
    Public Property MunicipioOri As String
    Public Property EstadoOri As String
End Class

Public Class Tiposervicio
    Public Property DescripcionServicio As String
    Public Property TipoEnvioRes As Integer
    Public Property AplicaCotizacion As String
    Public Property TarifaBase As Single
    Public Property CCTarifaBase As Single
    Public Property CargosExtra As Integer
    Public Property SobrePeso As Integer
    Public Property CCSobrePeso As Integer
    Public Property CostoTotal As Single
    Public Property Peso As Integer
    Public Property AplicaServicio As String
End Class