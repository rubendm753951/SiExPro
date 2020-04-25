<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="admin_adeudo_empresa.aspx.vb" Inherits="admin_pages_admin_adeudo_empresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    
    <asp:updateprogress id="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <span style="font-size: 10pt">&nbsp;Actualizando.. Por favor espere </span>
        </ProgressTemplate>
    </asp:updateprogress>
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
                            <table style="WIDTH: 600px" >
                                <tbody>
                                    <tr>
                                        <td style="WIDTH: 30px; HEIGHT: 25px"><span style="FONT-SIZE: 10pt">Total</span>&nbsp;</td>
                                        <td style="WIDTH: 150px; HEIGHT: 25px">
                                            <asp:TextBox CssClass="form-control" Height="27px"   ID="txtTotal" runat="server" Width="168px" Font-Size="Small"></asp:TextBox>
                                            
                                        </td>
                                        <td style="WIDTH: 150px; HEIGHT: 25px">
                                            <div class="col-lg-2 text-right">                    
                                                <asp:ImageButton ID="CheckoutImageBtn" CausesValidation="False" UseSubmitBehavior="false"  formnovalidate="formnovalidate" type="submit"   runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                                                    Width="145" AlternateText="Check out with PayPal"
                                                    OnClick="CheckoutImageBtn_Click"
                                                    BackColor="Transparent" BorderWidth="0"  />
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>       
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                                <asp:GridView class="table table-striped table-bordered table-hover"  ID="GridView1" runat="server" Font-Size="10pt" DataSourceID="adeudos" AllowPaging="True" DataKeyNames="idFactura" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="idFactura" HeaderText="Id Factura" Visible="False" ReadOnly="True" SortExpression="idFactura"/>
                                        <asp:BoundField DataField="id_usuario" HeaderText="id_usuario" Visible="False"  ReadOnly="True" SortExpression="id_usuario"/>
                                        <asp:BoundField DataField="id_empresa" HeaderText="id_empresa" Visible="False"  ReadOnly="True" SortExpression="id_empresa"/>
                                        <asp:BoundField DataField="Referencia" HeaderText="Referencia" Visible="False" ReadOnly="True" SortExpression="Referencia"/>
                                        <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" Visible="False" ReadOnly="True" SortExpression="Comentarios"/>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" Visible="False" SortExpression="Fecha"/>
                                        <asp:BoundField DataField="Anio" HeaderText="Año" SortExpression="Anio"/>
                                        <asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="Mes"/>
                                        <asp:BoundField DataField="Mensualidad" HeaderText="Mensualidad" SortExpression="Mensualidad"/>
                                        <asp:BoundField DataField="TarifaPorEnvio" HeaderText="Tarifa" SortExpression="TarifaPorEnvio"/>
                                        <asp:BoundField DataField="NumeroEnvios" HeaderText="Envios" SortExpression="NumeroEnvios"/>
                                        <asp:BoundField DataField="Facturado" HeaderText="Facturado" Visible="False" SortExpression="Facturado"/>
                                        <asp:BoundField DataField="Deposito" HeaderText="Depositos" SortExpression="Deposito"/>
                                        <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="tb" runat="server" Text='<%# String.Format("{0:c}", Eval("Mensualidad") - Eval("Deposito") + (Eval("TarifaPorEnvio") * Eval("NumeroEnvios"))) %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns> 
                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                    <HeaderStyle BackColor="Silver" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>     
            
            <asp:SqlDataSource ID="adeudos" runat="server"
                SelectCommandType="StoredProcedure" SelectCommand="sp_select_adeudo_empresa" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                <SelectParameters>
                    <asp:sessionparameter name="idEmpresa" sessionfield="id_empresa" type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:updatepanel>
    </div>
</asp:Content>
