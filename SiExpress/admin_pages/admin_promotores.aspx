<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_promotores.aspx.vb" Inherits="admin_pages_admin_promotores" %>

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
                                        <td colspan="2" style="text-align: center; height: 47px;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" /><br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center; height: 39px;">
                                            <asp:TextBox CssClass="form-control" Height="51px" ID="txtMsg" runat="server" TextMode="MultiLine"
                                                Visible="False" Width="579px"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center"><b>Administración de Promotores</b></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 299px">Nombre del Promotor</td>
                                        <td>Oficina</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 18px; width: 299px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox1" runat="server" Width="283px"></asp:TextBox></td>
                                        <td style="height: 18px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" Width="339px"
                                                AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="nombre"
                                                DataValueField="id_oficina">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="BtnInsertPromo" runat="server" Text="Crear Promotor" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Width="658px"
                                                AutoGenerateColumns="False"
                                                DataSourceID="SqlDataSource2" AllowPaging="True"
                                                DataKeyNames="id_promotor">
                                                <Columns>
                                                    <asp:CommandField ShowEditButton="True" />
                                                    <asp:BoundField DataField="id_promotor" HeaderText="id_promotor" ReadOnly="True"
                                                        SortExpression="id_promotor"></asp:BoundField>
                                                    <asp:BoundField DataField="nombre" HeaderText="nombre"
                                                        SortExpression="nombre"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="id_oficina" SortExpression="id_oficina">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre"
                                                                DataValueField="id_oficina" SelectedValue='<%# Bind("id_oficina") %>'>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_oficina") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="baja" DataFormatString="{0:d}" HeaderText="baja"
                                                        SortExpression="baja" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_oficinas" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_promotores" SelectCommandType="StoredProcedure"
                    UpdateCommand="sp_update_promotores" UpdateCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" DefaultValue=""
                            Name="id_oficina" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="id_promotor" Type="Int32" />
                        <asp:Parameter Name="nombre" Type="String" />
                        <asp:Parameter Name="id_oficina" Type="Int32" />
                        <asp:Parameter Name="baja" Type="DateTime" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

