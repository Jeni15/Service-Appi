function mostrarConfirm(btnEliminar) {
    $.SmartMessageBox({
        title: "Eliminando Registro!",
        content: "Esta seguro que desea eliminar este registro?",
        buttons: '[No][Si]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Si") {
            $(btnEliminar).click();
        }
    });
};

function iniciarControles(txtNombre, txtDescripcion, txtAlias) {
    var errorClass = 'invalid';
    var errorElement = 'em';

    var validation = {
        errorClass: errorClass,
        errorElement: errorElement,
        highlight: function (element) {
            $(element).parent().removeClass('state-success').addClass("state-error");
            $(element).removeClass('valid');
        },
        unhighlight: function (element) {
            $(element).parent().removeClass("state-error").addClass('state-success');
            $(element).addClass('valid');
        },

        rules: {},

        messages: {},

        // Do not change code below
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        }
    };

    validation.rules[txtNombre] = { required: true };
    validation.rules[txtDescripcion] = { required: true };
    validation.rules[txtAlias] = { required: true };
    validation.messages[txtNombre] = "Por favor escriba un nombre al Set";
    validation.messages[txtDescripcion] = "Debe colocar una descripcion";
    validation.messages[txtAlias] = "El alias es requerido";

    $('#masterForm').validate(validation);
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
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'<'toolbar'>>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>',
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
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