
Partial Public Class ClientAddrOrig
    Public Property ColonyName As String
    Public Property ZipCode As String
    Public Property Branch As String
    Public Property Zone As String
    Public Property Ol As String
End Class

Partial Public Class ClientAddrDest
    Public Property ColonyName As String
    Public Property ZipCode As String
    Public Property Branch As String
    Public Property Zone As String
    Public Property Ol As String
End Class

Partial Public Class PEShipment
    Public Property Sequence As Integer
    Public Property Quantity As Integer
    Public Property ShpCode As String
    Public Property Weight As Double
    Public Property Volume As Double
    Public Property LongShip As Double
    Public Property WidthShip As Double
    Public Property HighShip As Double
    Public Property SrvcId As String
    Public Property SrvcRefId As String
    Public Property SlabNo As String
    Public Property SlabDisc As Double
    Public Property SlabTax As Double
    Public Property SlabTaxRet As Double
    Public Property SlabAmount As Double
    Public Property Convenio As String
    Public Property Cpny As String
End Class

Partial Public Class PEShipmentDetail
    Public Property Shipments As PEShipment()
End Class

Partial Public Class PEServices
    Public Property DlvyType As String
    Public Property AckType As String
    Public Property TotlDeclVlue As Double
    Public Property InvType As String
    Public Property RadType As String
    Public Property DlvyTypeAmt As Double
    Public Property DlvyTypeAmtDisc As Double
    Public Property DlvyTypeAmtTax As Double
    Public Property DlvyTypeAmtRetTax As Double
    Public Property AckTypeAmt As Double
    Public Property AckTypeAmtDisc As Double
    Public Property AckTypeAmtTax As Double
    Public Property AckTypeAmtRetTax As Double
    Public Property RadTypeAmt As Double
    Public Property RadTypeAmtDisc As Double
    Public Property RadTypeAmtTax As Double
    Public Property RadTypeAmtRetTax As Double
    Public Property ShpTypeAmt As Double
    Public Property ShpTypeAmtDisc As Double
    Public Property ShpTypeAmtTax As Double
    Public Property ShpTypeAmtRetTax As Double
    Public Property DlvyTypeConvenio As String
    Public Property ShpTypeConvenio As Object
    Public Property ShpCpny As Object
    Public Property RadTypeConvenio As Object
    Public Property AckTypeConvenio As String
End Class

Partial Public Class PEOtherService
    Public Property Id As String
    Public Property IdRef As String
    Public Property Description As String
    Public Property AditionalData1 As String
    Public Property AditionalData2 As String
    Public Property Cpny As String
    Public Property Amt As Double
    Public Property AmtDisc As Double
    Public Property AmtTax As Double
    Public Property AmtRetTax As Double
End Class


Partial Public Class OtherServices
    Public Property otherServices As PEOtherService()
End Class

Partial Public Class PEAmount
    Public Property ShpAmnt As Double
    Public Property DiscAmnt As Double
    Public Property SrvcAmnt As Double
    Public Property SubTotlAmnt As Double
    Public Property TaxAmnt As Double
    Public Property TaxRetAmnt As Double
    Public Property TotalAmnt As Double
End Class

Partial Public Class PEQuotation
    Public Property ServiceType As String
    Public Property Id As String
    Public Property IdRef As String
    Public Property ServiceName As String
    Public Property ServiceInfoDescr As String
    Public Property ServiceInfoDescrLong As String
    Public Property CutoffDateTime As String
    Public Property CutoffTime As String
    Public Property MaxRadTime As String
    Public Property MaxBokTime As String
    Public Property OnTime As Boolean
    Public Property PromiseDate As String
    Public Property PromiseDateDaysQty As Integer
    Public Property PromiseDateHoursQty As Integer
    Public Property InOffer As Boolean
    Public Property ShipmentDetail As PEShipmentDetail
    Public Property Services As PEServices
    Public Property OtherServices As OtherServices
    Public Property Amount As PEAmount
End Class

Partial Public Class QuoteServiceResponse
    Public Property ClientId As String
    Public Property ClientDest As String
    Public Property ClntClasifTarif As String
    Public Property AgreementType As String
    Public Property PymtMode As String
    Public Property ClientAddrOrig As ClientAddrOrig
    Public Property ClientAddrDest As ClientAddrDest
    Public Property QuoteServices As String()
    Public Property Quotations As PEQuotation()
End Class

Partial Public Class PaqueteExpressQuoteServiceResponse
    Public Property Data() As QuoteServiceResponse
    Public Property ErrorMessage() As String

    Public Property Success() As Boolean
End Class

Partial Public Class PaqueteExpressShipResponse
    Public Property Data() As String
    Public Property ErrorMessage() As String

    Public Property Success() As Boolean
End Class