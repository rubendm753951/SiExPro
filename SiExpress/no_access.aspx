<%@ Page Language="VB" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="false" title="Acceso Denegado" CodeFile="no_access.aspx.vb" Inherits="no_access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div style="text-align: center; position: absolute; top: 239px; left: 313px; height: 35px; width: 462px;">
    <strong><span style="font-size: 24pt; font-family: Arial">No tiene acceso a esta opción </span></strong>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/admin_pages/admin.aspx">Regresar ...</asp:HyperLink>
</div> 
</asp:Content>

