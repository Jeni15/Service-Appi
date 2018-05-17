//CONTROLES
var grvDatos;
var txtParametro;
var txtDescripcion;
var txtAlias;
//---------

function iniciarControles() {
    var errorClass = 'invalid';
    var errorElement = 'em';

    var $checkForm = $('#masterForm').validate({
        errorClass		: errorClass,
        errorElement	: errorElement,
        highlight: function(element) {
            $(element).parent().removeClass('state-success').addClass("state-error");
            $(element).removeClass('valid');
        },
        unhighlight: function(element) {
            $(element).parent().removeClass("state-error").addClass('state-success');
            $(element).addClass('valid');
        },

        // Rules for form validation
        rules : {
            txtParametro : {
                required : true
            },
            txtDescripcion : {
                required : true
            },
            txtAlias : {
                required : true
            }
        },
	
        // Messages for form validation
        messages : {
            txtParametro : {
                required : 'Please enter the parameter name'
            },
            txtDescripcion : {
                required : 'Please enter the description'
            },
            txtAlias : {
                required : 'Please enter the alias'
            }
        },
	
        // Do not change code below
        errorPlacement : function(error, element) {
            error.insertAfter(element.parent());
        }
    });
}

function iniDataTable() {
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

function bredcrumb() {
    var url = location.pathname.substring(location.pathname.lastIndexOf("/") + 1);
    var currentItem = $("li").find("[href$='" + url + "']");
    var path = $("#hdBasePath").val();
    var liParentCount = currentItem.parents("li").length - 1;

    $(".breadcrumb").append('<li><a href="' + path + ' ">Inicio</a></li>');

    $(currentItem.parents("li").get().reverse()).each(function () {
        path += "/" + $(this).children("a").text().replace(/\s+/g, "");
        $(".breadcrumb").append('<li><a>' + $(this).children("a").text() + '</a></li>');
        if (liParentCount != 0) {
            $(this).addClass("active");
        }
        liParentCount--;
    });

    $(".breadcrumb li:last-child").addClass("active").addClass(" font-bold");
    //$(".bredcrumb").html(path);
};