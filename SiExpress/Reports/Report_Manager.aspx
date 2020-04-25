<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Report_Manager.aspx.vb" Inherits="Reports_Report_Manager" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 152px; left: 250px; height: 286px; width: 542px;">
        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:panel id="Panel1" runat="server" height="289px" width="832px">
    <table>
        <tr>
            <td style="width: 346px; height: 15px; text-align: left; vertical-align: top; color: #2e6c80; ">
                <h1>
                    <b>Reportes Operativos</b></h1>
            </td>
            <td style="width: 354px; height: 15px; vertical-align: top; text-align: left; ">
                <h1 style="color: #2e6c80">
                    <b>Reportes Administrativos</b></h1>
            </td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Underline="True"
                    ForeColor="Blue" Width="271px" NavigateUrl="~/Reports/ventas_dia.aspx" 
                    Height="16px" style="text-align: left; margin-right: 0px">Reporte de Ventas Diario</asp:HyperLink></td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                <asp:HyperLink ID="HyperLink7" runat="server" 
                    NavigateUrl="~/Reports/ventas_agente.aspx" 
                    style="font-weight: 700; color: #0000FF; text-decoration: underline">Reporte de Ventas por Agente</asp:HyperLink>
                </td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink6" runat="server" 
                    NavigateUrl="~/Reports/manifiesto_dia.aspx" 
                    style="font-weight: 700; color: #0000CC">Manifiesto de Recolección</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                <asp:HyperLink ID="HyperLink10" runat="server" 
                    NavigateUrl="~/Reports/Reporte_entrega.aspx" 
                    style="font-weight: 700; color: #0000CC; text-decoration: underline">Tiempo de entrega por agente (en desarrollo)</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="#0000CC" 
                    NavigateUrl="~/Reports/Reporte_Estado.aspx">Reporte de Estado</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 26px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Bold="True" 
                    ForeColor="#0000CC" NavigateUrl="~/Reports/Reporte_estado_detalle.aspx">Reporte de estado por unidad</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 26px; vertical-align: top; text-align: left;">
                </td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink5" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="#0000CC" 
                    NavigateUrl="~/Reports/Reporte_env_no_mov.aspx">Reporte de envíos sin movimiento</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink9" runat="server" 
                    NavigateUrl="~/Reports/Envios_procesados_sin_movimiento.aspx" 
                    style="font-weight: 700; color: #0000FF; text-decoration: underline">Reporte de envios procesados sin movimiento</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink3" runat="server" 
                    NavigateUrl="~/Reports/Reporte_contenedores.aspx" 
                    style="font-weight: 700; color: #0000FF; text-decoration: underline">Reporte de contenedores</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top">
                <asp:HyperLink ID="HyperLink12" runat="server" 
                    NavigateUrl="~/Reports/Reporte_Comentarios.aspx" 
                    style="font-weight: 700; color: #0000FF; text-decoration: underline">Reporte de comentarios sin respuesta</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top; font-size: large; background-color: #666666;">
                <b><span style="color: #FFFFFF; background-color: #666666">Reportes Especiales</span></b></td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                </td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top; font-size: large;">
                <asp:HyperLink ID="HyperLink8" runat="server" ForeColor="#0000CC" 
                    NavigateUrl="~/Reports/summary_tracking_cliente.aspx" 
                    style="font-weight: 700; font-size: small">Reporte de estado (clientes)</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px; height: 25px; text-align: left; vertical-align: top; font-size: large;">
                <asp:HyperLink ID="HyperLink11" runat="server" ForeColor="#0000CC" 
                    NavigateUrl="~/Reports/summary_tracking_cliente2.aspx" 
                    style="font-weight: 700; font-size: small">Reporte de estado (clientes) New!</asp:HyperLink>
            </td>
            <td style="width: 354px; height: 25px; vertical-align: top; text-align: left;">
                &nbsp;</td>
        </tr>
    </table>
 </asp:panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

