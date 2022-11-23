Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System
Imports seguridad
Partial Class admin_agentes
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TxtBoxAlta.Text = Date.Today
        TxtBoxTermino.Text = Date.Today.AddYears(20)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nuevo_agente As Integer
        Dim agente As New ObjAgente
        Dim crea_agente As New admin_catalogos

        agente.id_pais = DropDownPais.SelectedValue
        agente.id_corporativo = DropDownCorp.SelectedValue
        agente.nombre = TxtBoxNombre.Text
        agente.direccion = TxtBoxDir.Text
        agente.provincia = TxtBoxEdoProv.Text
        agente.ciudad = TxtBoxCD.Text
        agente.fecha_alta = TxtBoxAlta.Text
        agente.fecha_termino = TxtBoxTermino.Text
        agente.id_moneda = DropDownMoneda.SelectedValue
        agente.limite_de_credito = TxtBoxLimCred.Text
        agente.NIT = TxtBoxNIT.Text
        agente.telefono = TxtBoxTel.Text
        agente.requiere_asignacion = Chk_requiere_asignacion.Checked
        agente.Factor = txtFactor.Text
        agente.CostoAdicional = IIf(txtCostoAdicional.Text = "", 0, txtCostoAdicional.Text)
        agente.EsquemaPorFactor = chkFactorAgent.Checked

        'Llamar método para insertar el agente
        nuevo_agente = crea_agente.insertar_agente(agente)

        'Inserta tarifas, tarifas sobrepeso y comisiones para cada agente y por cada tipo de tarifa
        'Dim i As Integer
        'For i = 0 To CheckBoxList1.Items.Count - 1
        '    If CheckBoxList1.Items(i).Selected Then

        '        'Inserta tarifas
        '        agente.id_tarifa_tipo = CheckBoxList1.Items(i).Value
        '        agente.id_agente = nuevo_agente
        '        agente.factor_tarifa = TextBox1.Text
        '        crea_agente.insetar_tarifas(agente)

        '        'Inserta comisiones
        '        agente.comision_moneda = TextBox2.Text
        '        agente.comision_porcent = TextBox3.Text
        '        crea_agente.insertar_comisiones(agente)

        '        'Inserta tarifas sobrepeso
        '        '----Las tarifas por sobrepeso serán tomadas c_tarifas_sobrepeso
        '        '----por omisón para evitar genear registros inecesarios.
        '        '--- Esta tabla se llenará solo en caso de requerir tarifas por sobrepeso especiales para un agente.

        '        'agente.id_moneda = DropDownMoneda.SelectedValue
        '        'crea_agente.insertar_tarifas_sobrepeso(agente)

        '    End If
        'Next
        GridView1.DataBind()

        'Limpiar campos
        TxtBoxNombre.Text = "" : TxtBoxDir.Text = "" : TxtBoxEdoProv.Text = ""
        TxtBoxDir.Text = "" : TxtBoxTel.Text = "" : TxtBoxCD.Text = "" : TxtBoxNIT.Text = ""

    End Sub
    Protected Sub sqlDataSource1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles Agencias.Updating
        'For x As Integer = 0 To e.Command.Parameters.Count - 1
        '    Trace.Write(e.Command.Parameters(x).ParameterName)
        '    Trace.Write(e.Command.Parameters(x).Value)
        'Next

    End Sub
End Class

