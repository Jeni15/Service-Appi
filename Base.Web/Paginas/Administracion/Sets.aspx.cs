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
    public partial class Sets : System.Web.UI.Page
    {
        private SetFormViewModel setFormViewModel = new SetFormViewModel();

        public ISetService SetService { get; set; }

        public IModeloService ModeloService { get; set; }

        public IVersionService VersionService { get; set; }

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
        private void BindData()
        {
            CargarSets();
            CargarModelos();
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
        }

        private void CargarSets()
        {
            setFormViewModel.Sets = SetService.GetAll().ToList();

            if (this.ddlFiltroVersiones.SelectedValue != "0"
                && this.ddlFiltroVersiones.SelectedValue != "")
            {
                long idVersion = Convert.ToInt64(this.ddlFiltroVersiones.SelectedValue);
                setFormViewModel.Sets = setFormViewModel.Sets.Where(sv => sv.IdVersion == idVersion).ToList();
            }

            this.grvParametros.DataSource = setFormViewModel.Sets;
            this.grvParametros.DataBind();

            if (this.grvParametros.Rows.Count > 0)
            {
                this.grvParametros.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void CargarModelos()
        {
            setFormViewModel.Modelos = ModeloService.GetAll().ToList();

            this.ddlFiltroModelos.DataSource = setFormViewModel.Modelos;
            this.ddlFiltroModelos.DataTextField = "Nombre";
            this.ddlFiltroModelos.DataValueField = "IdModelo";
            this.ddlFiltroModelos.DataBind();
            this.ddlFiltroModelos.Items.Insert(0, new ListItem { Value = "0", Text = "(MODELO)" });
        }

        private void CargarVersiones(DropDownList ddlDestino, DropDownList ddlPadre)
        {
            if (ddlPadre.SelectedValue != "0"
                && ddlPadre.Items.Count > 0)
            {
                setFormViewModel.Versiones = VersionService.GetAll().ToList();
                int idModelo = Convert.ToInt32(ddlPadre.SelectedValue);
                setFormViewModel.Versiones = setFormViewModel.Versiones.Where(par => par.IDSubModelo == idModelo).ToList();
            }

            ddlDestino.DataValueField = "IDVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = setFormViewModel.Versiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }
        
        private Set LlenarSet(Set set)
        {
            set.Nombre = this.txtNombre.Text;
            set.Descripcion = this.txtDescripcion.Text;
            set.AliasGAMS = this.txtAlias.Text;

            return set;
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarSets();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.lblModalTitulo.Text = "Agregar Set";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.hddIdSet.Value != "0")
            {
                /*int idParametro = Convert.ToInt32(this.hddIdParametro.Value);
                parametroFormViewModel.Parametro = ParametroService.GetById(idParametro);

                if (parametroFormViewModel.Parametro != null)
                {
                    parametroFormViewModel.Parametro = LlenarParametro(parametroFormViewModel.Parametro);

                    ParametroService.Update(parametroFormViewModel.Parametro);
                }*/
            }
            else
            {
                setFormViewModel.Set = LlenarSet(new Set());
                setFormViewModel.Set.Fecha_Creacion = DateTime.Now;
                setFormViewModel.Set.Usuario_Creacion = "iarias";
                setFormViewModel.Set.Usuario_UltMod = "iarias";
                setFormViewModel.Set.Activa = "1";
                SetService.Create(setFormViewModel.Set);
            }

            CargarSets();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnCerrarModal').click();", true);
        }

        protected void grvParametros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarParametro")
            {
                this.lblModalTitulo.Text = "Editar Set";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
        }

        protected void ddlModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}