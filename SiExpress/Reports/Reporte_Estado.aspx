<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reporte_Estado.aspx.vb" Inherits="Reports_Reporte_Estado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="position: absolute; top: 120px; left: 30px; width: 874px;">
    <table style="width: 43%;">
        <tr>
            <td colspan="2" style="text-align: left; background-color: #FFFFFF">
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="~/Reports/Report_Manager.aspx"
        Width="180px">Regresar</asp:HyperLink></td>
            <td style="text-align: left; background-color: #FFFFFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" 
                style="text-align: center; background-color: #CCCCCC; height: 23px;">
                <b>Parámetros del Reporte</b></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 158px; text-align: center">
                &nbsp;Fecha Inical</td>
            <td style="height: 22px; width: 175px; text-align: center">
                Fecha Final&nbsp;
            </td>
            <td style="height: 22px; width: 175px; text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 158px; text-align: center; height: 30px;">
                &nbsp;<asp:TextBox CssClass="form-control" Height="27px"   ID="FechaIni" runat="server" style="text-align: center"></asp:TextBox>
            </td>
            <td style="width: 175px; text-align: center; height: 30px;">
                &nbsp;
                <asp:TextBox CssClass="form-control" Height="27px"   ID="FechaFin" runat="server" style="text-align: center" 
                    Wrap="False"></asp:TextBox>
            </td>
            <td style="width: 175px; text-align: center; height: 30px;">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Height="27px" Text="Ejecutar" />
            </td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="1030px">
        <LocalReport ReportPath="Reports\Reporte_estado.rdlc" 
            EnableHyperlinks="True">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_sp_reporte_estado" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="FechaIni" Format="yyyy/MM/dd"></asp:CalendarExtender>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="FechaFin" Format="yyyy/MM/dd"></asp:CalendarExtender>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DataSet1TableAdapters.sp_reporte_estadoTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="FechaIni" Name="inicio" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="FechaFin" Name="fin" PropertyName="Text" 
                Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>

