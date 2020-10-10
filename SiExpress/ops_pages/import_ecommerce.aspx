<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="import_ecommerce.aspx.vb" EnableViewState="true" Inherits="ops_pages_import_ecommerce" Title="Importacion ECommerce" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                                            <b>Buscar envios Estafeta</b></td>
                                    </tr>
                                    <tr>
                                        <div class="form-group">
                                            <td style="width: 97px; text-align: left;">Agente</td>
                                            <td colspan="2">                                                
                                                <td>
                                                    <asp:Button class="btn btn-outline btn-success btn-sm" ID="btnSearch" runat="server" Text="Refrescar" Width="97px" />
                                                </td>
                                        </div>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center"><b>Seleccione las ordenes a importar</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: center">
                                            <div class="table-responsive" style="padding-bottom: 15px">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="Order_ID"
                                                    DataSourceID="EnviosAExportar" PageSize="10"
                                                    HorizontalAlign="Center"                                                                                                   
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkOrden" runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Order_ID" HeaderText="Orden" ReadOnly="True" SortExpression="Order_ID" >
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Customer_id" HeaderText="Cliente" SortExpression="Customer_id" >
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pais" HeaderText="Pais" SortExpression="Pais" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" SortExpression="Ciudad" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Empresa" HeaderText="Empresa" SortExpression="Empresa" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Colonia" HeaderText="Colonia" SortExpression="Colonia" >                                                       
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="Total_Impuestos" HeaderText="Total" SortExpression="Total_Impuestos" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                                
                                                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" >                                                       
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CP" HeaderText="CP" SortExpression="CP" >                                                       
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mensaje" HeaderText="Mensaje" SortExpression="mensaje" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                                </asp:GridView>                                                    
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnImportar" runat="server" Text="Importar"></asp:Button><br />                                
                                <br />
                            </div>
                        </div>
                    </div>
                </div>                
                <asp:SqlDataSource ID="EnviosAExportar" runat="server" ConnectionString="<%$ ConnectionStrings:GombarECommerceDB_ConnectionString %>"
                    SelectCommand="sp_select_ordenes_importar" SelectCommandType="StoredProcedure">
                    <SelectParameters>                        
                    </SelectParameters>
                    <SelectParameters>
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
