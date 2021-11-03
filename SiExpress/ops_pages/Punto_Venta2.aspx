<%@ Page Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Punto_Venta2.aspx.vb" Inherits="Punto_Venta" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" type="text/css" media="screen" href="../css/jquery-ui-1.8.2.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css/ui.multiselect.css" />
    <link rel="Stylesheet" type="text/css" media="screen" href="../css/jquery.fileupload.css" />
    <link rel="Stylesheet" type="text/css" media="screen" href="../css/jquery.fileupload.ui.css" />
    <link rel="Stylesheet" type="text/css" media="screen" href="../css/Punto_Venta2.css" />
    <link type="text/css" href="../Skin/CSS/fontawesome.min.css" rel="stylesheet" />
    <div id="page-wrapper" style="width: 700px; margin-right: 15px">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="position: absolute; top: 14px; left: 49px; width: 239px; height: 43px;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <img alt="" src="../Images/bigrotation2.gif" />Actualizando datos ...<br />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <br />
                            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                TargetControlID="Button8" PopupControlID="Internal_error" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>

                            <asp:Panel ID="Internal_error" runat="server" Height="91px" Width="400px"
                                Style="text-align: left">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="12pt" ForeColor="Black" Height="128px" Width="392px"
                                    BackColor="White" BorderStyle="Double"></asp:Label>
                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button3" runat="server" Text="Aceptar" />
                            </asp:Panel>

                            <div style="position: absolute; top: 145px; left: 31px; height: 175px;">
                                <asp:Panel ID="Panel4" runat="server" Height="173px" Width="600px">
                                    <table style="width: 600px; text-align: center" bgcolor="">
                                        <tbody>
                                            <tr>
                                                <td style="width: 164px; height: 21px"><span style="font-size: 9pt">Agente
                                                </span></td>
                                                <td style="font-size: 9pt; height: 21px; text-align: left" colspan="2">
                                                    <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownAgentes" runat="server" Width="367px" Font-Bold="False" DataSourceID="SqlDSAgentes" OnSelectedIndexChanged="DropDownAgentes_SelectedIndexChanged" DataValueField="id_agencia" DataTextField="agente" AutoPostBack="True"></asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 164px; height: 1px"><span style="font-size: 9pt">Contenido</span></td>
                                                <td style="width: 36px; height: 1px"><span style="font-size: 9pt">Subproducto</span></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 164px; height: 12px">
                                                    <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownContenidos" runat="server" Width="149px" DataSourceID="Contenidos" DataTextField="descripcion" DataValueField="id_contenido"></asp:DropDownList>
                                                </td>
                                                <td style="width: 36px; height: 12px">
                                                    <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownProduct" runat="server" Width="322px" DataSourceID="Tarifas" DataValueField="id_tarifa_agencia" DataTextField="SubProducto" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                <td style="width: 177px; height: 12px">&nbsp;</td>
                                                <td style="width: 177px; height: 12px">&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="width: 600px; text-align: center" bgcolor="">
                                        <tbody>
                                            <tr>
                                                <td style="width: 110px; height: 12px; text-align: left">Guía por caja<asp:CheckBox ID="guia_por_caja" runat="server" Text=" " /></td>
                                                <td style="width: 110px; height: 12px; text-align: left">Valor COD</td>
                                                <td style="width: 120px; height: 12px; text-align: left">Tarifa por Seguro</td>
                                                <td style="width: 120px; height: 12px; text-align: left">Envios Asignados</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 110px; height: 17px">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="TxtCajas" runat="server" Width="100px">1</asp:TextBox>
                                                </td>
                                                <td style="width: 110px; height: 17px; text-align: center">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="TxtSeguro" runat="server" Width="100px">0</asp:TextBox>
                                                </td>
                                                <td style="width: 205px; height: 17px; text-align: left">
                                                    <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownList1" runat="server" Width="160px"
                                                        DataSourceID="Tarifas_Seguro" DataValueField="Valor"
                                                        DataTextField="Descripcion" Enabled="False">
                                                    </asp:DropDownList></td>
                                                <td style="width: 150px; height: 17px;">
                                                    <asp:TextBox CssClass="form-control" Height="27px" Width="140px" ID="EnviosAsignados" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtPromo" runat="server" Width="1px" Visible="False">0</asp:TextBox>
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtAduana" runat="server" Width="1px" Visible="False">0</asp:TextBox>
                                        </tbody>
                                    </table>
                                    <table style="width: 600px; text-align: center">
                                        <tbody>
                                            <tr>
                                                <td style="width: 132px; height: 5px; text-align: left">
                                                    <span style="font-size: 10pt; text-align: left;">Referencia</span></td>
                                                <td style="width: 90px; height: 5px;"><span style="font-size: 9pt">Ancho (cms)</span></td>
                                                <td style="width: 90px; height: 5px;"><span style="font-size: 9pt">Largo (cms)</span></td>
                                                <td style="width: 90px; height: 5px;"><span style="font-size: 9pt">Alto (cms)</span></td>
                                                <td style="width: 90px; height: 5px;">
                                                    <asp:Label ID="lblPeso" runat="server" Font-Size="Medium"
                                                        Style="font-size: 9pt" Text="Peso (Kg)"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 132px; height: 17px; text-align: left">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="TxtRef" runat="server" Width="93px"></asp:TextBox>
                                                </td>
                                                <td style="width: 30px; height: 17px">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="txtLargo" runat="server" Width="96px">0</asp:TextBox></td>
                                                <td style="width: 90px; height: 17px">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="txtAncho" runat="server" Width="104px">0</asp:TextBox></td>
                                                <td style="width: 90px; height: 17px">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="txtAlto" runat="server" Width="92px">0</asp:TextBox></td>
                                                <td style="height: 17px; width: 80px;">
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="txtPeso" runat="server" Width="80px">1</asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 17px; text-align: left">Instrucciones de entrega
                                                    <asp:TextBox CssClass="form-control" Height="27px" ID="TxtInstEntrega" runat="server" Width="472px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                <br />
                                <table style="width: 600px; margin-top: 30px; margin-right: 10px">
                                    <tr style="margin-top: 10px">
                                        <td style="width: 87px; height: 9px; padding-top: 10px">
                                            <strong><span style="font-size: 10pt">Proveedor :</span></strong></td>
                                        <td style="width: 300px; height: 9px; padding-top: 10px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownProveedores" runat="server" AutoPostBack="True"
                                                DataSourceID="Proveedores" DataTextField="proveedor"
                                                DataValueField="id_proveedor">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 9px; padding-top: 10px"></td>
                                    </tr>
                                    </table>
                                <table style="width: 600px; margin-right: 10px;" runat="server" id="contenidosDesc">                                    
                                        <tr style="">
                                            <td style="width: 100px; height: 9px; padding-top: 10px">
                                                <strong><span style="font-size: 10pt">Cantidad :</span></strong></td>
                                            <td style="width: 110px; height: 9px; padding-top: 10px">
                                                <asp:TextBox CssClass="form-control" Height="27px" ID="txtContCant" runat="server" Width="100px"></asp:TextBox></td>
                                            <td style="height: 9px; padding-top: 10px"></td>
                                            <td style="width: 100px; height: 9px; padding-top: 10px">
                                                <strong><span style="font-size: 10pt">Contenido :</span></strong></td>
                                            <td style="width: 300px; height: 9px; padding-top: 10px">
                                                <asp:TextBox CssClass="form-control" Height="27px" ID="txtContCont" runat="server" Width="250px"></asp:TextBox></td>
                                            <td style="height: 9px; padding-top: 10px"></td>
                                        </tr>  
                                    <tr>
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <strong><span style="font-size: 10pt">Tipo de Paquete :</span></strong></td>
                                        <td style="width: 110px; height: 9px; padding-top: 10px">
                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="ddlTiposPaquete" runat="server" AutoPostBack="True"> 
                                                <asp:listitem text="CAJA" value="2"></asp:listitem>
                                                <asp:listitem text="SOBRE" value="1"></asp:listitem>                                                                                                
                                                <asp:listitem text="BOLSA" value="4"></asp:listitem>
                                                <asp:listitem text="PIEZA" value="5"></asp:listitem>                                                
                                                <asp:listitem text="TARIMA" value="11"></asp:listitem>
                                                <asp:listitem text="BULTO" value="27"></asp:listitem>                                                
                                                <asp:listitem text="PALET" value="14"></asp:listitem>
                                                <asp:listitem text="ATADO" value="24"></asp:listitem>
                                                <asp:listitem text="HUACAL" value="32"></asp:listitem>
                                                <asp:listitem text="CUBETA" value="6"></asp:listitem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 600px; margin-right: 10px;  margin-top: 10px; margin-bottom: 10px" runat="server" id="contenidosCampos">
                                    <tr >                                                
                                                <td style="width: 100px; height: 5px;"><span style="font-size: 10pt">Ancho (cms)</span></td>
                                                <td style="width: 100px; height: 5px;"><span style="font-size: 10pt">Largo (cms)</span></td>
                                                <td style="width: 100px; height: 5px;"><span style="font-size: 10pt">Alto (cms)</span></td>
                                                <td style="width: 100px; height: 5px;"><span style="font-size: 10pt">Peso (kg)</span></td>                                                    
                                        <td style="width: 100px; height: 5px;"></td>                                                    
                                            </tr>
                                    <tr>                                        
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtContAncho" runat="server" Width="80px"></asp:TextBox></td>                                        
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtContLargo" runat="server" Width="80px"></asp:TextBox></td>                                        
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtContAlto" runat="server" Width="80px"></asp:TextBox></td>                                        
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtContPeso" runat="server" Width="80px"></asp:TextBox></td>                                        
                                        <td style="width: 100px; height: 9px; padding-top: 10px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnAddCont" runat="server" Width="100px" OnClick="OnAdd" Text="Agregar"></asp:Button></td>
                                    </tr>                                    
                                </table>
                                <table style="width: 600px; margin-right: 10px" runat="server" id="contenidosGrid">
                                    <tr>
                                    <td>
                                        <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Contenido" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                                <asp:BoundField DataField="Contenido" HeaderText="Contenido" />
                                                <asp:BoundField DataField="Ancho" HeaderText="Ancho" />
                                                <asp:BoundField DataField="Largo" HeaderText="Largo" />
                                                <asp:BoundField DataField="Alto" HeaderText="Alto" />
                                                <asp:BoundField DataField="Peso" HeaderText="Peso" />
                                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>                                   
                                        </tr>
                                </table>
                                <table style="width: 600px; margin-right: 10px">
                                    <tr style="margin-top: 10px">
                                        <td style="width: 87px; height: 9px; padding-top: 10px">
                                            <strong><span style="font-size: 10pt">Destinatario :</span></strong></td>
                                        <td style="width: 300px; height: 9px; padding-top: 10px">
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtBuscaDest" runat="server" Width="250px"></asp:TextBox></td>
                                        <td style="height: 9px; padding-top: 10px">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button2" runat="server" Width="62px" Text="Buscar"></asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" valign="top">
                                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                                TargetControlID="Button7" PopupControlID="Destinatario" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="Destinatario" runat="server" HorizontalAlign="Center"
                                                ScrollBars="both" Width="600px">
                                                <asp:GridView class="table table-striped table-bordered table-hover" ID="GridView2" runat="server" Font-Size="8pt" Height="150px"
                                                    DataSourceID="Destinatarios" AutoGenerateColumns="False" AllowPaging="True"
                                                    Width="600px" BackColor="White" CssClass="grids" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:CommandField ShowSelectButton="True">
                                                            <ControlStyle Width="60px" />
                                                            <HeaderStyle Width="60px" />
                                                            <ItemStyle Width="60px" Wrap="False" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="id_destinatario" HeaderText="id_destinatario" SortExpression="id_destinatario">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="20px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Destinatario" ReadOnly="True" SortExpression="Nombre">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="apellidos" HeaderText="apellidos" ReadOnly="True" SortExpression="apellidos">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="empresa" HeaderText="empresa" ReadOnly="True" SortExpression="empresa">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="direccion" HeaderText="direccion" ReadOnly="True" SortExpression="direccion">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ciudad" HeaderText="ciudad" ReadOnly="True" SortExpression="ciudad">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="100px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estadoprovincia" HeaderText="estadoprovincia" ReadOnly="True"
                                                            SortExpression="estadoprovincia">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="20px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="telefono" HeaderText="telefono" ReadOnly="True" SortExpression="telefono">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="50px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email" HeaderText="email" ReadOnly="True" SortExpression="email">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="50px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigo_postal" HeaderText="codigo_postal" ReadOnly="True"
                                                            SortExpression="codigo_postal">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="50px" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pais" HeaderText="pais" ReadOnly="True" SortExpression="pais">
                                                            <ControlStyle Width="20px" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle Width="50px" Wrap="False" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys"></PagerStyle>
                                                    <RowStyle Wrap="False" HorizontalAlign="Left" Width="720px" />
                                                    <SelectedRowStyle Wrap="False" Width="720px" />
                                                    <HeaderStyle BackColor="" HorizontalAlign="Left" Width="720px" Wrap="False" />
                                                </asp:GridView>
                                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="BtnCancel2" runat="server" Text="Cancel" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 118px" valign="top">&nbsp;<asp:Panel ID="Panel3" runat="server" Wrap="False">
                                            <table style="width: 443px; height: 51px;">
                                                <tbody id="tblDest">
                                                    <tr>
                                                        <td style="width: 9px; text-align: left"><span style="font-size: 10pt"><strong>Pais</strong></span></td>
                                                        <td style="text-align: left">
                                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownPais2" runat="server" Width="350px" DataSourceID="Paises2" DataValueField="id_pais" DataTextField="country_code" AutoPostBack="True"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr style="font-weight: bold; font-size: 10pt">
                                                        <td style="height: 3px">Nombre</td>
                                                        <td style="height: 3px">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtNombre2" runat="server" MaxLength="30"
                                                                Width="350px"></asp:TextBox>
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtApellidos2" runat="server" Visible="False" MaxLength="20" Width="1px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-weight: bold; font-size: 10pt">
                                                        <td style="width: 9px; text-align: left">Empresa</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEmpresa2" runat="server" Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-weight: bold; font-size: 10pt">
                                                        <td style="width: 9px; height: 20px; text-align: left">Dirección</td>
                                                        <td style="height: 20px; text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCalle2" runat="server" MaxLength="30" Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt">
                                                        <td style="width: 302px; height: 16px; text-align: left">
                                                            <strong><span style="font-size: 10pt">CP</span></strong>
                                                        </td>
                                                        <td style="width: 320px; height: 27px; display: flex">
                                                            <asp:TextBox OnTextChanged="TxtCP2_TextChanged" runat="server" AutoPostBack="true" CssClass="form-control" Height="27px" ID="TxtCP2" Width="130px"></asp:TextBox>
                                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnActualizar" runat="server" Text="Actualizar" Height="27px" Width="98px"></asp:Button>
                                                        </td>

                                                    </tr>
                                                    <tr style="font-weight: bold; font-size: 10pt">
                                                        <td style="width: 9px; height: 20px; text-align: left">Colonia</td>
                                                        <td style="font-size: 10pt; height: 20px; text-align: left; width: 322px;">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtCol2" runat="server" Width="350px"></asp:TextBox>
                                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="DropDownColonia" runat="server" Width="350px"
                                                                DataValueField="id_asenta_cpcons" DataTextField="d_asenta" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-weight: bold; font-size: 10pt">
                                                        <td style="width: 9px; height: 20px; text-align: left">Municipio</td>
                                                        <td style="height: 20px; text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TxtMpio2" runat="server" Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt">
                                                        <td style="width: 302px; height: 16px; text-align: left">
                                                            <strong><span style="font-size: 10pt">Estado</span></strong></td>
                                                        <td colspan="2" style="height: 16px; text-align: left">
                                                            <asp:DropDownList CssClass="form-control" Height="30px" ID="txtEdo2" runat="server" DataSourceID="Estados2"
                                                                DataTextField="estado" DataValueField="codigo" Width="180px" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 9px; height: 11px; text-align: left"><strong><span style="font-size: 10pt">Ciudad</span></strong></td>
                                                        <td style="height: 11px; text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtCiudad2" runat="server" Width="350px" Wrap="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt">
                                                        <td style="width: 302px; height: 16px; text-align: left">
                                                            <strong><span style="font-size: 10pt">Teléfono</span></strong>
                                                        </td>
                                                        <td colspan="2" style="height: 16px; text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtTelefono2" runat="server" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt">
                                                        <td style="width: 302px; height: 16px; text-align: left">
                                                            <strong><span style="font-size: 10pt">Email</span></strong>
                                                        </td>
                                                        <td colspan="2" style="height: 16px; text-align: left">
                                                            <asp:TextBox CssClass="form-control" Height="27px" ID="txtEmail2" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button5" runat="server" Height="23px" OnClick="Button5_Click" Text="ValidarDomicilio"
                                                Width="164px" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 20px" valign="top">
                                            <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server"
                                                TargetControlID="Button10" PopupControlID="checkoutPanel" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>

                                            <asp:Panel ID="checkoutPanel" runat="server" Width="400px"
                                                Style="text-align: left; background-color: white; height: auto; padding-bottom: 15px">
                                                <asp:Label ID="lblOcurre" runat="server" Width="452px" Height="27px" Style="margin-left: 15px; margin-top: 15px; color: red; font-weight: bold">Ocurre Forzoso</asp:Label>
                                                <asp:RadioButton ID="rbCosto" GroupName="myg" Text=" DraftLogistic:" Style="margin-left: 15px; margin-top: 15px" runat="server" /><br id="brCosto" runat="server" />
                                                <asp:RadioButton ID="rbTerrestre" GroupName="myg" Text=" Terrestre:" Style="margin-left: 15px" runat="server" /><br id="brTerrestre"  runat="server"/>
                                                <asp:RadioButton ID="rbDiaSiguiente" GroupName="myg" Text=" Dia Siguiente:" Style="margin-left: 15px" runat="server" /><br id="brDiaSiguiente"  runat="server"/>
                                                <asp:RadioButton ID="rbLtl" GroupName="myg" Text=" Tarimas:" Style="margin-left: 15px" runat="server" /><br id="brLtl" runat="server"/>
                                                <asp:RadioButton ID="rbFedexExpress" GroupName="myg" Text=" Fedex Express Saver:" Style="margin-left: 15px" runat="server" /><br id="brFedexExpress"  runat="server"/>
                                                <asp:RadioButton ID="rbFedexStandard" GroupName="myg" Text=" Fedex Standar Overnight:" Style="margin-left: 15px" runat="server" /><br id="brFedexStandard"  runat="server"/>
                                                <asp:RadioButton ID="rbPaqueteExpressEconomic" GroupName="myg" Text=" Paquete Express Economico:" Style="margin-left: 15px" runat="server" /><br id="brPaqueteExpressEconomic"  runat="server"/>
                                                <asp:RadioButton ID="rbPaqueteExpressNextDay" GroupName="myg" Text=" Paquete Express Dia Siguiente:" Style="margin-left: 15px" runat="server" /><br id="brPaqueteExpressNextDay"  runat="server"/>
                                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnAceptar" Style="margin-left: 15px" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" runat="server" Text="Guardar Envío" Width="118px"></asp:Button>
                                                <asp:Button CssClass="btn btn-outline btn-success btn-sm" Style="margin-left: 10px" ID="btnCancelar" runat="server" Text="Cancelar" Width="118px"></asp:Button>
                                            </asp:Panel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" valign="top" style="padding-bottom: 10px">
                                            <asp:TextBox CssClass="form-control" Height="41px" ID="TxtObservaciones" runat="server" TextMode="MultiLine" Width="422px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 20px" valign="top">
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnCheckOut" name="btnCheckOut" runat="server" Text="Checkout" Width="118px"></asp:Button>
                                            <asp:TextBox CssClass="form-control" Height="27px" ID="TextBox2" runat="server" Width="18px" Visible="False"></asp:TextBox>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="Button4" runat="server" OnClick="Button4_Click" Text="Imprimir Guía FedEx"
                                                Width="154px" Enabled="False" Visible="False" />
                                            <%--<asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnAddBook" runat="server" OnClientClick="DisplayAddBookForm()" Text="Importar Direcciones" Width="148px"></asp:Button>--%>
                                            <asp:Button CssClass="btn btn-outline btn-success btn-sm" ID="btnDatosUltimoEnvio" runat="server" Text="Datos Ultimo Envio" Width="148px"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 20px" valign="top">
                                            <asp:Label ID="Label1" runat="server" Width="452px" Height="27px"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            <asp:HiddenField ID="TxtTarifa" Value="" runat="server" />
                            <asp:HiddenField ID="estafetaTerrestre" Value="" runat="server" />
                            <asp:HiddenField ID="estafetaDiaSig" Value="" runat="server" />
                            <asp:HiddenField ID="estafetaLtl" Value="" runat="server" />
                            <asp:HiddenField ID="estafetaTipoServicio" Value="" runat="server" />
                            <asp:HiddenField ID="fedexExpress" Value="" runat="server" />
                            <asp:HiddenField ID="fedexStandard" Value="" runat="server" />
                            <asp:HiddenField ID="paqueteExpressEconomic" Value="" runat="server" />
                            <asp:HiddenField ID="paqueteExpressNextDay" Value="" runat="server" />
                            <br />
                            <asp:SqlDataSource ID="Tarifas" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectTarifas_por_Agente" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownAgentes" Name="id_agencia" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:Parameter DefaultValue="1" Name="id_tipo" Type="Int32" />
                                    <asp:Parameter DefaultValue="True" Name="activado" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Paises" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectPaises" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <%-- <asp:Parameter Name="id_pais" Type="Int32"></asp:Parameter> --%>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDSAgentes" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_Select_Agentes_por_usuarios" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <asp:SessionParameter Name="id_usuario" SessionField="id_usuario" Type="Int32" DefaultValue="" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Contenidos" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>" SelectCommand="sp_select_contenidos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="Tarifas_Seguro" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_tarifas_seguro" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="52" Name="id_pais" Type="Int32" />
                                    <asp:Parameter DefaultValue="2" Name="id_moneda" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Destinatarios" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_busca_destinatarios2" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TxtBuscaDest" DefaultValue="" Name="Destinatario" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="DropDownPais2" DefaultValue="" Name="id_pais" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="DropDownAgentes" DefaultValue="" Name="id_agencia" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Paises2" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_SelectPaises" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <%-- <asp:Parameter Name="id_pais" Type="Int32"></asp:Parameter>--%>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="LibroDirecciones" runat="server" SelectCommandType="StoredProcedure" SelectCommand="sp_select_libro_direcciones" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>">
                                <SelectParameters>
                                    <asp:SessionParameter Name="idUsuario" SessionField="id_usuario" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Guia" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                                SelectCommand="sp_Select_Datos_Envio" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TextBox2" DefaultValue="" Name="id_envio" PropertyName="Text"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Estados2" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                                SelectCommand="sp_SelectEstados" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownPais2" Name="id_pais" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlColonias" runat="server" ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                                SelectCommand="sp_SelectColonias" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TxtCP2" Name="cp" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Proveedores" runat="server"
                                ConnectionString="<%$ ConnectionStrings:paqueteriaDB_ConnectionString %>"
                                SelectCommand="sp_select_proveedores" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                            <br />

                            <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button6" runat="server" Text="Button" Visible="True" />
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button7" runat="server" Text="Button" Visible="True" />
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button8" runat="server" Text="Button" Visible="True" />
                            <asp:Button CssClass="btn btn-outline btn-success btn-sm invisible" ID="Button10" runat="server" Text="Button" Visible="True" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal fade" id="addBookModal" tabindex="-1" role="dialog" aria-labelledby="addBookModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="addBookModalLabel">Importar Direcciones</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group" id="fileTable">
                                        <div class="col-lg-12">
                                            <div class="col-lg-5 ">
                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <span class="btn btn-success btn-xs fileinput-button">
                                                                <i class="glyphicon glyphicon-plus"></i>
                                                                <span>Agregar Archivo...</span>
                                                                <!-- The file input field used as target for the file upload widget -->
                                                                <input id="FileUpload1" type="file" onclick="javascript: if (!validUpload()) { return false; }" name="files[]" multiple />
                                                            </span>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6">
                                                                <label>Nombre de hoja: </label>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <input type="text" name="sheetName" id="sheetName" size="10" maxlength="10" value="Sheet1" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label>Renglon de Encabezado : </label>
                                                            <input type="text" name="rowHead" id="rowHead" size="3" maxlength="3" value="1" />
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-6">
                                                        <!-- The container for the uploaded files -->
                                                        <div id="files" class="files"></div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <!-- The global progress bar -->
                                                    <div id="progress" class="progress">
                                                        <div class="progress-bar progress-bar-success"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-7">
                                                <div class="col-lg-12">
                                                    <label class="col-lg-4 control-label" for="datepicker">Nombre Archivo:</label>
                                                    <div class="col-lg-8">
                                                        <h6>
                                                            <label id="attachedfiles" class="label label-success"></label>
                                                        </h6>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 ">
                                                    <label class="col-lg-4 control-label" for="selTemplates">Leer Template:</label>
                                                    <div class="col-lg-8">
                                                        <select id="selTemplates" name="selTemplates" style="width: 250px;"></select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div class="col-lg-5 text-left">
                                                <h5>
                                                    <label class="label label-default " for="">Campos de Archivo</label></h5>
                                            </div>
                                            <div class="col-lg-2 text-center ">
                                            </div>
                                            <div class="col-lg-5 text-left">
                                                <h5>
                                                    <label class="label label-default" for="">Campos Obligatorios</label></h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div class="col-lg-5 text-left">
                                                <select multiple="multiple" id='lstFileFields' class="form-control"></select>
                                            </div>
                                            <div class="col-lg-2 text-center ">
                                                <input class="btn btn-info btn-sm" type="button" id="btnMerge" name="btnMerge" value="<< - >>" />
                                            </div>
                                            <div class="col-lg-5 text-left">
                                                <select multiple="multiple" id='lstDBFields' class="form-control"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div class="col-lg-8 text-left">
                                                <h5>
                                                    <label class="label label-default " for="">Campos Relacionados</label></h5>
                                            </div>
                                            <div class="col-lg-4 text-left">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div class="col-lg-8 text-left">
                                                <select multiple="multiple" id='lstBoxMatches' class="form-control">
                                                </select>
                                            </div>
                                            <div class="col-lg-4 text-center">
                                                <input class="btn btn-info btn-sm" id="btnMatchRemove" name="btnMatchRemove" type="button" value="Remover" />
                                                <br />
                                                <br />
                                                <input class="btn btn-info btn-sm" type="button" id="btnPreview" name="btnPreview" value="Vista Previa" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-12 col-xs-12 col-sm-12 text-center" style="overflow: auto;">
                                            <table id="gridAddBook" class=" table table-striped table-bordered table-condensed"></table>
                                            <div id="pagerAddBook"></div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-3">
                                            <input class="btn btn-danger btn-sm" id="bedata" value="Ver Mensaje de error" />
                                        </div>
                                        <div class="col-lg-3">
                                            <input class="btn btn-success btn-sm" id="btnDelete" value="Eliminar Seleccionados" />
                                        </div>
                                        <div class="col-lg-3">
                                            <input class="btn btn-info btn-sm" type="button" id="btnClean" name="btnClean" value="Limpiar Datos" onclick="javascript: clearAddBookFields();" />
                                        </div>
                                        <%--<div class="col-lg-3">
                                            <input class="btn btn-primary btn-sm" type="button" id="btnNormaliza" name="btnNormaliza" value="Normalizar Direcciones" />
                                        </div>--%>
                                        <div class="col-lg-3">
                                            <input class="btn btn-primary btn-sm" type="button" id="btnImport" name="btnImport" value="Importar Direcciones" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="createAddBookModal" tabindex="-1" role="dialog" aria-labelledby="createAddBookModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="createAddBookModalLabel">Salvar Template</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-lg-2 control-label" for="selPaisAB">Pais</label>
                                            <div class="col-lg-3">
                                                <select class="form-control small" id="selPaisAB" name="selPaisAB">
                                                    <option value="52">México</option>
                                                    <option value="0">Otro</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-lg-2 control-label" for="templateName">Nombre Template</label>
                                            <div class="col-lg-6">
                                                <input type="text" name="templateName" class="form-control" id="templateName" maxlength="40" value="" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-lg-2 control-label" for="templateName">Libro de direcciones</label>
                                            <div class="col-lg-6">
                                                <input type="text" name="addBookName" class="form-control" id="addBookName" maxlength="40" value="" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-lg-2 control-label" for="templateName"></label>
                                            <div class="col-lg-6">
                                                <input type="checkbox" name="saveNewAddBook" id="saveNewAddBook" value="AddressBook" />Salvar nuevo libro<br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                    <button type="button" class="btn btn-primary" id="btnConfirmAddbook" name="btnConfirmAddbook" value="Aceptar">Importar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dialogConfirmMasive" title="Confirmar salvado..." style="width: 300px;">
                        <label id="lblConfirm"></label>
                    </div>
                    <input id="nombreRealArchivo" name="nombreRealArchivo" type="hidden" value="" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptSection" runat="server">

    <script type="text/javascript" src="../Scripts/grid.locale-en.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validator.js"></script>

    <script type="text/javascript" src="../Scripts/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.fileupload.js"></script>
    <script language="javascript" type="text/javascript">
        $("#FileUpload1")
            .fileupload({
                url: '../Scripts/Upload.ashx?upload=start',
                acceptFileTypes: /(\.|\/)(csv)$/i,
                maxFileSize: 999000,
                add: function (e, data) {
                    $('#progress').show();
                    data.context = $('<div/>').appendTo('#files');
                    console.log('add', data.files);
                    $.each(data.files, function (index, file) {
                        $("#attachedfiles").text(file.name);
                    });
                    data.submit();
                },
                progress: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $('#progress div').css('width', progress + '%');
                },
                success: function (response, status) {
                    var fileProperties = JSON.parse(response);
                    console.log('success', fileProperties);
                    readFile(fileProperties.fileName, $("#selTemplates").val(), fileProperties.name);
                },
                error: function (error) {
                    $('#progress').hide();
                    $('#progress div').css('width', '0%');
                    alert('Ocurrio un error al subir el archivo. Nombre de hoja no existe.');
                }
            });
    </script>
    <script language="javascript" type="text/javascript">          
        $('input[id$=btnCheckOut]').click(function () {
            $('input[id$=btnAceptar]').removeAttr('disabled');
        });

    </script>
    <script type="text/javascript" src="../Scripts/Punto_Venta2.js"></script>
</asp:Content>


