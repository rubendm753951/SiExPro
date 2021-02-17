<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin_clientes.aspx.vb" MasterPageFile="~/Site.master" Inherits="admin_pages_admin_clientes" %>

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
                                            <b>Buscar Tarifas</b></td>
                                    </tr>                                                                  
                                    <tr>
                                        <td colspan="9" style="text-align: center">
                                            <div class="table-responsive" style="padding-bottom: 15px">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="id_cliente"
                                                    DataSourceID="Clientes" PageSize="15"
                                                    AllowSorting="True"
                                                    HorizontalAlign="Center" Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>      
                                                        <asp:CommandField ShowEditButton="True" >
                                                            <ItemStyle Height="1px" Width="60px" HorizontalAlign="Left" Wrap="True" />
                                                            </asp:CommandField>
                                                        <asp:BoundField DataField="id_cliente" HeaderText="ID" Visible="false" SortExpression="id_cliente" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre"  >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True"  />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" SortExpression="apellidos" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="empresa" HeaderText="Empresa" SortExpression="empresa" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="calle" HeaderText="Calle" SortExpression="calle" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="noexterior" HeaderText="No Exterior" SortExpression="noexterior" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                         <asp:BoundField DataField="colonia" HeaderText="Colonia" SortExpression="colonia">
                                                             <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField >
                                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" SortExpression="ciudad" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estadoprovincia" HeaderText="Estado" SortExpression="estadoprovincia" >                                                       
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="telefono" HeaderText="Telefono" SortExpression="telefono" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                                
                                                        <asp:BoundField DataField="codigo_postal" HeaderText="CP" SortExpression="codigo_postal" >                                                       
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                                               
                                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>    
                                                        <asp:BoundField DataField="NIT" HeaderText="Cuenta" SortExpression="NIT" >
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
                <asp:SqlDataSource ID="Clientes" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_clientes" SelectCommandType="StoredProcedure"  
                    UpdateCommand="sp_Select_Update_Clientes" UpdateCommandType="StoredProcedure">
                    <SelectParameters>
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="id_cliente" Type="Int32" />
                        <asp:Parameter Name="nombre" Type="String" />
                        <asp:Parameter Name="empresa" Type="String" />
                        <asp:Parameter Name="apellidos" Type="String" />
                        <asp:Parameter Name="calle" Type="String" />
                        <asp:Parameter Name="noexterior" Type="Int32" />
                        <asp:Parameter Name="colonia" Type="String" />
                        <asp:Parameter Name="ciudad" Type="String" />
                        <asp:Parameter Name="municipio" Type="String" />
                        <asp:Parameter Name="estadoprovincia" Type="String" />
                        <asp:Parameter Name="telefono" Type="String" />
                        <asp:Parameter Name="codigo_postal" Type="String" />
                        <asp:Parameter Name="email" Type="String" />
                        <asp:Parameter Name="NIT" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
