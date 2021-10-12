<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintFedexLabel.aspx.vb" Inherits=" Reports_PrintFedexLabel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css" media="print">
    @page 
    {
        size: auto;   /* auto is the initial value */
        margin: 0mm;  /* this affects the margin in the printer settings */
    }
</style>
</head>
<body style=" margin:0px;width:295px; height:440px;" >
    <form id="form1" runat="server" style="width: 295px">
        <asp:Panel ID="FedexLabelPanel" runat="server" Width="295px">
        </asp:Panel>
    </form>
</body>
</html>
