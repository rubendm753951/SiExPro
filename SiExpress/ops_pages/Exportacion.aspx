<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Exportacion.aspx.vb" Inherits="ops_pages_Exportacion" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

<ContentTemplate>
    <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate><img alt="" src="../Images/bigrotation2.gif" /></progresstemplate>
    </asp:UpdateProgress>

<%--***************   Testing ********************  --%>
    <asp:ConfirmButtonExtender ID="btnConfirmExport" runat="server" 
        TargetControlID="Button1" 
        ConfirmText="El envío se exportará hacia el sistema del proveedor seleccionado, ¿está seguro?">
    </asp:ConfirmButtonExtender>
        <asp:ConfirmButtonExtender ID="btnConfirmCancek" runat="server" 
        TargetControlID="Button5" 
        ConfirmText="El envío será borrado del del sistema de FedEx, ¿está seguro?">
    </asp:ConfirmButtonExtender>
<%--***************  Testing   *******************  --%>


    <br />
    <strong><span style="font-size: 16pt">
        <br />
        </span></strong>
    <div style="left: 262px; width: 582px; position: absolute; top: 168px; height: 271px">
        <strong><span style="font-size: 16pt"></span></strong>
        <table border="1" style="width: 584px; height: 50px">
            <tr>
                <td colspan="3" style="height: 18px">
                    <strong><span style="font-size: 16pt">
        Módulo de Exportaciones</span></strong></td></tr><tr>
                <td style="width: 474px; height: 26px;">
                    <strong><span style="color: navy;">Seleccione Proveedor</span></strong></td>
                <td style="width: 313px; height: 26px;" colspan="2">
    <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="Proveedores" DataTextField="proveedor" 
                        DataValueField="id_proveedor">
        <asp:ListItem Value="FedEx">FedEx</asp:ListItem><asp:ListItem>USPS</asp:ListItem><asp:ListItem Enabled="False">UPS</asp:ListItem><asp:ListItem Enabled="False">DHL</asp:ListItem></asp:DropDownList>
                </td></tr><tr>
                <td style="width: 474px; height: 27px;">
                    <strong>Tipo Envio</strong></td><td style="width: 312px; height: 27px;">
                    <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownShipType" runat="server" Width="273px" 
                        DataSourceID="Prod_Proveedores" DataTextField="producto" 
                        DataValueField="id_value" >
                        <asp:ListItem Selected="True" Value="11">International Economy</asp:ListItem><asp:ListItem Value="13">International First</asp:ListItem><asp:ListItem Value="15">International Priority</asp:ListItem></asp:DropDownList></td>
                <td style="width: 313px; height: 27px;">
                    <asp:CheckBox ID="cbSignature" runat="server" Text="Signature Confirmation" />
                </td>
            </tr><tr>
                <td style="width: 474px">
                    <strong>Tipo de Empaque</strong></td><td style="width: 313px" colspan="2">
                    <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownPkg" runat="server">
                        <asp:ListItem Value="0">FedEx_10_KG_Box</asp:ListItem><asp:ListItem Value="1">FedEx_25_KG_Box</asp:ListItem><asp:ListItem Value="2">FedEx_Box</asp:ListItem><asp:ListItem Value="3">FedEx_Envelop</asp:ListItem><asp:ListItem Value="4">FedEx_Pack</asp:ListItem><asp:ListItem Value="5">FedEx_Tube</asp:ListItem><asp:ListItem Value="6">Your_Packing</asp:ListItem></asp:DropDownList></td></tr><tr>
                <td style="width: 474px">
                    <span><strong>Fecha Recolección</strong></span></td>
                <td style="width: 313px" colspan="2">
                    <asp:TextBox CssClass="form-control" Height="27px"   ID="txtFecha" runat="server"  Width="154px"></asp:TextBox></td></tr>
            <tr>
                <td style="width: 474px">
                    <b>Tipo de Impresión</b></td>
                <td style="width: 313px" colspan="2"> 
                    <asp:CheckBox ID="cbLasser" runat="server" Text="Lasser" Checked="true"  />
                    <asp:CheckBox ID="cbTermica" runat="server" Text="Térmica"/>
                </td>
            </tr>
            <tr>
                <td style="width: 474px">
                    <strong><span>Exportar el Envío &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</span></strong>&nbsp;<asp:Button CssClass="btn btn-outline btn-success btn-sm"  
                        ID="Button4" runat="server" Text="Consultar" Width="66px" />
                    <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button3" runat="server" Text="Obtener Tarifa" Width="101px" />
                </td><td style="width: 313px" colspan="2">
                    <asp:DropDownList CssClass="form-control" Height="30px"  ID="TextBox2" runat="server" DataSourceID="ExportData" 
                        DataTextField="id_envio" DataValueField="id_envio" Width="121px">
                    </asp:DropDownList>
                    <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Font-Bold="True" Text="Exportar" />
                    <br />
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 474px; height: 26px;">
                    <b>Cancelar el Envío</b></td><td style="height: 24px; width: 313px" 
                    colspan="2">
                    <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBox4" runat="server" Width="114px"></asp:TextBox>&nbsp;<asp:Button CssClass="btn btn-outline btn-success btn-sm"  
                        ID="Button5" runat="server" Height="24px" Text="Cancelar" Font-Bold="True" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:MutuallyExclusiveCheckBoxExtender ID="mecLasser" runat="server" TargetControlID="cbLasser" Key="printer" />
    <asp:MutuallyExclusiveCheckBoxExtender ID="mecTermica" runat="server" TargetControlID="cbTermica" Key="printer" />
    
    <br />
    <span style="color: blue; text-decoration: underline"> </span>
    <br />
    <br />
    <br />
    <br />
    <div style="left: 259px; width: 593px; position: absolute; top: 472px; height: 182px">
    <asp:TextBox CssClass="form-control" Height="181px"   ID="TextBox3" runat="server" TextMode="MultiLine" 
            Width="596px"></asp:TextBox>
        <asp:SqlDataSource ID="Proveedores" runat="server" 
            ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
            SelectCommand="sp_select_proveedores" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="Prod_Proveedores" runat="server" 
            ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
            SelectCommand="sp_select_productos_proveedores" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" DefaultValue="" 
                    Name="id_proveedor" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div><br />
    <br />
    <br />
    <br />
    &nbsp;&nbsp;<br />
    <br />
    <br />
    
    <asp:Panel ID="Panel1" runat="server" Visible="false" Width="367px">
    <div style="left: 639px; width: 385px; position: absolute; top: 671px; height: 264px" visible="true" >
        <table border="1" style="width: 374px" visible="True">
            <tr>
                <td colspan="2"><strong>Validar Direcciones con las&nbsp; WebTools del USPS</strong></td></tr><tr>
                <td style="width: 98px">Address1</td><td style="width: 59px"><asp:TextBox CssClass="form-control" Height="27px"   ID="address1" runat="server"></asp:TextBox></td></tr><tr><td style="width: 98px; height: 12px">Address 2</td><td style="width: 59px; height: 12px"><asp:TextBox CssClass="form-control" Height="27px"   ID="address2" runat="server">6406 Ivy Lane</asp:TextBox></td></tr><tr>
                <td style="width: 98px; height: 5px">City</td><td style="width: 59px; height: 5px"><asp:TextBox CssClass="form-control" Height="27px"   ID="city" runat="server">Greenbelt</asp:TextBox>&nbsp;</td></tr><tr>
                <td style="width: 98px; height: 4px">State</td><td style="width: 59px; height: 4px"><asp:TextBox CssClass="form-control" Height="27px"   ID="state" runat="server">MD</asp:TextBox>&nbsp;</td></tr><tr>
                <td style="width: 98px; height: 11px">Zip 5</td><td style="width: 59px; height: 11px"><asp:TextBox CssClass="form-control" Height="27px"   ID="zip5" runat="server"></asp:TextBox>&nbsp;</td></tr><tr>
                <td style="width: 98px; height: 16px">Zip 4</td><td style="width: 59px; height: 16px"><asp:TextBox CssClass="form-control" Height="27px"   ID="zip4" runat="server"></asp:TextBox>&nbsp;</td></tr><tr>
                <td style="width: 98px; height: 14px"></td>
                <td style="width: 59px; height: 14px"><asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button2" runat="server" Text="Address Validation" /><asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button6" runat="server" Text="GetLabel"/>
                    <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button7" runat="server" Text="tracking" />
                </td>
            </tr>
            <tr>
                <td style="width: 98px; height: 81px">Respuesta</td>
                <td style="width: 59px; height: 81px"><asp:TextBox CssClass="form-control" Height="74px"   ID="respuesta" runat="server" 
                        Width="158px" ></asp:TextBox>
                    <asp:TextBox CssClass="form-control" Height="74px"   ID="respuesta0" runat="server" Width="158px"></asp:TextBox>
                    <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBox5" runat="server">SUkqAAgAAAASAP4ABAABAAAAAAAAAAABBAABAAAApAYAAAEBBAABAAAAmAgAAAIB</asp:TextBox>
                </td>
            </tr>
         </table>
    </div>
    </asp:Panel>
    <asp:SqlDataSource ID="ExportData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_Select_envios_exportar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <%--<asp:Parameter DefaultValue="" Name="id_agencia" Type="Int32" /><asp:Parameter Name="id_ruta" Type="Int32" />--%>
              <asp:ControlParameter ControlID="DropDownList1" DefaultValue="" 
                   Name="id_proveedor" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
</ContentTemplate> 
</asp:UpdatePanel>    
</asp:Content>

