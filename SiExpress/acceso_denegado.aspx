<%@ Page Language="VB" MasterPageFile="~/DefaultMasterPAge.master" AutoEventWireup="false" CodeFile="acceso_denegado.aspx.vb" Inherits="acces_denegado" title="Acceso Denegado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
    <p></p>
    <div style="margin-bottom: 0px; position: absolute; top: 235px; left: 230px;">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
        Height="38px" Text="Nombre de usuario y/o contraseña incorrectos" Width="614px">
    </asp:Label>
    <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/acceso.aspx">Regresar ...</asp:HyperLink>
    </div> 
</asp:Content>

