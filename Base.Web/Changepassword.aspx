<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="SeedProject.Changepassword" %>

<!DOCTYPE html>

<html lang="en-us" id="extr-page">
	<head>
		<meta charset="utf-8">
		<title>Pensemos SI</title>
		<meta name="description" content="">
		<meta name="author" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
		
		<!-- #CSS Links -->
		<!-- Basic Styles -->
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/bootstrap.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/font-awesome.min.css">

		<!-- SmartAdmin Styles : Caution! DO NOT change the order -->
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/smartadmin-production-plugins.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/smartadmin-production.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/smartadmin-skins.min.css">

		<!-- SmartAdmin RTL Support -->
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/smartadmin-rtl.min.css"> 

		<!-- We recommend you use "your_style.css" to override SmartAdmin
		     specific styles this will also ensure you retrain your customization with each SmartAdmin update.
		<link rel="stylesheet" type="text/css" media="screen" href="Content/css/your_style.css"> -->

		<!-- #FAVICONS -->
		<link rel="shortcut icon" href="Content/img/favicon/favicon.ico" type="image/x-icon">
		<link rel="icon" href="Content/img/favicon/favicon.ico" type="image/x-icon">

		<!-- #GOOGLE FONT -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">

	</head>
	
	<body class="animated fadeInDown">
		<header id="header">
            <div id="logo-group">
				<span id="logo">
                    <img src="img/logo.png" alt="PensemosSI">
				</span>
			</div>
			<span id="extr-page-header-space">
                <span class="hidden-mobile hidden-xs">Necesita un Usuario?</span>
                <a href="Register.aspx" class="btn btn-danger">Crear Usuario</a>
			</span>
		</header>

		<div id="main" role="main">
			<!-- MAIN CONTENT -->
			<div id="content" class="container">
				<div class="row">
					<div class="col-xs-12 col-sm-12 col-md-7 col-lg-8 hidden-xs hidden-sm">
						<h1 class="txt-color-red login-header-big">Pensemos SI</h1>
						<div class="hero">
							<div class="pull-left login-desc-box-l">
								<h4 class="paragraph-header">Contribuir al éxito del sector industrial con soluciones que optimicen su gestión y le generen valor.</h4>
								<div class="login-app-icons">
									<a href="javascript:void(0);" class="btn btn-danger btn-sm">Boton 1</a>
									<a href="javascript:void(0);" class="btn btn-danger btn-sm">Boton 2</a>
								</div>
							</div>
							<img src="img/demo/iphoneview.png" class="pull-right display-image" alt="" style="width:210px">
						</div>


					</div>
					<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">
						<div class="well no-padding">
							<form id="loginform" class="smart-form client-form" runat="server">
								<header>
									Cambio de Clave
								</header>
								<fieldset>									
									<section>
										<label class="label">Ingrese su Usuario</label>
										<label class="input">
                                            <i class="icon-append fa fa-user"></i>
                                            <asp:TextBox ID="username" runat="server" placeholder=""></asp:TextBox>
											<b class="tooltip tooltip-top-right">
                                                <i class="fa fa-user txt-color-teal"></i>
                                                Ingrese su Usuario
											</b>
										</label>
										<div class="note">
											<a href="Login.aspx">Recorde mi clave!</a>
										</div>
									</section>
                                    <section>
										<span class="timeline-seperator text-center text-primary" />
                                        <span class="font-sm">Seguridad</span> 
									</section>
                                    <section>
										<label class="label">Su Contraseña Actual</label>
										<label class="input">
                                            <i class="icon-append fa fa-user"></i>
                                            <asp:TextBox ID="currentpassword" runat="server" TextMode="Password" placeholder=""></asp:TextBox>
											<b class="tooltip tooltip-top-right">
                                                <i class="fa fa-user txt-color-teal"></i>
                                                Ingrese su Contraseña Actual
											</b>
										</label>
									</section>
                                    <section>
										<label class="label">Su Nueva Contraseña</label>
										<label class="input">
                                            <i class="icon-append fa fa-user"></i>
                                            <asp:TextBox ID="newpassword" runat="server" TextMode="Password" placeholder=""></asp:TextBox>
											<b class="tooltip tooltip-top-right">
                                                <i class="fa fa-user txt-color-teal"></i>
                                                Ingrese su Nueva Contraseña
											</b>
										</label>
									</section>
                                    <section>
										<label class="label">Confirme Su Nueva Contraseña</label>
										<label class="input">
                                            <i class="icon-append fa fa-user"></i>
                                            <asp:TextBox ID="newpasswordconfirm" runat="server" TextMode="Password" placeholder=""></asp:TextBox>
											<b class="tooltip tooltip-top-right">
                                                <i class="fa fa-user txt-color-teal"></i>
                                                Confirme su Nueva Contraseña
											</b>
										</label>
									</section>
								</fieldset>
								<footer>
									<%--<button type="submit" class="btn btn-primary">
										<i class="fa fa-refresh"></i>Reiniciar Clave
									</button>--%>

                                    <asp:Button ID="btnRegistrar" runat="server" Text="Cambiar" CssClass="btn btn-primary"
                                        CausesValidation="true" ValidationGroup="approvalgroup" OnClick="CambiarClave_Click" OnClientClick="return $('#loginform').valid();" />
						
                      

								</footer>
							</form>
						</div>
						
						<h5 class="text-center"> - O ingrese usando -</h5>
															
						<ul class="list-inline text-center">
							<li>
								<a href="javascript:void(0);" class="btn btn-primary btn-circle"><i class="fa fa-facebook"></i></a>
							</li>
							<li>
								<a href="javascript:void(0);" class="btn btn-info btn-circle"><i class="fa fa-twitter"></i></a>
							</li>
							<li>
								<a href="javascript:void(0);" class="btn btn-warning btn-circle"><i class="fa fa-linkedin"></i></a>
							</li>
						</ul>
					</div>
				</div>
			</div>

		</div>

		<!--================================================== -->	

	    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
	    <script src="//ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
		<script> if (!window.jQuery) { document.write('<script src="Content/js/libs/jquery-3.2.1.min.js"><\/script>');} </script>

	    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
		<script> if (!window.jQuery.ui) { document.write('<script src="Content/js/libs/jquery-ui.min.js"><\/script>');} </script>

		<!-- IMPORTANT: APP CONFIG -->
		<script src="Content/js/app.config.js"></script>

		<!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
		<script src="Content/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

		<!-- BOOTSTRAP JS -->		
		<script src="Content/js/bootstrap/bootstrap.min.js"></script>

		<!-- JQUERY VALIDATE -->
		<script src="Content/js/plugin/jquery-validate/jquery.validate.min.js"></script>
		
		<!-- JQUERY MASKED INPUT -->
		<script src="Content/js/plugin/masked-input/jquery.maskedinput.min.js"></script>
		
		<!--[if IE 8]>
			
			<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
			
		<![endif]-->

		<!-- MAIN APP JS FILE -->
		<script src="Content/js/app.min.js"></script>

		<script>

            runAllForms();

            // Validation
            $(function () {
                // Validation
                $("#loginform").validate({

                    // Rules for form validation
                    rules: {
                        username: {
                            required: true
                        },                        
                        currentpassword: {
                            required: true,
                            minlength: 3,
                            maxlength: 20
                        },
                        newpassword: {
                            required: true,
                            minlength: 3,
                            maxlength: 20
                        },
                        newpasswordconfirm: {
                            required: true,
                            minlength: 3,
                            maxlength: 20,
                            equalTo: '#newpassword'
                        }
                    },

                    // Messages for form validation
                    messages: {
                        username: {
                            required: 'Please enter your username'
                        },                       
                        currentpassword: {
                            required: 'Please enter your password'
                        },
                        newpassword: {
                            required: 'Please enter your password'
                        },
                        newpasswordconfirm: {
                            required: 'Please enter your password one more time',
                            equalTo: 'Please enter the same password as above'
                        }

                    },



                    // Do not change code below
                    errorPlacement: function (error, element) {
                        error.insertAfter(element.parent());
                    }
                });
            });

		</script>

	</body>
</html>