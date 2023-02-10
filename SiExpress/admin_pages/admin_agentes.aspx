<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_agentes.aspx.vb" Inherits="admin_agentes" Title="Admin Agents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <span style="font-size: 10pt">&nbsp;Actualizando.. Por favor espere </span>
        </ProgressTemplate>
    </asp:UpdateProgress>
<div style="position: absolute; top: 160px; left: 220px; width: 626px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
                            <table style="width: 814px">
                                <tbody>
                                    <tr>
                                        <td style="width: 3123px; height: 25px"><span style="font-size: 10pt">Corporativo</span></td>
                                        <td style="width: 338px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownCorp" runat="server" Width="374px" Font-Size="Small" AutoPostBack="True" DataValueField="id_corporativo" DataTextField="nombre" DataSourceID="Corporativos"></asp:DropDownList></td>
                                        <td style="width: 20766px; height: 25px"><span style="font-size: 10pt">País</span></td>
                                        <td style="width: 20766px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownPais" runat="server" Width="241px" Font-Size="Small" DataValueField="id_pais" DataTextField="nombre" DataSourceID="Paises"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3123px; height: 25px"><span style="font-size: 10pt">Nombre</span>&nbsp;</td>
                                        <td style="width: 338px; height: 25px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxNombre" runat="server" Width="368px" Font-Size="Small"></asp:TextBox></td>
                                        <td style="width: 20766px; height: 25px"><span style="font-size: 10pt">Estado</span>/<span style="font-size: 10pt">Provincia</span></td>
                                        <td style="width: 20766px; height: 25px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxEdoProv" runat="server" Width="234px" Font-Size="Small" Wrap="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3123px; height: 20px">Dirección </td>
                                        <td style="width: 338px; height: 20px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxDir" runat="server" Width="368px" Font-Size="Small"></asp:TextBox></td>
                                        <td style="width: 20766px; height: 20px">&nbsp;Teléfono</td>
                                        <td style="width: 20766px; height: 20px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxTel" runat="server" Width="234px" Font-Size="Small" Wrap="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3123px; height: 21px"><span style="font-size: 10pt">Ciudad</span></td>
                                        <td style="width: 338px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxCD" runat="server" Width="369px" Font-Size="Small"></asp:TextBox></td>
                                        <td style="width: 20766px; height: 21px"><span style="font-size: 10pt">NIT</span></td>
                                        <td style="width: 20766px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxNIT" runat="server" Width="235px" Font-Size="Small"></asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width: 813px">
                                <tbody>
                                    <tr>
                                        <td style="width: 37px; height: 21px" ><span style="font-size: 10pt">Alta</span></td>
                                        <td style="width: 90px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxAlta" runat="server" Width="93px" Enabled="False"></asp:TextBox></td>
                                        <td style="width: 38px; height: 21px">Baja</td>
                                        <td style="width: 66px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxTermino" runat="server" Width="86px"></asp:TextBox></td>
                                        <td style="width: 59px; height: 21px">Moneda</td>
                                        <td style="width: 127px; height: 21px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownMoneda" runat="server" Width="179px" DataValueField="id_moneda" DataTextField="nombre" DataSourceID="Monedas"></asp:DropDownList></td>
                                        <td style="width: 138px; height: 21px"><span style="font-size: 10pt">Límite de Crédito </span></td>
                                        <td style="width: 78px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxLimCred" runat="server" Width="133px">0.00</asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width: 814px">
                                <tbody>
                                    <tr>
                                        <td style="width: 457px; height: 38px" valign="top" >
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Width="225px" Text="Condiciones de Venta" Font-Size="11pt"></asp:Label><br />
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Font-Size="Small" DataValueField="id_tipo" DataTextField="descripcion" DataSourceID="SelectTiposVenta">
                                            </asp:CheckBoxList><br />
                                            <asp:CheckBox ID="Chk_requiere_asignacion" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Text="El agente requiere guias preasignadas" /></td>
                                        <td style="width: 114px; height: 38px" valign="top" >
                                            <asp:Label CssClass="form-control" ID="Label4" runat="server" Width="249px" Text="Factor para modiicar tarifas generales" Font-Size="Small" Height="27px" ></asp:Label>
                                            <asp:Label CssClass="form-control" ID="Label5" runat="server" Width="249px" Text="Comisiones por Envío (moneda)" Font-Size="Small" Height="27px" ></asp:Label>
                                            <asp:Label CssClass="form-control" ID="Label6" runat="server" Width="249px" Text="Comisiones (%)" Font-Size="Small" Height="27px" ></asp:Label></td>
                                        <td style="width: 23px; height: 38px" valign="top" >
                                            <asp:TextBox CssClass="form-control" Height="14px" ID="TextBox1" runat="server" Width="75px" Font-Size="Small">1.00</asp:TextBox><br />
                                            <asp:TextBox CssClass="form-control" Height="14px" ID="TextBox2" runat="server" Width="74px" Font-Size="Small">0.00</asp:TextBox><br />
                                            <asp:TextBox CssClass="form-control" Height="14px" ID="TextBox3" runat="server" Width="73px" Font-Size="Small">0.00</asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>

                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Insertar Datos"></asp:Button><br />
                            <br />

                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Font-Size="10pt" DataSourceID="Agencias" AllowPaging="True" DataKeyNames="id_agencia" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="id_agencia" HeaderText="ID Agente" ReadOnly="True" InsertVisible="False" SortExpression="id_agencia">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_corporativo" HeaderText="id_corporativo" SortExpression="id_corporativo" Visible="False">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Pa&#237;s" SortExpression="id_pais">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" DataSourceID="Paises" DataTextField="nombre"
                                                    DataValueField="id_pais" SelectedValue='<%# Bind("id_pais") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("pais") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="pais" HeaderText="pais" SortExpression="pais" Visible="False">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Direcci&#243;n" SortExpression="direccion">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado_provincia" HeaderText="Estado/Provincia" SortExpression="estado_provincia">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" SortExpression="ciudad">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="telefono" SortExpression="telefono">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_alta" DataFormatString="{0:d}" HeaderText="Alta" SortExpression="fecha_alta">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_termino" DataFormatString="{0:d}" HeaderText="T&#233;rmino" SortExpression="fecha_termino">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Moneda" SortExpression="id_moneda">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList2" runat="server" DataSourceID="Monedas" DataTextField="nombre"
                                                    DataValueField="id_moneda" SelectedValue='<%# Bind("id_moneda") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("moneda") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="moneda" HeaderText="moneda" SortExpression="moneda" Visible="False">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="limite_credito" HeaderText="L&#237;mite Cr&#233;dito" SortExpression="limite_credito">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:CheckBoxField DataField="requiere_asignacion" HeaderText="requiere_asignacion" SortExpression="requiere_asignacion" />

                                        <asp:TemplateField HeaderText="Recibo" SortExpression="id_recibo">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList3" runat="server" DataSourceID="Recibos" DataTextField="recibo"
                                                    DataValueField="id_recibo" SelectedValue='<%# Bind("id_recibo") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("recibo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="recibo" HeaderText="recibo"
                                            SortExpression="recibo" Visible="False"></asp:BoundField>


                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <asp:SqlDataSource ID="Agencias" runat="server"
                SelectCommandType="StoredProcedure" SelectCommand="sp_SelectAgencias" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                UpdateCommandType="StoredProcedure" UpdateCommand="sp_UpdateAgencias">
                <UpdateParameters>
                    <asp:Parameter Name="id_agencia" Type="Int32" />
                    <asp:Parameter Name="id_pais" Type="Int32" />
                    <asp:Parameter Name="nombre" Type="String" />
                    <asp:Parameter Name="direccion" Type="String" />
                    <asp:Parameter Name="estado_provincia" Type="String" />
                    <asp:Parameter Name="ciudad" Type="String" />
                    <asp:Parameter Name="fecha_alta" Type="DateTime" />
                    <asp:Parameter Name="fecha_termino" Type="DateTime" />
                    <asp:Parameter Name="id_moneda" Type="Int32" />
                    <asp:Parameter Name="limite_credito" Type="Decimal" />
                    <asp:Parameter Name="NIT" Type="String" />
                    <asp:Parameter Name="telefono" Type="String" />
                    <asp:Parameter Name="requiere_asignacion" Type="Boolean" />
                    <asp:Parameter Name="id_recibo" Type="Int32" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownCorp" Name="id_corporativo" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="Corporativos" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectCorp" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="Paises" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectPaises" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="Monedas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectMonedas" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SelectTiposVenta" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectTipos_Venta" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="Recibos" runat="server"
                ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                SelectCommand="sp_SelectRecibos" SelectCommandType="StoredProcedure">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>

