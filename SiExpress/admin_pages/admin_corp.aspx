<%@ Page Language="VB" MasterPageFile="~/SIte.master" AutoEventWireup="false" CodeFile="admin_corp.aspx.vb" Inherits="admin_corp" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script language="javascript" type="text/javascript">
<!--

    function TABLE1_onclick() {

    }

    // -->
    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            &nbsp;<img src="../Images/bigrotation2.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
                            <table style="width: 796px; height: 48px">
                                <tbody>
                                    <tr>
                                        <td style="width: 5804px; height: 1px" valign="top"><span style="font-size: 10pt">Corporatativo</span> ID&nbsp; </td>
                                        <td style="width: 308px; height: 1px" valign="top">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox1" runat="server" Width="121px" Enabled="False">Autogenerable</asp:TextBox></td>
                                        <td style="width: 755px; height: 1px" valign="top"><span style="font-size: 10pt">Nombre</span></td>
                                        <td style="width: 399px; height: 1px" valign="top">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxNombre" runat="server" Width="337px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5804px; height: 1px" valign="top"><span style="font-size: 10pt">NIT</span></td>
                                        <td style="width: 308px; height: 1px" valign="top">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxNIT" runat="server" Width="121px" Wrap="False"></asp:TextBox></td>
                                        <td style="width: 755px; height: 1px" valign="top"><span style="font-size: 10pt">Dirección </span></td>
                                        <td style="width: 399px; height: 1px" valign="top">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxDir" runat="server" Width="338px"></asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width: 796px; height: 83px" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
                                <tbody>
                                    <tr>
                                        <td style="width: 103px; height: 15px"><span style="font-size: 10pt">Ciudad</span> </td>
                                        <td style="width: 229px; height: 15px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxCD" runat="server" Width="290px"></asp:TextBox></td>
                                        <td style="width: 405px; height: 15px"><span style="font-size: 10pt">Estado</span>/Proivincia</td>
                                        <td style="width: 3666px; height: 15px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxEdoProv" runat="server" Width="256px" Wrap="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 103px; height: 5px"><span style="font-size: 10pt">Teléfono</span> </td>
                                        <td style="width: 229px; height: 5px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxTel" runat="server" Width="291px"></asp:TextBox></td>
                                        <td style="width: 405px; height: 5px">País </td>
                                        <td style="width: 3666px; height: 5px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownPais" runat="server" Width="264px" DataValueField="id_pais" DataTextField="nombre" DataSourceID="SelectPaises">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 103px; height: 14px"><span style="font-size: 10pt">Moneda</span> </td>
                                        <td style="width: 229px; height: 14px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownMoneda" runat="server" Width="153px" DataValueField="id_moneda" DataTextField="nombre" DataSourceID="SelectMonedas">
                                            </asp:DropDownList></td>
                                        <td style="width: 405px; height: 14px">Límite de <span style="font-size: 10pt">Crédito</span></td>
                                        <td style="width: 3666px; height: 14px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBoxLimCred" runat="server" Width="260px">0.00</asp:TextBox></td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Text="Insertar Datos"></asp:Button><br />
                            <br />
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Width="1475px" Font-Size="10pt" DataSourceID="SelectCorp" AllowSorting="True" DataKeyNames="id_corporativo" AutoGenerateColumns="False" AllowPaging="True">
                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:BoundField DataField="id_corporativo" HeaderText="ID Corp" InsertVisible="False" ReadOnly="True" SortExpression="id_corporativo">
                                            <ItemStyle Width="75px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                                            <ItemStyle Height="1px" Width="250px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Direcci&#243;n" SortExpression="direccion">
                                            <ItemStyle Width="250px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" SortExpression="ciudad">
                                            <ItemStyle Width="150px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado_provincia" HeaderText="Estado/Provincia" SortExpression="estado_provincia">
                                            <ItemStyle Width="150px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Tel&#233;fono" SortExpression="telefono">
                                            <ItemStyle Width="150px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NIT" HeaderText="Indent. Tributaria" SortExpression="NIT">
                                            <ItemStyle Width="150px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Pa&#237;s" SortExpression="pais">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" DataSourceID="SelectPaises" DataTextField="nombre"
                                                    DataValueField="id_pais" SelectedValue='<%# Bind("id_pais") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("pais") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Moneda" SortExpression="moneda">
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList2" runat="server" DataSourceID="SelectMonedas"
                                                    DataTextField="abreviatura" DataValueField="id_moneda" SelectedValue='<%# Bind("id_moneda") %>'>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("moneda") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="limite_credito" HeaderText="L&#237;mite de Cr&#233;dito" SortExpression="limite_credito" DataFormatString="{0:C2}">
                                            <ItemStyle Width="75px" Wrap="True" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                    <EditRowStyle BorderStyle="None" />
                                </asp:GridView>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource ID="SelectPaises" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectPaises" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SelectMonedas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectMonedas" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SelectCorp" runat="server" SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                SelectCommand="sp_SelectCorp"
                UpdateCommandType="StoredProcedure" UpdateCommand="sp_UpdateCorp">
                <SelectParameters>
                    <%--<asp:Parameter Name="id_pais" Type="Int32" />--%>
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="id_corporativo" Type="Int32" />
                    <asp:Parameter Name="nombre" Type="String" />
                    <asp:Parameter Name="direccion" Type="String" />
                    <asp:Parameter Name="ciudad" Type="String" />
                    <asp:Parameter Name="estado_provincia" Type="String" />
                    <asp:Parameter Name="telefono" Type="String" />
                    <asp:Parameter Name="NIT" Type="String" />
                    <asp:Parameter Name="id_pais" Type="Int32" />
                    <asp:Parameter Name="id_moneda" Type="Int32" />
                    <asp:Parameter Name="limite_credito" Type="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
   </div>
</asp:Content>

