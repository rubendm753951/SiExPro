$(document).ready(function () {
    var context = this;

    $('#btnConfirmAddbook').click(function () {
        if ($('#saveNewAddBook').prop('checked') == true && $('#addBookName').val() == '') {
            alert('Debe proporcionar el nombre del libro.');
        } else {
            var saveTemp = saveAddressBook();

            if (saveTemp == 1) {
                $('#templateName').empty();
                $('#addBookName').empty();
                $('#createAddBookModal').modal("hide");
                clearAddBookFields();                
                loadAddBook();
            }
        }
    });  

    $("#btnMerge").click(function () {
        var fileFieldValue = $("#lstFileFields option:selected").val();
        var fileFieldText = $("#lstFileFields option:selected").text();

        var DBFieldValue = $("#lstDBFields option:selected").val();
        var DBFieldText = $("#lstDBFields option:selected").text();

        console.log('btnMerge');

        if (DBFieldText == "" || fileFieldText == "") {
            alert("Para relacionar los campos: Debes seleccionar un campo de cada lista.");
        } else {
            $('#lstBoxMatches').append($('<option>', {
                value: DBFieldValue + ':' + DBFieldText,
                text: fileFieldValue + ':' + fileFieldText + ' < -- > ' + DBFieldValue + ':' + DBFieldText
            }));

            if (DBFieldText != "OBSERVACIONES")
                $("#lstDBFields option:selected").remove();
        }
    });

    $("#btnMatchRemove").click(function () {
        var matchText = $("#lstBoxMatches option:selected").text();
        if (matchText == "") {
            alert("Debe seleccionar un elemento.");
        } else {
            var matchVal = $("#lstBoxMatches option:selected").val();
            var parts = matchVal.split(':');
            var dbFieldValue = parts[0];
            var dbFieldText = parts[1];
            if (!existOption("lstDBFields", dbFieldText)) {
                $('#lstDBFields').append($('<option>', {
                    value: dbFieldValue,
                    text: dbFieldText
                }));
            }
            $("#lstBoxMatches option:selected").remove();
        }
    });

    $("#btnPreview").click(function () {
        var fileName = $("#nombreRealArchivo").text();
        var mandFileds = $("#lstDBFields option").length;

        if (mandFileds > 1) {
            alert("Existen campos mandatorios que aun no han sido relacionados.");
        } else {
            $("#gridAddBook").jqGrid('GridUnload');

            addBookGrid(fileName, false);
        }
    });

    //Address Book - Boton Ver Mensaje - si ha un mensaje en algun registro que no se normalizo
    $("#bedata").click(function () {
        var gr = jQuery("#gridAddBook").jqGrid('getGridParam', 'selrow');
        if (gr != null) {
            var data = $('#gridAddBook').getRowData(gr);

            if (data.Message == "")
                alert("No hay mensaje de error.");
            else
                alert(data.Message);
        }
        else alert("Por favor seleccione un renglon.");
    });

    //Boton Importar direcciones, muestra el dialog para confirmar que se va a importar libro de direcciones.
    $("#btnImport").on("click", function (e) {
        e.preventDefault();
        var rows = jQuery("#gridAddBook").jqGrid('getRowData');
        if (rows.length > 0) {
            $("#createAddBookModal").modal('show');
            $('#templateName').empty();
            $('#addBookName').empty();
        } else {
            alert('No hay direcciones para importar');
        }
    });

    $("#btnGetAddBook").click(function () {
        var idBook = $('#selAddBook').val();

        if (idBook != null) {
            getAddBookClientes(idBook);
            $('#tblDest').hide();
            $('#mailToAddress').hide();
            $('#subButtons').hide();
            $('#tblAddBook').show();
            $('#btnSave').show();
            $('#Inserta').hide();
        } else {
            alert('Debe seleccionar un libro de direcciones');
        }
    });

    $("#btnHideAddBook").click(function () {
        var mydata = [];

        $('#tblAddBook').hide();
        createABEmptyGrid(mydata, "gridAddressBook", "pagerAddressBook");
        $('#tblDest').show();
        $('#mailToAddress').show();
        $('#subButtons').show();
        $('#btnSave').hide();
        $('#Inserta').show();
    });
    
    //Dialogo para confirmar que se quiere salvar el libro de direcciones
    $("#dialogConfirmMasive").dialog({
        width: 300,
        autoOpen: false,
        modal: true,
        buttons: {
            "Confirmar": function () {
                imprimirEnviosDeLibro();
                $(this).dialog("close");
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#btnDelete').click(function () {
        var rowsSelected = $('#gridAddBook').getGridParam('selarrrow');
        do {
            $.each(rowsSelected, function(index, rowid) {
                $('#gridAddBook').delRowData(rowid);
            });

            rowsSelected = $('#gridAddBook').getGridParam('selarrrow');
        }while(rowsSelected.length > 0)
    
    });

    $('#saveNewAddBook').change(function () {
        if ($('#saveNewAddBook').prop('checked') == true) {
            $("#addBookName").removeAttr("disabled");
        } else {
            $("#addBookName").attr("disabled", "disabled");
        }
    });

    //$('#btnReadFile').click(function () {
    //    console.log($('#FileUpload1'));
    //    if (validUpload()) {            
    //        console.log($('#FileUpload1'));
    //        $('#FileUpload1').fileUploadStart();
    //    }
    //});    

    

    loadAddBook();
    $("#addBookName").attr("disabled", "disabled");
    $('#tblAddBook').hide();
    $('[id$=btnSave]').hide();
    $('#Inserta').show();
});

function crearEnviosLibro() {
    var idBook = $('[id$=selAddBook]').val();

    if (idBook > 0) {
        var bookText = $('[id$=selAddBook] :selected').text();

        $('#lblConfirm').text("Esta seguro de crear los envios del libro de direcciones: " + bookText);
        $("#dialogConfirmMasive").dialog("open");
    } else {
        alert('Seleccione un libro de direcciones');
    }
}

function DisplayAddBookForm() {    

    $('#addBookModal').modal('show');    

    $("#lstDBFields").empty();
    clearAddBookFields();
    getTemplates();
    getMandatoryFields();
}

function imprimirEnviosDeLibro() {
    
    var sender = {
        id_pais: $('[id$=DropDownPais]').val(),
        nombre: $('[id$=txtNombre]').val(),
        apellidos: $('[id$=TxtApellidos]').val(),
        empresa: $('[id$=txtEmpresa]').val(),
        calle: $('[id$=txtCalle]').val(),
        noexterior: 0,
        nointerior: 0,
        direccion2: '',
        colonia: $('[id$=TxtCol]').val(),
        ciudad: $('[id$=txtCiudad]').val(),
        municipio: $('[id$=TxtMpio]').val(),
        estadoprovincia: $('[id$=txtEdo]').val(),
        telefono: $('[id$=txtTelefono]').val(),
        codigo_postal: $('[id$=TxtCP]').val(),
        email: $('[id$=txtEmail]').val()
    };

    var envio = {
        id_agente: $('[id$=DropDownAgentes]').val(),
        precio: $('[id$=TxtTarifa]').val(),
        valor_seguro: $('[id$=TxtSeguro]').val(),
        id_tarifa_agencia: $('[id$=DropDownProduct]').val(),
        id_codigo_promocion: 0,
        //id_codigo_promocion : ($('#TxtPromo').val() != '') ? $('#TxtPromo').val() : 0 ,
        valor_aduana: 0,
        //valor_aduana: ($('#TxtAduana').val() != '') ? $('#TxtAduana').val() : 0,
        total_envio : 0,
        fecha : getDate(new Date()),
        instrucciones_entrega: $('[id$=TxtInstEntrega]').val(),
        observaciones : '',
        id_usuario : 0,
        id_ruta : 0, 
        id_destinatario : 0,
        id_cliente : 0,
        largo: $('[id$=txtLargo]').val(),
        ancho: $('[id$=txtAncho]').val(),
        alto: $('[id$=txtAlto]').val(),
        peso: $('[id$=txtPeso]').val(),
        referencia: $('[id$=TxtRef]').val(),
        contenido: $('[id$=DropDownContenidos]').val(),
        dimension_peso : 0,
        contenedores: $('[id$=TxtCajas]').val()
    };

    //var rows = $('#gridAddressBook').getGridParam('selarrrow');
    //var beneficiaries = [];
    //$.each(rows, function (index, rowid) {
    //    var item = $('#gridAddressBook').getRowData(rowid);
    //    beneficiaries.push(item);
    //});

    var idBook = $('[id$=selAddBook]').val();

    var url = "Punto_Venta2.aspx/createMasiveIndicium";
    var params = '{requestSender:' + JSON.stringify(sender) + ',requestEnvio:' + JSON.stringify(envio) + ', idAddBook:' + idBook + '}';
    var errMsg = "Error crear envios: ";
    var jsondata = genericCallWebMethod(url, params, errMsg, false);
    
    if (jsondata.d.SendMessages == "") {
        var BenefMessages = jsondata.d.BenefMessages;
        var shipments;

        if (BenefMessages.length == 0) {
            $('[id$=selAddBook]').val("0").trigger('change');
            CleanPage();

            alert('Los envios fueron creados.');
        } else {
            shipments = jsondata.d.shipments;
            if (shipments.length == 0) {
                alert('No se pudieron crear los envios');
            } else {
                //alert('A los clientes seleccionados no se les pudo crear envio.');
            }
            //var rows;
            //$.each(BenefMessages, function (indexes, itemBenef) {
            //    rows = jQuery("#gridAddressBook").jqGrid('getRowData');
            //    $.each(rows, function (index, item) {
            //        if (item.id_cliente == itemBenef.id_dest) {
            //            var su = jQuery("#gridAddressBook").jqGrid('setRowData', index + 1, { id_book: item.id_book, id_cliente: item.id_cliente, nombrecompleto: item.nombrecompleto, direccioncompleta: item.direccioncompleta, telefono: item.telefono, nombres: item.nombres, apellidos: item.apellidos, direccion: item.direccion, estadoprovincia: item.estadoprovincia, ciudad: item.ciudad, codigo_postal: item.codigo_postal, addError: 1, message: itemBenef.Message });
            //        }
            //    });
            //});

            //rows = jQuery("#gridAddressBook").jqGrid('getRowData');
            //$.each(rows, function (index, item) {
            //    if (item.addError == "") {
            //        jQuery("#gridAddressBook").setSelection(index + 1, false);
            //    }
            //});
        }
    } else {
        alert('No fue posible crear envios. ' + jsondata.d.SendMessages);
    }
}

function CleanPage() {

    $('[id$=DropDownPais]').val(0);
    $('[id$=txtNombre]').val('');
    $('[id$=TxtApellidos]').val('');
    $('[id$=txtEmpresa]').val('');
    $('[id$=txtCalle]').val('');
    $('[id$=TxtCol]').val('');
    $('[id$=txtCiudad]').val('');
    $('[id$=TxtMpio]').val('');
    $('[id$=txtEdo]').val('');
    $('[id$=txtTelefono]').val('');
    $('[id$=TxtCP]').val('');
    $('[id$=txtEmail]').val('');

    $('[id$=DropDownAgentes]').val(0);
    $('[id$=TxtTarifa]').val('0');
    $('[id$=TxtSeguro]').val('0');
    $('[id$=DropDownProduct]').val(0);
    $('[id$=TxtInstEntrega]').val('');
    $('[id$=txtLargo]').val('0');
    $('[id$=txtAncho]').val('0');
    $('[id$=txtAlto]').val('0');
    $('[id$=txtPeso]').val('0');
    $('[id$=TxtRef]').val('');
    $('[id$=DropDownContenidos]').val(0);
    $('[id$=TxtCajas]').val('');
}

function getDate(dateObj) {
    var month = dateObj.getMonth() + 1;
    var day = dateObj.getDate();
    var monthStr, dayStr;
    if (month > 9) {
        monthStr = '' + month + '';
    } else {
        monthStr = '0' + month + '';
    }
    if (day > 9) {
        dayStr = '' + day + '';
    } else {
        dayStr = '0' + day + '';
    }
    var dateStr = dateObj.getFullYear() + '-' + monthStr + '-' + dayStr;
    return dateStr;
}

//SETTING UP OUR POPUP
//0 means disabled; 1 means enabled;
var popupStatusContact = 0;
var popupStatusCancel = 0;
var popupStatusReprint = 0;
var popupAddBook = 0;

//loading popup with jQuery magic!
function loadPopup(popup) {
    //loads popup only if it is disabled
    if (popupStatusContact == 0 || popupStatusCancel == 0) {
        $("#backgroundPopup").css({
            "opacity": "0.7"
        });
        $("#backgroundPopup").fadeIn("slow");
        $(popup).fadeIn("slow");

        switch (popup) {
            case "#popupAddBook":
                popupAddBook = 1;
                break;
        }
    }
}

//disabling popup with jQuery magic!
function disablePopup() {
    if (popupAddBook == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupAddBook").fadeOut("slow");
        $("#AddBookLabel").hide();
        $("#popupAddBook").hide();
        popupAddBook = 0;
    }
}

//centering popup
function centerPopup(popup) {
    //request data for centering
    var windowWidth = document.documentElement.clientWidth;
    var windowHeight = document.documentElement.clientHeight;
    var popupHeight = $(popup).height();
    var popupWidth = $(popup).width();
    //centering
    $(popup).css({
        "position": "absolute",
        "top": windowHeight / 2 - popupHeight / 2,
        "left": windowWidth / 2 - popupWidth / 2
    });
    //only need force for IE6

    $("#backgroundPopup").css({
        "height": windowHeight
    });

    $("#popupAddBookClose").click(function () {
        disablePopup();
    });

}

function loadAddBook() {
    $("#selAddBook").empty();    

    var url = "Punto_Venta2.aspx/getAddBook";
    var params = '{}';
    var errMsg = "Error al cargar libros de direcciones: ";

    var response = genericCallWebMethod(url, params, errMsg, false);

    if (response.d.responseMessage == "") {
        $("#selAddBook").empty();        
        $('#selAddBook').append($('<option>', {
            value: 0,
            text: 'Seleccione libro de direcciones'
        }));
        $.each(response.d.responseArray, function (index, item) {
            addOption("selAddBook", item.id_book, item.nombre);
        });
    }

}

function validUpload() {    
    if ($('#sheetName').val() == '') {
        alert('Debe proporcionar el nombre de la hoja que desea leer.');
        return false;
    }
    if ($('#rowHead').val() == '') {
        alert('Debe proporcionar el renglon donde se encuentran los nombres de las columnas.');
        return  false;
    }
    return true;
}

function readFile(fileName, idTemplate, fileUploadName) {

    if (!validUpload()) {
        return false;
    }

    clearAddBookFields();

    var sheetName = $('#sheetName').val();
    var rowHead = $('#rowHead').val();
    var url = "Punto_Venta2.aspx/readFile";
    var params = '{fileFullName:"' + fileName + '",idTemplate:' + idTemplate + ', sheetName: "' + sheetName + '", rowHead:' + rowHead + '}';
    var errMsg = "Error al leer archivo: ";

    var response = genericCallWebMethod(url, params, errMsg, false);

    console.log(response);

    if (response.d.responseMessage == "") {
        $.each(response.d.fileFields, function (index, item) {
            var position = index + 1;
            addOption("lstFileFields", position, item);
        });

        $("#attachedfiles").text(fileUploadName);
        $("#nombreRealArchivo").text(fileName);

        if (idTemplate > 0) {
            try {
                $.each(response.d.matchTemplate, function (index, item) {
                    var position = item.split(":");

                    $('#lstFileFields option[value=' + position[0] + ']').attr('selected', true);
                    $('#lstDBFields option[value=' + position[1] + ']').attr('selected', true);

                    var fileFieldValue = $("#lstFileFields option:selected").val();
                    var fileFieldText = $("#lstFileFields option:selected").text();

                    var DBFieldValue = $("#lstDBFields option:selected").val();
                    var DBFieldText = $("#lstDBFields option:selected").text();

                    if (DBFieldText == "" || fileFieldText == "") {
                        alert("Template invalido, campos no coinciden.");
                    } else {

                        addOption("lstBoxMatches", DBFieldValue + ':' + DBFieldText, fileFieldValue + ':' + fileFieldText + ' < -- > ' + DBFieldValue + ':' + DBFieldText);
                        if (DBFieldText != "OBSERVACIONES")
                            $("#lstDBFields option:selected").remove();

                        $('#lstFileFields option[value=' + position[0] + ']').attr('selected', false);
                    }
                });

                $("#btnPreview").trigger('click');
            } catch (err) {
                alert("Template invalido, campos no coinciden.");
            }
        }
    } else {
        if (response.d.responseMessage.indexOf('valid name') >= 0) {
            alert('Nombre de hoja invalido.');
        } else {
            alert(response.d.responseMessage);
        }        
    }
}

function getTemplates() {
    var url = "Punto_Venta2.aspx/getTemplates";
    var params = "{}";
    var errMsg = "Error al cargar templates: ";

    //get all templates availables
    var response = genericCallWebMethod(url, params, errMsg, false);

    $("#selTemplates").empty();
    $('#selTemplates').append($('<option>', {
        value: 0,
        text: 'Seleccionar Template'
    }));

    // $("#selTemplates").get(0).options[0] = new Option("Seleccionar Template", "0");

    $.each(response.d.responseArray, function (index, item) {
        addOption("selTemplates", item.id_template, item.nombre);
    });
}

function getMandatoryFields() {
    var url = "Punto_Venta2.aspx/getMandatoryFields";
    var params = "{}";
    var errMsg = "Error al leer archivo: ";
    //get the list of all the mandatory fields
    var response = genericCallWebMethod(url, params, errMsg, false);

    $.each(response.d.responseArray, function (index, item) {
        addOption("lstDBFields", item.id_campo, item.nombre);
    });
}

function saveAddressBook() {
    var templateName = $.trim($('#templateName').val());
    var addBookName = $.trim($('#addBookName').val());
    var matches = '';
    var pais = $('#selPaisAB').val();
    var fileName = $("#attachedfiles").text();
    
    var ABDetail = getABDetail("gridAddBook", false);

    if (templateName != '') {
        matches = fieldsMatch();
    }

    var url = "Punto_Venta2.aspx/saveAddressBook";
    var params = '{templateName:"' + templateName + '",positions:"' + matches + '",addBookName:"' + addBookName + '",beneficiaries:' + ABDetail + ',pais:' + pais + ', fileName:"' + fileName + '"}';
    var errMsg = "Error al salvar template: ";

    var response = genericCallWebMethod(url, params, errMsg, false);

    if (response.d.responseSuccess == 1) {
        alert('Datos salvados exitosamente.');
        return 1;
    } else {
        return 0;
    }
}


function getABDetail(gridCtrl, hasRec) {
    var rows = jQuery("#" + gridCtrl).jqGrid('getGridParam', 'data');
    var beneficiaries = [];

    $.each(rows, function (index, item) {
        beneficiaries.push(item);
    });

    return JSON.stringify(beneficiaries);
}

function addBookGrid(fileName, isEmpty) {
    var dataResult;
    
    if (isEmpty == true) {
        var mydata = [];
        createEmptyGrid(mydata, 'gridAddBook', 'pagerAddBook');
    } else {
        if (!validUpload()) {
            return false;
        }
        var sheetName = $('#sheetName').val();
        var rowHead = $('#rowHead').val();
        var matches = fieldsMatch();

        var url = "Punto_Venta2.aspx/readFileContent";
        var params = '{fileName:"' + fileName + '",positions:"' + matches + '", sheetName: "' + sheetName + '", rowHead:' + rowHead + '}';
        var errMsg = "Error al cargar archivo: ";

        var response = genericCallWebMethod(url, params, errMsg, false);

        if (response.d.responseSuccess == 1) {
            $("#gridAddBook").trigger("reloadGrid");
            dataResult = response.d.responseArray;
            createEmptyGrid(dataResult, 'gridAddBook', 'pagerAddBook');
            if (response.d.responseMessage != "") {
                alert(response.d.responseMessage);
            }
        }
    }
    return true;
}

function getAddBookClientes(idLibro) {
    var url = "Punto_Venta2.aspx/getAddBookClientes";
    var params = '{idLibro:' + idLibro + '}';
    var errMsg = "Error al cargar clientes del libro de direcciones: ";

    $("#gridAddressBook").jqGrid('GridUnload');
    var response = genericCallWebMethod(url, params, errMsg, false);

    if (response.d.responseMessage == "") {
        $("#gridAddressBook").trigger("reloadGrid");
        createABEmptyGrid(response.d.responseArray, "gridAddressBook", "pagerAddressBook");
    }
}

function genericCallWebMethod(urlParam, params, errMsg, paramAsync) {
    var resp;

    $.ajax({
        type: "POST",
        url: urlParam,
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            resp = response;
            if (response.d.responseSuccess == 0) {
                alert(response.d.responseMessage);
            }
        },
        async: paramAsync,
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText           
            alert(errMsg + errorMessage);
        }
    });

    return resp;
}

