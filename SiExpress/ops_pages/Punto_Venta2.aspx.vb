Imports System.Data
Imports System.Web.Services
Imports System.Data.OleDb
Imports SiExProData

Partial Class Punto_Venta
    Inherits BasePage
    Public Shared Paso As String = ""
    Private Shared _idModulo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
        Dim modulo As Modulo = DaspackDALC.GetModuloPorDescripcion(Me.AppRelativeVirtualPath.ToString)

        If modulo IsNot Nothing Then
            _idModulo = modulo.IdModulo
        End If

        If IsPostBack Then
            If selAddBook.SelectedValue > 0 Then
                Inserta.Visible = False
                btnSave.Visible = True
            Else
                Inserta.Visible = True
                btnSave.Visible = False
            End If
        Else
            btnSave.Visible = False

            If (DaspackDALC.GetModuloPrivilegio(_idModulo, usuarioId, TipoPrivilegio.Escribe) = True) Then
                lblIdEnvio.Visible = True
                txtIdEnvio.Visible = True
                txtIdEnvio.Text = "0"
            Else
                lblIdEnvio.Visible = False
                txtIdEnvio.Visible = False
            End If
        End If

        '****************************************************************

        If Not IsPostBack Then
            DropDownCiudades.Visible = False
            DropDownCiudades2.Visible = False
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            If Not (GridView1.SelectedIndex = 0 And GridView1.PageIndex = 0) Then
                'PopupControlExtender.GetProxyForCurrentPopup(Me.Page).Commit(GridView1.SelectedValue)
                Dim row As GridViewRow = GridView1.SelectedRow
                'MsgBox(row.Cells(2).Text + " " + row.Cells(3).Text)
                txtNombre.Text = row.Cells(2).Text
                TxtApellidos.Text = row.Cells(3).Text
                txtEmpresa.Text = row.Cells(4).Text
                txtCalle.Text = row.Cells(5).Text
                'TxtNoExt.Text = row.Cells(5).Text
                'TxtNoInt.Text = row.Cells(6).Text
                txtEdo.SelectedValue = row.Cells(7).Text
                If DropDownPais.SelectedValue = 52 Then
                    DropDownCiudades.DataBind()
                    DropDownCiudades.SelectedIndex = DropDownCiudades.Items.IndexOf(DropDownCiudades.Items.FindByText(row.Cells(6).Text.ToUpper().Trim))
                Else
                    txtCiudad.Text = row.Cells(6).Text
                End If

                'txtMpio.Text = row.Cells(9).Text
                txtTelefono.Text = row.Cells(8).Text
                txtEmail.Text = row.Cells(9).Text
                TxtCP.Text = row.Cells(10).Text

                Session("id_cliente") = row.Cells(1).Text
                TextBox1.Text = ""
                GridView1.DataBind()
                'DropDownPais.DataBind()
                'Panel2.Enabled = False
            Else
                Panel2.Enabled = True
                txtNombre.Focus()
                TextBox1.Text = ""
                GridView1.DataBind()
                Session("id_cliente") = 0
            End If
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Me.ModalPopupExtender1.Show()
    End Sub
    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        Me.ModalPopupExtender2.Show()
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

            'Dim parm2 As Data.Common.DbParameter = cmd.CreateParameter()
            'parm2.ParameterName = "@id_tipo"
            'parm2.Value = 1
            'cmd.Parameters.Add(parm2)

            'Dim parm3 As Data.Common.DbParameter = cmd.CreateParameter()
            'parm3.ParameterName = "@activado"
            'parm3.Value = True
            'cmd.Parameters.Add(parm3)

            connection.Open()
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                Do While reader.GetInt32(0) <> DropDownProduct.SelectedValue
                    reader.Read()
                Loop
                TxtTarifa.Text = Format(reader.GetValue(6), "$#,##0.00;($#,##0.00);$0.00")
                Session("dimension_peso") = reader.GetString(12)
                lblPeso.Text = "Peso (" & Session("dimension_peso") & ")"
            End If
            connection.Close()
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'If Panel2.Enabled = False Then
        '    Panel2.Enabled = True
        'End If
        GridView1.DataBind()
        ModalPopupExtender1.Show()
        'Button6_Click(sender, e)
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If Panel3.Enabled = False Then
        '    Panel3.Enabled = True
        'End If
        GridView2.DataBind()
        ModalPopupExtender2.Show()
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Try
            If Not (GridView2.SelectedIndex = 0 And GridView2.PageIndex = 0) Then
                'PopupControlExtender.GetProxyForCurrentPopup(Me.Page).Commit(GridView1.SelectedValue)
                Dim row As GridViewRow = GridView2.SelectedRow
                'MsgBox(row.Cells(2).Text + " " + row.Cells(3).Text)
                TxtNombre2.Text = row.Cells(2).Text
                txtApellidos2.Text = row.Cells(3).Text
                txtEmpresa2.Text = row.Cells(4).Text
                txtCalle2.Text = row.Cells(5).Text
                'TxtNoExt2.Text = row.Cells(5).Text
                'TxtNoInt2.Text = row.Cells(6).Text

                txtEdo2.SelectedValue = row.Cells(7).Text
                If DropDownPais2.SelectedValue = 52 Then
                    DropDownCiudades2.DataBind()
                    DropDownCiudades2.SelectedIndex = DropDownCiudades2.Items.IndexOf(DropDownCiudades2.Items.FindByText(row.Cells(6).Text.ToUpper.Trim))
                Else
                    txtCiudad2.Text = row.Cells(6).Text
                End If
                'txtMpio2.Text = row.Cells(9).Text
                txtTelefono2.Text = row.Cells(8).Text
                txtEmail2.Text = row.Cells(9).Text
                TxtCP2.Text = row.Cells(10).Text

                Session("id_destinatario") = row.Cells(1).Text
                TxtBuscaDest.Text = ""
                GridView2.DataBind()
                'Panel3.Enabled = False
            Else
                'Panel3.Enabled = True
                TxtNombre2.Focus()
                TxtBuscaDest.Text = ""
                GridView2.DataBind()
                Session("id_destinatario") = 0
            End If
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub Inserta_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal id_envio_imp As Integer = 0, Optional ByVal genera_guia As Boolean = True) 'Handles Inserta.Click
        Try
            Dim Mensaje As String = "" 'para devolver resultado de validación de mensajes
            Dim datos_cliente As New ObjCliente
            Dim Datos_Dest As New ObjDestinatario
            Dim datos_envio As New ObjEnvio

            If txtIdEnvio.Visible Then
                If txtIdEnvio.Text <> "0" Then
                    If IsNumeric(txtIdEnvio.Text) Then
                        id_envio_imp = CType(txtIdEnvio.Text, Integer)

                        datos_envio.id_envio = id_envio_imp

                        Dim seguimiento As New seguimiento_envios
                        seguimiento.consulta_envio(datos_envio, datos_cliente, Datos_Dest)

                        If datos_envio.id_tarifa_agencia IsNot Nothing And datos_envio.id_tarifa_agencia > 0 Then
                            Label2.Text = "El numero de envio ya se encuentra asignado."
                            ModalPopupExtender3.Show()
                            Exit Sub
                        End If
                    Else
                        Label2.Text = "Ocurrió un error, el numero de envio debe ser numerico. "
                        ModalPopupExtender3.Show()
                        Exit Sub
                    End If
                End If
            End If

            Dim Crear_Envio As New Insertar_Envios
            'Insetar nuevo cliente
            Dim id_cliente As Integer
            datos_cliente.id_pais = DropDownPais.SelectedValue
            datos_cliente.nombre = txtNombre.Text
            datos_cliente.apellidos = TxtApellidos.Text
            datos_cliente.empresa = txtEmpresa.Text
            datos_cliente.calle = txtCalle.Text
            datos_cliente.noexterior = 0 ' Estamos pasando la dirección completa en el campo de calle.
            datos_cliente.nointerior = Nothing
            datos_cliente.direccion2 = Nothing
            datos_cliente.colonia = TxtCol.Text
            If datos_cliente.id_pais = 52 Then
                If DropDownCiudades.SelectedItem.Value > 0 Then
                    datos_cliente.ciudad = DropDownCiudades.SelectedItem.Text
                Else
                    datos_cliente.ciudad = ""
                End If
            Else
                datos_cliente.ciudad = txtCiudad.Text
            End If

            datos_cliente.municipio = TxtMpio.Text
            datos_cliente.estadoprovincia = txtEdo.Text
            datos_cliente.telefono = txtTelefono.Text
            datos_cliente.codigo_postal = TxtCP.Text
            datos_cliente.email = txtEmail.Text


            Mensaje = Crear_Envio.valida_datos_cliente(datos_cliente)
            If Mensaje = "OK" Then
                id_cliente = Crear_Envio.crea_cliente(datos_cliente)
                Session("id_cliente") = id_cliente
            Else
                Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                ModalPopupExtender3.Show()
                Exit Sub
            End If

            'Insetar nuevo destinataro
            Dim id_destinatario As Integer
            Datos_Dest.id_pais = DropDownPais2.SelectedValue
            Datos_Dest.nombre = TxtNombre2.Text
            Datos_Dest.apellidos = txtApellidos2.Text
            Datos_Dest.empresa = txtEmpresa2.Text
            Datos_Dest.calle = txtCalle2.Text
            Datos_Dest.noexterior = 0 ' Estamos pasando la dirección completa en el campo de calle.
            Datos_Dest.nointerior = Nothing
            Datos_Dest.direccion2 = Nothing
            Datos_Dest.colonia = TxtCol2.Text
            If Datos_Dest.id_pais = 52 Then
                If DropDownCiudades2.SelectedItem.Value > 0 Then
                    Datos_Dest.ciudad = DropDownCiudades2.SelectedItem.Text
                Else
                    Datos_Dest.ciudad = ""
                End If

            Else
                Datos_Dest.ciudad = txtCiudad2.Text
            End If

            Datos_Dest.municipio = TxtMpio2.Text
            Datos_Dest.estadoprovincia = txtEdo2.Text
            Datos_Dest.telefono = txtTelefono2.Text
            Datos_Dest.email = txtEmail2.Text
            If Not IsNothing(TxtCP2.Text) And Len(TxtCP2.Text) = 5 Then
                Datos_Dest.codigo_postal = TxtCP2.Text
            Else
                Datos_Dest.codigo_postal = Crear_Envio.valida_cp_mx(Datos_Dest.ciudad, txtEdo2.SelectedItem.Text)
            End If

            Mensaje = Crear_Envio.valida_datos_dest(Datos_Dest)
            If Mensaje = "OK" Then
                id_destinatario = Crear_Envio.crea_destinatario(Datos_Dest)
                Session("id_destinatario") = id_destinatario
            Else
                Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                ModalPopupExtender3.Show()
                Exit Sub
            End If

            Dim cajas_count As Integer = 0
            Dim envios(CInt(TxtCajas.Text) - 1) As Integer

            If Not guia_por_caja.Checked Then
                ReDim envios(0)
            End If


            Do While cajas_count < envios.Length()

                'Insertar el Envío
                Dim id_envio As Integer
                datos_envio.id_agente = DropDownAgentes.Text 'it's an argument calling the method
                datos_envio.precio = TxtTarifa.Text
                datos_envio.valor_seguro = TxtSeguro.Text
                datos_envio.id_tarifa_agencia = DropDownProduct.SelectedValue

                If TxtPromo.Text <> "" And IsNumeric(TxtPromo.Text) Then
                    datos_envio.id_codigo_promocion = TxtPromo.Text
                Else
                    datos_envio.id_codigo_promocion = Nothing
                End If

                If TxtAduana.Text <> "" And IsNumeric(TxtAduana.Text) Then
                    datos_envio.valor_aduana = TxtAduana.Text
                Else
                    datos_envio.valor_aduana = Nothing
                End If

                datos_envio.total_envio = datos_envio.precio + datos_envio.valor_seguro
                datos_envio.fecha = DateTime.Now.ToString
                datos_envio.instrucciones_entrega = TxtInstEntrega.Text
                datos_envio.observaciones = Nothing   'future reference 
                datos_envio.id_usuario = Session("id_usuario")

                Dim idRuta As Integer = 0
                If Datos_Dest.id_pais = 52 Then
                    Dim db As New SiExProEntities
                    Dim ciudadRuta As CiudadesRutas = db.D_CIUDADES_RUTAS.FirstOrDefault(Function(x) x.id_ciudad = DropDownCiudades2.SelectedValue)

                    If ciudadRuta IsNot Nothing Then
                        idRuta = ciudadRuta.id_ruta
                    End If

                End If

                datos_envio.id_ruta = idRuta
                datos_envio.id_destinatario = id_destinatario
                datos_envio.id_cliente = id_cliente
                datos_envio.largo = txtLargo.Text
                datos_envio.ancho = txtAncho.Text
                datos_envio.alto = txtAlto.Text
                datos_envio.peso = txtPeso.Text
                datos_envio.referencia = TxtRef.Text
                datos_envio.contenido = DropDownContenidos.Text
                datos_envio.dimension_peso = Session("dimension_peso")
                datos_envio.contenedores = TxtCajas.Text

                'PreRegistor del Envío
                Mensaje = Crear_Envio.valida_preregistro(datos_envio)
                If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                    If txtIdEnvioPrechequeado.Text = "" Then
                        id_envio = Crear_Envio.PreRegistro_Envios(DropDownAgentes.Text, datos_envio, id_envio_imp)
                    Else
                        datos_envio.id_envio = txtIdEnvioPrechequeado.Text
                        id_envio = Crear_Envio.Update_PreRegistro_Envios(DropDownAgentes.Text, datos_envio, id_envio_imp)
                    End If
                Else
                    Label2.Text = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                    ModalPopupExtender3.Show()
                    Mensaje = ""
                    Exit Sub
                End If

                'Registro de Envíos (Detalles)
                Crear_Envio.Detalle_Envios(id_envio, datos_envio)
                TextBox2.Text = id_envio.ToString

                Guia.DataBind()

                If txtIdEnvioPrechequeado.Text = "" Then
                    'Insertar SobreCargos
                    Crear_Envio.inserta_SobreCargos(id_envio)

                    'Inserta seguimiento
                    Dim ins_seguimiento As New seguimiento_envios
                    ins_seguimiento.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath.ToString, "", Session("id_usuario"))

                End If

                envios(cajas_count) = id_envio
                cajas_count = cajas_count + 1
                Label1.Text = "Útimo envío-> " & id_envio.ToString & " fue creado con exito"
            Loop

            If genera_guia Then ' Si los datos vienen por importacion no requiren guia
                Dim sjscript2 As String = "<script language=""javascript"">" &
                        " window.open('guia_mult.aspx?id_envio1=" & envios(0).ToString & "&id_envio2=" & envios(cajas_count - 1).ToString & "&id_agente=" & datos_envio.id_agente & "','','width=600,height=800, toolbar=1, scrollbars=1')" &
                        "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sjscript2, False)
            End If

            Dim Ctrl As Control
            For Each Ctrl In Panel2.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) And CBFijaOrigen.Checked = False Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Text = ""
                End If
                If (Ctrl.GetType() Is GetType(DropDownList)) And CBFijaOrigen.Checked = False Then
                    Dim cbobx As DropDownList = CType(Ctrl, DropDownList)
                    If cbobx.ID <> "DropDownPais" Then
                        cbobx.SelectedIndex = -1
                    End If
                End If
            Next
            For Each Ctrl In Panel3.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Text = ""
                End If
            Next
            For Each Ctrl In Panel4.Controls
                If (Ctrl.GetType() Is GetType(TextBox)) And CBFijaOrigen.Checked = False Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    If txt.ID = "txtLargo" Or txt.ID = "txtAncho" Or txt.ID = "txtAlto" Or txt.ID = "txtPeso" Or txt.ID = "txtIdEnvio" Then
                        txt.Text = 0
                    ElseIf txt.ID <> "TxtTarifa" Then
                        txt.Text = ""
                    End If
                End If
                If (Ctrl.GetType() Is GetType(DropDownList)) And CBFijaOrigen.Checked = False Then
                    Dim cbobx As DropDownList = CType(Ctrl, DropDownList)
                    'MsgBox(cbobx.ID.ToString)
                    If cbobx.ID <> "DropDownAgentes" And cbobx.ID <> "DropDownProduct" Then
                        cbobx.SelectedIndex = -1
                    Else
                    End If
                End If
            Next
            If CBFijaOrigen.Checked = False Then
                Session("id_cliente") = 0
                id_cliente = 0
            End If
            Session("id_destinatario") = 0
            id_destinatario = 0

            TxtCajas.Text = "1"
            TxtSeguro.Text = "0"
            txtPeso.Text = "1"
            TxtSeguro.Text = "60"
            guia_por_caja.Checked = False


            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString
            connection.Open()

            Dim cmd2 As Data.IDbCommand = connection.CreateCommand()
            cmd2.CommandType = Data.CommandType.StoredProcedure
            cmd2.CommandText = "sp_Select_Envios_Pendientes_Uso"

            Dim parm2 As Data.Common.DbParameter = cmd2.CreateParameter()
            parm2.ParameterName = "@id_agencia"
            parm2.Value = DropDownAgentes.SelectedValue
            cmd2.Parameters.Add(parm2)

            Dim reader2 As Data.SqlClient.SqlDataReader = cmd2.ExecuteReader()
            If reader2.HasRows Then
                reader2.Read()
                EnviosAsignados.Text = Format(reader2.GetValue(0), "#,##;(#,##);0")
            End If
            reader2.Close()
            connection.Close()


        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub btnDatosUltimoEnvio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDatosUltimoEnvio.Click
        Dim id_usuario As Integer = Session("id_usuario")
        Dim mensaje As String = ""
        Try
            Dim envio As Envio = DaspackDALC.GetUltimoEnvio(id_usuario)
            If envio IsNot Nothing Then
                Dim tarifaAgencia As TarifaAgencia = DaspackDALC.GetTarifaAgencia(envio.id_tarifa_agencia)
                If tarifaAgencia IsNot Nothing Then
                    DropDownAgentes.SelectedValue = tarifaAgencia.id_agencia
                    DropDownProduct.DataBind()
                    DropDownProduct.SelectedValue = tarifaAgencia.id_tarifa_agencia
                End If

                With envio
                    TxtTarifa.Text = .precio
                    TxtSeguro.Text = .valor_seguro
                    TxtPromo.Text = .id_codigo_promocion
                    TxtAduana.Text = .valor_aduana
                    TxtInstEntrega.Text = .instrucciones_entrega

                    txtLargo.Text = .largo
                    txtAncho.Text = .ancho
                    txtAlto.Text = .alto
                    txtPeso.Text = .peso
                    TxtRef.Text = .Referencia1
                    DropDownContenidos.SelectedValue = .id_contenido
                    TxtCajas.Text = .contenedores
                End With

                Dim cliente As Cliente = DaspackDALC.GetDatosCliente(envio.id_envio)
                If cliente IsNot Nothing Then
                    With cliente
                        DropDownPais.SelectedValue = .id_pais
                        If DropDownPais.SelectedValue = 52 Then
                            txtCiudad.Visible = False
                            DropDownCiudades.Visible = True
                        Else
                            txtCiudad.Visible = True
                            DropDownCiudades.Visible = False
                        End If

                        txtEdo.DataBind()
                        txtNombre.Text = .nombre
                        TxtApellidos.Text = .apellidos
                        txtEmpresa.Text = .empresa
                        txtCalle.Text = .calle
                        TxtCol.Text = .colonia
                        txtEdo.SelectedValue = .estadoprovincia

                        If DropDownPais.SelectedValue = 52 Then
                            DropDownCiudades.DataBind()
                            DropDownCiudades.SelectedIndex = DropDownCiudades.Items.IndexOf(DropDownCiudades.Items.FindByText(.ciudad.ToUpper().Trim()))
                        Else
                            txtCiudad.Text = .ciudad
                        End If

                        TxtMpio.Text = .municipio

                        txtTelefono.Text = .telefono
                        TxtCP.Text = .codigo_postal
                        txtEmail.Text = .email
                    End With
                End If

                Dim destinatario As Destinatario = DaspackDALC.GetDatosDestinatario(envio.id_envio)
                If destinatario IsNot Nothing Then
                    With destinatario
                        DropDownPais2.SelectedValue = .id_pais
                        If DropDownPais2.SelectedValue = 52 Then
                            txtCiudad2.Visible = False
                            DropDownCiudades2.Visible = True
                        Else
                            txtCiudad2.Visible = True
                            DropDownCiudades2.Visible = False
                        End If
                        txtEdo2.DataBind()
                        TxtNombre2.Text = .nombre
                        txtApellidos2.Text = .apellidos
                        txtEmpresa2.Text = .empresa
                        txtCalle2.Text = .calle
                        TxtCol2.Text = .colonia
                        txtEdo2.SelectedValue = .estadoprovincia
                        DropDownCiudades2.DataBind()

                        If DropDownPais2.SelectedValue = 52 Then
                            DropDownCiudades2.DataBind()
                            DropDownCiudades2.SelectedIndex = DropDownCiudades2.Items.IndexOf(DropDownCiudades2.Items.FindByText(.ciudad.ToUpper().Trim()))
                        Else
                            txtCiudad2.Text = .ciudad
                        End If

                        TxtMpio2.Text = .municipio

                        txtTelefono2.Text = .telefono
                        txtEmail2.Text = .email
                        TxtCP2.Text = .codigo_postal
                    End With
                End If
            Else
                mensaje = "El usuario no tiene envios creados."
            End If

        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + mensaje
            Dim errorLog = New ErrorLog
            errorLog.Log(HttpContext.Current.Request.Url.AbsoluteUri + " - " + Me.GetType().Name + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, ex.Source, id_usuario.ToString())
        End Try

    End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function createMasiveIndicium(ByVal requestSender As ObjCliente, ByVal requestEnvio As ObjEnvio, ByVal idAddBook As Integer) As createMasiveResponse
        Dim response As New createMasiveResponse
        Dim Mensaje As String
        Dim id_cliente As Integer
        Dim Crear_Envio As New Insertar_Envios
        Dim add = (New With {.Message = String.Empty, .id_dest = Integer.MinValue, .benAddError = Integer.Parse("0")})
        Dim BenefMessages As New ArrayList
        Dim shipments As New ArrayList
        Dim id_envio As Integer
        Dim db As New DaspackDataContext
        Try
            response.SendMessages = ""
            response.remAddError = 0

            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim dimensionPeso As String = CType(HttpContext.Current.Session("dimension_peso"), String)
            Dim appRelativeVirtualPath As String = CType(HttpContext.Current.Session("AppRelativeVirtualPath"), String)

            Mensaje = Crear_Envio.valida_datos_cliente(requestSender)
            If Mensaje = "OK" Then
                id_cliente = Crear_Envio.crea_cliente(requestSender)
                HttpContext.Current.Session.Add("id_cliente", id_cliente)
            Else
                response.SendMessages = "Ocurrió un error, por favor revise los datos ---> " + Mensaje
                Return response
            End If

            Dim addBook As IEnumerable(Of addressBook) = db.GetAddBookArchivo(idAddBook)
            Dim addBookClientes As List(Of addressBook) = New List(Of addressBook)(addBook.ToList())

            Dim tarifasAgencia As IEnumerable(Of TarifasAgencia) = db.GetTarifasAgencia(requestEnvio.id_agente)
            Dim tarifas As List(Of TarifasAgencia) = New List(Of TarifasAgencia)(tarifasAgencia.ToList())

            Dim result As IEnumerable(Of addBookClientes) = db.GetAddBookClientes(idAddBook)
            Dim requestBeneficiary = New ArrayList(result.ToArray)

            For Each Benef In requestBeneficiary
                Try
                    add.id_dest = Benef.id_cliente

                    requestEnvio.id_cliente = id_cliente
                    requestEnvio.id_destinatario = Benef.id_cliente
                    requestEnvio.dimension_peso = dimensionPeso
                    requestEnvio.fecha = DateTime.Now.ToString

                    Dim ab As addressBook = addBookClientes.FirstOrDefault(Function(a) a.id_destinatario = Benef.id_cliente.ToString())

                    requestEnvio.observaciones = ab.Observaciones
                    requestEnvio.contenedores = ab.Contenedor
                    requestEnvio.referencia = ab.Inventario

                    If ab.Transporte IsNot Nothing AndAlso ab.Transporte.ToUpper().Contains("FEDEX") Then
                        requestEnvio.FedExRef = ab.Guia
                    Else
                        requestEnvio.FedExRef = "0"
                        requestEnvio.observaciones = requestEnvio.observaciones + "Transporte: " + ab.Transporte + " - Guia: " + ab.Guia
                    End If

                    If ab.Servicio.ToUpper().Contains("DOM") Then
                        requestEnvio.id_tarifa_agencia = tarifas.FirstOrDefault(Function(t) t.id_tarifa = 3).id_tarifa_agencia
                    Else
                        requestEnvio.id_tarifa_agencia = tarifas.FirstOrDefault(Function(t) t.id_tarifa = 5).id_tarifa_agencia
                    End If

                    Dim cobranza As Decimal = 0
                    If IsNumeric(ab.Cobranza) Then
                        cobranza = CType(ab.Cobranza, Decimal)
                    End If

                    If ab.Cobranza IsNot Nothing Then
                        requestEnvio.valor_seguro = requestEnvio.valor_seguro + cobranza
                    End If

                    requestEnvio.id_usuario = usuarioId
                    requestEnvio.id_ruta = Nothing

                    'PreRegistor del Envío
                    Mensaje = Crear_Envio.valida_preregistro(requestEnvio)
                    If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                        id_envio = Crear_Envio.PreRegistro_Envios(requestEnvio.id_agente, requestEnvio, 0)
                    Else
                        Throw New Exception("Ocurrió un error, por favor revise los datos ---> " + Mensaje)
                    End If

                    'Registro de Envíos (Detalles)
                    Crear_Envio.Detalle_Envios(id_envio, requestEnvio)
                    'Insertar SobreCargos
                    Crear_Envio.inserta_SobreCargos(id_envio)
                    'Inserta seguimiento
                    Dim ins_seguimiento As New seguimiento_envios
                    ins_seguimiento.insertar_seguimiento(id_envio, appRelativeVirtualPath, "", usuarioId)

                    shipments.Add(id_envio)
                Catch ex As Exception
                    add.Message = ex.Message
                    BenefMessages.Add(add)
                End Try
            Next

            response.shipments = shipments
            response.BenefMessages = BenefMessages
            Return response
        Catch ex As Exception
            response.shipments = New ArrayList()

            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Label2.Text = ""
        'Button3.Visible = False
        'Panel2.Enabled = True
        'Panel3.Enabled = True
    End Sub

    Protected Sub DropDownAgentes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAgentes.SelectedIndexChanged
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_Select_datos_agente"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_agencia"
            parm1.Value = DropDownAgentes.SelectedValue
            cmd.Parameters.Add(parm1)

            connection.Open()
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                DropDownPais.SelectedValue = reader.GetInt32(1)
                DropDownPais2.SelectedValue = reader.GetInt32(1) ' For masupack

                If DropDownPais.SelectedValue = 52 Then
                    txtCiudad.Visible = False
                    DropDownCiudades.Visible = True
                Else
                    txtCiudad.Visible = True
                    DropDownCiudades.Visible = False
                End If

                If DropDownPais2.SelectedValue = 52 Then
                    txtCiudad2.Visible = False
                    DropDownCiudades2.Visible = True
                Else
                    txtCiudad2.Visible = True
                    DropDownCiudades2.Visible = False
                End If
            End If
            reader.Close()

            Dim cmd2 As Data.IDbCommand = connection.CreateCommand()
            cmd2.CommandType = Data.CommandType.StoredProcedure
            cmd2.CommandText = "sp_Select_Envios_Pendientes_Uso"

            Dim parm2 As Data.Common.DbParameter = cmd2.CreateParameter()
            parm2.ParameterName = "@id_agencia"
            parm2.Value = DropDownAgentes.SelectedValue
            cmd2.Parameters.Add(parm2)

            Dim reader2 As Data.SqlClient.SqlDataReader = cmd2.ExecuteReader()
            If reader2.HasRows Then
                reader2.Read()
                EnviosAsignados.Text = Format(reader2.GetValue(0), "#,##;(#,##);0")

            End If
            reader2.Close()
            connection.Close()
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
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Genera Guía FedEX en otra ventana y desactiva botón
        Button4.Enabled = False
        Dim sJScript3 As String = "<script language=""Javascript"">" &
