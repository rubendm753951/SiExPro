Imports System
Imports System.Collections

' Implements the strongly typed collection of Books
Public Class Track_Records
    Inherits CollectionBase
    Default Public Property Item(ByVal Index As Integer) As Track_Record
        Get
            Return CType(List.Item(Index), Track_Record)
        End Get
        Set(ByVal Value As Track_Record)
            List.Item(Index) = Value
        End Set
    End Property
    Public Function Add(ByVal Item As Track_Record) As Integer
        Return List.Add(Item)
    End Function
End Class

' Implements the Track_Record class
Public Class Track_Record
    Private PTracking_Number As String
    Private PTracking_ID As String
    Private PTrack_Desc As String
    Private PTime_Stamp As Date
    Private PEvent_Desc As String
    Private PCity As String
    Private PState As String

    Public Sub New()
        ' Empty constructor is needed for serialization
    End Sub
    Public Sub New(ByVal Tracking_Number As String, ByVal Tracking_ID As String, ByVal Track_Desc As String, _
                   ByVal Time_Stamp As Date, ByVal Event_Desc As String, ByVal City As String, ByVal State As String)
        PTracking_Number = Tracking_Number
        PTracking_ID = Tracking_ID
        PTrack_Desc = Track_Desc
        PTime_Stamp = Time_Stamp
        PEvent_Desc = Event_Desc
        PCity = City
        PState = State
    End Sub

    Public Property Tracking_Number() As String
        Get
            Return PTracking_Number
        End Get
        Set(ByVal Value As String)
            PTracking_Number = Value
        End Set
    End Property
    Public Property Tracking_ID() As String
        Get
            Return PTracking_ID
        End Get
        Set(ByVal Value As String)
            PTracking_ID = Value
        End Set
    End Property
    Public Property Track_Desc() As String
        Get
            Return PTrack_Desc
        End Get
        Set(ByVal value As String)
            PTrack_Desc = value
        End Set
    End Property
    Public Property Time_Stamp() As Date
        Get
            Return PTime_Stamp
        End Get
        Set(ByVal value As Date)
            PTime_Stamp = value
        End Set
    End Property
    Public Property Event_Desc() As String
        Get
            Return PEvent_Desc
        End Get
        Set(ByVal value As String)
            PEvent_Desc = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return PCity
        End Get
        Set(ByVal value As String)
            PCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return PState
        End Get
        Set(ByVal value As String)
            PState = value
        End Set
    End Property
End Class

