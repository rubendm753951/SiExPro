    <%@ Page Language="VB" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="acceso.aspx.vb" Inherits="acceso" title="Acceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
    <div style="width:36%; margin: 0 auto; text-align:left; left: 362px; position: absolute; top: 150px;" >
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate><img alt="" src="Images/bigrotation2.gif" /><br/></ProgressTemplate>
        </asp:UpdateProgress>
            <div class="row">
                <div class="col-lg-8">
                    <div class="panel panel-red">
                        <div class="panel-heading">
                            Solo Personal Autorizado
                        </div>
                        <div class="panel-body">
                            <table style="width: 315px;" >
                                <tr>
                                    <td style="width: 1063px; height: 37px">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="25px" Text="Nombre de Usuario" Width="173px"></asp:Label>
                                    </td>
                                    <td style="width: 305px; height: 37px">
                                        <asp:TextBox CssClass="form-control" Height="27px"   ID="TxtBoxUser_Name" runat="server" Width="142px">admin</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1063px; height: 37px">
                                        <asp:Label ID="Contraseña" runat="server" Font-Bold="True" Height="23px" Text="Contraseña" Width="174px"></asp:Label>
                                    </td>
                                    <td style="width: 305px; height: 37px">
                                        <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBoxPwd" runat="server" TextMode="Password" Width="141px">jk134rdm</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1063px">
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Button CssClass="btn btn-success btn-sm"  ID="Button1" runat="server" Text="Ingresar" />
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

