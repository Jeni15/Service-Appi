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
    public partial class Subsets : System.Web.UI.Page
    {
        private SetFormViewModel setFormViewModel = new SetFormViewModel();
        public ISetService SetService { get; set; }

        private SubsetFormViewModel subsetFormViewModel = new SubsetFormViewModel();

        public ISubsetService SubsetService { get; set; }

        public IModeloService ModeloService { get; set; }

        public IVersionService VersionService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            //this.grvDatos.DataSource = new List<string>();
            //this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
        }

        protected void ddlFiltroModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSets();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarSets();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.lblModalTitulo.Text = "Agregar Subset";
            this.hddIdSet.Value = "0";

            LimpiarCampos(false);

            CargarSetsPadres(0);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        private void CargarSetsPadres(long IDSet_Padre)
        {
            setFormViewModel.Sets = SetService.GetAll().ToList();

            this.ddlSets.DataSource = setFormViewModel.Sets;
            this.ddlSets.DataTextField = "Nombre";
            this.ddlSets.DataValueField = "IdSet";
            this.ddlSets.DataBind();

            if (IDSet_Padre == 0)
                this.ddlSets.Items.Insert(0, new ListItem { Value = "0", Text = "(SET)" });
            else
            { 
                foreach (ListItem item in ddlSets.Items)
                {
                    if (item.Value == IDSet_Padre.ToString())
                        item.Selected = true;
                }
            }

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.hddIdSubset.Value != "0")
            {
                long idSet = Convert.ToInt64(this.hddIdSubset.Value);
                subsetFormViewModel.Subset = SubsetService.GetById(Convert.ToInt32(idSet));

                if (subsetFormViewModel.Subset != null)
                {
                    subsetFormViewModel.Subset = LlenarSet(subsetFormViewModel.Subset);
                    subsetFormViewModel.Subset.Fecha_UltMod = DateTime.Now;
                    subsetFormViewModel.Subset.Usuario_UltMod = "iarias";  // JBCA: se debe rempalzar por el usuario actual
                    subsetFormViewModel.Subset.Activa = this.chkActivo.Checked ? "1" : "0";
                    subsetFormViewModel.Subset.IDSet_Padre = Convert.ToInt32(this.ddlSets.SelectedValue);

                    SubsetService.Update(subsetFormViewModel.Subset);
                }
            }
            else
            {
                subsetFormViewModel.Subset = LlenarSet(new Subset());
                subsetFormViewModel.Subset.Fecha_Creacion = DateTime.Now;
                subsetFormViewModel.Subset.Fecha_UltMod = DateTime.Now;
                subsetFormViewModel.Subset.Usuario_Creacion = "iarias";  // JBCA: se debe rempalzar por el usuario actual
                subsetFormViewModel.Subset.Usuario_UltMod = "iarias";  // JBCA: se debe rempalzar por el usuario actual
                subsetFormViewModel.Subset.Activa = "1";
                subsetFormViewModel.Subset.IDSet_Padre = Convert.ToInt32(this.ddlSets.SelectedValue); 

                SubsetService.Create(subsetFormViewModel.Subset);
            }

            CargarSets();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnCerrarModal').click();", true);
        }

        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.hddIdSubset.Value = e.CommandArgument.ToString();

            if (e.CommandName == "EditarSubset")
            {
                this.lblModalTitulo.Text = "Editar Subset";

                LimpiarCampos(true);
                CargarSet(Convert.ToInt64(this.hddIdSubset.Value));

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
            else if (e.CommandName == "EliminarSubset")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "mostrarConfirm();", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.hddIdSubset.Value != "0")
            {
                long idSet = Convert.ToInt64(this.hddIdSubset.Value);

                subsetFormViewModel.Subset = new Subset();
                subsetFormViewModel.Subset.IDSet = idSet;
                subsetFormViewModel.Subset.Fecha_UltMod = DateTime.Now;
                subsetFormViewModel.Subset.Usuario_UltMod = "iarias";

                SubsetService.Delete(subsetFormViewModel.Subset);

                CargarSets();
            }
        }

        protected void btnCargueMasivo_Click(object sender, EventArgs e)
        {
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCargarCargueMasivo_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarCargueMasivo_Click(object sender, EventArgs e)
        {
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
        }

        private void BindData()
        {
            CargarSets();
            CargarModelos();
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
        }

        private void CargarSets()
        {
            subsetFormViewModel.Subsets = SubsetService.GetAll().ToList();

            if (this.ddlFiltroVersiones.SelectedValue != "0"
                && this.ddlFiltroVersiones.SelectedValue != "")
            {
                long idVersion = Convert.ToInt64(this.ddlFiltroVersiones.SelectedValue);
                subsetFormViewModel.Subsets = subsetFormViewModel.Subsets.Where(sv => sv.IdVersion == idVersion).ToList();
            }

            this.grvDatos.DataSource = subsetFormViewModel.Subsets;
            this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void CargarModelos()
        {
            subsetFormViewModel.Modelos = ModeloService.GetAll().ToList();

            this.ddlFiltroModelos.DataSource = subsetFormViewModel.Modelos;
            this.ddlFiltroModelos.DataTextField = "Nombre";
            this.ddlFiltroModelos.DataValueField = "IdModelo";
            this.ddlFiltroModelos.DataBind();
            this.ddlFiltroModelos.Items.Insert(0, new ListItem { Value = "0", Text = "(MODELO)" });
        }

        private void CargarVersiones(DropDownList ddlDestino, DropDownList ddlPadre)
        {
            ddlDestino.Items.Clear();
            if (ddlPadre.SelectedValue != "0"
                && ddlPadre.Items.Count > 0)
            {
                subsetFormViewModel.Versiones = VersionService.GetAll().ToList();
                int idModelo = Convert.ToInt32(ddlPadre.SelectedValue);
                subsetFormViewModel.Versiones = subsetFormViewModel.Versiones.Where(par => par.IDSubModelo == idModelo).ToList();
            }

            ddlDestino.DataValueField = "IDVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = subsetFormViewModel.Versiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }

        private void CargarSet(long idSet)
        {
            subsetFormViewModel.Subset = SubsetService.GetById(Convert.ToInt32(idSet));

            if (subsetFormViewModel.Subset != null)
            {
                this.lblIdSet.Text = subsetFormViewModel.Subset.IDSet.ToString();
                this.txtNombre.Text = subsetFormViewModel.Subset.Nombre;
                this.txtDescripcion.Text = subsetFormViewModel.Subset.Descripcion;
                this.txtAlias.Text = subsetFormViewModel.Subset.AliasGAMS;
                this.chkActivo.Checked = subsetFormViewModel.Subset.Activa == "1";
                CargarSetsPadres(subsetFormViewModel.Subset.IDSet_Padre);

//                SelectedSetPadre(subsetFormViewModel.Subset.IDSet_Padre);
            }
        }

        private void SelectedSetPadre(long IDSet_Padre)
        {
            foreach (ListItem item in ddlSets.Items)
            {
                if (item.Value == IDSet_Padre.ToString())
                {
                    item.Selected = true;
                } 

            }
            
        }

        private Subset LlenarSet(Subset set)
        {
            set.Nombre = this.txtNombre.Text;
            set.Descripcion = this.txtDescripcion.Text;
            set.AliasGAMS = this.txtAlias.Text;

            return set;
        }

        private void LimpiarCampos(bool esUpdate)
        {
            this.pnlIdSet.Visible = esUpdate;
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.txtAlias.Text = "";
            this.pnlActivo.Visible = esUpdate;
        }
    }
}