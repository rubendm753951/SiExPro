Imports Microsoft.VisualBasic

Public Class ShipmentRequestDto
    Public Property AgentId() As Integer
    Public Property ShipmentId() As Integer
    Public Property ClientId() As Integer
    Public Property ProductId() As Integer
    Public Property PromoId() As Integer
    Public Property CarrierId() As Integer
    Public Property MultipleLabels() As Boolean
    Public Property EstafetaServiceType() As Integer
    Public Property FedexExpressSaver() As Boolean
    Public Property FedexStandardOvernight() As Boolean
    Public Property DeliveryInstructions() As String
    Public Property Reference() As String
    Public Property AccountId() As Integer
    Public Property TypeSrvcId() As String
    Public Property ShipmentItems() As List(Of ShipmentRequestItemDto)
    Public Property PaperType() As Integer
    Public Property TotlDeclVlue() As Decimal
    Public Property IsOcurre() As Integer
End Class

