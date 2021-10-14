<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reporte_env_no_mov.aspx.vb" Inherits="Reports_Reporte_env_no_mov" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<Div style="position: absolute; top: 124px; left: 42px; width: 800px;">
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
                &nbsp;
                Unidad</td>
            <td style="height: 23px; ">
                &nbsp;<asp:DropDownList CssClass="form-control" Height="30px"  ID="DropDownUnis" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="nombre" DataValueField="id_oficina" Width="279px" 
                    style="margin-bottom: 0px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 21px; width: 370px;">
                Horas sin movimiento
                <asp:TextBox CssClass="form-control" Height="27px"   ID="TxtHoras" runat="server" Wrap="False"></asp:TextBox>
            </td>
            <td style="height: 21px; text-align: right;">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Text="Ejecutar Reporte" Width="124px" />
            </td>
        </tr>
    </table>
    <rsweb:reportviewer runat="server" Height="672px" Width="884px" 
        ID="Reportviewer1" Font-Names="Verdana" Font-Size="8pt">
        <LocalReport ReportPath="Reports\Reporte_env_no_mov.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_sp_reporte_env_sin_mov" />
            </DataSources>
        </LocalReport>
    </rsweb:reportviewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DataSet1TableAdapters.sp_reporte_env_sin_movTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownUnis" Name="id_oficina" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TxtHoras" Name="horas" PropertyName="Text" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_select_oficinas" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
</Div>
</asp:Content>

