function loadDataPrivilegios(appVirtualPath, idUsuario, usuarioNombre) {
    $('#appVirtualPrivilegios').text(appVirtualPath);
    $('#idUsuarioPrivilegios').text(idUsuario);
    $('#usuarioNombre').text(usuarioNombre);
}

$(document).ready(function () {
    var context = this;
    Controller = new ControllerClass();

    $('#btnAdd').click(function () {
        if ($('#selectModulos').valid()) {
            addUsersToGrid();
        }
    });

    $('#btnConsultar').click(function () {
        if ($('#selectModulos').valid()) {
            $('#grid').jqGrid('clearGridData');
            $("#grid").trigger("reloadGrid");
            GetUserPrivileges();
        }
    });

    $('#btnSave').click(function () {
        if ($('#selectModulos').valid()) {
            Save();
        }
    });

    $('#btnClean').click(function () {
        $('#grid').jqGrid('clearGridData');
        $("#grid").trigger("reloadGrid");
    });

    createPrivilegiosGrid('', 'grid', 'pager');

    loadModulos();
    loadUsuarios();
});

ControllerClass = function () {

    /*   Begin Validators  */
    this.validator = $('#aspnetForm').validate({
        debug: true,
        ignore: ':hidden, :disabled',
        rules: {
            selectModulos: {
                selectModulosMsgReq: true
            }
        },
        messages: {
            
        },
        errorPlacement: function (error, element) {
            switch ($(element).attr('name')) {
                case 'selectModulos': 
                    $("#" + $(element).attr('name') + "Msg").append(error);
                    $("#" + $(element).attr('name') + "Req").text("*");
                    break;
            }
        },
        highlight: function (element, errorClass) {
            switch ($(element).attr('name')) {
                case 'selectModulos': 
                    $("#" + $(element).attr('name') + "Msg").addClass('expValError');
                    $("#" + $(element).attr('name') + "Req").addClass('expValError');
                    $("#" + $(element).attr('name') + "Req").text("*");
                    break;
            }
        },
        unhighlight: function (element, errorClass) {
            switch ($(element).attr('name')) {
                case 'selectModulos':  
                    $("#" + $(element).attr('name') + "Msg").removeClass('expValError');
                    $("#" + $(element).attr('name') + "Req").removeClass('expValError');
                    $("#" + $(element).attr('name') + "Req").text("");
                    break;
            }
        }
    });

    setDefaultEvents();

    $.validator.addMethod('selectModulosMsgReq', function (value, element) {
        return value != "0";
    }, 'Seleccione un modulo. <br />');
};

function setDefaultEvents() {

    //Set focusout event
    var arrFocusOut = ["selectModulos"];

    $.each(arrFocusOut, function (index, val) {
        $("#" + val).bind("focusout", function () {
            Controller.validator.element("#" + val);
        });
    });

    $("#selectModulos").bind("change", function () {
        $('#grid').jqGrid('clearGridData');
        $("#grid").trigger("reloadGrid");
        GetUserPrivileges();
    });
}

function addUsersToGrid() {
    var idModulo = $('#selectModulos').val();
    var modulo = $("#selectModulos option:selected").text();
    var idUsuario = $('#selectUsuarios').val();
    var rowIndex, rows, id;
    var mydata;

    if (idUsuario == 0) {
        $("#selectUsuarios > option").each(function () {
            if (this.value != 0) {
                rowIndex = FindDataInGrid(this.value, 'grid');

                if (rowIndex == 0) {
                    rows = $("#grid").getGridParam("reccount");
                    id = GetMaxIdInGrid('grid');
                    mydata = [
                        {
                            id: id,
                            id_usuario: this.value,
                            Nombre: this.text,
                            id_modulo: idModulo,
                            Modulo: modulo,
                            puedeLeer: true,
                            puedeEscribir: true,
                            puedeBorrar: true,
                            Fecha_Creacion: getCurrentShortDate(),
                            Fecha_Actualizacion: getCurrentShortDate(),
                            ActualizadoPor: $('#idUsuarioPrivilegios').text(),
                            ActualizadoPorNombre:  $('#usuarioNombre').text()
                        }
                    ];

                    jQuery("#grid").jqGrid('addRowData', rows + 1, mydata[0]);
                }
            }
        });
    } else {
        rowIndex = FindDataInGrid(idUsuario, 'grid');
        if (rowIndex == 0) {
            rows = $("#grid").getGridParam("reccount");
            id = GetMaxIdInGrid('grid');
            mydata = [
                {
                    id: id,
                    id_usuario: idUsuario,
                    Nombre: $("#selectUsuarios option:selected").text(),
                    id_modulo: idModulo,
                    Modulo: modulo,
                    puedeLeer: true,
                    puedeEscribir: true,
                    puedeBorrar: true,
                    Fecha_Creacion: getCurrentShortDate(),
                    Fecha_Actualizacion: getCurrentShortDate(),
                    ActualizadoPor: $('#idUsuarioPrivilegios').text(),
                    ActualizadoPorNombre: $('#usuarioNombre').text()
                }
            ];

            jQuery("#grid").jqGrid('addRowData', rows + 1, mydata[0]);
        }
    }
}

