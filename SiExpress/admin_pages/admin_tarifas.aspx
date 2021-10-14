<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_tarifas.aspx.vb" Inherits="admin_tarifas" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 895px">
                                    <tr>
                                        <td style="height: 41px" colspan="3">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" />
                                                    <span style="font-size: 10pt">Actualizando, por favor espere...</span>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 174px; height: 20px">
                                            <asp:Label ID="Label1" runat="server" Width="182px" Text="País" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"></asp:Label></td>
                                        <td style="width: 88px; height: 20px">
                                            <asp:Label ID="Label3" runat="server"
                                                Width="284px" Text="Corporativo" Font-Bold="True" Font-Size="10pt"
                                                Height="16px"></asp:Label></td>
                                        <td style="width: 104px; height: 20px">
                                            <asp:Label ID="Label2" runat="server" Width="296px" Text="Agente" Font-Bold="True" Font-Size="10pt"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 174px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownPais" runat="server" Width="183px" AutoPostBack="True" DataValueField="id_pais" DataTextField="nombre" DataSourceID="paises" Font-Size="10pt">
                                            </asp:DropDownList></td>
                                        <td style="width: 88px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownCorp" runat="server" Width="284px" AutoPostBack="True" DataValueField="id_corporativo" DataTextField="nombre" DataSourceID="corps" Font-Size="10pt">
                                            </asp:DropDownList></td>
                                        <td style="width: 104px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownAgente" runat="server" Width="296px" AutoPostBack="True" DataValueField="id_agencia" DataTextField="nombre" DataSourceID="agentes" Font-Size="10pt">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="Label4" runat="server" Width="911px" Font-Bold="False" ForeColor="Blue" Font-Underline="False" Font-Size="10pt"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="3">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Width="403px"
                                                DataSourceID="Agente_Tarifas" HorizontalAlign="Center"
                                                AutoGenerateColumns="False" AllowPaging="True"
                                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small"
                                                DataKeyNames="id_tarifa_agencia,id_agencia,id_tarifa">
                                                <Columns>
                                                    <asp:CommandField ShowEditButton="True" />
                                                    <asp:BoundField DataField="id_tarifa_agencia" HeaderText="id_tarifa_agencia" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="id_tarifa_agencia" Visible="False" />
                                                    <asp:BoundField DataField="id_agencia" HeaderText="id_agencia" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="id_agencia" Visible="False" />
                                                    <asp:BoundField DataField="id_tarifa" HeaderText="id_tarifa" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="id_tarifa" Visible="False" />
                                                    <asp:BoundField DataField="Producto" HeaderText="Producto" SortExpression="Producto" ReadOnly="True" Visible="False">
                                                        <ItemStyle Width="500px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SubProducto" HeaderText="Producto" ReadOnly="True" SortExpression="SubProducto">
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" ReadOnly="True" />
                                                    <asp:BoundField DataField="Tarifa" HeaderText="Tarifa" SortExpression="Tarifa" DataFormatString="{0:c}" />
                                                    <asp:BoundField DataField="Tarifa_Tipo" HeaderText="Tarifa_Tipo" SortExpression="Tarifa_Tipo" ReadOnly="True" />
                                                    <asp:BoundField DataField="Zona" HeaderText="Zona" SortExpression="Zona" ReadOnly="True">
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="activado" HeaderText="activado" SortExpression="activado" />
                                                    <asp:BoundField DataField="comm_por_envio" HeaderText="comm_por_envio" SortExpression="comm_por_envio" DataFormatString="{0:F3}" />
                                                    <asp:BoundField DataField="comm_porcentaje" HeaderText="comm_porcentaje" SortExpression="comm_porcentaje" DataFormatString="{0:F3}" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="paises" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectPaises" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                    <SelectParameters>
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="corps" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectCorp" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownPais" Name="id_pais" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="agentes" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectAgencias" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownCorp" Name="id_corporativo" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Agente_Tarifas" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_SelectTarifas_por_Agente" SelectCommandType="StoredProcedure" UpdateCommand="sp_Update_Comisiones_tarifa_Agencia" UpdateCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownAgente" DefaultValue="" Name="id_agencia" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="id_tarifa" Type="Int32" />
                        <asp:Parameter Name="id_tarifa_agencia" Type="Int32" />
                        <asp:Parameter Name="activado" Type="Boolean" />
                        <asp:Parameter Name="tarifa" Type="Decimal" />
                        <asp:Parameter Name="comm_por_envio" Type="Decimal" />
                        <asp:Parameter Name="comm_porcentaje" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

