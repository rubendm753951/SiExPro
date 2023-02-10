<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="asigna_guias.aspx.vb" Inherits="ops_pages_asigna_guias" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="../Images/bigrotation2.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <br />
                                <br />
                                Seleccione un Agente<br />
                                <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre" DataValueField="id_agencia" Width="422px" AutoPostBack="True">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <br />
                                <table>
                                    <tr>
                                        <td colspan="3" style="height: 19px">
                                            <strong><span >Guías ya utilizadas</span></strong></td>
                                        <td colspan="3" style="height: 19px">
                                            <strong>Guías sin utilizar</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 69px; height: 19px">
                                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Total"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 90px; height: 19px">
                                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Primera Guía"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 97px; height: 19px">
                                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Última Guía"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 124px; height: 19px">
                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Total"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                        <td style="height: 19px">
                                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Primera Guía"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 767px; height: 19px">
                                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Última Guía"
                                                Width="133px" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 69px; height: 19px">
                                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                        <td style="width: 90px; height: 19px">
                                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                        <td style="width: 97px; height: 19px">
                                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                        <td style="width: 124px; height: 19px">
                                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                        <td style="height: 19px">
                                            <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                        <td style="width: 767px; height: 19px">
                                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="9pt" Width="133px" BackColor="#FFFFFF"></asp:Label></td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table style="width: 231px; height: 12px;">
                                    <tr>
                                        <td colspan="3" style="height: 14px">
                                            <strong>Asignar nuevo rango</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 19px">
                                            <strong>Guia Inicial</strong></td>
                                        <td style="width: 97px; height: 19px; text-align: center;">
                                            <strong>Guia Final</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90px; height: 19px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox1" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 97px; height: 19px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox2" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Asignar Rango" /><br />
                                <br />
                                Mensaje
                                <asp:Label ID="Label13" runat="server" Text="Label" Width="95px"></asp:Label>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_SelectAgencias" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