function FindDataInGrid(data, gridName) {
    var rows = $("#" + gridName).jqGrid('getRowData');
    var rowIndex = -1;

    $.each(rows, function (index, row) {
        if (row.id_usuario == data) {
            rowIndex = index;
        }
    });

    return rowIndex + 1;
}

function GetMaxIdInGrid(gridName) {
    var rows = $("#" + gridName).jqGrid('getRowData');
    var rowIndex = 0;

    $.each(rows, function (index, row) {
        if (row.id > rowIndex) {
            rowIndex = row.id;
        }
    });

    return rowIndex;
}

function Save() {

    var rows = $("#grid").getGridParam("reccount");

    if (rows > 0) {
        var userPriv = [];
        var allRowsInGrid = $('#grid').getRowData();

        $.each(allRowsInGrid, function (index, rowid) {
            userPriv.push(rowid);
        });

        if (userPriv.length > 0) {
            var url = "admin_privilegios.aspx/AddUpdateUserPrivileges";
            var params = "{userPriv:" + JSON.stringify(userPriv) + "}";
            var errMsg = "Error al cargar modulos: ";

            var response = genericAjaxWebMethod(url, params, errMsg, false);
            if (response != null) {
                if (response == true) {
                    alert('Datos actualizados.');
                }
            }
        } else {
            alert('No hay usuarios agregados.');
        }
    } else {
        alert('No hay usuarios agregados.');
    }
}

function GetUserPrivileges() {
    var idModulo = $('#selectModulos').val();
    var idUsuario = $('#selectUsuarios').val();
    var url = "admin_privilegios.aspx/GetUserPrivileges";
    var params = "{id_modulo:" + idModulo + ",id_usuario:" + idUsuario + "}";
    var errMsg = "Error al cosultar privilegios en el modulo: ";

    var response = genericAjaxWebMethod(url, params, errMsg, false);
    if (response != null) {
        $('#grid').jqGrid('clearGridData');
        $("#grid").trigger("reloadGrid");
        $.each(response, function (index, item) {
            var rows = $("#grid").getGridParam("reccount");
            var count = 1;

            var mydata = [
                {
                    id: item.id,
                    id_usuario: item.id_usuario,
                    Nombre: item.nombre,
                    id_modulo: item.id_modulo,
                    Modulo: item.modulo,
                    puedeLeer: item.puedeLeer,
                    puedeEscribir: item.puedeEscribir,
                    puedeBorrar: item.puedeBorrar,
                    Fecha_Creacion: formatJsonDate(item.fechaCreacion, false),
                    Fecha_Actualizacion: formatJsonDate(item.fechaActualizacion, false),
                    ActualizadoPor: item.actualizadoPor,
                    ActualizadoPorNombre: item.actualizadoPorNombre
                }
            ];

            $("#grid").jqGrid('addRowData', rows + 1, mydata[0]);
            count++;
        });
    
    } 
}

