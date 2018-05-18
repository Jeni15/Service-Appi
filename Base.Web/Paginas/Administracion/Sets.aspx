<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Sets.aspx.cs" Inherits="SeedProject.Paginas.Administracion.Sets" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
	    <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
		    <h1 class="page-title txt-color-blueDark">
			
			    <!-- PAGE HEADER -->
			    <i class="fa-fw fa fa-pencil-square-o"></i> 
				    Configuración
			    <span>>  
				    Sets
			    </span>
		    </h1>
	    </div>
    </div>

    <!-- widget grid -->
    <section id="widget-grid" class="">
	    <!-- START ROW -->
	    <div class="row">
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
                                                                <asp:DropDownList ID="ddlFiltroVersiones" name="filtermodelsversions" runat="server"></asp:DropDownList>
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
					                <h2>Lista Sets</h2>
				                </header>

				                <!-- widget div-->
				                <div>

					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
						                <!-- This area used as dropdown edit box -->

					                </div>
					                <!-- end widget edit box -->

					                <asp:UpdatePanel ID="updSets" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <!-- widget content -->
					                        <div class="widget-body no-padding">
                                                <asp:GridView ID="grvDatos" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-warning btn-xs" CausesValidation="false" CommandName="EditarSet" CommandArgument='<%#Eval("IDSet")%>'>
                                                                    <i class="fa fa-pencil"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-xs" CausesValidation="false" CommandName="EliminarSet" CommandArgument='<%#Eval("IDSet")%>'>
                                                                    <i class="fa fa-trash"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IDSet" HeaderText="Id" />
                                                        <asp:BoundField DataField="Nombre" HeaderText="Parámetro" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="AliasGAMS" HeaderText="Alias GAMS" />
                                                        <asp:TemplateField HeaderText="Activo">
                                                            <ItemTemplate>
                                                                <label class="checkbox state-disabled">
											                        <asp:CheckBox ID="chkActivo" runat="server" Checked='<%#Eval("Activa").ToString() == "1"%>' Enabled="false" />
											                        <i></i>
                                                                </label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                <button id="btnAgregarModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalSet" style="display: none;">
                                                    ShowModal
                                                </button>
                                                <button id="btnCargueMasivoModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalSetCargueMasivo" style="display: none;">
                                                    ShowModal
                                                </button>

                                                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" style="display:none" />
                                                <asp:HiddenField ID="hddIdSet" runat="server" Value="0" />  
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
					        <h2>Cargue Masivos Sets</h2>
				        </header>

				        <!-- widget div-->
				        <div>

					        <!-- widget edit box -->
					        <div class="jarviswidget-editbox">
						        <!-- This area used as dropdown edit box -->

					        </div>
					        <!-- end widget edit box -->

					        <asp:UpdatePanel ID="updModalContenidoCargueMasivo" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
				                    <div class="form-horizontal">
						                <fieldset>
								            <div class="form-group">
									            <label class="col-md-2 control-label">Archivo</label>
									            <div class="col-md-10">
										            <input type="file" class="btn btn-default" id="upfArchivo" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel" />
									            </div>
								            </div>
                                        </fieldset>
                                    </div>
							        <div class="smart-form">
                                        <asp:UpdatePanel ID="updSetCargueMasivo" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <!-- widget content -->
					                            <div class="widget-body no-padding">
                                                    <asp:GridView ID="grvCargueMasivo" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand" EmptyDataText="No se han cargado registros">
                                                        <Columns>
                                                            <asp:BoundField DataField="Nombre" HeaderText="Parámetro" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                            <asp:BoundField DataField="AliasGAMS" HeaderText="Alias GAMS" />
                                                            <asp:TemplateField HeaderText="Cargar">
                                                                <ItemTemplate>
                                                                    <label class="checkbox state-disabled">
											                            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
											                            <i></i>
                                                                    </label>
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
                                        
                                        <asp:LinkButton ID="btnCargarCargueMasivo" runat="server" CssClass="btn btn-primary disabled" OnClick="btnCargarCargueMasivo_Click" Enabled="false">
                                            <i class="fa fa-upload"></i>
                                            Cargar
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnCancelarCargueMasivo" runat="server" CssClass="btn btn-default" OnClick="btnCancelarCargueMasivo_Click">
                                            Cancelar
                                        </asp:LinkButton>
					                </div>
                                </ContentTemplate>
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
    <div class="modal fade" id="modalSet" tabindex="-1" role="dialog">
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
				            <div class="smart-form">
						        <fieldset>
                                    <asp:Panel ID="pnlIdSet" runat="server">
                                        <section>
								            <div class="row">
									            <label class="label col col-2">Id</label>
									            <div class="col col-10">
										            <label class="input">
                                                        <asp:Label ID="lblIdSet" runat="server"></asp:Label>
										            </label>
									            </div>
								            </div>
							            </section>
                                    </asp:Panel>

                                    <section>
								        <div class="row">
									        <label class="label col col-2">Nombre</label>
									        <div class="col col-10">
										        <label class="input">
                                                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
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
									        <label class="label col col-2">Alias Gams</label>
									        <div class="col col-10">
										        <label class="input">
											        <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
										        </label>
									        </div>
								        </div>
							        </section>

                                    <asp:Panel ID="pnlActivo" runat="server">
                                        <section>
								            <div class="row">
									            <label class="label col col-2">Activo</label>
									            <div class="col col-10">
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

    <script src='<%= ResolveUrl("~/Scripts/WebForms/Paginas/Administracion/sets.js") %>'></script>
    <script>
        $(document).ready(function () {
            grvDatos = $("#<%=grvDatos.ClientID%>");

            iniciarControles();
            iniciarDataTable();
        });

        var pagefunction = function () {
            iniDataTable();

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

        function iniciarDataTable() {
            pagefunction();
        };

        function mostrarConfirm() {
            $.SmartMessageBox({
                title: "Eliminando Registro!",
                content: "Esta seguro que desea eliminar este registro?",
                buttons: '[No][Si]'
            }, function (ButtonPressed) {
                if (ButtonPressed === "Si") {
                    $("#<%=btnEliminar.ClientID%>").click();
                }
            });
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function EndRequestHandler(sender, args) {
            grvDatos = $("#<%=grvDatos.ClientID%>");
            iniciarDataTable();

            $("#<%=upfArchivo.ClientID%>").on('change', function () {
                $("#<%=btnCargarArchivo.ClientID%>").click();
            });
        };

        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>