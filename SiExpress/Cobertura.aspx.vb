
Imports System.Web.Services

Partial Class Cobertura
    Inherits System.Web.UI.Page

    <WebMethod()> _
    Public Shared Function ObtenerCiudades(ByVal nombre As String) As ArrayList
        Dim db As New DaspackDataContext
        Dim response As ArrayList
        Dim ciudadesList As IEnumerable(Of Ciudades) = db.GetCitiesByStateName(nombre)

        response = New ArrayList(ciudadesList.ToArray)

        Return response
    End Function

End Class
