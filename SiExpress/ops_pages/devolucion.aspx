<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="devolucion.aspx.vb" Inherits="ops_pages_devolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div style="position: absolute; top: 170px; left: 235px; width: 699px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table>
                                    <tr>
                                        <td style="height: 50px;" colspan="2">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" /><br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="height: 50px"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 28px;" colspan="2">
                                            <asp:TextBox CssClass="form-control" Height="39px" ID="txtMsg" runat="server" TextMode="MultiLine"
                                                Visible="False" Width="517px"></asp:TextBox></td>
                                        <td style="height: 28px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px; height: 28px;">&nbsp;</td>
                                        <td style="height: 28px; width: 220px;"><b>DEVOLUCION DE ENVIOS</b></td>
                                        <td style="height: 28px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px; height: 28px;">
                                            <asp:Label ID="Label4" runat="server" Text="Número de Envío"></asp:Label></td>
                                        <td style="height: 28px; width: 220px;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" Width="129px"></asp:TextBox></td>
                                        <td style="height: 28px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px">Comentarios</td>
                                        <td style="text-align: left; width: 220px;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtObs" runat="server" Width="340px" Wrap="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Devolver Envío" Width="170px"
                                                Height="23px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 23px;" colspan="3">
                                            <asp:Label ID="Label2" runat="server" Height="23px" Width="696px"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

