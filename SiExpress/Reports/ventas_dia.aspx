<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ventas_dia.aspx.vb" Inherits="Reports_ventas_dia" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:92%; margin: 0 auto; text-align:left; left: 56px; position: absolute; top: 125px;" 
        class="central" >

    <table  align="Center" 
        style="width: 485px; TEXT-ALIGN: center; height: 83px; border-style:solid; table-layout:auto">
        <tr>
            <td style="height: 23px; background-color: #FFFFFF; text-align: left;" colspan="2" 
                bgcolor="#CCCCCC">
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="~/Reports/Report_Manager.aspx"
        Width="180px">Regresar</asp:HyperLink></td>
        </tr>
        <tr>
            <td colspan="2" bgcolor="#CCCCCC">
                Parámetros del Reporte</td>
        </tr>
        <tr>
            <td style="width: 167px; ">
                Agente</td>
            <td style="width: 337px; TEXT-ALIGN: left" valign="top">
                <asp:DropDownList CssClass="form-control" Height="30px"  ID="IdAgente" runat="server" DataSourceID="DSAgentsUser" DataTextField="agente"
                    DataValueField="id_agencia" Width="357px">
                </asp:DropDownList><br />
                </td>
        </tr>
        <tr>
            <td style="width: 167px; height: 22px; ">
                Fecha</td>
            <td style="height: 22px; width: 337px; TEXT-ALIGN: left" valign="top">
                <asp:TextBox CssClass="form-control" Height="27px"   ID="Fecha" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 167px; ">
                Moneda</td>
            <td style="width: 337px; TEXT-ALIGN: left" valign="top">
                <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownList1" runat="server" 
                    DataSourceID="DSMonedas" DataTextField="nombre"
                    DataValueField="id_moneda" Width="144px">
                </asp:DropDownList>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 167px; ">
                Tipo de Cambio</td>
            <td style="width: 337px; TEXT-ALIGN: left" valign="top">
                <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBox1" runat="server" Width="73px">1</asp:TextBox></td>
        </tr>
    </table>
    <br />
<rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt"
        height="400px" width="876px">
<LocalReport ReportPath="Reports\ventas_dia.rdlc">
    <DataSources>
        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet3_sp_select_reporte_venta_dia" />
    </DataSources>
</LocalReport>
</rsweb:reportviewer>
    
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
        TargetControlID="Fecha" Format="yyyy/MM/dd">
    </asp:CalendarExtender>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="DataSet3TableAdapters.sp_select_reporte_venta_diaTableAdapter" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="IdAgente" DefaultValue="16" Name="id_agente" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="Fecha" DefaultValue="15-Sep-2010" Name="fecha" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="TextBox1" DefaultValue="1" Name="echange_rate" PropertyName="Text" Type="Decimal" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="DSAgentsUser" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
        SelectCommand="sp_Select_Agentes_por_usuarios" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DSMonedas" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
        SelectCommand="sp_SelectMonedas" SelectCommandType="StoredProcedure">
<%--        <SelectParameters>
            <asp:Parameter Name="id_moneda" Type="Int32" />
        </SelectParameters>--%>
    </asp:SqlDataSource>
    <br />
</div>
</asp:Content>

