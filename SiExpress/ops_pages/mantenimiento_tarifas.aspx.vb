
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports SiExProData

Partial Class ops_pages_mantenimiento_tarifas
    Inherits BasePage
    Public Shared Paso As String = ""
    Private Shared _idModulo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim modulo As Modulo = DaspackDALC.GetModuloPorDescripcion(Me.AppRelativeVirtualPath.ToString)

        If modulo IsNot Nothing Then
            _idModulo = modulo.IdModulo
        End If

        If Not IsPostBack Then

        End If
    End Sub
    <WebMethod()>
    Public Shared Function ReadFile(fileFullName As String, proveedor As Integer) As genericResponse
        Dim response As New genericResponse
        Dim listField As New ArrayList
        Try
            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim tarifas As New List(Of Object)

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(ConfigurationManager.AppSettings("fullPath") + fileFullName, Encoding.Default)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")

                Dim currentRow As String()
                Dim isFirstRow As Boolean = True
                While Not MyReader.EndOfData
                    currentRow = MyReader.ReadFields()
                    If Not isFirstRow Then
                        If currentRow(0).Trim <> "" AndAlso currentRow(1).Trim <> "" Then
                            Select Case proveedor
                                Case 10 'FEDEX
                                    tarifas.Add(TarifasFedExRow(currentRow, fileFullName))
                                Case 30 'ESTAFETA
                                    tarifas.Add(TarifasEstafetaRow(currentRow, fileFullName))
                                Case 40 'PE
                                    tarifas.Add(TarifasPaqueteExpressRow(currentRow, fileFullName))
                                Case 50 'DRAFT
                                    tarifas.Add(TarifasDraftRow(currentRow, fileFullName))
                                Case 51 'DliverExpress
                                    tarifas.Add(TarifasDliverExpress(currentRow, fileFullName))
                            End Select
                        End If
                    Else
                        isFirstRow = False
                    End If
                End While
                response.responseArray = New ArrayList(tarifas)
            End Using
            response.responseMessage = ""
            response.responseSuccess = True

            Return response
        Catch ex As Exception
            response.responseSuccess = False
            response.responseMessage = "Ocurrió un error al leer archivo -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function
    <WebMethod(EnableSession:=True)>
    Public Shared Function GetAgentes() As genericResponse
        Dim response As New genericResponse
        Try

            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim agentes = DaspackDALC.AgentesPorUsuario(1)
            If agentes IsNot Nothing Then
                response.responseArray = New ArrayList(agentes.Where(Function(x) x.IdAgente > 0).OrderBy(Function(x) x.Orden).ThenBy(Function(x) x.Nombre).ToList())
            End If

            response.responseMessage = ""
            response.responseSuccess = True

            Return response
        Catch ex As Exception
            response.responseSuccess = False
            response.responseMessage = "Ocurrió un error al leer archivo -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function
    <WebMethod(EnableSession:=True)>
    Public Shared Function ActualizasTarifas(tarifas As List(Of Object), idProveedor As Integer, idAgente As Integer) As genericResponse
        Dim response As New genericResponse
        Try

            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim serializer As New JavaScriptSerializer()
            Select Case idProveedor
                Case 10 'FEDEX
                    Dim tarifasFedex = serializer.ConvertToType(Of List(Of TarifaAgenciaFedex))(tarifas)
                    response.responseMessage = DaspackDALC.ActualizaTarifas(tarifasFedex, idAgente)
                Case 30 'ESTAFETA
                    Dim tarifasEstafeta = serializer.ConvertToType(Of List(Of TarifaAgenciaEstafeta))(tarifas)
                    response.responseMessage = DaspackDALC.ActualizaTarifas(tarifasEstafeta, idAgente)
                Case 40 'PE
                    Dim tarifasPE = serializer.ConvertToType(Of List(Of TarifaAgenciaPaqueteExpress))(tarifas)
                    response.responseMessage = DaspackDALC.ActualizaTarifas(tarifasPE, idAgente)
                Case 50 'DRAFT
                    Dim tarifasDraft = serializer.ConvertToType(Of List(Of TarifaAgenciaDraft))(tarifas)
                    response.responseMessage = DaspackDALC.ActualizaTarifas(tarifasDraft, idAgente)
                Case 51 'DLIVER EXPRESS
                    Dim tarifasDliverExpress = serializer.ConvertToType(Of List(Of TarifaAgenciaDliverExpress))(tarifas)
                    response.responseMessage = DaspackDALC.ActualizaTarifas(tarifasDliverExpress, idAgente)
            End Select

            response.responseSuccess = True

            Return response
        Catch ex As Exception
            response.responseSuccess = False
            response.responseMessage = "Ocurrió un error al actualizar tarifas -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function



    Private Shared Function TarifasDraftRow(row As String(), fileName As String) As TarifaAgenciaDraft
        Dim tarifa As New TarifaAgenciaDraft

        With tarifa
            .ID_AGENCIA = row(1)
            .ID_ZONA = row(2)
            .ID_CUENTA = row(3)
            .SERVICE_ID = row(4)
            .PESO_LIMITE_INFERIOR = row(5)
            .PESO_LIMITE_SUPERIOR = row(6)
            .PRECIO = row(7)
            .PRECIO_KILO = row(8)
            .PRECIO_KILO_ADICIONAL = row(9)
            .PRECIO_AREA_EXTENDIDA = row(10)
            .PRECIO_EXCESO_DIMENSIONES = row(11)
            .IsDeleted = row(11)
        End With

        Return tarifa
    End Function
    Private Shared Function TarifasPaqueteExpressRow(row As String(), fileName As String) As TarifaAgenciaPaqueteExpress
        Dim tarifa As New TarifaAgenciaPaqueteExpress

        With tarifa
            .ID_AGENCIA = row(1)
            .Tarifa = row(2)
            .KG_start = row(3)
            .KG_End = row(4)
            .CUM_start = Convert.ToDecimal(IIf(row(5) = "NULL", 0, row(5)))
            .CUM_end = Convert.ToDecimal(IIf(row(6) = "NULL", 0, row(6)))
            .PRECIO_ECONOMICO = row(7)
            .DIA_SIGUIENTE_BASE = row(8)
            .DIA_SIGUIENTE_KILO = row(9)
            .is_deleted = row(10)
        End With

        Return tarifa
    End Function

    Private Shared Function TarifasFedExRow(row As String(), fileName As String) As TarifaAgenciaFedex
        Dim tarifa As New TarifaAgenciaFedex

        With tarifa
            .ID_AGENCIA = row(1)
            .ID_CUENTA = row(2)
            .SERVICE_ID = row(3)
            .PESO_LIMITE_INFERIOR = row(4)
            .PESO_LIMITE_SUPERIOR = row(5)
            .PRECIO = row(6)
            .PRECIO_KILO = row(7)
            .PRECIO_KILO_ADICIONAL = row(8)
            .PRECIO_AREA_EXTENDIDA = row(9)
            .PRECIO_EXCESO_DIMENSIONES = row(10)
            .IsDeleted = row(11)
        End With

        Return tarifa
    End Function

    Private Shared Function TarifasEstafetaRow(row As String(), fileName As String) As TarifaAgenciaEstafeta
        Dim tarifa As New TarifaAgenciaEstafeta

        With tarifa
            .ID_AGENCIA = row(1)
            .ID_ZONA = row(2)
            .ID_CUENTA = row(3)
            .PESO_LIMITE_INFERIOR = row(4)
            .PESO_LIMITE_SUPERIOR = row(5)
            .PRECIO = row(6)
            .PRECIO_POR_KILO = row(7)
            .PRECIO_KILO_ADICIONAL = row(8)
            .PRECIO_DIA_SIGUIENTE = row(9)
            .PRECIO_POR_KILO_DIA_SIGUIENTE = row(10)
            .PRECIO_KILO_ADICIONAL_DIA_SIGUIENTE = row(11)
            .PRECIO_GOMBAR = row(12)
            .PRECIO_POR_KILO_GOMBAR = row(13)
            .PRECIO_KILO_ADICIONAL_GOMBAR = row(14)
            .IsDeleted = row(15)
        End With

        Return tarifa
    End Function

    Private Shared Function TarifasDliverExpress(row As String(), fileName As String) As TarifaAgenciaDliverExpress
        Dim tarifa As New TarifaAgenciaDliverExpress

        With tarifa
            .ID_AGENCIA = row(1)
            .ID_ZONA = row(2)
            .ID_CUENTA = row(3)
            .SERVICE_ID = row(4)
            .PESO_LIMITE_INFERIOR = row(5)
            .PESO_LIMITE_SUPERIOR = row(6)
            .PRECIO = row(7)
            .PRECIO_KILO = row(8)
            .PRECIO_KILO_ADICIONAL = row(9)
            .PRECIO_AREA_EXTENDIDA = row(10)
            .PRECIO_EXCESO_DIMENSIONES = row(11)
            .IsDeleted = row(12)
        End With

        Return tarifa
    End Function
End Class
