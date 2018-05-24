using Base.Model.Models;
using Base.Service.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SeedProject.Paginas.Administracion
{
    public partial class Parametros : System.Web.UI.Page
    {
        private ParametroFormViewModel parametroFormViewModel = new ParametroFormViewModel();

        public IParametroService ParametroService { get; set; }

        public IModeloService ModeloService { get; set; }

        public IVersionService VersionService { get; set; }

        public ISetService SetService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarParametros();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.lblModalTitulo.Text = "Agregar Parametro";
            this.hddIdParametro.Value = "0";
            this.hddDimensionVals.Value = "";

            LimpiarCampos(false);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.hddIdParametro.Value != "0")
            {
                long idParametro = Convert.ToInt64(this.hddIdParametro.Value);
                parametroFormViewModel.Parametro = ParametroService.GetById(Convert.ToInt32(idParametro));

                if (parametroFormViewModel.Parametro != null)
                {
                    parametroFormViewModel.Parametro = LlenarParameter(parametroFormViewModel.Parametro);
                    parametroFormViewModel.Parametro.Fecha_UltMod = DateTime.Now;
                    parametroFormViewModel.Parametro.Usuario_UltMod = "iarias";
                    parametroFormViewModel.Parametro.Activa = this.chkActivo.Checked ? "1" : "0";

                    ParametroService.Update(parametroFormViewModel.Parametro);
                }
            }
            else
            {
                parametroFormViewModel.Parametro = LlenarParameter(new Parametro());
                parametroFormViewModel.Parametro.Cantidad_Dimensiones = Convert.ToInt64(this.txtDimension.Text) > 10 ? 10 : Convert.ToInt64(this.txtDimension.Text);
                parametroFormViewModel.Parametro.Dimension = this.hddDimensionVals.Value;
                parametroFormViewModel.Parametro.Fecha_Creacion = DateTime.Now;
                parametroFormViewModel.Parametro.Usuario_Creacion = "iarias";
                parametroFormViewModel.Parametro.Activa = "1";

                parametroFormViewModel.Parametro.IdUOM = 1; //TMP
                parametroFormViewModel.Parametro.Entrada_Manual = ""; //TMP
                parametroFormViewModel.Parametro.Resultado = "S"; //TMP
                parametroFormViewModel.Parametro.Fecha_UltMod = DateTime.Now; //TMP
                parametroFormViewModel.Parametro.Usuario_UltMod = "iarias"; //TMP

                ParametroService.Create(parametroFormViewModel.Parametro);
            }

            CargarParametros();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnCerrarModal').click();", true);
        }

        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.hddIdParametro.Value = e.CommandArgument.ToString();

            if (e.CommandName == "EditarParametro")
            {
                this.lblModalTitulo.Text = "Editar Parametro";

                LimpiarCampos(true);
                CargarParametro(Convert.ToInt64(this.hddIdParametro.Value));

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();txtDimensionOnKeyPress();", true);
            }
            else if (e.CommandName == "EliminarParametro")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "mostrarConfirm('eliminar', '#" + this.btnEliminar.ClientID + "');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.hddIdParametro.Value != "0")
            {
                long idParametro = Convert.ToInt64(this.hddIdParametro.Value);

                parametroFormViewModel.Parametro = new Parametro();
                parametroFormViewModel.Parametro.IDParameter = idParametro;
                parametroFormViewModel.Parametro.Fecha_UltMod = DateTime.Now;
                parametroFormViewModel.Parametro.Usuario_UltMod = "iarias";

                ParametroService.Delete(parametroFormViewModel.Parametro);

                CargarParametros();
            }
        }

        protected void btnCargueMasivo_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = false;
            this.pnlCargueMasivo.Visible = true;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = false;
            this.pnlExportar.Visible = true;

            parametroFormViewModel.Parametros = ParametroService.GetAll().ToList();

            this.grvExportar.DataSource = parametroFormViewModel.Parametros;
            this.grvExportar.DataBind();
        }

        protected void btnCargueMasivoCargar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in this.grvCargueMasivo.Rows)
            {
                CheckBox chkActivo = (CheckBox)item.FindControl("chkActivo");
                HtmlGenericControl spnEstado = (HtmlGenericControl)item.FindControl("spnEstado");
                Label lblEstado = (Label)item.FindControl("lblEstado");

                if (chkActivo.Checked)
                {
                    try
                    {
                        parametroFormViewModel.Parametro = new Parametro();
                        parametroFormViewModel.Parametro.Nombre = item.Cells[0].Text;
                        parametroFormViewModel.Parametro.Descripcion = item.Cells[1] != null ? item.Cells[1].Text : null;
                        parametroFormViewModel.Parametro.AliasGAMS = item.Cells[2] != null ? item.Cells[2].Text : null;
                        parametroFormViewModel.Parametro.Fecha_Creacion = DateTime.Now;
                        parametroFormViewModel.Parametro.Usuario_Creacion = "iarias";
                        parametroFormViewModel.Parametro.Activa = "1";

                        parametroFormViewModel.Parametro.Cantidad_Dimensiones = 0; //TMP
                        parametroFormViewModel.Parametro.Dimension = ""; //TMP
                        parametroFormViewModel.Parametro.IdUOM = 1; //TMP
                        parametroFormViewModel.Parametro.Entrada_Manual = ""; //TMP
                        parametroFormViewModel.Parametro.Resultado = "S"; //TMP
                        parametroFormViewModel.Parametro.Fecha_UltMod = DateTime.Now; //TMP
                        parametroFormViewModel.Parametro.Usuario_UltMod = "iarias"; //TMP
                        
                        ParametroService.Create(parametroFormViewModel.Parametro);

                        spnEstado.Attributes["class"] = "label label-success";
                        lblEstado.Text = "Cargado";
                    }
                    catch (Exception ex)
                    {
                        spnEstado.Attributes["class"] = "label label-danger";
                        lblEstado.Text = "Error";
                    }
                }
            }
        }

        protected void btnCargueMasivoCerrar_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = true;
            this.pnlCargueMasivo.Visible = false;

            CargarParametros();
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            if (this.upfArchivo.PostedFile != null
                && (Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xlsx"
                    || Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xls"))
            {
                CargarParametrosExcel();
            }
        }

        protected void btnExportarDescargar_Click(object sender, EventArgs e)
        {
            if (this.txtNombreArchivo.Text.Trim() != "")
            {
                GenerarExcel();
            }
        }

        protected void btnExportarCerrar_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = true;
            this.pnlExportar.Visible = false;
        }

        private void BindData()
        {
            CargarParametros();
            CargarModelos();
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
            CargarSets();

            this.grvCargueMasivo.DataSource = new List<string>();
            this.grvCargueMasivo.DataBind();
        }

        private void CargarParametros()
        {
            parametroFormViewModel.Parametros = ParametroService.GetAll().ToList();

            if (this.ddlFiltroVersiones.SelectedValue != "0"
                && this.ddlFiltroVersiones.SelectedValue != "")
            {
                long idVersion = Convert.ToInt64(this.ddlFiltroVersiones.SelectedValue);
                parametroFormViewModel.Parametros = parametroFormViewModel.Parametros.Where(pv => pv.IdVersion == idVersion).ToList();
            }

            this.grvDatos.DataSource = parametroFormViewModel.Parametros;
            this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void CargarModelos()
        {
            parametroFormViewModel.Modelos = ModeloService.GetAll().ToList();

            this.ddlFiltroModelos.DataSource = parametroFormViewModel.Modelos;
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
                parametroFormViewModel.Versiones = VersionService.GetAll().ToList();
                int idModelo = Convert.ToInt32(ddlPadre.SelectedValue);
                parametroFormViewModel.Versiones = parametroFormViewModel.Versiones.Where(par => par.IDSubModelo == idModelo).ToList();
            }
            else
            {
                ddlDestino.Items.Clear();
            }

            ddlDestino.DataValueField = "IDVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = parametroFormViewModel.Versiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }

        private void CargarSets()
        {
            parametroFormViewModel.Sets = SetService.GetAll().ToList();

            this.ddlDimension.DataSource = parametroFormViewModel.Sets;
            this.ddlDimension.DataTextField = "Nombre";
            this.ddlDimension.DataValueField = "IDSet";
            this.ddlDimension.DataBind();
            this.ddlDimension.Items.Insert(0, new ListItem { Value = "", Text = "(Set)" });
        }

        private void CargarParametro(long idParametro)
        {
            parametroFormViewModel.Parametro = ParametroService.GetById(Convert.ToInt32(idParametro));

            if (parametroFormViewModel.Parametro != null)
            {
                this.lblIdParametro.Text = parametroFormViewModel.Parametro.IDParameter.ToString();
                this.txtNombre.Text = parametroFormViewModel.Parametro.Nombre;
                this.txtDescripcion.Text = parametroFormViewModel.Parametro.Descripcion;
                this.txtAlias.Text = parametroFormViewModel.Parametro.AliasGAMS;
                this.txtDimension.Text = parametroFormViewModel.Parametro.Cantidad_Dimensiones.ToString();
                this.hddDimensionVals.Value = parametroFormViewModel.Parametro.Dimension;
                this.chkActivo.Checked = parametroFormViewModel.Parametro.Activa == "1";
            }
        }

        private void CargarParametrosExcel()
        {
            ExcelPackage excel = new ExcelPackage(this.upfArchivo.PostedFile.InputStream);
            DataTable tbl = new DataTable();
            ExcelWorksheet ws = excel.Workbook.Worksheets.First();
            bool hasHeader = true;

            foreach (ExcelRangeBase firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(hasHeader ? firstRowCell.Text : String.Format("Column {0}", firstRowCell.Start.Column));
            }

            if (tbl.Columns.Count > 0)
            {
                int startRow = hasHeader ? 2 : 1;

                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    ExcelRange wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.NewRow();

                    foreach (ExcelRangeBase cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }

                    tbl.Rows.Add(row);
                }
            }

            parametroFormViewModel.Parametros = new List<Parametro>();

            foreach (DataRow dr in tbl.Rows)
            {
                parametroFormViewModel.Parametro = new Parametro();
                parametroFormViewModel.Parametro.Nombre = dr[0].ToString();
                parametroFormViewModel.Parametro.Descripcion = dr[1] != null ? dr[1].ToString() : null;
                parametroFormViewModel.Parametro.AliasGAMS = dr[2] != null ? dr[2].ToString() : null;

                parametroFormViewModel.Parametros.Add(parametroFormViewModel.Parametro);
            }

            if (parametroFormViewModel.Parametros.Count > 0)
            {
                this.pnlMensajeCargueMasivo.Visible = false;
                this.btnCargueMasivoCargar.CssClass = "btn-sm btn btn-primary";
                this.btnCargueMasivoCargar.Enabled = true;
            }
            else
            {
                this.pnlMensajeCargueMasivo.Visible = true;
                this.btnCargueMasivoCargar.CssClass = "btn-sm btn btn-primary disabled";
                this.btnCargueMasivoCargar.Enabled = false;
            }


            this.grvCargueMasivo.DataSource = parametroFormViewModel.Parametros;
            this.grvCargueMasivo.DataBind();
        }

        private void GenerarExcel()
        {
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add("Datos");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = GetCellByName(this.grvExportar.HeaderRow, "Nombre").ContainingField.ToString();
            workSheet.Cells[1, 2].Value = GetCellByName(this.grvExportar.HeaderRow, "Descripcion").ContainingField.ToString();
            workSheet.Cells[1, 3].Value = GetCellByName(this.grvExportar.HeaderRow, "AliasGAMS").ContainingField.ToString();

            int recordIndex = 2;

            foreach (GridViewRow row in this.grvExportar.Rows)
            {
                CheckBox chkActivo = (CheckBox)row.FindControl("chkActivo");

                if (chkActivo.Checked)
                {
                    workSheet.Cells[recordIndex, 1].Value = GetCellByName(row, "Nombre").Text;
                    workSheet.Cells[recordIndex, 2].Value = GetCellByName(row, "Descripcion").Text;
                    workSheet.Cells[recordIndex, 3].Value = GetCellByName(row, "AliasGAMS").Text;
                    recordIndex++;
                }
            }

            if (recordIndex > 2)
            {
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();

                string excelName = this.txtNombreArchivo.Text;
                MemoryStream memoryStream = new MemoryStream();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        private Parametro LlenarParameter(Parametro parameter)
        {
            parameter.Nombre = this.txtNombre.Text;
            parameter.Descripcion = this.txtDescripcion.Text;
            parameter.AliasGAMS = this.txtAlias.Text;

            return parameter;
        }

        private void LimpiarCampos(bool esUpdate)
        {
            this.pnlIdParametro.Visible = esUpdate;
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.txtAlias.Text = "";
            this.txtDimension.Text = "";
            this.txtDimension.Enabled = !esUpdate;
            this.ddlDimension.Enabled = !esUpdate;
            this.lblTextboxDimension.Attributes["class"] = esUpdate ? "input state-disabled" : "input";
            this.lblSelectDimension.Attributes["class"] = esUpdate ? "select state-disabled" : "select";
            this.pnlActivo.Visible = esUpdate;
        }

        public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
        {
            foreach (DataControlFieldCell Cell in Row.Cells)
            {
                if (Cell.ContainingField is BoundField)
                {
                    if (((BoundField)(Cell.ContainingField)).DataField == CellName)
                    {
                        return Cell;
                    }
                }
            }

            return null;
        }
    }
}