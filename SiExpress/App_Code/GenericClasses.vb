Imports Microsoft.VisualBasic

Public Class GenericClasses
    
End Class

Public Class genericResponse
    Public Property responseArray As ArrayList
    Public Property responseMessage As String
    Public Property responseSuccess As Integer
End Class

Public Class readFileResponse
    Public Property fileFields As ArrayList
    Public Property matchTemplate As ArrayList
    Public Property responseMessage As String
End Class

Public Class createMasiveResponse
    Public Property BenefMessages As ArrayList
    Public Property SendMessages As String
    Public Property shipments As ArrayList
    Public Property remAddError As Integer
End Class
