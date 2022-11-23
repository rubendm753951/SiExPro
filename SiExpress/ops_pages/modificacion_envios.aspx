<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="modificacion_envios.aspx.vb" EnableViewState="true" Inherits="ops_pages_modificacion_envios" Title="Modificacion Envios" %>

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
                                            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                                TargetControlID="Button8" PopupControlID="Internal_error" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="Internal_error" runat="server" Height="91px" Width="400px" Style="text-align: left">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="12pt" ForeColor="Black" Height="128px" Width="392px"
                                                    BackColor="White" BorderStyle="Double"></asp:Label>
                                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Text="Aceptar" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="16pt" Height="30px" Text="Modificación de Envios" Width="270px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><br /></td>
                                    </tr>
                                </table>
                                <table style="width: 732px; left: 6px;">
                                    <tr>
                                        <td style="width: 200px; text-align: right">Número de Guia</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEnvio" runat="server" TabIndex="1"></asp:TextBox>
                                        </td>                                        
                                    </tr>  
                                    <tr>
                                        <td style="width: 200px; text-align: right">Total Envio</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtTotalEnvio" Width="100px" runat="server" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="width: 200px; text-align: right">Proveedor</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:DropDownList class="form-control" Height="30px" ID="DropDownProveedores" runat="server" Width="357px" AutoPostBack="True" DataSourceID="dsProveedores" DataTextField="proveedor" DataValueField="id_proveedor">
                                                </asp:DropDownList>    
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="width: 200px; text-align: right">Referencia Fedex</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:textbox cssclass="form-control" height="27px" id="txtReferencia" runat="server" width="233px"></asp:textbox>
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="width: 200px; text-align: right">Comentarios</td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px">
                                            <asp:TextBox CssClass="form-control" Height="90px" ID="txtComentarios" runat="server" TabIndex="1" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </td>                                                                                
                                    </tr>                                                                          
                                    <tr>
                                        <td style="width: 200px; text-align: right"></td>
                                        <td style="width: 294px; padding-left: 10px; padding-right: 10px; margin-top: 30px; position:absolute; display: flex;" >
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnUpdate" runat="server" Text="Actualizar" Width="84px" TabIndex="2" />
                                        </td>                                        
                                    </tr>
                                </table>                                
                            </div>
                        </div>
                        <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button8" runat="server" Text="Button" Visible="True" />
                    </div>
                </div>    
                
                <asp:SqlDataSource ID="dsProveedores" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_SelectProveedoresNoAutomatizados"
                    SelectCommandType="StoredProcedure">                    
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
