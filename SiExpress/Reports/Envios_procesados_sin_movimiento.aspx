<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Envios_procesados_sin_movimiento.aspx.vb" Inherits="Reports_Envios_procesados_sin_movimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position: absolute; top: 124px; left: 42px; width: 800px;">

    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" Format="yyyy/MM/dd">
    </asp:CalendarExtender>
    
   <table style="width: 77%;" align="center">
        <tr>
            <td colspan="2" style="text-align: left; background-color: #FFFFFF">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Reports/Report_Manager.aspx" style="font-weight: 700">Regresar</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; background-color: #CCCCCC">
                <b>&nbsp;
                &nbsp;
                &nbsp;
                Parámetros del Reporte</b></td>
        </tr>
        <tr>
            <td style="height: 23px; width: 370px; text-align: right;">
                &nbsp;</td>
            <td style="height: 23px; ">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right; height: 21px; width: 370px;">
                Fecha
                <asp:TextBox CssClass="form-control" Height="27px"   ID="TxtFecha" runat="server"></asp:TextBox>
            </td>
            <td style="height: 21px; text-align: right;">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Text="Ejecutar Reporte" Width="124px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; height: 21px; font-size: large;" colspan="2">
                <b>Envíos Procesados sin movimiento</b></td>
        </tr>
    </table>


    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="754px">
        <LocalReport ReportPath="Reports\Envios_procesados_sin_movimiento.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_sp_select_envios_sin_movimiento" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>


    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        
        TypeName="DataSet1TableAdapters.sp_select_envios_sin_movimientoTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="TxtFecha" Name="fecha" 
                PropertyName="Text" Type="DateTime" />
            <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>


</asp:Content>

