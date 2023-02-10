<%@ Page Inherits="Reports_Reporte_Comentarios" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reporte_Comentarios.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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
    </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1002px" 
            Font-Names="Verdana" Font-Size="8pt" Height="400px">
        <LocalReport EnableHyperlinks="True" ReportPath="Reports\Reporte_Comentarios.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    
    <br />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="Reporte_ComentariosTableAdapters.sp_select_reporte_comentariosTableAdapter">
        <SelectParameters>
            <asp:sessionparameter name="id_usuario" sessionfield="id_usuario" type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>