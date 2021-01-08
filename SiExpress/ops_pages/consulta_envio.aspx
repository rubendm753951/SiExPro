<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="consulta_envio.aspx.vb" Inherits="ops_pages_consulta_envio" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 200px; left: 220px; width: 626px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="page-wrapper">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <table style="width: 532px; left: 6px;">
                                    <tr>
                                        <td style="width: 165px;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img src="../Images/bigrotation2.gif" style="width: 36px" alt="Rotation" /></ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 532px; left: 6px;">
                                    <tr>
                                        <td style="width: 110px; height: 47px;">Número de Envío: </td>
                                        <td style="width: 189px; height: 47px;">
                                            <asp:TextBox CssClass="form-control" Height="27px" Width="180px" ID="TextBox1" runat="server" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td style="width: 189px; height: 47px;">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button1" runat="server" TabIndex="2" Text="Consultar" /></td>
                                    </tr>
                                </table>
                                <table style="width: 532px; left: 6px;">
                                    <tr>
                                        <td style="height: 26px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Enabled="False" Text="Regenerar Guía" />
                                            <asp:CheckBox ID="CheckBox1" Text="Habilitar" runat="server" AutoPostBack="True" />
                                        </td>
                                        <td style="height: 26px" colspan="2">
                                            <asp:Label ID="Label1" runat="server" Text="Insertar Comentario:"></asp:Label>
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtComentario" runat="server" Wrap="False" Width="404px"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnInsertar" runat="server" TabIndex="2" Text="Insertar" />
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtIdAgente" runat="server" Visible="False" Wrap="False" Width="16px"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 165px; height: 26px">
                                            <asp:TextBox CssClass="form-control" Height="174px" ID="TextBox2" runat="server" TextMode="MultiLine" Width="309px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>                                
                                <table style="width: 832px; left: 6px;">
                                    <tr>
                                        <td style="width: 600px; height: 26px;" colspan="2" valign="top">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView3" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" DataSourceID="Comentarios" HorizontalAlign="Center"
                                                OnSelectedIndexChanged="GridView3_SelectedIndexChanged" Width="602px" Font-Size="X-Small" DataKeyNames="idComentario">
                                                <Columns>
                                                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="idComentario" Visible="False" HeaderText="idComentario" SortExpression="idComentario" />
                                                    <asp:BoundField DataField="idEnvio" Visible="False" HeaderText="idEnvio" SortExpression="idEnvio" />
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
                                <table style="width: 832px; left: 6px;">
                                    <tr>
                                        <td style="height: 26px; text-align: center;"><b>Rastreo</b></td>
                                    </tr>
                                    <tr>
                                        <td  style="height: 26px; text-align: center; width: 800px">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" DataSourceID="Seguimiento" HorizontalAlign="Center"
                                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="800px">
                                                <Columns>
                                                    <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                                                    <asp:BoundField DataField="id_envio" HeaderText="id_envio" SortExpression="id_envio" />
                                                    <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" />
                                                    <asp:BoundField DataField="ciudad" HeaderText="ciudad" SortExpression="ciudad" />
                                                    <asp:BoundField DataField="oficina" HeaderText="oficina" SortExpression="oficina" />
                                                    <asp:BoundField DataField="usuario" HeaderText="usuario" SortExpression="usuario" />
                                                    <asp:BoundField DataField="observaciones" HeaderText="observaciones" SortExpression="observaciones" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 26px; text-align: center;"><b>Seguimiento a Exportación</b></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 26px; text-align: center">
                                            <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView2" runat="server" Height="25px" Width="800px" AllowPaging="True"
                                                AutoGenerateColumns="False"
                                                PageSize="10"
                                                HorizontalAlign="Center"
                                                Font-Size="Small">
                                                <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:BoundField DataField="EventTime" HeaderText="Fecha" SortExpression="EventTime">
                                                        <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EventDescription" HeaderText="Descripcion" SortExpression="EventDescription">
                                                        <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EventPlace" HeaderText="Lugar" SortExpression="EventPlace">
                                                        <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EventException" HeaderText="Mensaje" SortExpression="EventException">
                                                        <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
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
                <asp:SqlDataSource ID="Seguimiento" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_seguimiento" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBox1" Name="id_envio" PropertyName="Text" Type="Int32" />
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Comentarios" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_envio_comentario" SelectCommandType="StoredProcedure" UpdateCommand="sp_update_envio_comentario" UpdateCommandType="StoredProcedure"
                    DeleteCommand="sp_delete_envio_comentario" DeleteCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBox1" Name="idEnvio" PropertyName="Text" Type="Int32" />
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

