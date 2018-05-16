<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Sitio.Master" CodeBehind="Multisets.aspx.cs" Inherits="SeedProject.Paginas.Administracion.Multisets" %>

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

    <script src='<%= ResolveUrl("~/Scripts/Paginas/Administracion/Parametros/parametros.js") %>'></script>

    <script>
        $(document).ready(function () {
            grvParametros = $("#<%=grvParametros.ClientID%>");
            txtParametro = $("#<%=txtParametro.ClientID%>");
            txtDescripcion = $("#<%=txtDescripcion.ClientID%>");
            txtAlias = $("#<%=txtAlias.ClientID%>");

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

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function EndRequestHandler(sender, args) {
            grvParametros = $("#<%=grvParametros.ClientID%>");
            iniciarDataTable();
        };

        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>