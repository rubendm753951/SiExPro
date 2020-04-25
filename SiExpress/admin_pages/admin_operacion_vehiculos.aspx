<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_operacion_vehiculos.aspx.vb" Inherits="admin_pages_admi_operacion_vehiculos" %>

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
                            <table style="width: 700px">
                                <tbody>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Oficina</span></td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownOficina" runat="server" Width="374px" Font-Size="Small" AutoPostBack="True" DataValueField="id_oficina" DataTextField="nombre" DataSourceID="Oficinas"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Promotor</span></td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownPromotor" runat="server" Width="374px" Font-Size="Small" AutoPostBack="True" DataValueField="id_promotor" DataTextField="nombre" DataSourceID="Promotores"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Vehiculo</span></td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="dropDownVehiculo" runat="server" Width="150px" Font-Size="Small" AutoPostBack="True" DataValueField="id_vehiculo" DataTextField="placas" DataSourceID="Vehiculos"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Km Inicial</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtKmIncial" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator2" ControlToValidate="txtKmIncial" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Km Final</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtKmFinal" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator3" ControlToValidate="txtKmFinal" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Costo Combustible</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCostoCombustible" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator4" ControlToValidate="txtCostoCombustible" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 21px"><span style="font-size: 10pt">Cantidad Lts</span></td>
                                        <td style="width: 150px; height: 21px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCantidadLts" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="errorVal" ID="RegularExpressionValidator5" ControlToValidate="txtCantidadLts" runat="server" ErrorMessage="Solo Numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 25px"><span style="font-size: 10pt">Fecha</span>&nbsp;</td>
                                        <td style="width: 150px; height: 25px">
                                            <asp:Calendar ID="Calendar1" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtFecha" ReadOnly="True" runat="server" Width="100px" Font-Size="Small"></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Seleccionar Fecha...</asp:LinkButton>
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
                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Font-Size="10pt" DataSourceID="OperacionVehiculos" AllowPaging="True" DataKeyNames="id_vehiculo" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="id_vehiculo" HeaderText="Id Vehiculo" ReadOnly="True" Visible="False" InsertVisible="False" SortExpression="id_vehiculo">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="placa" HeaderText="Placa" ReadOnly="True" SortExpression="placa">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_oficina" HeaderText="id_oficina" SortExpression="id_oficina">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="oficina" HeaderText="Oficina" ReadOnly="True" SortExpression="oficina">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_promotor" HeaderText="id_promotor" SortExpression="id_promotor">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="promotor" HeaderText="Promotor" ReadOnly="True" SortExpression="promotor">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="km_inicial" HeaderText="Km Inicial" SortExpression="km_inicial">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="km_final" HeaderText="Km Final" SortExpression="km_final">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="costo_combustible" HeaderText="Costo Combustible" SortExpression="costo_combustible">
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha" SortExpression="fecha">
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
            <asp:SqlDataSource ID="Promotores" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" SelectCommand="sp_select_promotores" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="dropDownOficina" DefaultValue="" Name="id_oficina" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="Vehiculos" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_vehiculos" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                <SelectParameters>
                    <asp:ControlParameter ControlID="dropDownOficina" DefaultValue="" Name="id_oficina" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="OperacionVehiculos" runat="server"
                SelectCommandType="StoredProcedure" SelectCommand="sp_select_operacion_vehiculos" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                UpdateCommandType="StoredProcedure" UpdateCommand="sp_update_operacion_vehiculo" FilterExpression="placas LIKE '{0}%'">
                <UpdateParameters>
                    <asp:Parameter Name="id_vehiculo" Type="Int32" />
                    <asp:Parameter Name="id_promotor" Type="Int32" />
                    <asp:Parameter Name="km_inicial" Type="Int32" />
                    <asp:Parameter Name="km_final" Type="Int32" />
                    <asp:Parameter Name="costo_combustible" Type="Decimal" />
                    <asp:Parameter Name="cantidad_lts" Type="Int32" />
                    <asp:Parameter Name="fecha" Type="DateTime" />
                    <asp:Parameter Name="id_oficina" Type="Int32" />
                </UpdateParameters>
                <FilterParameters>
                    <asp:ControlParameter Name="placas" ControlID="txtSearchPlaca" PropertyName="Text" />
                </FilterParameters>
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
