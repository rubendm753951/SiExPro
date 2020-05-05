<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="EnviosPendientes.aspx.vb" Inherits="ops_pages_EnviosPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 177px; left: 248px; height: 771px; width: 943px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 932px; left: 6px; position: absolute;">
                                    <tr>
                                        <td>
                                            <h3 style="width: 802px;"><span class="label label-info" style="width: 802px;">Existen envios que necesitan una respuesta.</span></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 876px; height: 26px;" colspan="2" valign="top">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView3" runat="server" PageSize="15" AllowPaging="True"
                                                AutoGenerateColumns="False" DataSourceID="Comentarios" HorizontalAlign="Center"
                                                OnSelectedIndexChanged="GridView3_SelectedIndexChanged" Width="802px" Font-Size="X-Small" DataKeyNames="idComentario">
                                                <Columns>
                                                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="idComentario" Visible="False" HeaderText="idComentario" SortExpression="idComentario" />
                                                    <asp:BoundField DataField="idEnvio" Visible="True" HeaderText="idEnvio" SortExpression="idEnvio" ReadOnly="True"  />
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="Fec" />
                                                    <asp:BoundField DataField="idUsrCom" Visible="False" HeaderText="idUsrCom" SortExpression="idUsrCom" />
                                                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario" InsertVisible="False" ReadOnly="True" />
                                                    <asp:BoundField DataField="idUsrRes" Visible="False" HeaderText="idUsrRes" SortExpression="idUsrRes" />
                                                    <asp:BoundField DataField="Respuesta" HeaderText="Respuesta" SortExpression="Respuesta" ReadOnly="False" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="Comentarios" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_envio_pendiente_comentario" SelectCommandType="StoredProcedure" UpdateCommand="sp_update_envio_comentario" UpdateCommandType="StoredProcedure"
                    DeleteCommand="sp_delete_envio_comentario" DeleteCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="idComentario" Type="Int32" />
                        <asp:SessionParameter Name="idUsuario" SessionField="id_usuario" Type="Int32" />
                        <asp:Parameter Name="Comentario" Type="String" />
                        <asp:Parameter Name="Respuesta" Type="String" />
                    </UpdateParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="idComentario" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
