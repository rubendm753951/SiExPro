
Partial Class ops_pages_PreChequeo
    Inherits BasePage
    Protected Sub DropDownAgentes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgentes.SelectedIndexChanged
        Try
            TxtTarifa.Text = 0

            Dim idAgente As Integer = 0
            Integer.TryParse(DropDownAgentes.SelectedValue, idAgente)
            Dim Crear_Envio As New Insertar_Envios
            If Crear_Envio.AgenteCOD(idAgente) Then
                TxtSeguro.Text = ConfigurationManager.AppSettings("ValorCOD")
            Else
                TxtSeguro.Text = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DropDownProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownProduct.SelectedIndexChanged
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_SelectTarifas_por_Agente"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_agencia"
            parm1.Value = DropDownAgentes.SelectedValue
            cmd.Parameters.Add(parm1)

            connection.Open()
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                Do While reader.GetInt32(0) <> DropDownProduct.SelectedValue
                    reader.Read()
                Loop
                TxtTarifa.Text = Format(reader.GetValue(6), "$#,##0.00;($#,##0.00);$0.00")
                Session("dimension_peso") = reader.GetString(12)

            End If
            connection.Close()
        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Label2.Text = ""
    End Sub

    Protected Sub Inserta_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal id_envio_imp As Integer = 0, Optional ByVal genera_guia As Boolean = True) 'Handles Inserta.Click
        Try
            Dim Mensaje As String = "" 'para devolver resultado de validación de mensajes
            Dim datos_envio As New ObjEnvio
            Dim Crear_Envio As New Insertar_Envios


            'Insertar el Envío
            Dim id_envio As Integer
            datos_envio.id_agente = DropDownAgentes.Text 'it's an argument calling the method
            datos_envio.precio = TxtTarifa.Text
            datos_envio.valor_seguro = TxtSeguro.Text
            datos_envio.id_tarifa_agencia = DropDownProduct.SelectedValue
            datos_envio.id_codigo_promocion = Nothing
            datos_envio.valor_aduana = Nothing
            datos_envio.total_envio = datos_envio.precio + datos_envio.valor_seguro
            datos_envio.fecha = DateTime.Now.ToString
            datos_envio.instrucciones_entrega = ""
            datos_envio.observaciones = Nothing   'future reference 
            datos_envio.id_usuario = Session("id_usuario")
            datos_envio.id_ruta = DropDownRuta.SelectedValue
            datos_envio.id_destinatario = 0
            datos_envio.id_cliente = 0
            datos_envio.largo = 0
            datos_envio.ancho = 0
            datos_envio.alto = 0
            datos_envio.peso = 0
            datos_envio.referencia = ""
            datos_envio.contenido = DropDownContenidos.Text
            datos_envio.dimension_peso = ""
            datos_envio.contenedores = 0

            'PreRegistor del Envío
            Mensaje = Crear_Envio.valida_preregistro(datos_envio)
            If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                id_envio = Crear_Envio.PreRegistro_Envios(DropDownAgentes.Text, datos_envio, id_envio_imp)
            Else
                Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                ModalPopupExtender3.Show()
                Mensaje = ""
                Exit Sub
            End If

            'Insertar SobreCargos
            Crear_Envio.inserta_SobreCargos(id_envio)

            'Inserta seguimiento
            Dim ins_seguimiento As New seguimiento_envios
            ins_seguimiento.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, "", Session("id_usuario"))

            If genera_guia Then ' Si los datos vienen por importacion no requiren guia
                Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('guia_iata.aspx?id_envio1=" & id_envio.ToString & "&id_agente=" & datos_envio.id_agente & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
            End If
            For Each Ctrl In Panel4.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    If txt.ID = "txtLargo" Or txt.ID = "txtAncho" Or txt.ID = "txtAlto" Or txt.ID = "txtPeso" Or txt.ID = "txtIdEnvio" Then
                        txt.Text = 0
                    ElseIf txt.ID <> "TxtTarifa" Then
                        txt.Text = ""
                    End If
                End If
                If (Ctrl.GetType() Is GetType(DropDownList)) Then
                    Dim cbobx As DropDownList = CType(Ctrl, DropDownList)
                    'MsgBox(cbobx.ID.ToString)
                    If cbobx.ID <> "DropDownAgentes" And cbobx.ID <> "DropDownProduct" Then
                        cbobx.SelectedIndex = -1
                    Else
                    End If
                End If
            Next
            TxtSeguro.Text = "0"
        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub
End Class
