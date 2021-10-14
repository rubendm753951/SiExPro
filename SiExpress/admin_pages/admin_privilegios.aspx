<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeFile="admin_privilegios.aspx.vb" Inherits="admin_pages_admin_privilegios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="stylesheet" type="text/css" media="all"    href="../css/Privilegios.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css/jquery-ui-1.8.2.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css/ui.multiselect.css" />
    
    <script type="text/javascript" src="../scripts/jquery-1.7.2.js"></script>
    <script type="text/javascript" src="../scripts/jquery.validator.js"></script>
    <script type="text/javascript" src="../scripts/grid.locale-en.js"></script>       
    <script type="text/javascript" src="../scripts/jquery-ui-custom.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../scripts/commons.js"></script>    
    <script type="text/javascript" src="../scripts/Privilegios.js"></script>   
    <div class="page">
        <div class="pageTitle" >
            <span>Privilegios de Usuarios</span><br/>
        </div> 
        <div class="row">
            <div class="r1-left">
                <span>Modulos: </span>
            </div>
            <div class="r1-right">
                <select id="selectModulos" name="selectModulos">															
				        <option value="0" selected="selected">Modulos...</option>																                
			    </select>                            
                <span id="selectModulosReq"></span><span id="selectModulosMsg"></span> 
            </div>
        </div> 
         <div class="row">
            <div class="r1-left">
                <span>Usuarios: </span>
            </div>
            <div class="r1-right">
                <select id="selectUsuarios" name="selectUsuarios">
                    <option value="0" selected="selected">Agregar todos los Usuarios</option>															
				</select><span id="selectUsuariosReq"></span><span id="selectUsuariosMsg"></span>
            </div>
        </div>
        <div class="row">
            <div class="r1-left">
                
            </div>
            <div class="r1-right" style="text-align: left">
                <input class="primary" type="button" id="btnAdd" name="btnAdd" value="Agregar" />   
                <input class="primary" type="button" id="btnConsultar" name="btnConsultar" value="Consultar" />
                <input class="primary" type="button" id="btnSave" name="btnSave" value="Grabar Cambios" />   
                <input class="primary" type="button" id="btnClean" name="btnClean" value="Limpiar" />   
            </div>
        </div>
        <div class="row">
            <div class="r0-left">
                <table id="grid"></table>
                <div id="pager"></div> 
            </div>
        </div> 
        <input id="appVirtualPrivilegios" name="appVirtualPath" type="hidden" value="" />
        <input id="idUsuarioPrivilegios" name="idUsuario" type="hidden" value="" />
        <input id="usuarioNombre" name="usuarioNombre" type="hidden" value="" />
    </div>
</asp:Content>
