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

Partial Public Class TarifaAgencia
    Public Property id_tarifa_agencia As Integer
    Public Property id_agencia As Integer
    Public Property id_tarifa As Integer
    Public Property valor As Nullable(Of Decimal)
    Public Property Activado As Boolean

    Public Overridable Property C_AGENCIAS As Agencias
    Public Overridable Property D_ENVIOS As ICollection(Of Envio) = New HashSet(Of Envio)

End Class