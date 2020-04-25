<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="check_point.aspx.vb" Inherits="ops_pages_check_point" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 73%;">
                                    <tr>
                                        <td style="height: 50px">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" /><br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="height: 50px">
                                            <asp:TextBox CssClass="form-control" Height="39px" ID="txtMsg" runat="server" TextMode="MultiLine"
                                                Visible="False" Width="453px"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="16pt" Height="30px" Text="Punto de Control " Width="270px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">Número de Envío</td>
                                        <td style="width: 494px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" TabIndex="1"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">Observaciones</td>
                                        <td style="width: 494px">
                                            <asp:TextBox CssClass="form-control" Height="40px" ID="txtObs" runat="server" TextMode="MultiLine" Width="418px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; height: 30px;"></td>
                                        <td style="height: 30px; width: 494px">
                                            <input type="text"
                                                style="visibility: hidden; position: absolute; top: 178px; left: 127px; width: 3px;">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Check Point" Width="84px" TabIndex="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px"></td>
                                        <td style="width: 494px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="Label2" runat="server"
                                                Height="23px" Width="618px"></asp:Label></td>
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

