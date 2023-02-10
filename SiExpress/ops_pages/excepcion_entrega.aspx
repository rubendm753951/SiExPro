<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="excepcion_entrega.aspx.vb" Inherits="ops_pages_excepcion_entrega" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 168px; left: 249px; width: 699px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table>
                                    <tr>
                                        <td style="height: 42px;" colspan="2">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" /><br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="height: 42px"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 28px;" colspan="2">
                                            <asp:TextBox CssClass="form-control" Height="39px" ID="txtMsg" runat="server" TextMode="MultiLine" Visible="False"
                                                Width="515px"></asp:TextBox>
                                        </td>
                                        <td style="height: 28px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px; height: 28px;">&nbsp;</td>
                                        <td style="height: 28px; width: 267px;"><b>REGISTRA EXCEPCION DE ENTREGA</b></td>
                                        <td style="height: 28px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px; height: 28px;">
                                            <asp:Label ID="Label4" runat="server" Text="Número de Envío"></asp:Label></td>
                                        <td style="height: 28px; width: 267px;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" Width="129px"></asp:TextBox></td>
                                        <td style="height: 28px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px; height: 26px;">Motivo de excepción</td>
                                        <td style="height: 26px; width: 267px;">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="ListExceptions"
                                                runat="server" Width="362px" DataSourceID="SqlDataSource1"
                                                DataTextField="descripcion" DataValueField="id_excepcion">
                                            </asp:DropDownList></td>
                                        <td style="height: 26px">&nbsp;<asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Height="23px"
                                            Text="Registra Excepcion" Width="170px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 23px;">Observaciones </td>
                                        <td style="height: 23px;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtObs" runat="server" Width="360px" Wrap="False"></asp:TextBox>
                                        </td>
                                        <td style="height: 23px;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 23px;">
                                            <asp:Label ID="Label2" runat="server" Height="19px" Width="686px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_excepciones" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

