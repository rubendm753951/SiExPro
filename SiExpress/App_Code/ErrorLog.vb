Option Strict On
Option Explicit On

Imports System
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Reflection

Public Class ErrorLog
    Inherits System.Data.Linq.DataContext

#Region "Extensibility Method Definitions"
    Partial Private Sub OnCreated()
    End Sub
#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(System.Configuration.ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString").ConnectionString)
        OnCreated()
    End Sub

    Public Sub LogError(ByVal modulo As String, ByVal ex As Exception, ByVal source As String, ByVal usuario As String)
        If ex.InnerException IsNot Nothing Then
            If ex.InnerException.InnerException IsNot Nothing Then
                ErrorLog(modulo, ex.InnerException.InnerException.Message, source, usuario, ex.StackTrace)
            Else
                ErrorLog(modulo, ex.InnerException.Message, source, usuario, ex.StackTrace)
            End If
        Else
            ErrorLog(modulo, ex.Message, source, usuario, ex.StackTrace)
        End If
    End Sub

    <FunctionAttribute(Name:="dbo.sp_insert_errores")>
    Public Function ErrorLog(<Parameter(Name:="Modulo", DbType:="Nvarchar(300)")> ByVal Modulo As String,
                            <Parameter(Name:="Descripcion", DbType:="Nvarchar(2000)")> ByVal Descripcion As String,
                            <Parameter(Name:="Fuente", DbType:="Nvarchar(2000)")> ByVal Fuente As String,
                            <Parameter(Name:="Usuario", DbType:="Nvarchar(50)")> ByVal Usuario As String,
                            <Parameter(Name:="Trace", DbType:="NText")> ByVal Trace As String) As Integer
        Try
            Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), Modulo, Descripcion, Fuente, Usuario, Trace)

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
