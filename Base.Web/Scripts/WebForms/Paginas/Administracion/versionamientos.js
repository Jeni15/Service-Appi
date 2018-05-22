function iniControles(txtNombre, txtDescripcion, txtAlias) {
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
        messages : {},

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

    $('#masterForm').validateWebForm(validation);
};