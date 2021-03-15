<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin_tarifas_tarimas.aspx.vb"  MasterPageFile="~/Site.master" Inherits="admin_pages_admin_tarifas_tarimas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table>
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
                                        <td colspan="4" style="text-align: center; height: 25px;">
                                            <b>Tarifas Tarimas</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: center">
                                            <div class="table-responsive" style="padding-bottom: 15px">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="ID"
                                                    DataSourceID="Tarifas" PageSize="15"
                                                    AllowSorting="True"
                                                    HorizontalAlign="Center" Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>      
                                                        <asp:CommandField ShowEditButton="True" >
                                                            <ItemStyle Height="1px" Width="60px" HorizontalAlign="Left" Wrap="True" />
                                                            </asp:CommandField>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" SortExpression="ID" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="km_rango_de" HeaderText="Km De" SortExpression="km_rango_de" ReadOnly="True" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True"  />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="km_rango_a" HeaderText="Km A" SortExpression="km_rango_a" ReadOnly="True">
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zona" HeaderText="Zona" SortExpression="zona" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" SortExpression="Cuenta" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                                 
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                                </asp:GridView>  
                                            </div>
                                        </td>
                                    </tr>
                                </table>                               
                                
                                <br />
                            </div>
                        </div>
                    </div>
                </div>                
                <asp:SqlDataSource ID="Tarifas" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_tarifas_tarimas" SelectCommandType="StoredProcedure"  UpdateCommand="sp_update_tarifas_tarimas" UpdateCommandType="StoredProcedure">
                    <SelectParameters>                        
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:Parameter Name="zona" Type="Int32" />
                        <asp:Parameter Name="total" Type="Double" />
                        <asp:Parameter Name="cuenta" Type="Int32" />                        
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>