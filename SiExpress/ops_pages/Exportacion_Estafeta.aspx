<%@ Page Language="VB"  MasterPageFile="~/Site.master"  AutoEventWireup="false" CodeFile="Exportacion_Estafeta.aspx.vb" Inherits="ops_pages_Exportacion_Estafeta" Title="Exportacion Estafeta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="position: absolute; top: 141px; left: 290px; height: 327px; width: 395px;">
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
                                        <td colspan="3" style="text-align: left">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownAgente" runat="server" Width="387px"
                                                DataSourceID="DSAgentes" DataTextField="agente" DataValueField="id_agencia">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>    
                                    <tr>                                        
                                        <td style="width: 74px; text-align: left;">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnExport" runat="server" Text="Exportar" Width="97px" />
                                        </td>
                                    </tr>
                                </table>
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

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>