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
        private ParametroFormViewModel parametroFormViewModel = new ParametroFormViewModel();

        public IParametroService ParametroService { get; set; }

        public IModeloService ModeloService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            if (this.grvParametros.Rows.Count > 0)
            {
                this.grvParametros.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlFiltroModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void grvParametros_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlModelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlModelosVersiones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}