<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Versionamientos.aspx.cs" Inherits="SeedProject.Paginas.Administracion.Versionamientos" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
	    <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
		    <h1 class="page-title txt-color-blueDark">
			
			    <!-- PAGE HEADER -->
			    <i class="fa-fw fa fa-pencil-square-o"></i> 
				    Configuración
			    <span>>  
				    Versionamientos
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
                                                                    <asp:DropDownList ID="ddlFiltroModelos" name="filtermodels" runat="server"></asp:DropDownList>
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
					                <h2>Lista Versionamientos</h2>
				                </header>

				                <!-- widget div-->
				                <div>

					                <!-- widget edit box -->
					                <div class="jarviswidget-editbox">
						                <!-- This area used as dropdown edit box -->

					                </div>
					                <!-- end widget edit box -->

					                <asp:UpdatePanel ID="updGrilla" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <!-- widget content -->
					                        <div class="widget-body no-padding">
                                                <asp:GridView ID="grvDatos" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered" OnRowCommand="grvDatos_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-warning btn-xs" CausesValidation="false" CommandName="EditarVersionamiento" CommandArgument='<%#Eval("IDSet")%>'>
                                                                    <i class="fa fa-pencil"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-xs" CausesValidation="false" CommandName="EliminarVersionamiento" CommandArgument='<%#Eval("IDSet")%>'>
                                                                    <i class="fa fa-trash"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>

                                                            <ItemStyle Width="60px"></ItemStyle>
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

                                                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" style="display:none" />
                                                <asp:HiddenField ID="hddIdVersionamiento" runat="server" Value="0" />  
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
	    <div class="modal-dialog modal-lg">
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
			    <div class="modal-body">
                    <asp:UpdatePanel ID="updModalContenido" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
				            <div id="frmMain" class="smart-form form">
                                <fieldset>
                                    <asp:Panel ID="pnlIdVersionamiento" runat="server">
                                        <section>
								            <div class="row">
									            <label class="label col col-2">Id</label>
									            <div class="col col-10">
										            <label class="input">
                                                        <asp:Label ID="lblIdVersionamiento" runat="server"></asp:Label>
										            </label>
									            </div>
								            </div>
							            </section>
                                    </asp:Panel>

                                    <section>
								        <div class="row">
									        <label class="label col col-2">Versión</label>
									        <div class="col col-10">
										        <label class="input">
                                                    <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
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

                                    
                                    <div class="row">
                                        <section class="col col-lg-6">
									        <label class="label col col-4">Fecha Versión</label>
                                            <div class="col col-lg-8">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtFechaVersion" CssClass="form-control isDatepicker" runat="server"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </span>
                                                </div>
                                            </div>
								        </section>

                                        <section class="col col-lg-6">
                                            <label class="label col col-4">Fecha Actual</label>
									        <div class="col col-8">
										        <div class="input-group state-disabled">
											        <asp:TextBox ID="txtFechaActual" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </span>
										        </label>
									        </div>
								        </section>
                                    </div>

                                    <asp:Panel ID="pnlCopiar" runat="server">
                                        <div class="row">
                                            <section class="col col-lg-6">
								                <label class="label col col-4">Copiar Existente</label>
									            <div class="col col-8">
										            <label class="checkbox">
                                                        <asp:CheckBox ID="chkCopiarExistente" runat="server" onclick="ShowHideDiv(this);" />
											            <i></i>
										            </label>
									            </div>
							                </section>

                                            <div id="divCopiarVersion" style="display:none">
                                                <section class="col col-lg-6">
								                    <label class="label col col-4">Versión</label>
									                <div class="col col-8">
										                <label class="select">
                                                            <asp:DropDownList ID="ddlCopiarVersion" runat="server"></asp:DropDownList>
                                                            <i></i>
                                                        </label>
									                </div>
							                    </section>
                                            </div>
                                        </div>
                                    </asp:Panel>
						        </fieldset>
							
						        <footer>
							        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary submit frmMain" OnClick="btnGuardar_Click" >
                                        <i class="fa fa-floppy-o"></i>
                                        Guardar
                                    </asp:LinkButton>
						        </footer>
					        </div>

                            <div id="frmChildren" class="smart-form form">
                                <ul id="tabMain" class="nav nav-tabs bordered">
								    <li class="active">
									    <a href="#tabSet" data-toggle="tab"><i class="fa fa-fw fa-lg fa-gear"></i> Set</a>
								    </li>
								    <li>
									    <a href="#tabSubset" data-toggle="tab"><i class="fa fa-fw fa-lg fa-cube"></i> Subset</a>
								    </li>
                                    <li>
									    <a href="#tabMultiset" data-toggle="tab"><i class="fa fa-fw fa-lg fa-cubes"></i> Multiset</a>
								    </li>
                                    <li>
									    <a href="#tabParametro" data-toggle="tab"><i class="fa fa-fw fa-lg fa-sliders"></i> Parametro</a>
								    </li>
                                    <li>
									    <a href="#tabEscalar" data-toggle="tab"><i class="fa fa-fw fa-lg fa-database"></i> Escalar</a>
								    </li>
							    </ul>

                                <div class="tab-content">
                                    <div id="tabSet" class="tab-pane active">
                                        <asp:GridView ID="grvSets" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" EmptyDataText="Ningún dato disponible en esta tabla">
                                            <Columns>
                                                <asp:BoundField DataField="IDSet" HeaderText="Id" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Set" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:TemplateField HeaderText="Asociar">
                                                    <ItemTemplate>
                                                        <label class="checkbox">
										                    <asp:CheckBox ID="chkActivo" runat="server" />
										                    <i></i>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
								    </div>
								    <div id="tabSubset" class="tab-pane fade">
									    <asp:GridView ID="grvSubset" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" EmptyDataText="Ningún dato disponible en esta tabla">
                                            <Columns>
                                                <asp:BoundField DataField="IDSubset" HeaderText="Id" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Subset" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:TemplateField HeaderText="Asociar">
                                                    <ItemTemplate>
                                                        <label class="checkbox">
										                    <asp:CheckBox ID="chkActivo" runat="server" />
										                    <i></i>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
								    </div>
								    <div class="tab-pane fade" id="tabMultiset">
									    <asp:GridView ID="grvMultiset" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" EmptyDataText="Ningún dato disponible en esta tabla">
                                            <Columns>
                                                <asp:BoundField DataField="IDSubset" HeaderText="Id" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Multiset" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:TemplateField HeaderText="Asociar">
                                                    <ItemTemplate>
                                                        <label class="checkbox">
										                    <asp:CheckBox ID="chkActivo" runat="server" />
										                    <i></i>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
								    </div>
								    <div class="tab-pane fade" id="tabParametro">
									    <asp:GridView ID="grvParametro" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" EmptyDataText="Ningún dato disponible en esta tabla">
                                            <Columns>
                                                <asp:BoundField DataField="IDSubset" HeaderText="Id" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Parametro" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:TemplateField HeaderText="Asociar">
                                                    <ItemTemplate>
                                                        <label class="checkbox">
										                    <asp:CheckBox ID="chkActivo" runat="server" />
										                    <i></i>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
								    </div>
                                    <div class="tab-pane fade" id="tabEscalar">
									    <asp:GridView ID="grvEscalar" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" EmptyDataText="Ningún dato disponible en esta tabla">
                                            <Columns>
                                                <asp:BoundField DataField="IDSubset" HeaderText="Id" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Escalar" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                <asp:TemplateField HeaderText="Asociar">
                                                    <ItemTemplate>
                                                        <label class="checkbox">
										                    <asp:CheckBox ID="chkActivo" runat="server" />
										                    <i></i>
                                                        </label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
								    </div>
                                </div>

                                <footer>
							        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary submit frmMain" OnClick="btnGuardar_Click" >
                                        <i class="fa fa-retweet"></i>
                                        Asociar
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
    <script src='<%= ResolveUrl("~/Scripts/WebForms/Paginas/Administracion/versionamientos.js") %>'></script>

    <script>
        function ShowHideDiv(chkPassport) {
            var dvPassport = document.getElementById("divCopiarVersion");
            dvPassport.style.display = chkPassport.checked ? "block" : "none";
        }

        function fn_init() {
            
            iniDataTable($("#<%=grvDatos.ClientID%>"));

            $('.isDatepicker').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                todayHighlight: true
            });

            // custom toolbar
            $("div.toolbar").html('' +
                '<div class="text-right">' +
                    '<asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary form-control" OnClick="btnAgregar_Click">' +
                        '<i class="fa fa-plus"></i> ' +
                        'Agregar' +
                    '</asp:LinkButton>' +
                '</div>');

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(onEachRequest);
        };

        function onEachRequest(sender, args) {
            if ($("#masterForm").validateWebForm() == false) {
                args.set_cancel(true);
            }
        };

        function pageLoad() {
            fn_init();
        };
    </script>
</asp:Content>