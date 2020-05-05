<%@ Page Language="VB" AutoEventWireup="false" CodeFile="guia_mult.aspx.vb" Inherits="ops_pages_guia_mult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
            &nbsp;<asp:SqlDataSource ID="Guia" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
            SelectCommand="sp_Select_Datos_Envio_mult" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="id_envio1" QueryStringField="id_envio1" 
                    Type="Int32" DefaultValue="" />
                <asp:QueryStringParameter Name="id_envio2" QueryStringField="id_envio2" 
                    Type="Int32" />
                <asp:QueryStringParameter Name="id_agente" QueryStringField="id_agente" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

            <asp:TextBox CssClass="form-control" Height="154px"   ID="TxtMsg" runat="server" Width="322px"></asp:TextBox>

    </div>
    </form>
</body>
</html>
