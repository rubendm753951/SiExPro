<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_vehiculos.aspx.vb" Inherits="admin_pages_admin_vehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:updateprogress id="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <span style="font-size: 10pt">&nbsp;Actualizando.. Por favor espere </span>
        </ProgressTemplate>
    </asp:updateprogress>
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
            <table style="WIDTH: 600px" >
                <tbody>
                    <tr>
                        <td style="WIDTH: 90px; HEIGHT: 25px"><span style="FONT-SIZE: 10pt">Oficina</span></td>
                        <td style="WIDTH: 150px; HEIGHT: 25px">
                            <asp:DropDownList CssClass="form-control" Height="30px"  ID="dropDownOficina" runat="server" Width="374px" Font-Size="Small" AutoPostBack="True" DataValueField="id_oficina" DataTextField="nombre" DataSourceID="Oficinas"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 90px; HEIGHT: 25px"><span style="FONT-SIZE: 10pt">Placas</span>&nbsp;</td>
                        <td style="WIDTH: 150px; HEIGHT: 25px">
                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtPlacas" runat="server" Width="368px" Font-Size="Small"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 90px; HEIGHT: 20px">Serie</td>
                        <td style="WIDTH: 150px; HEIGHT: 20px">
                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtSerie" runat="server" Width="368px" Font-Size="Small"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 90px; HEIGHT: 21px"><span style="FONT-SIZE: 10pt">Modelo</span></td>
                        <td style="WIDTH: 150px; HEIGHT: 21px">
                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtModelo" runat="server" Width="369px" Font-Size="Small"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 90px; HEIGHT: 21px"><span style="FONT-SIZE: 10pt">Fecha Compra</span></td>
                        <td style="WIDTH: 150px; HEIGHT: 21px">
                            <asp:Calendar ID="Calendar1" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtFechaCompra" runat="server" ReadOnly="True" Width="100px" Font-Size="Small"></asp:TextBox>
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Seleccionar Fecha...</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 39px;">
                            <asp:TextBox CssClass="form-control" Height="51px"   ID="txtMsg" runat="server" TextMode="MultiLine" Visible="False" Width="579px"></asp:TextBox>
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button2" runat="server" Text="Ok" Visible="False" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="btnInsert" runat="server" Text="Insertar Datos"></asp:Button><br />
            <br />
            <table>
                <tbody>
                    <tr>
                        <td style="WIDTH: 150px; HEIGHT: 21px"><span style="FONT-SIZE: 10pt">Busqueda por Placa</span></td>
                        <td style="WIDTH: 250px; HEIGHT: 21px">
                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtSearchPlaca" runat="server" Width="100px" Font-Size="Small" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="btnSearch" runat="server" Text="Buscar" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                <asp:GridView class="table table-striped table-bordered table-hover"  ID="GridView1" runat="server" Font-Size="10pt" DataSourceID="Vehiculos" AllowPaging="True" DataKeyNames="id_vehiculo" AutoGenerateColumns="False">
                    <Columns>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:BoundField DataField="id_vehiculo" HeaderText="Id Vehiculo" ReadOnly="True" InsertVisible="False" SortExpression="id_vehiculo">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Oficina" SortExpression="id_oficina">
                            <EditItemTemplate>
                                <asp:DropDownList CssClass="form-control" Height="30px"  ID="dropDownOficinaGrid" runat="server" DataSourceID="Oficinas" DataTextField="nombre"
                                    DataValueField="id_oficina" SelectedValue='<%# Bind("id_oficina")%>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("oficina")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="placas" HeaderText="Placas" SortExpression="placas">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="serie" HeaderText="Serie" SortExpression="seria">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_compra" HeaderText="Fecha Compra" DataFormatString="{0:dd-MM-yyyy}" SortExpression="fecha_compra">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                    </Columns> <PagerStyle CssClass="pagination-ys"></PagerStyle>

                    <HeaderStyle BackColor="Silver" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                </asp:GridView>
            </asp:Panel>
                            </div>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource id="Oficinas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_oficinas" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" >
               
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="Vehiculos" runat="server"
                SelectCommandType="StoredProcedure" SelectCommand="sp_select_vehiculos" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                UpdateCommandType="StoredProcedure" UpdateCommand="sp_update_vehiculo" FilterExpression="placas LIKE '{0}%'">
                <UpdateParameters>
                    <asp:Parameter Name="id_vehiculo" Type="Int32" />
                    <asp:Parameter Name="id_oficina" Type="Int32" />
                    <asp:Parameter Name="placas" Type="String" />
                    <asp:Parameter Name="serie" Type="String" />
                    <asp:Parameter Name="modelo" Type="String" />
                </UpdateParameters>
                 <FilterParameters>
                    <asp:ControlParameter Name="placas" ControlID="txtSearchPlaca" PropertyName="Text" />
                </FilterParameters>    
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:updatepanel>
    </div>
</asp:Content>
