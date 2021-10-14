<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="manifiesto_dia.aspx.vb" Inherits="Reports_manifiesto_dia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="position: absolute; top: 187px; left: 102px; height: 369px; width: 600px;">
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
    </table>
    
    
      <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
          Font-Size="8pt" Height="400px" Width="639px">
          <LocalReport ReportPath="Reports\manifiesto_dia.rdlc">
              <DataSources>
                  <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                      Name="DataSet3_sp_select_reporte_venta_dia" />
              </DataSources>
          </LocalReport>
    </rsweb:ReportViewer>
    
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Fecha" Format="yyyy/MM/dd">
    </asp:CalendarExtender>
    
      <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
          SelectMethod="GetDataBy" 
          TypeName="DataSet3TableAdapters.sp_select_reporte_venta_diaTableAdapter" 
        OldValuesParameterFormatString="original_{0}">
          <SelectParameters>
              <asp:ControlParameter ControlID="IdAgente" Name="id_agente" 
                  PropertyName="SelectedValue" Type="Int32" />
              <asp:ControlParameter ControlID="Fecha" Name="fecha" PropertyName="Text" 
                  Type="DateTime" />
              <asp:Parameter DefaultValue="1" Name="echange_rate" Type="Decimal" />
          </SelectParameters>
      </asp:ObjectDataSource>
    <asp:SqlDataSource ID="DSAgentsUser" runat="server" 
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

