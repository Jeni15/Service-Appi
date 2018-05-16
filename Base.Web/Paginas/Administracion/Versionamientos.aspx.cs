using Base.Model.Models;
using Base.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeedProject.Paginas.Administracion
{
    public partial class Versionamientos : System.Web.UI.Page
    {
        private ParametroFormViewModel parametroFormViewModel = new ParametroFormViewModel();

        public IParametroService ParametroService { get; set; }

        public IModeloService ModeloService { get; set; }

        public IModeloVersionService ModeloVersionService { get; set; }

        public IModeloCasoService ModeloCasoService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            if (this.grvParametros.Rows.Count > 0)
            {
                this.grvParametros.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelosVersiones(this.ddlFiltroModelosVersiones, this.ddlFiltroModelos);
        }

        protected void ddlFiltroModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelosCasos(this.ddlFiltroModelosCasos, this.ddlFiltroModelosVersiones);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            parametroFormViewModel.Parametros = ParametroService.GetAll().ToList();

            if (this.ddlFiltroModelosCasos.SelectedValue != "0")
            {
                int idCaso = Convert.ToInt32(this.ddlFiltroModelosCasos.SelectedValue);
                parametroFormViewModel.Parametros = parametroFormViewModel.Parametros.Where(par => par.IdModeloCaso == idCaso).ToList();
            }

            this.grvParametros.DataSource = parametroFormViewModel.Parametros;
            this.grvParametros.DataBind();

            if (parametroFormViewModel.Parametros.Count > 0)
            {
                this.grvParametros.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.lblModalTitulo.Text = "Agregar Parametro";

            LimpiarCampos();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.hddIdParametro.Value != "0")
            {
                int idParametro = Convert.ToInt32(this.hddIdParametro.Value);
                parametroFormViewModel.Parametro = ParametroService.GetById(idParametro);

                if (parametroFormViewModel.Parametro != null)
                {
                    parametroFormViewModel.Parametro = LlenarParametro(parametroFormViewModel.Parametro);

                    ParametroService.Update(parametroFormViewModel.Parametro);
                }
            }
            else
            {
                parametroFormViewModel.Parametro = LlenarParametro(new Parametro());
                ParametroService.Create(parametroFormViewModel.Parametro);
            }

            CargarParametros();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnCerrarModal').click();", true);
        }

        protected void grvParametros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarParametro")
            {
                this.hddIdParametro.Value = e.CommandArgument.ToString();
                this.lblModalTitulo.Text = "Editar Parametro";

                CargarParametro();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
        }

        protected void ddlModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelosVersiones(this.ddlModelosVersiones, this.ddlModelos);
        }

        protected void ddlModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarModelosCasos(this.ddlModelosCasos, this.ddlModelosVersiones);
        }

        private void BindData()
        {
            CargarModelos(this.ddlFiltroModelos);
            CargarModelosVersiones(this.ddlFiltroModelosVersiones, this.ddlFiltroModelos);
            CargarModelosCasos(this.ddlFiltroModelosCasos, this.ddlFiltroModelosVersiones);
            CargarParametros();

            CargarModelos(this.ddlModelos);
            CargarModelosVersiones(this.ddlModelosVersiones, this.ddlModelos);
            CargarModelosCasos(this.ddlModelosCasos, this.ddlModelosVersiones);
        }

        private void LimpiarCampos()
        {
            this.hddIdParametro.Value = "0";
            this.ddlModelos.SelectedValue = "0";
            this.ddlModelosVersiones.SelectedValue = "0";
            this.ddlModelosCasos.SelectedValue = "0";
            this.txtParametro.Text = "";
            this.txtDescripcion.Text = "";
            this.txtAlias.Text = "";
            this.chkActivo.Checked = true;
        }

        private void CargarModelos(DropDownList ddlDestino)
        {
            parametroFormViewModel.Modelos = ModeloService.GetAll().ToList();

            ddlDestino.DataValueField = "IdModelo";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = parametroFormViewModel.Modelos;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(MODELO)" });
        }

        private void CargarModelosVersiones(DropDownList ddlDestino, DropDownList ddlPadre)
        {
            if (ddlPadre.SelectedValue != "0"
                && ddlPadre.Items.Count > 0)
            {
                parametroFormViewModel.ModelosVersiones = ModeloVersionService.GetAll().ToList();
                int idModelo = Convert.ToInt32(ddlPadre.SelectedValue);
                parametroFormViewModel.ModelosVersiones = parametroFormViewModel.ModelosVersiones.Where(par => par.IdModelo == idModelo && par.IdModeloVersion != 0).ToList();
            }

            ddlDestino.DataValueField = "IdModeloVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = parametroFormViewModel.ModelosVersiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }

        private void CargarModelosCasos(DropDownList ddlDestino, DropDownList ddlPadre)
        {
            if (ddlPadre.SelectedValue != "0"
                && ddlPadre.Items.Count > 0)
            {
                parametroFormViewModel.ModelosCasos = ModeloCasoService.GetAll().ToList();
                int idVersion = Convert.ToInt32(ddlPadre.SelectedValue);
                parametroFormViewModel.ModelosCasos = parametroFormViewModel.ModelosCasos.Where(par => par.IdModeloVersion == idVersion && par.IdModeloCaso != 0).ToList();
            }

            ddlDestino.DataValueField = "IdModeloCaso";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = parametroFormViewModel.ModelosCasos;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(CASO)" });
        }

        private void CargarParametros()
        {
            List<Parametro> parametros = ParametroService.GetAll().ToList();

            this.grvParametros.DataSource = parametros;
            this.grvParametros.DataBind();

            if (parametros.Count > 0)
            {
                this.grvParametros.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void CargarParametro()
        {
            if (this.hddIdParametro.Value != "0")
            {
                int idParametro = Convert.ToInt32(this.hddIdParametro.Value);
                parametroFormViewModel.Parametro = ParametroService.GetById(idParametro);
                parametroFormViewModel.ModeloCaso = parametroFormViewModel.Parametro != null ? ModeloCasoService.GetById((int)parametroFormViewModel.Parametro.IdModeloCaso) : null;
                parametroFormViewModel.ModeloVersion = parametroFormViewModel.ModeloCaso != null ? ModeloVersionService.GetById((int)parametroFormViewModel.ModeloCaso.IdModeloVersion) : null;

                this.ddlModelos.SelectedValue = parametroFormViewModel.ModeloVersion != null ? parametroFormViewModel.ModeloVersion.IdModelo.ToString() : "0";
                ddlModelos_SelectedIndexChanged(null, null);
                this.ddlModelosVersiones.SelectedValue = parametroFormViewModel.ModeloVersion != null ? parametroFormViewModel.ModeloVersion.IdModeloVersion.ToString() : "0";
                ddlModelosVersiones_SelectedIndexChanged(null, null);
                this.ddlModelosCasos.SelectedValue = parametroFormViewModel.ModeloCaso != null ? parametroFormViewModel.ModeloCaso.IdModeloCaso.ToString() : "0";
                this.txtParametro.Text = parametroFormViewModel.Parametro.Nombre;
                this.txtDescripcion.Text = parametroFormViewModel.Parametro.Descripcion;
                this.txtAlias.Text = parametroFormViewModel.Parametro.Alias;
                this.chkActivo.Checked = parametroFormViewModel.Parametro.Activo;
            }
        }

        private Parametro LlenarParametro(Parametro parametro)
        {
            parametro.IdModeloCaso = this.ddlModelosCasos.SelectedValue != "0" ? Convert.ToInt32(this.ddlModelosCasos.SelectedValue) : (int?)null;
            parametro.Nombre = this.txtParametro.Text;
            parametro.Descripcion = this.txtDescripcion.Text;
            parametro.Alias = this.txtAlias.Text;
            parametro.Activo = this.chkActivo.Checked;

            return parametro;
        }
    }
}