<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/Site.master"  CodeFile="CheckoutConfirmation.aspx.vb" Inherits="admin_pages_CheckoutConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 726px;">
        <fieldset>
            <!-- Form Name -->
            <legend>El saldo se ha abonado satisfactoriamente</legend>
            <div class="alert alert-info" role="alert"> 
                <div class="form-group" >
                    <label class="col-lg-4 control-label" for="">ID de la Transacción:</label>
                    <div class="col-lg-8"> 
                        <strong><asp:Label ID="TransactionId" CssClass="control-label" runat="server"></asp:Label></strong>
                    </div>
                </div>
            </div>
            <h3></h3> 
            <p></p>
            <h3><div class="alert alert-info" role="alert">Gracias!!!</div></h3>
            <p></p>
            <hr />
        </fieldset>
    </div>
</asp:Content>
