<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Parametros.aspx.cs" Inherits="SeedProject.Paginas.Administracion.Parametros.Parametros" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
	    <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
		    <h1 class="page-title txt-color-blueDark">
			
			    <!-- PAGE HEADER -->
			    <i class="fa-fw fa fa-pencil-square-o"></i> 
				    Parametros
			    <span>>  
				    Registrar Parametros
			    </span>
		    </h1>
	    </div>
    </div>

    <!-- widget grid -->
    <section id="widget-grid" class="">
	    <!-- START ROW -->
	    <div class="row">
		    <!-- NEW COL START -->
		    <article class="col-sm-12 col-md-5">
			    <!-- Widget ID (each widget will need unique ID)-->
			    <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-2" data-widget-editbutton="false" data-widget-custombutton="false">
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
                                                        <asp:DropDownList ID="ddlFiltroModelosVersiones" name="filtermodelsversions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroModelosVersiones_SelectedIndexChanged"></asp:DropDownList>
                                                        <i></i>
                                                    </label>
                                                </section>
                                            </div>

                                            <div class="row">
                                                <section class="col col-10">
                                                    <label class="select">
                                                        <asp:DropDownList ID="ddlFiltroModelosCasos" name="filtermodelscases" runat="server"></asp:DropDownList>
                                                        <i></i>
                                                    </label>
                                                </section>
                                            </div>
                                        </fieldset>

                                        <footer>
                                            <div class="row">
                                                <section class="col col-12 pull-right">
                                                    <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="btnFiltrar_Click">
                                                        <i class="fa fa-search"></i>
                                                        Filtrar
                                                    </asp:LinkButton>
                                                </section>
                                            </div>
							            </footer>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </article>

            <article class="col-sm-12 col-md-12">
                <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-1" data-widget-editbutton="false">
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
					    <span class="widget-icon"> <i class="fa fa-table"></i> </span>
					    <h2>Lista Parametros</h2>

				    </header>

				    <!-- widget div-->
				    <div>

					    <!-- widget edit box -->
					    <div class="jarviswidget-editbox">
						    <!-- This area used as dropdown edit box -->

					    </div>
					    <!-- end widget edit box -->

					    <!-- widget content -->
					    <div class="widget-body no-padding">
                            <asp:UpdatePanel ID="updParametros" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:GridView ID="grvParametros" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="grvParametros_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-warning btn-xs" CausesValidation="false" CommandName="EditarParametro" CommandArgument='<%#Eval("IdParametro")%>'>
                                                        <i class="fa fa-pencil"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IdParametro" HeaderText="Id" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Parámetro" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="Alias" HeaderText="Alias GAMS" />
                                            <asp:TemplateField HeaderText="Activo">
                                                <ItemTemplate>
                                                    <label class="checkbox state-disabled">
											            <asp:CheckBox ID="chkParametroActivo" runat="server" Checked='<%#Bind("Activo")%>' Enabled="false" />
											            <i></i>
                                                    </label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <button id="btnAgregarModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalParametro" style="display: none;">
                                        ShowModal
                                    </button>

                                    <asp:HiddenField ID="hddIdParametro" runat="server" Value="0" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </article>
        </div>
    </section>

    <!-- Modal -->
    <div class="modal fade" id="modalParametro" tabindex="-1" role="dialog">
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
				            <div class="smart-form">
						        <fieldset>
                                    <section>
                                        <div class="row">
                                            <section class="col col-10">
										        <label class="select">
                                                    <asp:DropDownList ID="ddlModelos" name="models" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModelos_SelectedIndexChanged"></asp:DropDownList>
                                                    <i></i>
                                                </label>
									        </section>
                                        </div>
                                    </section>

                                    <section>
                                        <div class="row">
                                            <section class="col col-10">
                                                <label class="select">
                                                    <asp:DropDownList ID="ddlModelosVersiones" name="modelsversions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModelosVersiones_SelectedIndexChanged"></asp:DropDownList>
                                                    <i></i>
                                                </label>
                                            </section>
                                        </div>
                                    </section>

                                    <section>
                                        <div class="row">
                                            <section class="col col-10">
                                                <label class="select">
                                                    <asp:DropDownList ID="ddlModelosCasos" name="modelscases" runat="server"></asp:DropDownList>
                                                    <i></i>
                                                </label>
                                            </section>
                                        </div>
                                    </section>

							        <section>
								        <div class="row">
									        <label class="label col col-2">Parametro</label>
									        <div class="col col-10">
										        <label class="input"> <i class="icon-append fa fa-cog"></i>
                                                    <asp:TextBox ID="txtParametro" runat="server"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

							        <section>
								        <div class="row">
									        <label class="label col col-2">Descripción</label>
									        <div class="col col-10">
										        <label class="textarea">
											        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

							        <section>
								        <div class="row">
									        <label class="label col col-2">Alias</label>
									        <div class="col col-10">
										        <label class="input">
											        <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

							        <section>
								        <div class="row">
									        <label class="label col col-2">Activo</label>
									        <div class="col col-10">
										        <label class="checkbox">
                                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
											        <i></i>
										        </label>
									        </div>
								        </div>
							        </section>
						        </fieldset>
							
						        <footer>
							        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" OnClick="btnGuardar_Click" OnClientClick="return $('#masterForm').valid();">
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

    <script>
        $(document).ready(function () {
            CargarTodo();

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
				    <%=txtParametro.ClientID %> : {
					    required : true
				    },
				    <%=txtDescripcion.ClientID %> : {
					    required : true
				    },
				    <%=txtAlias.ClientID %> : {
					    required : true
				    }
			    },
	
			    // Messages for form validation
			    messages : {
				    <%=txtParametro.ClientID %> : {
					    required : 'Please enter the parameter name'
				    },
				    <%=txtDescripcion.ClientID %> : {
					    required : 'Please enter the description'
				    },
				    <%=txtAlias.ClientID %> : {
					    required : 'Please enter the alias'
				    }
			    },
	
			    // Do not change code below
			    errorPlacement : function(error, element) {
				    error.insertAfter(element.parent());
			    }
		    });
        });

        var pagefunction = function () {
            var responsiveHelper_datatable_fixed_column = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            /* COLUMN FILTER  */
            var otable = $("#<%=grvParametros.ClientID%>").DataTable({
                //"bFilter": false,
                //"bInfo": false,
                //"bLengthChange": false
                //"bAutoWidth": false,
                //"bPaginate": false,
                //"bStateSave": true // saves sort state using localStorage
                "order": [[ 1, "asc" ]],
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
                        responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper($('#<%=grvParametros.ClientID%>'), breakpointDefinition);
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

            // custom toolbar
            $("div.toolbar").html('' +
                '<div class="text-right">' +
                    '<asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary form-control" OnClick="btnAgregar_Click">' +
                        '<i class="fa fa-plus"></i> ' +
                        'Agregar' +
                    '</asp:LinkButton>' +
                '</div>');

            /* END TABLETOOLS */
        };

        function CargarTodo() {
            loadScript("<%= ResolveUrl("~/js/plugin/datatables/jquery.dataTables.min.js") %>", function () {
                loadScript("<%= ResolveUrl("~/js/plugin/datatables/dataTables.colVis.min.js") %>", function () {
                    loadScript("<%= ResolveUrl("~/js/plugin/datatables/dataTables.tableTools.min.js") %>", function () {
                        loadScript("<%= ResolveUrl("~/js/plugin/datatables/dataTables.bootstrap.min.js") %>", function () {
                            loadScript("<%= ResolveUrl("~/js/plugin/datatable-responsive/datatables.responsive.min.js") %>", pagefunction)
                        });
                    });
                });
            });
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function EndRequestHandler(sender, args) {
            CargarTodo();
        };

        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>