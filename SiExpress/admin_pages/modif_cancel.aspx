<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="modif_cancel.aspx.vb" Inherits="admin_pages_modif_cancel" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 175px; left: 267px;">
        <asp:updatepanel id="UpdatePanel1" runat="server">  
<ContentTemplate>
    <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
    <table style="width: 100%;">
        <tr>
            <td colspan="2" style="height: 44px">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate><img alt="" src="../Images/bigrotation2.gif" /><br/></ProgressTemplate>
                </asp:UpdateProgress></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox CssClass="form-control" Height="27px"   ID="txtMsg" runat="server"  
                    TextMode="MultiLine" Visible="False" Width="527px"></asp:TextBox>
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button2" runat="server" Text="Ok" Visible="False" /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center"><b><span style="font-size: large"> Cancelar un Envío</span></b></td>
        </tr>
        <tr>
            <td style="width: 250px; text-align: right">Número de envío</td>
            <td><asp:TextBox CssClass="form-control" Height="27px"   ID="txtEnvio" runat="server" TabIndex="1"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 250px; text-align: right">Observaciones</td>
            <td><asp:TextBox CssClass="form-control" Height="40px"   ID="txtObs" runat="server" TextMode="MultiLine" Width="315px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 250px; text-align: right"></td>
            <td><input type="text" style="visibility: hidden;position: absolute">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Text="Cancelar" Width="84px" 
                    TabIndex="2" /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center"><asp:Label ID="Label2" runat="server" Height="23px" Width="565px"></asp:Label></td>
        </tr>
    </table>
                                </div>
                        </div>
                    </div>
                </div>
</ContentTemplate>
</asp:updatepanel>
    </div>
</asp:Content>
