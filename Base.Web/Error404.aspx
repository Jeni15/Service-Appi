<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Error404.aspx.cs" Inherits="SeedProject.Error404" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN CONTENT -->
	<div id="content">
		<!-- row -->
		<div class="row">
			<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
				<div class="row">
					<div class="col-sm-12">
						<div class="text-center error-box">
							<h1 class="error-text tada animated"><i class="fa fa-times-circle text-danger error-icon-shadow"></i> Error 404</h1>
							<h2 class="font-xl"><strong><i class="fa fa-fw fa-warning fa-lg text-warning"></i> Página <u>No</u> Encontrada</strong></h2>
							<br />
							<p class="lead">
                                La página solicitada no pudo ser encontrada, consulte al administrador o intente nuevamente. Use el botón <b>Atrás</b> de su navegador para navegar a la pagina donde se encontraba antes.
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- end row -->
	</div>
	<!-- END MAIN CONTENT -->
</asp:Content>