<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reporte_estado_detalle.aspx.vb" Inherits="Reports_estado_envio_detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="position: absolute; top: 121px; left: 88px;">
    <table style="width: 66%;" align="center">
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
            <td style="height: 23px; width: 135px">
                &nbsp;
                Fecha Inicial</td>
            <td style="height: 23px; width: 156px">
                &nbsp;
                <asp:TextBox CssClass="form-control" Height="27px"   ID="FechaIni" runat="server"></asp:TextBox>
            </td>
            <td style="height: 23px; width: 100px">
                Fecha Final</td>
            <td style="height: 23px; width: 153px">
                &nbsp;
                <asp:TextBox CssClass="form-control" Height="27px"   ID="FechaFin" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; " colspan="2">
                &nbsp;
                &nbsp;
                Unidad</td>
            <td colspan="2">
                &nbsp;
                <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownList1" runat="server" DataSourceID="Oficinas" 
                    DataTextField="nombre" DataValueField="id_oficina" Width="237px" 
                    style="margin-bottom: 0px">
                </asp:DropDownList>
                </td>
        </tr>
    </table>
    <table style="width: 66%; height: 28px;">
        <tr>
            <td style="height: 4px">
                &nbsp;
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Height="24px" Text="Ejecutar Reporte" 
                    Width="134px" />
                &nbsp;</td>
        </tr>
    </table>
    
    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800px" 
        Font-Names="Verdana" Font-Size="8pt" Height="400px">
        <LocalReport ReportPath="Reports\Reporte_estado_detalle.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" 
                    Name="DataSet4_sp_reporte_estado_detalle" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetData" 
        TypeName="DataSet1TableAdapters.sp_reporte_estado_detalleTableAdapter" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="FechaIni" Name="inicio" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="FechaFin" Name="fin" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="DropDownList1" Name="id_oficina" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetData" 
        TypeName="DataSet1TableAdapters.sp_reporte_estado_detalleTableAdapter" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="FechaIni" DefaultValue="" Name="inicio" 
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="FechaFin" DefaultValue="" Name="fin" 
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="" 
                Name="id_oficina" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="Oficinas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_select_oficinas" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="FechaIni" Format="yyyy/MM/dd"></asp:CalendarExtender>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="FechaFin" Format="yyyy/MM/dd"></asp:CalendarExtender>
</div>
</asp:Content>

