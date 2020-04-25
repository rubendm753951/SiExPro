<%@ Page Language="VB" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="no_session.aspx.vb" Inherits="no_session" title="No ha iniciado Sesión" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div style="text-align: center; position: absolute; top: 239px; left: 313px; height: 35px; width: 462px;">
    <strong><span style="font-size: 24pt; font-family: Arial">No ha iniciado Sesión </span></strong>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/acceso.aspx">Regresar ...</asp:HyperLink>
</div> 
</asp:Content>

