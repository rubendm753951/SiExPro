<%@ Page Language="VB" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="Cotizador.aspx.vb" Inherits="Cotizador" title="Cotizador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
    <div style="width:36%; margin: 0 auto; text-align:left; left: 262px; position: absolute; top: 150px;" >
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 315px;" >
        <tr>
            <td colspan="2" style="height: 46px; text-align: center">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate><img alt="" src="Images/bigrotation2.gif" /><br/>
        </ProgressTemplate>
    </asp:UpdateProgress>
            </td>
        </tr>
        <table border="1" style="border: 4px solid #FFFFFF; font-family:arial; font-size:12px;" width="750">
            <tbody>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="right">
                            Tipo de Envio:</div>
                    </td>
                    <td colspan="2" style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF; " width="400">
                        <label>
                        <div id="area1">
                            <asp:DropDownList CssClass="form-control" Height="30px"  id="DropDownOrigen" runat="server" Width="367px" Font-Bold="False" AutoPostBack="True">
                                <asp:ListItem Value="52" >Nacional</asp:ListItem>
                                <asp:ListItem Value="200" >Internacional</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        </label>
                    </td>
                    <td rowspan="3" style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; " width="158">
                        <div id="area5">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="right">
                            Destino Estado:</div>
                    </td>
                    <td colspan="2" style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF; " width="400">
                        <label>
                        <div id="area2">
                            <asp:DropDownList CssClass="form-control" Height="30px"  id="DropDownDestino" runat="server" Width="367px" Font-Bold="False" DataSourceID="SqlDSEstados" DataValueField="id_estado" DataTextField="estado" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="right">
                            Destino Ciudad:</div>
                    </td>
                    <td colspan="2" style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF; " width="400">
                        <label>
                        <div id="Div1">
                            <asp:DropDownList CssClass="form-control" Height="30px"  id="DropDownCiudad" runat="server" Width="367px" Font-Bold="False" DataSourceID="SqlDataCiudades" DataValueField="id_ciudad" DataTextField="ciudad" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="right">
                            Tipo:</div>
                    </td>
                    <td colspan="2" style=" font-size: small; border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF; " width="400">
                        <asp:RadioButton ID="rbPaquete" GroupName="myg" Text="Paquete"  runat="server" />
                        <asp:RadioButton ID="rbSobre" GroupName="myg" Text="Sobre"  runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="right">
                            &nbsp;</div>
                    </td>
                    <td colspan="2" style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <div align="left">Medidas y peso por paquete</div>
                    </td>
                </tr>
                <tr>
                    <td style=" border-right: 5px solid #FFFFFF; border-bottom: 10px solid #FFFFFF; height: 39px; border-left-style: none; border-left-color: inherit; border-left-width: 0px; border-top-style: none; border-top-color: inherit; border-top-width: 5px;">
                        <div align="right">
                            Peso (max. 40 kg):</div>
                    </td>
                    <td style=" border-bottom: 10px solid #FFFFFF; height: 39px; border-left-style: none; border-left-color: inherit; border-left-width: 0px; border-right-style: none; border-right-color: inherit; border-right-width: 0px; border-top-style: none; border-top-color: inherit; border-top-width: 5px;" width="159">
                        <label>
                            <asp:TextBox CssClass="form-control" Height="27px"   id="txtPeso" runat="server" Width="62px" >0</asp:TextBox>&nbsp; kg 
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <div align="right">
                            Largo:</div>
                    </td>
                    <td style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF; ">
                        <label>
                            <asp:TextBox CssClass="form-control" Height="27px"   id="txtLargo" runat="server" Width="62px" >0</asp:TextBox>&nbsp; cm 
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <div align="right">
                            Ancho:</div>
                    </td>
                    <td style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <label>
                        <asp:TextBox CssClass="form-control" Height="27px"   id="txtAncho" runat="server" Width="62px" >0</asp:TextBox>&nbsp; cm</label></td>
                </tr>
                <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <div align="right">
                            Alto:
                        </div>
                    </td>
                    <td style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <label>
                        <asp:TextBox CssClass="form-control" Height="27px"   id="txtAlto" runat="server" Width="62px" >0</asp:TextBox>&nbsp; cm </label></td>
                </tr>
                 <tr>
                    <td style=" border-left: 0px; border-right: 5px solid #FFFFFF; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <div align="right">
                            Costo:
                        </div>
                    </td>
                    <td style=" border-left: 0px; border-right: 0px; border-top: 5px; border-bottom: 10px solid #FFFFFF;">
                        <label>
                        <asp:TextBox CssClass="form-control" Height="27px"   id="txtCosto" Enabled="False" runat="server" Width="62px" >0</asp:TextBox></label></td>
                </tr>
                <tr>
                    <td colspan="4" style=" border-left: 0px; border-right: 0px; border-top: 0px; border-bottom:0px;">
                        <br>
                        <center>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  id="btnCotizar" runat="server" Text=" Cotizar " Width="108px"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm"  id="btLimpiar" runat="server" Text=" Limpiar " Width="108px"></asp:Button>
                        </center>
                        <br>
                        <br>
                        <br>
                        <asp:Label ID="lblMensaje" runat="server" Font-Size="Medium" style="font-size: 9pt; color: red" Text=""></asp:Label>
                        <br>
                    </td>
                </tr>
            </tbody>
        </table>
        <tr>
        </tr>
    </table>
    <asp:SqlDataSource id="SqlDSEstados" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_estados" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownOrigen" Name="id_pais" PropertyName="SelectedValue" Type="Int32" DefaultValue="52" />
        </SelectParameters>
    </asp:SqlDataSource> 
    <asp:SqlDataSource id="SqlDataCiudades" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_ciudades" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownDestino" Name="id_estado" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource> 

</ContentTemplate> 
</asp:UpdatePanel>
</div>
</asp:Content>