" window.open('guia_fedex.aspx?id_envio=" & TextBox2.Text & "','','width=800,height=500, toolbar=1, Scrollbars=1')" &
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)

    End Sub
    Protected Sub BtnAddCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'TxtRemit.Text = ""
        'llama ventana de administración de clientes
        Button4.Enabled = False
        Dim sJScript3 As String = "<script language=""Javascript"">" &
" window.open('admin_clientes.aspx','','width=600,height=800, toolbar=1, Scrollbars=1')" &
"</script>"
        'Response.Write(sJScript2)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", sJScript3, False)

    End Sub
    Protected Sub TxtRemit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim valida_addr As FedEx_AddressValidation = New FedEx_AddressValidation
            valida_addr.Company = txtEmpresa2.Text
            valida_addr.Address_line = txtCalle2.Text
            valida_addr.City = txtCiudad2.Text
            valida_addr.StateProvince = txtEdo2.Text
            valida_addr.zip_code = IIf(TxtCP2.Text = "", 0, TxtCP2.Text)
            valida_addr.Country = DropDownPais2.SelectedItem.ToString

            Dim identity As Integer
            identity = valida_addr.addr_input

            Dim resut_valida As Integer
            resut_valida = valida_addr.addr_validation(identity)

            Dim datos_devueltos As New ObjAddressValidation
            Dim records As Integer
            records = valida_addr.GetData(identity, datos_devueltos)

            If records = 1 Then
                txtCalle2.Text = datos_devueltos.addrStreetline_r
                'txtCalle2.BackColor = Drawing.Color.Aqua
                txtCiudad.Text = datos_devueltos.addrCity_r
                txtEdo2.Text = datos_devueltos.addrState_r
                TxtCP2.Text = datos_devueltos.addrZipCode_r
                txtMessage.Text = datos_devueltos.addr_changes_r
                If datos_devueltos.addr_country_code_r <> DropDownPais2.SelectedItem.ToString Then
                    txtMessage.Text = "La dirección encontada corresponde al código de país " & datos_devueltos.addr_country_code_r
                End If
            Else
                txtMessage.Text = valida_addr.get_notis(identity)
            End If
        Catch ex As Exception
            'MsgBox("Ocurrió un error, por favor revise los datos ---> " + ex.Message.ToString)
            'Button3.Visible = True
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + ex.Message.ToString
            ModalPopupExtender3.Show()
        End Try
    End Sub

    Protected Sub CBFijaOrigen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBFijaOrigen.CheckedChanged
        If CBFijaOrigen.Checked = True Then
            Panel2.Enabled = False
            Panel4.Enabled = False
            BtnCargaRepositorio.Enabled = True
        Else
            Panel2.Enabled = True
            Panel4.Enabled = True
            BtnCargaRepositorio.Enabled = False
        End If
    End Sub

    Protected Sub BtnCargaRepositorio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCargaRepositorio.Click
        Try
            Dim MyConnection As ConnectionStringSettings
            MyConnection = ConfigurationManager.ConnectionStrings("paqueteriaDB_ConnectionString")
            Dim connection As Data.Common.DbConnection = New Data.SqlClient.SqlConnection()
            connection.ConnectionString = MyConnection.ConnectionString

            Dim cmd As Data.IDbCommand = connection.CreateCommand()
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "dbo.sp_select_tmp_importar"

            Dim parm1 As Data.Common.DbParameter = cmd.CreateParameter()
            parm1.ParameterName = "@id_usuario"
            parm1.Value = Session("id_usuario")
            cmd.Parameters.Add(parm1)

            connection.Open()
            Dim mensaje As String = ""
            Dim borrar As New Insertar_Envios
            Dim mensaje_borrar As String = ""
            Dim reader As Data.SqlClient.SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                While reader.Read()
                    TxtCajas.Text = 1
                    TxtSeguro.Text = reader.GetValue(10)
                    TxtInstEntrega.Text = reader.GetString(3)
                    TxtNombre2.Text = reader.GetString(4)
                    TxtCol2.Text = reader.GetString(5)
                    TxtMpio2.Text = reader.GetString(11)
                    txtApellidos2.Text = ""
                    TxtRef.Text = reader.GetString(1)
                    txtCalle2.Text = reader.GetString(8)
                    txtCiudad2.Text = reader.GetString(9)
                    txtEdo2.SelectedValue = reader.GetString(7)
                    txtTelefono2.Text = reader.GetString(6)
                    Inserta_Click(sender, Nothing, reader.GetString(2), False)
                    mensaje = mensaje + reader.GetString(2) + " " & TxtRef.Text & " En proceso de importacion" & vbCrLf
                    borrar.borrar_tmp(reader.GetValue(0), mensaje_borrar)
                End While
            End If
            connection.Close()
            txtMessage.Text = mensaje
        Catch ex As Exception
            txtMessage.Text = "Error, -->" + "Envío " & TxtRef.Text & ex.Message.ToString
        End Try
    End Sub

    <WebMethod()>
    Public Shared Function getTemplates() As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of Templates) = db.GetTemplates

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar templates -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    <WebMethod()>
    Public Shared Function getMandatoryFields() As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of camposObligatorios) = db.GetCamposObligatorios

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar campos obligatorios -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    <WebMethod()>
    Public Shared Function readFile(ByVal fileFullName As String, ByVal idTemplate As Integer, ByVal sheetName As String, ByVal rowHead As Integer) As readFileResponse
        Dim response As New readFileResponse
        Dim listField As New ArrayList
        Try
            listField = GetFileHeadColumnName(fileFullName, sheetName, rowHead - 2)
            response.fileFields = listField

            If idTemplate > 0 Then
                Dim db As New DaspackDataContext

                Dim result As IEnumerable(Of templateFields) = db.GetTemplateColumns(idTemplate)
                Dim resultFilter = (From r In result Select r.id_columna_archivo.ToString() + ":" + r.id_campo_obligatorio.ToString()).ToList()

                response.matchTemplate = New ArrayList(resultFilter.ToArray)
            End If

            response.responseMessage = ""

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al leer archivo -->" + ex.Message.ToString + " - " + Paso
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Private Shared Function GetFileHeadColumnName(ByVal fileName As String, ByVal sheetName As String, ByVal rowHead As Integer, Optional ByRef excelDt As DataTable = Nothing) As ArrayList
        Dim listField As New ArrayList
        Try
            Dim dt As DataTable
            dt = ReadExcelFile(fileName, sheetName, rowHead)

            excelDt = dt
            Dim rowIndex = 0
            For Each dr As DataRow In dt.Rows
                If rowIndex >= rowHead Then
                    listField = New ArrayList(dr.ItemArray)
                    Exit For
                End If
                rowIndex = rowIndex + 1
            Next
        Catch ex As Exception
            Throw
        End Try
        Return listField
    End Function

    Private Shared Function ReadExcelFile(ByVal fileName As String, ByVal sheetName As String, ByVal headRow As Integer) As DataTable
        Dim oledbConn As New OleDbConnection
        Dim path As String = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings("fullPath") + fileName)

        If IO.Path.GetExtension(path) = ".xls" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        ElseIf IO.Path.GetExtension(path) = ".csv" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"";")
        ElseIf IO.Path.GetExtension(path) = ".xlsx" Then
            oledbConn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';")
        End If

        Try
            ' Open connection
            oledbConn.Open()

            '' Create OleDbCommand object and select data from worksheet Sheet1
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [" + sheetName + "$]", oledbConn)
            ' Create new OleDbDataAdapter
            Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()

            oleda.SelectCommand = cmd

            '' Create a DataSet which will hold the data extracted from the worksheet.
            Dim ds As DataSet = New DataSet()

            ' Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds, "ImportedTable")

            Return ds.Tables(0)

        Catch ex As Exception
            Throw
        End Try
        oledbConn.Close()
    End Function

    <WebMethod()>
    Public Shared Function readFileContent(ByVal fileName As String, ByVal positions As String, ByVal sheetName As String, ByVal rowHead As Integer) As genericResponse
        Dim response As New genericResponse
        Dim listField As New ArrayList
        Dim pos() As String
        Dim match() As String
        Dim matches As String
        Dim rowCount As Integer
        Dim listHeadField As ArrayList
        Dim dt As New DataTable
        Try
            ReadExcelFile(fileName, sheetName, rowHead - 2)
            listHeadField = GetFileHeadColumnName(fileName, sheetName, rowHead - 2, dt)

            Dim rowIndex = 0
            Dim DBposition As Integer
            Dim fieldFile As String
            response.responseMessage = ""

            pos = positions.Split("|")
            rowCount = 1
            For Each dr As DataRow In dt.Rows
                If rowIndex >= rowHead - 2 Then
                    Dim addBook As New addressBook
                    addBook.Observaciones = ""
                    addBook.Message = ""
                    For Each matches In pos
                        addBook.id = rowCount

                        match = matches.Split(":")
                        DBposition = CInt(match(1))

                        fieldFile = dr.ItemArray(CInt(match(0)) - 1).ToString()
                        Select Case DBposition
                            Case 1
                                If fieldFile.Trim <> "" Then
                                    addBook.Contenedor = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Contenedor Vacio, "
                                End If
                            Case 2
                                If fieldFile.Trim <> "" Then
                                    addBook.Inventario = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Inventario Vacio, "
                                End If
                            Case 3
                                If fieldFile.Trim <> "" Then
                                    addBook.Destinatario = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Destinatario Vacio, "
                                End If
                            Case 4
                                If fieldFile.Trim <> "" Then
                                    addBook.Ciudad = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Ciudad Vacio, "
                                End If
                            Case 5
                                If fieldFile.Trim <> "" Then
                                    addBook.Estado = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Estado Vacio, "
                                End If
                            Case 6
                                addBook.Direccion = fieldFile.Trim
                            Case 7
                                addBook.codigo_postal = fieldFile.Trim
                            Case 8
                                If fieldFile.Trim <> "" Then
                                    addBook.Telefono = fieldFile.Trim
                                Else
                                    addBook.Message = addBook.Message + "Telefono Vacio, "
                                End If
                            Case 9
                                If fieldFile.Trim <> "" Then
                                    addBook.Servicio = fieldFile.Trim()
                                Else
                                    addBook.Message = addBook.Message + "Servicio Vacio, "
                                End If
                            Case 10
                                If fieldFile.Trim <> "" Then
                                    addBook.Cobranza = fieldFile.Trim()
                                Else
                                    addBook.Message = addBook.Message + "Cobranza Vacio, "
                                End If
                            Case 11
                                If fieldFile.Trim <> "" Then
                                    addBook.Transporte = fieldFile.Trim()
                                Else
                                    addBook.Message = addBook.Message + "Transporte Vacio, "
                                End If
                            Case 12
                                If fieldFile.Trim <> "" Then
                                    addBook.Guia = fieldFile.Trim()
                                Else
                                    addBook.Message = addBook.Message + "Guia Vacio, "
                                End If
                            Case 13
                                addBook.Observaciones = addBook.Observaciones + ", " + listHeadField(CInt(match(0)) - 1).ToString() + ":" + fieldFile.Trim()
                        End Select
                    Next

                    If (rowCount > 1) Then
                        listField.Add(addBook)
                        If addBook.Message <> "" Then
                            response.responseMessage = "Algunos registros del archivo contienen campos vacios."
                        End If
                    End If
                    rowCount = rowCount + 1
                End If
                rowIndex = rowIndex + 1
            Next
            response.responseSuccess = 1
            response.responseArray = New ArrayList(listField.ToArray)
            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al leer contenido de archivo -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try

    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function saveAddressBook(ByVal templateName As String, ByVal positions As String, ByVal addBookName As String, ByVal beneficiaries() As addressBook, ByVal pais As Integer, ByVal fileName As String) As genericResponse
        Dim response As New genericResponse
        Dim pos(), match(), matches, Mensaje As String
        Dim templateID, mandatoryField, columnFile, idAddBook As Integer
        Dim id_destinatario As Integer
        Dim idAddBookArchivo As Integer
        Try
            Dim db As New DaspackDataContext
            Dim Crear_Envio As New Insertar_Envios

            If templateName <> "" Then
                templateID = db.InsTemplate(templateName)

                pos = positions.Split("|")

                For Each matches In pos
                    match = matches.Split(":")
                    mandatoryField = CInt(match(1))
                    columnFile = CInt(match(0))
                    db.InsTemplateColumnas(templateID, columnFile, mandatoryField)
                Next
            End If

            If (beneficiaries.Length > 0) Then
                Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
                If addBookName = "" Then
                    addBookName = "LIBRO_DEFAULT_" + usuarioId.ToString()
                End If

                idAddBook = db.InsAddBook(addBookName, usuarioId)

                For Each beneficiary As addressBook In beneficiaries
                    Mensaje = Crear_Envio.valida_datos_libro_direcciones(beneficiary)

                    If beneficiary.Destinatario Is Nothing Then
                        beneficiary.Destinatario = "Desconocido"
                    End If

                    If beneficiary.Telefono Is Nothing Then
                        beneficiary.Telefono = "000000000"
                    End If

                    If beneficiary.codigo_postal Is Nothing Then
                        beneficiary.codigo_postal = "45000"
                    End If

                    If beneficiary.Servicio Is Nothing Then
                        beneficiary.Servicio = "DOM"
                    End If

                    If Mensaje <> "OK" Then
                        response.responseMessage = "Ocurrió un error, por favor revise los datos ---> " + beneficiary.Destinatario + " - " + Mensaje
                        response.responseSuccess = 0
                        Return response
                    End If

                Next

                For Each beneficiary As addressBook In beneficiaries
                    Dim destinatario As New ObjDestinatario
                    beneficiary.Estado = Regex.Replace(beneficiary.Estado, "[^\w\.@-]", "").Trim()

                    destinatario.id_pais = pais
                    destinatario.nombre = beneficiary.Destinatario
                    destinatario.direccion = beneficiary.Direccion
                    destinatario.ciudad = beneficiary.Ciudad
                    destinatario.codigo_postal = beneficiary.codigo_postal
                    destinatario.estadoprovincia = beneficiary.Estado
                    destinatario.telefono = beneficiary.Telefono
                    destinatario.apellidos = ""
                    destinatario.calle = ""
                    destinatario.codigo_pais = ""
                    destinatario.colonia = ""
                    destinatario.direccion2 = ""
                    destinatario.email = ""
                    destinatario.empresa = ""
                    destinatario.municipio = ""
                    destinatario.noexterior = 0
                    destinatario.nointerior = ""

                    id_destinatario = Crear_Envio.crea_destinatario(destinatario)

                    idAddBookArchivo = db.InsAddBookDatosArchivo(beneficiary.Contenedor, beneficiary.Inventario, beneficiary.Destinatario, beneficiary.Ciudad, beneficiary.Estado, beneficiary.Direccion, beneficiary.codigo_postal, beneficiary.Telefono, beneficiary.Servicio, beneficiary.Cobranza, beneficiary.Transporte, beneficiary.Guia, beneficiary.Observaciones)

                    db.InsAddBookClientes(idAddBook, id_destinatario, idAddBookArchivo)
                Next
            End If

            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseSuccess = 0
            response.responseMessage = "Ocurrió un error al salvar template y libro de direcciones -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try

    End Function

    <WebMethod()>
    Public Shared Function getAddBookClientes(ByVal idLibro As Integer) As genericResponse
        Dim response As New genericResponse
        Try
            Dim db As New DaspackDataContext

            Dim result As IEnumerable(Of addBookClientes) = db.GetAddBookClientes(idLibro)

            response.responseMessage = ""
            response.responseSuccess = 1
            response.responseArray = New ArrayList(result.ToArray)

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar clientes de libro -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Private Shared Sub deleteFile(ByVal fullPath As String)
        If System.IO.File.Exists(ConfigurationManager.AppSettings("fullPath") + fullPath) Then
            System.IO.File.Delete(ConfigurationManager.AppSettings("fullPath") + fullPath)
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function getAddBook() As genericResponse
        Dim response As New genericResponse
        Try
            Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
            Dim db As New DaspackDataContext
            Dim result As IEnumerable(Of addBook) = db.getAddBook(usuarioId)

            response.responseArray = New ArrayList(result.ToArray)
            response.responseMessage = ""
            response.responseSuccess = 1

            Return response
        Catch ex As Exception
            response.responseMessage = "Ocurrió un error al cargar libros de direcciones -->" + ex.Message.ToString
            Return response
        Finally
            response = Nothing
        End Try
    End Function

    Protected Sub selAddBook_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selAddBook.SelectedIndexChanged
        If selAddBook.SelectedValue > 0 Then
            Inserta.Visible = False
            btnSave.Visible = True
        Else
            Inserta.Visible = True
            btnSave.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If selAddBook.SelectedValue > 0 Then
            Inserta.Visible = False
            btnSave.Visible = True
        Else
            Inserta.Visible = True
            btnSave.Visible = False
        End If
    End Sub

    Protected Sub btnAddBook_Click(sender As Object, e As EventArgs) Handles btnAddBook.Click
        If selAddBook.SelectedValue > 0 Then
            Inserta.Visible = False
            btnSave.Visible = True
        Else
            Inserta.Visible = True
            btnSave.Visible = False
        End If
    End Sub

    Protected Sub btnEnvioPrechequeado_Click(sender As Object, e As EventArgs) Handles btnEnvioPrechequeado.Click
        Dim id_usuario As Integer = Session("id_usuario")
        Dim mensaje As String = ""
        Try
            Dim idEnvio As Integer
            If Integer.TryParse(txtIdEnvioPrechequeado.Text, idEnvio) Then
                Dim envio As Envio = DaspackDALC.GetDatosEnvio(idEnvio)
                If envio IsNot Nothing Then
                    Dim tarifaAgencia As TarifaAgencia = DaspackDALC.GetTarifaAgencia(envio.id_tarifa_agencia)
                    If tarifaAgencia IsNot Nothing Then
                        DropDownAgentes.SelectedValue = tarifaAgencia.id_agencia
                        DropDownProduct.DataBind()
                        DropDownProduct.SelectedValue = tarifaAgencia.id_tarifa_agencia
                        DropDownAgentes_SelectedIndexChanged(DropDownAgentes, EventArgs.Empty)
                    End If

                    With envio
                        TxtTarifa.Text = .precio
                        TxtSeguro.Text = .valor_seguro
                        TxtPromo.Text = .id_codigo_promocion
                        TxtAduana.Text = .valor_aduana
                        TxtInstEntrega.Text = .instrucciones_entrega

                        txtLargo.Text = .largo
                        txtAncho.Text = .ancho
                        txtAlto.Text = .alto
                        txtPeso.Text = .peso
                        TxtRef.Text = .Referencia1
                        DropDownContenidos.SelectedValue = .id_contenido
                        TxtCajas.Text = .contenedores
                    End With

                    Dim cliente As Cliente = DaspackDALC.GetDatosCliente(envio.id_envio)
                    If cliente IsNot Nothing Then
                        With cliente
                            DropDownPais.SelectedValue = .id_pais
                            If DropDownPais.SelectedValue = 52 Then
                                txtCiudad.Visible = False
                                DropDownCiudades.Visible = True
                            Else
                                txtCiudad.Visible = True
                                DropDownCiudades.Visible = False
                            End If

                            txtEdo.DataBind()
                            txtNombre.Text = .nombre
                            TxtApellidos.Text = .apellidos
                            txtEmpresa.Text = .empresa
                            txtCalle.Text = .calle
                            TxtCol.Text = .colonia
                            txtEdo.SelectedValue = .estadoprovincia

                            If DropDownPais.SelectedValue = 52 Then
                                DropDownCiudades.DataBind()
                                DropDownCiudades.SelectedIndex = DropDownCiudades.Items.IndexOf(DropDownCiudades.Items.FindByText(.ciudad.ToUpper().Trim()))
                            Else
                                txtCiudad.Text = .ciudad
                            End If

                            TxtMpio.Text = .municipio

                            txtTelefono.Text = .telefono
                            TxtCP.Text = .codigo_postal
                            txtEmail.Text = .email
                        End With
                    End If

                    Dim destinatario As Destinatario = DaspackDALC.GetDatosDestinatario(envio.id_envio)
                    If destinatario IsNot Nothing Then
                        With destinatario
                            DropDownPais2.SelectedValue = .id_pais
                            If DropDownPais2.SelectedValue = 52 Then
                                txtCiudad2.Visible = False
                                DropDownCiudades2.Visible = True
                            Else
                                txtCiudad2.Visible = True
                                DropDownCiudades2.Visible = False
                            End If
                            txtEdo2.DataBind()
                            TxtNombre2.Text = .nombre
                            txtApellidos2.Text = .apellidos
                            txtEmpresa2.Text = .empresa
                            txtCalle2.Text = .calle
                            TxtCol2.Text = .colonia
                            txtEdo2.SelectedValue = .estadoprovincia
                            DropDownCiudades2.DataBind()

                            If DropDownPais2.SelectedValue = 52 Then
                                DropDownCiudades2.DataBind()
                                DropDownCiudades2.SelectedIndex = DropDownCiudades2.Items.IndexOf(DropDownCiudades2.Items.FindByText(.ciudad.ToUpper().Trim()))
                            Else
                                txtCiudad2.Text = .ciudad
                            End If

                            TxtMpio2.Text = .municipio

                            txtTelefono2.Text = .telefono
                            txtEmail2.Text = .email
                            TxtCP2.Text = .codigo_postal
                        End With
                    End If
                Else
                    mensaje = "El usuario no tiene envios creados."
                End If
            Else
                mensaje = "El envio debe ser numerico."
                Label2.Text = "Ocurrió un error --> " + mensaje
                ModalPopupExtender3.Show()
            End If

        Catch ex As Exception
            Label2.Text = "Ocurrió un error, por favor revise los datos -->" + mensaje
            Dim errorLog = New ErrorLog
            errorLog.Log(HttpContext.Current.Request.Url.AbsoluteUri + " - " + Me.GetType().Name + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, ex.Source, id_usuario.ToString())
        End Try

    End Sub
End Class


