<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ventas_agente.aspx.vb" Inherits="Reports_ventas_agente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="position: absolute; top: 120px; left: 21px; width: 718px; height: 226px;">




    <table style="width: 74%;">
        <tr>
            <td colspan="2" style="text-align: left; height: 27px">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Reports/Report_Manager.aspx" 
                    style="font-weight: 700; color: #0000CC; text-decoration: underline">Regresar</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; height: 27px">
                <b>Parámetros del Reporte</b></td>
        </tr>
        <tr>
            <td style="width: 136px">
                Fecha Inicial</td>
            <td>
                <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 136px" atomicselection="False">
                Fecha Final</td>
            <td>
                <asp:TextBox CssClass="form-control" Height="27px"   ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Agente</td>
            <td>
                <asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownList1" runat="server" Width="439px" 
                    DataSourceID="SqlDataSource1" DataTextField="agente" 
                    DataValueField="id_agencia">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1002px" 
        Font-Names="Verdana" Font-Size="8pt" Height="400px">
        <LocalReport ReportPath="Reports\ventas_agente.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet3_sp_select_reporte_venta" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd"> </asp:CalendarExtender>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2" Format="yyyy/MM/dd"></asp:CalendarExtender>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DataSet3TableAdapters.sp_select_reporte_ventaTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="id_agente" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TextBox1" Name="fecha" PropertyName="Text" 
                Type="DateTime" />
            <asp:ControlParameter ControlID="TextBox2" Name="fecha2" PropertyName="Text" 
                Type="DateTime" />
            <asp:Parameter Name="echange_rate" Type="Decimal" />
        </SelectParameters>
    </asp:ObjectDataSource>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_Select_Agentes_por_usuarios" 
        SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" 
            Type="Int32" />
    </SelectParameters>
    </asp:SqlDataSource>


</div>
</asp:Content>