function loadModulos() {
    var url = "admin_privilegios.aspx/GetModulos";
    var params = '{}';
    var errMsg = "Error al cargar modulos: ";

    $("#selectModulos").empty();
    $("#selectModulos").get(0).options[0] = new Option("Cargando Modulos...", "0");

    var response = genericAjaxWebMethod(url, params, errMsg, false);
    if (response != null) {
        $("#selectModulos").empty();
        $("#selectModulos").get(0).options[0] = new Option("Seleccionar un modulo", "0");
        $.each(response, function (index, item) {
            addOption("selectModulos", item.id_modulo, item.nombre);
        });
    } else {
        $("#selectModulos").empty();
    }
}

function loadUsuarios() {
    var url = "admin_privilegios.aspx/GetUsuarios";
    var params = '{}';
    var errMsg = "Error al cargar usuarios: ";

    $("#selectUsuarios").empty();
    $("#selectUsuarios").get(0).options[0] = new Option("Cargando usuarios...", "0");

    var response = genericAjaxWebMethod(url, params, errMsg, false);
    if (response != null) {
        $("#selectUsuarios").empty();
        $("#selectUsuarios").get(0).options[0] = new Option("Todos los usuarios", "0");
        $.each(response, function (index, item) {
            addOption("selectUsuarios", item.id_usuario, item.nombre);
        });
    } else {
        $("#selectUsuarios").empty();
    }
}

function createPrivilegiosGrid(mydata, gridName, gridPager) {

    $("#" + gridName).jqGrid({
        data: mydata,
        datatype: "xml",
        height: '300',
        width: "833",
        mtype: 'GET',
        jsonReader: {
            repeatitems: false,
            id: 'id',
            root: function (obj) { return obj; },
            page: function (obj) { return 1; },
            total: function (obj) { return 1; },
            records: function (obj) { return obj.length; }
        },
        colNames: ['id', 'id_usuario', 'Nombre', 'id_modulo', 'Modulo','Lee', 'Escribe', 'Borra', 'Creado', 'Actualizado', 'Actualizado', 'Actualizado Por'],
        colModel: //Columns
        [
            { name: 'id', index: 'id', width: 100, align: 'Left', hidden: true },
            { name: 'id_usuario', index: 'id_usuario', width: 120, align: 'Left', hidden: true },
            { name: 'Nombre', index: 'Nombre', width: 150, align: 'Left' },
            { name: 'id_modulo', index: 'id_modulo', width: 100, align: 'Left', hidden: true },
            { name: 'Modulo', index: 'Modulo', width: 150, align: 'left' },
            { name: 'puedeLeer', index: 'puedeLeer', width: 30, align: 'center', editable: true, formatter: 'checkbox', editoptions: { value: '1:0' }, formatoptions: { disabled: false } },
            { name: 'puedeEscribir', index: 'puedeEscribir', width: 40, align: 'center', editable: true, formatter: 'checkbox', editoptions: { value: '1:0' }, formatoptions: { disabled: false } },
            { name: 'puedeBorrar', index: 'puedeBorrar', width: 30, align: 'center', formatter: 'checkbox', editable: true, editoptions: { value: '1:0' }, formatoptions: { disabled: false } },
            { name: 'Fecha_Creacion', index: 'Fecha_Creacion', width: 65, align: 'center' },
            { name: 'Fecha_Actualizacion', index: 'Fecha_Actualizacion', width: 65, align: 'Left' },
            { name: 'ActualizadoPor', index: 'ActualizadoPor', width: 80, align: 'Left', hidden: true },
            { name: 'ActualizadoPorNombre', index: 'ActualizadoPorNombre', width: 80, align: 'Left' }
        ],
        rowNum: 30,
        rowList: [30, 60, 90, 120, 150],
        pager: $('#' + gridPager),
        sortname: 'id',
        viewrecords: true,
        sortorder: "desc",
        loadonce: true,
        rownumWidth: 40,
        gridview: true,
        loadtext: 'Cargando datos...',
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        editurl: 'clientArray',
        cellSubmit: 'clientArray',
        rowheight: "20",
        caption: "Privilegios"
    });

    jQuery("#" + gridName).jqGrid('navGrid', '#' + gridPager, { edit: false, add: false, del: false, search: false });
}