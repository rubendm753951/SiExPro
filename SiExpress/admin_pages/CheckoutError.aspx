<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master"  CodeFile="CheckoutError.aspx.vb" Inherits="admin_pages_CheckoutError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 726px;"> 
        <fieldset>
            <!-- Form Name -->
            <legend>Error al confirmar compra</legend>
            <div class="alert alert-info" role="alert">
                <div class="form-group">
                    <label for="" class="col-lg-3 control-label">Código</label>
                    <div class="col-lg-9">
                        <input type="text" name="" id="" class="form-control" placeholder="Error" value="<%=Request.QueryString.Get("ErrorCode")%>"/>
                    </div>
                </div> 
                <div class="form-group">
                    <label for="" class="col-lg-3 control-label">Descripción</label>
                    <div class="col-lg-9">
                        <input type="text" name="" id="Text1" class="form-control" placeholder="Error" value="<%=Request.QueryString.Get("Desc")%>"/>
                    </div>
                </div> 
                <div class="form-group">
                    <label for="" class="col-lg-3 control-label"></label>
                    <div class="col-lg-9">
                        <input type="text" name="" id="Text2" class="form-control" placeholder="Error" value="<%=Request.QueryString.Get("Desc2")%>"/>
                    </div>
                </div> 
            </div>
        </fieldset>
    </div>
</asp:Content>