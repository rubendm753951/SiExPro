<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeFile="CheckoutReview.aspx.vb" Inherits="admin_pages_CheckoutReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 726px;">
        <fieldset>
            <!-- Form Name -->
            <legend>Datos de la cuenta de pago</legend>
            <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="false" GridLines="None" CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
                <Fields>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <strong>  <asp:Label ID="FirstName"  runat="server" Text='<%#: Eval("FirstName") %>'></asp:Label></strong>
                            <strong> <asp:Label ID="LastName"  runat="server" Text='<%#: Eval("LastName") %>'></asp:Label></strong>
                            <br />
                            <asp:Label ID="Address"  runat="server" Text='<%#: Eval("Address") %>'></asp:Label>
                            <br />
                            <asp:Label ID="City" runat="server" Text='<%#: Eval("City") %>'></asp:Label>
                            <asp:Label ID="State" runat="server" Text='<%#: Eval("State") %>'></asp:Label>
                            <asp:Label ID="PostalCode" runat="server" Text='<%#: Eval("PostalCode") %>'></asp:Label>
                            <p></p>
                            <h3><strong>   Total: </strong><asp:Label ID="Total" runat="server" class="label label-danger" Text='<%#: Eval("Total", "{0:C}") %>'></asp:Label></h3>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
            <p></p>
            <hr />
            <asp:Button ID="CheckoutConfirm" class="btn btn-sm btn-success" runat="server" Text="Confirmar pago" OnClick="CheckoutConfirm_Click" />
        </fieldset>
    </div>
</asp:Content>