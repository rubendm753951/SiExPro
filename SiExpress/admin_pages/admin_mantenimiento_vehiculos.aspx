<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_mantenimiento_vehiculos.aspx.vb" Inherits="admin_pages_admin_mantenimiento_vehiculos" %>

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
                            <table style="width: 600px">
                                <tbody>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Vehiculo</span></td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownVehiculo" runat="server" Width="150px" Font-Size="Small" AutoPostBack="True" DataValueField="id_vehiculo" DataTextField="placas" DataSourceID="Vehiculos"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Fecha Mtto</span>&nbsp;</td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:Calendar ID="Calendar1" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtFechaMtto" ReadOnly="True" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Seleccionar Fecha...</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Tipo de Servicio</span></td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownTipoServicio" runat="server" Width="150px" Font-Size="Small" AutoPostBack="True" DataValueField="id_tipo_servicio" DataTextField="descripcion" DataSourceID="TipoServicio"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Kilometraje</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtKilometraje" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator1" ControlToValidate="txtKilometraje" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Costo Refacciones</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCostoRefacciones" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator4" ControlToValidate="txtCostoRefacciones" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Costo Mano de Obra</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCostoManoObra" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator2" ControlToValidate="txtCostoManoObra" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center; height: 39px;">
                                            <asp:TextBox CssClass="form-control" Height="51px" ID="txtMsg" runat="server" TextMode="MultiLine" Visible="False" Width="579px"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnInsert" runat="server" Text="Insertar Datos"></asp:Button><br />
                            <br />
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 150px; height: 21px"><span style="font-size: 10pt">Busqueda por Placa</span></td>
                                        <td style="width: 250px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtSearchPlaca" runat="server" Width="100px" Font-Size="Small" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnSearch" runat="server" Text="Buscar" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Font-Size="10pt" DataSourceID="MttoVehiculos" AllowPaging="True" DataKeyNames="id_mantenimiento" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="id_mantenimiento" HeaderText="Id Mantenimiento" ReadOnly="True" InsertVisible="False" SortExpression="id_mantenimiento">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Vehiculo" SortExpression="id_vehiculo">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownVehiculosGrid" runat="server" DataSourceID="Vehiculos" DataTextField="placas"
                                                    DataValueField="id_vehiculo" SelectedValue='<%# Bind("id_vehiculo")%>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("placas")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha Mtto" DataFormatString="{0:dd-MM-yyyy}" SortExpression="fecha_compra">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Tipo Servicio" SortExpression="id_tipo_servicio">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownTipoServicioGrid" runat="server" DataSourceID="TipoServicio" DataTextField="descripcion"
                                                    DataValueField="id_tipo_servicio" SelectedValue='<%# Bind("id_tipo_servicio")%>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("descripcion")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="kilometraje" HeaderText="Kilometraje" SortExpression="kilometraje">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="costorefacciones" HeaderText="Costo Refacciones" SortExpression="costorefacciones">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="costomanodeobra" HeaderText="Costo Mano de Obra" SortExpression="costomanodeobra">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>

                                    <HeaderStyle BackColor="Silver" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource ID="Oficinas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_oficinas" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="TipoServicio" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_tipo_servicio" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="Vehiculos" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_vehiculos" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="MttoVehiculos" runat="server"
                SelectCommandType="StoredProcedure" SelectCommand="sp_select_mantenimiento_vehiculos" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                UpdateCommandType="StoredProcedure" UpdateCommand="sp_update_mantenimiento_vehiculos" FilterExpression="placas LIKE '{0}%'">
                <UpdateParameters>
                    <asp:Parameter Name="id_mantenimiento" Type="Int32" />
                    <asp:Parameter Name="id_vehiculo" Type="Int32" />
                    <asp:Parameter Name="fecha" Type="DateTime" />
                    <asp:Parameter Name="id_tipo_servicio" Type="Int32" />
                    <asp:Parameter Name="kilometraje" Type="Int32" />
                    <asp:Parameter Name="costorefacciones" Type="Decimal" />
                    <asp:Parameter Name="costomanodeobra" Type="Decimal" />
                </UpdateParameters>
                <FilterParameters>
                    <asp:ControlParameter Name="placas" ControlID="txtSearchPlaca" PropertyName="Text" />
                </FilterParameters>
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
        </div>
</asp:Content>
