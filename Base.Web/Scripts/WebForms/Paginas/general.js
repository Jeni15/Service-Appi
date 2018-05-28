function mostrarMensajeEtiqueta(divMensaje, tipoMensaje, msgEtiqueta) {
    var alertType = 'alert-warning';
    var alertIcon = 'fa-check';
    var alertTitle = 'Info!';
    var alertHtml = "<div class='alert %TYPE% fade in'><button class='close' data-dismiss='alert'>&#215</button><i class='fa-fw fa %ICON%'></i><strong>%TITLE%</strong> %MENSAJE%</div>";

    $(divMensaje).empty();

    if (tipoMensaje == 'error') {
        alertType = 'alert-danger';
        alertIcon = 'fa-times';
        alertTitle = 'Error!';
    }
    else if (tipoMensaje == 'warning') {
        alertType = 'alert-warning';
        alertIcon = 'fa-warning';
        alertTitle = 'Advertencia';
    }
    else if (tipoMensaje == 'success') {
        alertType = 'alert-success';
        alertIcon = 'fa-check';
        alertTitle = 'Exito';
    }
    else if (tipoMensaje == 'info') {
        alertType = 'alert-info';
        alertIcon = 'fa-info';
        alertTitle = 'Info!';
    };

    alertHtml = alertHtml.replace("%TYPE%", alertType);
    alertHtml = alertHtml.replace("%ICON%", alertIcon);
    alertHtml = alertHtml.replace("%TITLE%", alertTitle);
    alertHtml = alertHtml.replace("%MENSAJE%", msgEtiqueta);

    $(divMensaje).html(alertHtml).show(0).delay(5000).hide('slow');
};

function mostrarMensajePopup(msgError) {
    $.SmartMessageBox({
        title: '<i class="fa fa-exclamation txt-color-red"></i> Se presento un <span class="txt-color-red"><strong>Error</strong></span>',
        content: msgError,
        buttons: '[Aceptar]'
    });
};

function mostrarConfirm(tipoMensaje, btnPostback) {
    var titulo = '';
    var mensaje = '';

    if (tipoMensaje == 'eliminar') {
        titulo = '<i class="fa fa-trash-o txt-color-orangeDark"></i> Eliminando Registro!';
        mensaje = 'Esta seguro que desea eliminar este registro?';
    };

    $.SmartMessageBox({
        title: titulo,
        content: mensaje,
        buttons: '[No][Si]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Si") {
            $(btnPostback).click();
        }
    });
};

function iniDataTable(grvDatos) {
    var responsiveHelper_datatable_fixed_column = undefined;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    /* COLUMN FILTER  */
    var otable = grvDatos.DataTable({
        //"bFilter": false,
        //"bInfo": false,
        //"bLengthChange": false
        //"bAutoWidth": false,
        "bPaginate": false,
        "responsive": true,
        //"bStateSave": true // saves sort state using localStorage
        "order": [[1, "asc"]],
        columnDefs: [{
            targets: 0,
            orderable: false
        },
        {
            className: "text-center",
            targets: "_all"
        }],
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-xs-12 col-sm-6'<'toolbar'>>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-12 col-xs-12'i><'col-xs-12 col-sm-12'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>',
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ning&#250n dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        "autoWidth": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_fixed_column) {
                responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper(grvDatos, breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_fixed_column.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_fixed_column.respond();
        }
    });
};