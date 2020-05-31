<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Exportacion_Estafeta.aspx.vb" EnableViewState="true" Inherits="ops_pages_Exportacion_Estafeta" Title="Exportacion Estafeta" %>

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
                                                <asp:DropDownList class="form-control" Height="30px" ID="DropDownAgente" runat="server" Width="357px"
                                                    DataSourceID="DSAgentes" DataTextField="agente" DataValueField="id_agencia">
                                                </asp:DropDownList>
                                                <td>
                                                    <asp:Button class="btn btn-outline btn-success btn-sm" ID="btnSearch" runat="server" Text="Buscar" Width="97px" />
                                                </td>
                                        </div>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center"><b>Seleccione un los envios a exportar</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: center">
                                            <div class="table-responsive">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="id_envio"
                                                    DataSourceID="EnviosAExportar" PageSize="10"
                                                    HorizontalAlign="Center"                                                                                                   
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkEnvio" runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id_envio" HeaderText="Envio" ReadOnly="True" SortExpression="id_envio" >
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubProducto" HeaderText="Producto" SortExpression="SubProducto" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_remit" HeaderText="Remitente" SortExpression="nombre_remit" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="direccion_remit" HeaderText="Direccion Remitente" SortExpression="direccion_remit" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tel_remit" HeaderText="Telefono Remitente" SortExpression="tel_remit" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cp_remit" HeaderText="CP Remit" SortExpression="cp_remit" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_dest" HeaderText="Destinatario" SortExpression="nombre_dest" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="direccion_dest" HeaderText="Direccion Destinatario" SortExpression="direccion_dest" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tel_dest" HeaderText="Telefono Destinatario" SortExpression="tel_dest" >                                                       
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cp_dest" HeaderText="CP Dest" SortExpression="cp_dest" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                        <asp:TemplateField HeaderText="TipoServicio" SortExpression="TipoServicio">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoServicio" runat="server" Text='<%# Eval("TipoServicio") %>' Visible = "false" />
                                                                <asp:DropDownList ID="DropDownTipoServicio" runat="server"
                                                                    AutoPostBack="true" 
                                                                    ViewStateMode="Enabled"
                                                                    OnSelectedIndexChanged="DropDownTipoServicio_SelectedIndexChanged" >
                                                                </asp:DropDownList>
                                                                <asp:DataList runat="server" ID="dlTipoServicio"></asp:DataList>                                                                
                                                            </ItemTemplate>                                                            
                                                        </asp:TemplateField>                                                        
                                                        <asp:BoundField DataField="mensaje" HeaderText="Mensaje" SortExpression="mensaje" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Ver Etiqueta" SortExpression="Etiqueta">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEtiqueta" runat="server" Text='<%# Eval("Etiqueta") %>' Visible = "false" />                                                                                                                          
                                                            </ItemTemplate>                                                            
                                                        </asp:TemplateField> 

                                                        <%--<asp:hyperlinkfield ID="linkLabel" headertext="Ver Etiqueta"
                                                              datatextfield="id_envio"
                                                              datanavigateurlfields="id_envio"
                                                              datanavigateurlformatstring="~\Reposrt\EstafetaLabel.aspx?id_envio={0}" 
                                                              target="_blank"/>--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                                </asp:GridView>                                                    
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnCotizar" runat="server" Text="Cotizar"></asp:Button>
                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnExport" runat="server" Text="Exportar"></asp:Button><br />
                                
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="DSAgentes" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_Select_Agentes_por_usuarios"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="EnviosAExportar" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_envios_estafeta_exportar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
                        <asp:ControlParameter ControlID="DropDownAgente" DefaultValue="" Name="id_agencia" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <SelectParameters>
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
