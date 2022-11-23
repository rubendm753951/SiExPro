Imports Microsoft.VisualBasic

Public Class EstafetaPrecio
    Public Property Terrestre() As Decimal
    Public Property DiaSiguiente() As Decimal
    Public Property Ltl() As Decimal
    Public Property Gombar() As Decimal
    Public Property CuentaLtl() As Integer
    Public Property Cuenta() As Integer
    Public Property Zona() As Integer
    Public Property ZonaLtl() As Integer
    Public Property Ocurre() As Boolean
    Public Property ExpressSaverAmount() As Decimal
    Public Property ExpressSaverUser() As Integer
    Public Property StandardOvernightAmount() As Decimal
    Public Property StandardOvernightUser() As Integer
    Public Property PaqueteExpressEconomic() As Decimal
    Public Property PaqueteExpressNextDay() As Decimal
    Public Property UserAccountPe() As Integer
    Public Property AmountGombarExpress() As Decimal
    Public Property AmountGombarTarima() As Decimal
    Public Property AmountGombarNacional() As Decimal
    Public Property AmountGombarDLRutaLeonPueCdmx() As Decimal
    Public Property AmountGombarDLTarimasRutaLeonPueCdmx() As Decimal
    Public Property AmountGombarDLRutaPacifico() As Decimal
    Public Property AmountGombarDLTarimasRutaPacifico() As Decimal
    Public Property AmountGombarDLTarimasOcurreRutaPacifico() As Decimal
End Class

Public Class EstafetaTarimas
    Public Property Id() As Integer
    Public Property Km_Rango_De() As Integer
    Public Property Km_Rango_A() As Integer
    Public Property Zona() As Integer
    Public Property Cuenta() As Integer
    Public Property Total() As Decimal
End Class