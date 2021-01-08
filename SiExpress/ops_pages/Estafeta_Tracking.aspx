<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Estafeta_Tracking.aspx.vb" EnableViewState="true" Inherits="ops_pages_Estafeta_Tracking" Title="Estafeta Tracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 532px; left: 6px;">
                                    <tr>
                                        <td colspan="3" style="height: 52px; text-align: center;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="../Images/bigrotation2.gif" />Actualizando datos ...<br />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="16pt" Height="30px" Text="Estafeta Tracking" Width="270px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">Número de Envío</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td style="height: 30px; width: 494px">                                            
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" Text="Buscar" Width="84px" TabIndex="2" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td colspan="9" style="text-align: center; margin-top: 15px; padding-top: 15px">
                                            <div class="table-responsive">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" 
                                                    PageSize="10"
                                                    HorizontalAlign="Center"                                                                                                   
                                                    Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>                                                        
                                                        <asp:BoundField DataField="EventTime" HeaderText="Fecha" SortExpression="EventTime" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EventDescription" HeaderText="Descripcion" SortExpression="EventDescription" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EventPlace" HeaderText="Lugar" SortExpression="EventPlace" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EventException" HeaderText="Mensaje" SortExpression="EventException" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                               
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                                </asp:GridView>                                                    
                                            </div>
                                        </td>
                                    </tr>
                                </table>                                
                            </div>
                        </div>
                    </div>
                </div>               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
