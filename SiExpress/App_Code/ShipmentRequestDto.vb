Imports Microsoft.VisualBasic

Public Class ShipmentRequestDto
    Public Property AgentId() As Integer
    Public Property ShipmentId() As Integer
    Public Property Insurance() As Decimal
    Public Property ClientId() As Integer
    Public Property ProductId() As Integer
    Public Property PromoId() As Integer
    Public Property PackageCount() As Integer
    Public Property CarrierId() As Integer
    Public Property MultipleLabels() As Boolean
    Public Property EstafetaServiceType() As Integer
    Public Property FedexExpressSaver() As Boolean
    Public Property FedexStandardOvernight() As Boolean
    Public Property Height() As Double
    Public Property Width() As Double
    Public Property Length() As Double
    Public Property Weight() As Double
    Public Property DeliveryInstructions() As String
    Public Property Reference() As String
    Public Property Content() As String
    Public Property AccountId() As Integer
    Public Property TypeSrvcId() As String
    Public Property ShipmentItems() As List(Of ShipmentRequestItemDto)
    Public Property PaperType() As Integer
    Public Property TotlDeclVlue() As Decimal
End Class

