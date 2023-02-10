<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="rastreo.aspx.vb" Inherits="ops_pages_rastreo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<Div style="position: absolute; top: 92px; left: 222px; height: 602px; width: 599px;">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Panel ID="Panel1" runat="server">
<div style="position: absolute; top: 2px; left: -209px; width: 63px;">
    <table style="width: 100%;">
        <tr>
            <td style="height: 49px">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate><img alt="" src="../Images/bigrotation2.gif" /></ProgressTemplate>
            </asp:UpdateProgress></td>
        </tr>
        <tr>
            <td style="height: 23px">
                Hasta 10 envíos<br />
                <asp:TextBox CssClass="form-control" Height="138px"   ID="TxtBoxEnvios" runat="server" Rows="10" 
                    TextMode="MultiLine"></asp:TextBox><br />
                <asp:CheckBox ID="cbMasupack" runat="server" Checked="True" 
                    Text="Número de Guía" /><br />
                <asp:CheckBox ID="cbRef" runat="server" Text="Referencia Cliente" />
                <asp:MutuallyExclusiveCheckBoxExtender ID="MeMasupack" runat="server" 
                    Key="guia" TargetControlID="cbMasupack">
                </asp:MutuallyExclusiveCheckBoxExtender>
                <asp:MutuallyExclusiveCheckBoxExtender ID="MeRef" runat="server" Key="guia" 
                    TargetControlID="cbRef">
                </asp:MutuallyExclusiveCheckBoxExtender>
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="BtnRastreo" runat="server" Text="Rastrear" 
                 style="text-align: left" Height="26px" /></td>
        </tr>
    </table>
</div>
    <table style="width: 100%">
      <tr>
        <td>
            <asp:TextBox CssClass="form-control" Height="52px"   ID="TextBox1" runat="server" BorderStyle="Solid" 
                style="font-weight: 700" TextMode="MultiLine" Visible="False" Width="604px"></asp:TextBox>
            <asp:GridView class="table table-striped table-bordered table-hover"  ID="GridView1" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                DataSourceID="SummaryTracking" GridLines="Horizontal" style="font-size: small" 
                Width="609px">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="id_envio" HeaderText="Guía" ReadOnly="True" 
                        SortExpression="id_envio" />
                    <asp:BoundField DataField="referencia1" HeaderText="Referencia" 
                        SortExpression="referencia1">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="id_status" HeaderText="id_status" 
                        SortExpression="id_status" Visible="False" />
                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" 
                        ReadOnly="True" SortExpression="observaciones">
                        <ItemStyle Width="400px" />
                    </asp:BoundField>
                </Columns> <PagerStyle CssClass="pagination-ys"></PagerStyle>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView class="table table-striped table-bordered table-hover"  ID="GridView2" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                DataSourceID="DetailTracking" GridLines="Horizontal" Height="122px" 
                HorizontalAlign="Left" style="font-size: small" Width="608px">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="id_envio" HeaderText="Guía" 
                        SortExpression="id_envio">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="referencia1" HeaderText="Referencia" 
                        SortExpression="referencia1">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="obs" HeaderText="Observaciones" ReadOnly="True" 
                        SortExpression="obs" />
                </Columns> <PagerStyle CssClass="pagination-ys"></PagerStyle>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
      </tr>
    </table>
        <asp:SqlDataSource ID="SummaryTracking" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_select_sumary_tracking" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="envio1" SessionField="Envio1" 
                    Type="String" />
                <asp:SessionParameter DefaultValue="" Name="envio2" SessionField="Envio2" 
                    Type="String" />
                <asp:SessionParameter DefaultValue="" Name="envio3" SessionField="Envio3" 
                    Type="String" />
                <asp:SessionParameter DefaultValue="" Name="envio4" SessionField="Envio4" 
                    Type="String" />
                <asp:SessionParameter Name="envio5" SessionField="Envio5" Type="String" />
                <asp:SessionParameter Name="envio6" SessionField="Envio6" Type="String" />
                <asp:SessionParameter Name="envio7" SessionField="Envio7" Type="String" />
                <asp:SessionParameter DefaultValue="" Name="envio8" SessionField="Envio8" 
                    Type="String" />
                <asp:SessionParameter DefaultValue="" Name="envio9" SessionField="Envio9" 
                    Type="String" />
                <asp:SessionParameter Name="envio10" SessionField="Envio10" Type="String" 
                    DefaultValue="" />
                <asp:SessionParameter DefaultValue="" Name="tipo_tracking" 
                    SessionField="tipo_tracking" Type="Int32" />
            </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DetailTracking" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        SelectCommand="sp_select_detail_tracking" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="id_envio" SessionField="id_envio" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel>
</ContentTemplate> 
</asp:UpdatePanel>   
</div>
</asp:Content>

