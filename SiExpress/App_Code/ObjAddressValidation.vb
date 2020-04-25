Imports Microsoft.VisualBasic

Public Class ObjAddressValidation
    Private _addrId As String, _addrStreetline As String, _addrZipCode As String, _addrCity As String, _addrState As String, _addrCompany As String
    Private _addrId_r As String, _addrStreetline_r As String, _addrZipCode_r As String, _addrCity_r As String, _addrCountryCode_r As String
    Private _addrState_r As String, _response_r As String, _addr_changes_r As String, _error_r As String, _SOAP_err_r As String, _score_r As String
    Public Property addrId() As String
        Get
            Return _addrId
        End Get
        Set(ByVal value As String)
            _addrId = value
        End Set
    End Property
    Public Property addrStreetLine() As String
        Get
            Return _addrStreetline
        End Get
        Set(ByVal value As String)
            _addrStreetline = value
        End Set
    End Property
    Public Property addrZipCode() As String
        Get
            Return _addrZipCode
        End Get
        Set(ByVal value As String)
            _addrZipCode = value
        End Set
    End Property
    Public Property addrCity() As String
        Get
            Return _addrCity
        End Get
        Set(ByVal value As String)
            _addrCity = value
        End Set
    End Property
    Public Property addrState() As String
        Get
            Return _addrState
        End Get
        Set(ByVal value As String)
            _addrState = value
        End Set
    End Property
    Public Property addrCompany() As String
        Get
            Return _addrCompany
        End Get
        Set(ByVal value As String)
            _addrCompany = value
        End Set
    End Property
    Public Property addrId_r() As String
        Get
            Return _addrId_r
        End Get
        Set(ByVal value As String)
            _addrId_r = value
        End Set
    End Property
    Public Property addrStreetline_r() As String
        Get
            Return _addrStreetline_r
        End Get
        Set(ByVal value As String)
            _addrStreetline_r = value
        End Set
    End Property
    Public Property addrZipCode_r() As String
        Get
            Return _addrZipCode_r
        End Get
        Set(ByVal value As String)
            _addrZipCode_r = value
        End Set
    End Property
    Public Property addrCity_r() As String
        Get
            Return _addrCity_r
        End Get
        Set(ByVal value As String)
            _addrCity_r = value
        End Set
    End Property
    Public Property addrState_r() As String
        Get
            Return _addrState_r
        End Get
        Set(ByVal value As String)
            _addrState_r = value
        End Set
    End Property
    Public Property response_r() As String
        Get
            Return _response_r
        End Get
        Set(ByVal value As String)
            _response_r = value
        End Set
    End Property
    Public Property addr_changes_r() As String
        Get
            Return _addr_changes_r
        End Get
        Set(ByVal value As String)
            _addr_changes_r = value
        End Set
    End Property
    Public Property error_r() As String
        Get
            Return _error_r
        End Get
        Set(ByVal value As String)
            _error_r = value
        End Set
    End Property
    Public Property SOAP_err_r() As String
        Get
            Return _SOAP_err_r
        End Get
        Set(ByVal value As String)
            _SOAP_err_r = value
        End Set
    End Property
    Public Property score_r() As String
        Get
            Return _score_r
        End Get
        Set(ByVal value As String)
            _score_r = value
        End Set
    End Property
    Public Property addr_country_code_r() As String
        Get
            Return _addrCountryCode_r
        End Get
        Set(ByVal value As String)
            _addrCountryCode_r = value
        End Set
    End Property
End Class




