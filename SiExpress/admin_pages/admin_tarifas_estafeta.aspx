<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin_tarifas_estafeta.aspx.vb" MasterPageFile="~/Site.master" Inherits="admin_pages_admin_tarifas_estafeta" %>

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
                                        <div class="form-group">
                                            <td style="width: 97px; text-align: left;">Agente</td>
                                            <td colspan="2">
                                                <asp:DropDownList class="form-control" Height="30px" ID="DropDownAgente" runat="server" Width="357px" AutoPostBack="True" DataSourceID="DSAgentes" DataTextField="agente" DataValueField="id_agencia">
                                                </asp:DropDownList>                                                       
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList class="form-control" Height="30px" ID="DropDownZonas" runat="server" Width="357px" AutoPostBack="True" DataSourceID="DSZonas" DataTextField="nombre" DataValueField="id_zona">
                                                </asp:DropDownList>                                                       
                                            </td>
                                        </div>
                                    </tr>                                    
                                    <tr>
                                        <td colspan="9" style="text-align: center">
                                            <div class="table-responsive" style="padding-bottom: 15px">                                                
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" Height="25px" Width="1215px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="ID_TARIFAS_AGENCIA_ESTAFETA"
                                                    DataSourceID="Tarifas" PageSize="15"
                                                    AllowSorting="True"
                                                    HorizontalAlign="Center" Font-Size="Small">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>      
                                                        <asp:CommandField ShowEditButton="True" >
                                                            <ItemStyle Height="1px" Width="60px" HorizontalAlign="Left" Wrap="True" />
                                                            </asp:CommandField>
                                                        <asp:BoundField DataField="ID_TARIFAS_AGENCIA_ESTAFETA" HeaderText="ID" Visible="false" SortExpression="SubProducto" >
                                                            <ItemStyle Height="1px" Width="180px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ID_ZONA" HeaderText="Zona" SortExpression="ID_ZONA" ReadOnly="True" >
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True"  />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ID_CUENTA" HeaderText="Cuenta" SortExpression="ID_CUENTA" ReadOnly="True">
                                                            <ItemStyle Height="1px" Width="50px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PESO_LIMITE_INFERIOR" HeaderText="Peso Limite Inferior" SortExpression="PESO_LIMITE_INFERIOR" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PESO_LIMITE_SUPERIOR" HeaderText="Peso Limite Superior" SortExpression="PESO_LIMITE_SUPERIOR" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PRECIO" HeaderText="Precio Terrestre" SortExpression="PRECIO" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                        
                                                         <asp:CheckBoxField DataField="PRECIO_POR_KILO" HeaderText="Precio Por Kg" SortExpression="PRECIO_POR_KILO">
                                                             <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:CheckBoxField>
                                                        <asp:BoundField DataField="PRECIO_KILO_ADICIONAL" HeaderText="Precio Kg Adicional" SortExpression="PRECIO_KILO_ADICIONAL" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PRECIO_DIA_SIGUIENTE" HeaderText="Precio Dia Sig" SortExpression="PRECIO_DIA_SIGUIENTE" >                                                       
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="PRECIO_POR_KILO_DIA_SIGUIENTE" HeaderText="Precio Kg Dia Sig" SortExpression="PRECIO_POR_KILO_DIA_SIGUIENTE">
                                                             <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:CheckBoxField>                                                                                                      
                                                        <asp:BoundField DataField="PRECIO_KILO_ADICIONAL_DIA_SIGUIENTE" HeaderText="Precio Kg Adicional Dia Sig" SortExpression="PRECIO_KILO_ADICIONAL_DIA_SIGUIENTE" >
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>                                                                                                                
                                                        <asp:BoundField DataField="PRECIO_GOMBAR" HeaderText="Precio Gombar" SortExpression="PRECIO_GOMBAR" >                                                       
                                                            <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="PRECIO_POR_KILO_GOMBAR" HeaderText="Precio Kg Gombar" SortExpression="PRECIO_POR_KILO_GOMBAR">
                                                             <ItemStyle Height="1px" Width="70px" HorizontalAlign="Left" Wrap="True" />
                                                        </asp:CheckBoxField>                                                                                                      
                                                        <asp:BoundField DataField="PRECIO_KILO_ADICIONAL_GOMBAR" HeaderText="Precio Kg Adicional Gombar" SortExpression="PRECIO_KILO_ADICIONAL_GOMBAR" >
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
                <asp:SqlDataSource ID="DSAgentes" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_Select_Agentes_por_usuarios"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="DSZonas" runat="server"
                    ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_SelectZonas"
                    SelectCommandType="StoredProcedure">                    
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Tarifas" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                    SelectCommand="sp_select_tarifas_agencia_estafeta" SelectCommandType="StoredProcedure"  UpdateCommand="sp_update_tarifas_agencia_estafeta" UpdateCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownAgente" DefaultValue="" Name="id_agencia" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="DropDownZonas" DefaultValue="" Name="id_zona" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ID_TARIFAS_AGENCIA_ESTAFETA" Type="Int32" />
                        <asp:Parameter Name="PESO_LIMITE_INFERIOR" Type="Double" />
                        <asp:Parameter Name="PESO_LIMITE_SUPERIOR" Type="Double" />
                        <asp:Parameter Name="PRECIO" Type="Decimal" />
                        <asp:Parameter Name="PRECIO_POR_KILO" Type="boolean" />
                        <asp:Parameter Name="PRECIO_KILO_ADICIONAL" Type="Decimal" />
                        <asp:Parameter Name="PRECIO_DIA_SIGUIENTE" Type="Decimal" />
                        <asp:Parameter Name="PRECIO_POR_KILO_DIA_SIGUIENTE" Type="boolean" />
                        <asp:Parameter Name="PRECIO_KILO_ADICIONAL_DIA_SIGUIENTE" Type="Decimal" />
                        <asp:Parameter Name="PRECIO_GOMBAR" Type="Decimal" />
                        <asp:Parameter Name="PRECIO_POR_KILO_GOMBAR" Type="boolean" />
                        <asp:Parameter Name="PRECIO_KILO_ADICIONAL_GOMBAR" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
