<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Multisets.aspx.cs" Inherits="SeedProject.Paginas.Administracion.Multisets" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
	    <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
		    <h1 class="page-title txt-color-blueDark">
			
			    <!-- PAGE HEADER -->
			    <i class="fa-fw fa fa-pencil-square-o"></i> 
				    Sistema de Información MICV
			    <span>-  
				    Configuración de Multisets
			    </span>
		    </h1>
	    </div>
    </div>

    <!-- widget grid -->
    <section id="widget-grid" class="">
	    <!-- START ROW -->
	    <div class="row">
            <div id="divMensajeMain"></div>

		    <!-- NEW COL START -->
            <asp:UpdatePanel ID="updDatos" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlDatos" runat="server">
		                <article class="col-sm-12 col-md-5">
			                <!-- Widget ID (each widget will need unique ID)-->
			                <div class="jarviswidget jarviswidget-color-greenLight" id="wid-id-2" data-widget-editbutton="false" data-widget-custombutton="false">
				                <!-- widget options:
					                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
					
					                data-widget-colorbutton="false"	
					                data-widget-editbutton="false"
					                data-widget-togglebutton="false"
					                data-widget-deletebutton="false"
					                data-widget-fullscreenbutton="false"
					                data-widget-custombutton="false"
					                data-widget-collapsed="true" 
					                data-widget-sortable="false"
					
				                -->
				                <header>
					                <span class="widget-icon">
                                        <i class="fa fa-edit"></i>
					                </span>
					                <h2>Filtros</h2>
				                </header>

                                <div>
					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
                                        <!-- This area used as dropdown edit box -->
					                </div>
					                <!-- end widget edit box -->
					
					                <!-- widget content -->
					                <div class="widget-body no-padding">
                                        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="smart-form">
                                                    <fieldset>
                                                        <div class="row">
                                                            <section class="col col-10">
										                        <label class="select">
                                                                    <asp:DropDownList ID="ddlFiltroModelos" name="filtermodels" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroModelos_SelectedIndexChanged"></asp:DropDownList>
                                                                    <i></i>
                                                                </label>
									                        </section>
                                                        </div>

                                                        <div class="row">
                                                            <section class="col col-10">
                                                                <label class="select">
                                                                    <asp:DropDownList ID="ddlFiltroVersiones" name="filterversions" runat="server"></asp:DropDownList>
                                                                    <i></i>
                                                                </label>
                                                            </section>
                                                        </div>
                                                    </fieldset>

                                                    <section class="col col-12 pull-right">
                                                        <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn-sm btn btn-primary" OnClick="btnFiltrar_Click">
                                                            <i class="fa fa-search"></i>
                                                            Filtrar
                                                        </asp:LinkButton>
                                                    </section>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </article>

                        <article class="col-sm-12 col-md-12">
                            <div class="jarviswidget jarviswidget-color-greenLight" id="wid-id-1" data-widget-editbutton="false">
				                <!-- widget options:
				                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

				                data-widget-colorbutton="false"
				                data-widget-editbutton="false"
				                data-widget-togglebutton="false"
				                data-widget-deletebutton="false"
				                data-widget-fullscreenbutton="false"
				                data-widget-custombutton="false"
				                data-widget-collapsed="true"
				                data-widget-sortable="false"

				                -->
				                <header>
					                <span class="widget-icon"> 
                                        <i class="fa fa-table"></i> 
					                </span>
					                <h2>Lista Multisets</h2>
				                </header>

				                <!-- widget div-->
				                <div>

					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
						                <!-- This area used as dropdown edit box -->

					                </div>
					                <!-- end widget edit box -->

					                <!-- widget content -->
                                    <asp:UpdatePanel ID="updGrilla" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <div class="widget-body no-padding">
                                                <asp:GridView ID="grvDatos" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-warning btn-xs" CausesValidation="false" CommandName="EditarMultiset" CommandArgument='<%#Eval("IDMultiset")%>'>
                                                                    <i class="fa fa-pencil"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-xs" CausesValidation="false" CommandName="EliminarMultiset" CommandArgument='<%#Eval("IDMultiset")%>'>
                                                                    <i class="fa fa-trash"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>

                                                            <ItemStyle Width="60px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IDMultiset" HeaderText="Id" />
                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="AliasGAMS" HeaderText="Alias GAMS" />
                                                        <asp:BoundField DataField="Dimensiones" HeaderText="Dimensiones" />
                                                        <asp:TemplateField HeaderText="Activo">
                                                            <ItemTemplate>
                                                                <label class="checkbox state-disabled">
											                        <asp:CheckBox ID="chkMultisetActivo" runat="server" Checked='<%#Eval("Activa").ToString() == "1"%>' Enabled="false" />
											                        <i></i>
                                                                </label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                    
                                                <button id="btnAgregarModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalMultiset" style="display: none;">
                                                    ShowModal
                                                </button>
                                                <button id="btnCargueMasivoModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalSetCargueMasivo" style="display: none;">
                                                    ShowModal
                                                </button>

                                                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" style="display:none" />
                                                <asp:HiddenField ID="hddIdMultiset" runat="server" Value="0" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="widget-footer">
                                        <asp:LinkButton ID="btnCargueMasivo" runat="server" CssClass="btn btn-primary" OnClick="btnCargueMasivo_Click">
                                            <i class="fa fa-upload"></i>
                                            Cargue Masivo
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-primary" OnClick="btnExportar_Click">
                                            <i class="fa fa-download"></i>
                                            Exportar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="updCargueMasivo" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlCargueMasivo" runat="server" Visible="false">
                        <article class="col-sm-12 col-md-12">
                            <div class="jarviswidget jarviswidget-color-greenLight" id="wid-id-3" data-widget-editbutton="false">
				                <!-- widget options:
				                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

				                data-widget-colorbutton="false"
				                data-widget-editbutton="false"
				                data-widget-togglebutton="false"
				                data-widget-deletebutton="false"
				                data-widget-fullscreenbutton="false"
				                data-widget-custombutton="false"
				                data-widget-collapsed="true"
				                data-widget-sortable="false"

				                -->
				                <header>
					                <span class="widget-icon">
                                        <i class="fa fa-table"></i>
					                </span>
					                <h2>Cargue Masivo Multisets</h2>
				                </header>

				                <!-- widget div-->
				                <div>

					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
						                <!-- This area used as dropdown edit box -->

					                </div>
					                <!-- end widget edit box -->

					                <asp:UpdatePanel ID="updContenidoCargueMasivo" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
				                            <div class="form-horizontal">
						                        <fieldset>
								                    <div class="form-group">
									                    <label class="col-md-1 control-label">Archivo</label>
									                    <div class="col-md-11">
										                    <input type="file" class="btn btn-default" id="upfArchivo" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel" />
									                    </div>
								                    </div>

                                                    <div class="form-group">
                                                        <asp:Panel ID="pnlMensajeCargueMasivo" runat="server" Visible="false">
                                                            <div class="alert alert-danger fade in">
				                                                <button class="close" data-dismiss="alert">
					                                                ×
				                                                </button>
				                                                <i class="fa-fw fa fa-times"></i>
				                                                <strong>Error!</strong> El archivo seleccionado no tiene una estructura valida.
			                                                </div>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="form-group text-center">
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel ID="updSetCargueMasivo" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                                <ContentTemplate>
                                                                    <!-- widget content -->
					                                                <div class="widget-body no-padding">
                                                                        <asp:GridView ID="grvCargueMasivo" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand" EmptyDataText="No se han cargado registros">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                                                <asp:BoundField DataField="AliasGAMS" HeaderText="Alias GAMS" />
                                                                                <asp:BoundField DataField="Dimensiones" HeaderText="Dimensiones" />
                                                                                <asp:TemplateField HeaderText="Cargar" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-12 text-center">
                                                                                            <label class="checkbox checkbox-inline">
											                                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
											                                                    <i></i>
                                                                                            </label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Estado">
                                                                                    <ItemTemplate>
                                                                                        <span id="spnEstado" runat="server" class="label label-warning">
                                                                                            <asp:Label ID="lblEstado" runat="server" Text="Espera"></asp:Label>
                                                                                        </span>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                        <asp:Button ID="btnCargarArchivo" runat="server" OnClick="btnCargarArchivo_Click" style="display:none" />
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnCargarArchivo" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <section class="col col-12 pull-right">
                                                            <asp:LinkButton ID="btnCargueMasivoCargar" runat="server" CssClass="btn-sm btn btn-primary disabled" OnClick="btnCargueMasivoCargar_Click" Enabled="false">
                                                                <i class="fa fa-upload"></i>
                                                                Cargar
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="btnCargueMasivoCerrar" runat="server" CssClass="btn-sm btn btn-default" OnClick="btnCargueMasivoCerrar_Click">
                                                                Cerrar
                                                            </asp:LinkButton>
                                                        </section>
                                                    </div>
                                                </fieldset>
					                        </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </article>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="updExportar" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="pnlExportar" runat="server" Visible="false">
                        <article class="col-sm-12 col-md-12">
                            <div class="jarviswidget jarviswidget-color-greenLight" id="wid-id-4" data-widget-editbutton="false">
				                <!-- widget options:
				                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

				                data-widget-colorbutton="false"
				                data-widget-editbutton="false"
				                data-widget-togglebutton="false"
				                data-widget-deletebutton="false"
				                data-widget-fullscreenbutton="false"
				                data-widget-custombutton="false"
				                data-widget-collapsed="true"
				                data-widget-sortable="false"

				                -->
				                <header>
					                <span class="widget-icon">
                                        <i class="fa fa-table"></i>
					                </span>
					                <h2>Exportar Multisets</h2>
				                </header>

				                <!-- widget div-->
				                <div>

					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
						                <!-- This area used as dropdown edit box -->

					                </div>
					                <!-- end widget edit box -->

					                <asp:UpdatePanel ID="updContenidoExportar" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
				                            <div class="form-horizontal">
						                        <fieldset>
								                    <div class="form-group text-center">
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel ID="updMultisetExportar" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                                <ContentTemplate>
                                                                    <!-- widget content -->
					                                                <div class="widget-body no-padding">
                                                                        <asp:GridView ID="grvExportar" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand" EmptyDataText="No se han cargado registros">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="IDMultiSet" HeaderText="Id" />
                                                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                                                <asp:BoundField DataField="AliasGAMS" HeaderText="Alias GAMS" />
                                                                                <asp:BoundField DataField="Dimensiones" HeaderText="Dimensiones" />
                                                                                <asp:TemplateField HeaderText="Exportar" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-12 text-center">
                                                                                            <label class="checkbox checkbox-inline">
											                                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
											                                                    <i></i>
                                                                                            </label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>

                                                    <div id="frmFile" class="form">
                                                        <div class="form-group">
                                                            <fieldset>
                                                                <div class="col-md-6" style="display:none;">
									                                <label class="col-md-2 control-label">Archivo</label>
									                                <div class="col-md-10">
										                                <div class="input-group">
													                        <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="form-control" Text="MultiSets"></asp:TextBox>
													                        <span class="input-group-addon">.xlsx</span>
												                        </div>
                                                                    </div>
									                            </div>
                                                            </fieldset>
								                        </div>

                                                        <div class="smart-form">
                                                            <section class="col col-12 pull-right">
                                                                <asp:LinkButton ID="btnExportarDescargar" runat="server" CssClass="btn-sm btn btn-primary submit frmFile" OnClick="btnExportarDescargar_Click">
                                                                    <i class="fa fa-download"></i>
                                                                    Descargar
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="btnExportarCerrar" runat="server" CssClass="btn-sm btn btn-default" OnClick="btnExportarCerrar_Click">
                                                                    Cerrar
                                                                </asp:LinkButton>
                                                            </section>
                                                        </div>
                                                    </div>
                                                </fieldset>
					                        </div>
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExportarDescargar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </article>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

    <!-- Modal -->
       <div class="modal fade" id="modalMultiset" tabindex="-1" role="dialog">
	    <div class="modal-dialog">
		    <div class="modal-content">
			    <div class="modal-header">
                    <asp:UpdatePanel ID="updModalTitulo" runat="server">
                        <ContentTemplate>
				            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					            &times;
				            </button>
				            <h4 class="modal-title">
                                <asp:Literal ID="lblModalTitulo" runat="server"></asp:Literal>
				            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
			    </div>
			    <div class="modal-body no-padding">
                    <asp:UpdatePanel ID="updModalContenido" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div id="divMensajeModal"></div>

                            <div id="frmMain" class="smart-form form">
						        <fieldset>
                                    <asp:Panel ID="pnlIdMultiset" runat="server">
                                        <section>
								            <div class="row">
									            <label class="label col col-3">Id</label>
									            <div class="col col-9">
										            <label class="input">
                                                        <asp:Label ID="lblIdMultiset" runat="server"></asp:Label>
										            </label>
									            </div>
								            </div>
							            </section>
                                    </asp:Panel>

                                    <section>
								        <div class="row">
									        <label class="label col col-3">Nombre</label>
									        <div class="col col-9">
										        <label class="input"> <i class="icon-append fa fa-cog"></i>
                                                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

							        <section>
								        <div class="row">
									        <label class="label col col-3">Descripción</label>
									        <div class="col col-9">
										        <label class="textarea">
											        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

							        <section>
								        <div class="row">
									        <label class="label col col-3">Alias</label>
									        <div class="col col-9">
										        <label class="input">
											        <asp:TextBox ID="txtAlias" runat="server" onkeyup="txtAliasOnKeyPress();"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

                                    <section>
								        <div class="row">
									        <label class="label col col-3">Dimensiones</label>
									        <div class="col col-9">
										        <label id="lblTextboxDimension" class="input" runat="server">
											        <asp:TextBox ID="txtDimension" runat="server" onkeyup="txtDimensionOnKeyPress();"></asp:TextBox>
										        </label>
									        </div>

                                            <asp:HiddenField ID="hddDimensionVals" runat="server" />
								        </div>

                                        <div id="divDimensionControles" style="display:none;">
                                            <section>
                                                <div class="row">
                                                    <label id="lblDimension" class="label col col-3">Dimensión</label>
                                                    <div class="col col-9">
                                                        <label id="lblSelectDimension" class="select" runat="server">
                                                            <asp:DropDownList ID="ddlDimension" runat="server" onchange="ddlDimensionOnChange();"></asp:DropDownList>
                                                            <i></i>
                                                        </label>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
							        </section>

                                    <div id="divDimensiones"></div>

                                    <asp:Panel ID="pnlActivo" runat="server">
                                        <section>
								            <div class="row">
									            <label class="label col col-3">Activo</label>
									            <div class="col col-9">
										            <label class="checkbox">
                                                        <asp:CheckBox ID="chkActivo" runat="server" />
											            <i></i>
										            </label>
									            </div>
								            </div>
							            </section>
                                    </asp:Panel>
						        </fieldset>
							
						        <footer>
							        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary submit frmMain" OnClick="btnGuardar_Click" OnClientClick="return $('#masterForm').valid();">
                                        <i class="fa fa-floppy-o"></i>
                                        Guardar
                                    </asp:LinkButton>

							        <button id="btnCerrarModal" type="button" class="btn btn-default" data-dismiss="modal">
								        Cancelar
							        </button>
						        </footer>
					        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
			    </div>
		    </div>
	    </div>
    </div>

    <script src='<%= ResolveUrl("~/Scripts/WebForms/Paginas/general.js") %>'></script>
    <script src='<%= ResolveUrl("~/Scripts/WebForms/Paginas/Administracion/multisets.js") %>'></script>

    <script>
        function fn_init() {
            iniControles('<%=txtNombre.UniqueID%>', '<%=txtDescripcion.UniqueID%>', '<%=txtAlias.UniqueID%>', '<%=txtDimension.UniqueID%>', '<%=ddlDimension.UniqueID%>', '<%=txtNombreArchivo.UniqueID%>');
            iniDataTable($("#<%=grvDatos.ClientID%>"));

            // custom toolbar
            $("div.toolbar").html('' +
                '<div class="text-right">' +
                    '<asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary form-control" OnClick="btnAgregar_Click">' +
                        '<i class="fa fa-plus"></i> ' +
                        'Agregar' +
                    '</asp:LinkButton>' +
                '</div>');

            iniDataTableRead($("#<%=grvCargueMasivo.ClientID%>"));
            iniDataTableRead($("#<%=grvExportar.ClientID%>"));

            $("#<%=upfArchivo.ClientID%>").on('change', function () {
                $("#<%=btnCargarArchivo.ClientID%>").click();
            });

            $("#<%=txtDimension.ClientID%>").mask("?99", { placeholder: " " });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(onEachRequest);
        };

        function txtDimensionOnKeyPress() {
            establecerDimensionesControles($("#<%=txtDimension.ClientID%>"), $("#<%=hddDimensionVals.ClientID%>"))
        };

        function ddlDimensionOnChange() {
            guardarDimensionesValores($("#<%=hddDimensionVals.ClientID%>"));
        };

        function onEachRequest(sender, args) {
            if ($("#masterForm").validateWebForm() == false) {
                args.set_cancel(true);
            }
        };

        function pageLoad() {
            fn_init();
        };

        function txtAliasOnKeyPress() {
            txtValidarCaracteres($("#<%=txtAlias.ClientID%>"))
        };

        function txtNombreOnUnload() {
            txtQuitarEspaciosIniFin($("#<%=txtNombre.ClientID%>"))
        };

    </script>
</asp:Content>