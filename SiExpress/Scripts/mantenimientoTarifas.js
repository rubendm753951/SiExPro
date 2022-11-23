/* This is a template for page-related javascript */
var MANTENIMIENTOTARIFAS = (function ($) { // parameters relate to scope
    /*  Hoisting Private variables */
    var gridEstafetaColumns = [
        "Id", "Agencia", "Zona", "Cuenta", "Peso Limite Inferior", "Peso Limite Superior", "Precio", "Precio Por Kilo", "Precio Kilo Adicional",
        "Precio Dia Sig", "Precio Kilo Dia Sig", "Precio Kilo Adicional Dia Sig", "Precio Gombar", "Precio Kilo Gomabr", "Precio Kilo Adicional Gombar", "Desactivado"
    ];

    var gridEstafetaColModel = [
        { name: "ID_TARIFAS_AGENCIA_ESTAFETA", index: "Id", width: 100, align: "Left", sorttype: "int", hidden: true },
        { name: "ID_AGENCIA", index: "Agencia", width: 100, align: "Left" },
        { name: "ID_ZONA", index: "Zona", width: 100, align: "Left" },
        { name: "ID_CUENTA", index: "Cuenta", width: 100, align: "Left" },
        { name: "PESO_LIMITE_INFERIOR", index: "PesoLimiteInferior", width: 100, align: "Left" },
        { name: "PESO_LIMITE_SUPERIOR", index: "PesoLimiteSuperior", width: 100, align: "Left" },
        { name: "PRECIO", index: "Precio", width: 100, align: "Left" },
        { name: "PRECIO_POR_KILO", index: "PrecioPorKilo", width: 100, align: "Left", formatter: "checkbox", editoptions: { value: 'true:false' }},
        { name: "PRECIO_KILO_ADICIONAL", index: "PrecioKiloAdicional", width: 100, align: "Left"},
        { name: "PRECIO_DIA_SIGUIENTE", index: "PrecioDiaSig", width: 100, align: "Left"},
        { name: "PRECIO_POR_KILO_DIA_SIGUIENTE", index: "PrecioKiloDiaSig", width: 100, align: "Left", formatter: "checkbox", editoptions: { value: 'true:false' } },
        { name: "PRECIO_KILO_ADICIONAL_DIA_SIGUIENTE", index: "PrecioKiloAdicionalDiaSig", width: 100, align: "Left" },
        { name: "PRECIO_GOMBAR", index: "PrecioGombar", width: 100, align: "Left" },
        { name: "PRECIO_POR_KILO_GOMBAR", index: "PrecioKiloGomabr", width: 100, align: "Left", formatter: "checkbox", editoptions: { value: 'true:false' } },
        { name: "PRECIO_KILO_ADICIONAL_GOMBAR", index: "PrecioKiloAdicionalGombar", width: 100, align: "Left" },
        { name: "IsDeleted", index: "Desactivado", width: 100, align: "Left" }
    ];

    var gridFedexColumns = [
        "Id", "Agencia", "Cuenta", "Servicio", "Peso Limite Inferior", "Peso Limite Superior", "Precio", "Precio Por Kilo", "Precio Kilo Adicional",
        "Precio Area Extendida", "Precio Exceso Dimensiones", "Desactivado"
    ];

    var gridFedexColModel = [
        { name: "ID", index: "Id", width: 100, align: "Left", sorttype: "int", hidden: true },
        { name: "ID_AGENCIA", index: "Agencia", width: 100, align: "Left" },
        { name: "ID_CUENTA", index: "Cuenta", width: 100, align: "Left" },
        { name: "SERVICE_ID", index: "Servicio", width: 100, align: "Left" },
        { name: "PESO_LIMITE_INFERIOR", index: "PesoLimiteInferior", width: 100, align: "Left" },
        { name: "PESO_LIMITE_SUPERIOR", index: "PesoLimiteSuperior", width: 100, align: "Left" },
        { name: "PRECIO", index: "Precio", width: 100, align: "Left" },
        { name: "PRECIO_KILO", index: "PrecioPorKilo", width: 100, align: "Left", formatter: "checkbox", editoptions: { value: 'true:false' } },
        { name: "PRECIO_KILO_ADICIONAL", index: "PrecioKiloAdicional", width: 100, align: "Left" },
        { name: "PRECIO_AREA_EXTENDIDA", index: "PrecioAreaExtendida", width: 100, align: "Left" },
        { name: "PRECIO_EXCESO_DIMENSIONES", index: "PrecioExcesoDimensiones", width: 100, align: "Left" },        
        { name: "IsDeleted", index: "Desactivado", width: 100, align: "Left" }
    ];

    var gridDraftColumns = [
        "Id", "Agencia", "Zona", "Cuenta", "Servicio", "Peso Limite Inferior", "Peso Limite Superior", "Precio", "Precio Por Kilo", "Precio Kilo Adicional",
        "Precio Area Extendida", "Precio Exceso Dimensiones", "Desactivado"
    ];

    var gridDraftColModel = [
        { name: "ID", index: "Id", width: 100, align: "Left", sorttype: "int", hidden: true },
        { name: "ID_AGENCIA", index: "Agencia", width: 100, align: "Left" },
        { name: "ID_ZONA", index: "Zona", width: 100, align: "Left" },
        { name: "ID_CUENTA", index: "Cuenta", width: 100, align: "Left" },
        { name: "SERVICE_ID", index: "Servicio", width: 100, align: "Left" },
        { name: "PESO_LIMITE_INFERIOR", index: "PesoLimiteInferior", width: 100, align: "Left" },
        { name: "PESO_LIMITE_SUPERIOR", index: "PesoLimiteSuperior", width: 100, align: "Left" },
        { name: "PRECIO", index: "Precio", width: 100, align: "Left" },
        { name: "PRECIO_KILO", index: "PrecioPorKilo", width: 100, align: "Left"},
        { name: "PRECIO_KILO_ADICIONAL", index: "PrecioKiloAdicional", width: 100, align: "Left" },
        { name: "PRECIO_AREA_EXTENDIDA", index: "PrecioAreaExtendida", width: 100, align: "Left" },
        { name: "PRECIO_EXCESO_DIMENSIONES", index: "PrecioExcesoDimensiones", width: 100, align: "Left" },
        { name: "IsDeleted", index: "Desactivado", width: 100, align: "Left" }
    ];

    var gridPaqueteExpressColumns = [
        "Id", "Agencia", "Tarifa", "Kg Start", "Kg End", "Cum Start", "Cum End", "Precio Economico", "Dia Siguiente Base", "Dia Siguiente Kilo", "Desactivado"
    ];

    var gridPaqueteExpressColModel = [
        { name: "ID_TARIFAS_AGENCIA_PAQUETE_EXPRESS", index: "Id", width: 100, align: "Left", sorttype: "int", hidden: true },
        { name: "ID_AGENCIA", index: "Agencia", width: 100, align: "Left" },
        { name: "Tarifa", index: "Tarifa", width: 100, align: "Left" },
        { name: "KG_start", index: "KgStart", width: 100, align: "Left" },
        { name: "KG_End", index: "KgEnd", width: 100, align: "Left" },
        { name: "CUM_start", index: "CumStart", width: 100, align: "Left" },
        { name: "CUM_end", index: "CumEnd", width: 100, align: "Left" },
        { name: "PRECIO_ECONOMICO", index: "PrecioEconomico", width: 100, align: "Left" },
        { name: "DIA_SIGUIENTE_BASE", index: "DiaSiguienteBase", width: 100, align: "Left" },
        { name: "DIA_SIGUIENTE_KILO", index: "DiaSiguienteKilo", width: 100, align: "Left" },
        { name: "is_deleted", index: "IsDeleted", width: 100, align: "Left" }
    ];

    var gridDliverColumns = [
        "Id", "Agencia", "Zona", "Cuenta", "Servicio", "Peso Limite Inferior", "Peso Limite Superior", "Precio", "Precio Por Kilo", "Precio Kilo Adicional",
        "Precio Area Extendida", "Precio Exceso Dimensiones", "Desactivado"
    ];

    var gridDliverColModel = [
        { name: "ID", index: "Id", width: 100, align: "Left", sorttype: "int", hidden: true },
        { name: "ID_AGENCIA", index: "Agencia", width: 100, align: "Left" },
        { name: "ID_ZONA", index: "Zona", width: 100, align: "Left" },
        { name: "ID_CUENTA", index: "Cuenta", width: 100, align: "Left" },
        { name: "SERVICE_ID", index: "Servicio", width: 100, align: "Left" },
        { name: "PESO_LIMITE_INFERIOR", index: "PesoLimiteInferior", width: 100, align: "Left" },
        { name: "PESO_LIMITE_SUPERIOR", index: "PesoLimiteSuperior", width: 100, align: "Left" },
        { name: "PRECIO", index: "Precio", width: 100, align: "Left" },
        { name: "PRECIO_KILO", index: "PrecioPorKilo", width: 100, align: "Left" },
        { name: "PRECIO_KILO_ADICIONAL", index: "PrecioKiloAdicional", width: 100, align: "Left" },
        { name: "PRECIO_AREA_EXTENDIDA", index: "PrecioAreaExtendida", width: 100, align: "Left" },
        { name: "PRECIO_EXCESO_DIMENSIONES", index: "PrecioExcesoDimensiones", width: 100, align: "Left" },
        { name: "IsDeleted", index: "Desactivado", width: 100, align: "Left" }
    ];

    function updateTarifas() {
        var rows = jQuery("#gridTarifas").jqGrid('getRowData');        
        if (rows.length > 0) {
            if ($("#selectAgencies").val() > 0) {
                var agenteSeleccionado = $("#selectAgencies").val();                
                var tarifas = rows.filter(x => x.ID_AGENCIA == agenteSeleccionado);
                console.log(tarifas, agenteSeleccionado)
                if (tarifas && tarifas.length > 0) {
                    var url = "mantenimiento_tarifas.aspx/ActualizasTarifas";
                    var params = '{tarifas:' + JSON.stringify(tarifas) + ',idProveedor:' + $("#selectProveedor").val() + ',idAgente:' + agenteSeleccionado + '}';
                    var errMsg = "Error al obtener agentes: ";

                    var response = genericCallWebMethod(url, params, errMsg, false);
                    if (response && response.responseMessage) {
                        alert(response.responseMessage);
                    }
                } else {
                    alert('No hay tarifas a actualizar para el agente seleccionado');
                }
            } else {
                alert('Seleccione un agente');
            }            
        } else {
            alert('No hay tarifas para importar');
        }        
    }

    function loadAgents() {
        var url = "mantenimiento_tarifas.aspx/GetAgentes";
        var params = '{}';
        var errMsg = "Error al obtener agentes: ";

        var response = genericCallWebMethod(url, params, errMsg, false);        
        if (response.responseArray) {
            var agentes = response.responseArray.map(x => {
                return {
                    id: x.IdAgente,
                    description: `${x.IdAgente} - ${x.Nombre}`
                };
            });            
            fillDropDown("#selectAgencies", agentes, "Agente", true, false, false);
        }        
    }

    function fillDropDown(controlName, values, controlText, insertFirstValue, hasAdditional, hasAdditional2) {
        if (insertFirstValue == null) {
            insertFirstValue = true;
        }

        if (hasAdditional == null) {
            hasAdditional = false;
        }

        if (hasAdditional2 == null) {
            hasAdditional2 = false;
        }
        $(controlName).empty();
        $(controlName).get(0).options[0] = new Option("Cargando " + controlText + "...", "0");
        if (values != null) {
            $(controlName).empty();
            if (insertFirstValue == true) {
                $(controlName).get(0).options[0] = new Option("Seleccionar " + controlText, "0");
            }
            $.each(values, function (index, item) {
                if (hasAdditional == true) {
                    addOption(controlName, item.id, item.description, null, item.additional, item.additional2);
                } else {
                    addOption(controlName, item.id, item.description, null);
                }
            });
        } else {
            $(controlName).empty();
            $(controlName).get(0).options[0] = new Option(controlText + "...", "0");
        }
    }

    function addOption(controlName, valueParam, textParam, toolTip, additional, additional2) {
        if (toolTip == null) {
            toolTip = "";
        }

        if (additional == null) {
            additional = "";
        }

        if (additional2 == null) {
            additional2 = "";
        }

        $(controlName).append($("<option></option>")
            .attr("value", valueParam)
            .attr("data-toggle", "tooltip")
            .attr("data-placement", "tooltip")
            .attr("data-additional", additional)
            .attr("data-additional2", additional2)
            .attr("title", toolTip)
            .text(textParam));
    }

    function createEmptyGrid(idProveedor) {
        var gridName = "#gridTarifas";
        var pagerName = "#pagerTarifas";
        var sortName = "Id";
        var sortOrder = "asc";
        var gridColumns = [];
        var gridColumnsModel = [];        

        switch (idProveedor) {
            case '10': //fedex                
                gridColumns = gridFedexColumns;
                gridColumnsModel = gridFedexColModel;
                break;
            case '30': //estafeta                
                gridColumns = gridEstafetaColumns;
                gridColumnsModel = gridEstafetaColModel;                
                break;
            case '40': //pe
                gridColumns = gridPaqueteExpressColumns;
                gridColumnsModel = gridPaqueteExpressColModel;
                break;
            case '50': //draft
                gridColumns = gridDraftColumns
                gridColumnsModel = gridDraftColModel
                break;
            case '51': //dliver
                gridColumns = gridDliverColumns
                gridColumnsModel = gridDliverColModel
                break;
            default:
        }               
        createGrid(gridName, pagerName, "", gridColumns, gridColumnsModel, sortName, sortOrder, "1100", "450");
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
                resp = response.d;
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

    function createGrid(gridName, pagerName, myData, gridColNames, gridColModel, sortName, sortOrder, width, height, multiselect, rownumbers, rowNum) {
        if (width == null) {
            width = '600';
        }
        if (height == null) {
            height = '1020';
        }
        if (multiselect == null) {
            multiselect = false;
        }
        if (rownumbers == null) {
            rownumbers = true;
        }
        if (rowNum == null) {
            rowNum = 100;
        }

        $(gridName).jqGrid('GridUnload');
        $(gridName).jqGrid('clearGridData');
        $(gridName).trigger("reloadGrid");
        $(gridName).jqGrid(
            {
                datatype: 'local',
                data: myData,
                async: false,
                jsonReader: {
                    root: 'rows',
                    page: 'page',
                    repeatitems: false
                },
                colNames: gridColNames,
                colModel: gridColModel,
                pager: jQuery(pagerName),
                rowNum: rowNum,
                hidegrid: false,
                height: height,
                width: width,
                recordtext: "{0} - {1} de {2} elementos",
                emptyrecords: 'No hay resultados',
                sortname: sortName,
                sortorder: sortOrder, //Default SortOrder.
                viewrecords: true,
                rowheight: "100",
                rownumbers: rownumbers,
                gridview: true,
                multiselect: multiselect,
                view: {
                    caption: "Ver Registro",
                    bClose: "Cerrar"
                },
                loadError: function (jqXHR, textStatus, errorThrown) {
                    alert('HTTP status code: ' + jqXHR.status + '\n' +
                        'textStatus: ' + textStatus + '\n' +
                        'errorThrown: ' + errorThrown);
                    alert('HTTP message body (jqXHR.responseText): ' + '\n' + jqXHR.responseText);
                }
            });
        $(gridName).jqGrid('navGrid', pagerName, { edit: false, add: false, del: false, search: false, refresh: false, viewrecords: true });
    }

    // Public section
    return { // NOTE: module export breaks chaining
        // set init as first property
        init: function () {
            if (!this.listenersInitialized) {
                // Attach spinner to AJAX requests               
                
                createEmptyGrid('10');
                loadAgents();

                $("#selectProveedor").bind("change", function () {                    
                    createEmptyGrid($("#selectProveedor").val());
                });

                $("#btnUploadTarifas").on("click", function (e) {
                    e.preventDefault();
                    updateTarifas();
                });
            }
        },

        name: "MANTENIMIENTOTARIFAS",
        listenersInitialized: false,
        readFile: function (fileName, idProvider) {            
            var gridName = "#gridTarifas";
            var pagerName = "#pagerTarifas";

            var url = "mantenimiento_tarifas.aspx/readFile";
            var params = '{fileFullName:"' + fileName + '",proveedor:' + idProvider + '}';
            var errMsg = "Error al leer archivo: ";

            var response = genericCallWebMethod(url, params, errMsg, false);
            if (response.responseArray.length > 1) {
                var sortName = "Id";
                var sortOrder = "asc";
                var gridColumns = [];
                var gridColumnsModel = [];

                switch (idProvider) {
                    case '10': //fedex                
                        gridColumns = gridFedexColumns;
                        gridColumnsModel = gridFedexColModel;
                        break;
                    case '30': //estafeta                
                        gridColumns = gridEstafetaColumns;
                        gridColumnsModel = gridEstafetaColModel;
                        break;
                    case '40': //pe
                        gridColumns = gridPaqueteExpressColumns;
                        gridColumnsModel = gridPaqueteExpressColModel;
                        break;
                    case '50': //draft
                        gridColumns = gridDraftColumns
                        gridColumnsModel = gridDraftColModel
                        break;
                    case '51': //dliver
                        gridColumns = gridDliverColumns
                        gridColumnsModel = gridDliverColModel
                        break;
                    default:
                }

                createGrid(gridName, pagerName, response.responseArray, gridColumns, gridColumnsModel, sortName, sortOrder, "1500", "450");

                $(gridName)
                    .jqGrid("setGridParam",
                        {
                            ondblClickRow: function (rowid, iRow, iCol, e) {
                                var data = $(gridName).getRowData(rowid);                                
                                jQuery(gridName)
                                    .jqGrid("viewGridRow", rowid, { caption: "Ver Registro", bClose: "Cerrar" });
                                
                            }
                        });
            } else {
                alert("Ocurrio un error al leer archivo o el archivo esta vacio.");
            }
        }
    };
}(jQuery)); // Pass globals to inner object scope as parameters to avoid lookups and set specific usages

// Call object(s) init to be executed first
$(document).ready(function () {
    MANTENIMIENTOTARIFAS.init();
});
