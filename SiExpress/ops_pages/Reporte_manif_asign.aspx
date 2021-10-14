<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reporte_manif_asign.aspx.vb" Inherits="Reports_Reporte__manif_asign" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="622px" style="margin-top: 0px">
        <LocalReport ReportPath="App_Code\Reports\Reporte_manif_asign.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_sp_select_envios_asignados_promotor" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="DataSet1TableAdapters.sp_select_envios_asignados_promotorTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="id_promotor" Type="Int32" />
            <asp:Parameter Name="fecha" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="Asignacion" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_select_envios_asignados_promotor" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="id_promotor" QueryStringField="id_promotor" 
                Type="Int32" />
            <asp:QueryStringParameter Name="fecha" QueryStringField="fecha" 
                Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

