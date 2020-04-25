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

function genericAjaxWebMethod(urlParam, params, errMsg, paramAsync) {
    var resp;

    $.ajax({
        type: "POST",
        url: urlParam,
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            resp = response.d;
        },
        async: paramAsync,
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText           
            alert(errMsg + errorMessage);
        }
    });

    return resp;
}

function loadCountries() {
    var url = "stamps2.aspx/getCountries";
    var params = '{}';
    var errMsg = "Error al cargar paises: ";

    $("#selectRecCountry").empty();
    $("#selectRecCountry").get(0).options[0] = new Option("Cargando Paises...", "0");

    var response = genericCallWebMethod(url, params, errMsg, false);

    if (response.d.responseMessage == "") {
        $("#selectRecCountry").empty();
        $("#selectRecCountry").get(0).options[0] = new Option("Seleccionar Pais", "0");
        $.each(response.d.responseArray, function (index, item) {
            addOption("selectRecCountry", item.id_pais, item.nombre);
        });
    } else {
        $("#selectRecCountry").empty();
    }
}

function GetProducts(idAgencia, controlName) {
    var url = "stamps2.aspx/GetProducts";
    var params = "{IDAgencia:" + idAgencia + "}";
    var errMsg = "Error al cargar productos: ";

    $("#" + controlName).empty();
    $("#" + controlName).get(0).options[0] = new Option("Cargando datos...", "0");

    var response = genericCallWebMethod(url, params, errMsg, false);

    if (response.d.responseMessage == "") {
        $("#" + controlName).empty();
        $("#" + controlName).get(0).options[0] = new Option("Seleccionar Producto ", "0");
        $.each(response.d.responseArray, function (index, item) {
            addOption(controlName, item.Id, item.Descripcion);
        });
    } else {
        $("#" + controlName).empty();
    }
}

function loadAgencies(idUser) {
    $.ajax({
        type: "POST",
        url: "stamps2.aspx/getAgencies",
        //data: '{idUsuario:1}',
        data: '{idUsuario:' + idUser + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.responseMessage == "") {
                $("#selectAgencies").get(0).options.length = 0;
                $("#selectAgencies").get(0).options[0] = new Option("Seleccionar Agencia", "0");

                $.each(response.d.responseArray, function (index, item) {
                    $("#selectAgencies").get(0).options[$("#selectAgencies").get(0).options.length] = new Option(item.agente, item.id_agencia);
                });
            } else {
                alert(response.d.responseMessage);
            }
        },
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText  

            $("#selectAgencies").get(0).options.length = 0;

            alert("Error al cargar Agencias: " + errorMessage);
        }
    });
}

function GetSubProducts(id_agencia, producto) {
    //var id_agencia = $('#selectAgencies').val()
    $("#selectProduct").get(0).options.length = 0;
    $("#selectProduct").get(0).options[0] = new Option("Seleccionar Producto", "0");

    if (id_agencia > 0) {
        $.ajax({
            type: "POST",
            url: "stamps2.aspx/GetSubProducts",
            data: "{IDAgencia:" + id_agencia + ", Producto:'" + producto + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.d.responseMessage == "") {
                    $("#selectProduct").get(0).options.length = 0;
                    $("#selectProduct").get(0).options[0] = new Option("Seleccionar Producto", "0");

                    $.each(response.d.responseArray, function (index, item) {
                        $("#selectProduct").get(0).options[$("#selectProduct").get(0).options.length] = new Option(item.SubProd, item.Tarifa);
                    });

                    if (response.d.Balance <= 0) {
                        $("#Saldo span").css({ 'color': 'red', 'fontWeight': 'bold' });
                    } else {
                        $("#Saldo span").css({ 'color': 'black', 'fontWeight': 'bold' });
                    }

                    $('#Saldo span').html(formatCurrency(response.d.Balance, true), true);

                } else {
                    alert(response.d.responseMessage);
                }
            },
            error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
                var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText  
                $("#selectState").get(0).options.length = 0;

                alert("Error al cargar Productos: " + errorMessage);
            }
        });
    }
}

function getGathers() {
    $.ajax({
        type: "POST",
        url: "PreChequeo.aspx/GetGathers",
        //data: '{idUsuario:1}',
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d != null) {
                $("#selectRecolector").get(0).options.length = 0;
                $("#selectRecolector").get(0).options[0] = new Option("Seleccionar Vendedor", "0");

                $.each(response.d, function (index, item) {
                    $("#selectRecolector").get(0).options[$("#selectRecolector").get(0).options.length] = new Option(item.nombre, item.id_recolector);
                });
            }
        },
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText  

            $("#selectRecolector").get(0).options.length = 0;

            alert("Error al cargar Vendedores: " + errorMessage);
        }
    });
}

