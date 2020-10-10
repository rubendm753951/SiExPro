'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Shipment
    Public Property Id As Integer
    Public Property OrderId As Integer
    Public Property TrackingNumber As String
    Public Property TotalWeight As Nullable(Of Decimal)
    Public Property ShippedDateUtc As Nullable(Of Date)
    Public Property DeliveryDateUtc As Nullable(Of Date)
    Public Property AdminComment As String
    Public Property CreatedOnUtc As Date

    Public Overridable Property Order As Order
    Public Overridable Property ShipmentItems As ICollection(Of ShipmentItem) = New HashSet(Of ShipmentItem)

End Class
