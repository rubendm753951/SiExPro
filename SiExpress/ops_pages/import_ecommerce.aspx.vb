
Imports System.Data

Partial Class ops_pages_import_ecommerce
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GridView1.DataBind()
    End Sub

    Protected Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        Dim estafetaWrapper As New EstafetaWrapper()
        Dim Mensaje As String
        Dim id_cliente As Integer
        Dim Crear_Envio As New Insertar_Envios
        Dim add = (New With {.Message = String.Empty, .id_dest = Integer.MinValue, .benAddError = Integer.Parse("0")})
        Dim BenefMessages As New ArrayList
        Dim shipments As New ArrayList
        Dim id_envio As Integer
        Dim db As New DaspackDataContext

        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim checkRow As CheckBox = row.FindControl("chkOrden")
                If checkRow IsNot Nothing AndAlso checkRow.Checked Then
                    Dim dv As DataView = Me.EnviosAExportar.Select(DataSourceSelectArguments.Empty)
                    Dim dsRow = dv(row.RowIndex)

                    Try
                        Dim usuarioId As Integer = Integer.Parse(CType(HttpContext.Current.Session("id_usuario"), String))
                        Dim dimensionPeso As String = CType(HttpContext.Current.Session("dimension_peso"), String)
                        Dim appRelativeVirtualPath As String = CType(HttpContext.Current.Session("AppRelativeVirtualPath"), String)

                        Dim remitente = DaspackDALC.GetSender(1)
                        id_cliente = remitente.id_cliente
                        Dim id_agente = 55

                        Dim tarifasAgencia As IEnumerable(Of TarifasAgencia) = db.GetTarifasAgencia(id_agente)
                        Dim tarifas As List(Of TarifasAgencia) = New List(Of TarifasAgencia)(tarifasAgencia.ToList())

                        Dim id_destinatario As Integer
                        Dim Datos_Dest As New ObjDestinatario
                        Datos_Dest.id_pais = 52
                        Datos_Dest.nombre = dsRow(3)
                        Datos_Dest.apellidos = dsRow(4)
                        Datos_Dest.empresa = dsRow(7)
                        Datos_Dest.calle = dsRow(8)
                        Datos_Dest.noexterior = 0 ' Estamos pasando la dirección completa en el campo de calle.
                        Datos_Dest.nointerior = Nothing
                        Datos_Dest.direccion2 = Nothing
                        Datos_Dest.colonia = dsRow(9)
                        Datos_Dest.ciudad = dsRow(16)
                        Datos_Dest.municipio = ""
                        Datos_Dest.estadoprovincia = dsRow(6)
                        Datos_Dest.telefono = dsRow(11)
                        Datos_Dest.email = dsRow(12)
                        Datos_Dest.codigo_postal = dsRow(10)


                        Mensaje = Crear_Envio.valida_datos_dest(Datos_Dest)
                        If Mensaje = "OK" Then
                            id_destinatario = Crear_Envio.crea_destinatario(Datos_Dest)
                            Session("id_destinatario") = id_destinatario
                        Else
                            Exit Sub
                        End If


                        Try
                            add.id_dest = id_destinatario
                            Dim requestEnvio As ObjEnvio = New ObjEnvio()

                            requestEnvio.id_cliente = id_cliente
                            requestEnvio.id_destinatario = id_destinatario
                            requestEnvio.dimension_peso = dimensionPeso
                            requestEnvio.fecha = DateTime.Now.ToString
                            requestEnvio.contenedores = ""
                            requestEnvio.FedExRef = "0"

                            requestEnvio.id_tarifa_agencia = tarifas.FirstOrDefault().id_tarifa_agencia
                            requestEnvio.valor_seguro = 0
                            requestEnvio.id_usuario = usuarioId
                            requestEnvio.id_ruta = Nothing

                            requestEnvio.id_agente = id_agente
                            requestEnvio.precio = dsRow(14)
                            requestEnvio.valor_seguro = 0

                            requestEnvio.id_codigo_promocion = Nothing
                            requestEnvio.valor_aduana = Nothing
                            requestEnvio.total_envio = requestEnvio.precio + requestEnvio.valor_seguro
                            requestEnvio.instrucciones_entrega = ""
                            requestEnvio.observaciones = Nothing   'future reference 
                            requestEnvio.id_usuario = Session("id_usuario")

                            requestEnvio.id_ruta = 0

                            requestEnvio.largo = 5
                            requestEnvio.ancho = 5
                            requestEnvio.alto = 5
                            requestEnvio.peso = 5
                            requestEnvio.referencia = dsRow(1) 'Order ID
                            requestEnvio.contenido = ""
                            requestEnvio.dimension_peso = Session("dimension_peso")
                            requestEnvio.contenedores = 1

                            'PreRegistor del Envío
                            Mensaje = Crear_Envio.valida_preregistro(requestEnvio)
                            If Mensaje = "OK" Or Mensaje = "El envío ya está entregado" Then
                                id_envio = Crear_Envio.PreRegistro_Envios(requestEnvio.id_agente, requestEnvio, 0)
                            Else
                                Throw New Exception("Ocurrió un error, por favor revise los datos ---> " + Mensaje)
                            End If

                            'Registro de Envíos (Detalles)
                            Crear_Envio.Detalle_Envios(id_envio, requestEnvio, 0, "")
                            'Insertar SobreCargos
                            Crear_Envio.inserta_SobreCargos(id_envio)
                            'Inserta seguimiento
                            Dim ins_seguimiento As New seguimiento_envios
                            ins_seguimiento.insertar_seguimiento(id_envio, Me.AppRelativeVirtualPath, "", usuarioId)

                            ECommerceDALC.UpdateOrderStatus(dsRow(1), ECommerceOrderStatusEnum.Processing)

                            shipments.Add(id_envio)
                            row.Cells(15).Text = "Orden Importada"
                            checkRow.Checked = False
                            checkRow.Visible = False
                        Catch ex As Exception
                            'TODO Mostrar mensaje que se importo el envio
                            dsRow(15) = "Ocurrio un error en la importacion"
                        End Try

                    Catch ex As Exception
                        'TODO mostrar mensaje de que fallo
                    Finally

                    End Try
                End If
            End If
        Next
    End Sub

End Class
