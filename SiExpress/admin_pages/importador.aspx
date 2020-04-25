<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="importador.aspx.vb" Inherits="importador" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div style="height: 483px; position: absolute; top: 183px; left: 221px; width: 1078px; text-align: center;" >
<asp:UpdatePanel ID="UpdatePanel1" runat="server">  
<ContentTemplate>

    <table style="width:100%;">
        <tr>
            <td style="width: 184px; height: 49px">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate><img alt="" src="../Images/bigrotation2.gif" /><br/>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
            <td style="height: 49px">
                <asp:Button CssClass="btn btn-outline btn-success btn-sm"  ID="Button1" runat="server" Height="33px" Text="Importar Datos" 
                    Width="108px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox CssClass="form-control" Height="142px"   ID="TextBox1" runat="server"  style="font-size: xx-small" TextMode="MultiLine" Width="902px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView class="table table-striped table-bordered table-hover"  ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataSourceID="DSDatosImportar" 
                    
                    DataKeyNames="id" 
                    Width="1022px">
                    <RowStyle Font-Size="Small" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                            ReadOnly="True" SortExpression="id" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo1" HeaderText="campo1" 
                            SortExpression="campo1" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo2" HeaderText="campo2" 
                            SortExpression="campo2" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo3" HeaderText="campo3" 
                            SortExpression="campo3" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo4" HeaderText="campo4" 
                            SortExpression="campo4" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo5" HeaderText="campo5" 
                            SortExpression="campo5" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo6" HeaderText="campo6" 
                            SortExpression="campo6" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo7" HeaderText="campo7" 
                            SortExpression="campo7" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo8" HeaderText="campo8" 
                            SortExpression="campo8" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo9" HeaderText="campo9" 
                            SortExpression="campo9" >
                        </asp:BoundField>
                        <asp:BoundField DataField="campo10" HeaderText="campo10" 
                            SortExpression="campo10" >
                        </asp:BoundField>
                    </Columns> <PagerStyle CssClass="pagination-ys"></PagerStyle>
                    <HeaderStyle Font-Bold="False" Font-Size="Small" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <asp:SqlDataSource ID="DSDatosImportar" runat="server" 
        ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" 
        DeleteCommand="sp_delete_tmp_importar" DeleteCommandType="StoredProcedure" 
        SelectCommand="sp_select_tmp_importar" SelectCommandType="StoredProcedure" 
        UpdateCommand="sp_update_tmp_importar" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
           <asp:Parameter Name="id" Type="Int32" />
            <asp:Parameter Name="campo1" Type="String" />
            <asp:Parameter Name="campo2" Type="String" />
            <asp:Parameter Name="campo3" Type="String" />
            <asp:Parameter Name="campo4" Type="String" />
            <asp:Parameter Name="campo5" Type="String" />
            <asp:Parameter Name="campo6" Type="String" />
            <asp:Parameter Name="campo7" Type="String" />
            <asp:Parameter Name="campo8" Type="String" />
            <asp:Parameter Name="campo9" Type="String" />
            <asp:Parameter Name="campo10" Type="String" />
         </UpdateParameters>
    </asp:SqlDataSource>
</ContentTemplate>
</asp:UpdatePanel>  
</div>
</asp:Content>

