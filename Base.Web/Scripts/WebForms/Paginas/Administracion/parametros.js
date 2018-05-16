//CONTROLES
var grvParametros;
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
    var otable = grvParametros.DataTable({
        //"bFilter": false,
        //"bInfo": false,
        //"bLengthChange": false
        //"bAutoWidth": false,
        //"bPaginate": false,
        //"bStateSave": true // saves sort state using localStorage
        "order": [[1, "asc"]],
        "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'<'toolbar'>>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
        "oLanguage": {
            "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
        },
        "autoWidth": true,
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_fixed_column) {
                responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper(grvParametros, breakpointDefinition);
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_fixed_column.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_fixed_column.respond();
        },
        columnDefs: [{
            targets: 0,
            orderable: false
        }]
    });
};