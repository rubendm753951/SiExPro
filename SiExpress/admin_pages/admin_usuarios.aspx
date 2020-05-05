<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_usuarios.aspx.vb" Inherits="admin_usuarios" Title="Untitled Page" %>
<asp:Content ID="Content3" ContentPlaceHolderID="StyleSection" runat="server" >
     <link rel="Stylesheet" type="text/css" href="../Skin/CSS/bootstrap-multiselect.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: center; height: 35px;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img src="../Images/bigrotation2.gif" style="width: 36px" alt="Rotation" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center; height: 25px;">
                                            <b>Agregar Usuario</b></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px;" colspan="3">Nombre</td>
                                        <td colspan="2" style="height: 24px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtNombre" runat="server" Width="430px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <div class="form-group">
                                            <td style="width: 97px; text-align: left;">Usuario</td>
                                            <td style="width: 154px">
                                                <asp:TextBox CssClass="form-control" Height="27px" ID="Txtusuario" runat="server" Width="136px"></asp:TextBox></td>
                                            <td style="width: 83px">Oficina</td>
                                            <td colspan="2">
                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList3" runat="server" DataSourceID="SqlDataSource6"
                                                    DataTextField="nombre" DataValueField="id_oficina" Width="430px">
                                                </asp:DropDownList></td>
                                        </div>
                                    </tr>
                                    <tr>
                                        <td style="width: 97px; text-align: left;">Password</td>
                                        <td style="width: 154px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtPassword" runat="server" Width="136px"></asp:TextBox></td>
                                        <td style="width: 83px">Perfil</td>
                                        <td style="width: 254px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList6" runat="server" DataSourceID="SqlDataSource7"
                                                DataTextField="descripcion" DataValueField="id_perfil">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Agregar Usuario" Width="122px" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center"><b>Seleccione un usuario para modifcar</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: center">
                                            <div class="table-responsive">
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="712px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="id_usuario"
                                                    DataSourceID="SqlDataSource1" Font-Size="8pt" PageSize="10"
                                                    HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" ShowSelectButton="True"  />
                                                        <asp:BoundField DataField="id_usuario" HeaderText="id_usuario" ReadOnly="True" SortExpression="id_usuario" />
                                                        <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                                                        <asp:BoundField DataField="LOGIN" HeaderText="LOGIN" SortExpression="LOGIN" />
                                                        <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
                                                        <asp:BoundField DataField="baja" HeaderText="baja" SortExpression="baja" DataFormatString="{0:d}" />
                                                        <asp:TemplateField HeaderText="id_oficina" SortExpression="id_oficina">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList5" runat="server" DataSourceID="SqlDataSource6"
                                                                    DataTextField="nombre" DataValueField="id_oficina" SelectedValue='<%# Bind("id_oficina") %>'
                                                                    Width="159px">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" Height="20px" ID="TextBox2" runat="server" Enabled="False" ReadOnly="True" Text='<%# Bind("id_oficina") %>'
                                                                    Width="54px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="150px" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="id_perfil" SortExpression="id_perfil">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList4" runat="server" DataSourceID="SqlDataSource7"
                                                                    DataTextField="descripcion" DataValueField="id_perfil" SelectedValue='<%# Bind("id_perfil") %>'>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" Height="20px" ID="TextBox3" runat="server" Enabled="False" ReadOnly="True" Text='<%# Bind("id_perfil") %>'
                                                                    Width="60px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>

                                                    <EditRowStyle Wrap="False" />
                                                    <SelectedRowStyle Height="10px" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="4" style="text-align: center" class="text-center">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox1" runat="server" Width="362px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 263px">Asignar Agentes</td>
                                        <td style="width: 82px"></td>
                                        <td style="width: 245px">Asignar Privilegios</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <%--<td style="width: 263px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource3"
                                                DataTextField="nombre" DataValueField="id_agencia" Width="247px">
                                            </asp:DropDownList></td>--%>
                                        <td style="width: 263px">
                                            <asp:ListBox CssClass="form-control" Height="30px" ID="DropDownList7" runat="server" DataSourceID="SqlDataSource3"
                                                DataTextField="nombre" DataValueField="id_agencia" Width="247px" SelectionMode="Multiple">
                                            </asp:ListBox></td>
                                        <td style="width: 82px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Agregar" /></td>
                                        <td style="width: 245px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList2" runat="server" DataSourceID="SqlDataSource4"
                                                DataTextField="nombre" DataValueField="id_modulo" Width="239px">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button4" runat="server" Text="Agregar" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView2" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Height="99px"
                                                Width="345px" Font-Size="8pt" DataKeyNames="id_usuario,id_agencia">
                                                <RowStyle Height="12px" Width="100px" Wrap="False" />
                                                <Columns>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="id_usuario" HeaderText="id_usuario" ReadOnly="True" SortExpression="id_usuario" />
                                                    <asp:BoundField DataField="id_agencia" HeaderText="id_agencia" ReadOnly="True" SortExpression="id_agencia" />
                                                    <asp:BoundField DataField="agente" HeaderText="agente" ReadOnly="True" SortExpression="agente" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                            </asp:GridView>
                                        </td>
                                        <td colspan="2" style="text-align: center">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                DataKeyNames="id_usuarios_privilegios" DataSourceID="SqlDataSource5" Font-Size="8pt"
                                                Height="119px" Width="366px">
                                                <RowStyle Height="12px" Wrap="False" />
                                                <Columns>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="id_usuarios_privilegios" HeaderText="id" ReadOnly="True"
                                                        SortExpression="id_usuarios_privilegios">
                                                        <ItemStyle Width="25px" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
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
                <asp:TextBox CssClass="form-control" Height="37px" ID="TxtError" runat="server" TextMode="MultiLine" Visible="False"
                    Width="639px"></asp:TextBox>
                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Text="Ok" Visible="False" Width="30px" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_usuarios" SelectCommandType="StoredProcedure" UpdateCommand="sp_update_usuarios"
                    UpdateCommandType="StoredProcedure">
                    <UpdateParameters>
                        <asp:Parameter Name="id_usuario" Type="Int32" />
                        <asp:Parameter Name="nombre" Type="String" />
                        <asp:Parameter Name="login" Type="String" />
                        <asp:Parameter Name="password" Type="String" />
                        <asp:Parameter Name="baja" Type="DateTime" />
                        <asp:Parameter Name="id_oficina" Type="Int32" />
                    </UpdateParameters>
                    <SelectParameters>
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    DeleteCommand="sp_delete_agencias_usuarios" DeleteCommandType="StoredProcedure"
                    SelectCommand="sp_Select_Agentes_por_usuarios" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="id_usuario" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="id_agencia" Type="Int32" />
                        <asp:Parameter Name="id_usuario" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="SELECT [id_agencia], [nombre] FROM [C_AGENCIAS] Where fecha_termino > getdate()"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_modulos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    DeleteCommand="sp_delete_usuarios_modulos" DeleteCommandType="StoredProcedure"
                    SelectCommand="sp_select_usuarios_modulos" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="id_usuario" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="id_usuarios_privilegios" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_oficinas" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_perfiles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <br />
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptSection" runat="server" >
    <script type="text/javascript" src="../Skin/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=DropDownList7]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: "No hay elementos seleccionados.",
                nSelectedText: 'seleccionados',
                allSelectedText: 'Todos seleccionados',
                selectAllText: ' Seleccionar todos'
            });
        });
    </script>
</asp:Content>