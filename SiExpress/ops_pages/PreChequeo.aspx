<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PreChequeo.aspx.vb" Inherits="ops_pages_PreChequeo" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 177px; left: 248px; height: 771px; width: 943px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="position: absolute; top: 95px; left: 31px; height: 175px;">
                    <asp:Panel ID="Panel4" runat="server" Height="173px" Width="981px">
                        <table style="width: 979px; text-align: center">
                            <tbody>
                                <tr>
                                    <td style="width: 164px; height: 21px"><span style="font-size: 9pt">Agente</span></td>
                                    <td style="font-size: 9pt; height: 21px; text-align: left" colspan="2">
                                        <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownAgentes" runat="server" Width="367px" Font-Bold="False" DataSourceID="SqlDSAgentes" OnSelectedIndexChanged="DropDownAgentes_SelectedIndexChanged" DataValueField="id_agencia" DataTextField="agente" AutoPostBack="True"></asp:DropDownList></td>
                                    <td style="width: 164px; height: 1px"><span style="font-size: 9pt">Contenido</span></td>
                                    <td style="width: 164px; height: 12px">
                                        <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownContenidos" runat="server" Width="149px" DataSourceID="Contenidos" DataTextField="descripcion" DataValueField="id_contenido"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="width: 164px; height: 21px"><span style="font-size: 9pt">SubProducto</span></td>
                                    <td style="font-size: 9pt; height: 21px; text-align: left" colspan="2">
                                        <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownProduct" runat="server" Width="367px" DataSourceID="Tarifas" DataValueField="id_tarifa_agencia" DataTextField="SubProducto" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                    <td style="width: 66px; height: 12px"><span style="font-size: 9pt">Tarifa:</span></td>
                                    <td style="width: 66px; height: 12px">
                                        <asp:TextBox CssClass="form-control" Height="27px" ID="TxtTarifa" runat="server" Width="82px" Enabled="False">0</asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td style="width: 164px; height: 21px"><span style="font-size: 9pt">Ruta</span></td>
                                    <td style="font-size: 9pt; height: 21px; text-align: left" colspan="2">
                                        <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownRuta" runat="server" Width="367px" DataSourceID="Rutas" DataValueField="id_ruta" DataTextField="descripcion" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                    <td style="width: 166px; height: 12px"><span style="font-size: 9pt">Valor COD:</span></td>
                                    <td colspan="1" style="width: 145px; height: 12px">
                                        <asp:TextBox CssClass="form-control" Height="27px" ID="TxtSeguro" runat="server" Width="82px">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 20px" valign="top">
                                        <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Inserta" OnClick="Inserta_Click" runat="server" Text="Guardar Envío" Width="118px"></asp:Button></td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>

                </div>
                <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                    TargetControlID="Button8" PopupControlID="Internal_error" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <div style="position: absolute; top: 5px; left: 31px; height: 175px;">
                    <asp:Panel ID="Internal_error" runat="server" Height="91px" Width="400px" Style="text-align: left">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="12pt" ForeColor="Black" Height="68px" Width="392px"
                            BackColor="White" BorderStyle="Double"></asp:Label>
                        <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Text="Aceptar" />
                    </asp:Panel>
                </div>

                <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button8" runat="server" Text="Button" Visible="True" />

                <asp:SqlDataSource ID="SqlDSAgentes" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_Select_Agentes_por_usuarios" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                    <SelectParameters>
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" DefaultValue="" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Contenidos" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" SelectCommand="sp_select_contenidos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="Rutas" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" SelectCommand="sp_select_rutas" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="Tarifas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectTarifas_por_Agente" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownAgentes" Name="id_agencia" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="id_tipo" Type="Int32" />
                        <asp:Parameter DefaultValue="True" Name="activado" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
