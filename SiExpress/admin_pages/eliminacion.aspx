<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="eliminacion.aspx.vb" Inherits="admin_pages_eliminacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:ConfirmButtonExtender ID="btnConfirmExport" runat="server"
                                    TargetControlID="Button1"
                                    ConfirmText="Se eliminará el último status de este envío, ¿está seguro?">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                                    TargetControlID="Button3"
                                    ConfirmText="¡¡¡¡¡Alto!!!! El envío será elimiando, ¿está seguro?">
                                </asp:ConfirmButtonExtender>
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
                                        <td colspan="2" style="height: 28px; text-align: center;">
                                            <asp:TextBox CssClass="form-control" Height="39px" ID="txtMsg" runat="server" TextMode="MultiLine"
                                                Visible="False" Width="517px"></asp:TextBox>
                                        </td>
                                        <td colspan="2" style="height: 28px; text-align: center;">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Text="Ok" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 28px; text-align: center;">
                                            <b>MODIFICACION DE ENVIO</b></td>
                                        <td colspan="2" style="height: 28px; text-align: center;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 26px; text-align: center;">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" Width="129px"></asp:TextBox>
                                        </td>
                                        <td colspan="2" style="height: 26px; text-align: center;"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 309px">&nbsp;<asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Height="23px"
                                            Text="Eliminar Último Status" Width="170px" />
                                        </td>
                                        <td colspan="2" style="text-align: left; width: 220px;">
                                            <asp:Button CssClass="btn btn-outline btn-danger btn-sm" ID="Button3" runat="server" Font-Bold="True" 
                                                Height="23px" Text="Eliminar Envío" Width="170px" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 23px;">
                                            <asp:Label ID="Label2" runat="server" Height="23px" Width="696px"></asp:Label>
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
</asp:Content>

