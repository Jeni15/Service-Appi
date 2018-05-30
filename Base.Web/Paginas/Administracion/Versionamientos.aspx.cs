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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            this.grvDatos.DataSource = new List<string>();
            this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.grvSets.DataSource = new List<string>();
            this.grvSets.DataBind();
            this.grvSets.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.grvSubset.DataSource = new List<string>();
            this.grvSubset.DataBind();
            this.grvSubset.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.grvMultiset.DataSource = new List<string>();
            this.grvMultiset.DataBind();
            this.grvMultiset.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.grvParametro.DataSource = new List<string>();
            this.grvParametro.DataBind();
            this.grvParametro.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.grvEscalar.DataSource = new List<string>();
            this.grvEscalar.DataBind();
            this.grvEscalar.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.lblModalTitulo.Text = "Agregar Versionamiento";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
        }
    }
}