function iniControles(txtNombre, txtDescripcion, txtAlias, txtDimension, ddlDimension, txtNombreArchivo) {
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
    validation.rules[txtDimension] = { required: true };
    validation.rules[ddlDimension] = { required: true };
    validation.messages[txtNombre] = "Por favor escriba un nombre del Parametro";
    validation.messages[txtDescripcion] = "Debe colocar una descripci&#243n";
    validation.messages[txtAlias] = "El alias es requerido";
    validation.messages[txtDimension] = "Debe especificar la dimensi&#243n";
    validation.messages[ddlDimension] = "Debe seleccionar un Set";

    validation.rules[txtNombreArchivo] = { required: true };
    validation.messages[txtNombreArchivo] = "Debe colocar un nombre de archivo";

    $('#masterForm').validateWebForm(validation);
};

function establecerDimensionesControles(txtDimension, hddDimensionVals) {
    if (txtDimension != 'undefined' && txtDimension.val() != '') {
        var numDimensiones = txtDimension.val();
        var dimensionVals = [];
        var i = 0;

        if (numDimensiones >= 2 && numDimensiones <= 10 ) {

            if (hddDimensionVals.val() != '') {
                dimensionVals = hddDimensionVals.val().split(".");
            };

            $("#divDimensiones").empty();

            for (i = 0; i < numDimensiones; i++) {
                var divDimensionControles = $("#divDimensionControles").clone().show();
                divDimensionControles.find("label[id=lblDimension]").text(divDimensionControles.find("label[id=lblDimension]").text() + ' ' + (i + 1));

                if (dimensionVals.length == 0) {
                    dimensionVals.push('0');
                }
                else {
                    divDimensionControles.find("select").val(dimensionVals[i]);
                };

                divDimensionControles.appendTo($("#divDimensiones"));
            };

            hddDimensionVals.val(dimensionVals);
        };
    };
};

function guardarDimensionesValores(hddDimensionVals) {
    var ddlDimensiones = $("#divDimensiones").find("select");
    var dimensionVals = '';

    $.each(ddlDimensiones, function (i, item) {
        dimensionVals = dimensionVals + $(item).val() + '.';

        //dimensionVals = dimensionVals + $(item + " option:selected").html() + ',';
        //$("#selectId option:selected").html();

        //var indice = ddlDimensiones.selectedIndex;
        //var resultado = ddlDimensiones.options[indice].text;
        //dimensionVals = dimensionVals + resultado + '.';
    });

    dimensionVals = dimensionVals.slice(0, -1);
    $(hddDimensionVals).val(dimensionVals);
};
