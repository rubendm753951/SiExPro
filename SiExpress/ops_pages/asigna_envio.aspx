<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="asigna_envio.aspx.vb" Inherits="ops_pages_asigna_envio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div style="position: absolute; top: 150px; left: 235px; width: 668px; height: 460px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 160px; height: 73px">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" /><br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="height: 73px" colspan="2">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtMsg" runat="server" TextMode="MultiLine" Visible="False" Width="431px"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 160px; height: 43px;">&nbsp;
                                        </td>
                                        <td style="width: 258px; height: 43px;">&nbsp;<b> Asignación de Envíos para Entrega</b></td>
                                        <td style="height: 43px">&nbsp;<asp:TextBox CssClass="form-control" Height="27px" ID="TextBox1" runat="server" Width="250px"
                                            Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 160px; height: 30px;">&nbsp; Selecciona Promotor</td>
                                        <td style="width: 258px; height: 30px;">&nbsp;<asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownIdPromo" runat="server"
                                            Width="250px" DataSourceID="SqlDataSource1" DataTextField="nombre"
                                            DataValueField="id_promotor">
                                        </asp:DropDownList>
                                        </td>
                                        <td style="height: 30px">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 160px">&nbsp; Envío</td>
                                        <td style="width: 258px; text-align: center;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBox_id_envio" runat="server" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td>
                                            <input type="text" style="visibility: hidden; position: absolute">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Asignar" runat="server" Text="Asignar" TabIndex="2" />
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Text="Manifiesto de Asignación"
                                                Width="163px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center;">
                                            <br />
                                            Envíos Actualmente asignados</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center;">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Width="656px" AllowPaging="True"
                                                AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                                                <Columns>
                                                    <asp:BoundField DataField="id_promotor" HeaderText="id_promotor"
                                                        SortExpression="id_promotor" Visible="False" />
                                                    <asp:BoundField DataField="id_usuario" HeaderText="id_usuario"
                                                        SortExpression="id_usuario" Visible="False" />
                                                    <asp:BoundField DataField="id_envio" HeaderText="Envío"
                                                        SortExpression="id_envio" />
                                                    <asp:BoundField DataField="asignaciones" HeaderText="asignaciones"
                                                        SortExpression="asignaciones" />
                                                    <asp:BoundField DataField="promotor" HeaderText="Promotor"
                                                        SortExpression="promotor" />
                                                    <asp:BoundField DataField="asignado_por" HeaderText="Asignado por"
                                                        SortExpression="asignado_por" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
        SelectCommand="sp_select_promotores" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_oficina"
                SessionField="id_oficina" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
        SelectCommand="sp_select_envios_asignados_promotor"
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownIdPromo" Name="id_promotor"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