function clearAddBookFields() {
    clearMarches();
    $("#lstFileFields").empty();
    $("#lstBoxMatches").empty();
    $("#attachedfiles").empty();
    $('#templateName').empty();
    $('#addBookName').empty();    

    addBookGrid("", true);

    return true;
}

function fieldsMatch() {
    var matches;
    var match;
    var firstRow = true;

    $("#lstBoxMatches option").each(function () {
        var lstText = $(this).text();
        var fileField = lstText.substring(0, lstText.indexOf("<"));
        var dbField = $(this).val();

        match = getPosition(fileField) + ':' + getPosition(dbField);

        if (firstRow == true) {
            matches = match;
        } else {
            matches = matches + "|" + match;
        }
        firstRow = false;
    });

    return matches;
}

function clearMarches() {
    $("#lstBoxMatches option").each(function () {
        var matchVal = $(this).val();
        var parts = matchVal.split(':');
        var dbFieldValue = parts[0];
        var dbFieldText = parts[1];

        if (!existOption("lstDBFields", dbFieldText))
            addOption("lstDBFields", dbFieldValue, dbFieldText);
    });
}

function existOption(multipleOption, textValue) {
    var exist = false;
    $("#lstDBFields option").each(function () {
        if ($(this).text() == textValue)
            exist = true;
    });

    return exist;
}

