﻿Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions
Imports System.Security.Policy
Partial Class ops_pages_guia_mult
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim empresa As Empresa
            Dim logoImageBase As Byte()
            If Session("empresa") IsNot Nothing Then
                Try
                    empresa = CType(Session("empresa"), Empresa)
                    If empresa.logo IsNot Nothing Then
                        logoImageBase = empresa.logo
                    End If
                Catch ex As Exception

                End Try
            End If

            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString
            Dim archivo As String = ""
            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.Text
            connection.Open()
            cmd.CommandText = "select r.archivo from dbo.c_agencias a " &
                              "inner join dbo.c_recibos r on r.id_recibo=a.id_recibo " &
                              "where id_agencia = '" & Request.QueryString("id_agente") & "'"
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                archivo = reader.GetValue(0)
            End If
            connection.Close()


            Guia.DataBind()
            'Crear Guia Tabsa
            Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
            Dim streamids As String() = Nothing
            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim extension As String = Nothing
            Dim deviceInfo As String
            Dim bytes As Byte()
            Dim lr As New Microsoft.Reporting.WebForms.LocalReport
            lr.EnableExternalImages = True

            lr.EnableExternalImages = True
            lr.ReportPath = Server.MapPath("~/Reports/" & archivo)
            lr.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet2_sp_Select_Datos_Envio_Mult", Guia()))
            Dim parameter As New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Convert.ToBase64String(logoImageBase))
            lr.SetParameters(parameter)
            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"
            bytes = lr.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            Response.ContentType = "Application/pdf"
            Response.BinaryWrite(bytes)
            Response.End()
        Catch ex As Exception
            TxtMsg.Text = "El número de guías supera el máximo de 100 " & vbCrLf &
            "o no existen gías para el rango y agente indicados" & vbCrLf &
            ex.Message.ToString
        End Try

    End Sub
End Class