function displayImage() {
    $("#imgloading").show();
}

jQuery.ajaxSetup({
    beforeSend: function () {
        $("#imgloading").show();
    },
    complete: function () {
        $("#imgloading").hide();
    },
    success: function () { }
});


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
            case "#popupContact":
                popupStatusContact = 1;
                break;
            case "#popupCancelIndicium":
                popupStatusCancel = 1;
                break;
            case "#popupReprint":
                popupStatusReprint = 1;
                break;
            case "#popupAddBook":
                popupAddBook = 1;
                break;
        }
    }
}

//disabling popup with jQuery magic!
function disablePopup() {
    //disables popup only if it is enabled
    if (popupStatusContact == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupContact").fadeOut("slow");
        popupStatusContact = 0;
    }

    if (popupStatusCancel == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupCancelIndicium").fadeOut("slow");
        $("#cancelIndiciumLabel").hide();
        $("#popupCancelIndicium").hide();
        popupStatusCancel = 0;
    }

    if (popupAddBook == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupAddBook").fadeOut("slow");
        $("#AddBookLabel").hide();
        $("#popupAddBook").hide();
        popupAddBook = 0;
    }

    if (popupStatusReprint == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupReprint").fadeOut("slow");
        $("#ReprintLabel").hide();
        $("#popupReprint").hide();
        popupStatusReprint = 0;
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

}

//Format amount to currency
function formatCurrency(num, dispSym) {
    var currency;
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = '0';
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = '0' + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));

    currency = ((sign) ? '' : '-');

    if (dispSym) {
        currency = '$' + currency;
    }
    currency = currency + num + '.' + cents;

    return currency;
};

function addOption(controlName, valueParam, textParam) {
    $('#' + controlName).append($('<option>', {
        value: valueParam,
        text: textParam
    }));
}

function getRatesByAgentByProduct(idAgencia, idTarifa) {
    var tarifa = formatCurrency("0.00");

    $.ajax({
        type: "POST",
        url: "stamps2.aspx/GetRates",
        data: '{IDAgencia:' + idAgencia + ', IDTarifa:' + idTarifa + '}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.d.responseMessage == "") {
                tarifa = formatCurrency(data.d.Tarifa,false);
            } else {
                alert(data.d.responseMessage);
            }
        },
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText  
            alert("Error al obtener tarifas " + errorMessage);
        }
    });

    return tarifa;
}

function getCurrentDate() {
    var currentdate = new Date();
    var datetime =   currentdate.getDate() + "/"
                    + padString((currentdate.getMonth() + 1), 2, "0") + "/"
                    + currentdate.getFullYear() + " " +
                    + padString(currentdate.getHours(),2,"0") + ":"
                    + padString(currentdate.getMinutes(), 2, "0") + ":"
                    + padString(currentdate.getSeconds(),2, "0");

    return datetime;
}

function getCurrentShortDate() {
    var currentdate = new Date();
    var datetime = currentdate.getDate() + "/"
                    + padString((currentdate.getMonth() + 1), 2, "0") + "/"
                    + currentdate.getFullYear();

    return datetime;
}

function padString(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function formatJsonDate(jsonDate, includeMinutes) {
    var formatted = '';

    if (jsonDate != null && jsonDate != '') {
        var date = new Date(parseInt(jsonDate.substr(6)));
        formatted = ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
            ("0" + date.getDate()).slice(-2) + "-" +
            date.getFullYear();

        if (includeMinutes == true) {
            formatted += " " + date.getHours() + ":" + date.getMinutes();
        }
    }

    return formatted;
}

function addDaysToDate(str, days) {
    str = str.split(/\D+/);
    str = new Date(str[2], str[0] - 1, (parseInt(str[1]) + days));
    return MMDDYYYY(str);
}

function MMDDYYYY(str) {
    var ndateArr = str.toString().split(' ');
    var Months = 'Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec';
    return ndateArr[3] + '-' + (parseInt(Months.indexOf(ndateArr[1]) / 4) + 1) + '-' + ndateArr[2];
}

function FindByManifiestoByAgencia() {
    var manifiesto = $('#txtManifiesto').val();
    var idAgencia = $('#selectAgencies').val();
    var found = true;

    var url = "PreChequeo.aspx/FindByManifiestoByAgencia";
    var params = "{manifiesto:" + manifiesto + ", idAgencia:" + idAgencia + "}";
    var errMsg = "Error al verificar guia: ";

    var response = genericAjaxWebMethod(url, params, errMsg, false);
    if (response != null) {
        if (response == false) {
            alert("El manifiesto no se encuentra asignado a la agencia seleccionada.");
            found = false;
        }
    }

    return found;
}