<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ActualizaFedEx.aspx.vb" Inherits="ops_pages_ActualizaFedEx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; top: 160px; left: 220px; width: 626px;">
        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <p>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 143px">&nbsp;</td>
                                    <td style="width: 184px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">&nbsp;</td>
                                    <td style="width: 184px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">&nbsp;</td>
                                    <td style="width: 184px">
                                        <asp:textbox cssclass="form-control" height="56px" id="txtMsg" runat="server" visible="False" width="313px"></asp:textbox>
                                    </td>
                                    <td>
                                        <asp:button cssclass="btn btn-outline btn-success btn-sm" id="Button2" runat="server" text="Ok" visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">Envio</td>
                                    <td style="width: 184px">
                                        <asp:textbox cssclass="form-control" height="27px" id="txtEnvio" runat="server"></asp:textbox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">Referencia FedEx</td>
                                    <td style="width: 184px">
                                        <asp:textbox cssclass="form-control" height="27px" id="txtReferencia" runat="server" width="233px"></asp:textbox>
                                    </td>
                                    <td>
                                        <asp:button cssclass="btn btn-outline btn-success btn-sm" id="Button1" runat="server" height="22px" text="Actualizar" width="90px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">&nbsp;</td>
                                    <td style="width: 184px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 143px">&nbsp;</td>
                                    <td style="width: 184px">
                                        <asp:label id="Label2" runat="server"
                                            height="16px" width="318px"></asp:label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

