<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Error500.aspx.cs" Inherits="SeedProject.Error500" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN CONTENT -->
	<div id="content">
		<!-- row -->
		<div class="row">
			<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
				<div class="row">
					<div class="col-sm-12">
						<div class="text-center error-box">
							<h1 class="error-text tada animated"><i class="fa fa-times-circle text-danger error-icon-shadow"></i> Error 500</h1>
							<h2 class="font-xl"><strong>Oooops, algo paso!</strong></h2>
							<br />
							<p class="lead semi-bold">
								<strong>Se le ha presentado una falla técnica. Pedimos disculpas.</strong><br><br>
								<small>
                                    Estamos trabajando en resolver este problema. Por favor espere un momento y vuelva a intentarlo.
								</small>
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