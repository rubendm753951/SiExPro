<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="summary_tracking_cliente2.aspx.vb" Inherits="Reports_summary_tracking_cliente2" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="position: absolute; top: 125px; left: 66px; width: 691px;">
   <table style="width: 70%;" align="center">
        <tr>
            <td colspan="4" style="text-align: left; background-color: #FFFFFF">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Reports/Report_Manager.aspx" style="font-weight: 700">Regresar</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                <b>&nbsp;
                &nbsp;
                &nbsp;
                Parámetros del Reporte</b></td>
        </tr>
        <tr>
            <td style="height: 23px; width: 169px">
                &nbsp;
                Fecha Inicial</td>
            <td style="height: 23px; width: 158px">
                &nbsp;
                <asp:TextBox CssClass="form-control" Height="27px"   ID="FechaIni" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="FechaIni" Format="yyyy/MM/dd"></asp:CalendarExtender>
            </td>
            <td style="height: 23px; width: 102px">
                Fecha Final</td>
            <td style="height: 23px; width: 153px">
                &nbsp;
                <asp:TextBox CssClass="form-control" Height="27px"   ID="FechaFin" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="FechaFin" Format="yyyy/MM/dd"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="height: 23px; width: 169px">
                Cliente</td>
            <td style="height: 23px; " colspan="3">
                <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownList1" runat="server" 
                    DataSourceID="DSAgentes" DataTextField="agente" 
                    DataValueField="id_agencia" Width="384px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; " colspan="4">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Height="24px" Text="Ejecutar Reporte" 
                    Width="134px" />
            </td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="802px">
        <LocalReport ReportPath="Reports\summary_tracking_cliente2.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet3_sp_select_sumary_tracking_cliente" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="DSAgentes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_Select_Agentes_por_usuarios" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DataSet3TableAdapters.sp_select_sumary_tracking_clienteTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="FechaIni" Name="fecha1" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="FechaFin" Name="fecha2" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="DropDownList1" Name="id_agencia" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>

