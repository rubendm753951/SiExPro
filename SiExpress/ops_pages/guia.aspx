<%@ Page Language="VB" AutoEventWireup="false" CodeFile="guia.aspx.vb" Inherits="ops_pages_guia" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:SqlDataSource ID="Guia" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
            SelectCommand="sp_Select_Datos_Envio" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="id_envio" QueryStringField="id_envio" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
