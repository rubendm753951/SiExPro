$(function () {

    $('#fileupload')
        .fileupload({
            url: '../Scripts/Upload.ashx?upload=start',
            acceptFileTypes: /(\.|\/)(csv)$/i,
            maxFileSize: 999000,
            add: function (e, data) {
                console.log('add', data);
                $("#enviosRequest").show();
                $("#enviosResult").hide();
                $('#progress').show();
                data.context = $('<div/>').appendTo('#files');
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
                console.log('success', response);
                var jsonResponse = jQuery.parseJSON(response);
                MANTENIMIENTOTARIFAS.readFile(jsonResponse['fileName'], $("#selectProveedor").val());
            },
            error: function (error) {
                $('#progress').hide();
                $('#progress div').css('width', '0%');
                alert('Ocurrio un error al subir el archivo.');
            }
        });
});