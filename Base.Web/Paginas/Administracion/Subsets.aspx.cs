using Base.Model.Models;
using Base.Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OfficeOpenXml;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

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
            try
            {
                if (!IsPostBack)
                {
                    BindData();
                }

                this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
                this.grvCargueMasivo.HeaderRow.TableSection = TableRowSection.TableHeader;
                this.grvExportar.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void ddlFiltroModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarSets();
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblModalTitulo.Text = "Agregar Set";
                this.hddIdSet.Value = "0";

                LimpiarCampos(false);
                CargarSetsPadres(0);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
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
            try
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

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CloseModal", "$('#btnCerrarModal').click();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'success'", "'Registro guardado con exito!'"), true);
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeModal'", "'error'", message), true);
            }
        }

        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "mostrarConfirm('eliminar', '#" + this.btnEliminar.ClientID + "');", true);
                }
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
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

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'success'", "'Registro eliminado con exito!'"), true);
                }
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnCargueMasivo_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = false;
            this.pnlCargueMasivo.Visible = true;

            this.grvCargueMasivo.DataSource = new List<string>();
            this.grvCargueMasivo.DataBind();
            this.grvCargueMasivo.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlDatos.Visible = false;
                this.pnlExportar.Visible = true;

                setFormViewModel.Sets = SetService.GetAll().ToList();

                this.grvExportar.DataSource = setFormViewModel.Sets;
                this.grvExportar.DataBind();
                this.grvExportar.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnCargueMasivoCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Exception errorCargue = null;
                bool esMarcado = false;

                foreach (GridViewRow item in this.grvCargueMasivo.Rows)
                {
                    CheckBox chkActivo = (CheckBox)item.FindControl("chkActivo");
                    HtmlGenericControl spnEstado = (HtmlGenericControl)item.FindControl("spnEstado");
                    Label lblEstado = (Label)item.FindControl("lblEstado");

                    if (chkActivo.Checked)
                    {
                        esMarcado = true;

                        try
                        {
                            setFormViewModel.Set = new Set();
                            setFormViewModel.Set.Nombre = HttpUtility.HtmlDecode(item.Cells[0].Text);
                            setFormViewModel.Set.Descripcion = item.Cells[1] != null ? HttpUtility.HtmlDecode(item.Cells[1].Text) : null;
                            setFormViewModel.Set.AliasGAMS = item.Cells[2] != null ? HttpUtility.HtmlDecode(item.Cells[2].Text) : null;
                            setFormViewModel.Set.Fecha_Creacion = DateTime.Now;
                            setFormViewModel.Set.Usuario_Creacion = "iarias";
                            setFormViewModel.Set.Activa = "1";

                            setFormViewModel.Set.Fecha_UltMod = DateTime.Now; //TMP
                            setFormViewModel.Set.Usuario_UltMod = "iarias"; //TMP

                            SetService.Create(setFormViewModel.Set);

                            spnEstado.Attributes["class"] = "label label-success";
                            lblEstado.Text = "Cargado";
                        }
                        catch (Exception ex)
                        {
                            errorCargue = ex;
                            spnEstado.Attributes["class"] = "label label-danger";
                            lblEstado.Text = "Error";
                        }
                    }
                    else
                    {
                        spnEstado.Attributes["class"] = "label label-warning";
                        lblEstado.Text = "Espera";
                    }
                }

                if (errorCargue != null)
                {
                    throw errorCargue;
                }

                if (!esMarcado)
                {
                    throw new Exception("No se ha seleccionado ningun registro.");
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'success'", "'Cargue realizado con exito!'"), true);
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnCargueMasivoCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlDatos.Visible = true;
                this.pnlCargueMasivo.Visible = false;

                CargarSets();
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.upfArchivo.PostedFile != null
                    && (Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xlsx"
                        || Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xls"))
                {
                    CargarSetsExcel();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'success'", "'Cargue de archivo" + this.upfArchivo.PostedFile.FileName + " exito!'"), true);
                }
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnExportarDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNombreArchivo.Text.Trim() != "")
                {
                    GenerarExcel();
                }
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnExportarCerrar_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = true;
            this.pnlExportar.Visible = false;
        }

        private void BindData()
        {
            CargarSets();
            CargarModelos();
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);

            this.grvCargueMasivo.DataSource = new List<string>();
            this.grvCargueMasivo.DataBind();

            this.grvExportar.DataSource = new List<string>();
            this.grvExportar.DataBind();
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

        private void CargarSetsExcel()
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

            setFormViewModel.Sets = new List<Set>();

            foreach (DataRow dr in tbl.Rows)
            {
                setFormViewModel.Set = new Set();
                setFormViewModel.Set.Nombre = dr[0].ToString();
                setFormViewModel.Set.Descripcion = dr[1] != null ? dr[1].ToString() : null;
                setFormViewModel.Set.AliasGAMS = dr[2] != null ? dr[2].ToString() : null;

                setFormViewModel.Sets.Add(setFormViewModel.Set);
            }

            this.grvCargueMasivo.DataSource = setFormViewModel.Sets;
            this.grvCargueMasivo.DataBind();
            this.grvCargueMasivo.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (setFormViewModel.Sets.Count > 0)
            {
                this.btnCargueMasivoCargar.CssClass = "btn-sm btn btn-primary";
                this.btnCargueMasivoCargar.Enabled = true;
            }
            else
            {
                this.btnCargueMasivoCargar.CssClass = "btn-sm btn btn-primary disabled";
                this.btnCargueMasivoCargar.Enabled = false;

                throw new Exception("El archivo seleccionado no tiene una estructura valida.");
            }
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