function addOption(controlName, valueParam, textParam) {
    $('#' + controlName).append($('<option>', {
        value: valueParam,
        text: textParam
    }));
}

function getPosition(fieldName) {
    var position;

    position = fieldName.split(':');

    return position[0];
}


function createEmptyGrid(mydata, gridName, gridPager) {
    $("#" + gridName).jqGrid({
        data: mydata,
        datatype: "local",
        async: false,
        jsonReader: {
            root: 'rows',
            page: 'page',
            repeatitems: false
        },
        colNames: ['Contenedor', 'Inventario', 'Destinatario', 'Ciudad', 'Estado', 'Direccion', 'C.P.', 'Telefono', 'Servicio', 'X COB.MX DLLs', 'Transporte', 'Guia', 'Observaciones', 'Message'],
        colModel: //Columns
        [
            { name: 'Contenedor', index: 'Contenedor', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Inventario', index: 'Inventario', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Destinatario', index: 'Destinatario', width: 250, align: 'Left', editable: true, frozen: true },
            { name: 'Ciudad', index: 'Ciudad', width: 100, align: 'Left', editable: true, frozen : true },
            { name: 'Estado', index: 'Estado', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Direccion', index: 'Direccion', width: 50, align: 'Left', editable: true, frozen: true },
            { name: 'codigo_postal', index: 'codigo_postal', width: 50, align: 'Left', editable: true, frozen: true },
            { name: 'Telefono', index: 'Telefono', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Servicio', index: 'Servicio', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Cobranza', index: 'Cobranza', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Transporte', index: 'Transporte', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Guia', index: 'Guia', width: 100, align: 'Left', editable: true, frozen: true },
            { name: 'Observaciones', index: 'Observaciones', width: 600, align: 'Left', editable: true, frozen: true },
            { name: 'Message', index: 'Message', width: 200, align: 'Left', hidden: true, editable: false }
        ],
        pager: jQuery('#' + gridPager),
        rowNum: 100,
        hidegrid: false,
        height: '200',
        width: "345",
        rowList: [100, 200, 300, 400, 500],
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        sortname: "Nombres", //Default SortColumn
        sortorder: "asc", //Default SortOrder.
        scroll: 1,
        gridview: true,
        autowidth: true,
        loadtext: 'Cargando datos...',
        viewrecords: true,
        rowheight: "20",
        rownumbers: true,
        multiselect: true,
        shrinkToFit: false,
        afterInsertRow: function (rowid, aData) {
            if (aData.Message != "") {
                $("#" + gridName).jqGrid('setCell', rowid, 'Contenedor', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Inventario', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Destinatario', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Ciudad', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Estado', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'codigo_postal', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Telefono', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Servicio', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Cobranza', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Transporte', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Guia', '', { color: 'red' });
                $("#" + gridName).jqGrid('setCell', rowid, 'Observaciones', '', { color: 'red' });
            }
        }
    });

    jQuery("#" + gridName).jqGrid('navGrid', '#' + gridPager, { edit: false, add: false, del: false, search: false });
}

//function createEmptyGrid(mydata, gridName, gridPager) {
//    jQuery("#" + gridName).jqGrid({
//        data: mydata,
//        datatype: "local",
//        colNames: ['Contenedor', 'Inventario', 'Destinatario', 'Ciudad', 'Estado', 'Direccion', 'C.P.', 'Telefono', 'Servicio', 'X COB.MX DLLs', 'Transporte', 'Guia', 'Observaciones', 'Message'],
//        colModel: //Columns
//        [
//            { name: 'Contenedor', index: 'Contenedor', width: 250, align: 'Left', editable: true },
//            { name: 'Inventario', index: 'Inventario', width: 250, align: 'Left', editable: true },
//            { name: 'Destinatario', index: 'Destinatario', width: 250, align: 'Left', editable: true },
//            { name: 'Ciudad', index: 'Ciudad', width: 450, align: 'Left', editable: true },
//            { name: 'Estado', index: 'Estado', width: 230, align: 'Left', editable: true },
//            { name: 'Direccion', index: 'Direccion', width: 230, align: 'Left', editable: true },
//            { name: 'codigo_postal', index: 'codigo_postal', width: 100, align: 'Left', editable: true },
//            { name: 'Telefono', index: 'Telefono', width: 200, align: 'Left', editable: true },
//            { name: 'Servicio', index: 'Servicio', width: 200, align: 'Left', editable: true },
//            { name: 'Cobranza', index: 'Cobranza', width: 200, align: 'Left', editable: true },
//            { name: 'Transporte', index: 'Transporte', width: 200, align: 'Left', editable: true },
//            { name: 'Guia', index: 'Guia', width: 200, align: 'Left', editable: true },
//            { name: 'Observaciones', index: 'Observaciones', width: 200, align: 'Left', editable: true },
//            { name: 'Message', index: 'Message', width: 200, align: 'Left', hidden: true, editable: false }
//        ],
//        pager: jQuery('#' + gridPager),
//        rowNum: 10,
//        hidegrid: false,
//        viewrecords: false,
//        height: '200',
//        width: "945",
//        rowList: [20, 40, 60, 80, 100, 200, 300],
//        recordtext: "{0} - {1} de {2} elementos",
//        emptyrecords: 'No hay resultados',
//        sortname: "Nombres", //Default SortColumn
//        sortorder: "asc", //Default SortOrder.
//        rowheight: "20",
//        rownumbers: true,
//        forceFit: true,
//        cellEdit: true,
//        cellsubmit: 'clientArray',
//        editurl: 'clientArray',
//        url: 'clientArray',
//        jsonReader: {
//            repeatitems: false
//        },
//        afterInsertRow: function (rowid, aData) {
//            if (aData.Message != "") {
//                $("#" + gridName).jqGrid('setCell', rowid, 'Contenedor', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Inventario', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Destinatario', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Ciudad', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Estado', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'codigo_postal', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Telefono', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Servicio', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Cobranza', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Transporte', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Guia', '', { color: 'red' });
//                $("#" + gridName).jqGrid('setCell', rowid, 'Observaciones', '', { color: 'red' });
//            }
//        }
//    });

//    jQuery("#" + gridName).jqGrid('navGrid', '#' + gridPager, { edit: false, add: false, del: false, search: false });
//}

function createABEmptyGrid(mydata, gridName, gridPager) {
    jQuery("#" + gridName).jqGrid({
        data: mydata,
        datatype: "local",
        async: false,
        jsonReader: {
            root: 'rows',
            page: 'page',
            repeatitems: false
        },
        colNames: ['Id Libro', 'ID Cliente', 'Nombre Completo', 'Direccion', 'Telefono', 'Nombres', 'Apellidos', 'Direccion', 'Estado/Provincia', 'Ciudad', 'C.P.', 'addError', 'message'],
        colModel: //Columns
        [
            { name: 'id_book', index: 'id_book', width: 1, align: 'Left', hidden: true },
            { name: 'id_cliente', index: 'id_cliente', width: 110, align: 'Left' },
            { name: 'nombrecompleto', index: 'nombres', width: 450, align: 'Left' },
            { name: 'direccioncompleta', index: 'direccion', width: 1, align: 'Left', hidden: true },
            { name: 'telefono', index: 'telefono', width: 200, align: 'Left' },
            { name: 'nombres', index: 'nombres', width: 1, align: 'Left', hidden: true },
            { name: 'apellidos', index: 'apellidos', width: 1, align: 'Left', hidden: true },
            { name: 'direccion', index: 'direccion', width: 1, align: 'Left', hidden: true },
            { name: 'estadoprovincia', index: 'estadoprovincia', width: 1, align: 'Left', hidden: true },
            { name: 'ciudad', index: 'ciudad', width: 1, align: 'Left', hidden: true },
            { name: 'codigo_postal', index: 'codigo_postal', width: 1, align: 'Left', hidden: true },
            { name: 'addError', index: 'message', width: 1, align: 'Left', hidden: true },
            { name: 'message', index: 'message', width: 1, align: 'Left', hidden: true }
        ],
        pager: jQuery('#' + gridPager),
        rowNum: 20,
        scroll: true,
        loadtext: 'Cargando datos...',
        rowList: [20, 40, 60, 80, 100, 200, 300], //Variable PageSize DropDownList. 
        //sortable: false,
        hidegrid: false,
        height: 200,
        width: 428,
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        sortname: "Nombres", //Default SortColumn
        sortorder: "desc", //Default SortOrder.
        viewrecords: true,
        rowheight: "20",
        rownumbers: true,
        multiselect: true,
        gridComplete: function () {
            $('.cbox').trigger('click').attr('checked', true);
        }
    });

    jQuery("#" + gridName).jqGrid('navGrid', '#' + gridPager, { edit: false, add: false, del: false, search: false });
}
/*
function createABEmptyGrid(mydata, gridName, gridPager) {
    jQuery("#" + gridName).jqGrid({
        data: mydata,
        datatype: "local",
        colNames: ['Id Libro', 'ID Cliente', 'Nombre Completo', 'Direccion', 'Telefono', 'Nombres', 'Apellidos', 'Direccion', 'Estado/Provincia', 'Ciudad', 'C.P.', 'addError', 'message'],
        colModel: //Columns
        [
            { name: 'id_book', index: 'id_book', width: 1, align: 'Left', hidden: true },
            { name: 'id_cliente', index: 'id_cliente', width: 110, align: 'Left' },
            { name: 'nombrecompleto', index: 'nombres', width: 450, align: 'Left' },
            { name: 'direccioncompleta', index: 'direccion', width: 1, align: 'Left', hidden: true },
            { name: 'telefono', index: 'telefono', width: 200, align: 'Left' },
            { name: 'nombres', index: 'nombres', width: 1, align: 'Left', hidden: true },
            { name: 'apellidos', index: 'apellidos', width: 1, align: 'Left', hidden: true },
            { name: 'direccion', index: 'direccion', width: 1, align: 'Left', hidden: true },
            { name: 'estadoprovincia', index: 'estadoprovincia', width: 1, align: 'Left', hidden: true },
            { name: 'ciudad', index: 'ciudad', width: 1, align: 'Left', hidden: true },
            { name: 'codigo_postal', index: 'codigo_postal', width: 1, align: 'Left', hidden: true },
            { name: 'addError', index: 'message', width: 1, align: 'Left', hidden: true },
            { name: 'message', index: 'message', width: 1, align: 'Left', hidden: true }
        ],
        pager: jQuery('#' + gridPager),
        rowNum: 1000,
        hidegrid: false,
        height: '200',
        width: "428",
        multiselect: true,
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        sortname: "Nombres", //Default SortColumn
        sortorder: "asc", //Default SortOrder.
        viewrecords: true,
        rowheight: "1000",
        rownumbers: true,
        ondblClickRow: function (rowid, iRow, iCol, e) {
            var data = $('#' + gridName).getRowData(rowid);
            if (data.message != "") {
                alert(data.message);
            }
        },
        gridComplete: function () {
            $('.cbox').trigger('click').attr('checked', true);
        }
    });

    jQuery("#" + gridName).jqGrid('navGrid', '#' + gridPager, { edit: false, add: false, del: false, search: false });
}*/