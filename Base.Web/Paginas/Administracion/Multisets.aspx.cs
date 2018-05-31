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
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SeedProject.Paginas.Administracion
{
    public partial class Multisets : System.Web.UI.Page
    {
        private MultisetFormViewModel multisetFormViewModel = new MultisetFormViewModel();

        public IMultisetService multisetService { get; set; }

        public IModeloService ModeloService { get; set; }

        public IVersionService VersionService { get; set; }

        public ISetService SetService { get; set; }

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
                CargarMultisets();
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
                this.lblModalTitulo.Text = "Agregar Multiset";
                this.hddIdMultiset.Value = "0";
                this.hddDimensionVals.Value = "";

                LimpiarCampos(false);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
            catch (Exception ex)
            {
                var message = new JavaScriptSerializer().Serialize(ExceptionService.ConvertirError(ex));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", string.Format("mostrarMensajeEtiqueta({0},{1},{2});", "'#divMensajeMain'", "'error'", message), true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.hddIdMultiset.Value != "0")
                {
                    long idMultiset = Convert.ToInt64(this.hddIdMultiset.Value);
                    multisetFormViewModel.Multiset = multisetService.GetById(Convert.ToInt32(idMultiset));

                    if (multisetFormViewModel.Multiset != null)
                    {
                        multisetFormViewModel.Multiset = LlenarMultiset(multisetFormViewModel.Multiset);
                        multisetFormViewModel.Multiset.Cantidad_Dimensiones = Convert.ToInt64(this.txtDimension.Text) > 10 ? 10 : Convert.ToInt64(this.txtDimension.Text);
                        multisetFormViewModel.Multiset.Dimension = this.hddDimensionVals.Value;
                        //multisetFormViewModel.Multiset.Fecha_UltMod = DateTime.Now;
                        multisetFormViewModel.Multiset.Usuario_UltMod = "iarias";
                        multisetFormViewModel.Multiset.Activa = this.chkActivo.Checked ? "1" : "0";

                        multisetService.Update(multisetFormViewModel.Multiset);
                    }
                }
                else
                {
                    multisetFormViewModel.Multiset = LlenarMultiset(new Multiset());
                    multisetFormViewModel.Multiset.Cantidad_Dimensiones = Convert.ToInt64(this.txtDimension.Text) > 10 ? 10 : Convert.ToInt64(this.txtDimension.Text);
                    multisetFormViewModel.Multiset.Dimension = this.hddDimensionVals.Value;
                    //multisetFormViewModel.Multiset.Fecha_Creacion = DateTime.Now;
                    multisetFormViewModel.Multiset.Usuario_Creacion = "iarias";
                    multisetFormViewModel.Multiset.Activa = "1";
                    //multisetFormViewModel.Multiset.Fecha_UltMod = DateTime.Now; //TMP
                    multisetFormViewModel.Multiset.Usuario_UltMod = "iarias"; //TMP

                    multisetService.Create(multisetFormViewModel.Multiset);
                }

                CargarMultisets();

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
                this.hddIdMultiset.Value = e.CommandArgument.ToString();

                if (e.CommandName == "EditarMultiset")
                {
                    this.lblModalTitulo.Text = "Editar Multiset";
                    CargarSets();

                    LimpiarCampos(true);

                    CargarMultiset(Convert.ToInt64(this.hddIdMultiset.Value));

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();txtDimensionOnKeyPress();", true);
                }
                else if (e.CommandName == "EliminarMultiset")
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
                if (this.hddIdMultiset.Value != "0")
                {
                    long idMultiset = Convert.ToInt64(this.hddIdMultiset.Value);

                    multisetFormViewModel.Multiset = new Multiset();
                    multisetFormViewModel.Multiset.IDMultiSet = idMultiset;
                    multisetFormViewModel.Multiset.Fecha_UltMod = DateTime.Now;
                    multisetFormViewModel.Multiset.Usuario_UltMod = "iarias";

                    multisetService.Delete(multisetFormViewModel.Multiset);

                    CargarMultisets();
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
            this.grvCargueMasivo.DataSource = new List<string>();
            this.grvCargueMasivo.DataBind();
            this.grvCargueMasivo.HeaderRow.TableSection = TableRowSection.TableHeader;

            this.pnlDatos.Visible = false;
            this.pnlCargueMasivo.Visible = true;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlDatos.Visible = false;
                this.pnlExportar.Visible = true;

                multisetFormViewModel.Multisets = multisetService.GetAll().ToList();

                this.grvExportar.DataSource = multisetFormViewModel.Multisets;
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
                        try
                        {
                            multisetFormViewModel.Multiset = new Multiset();
                            multisetFormViewModel.Multiset.Nombre = HttpUtility.HtmlDecode(item.Cells[0].Text);
                            multisetFormViewModel.Multiset.Descripcion = item.Cells[1] != null ? HttpUtility.HtmlDecode(item.Cells[1].Text) : null;
                            multisetFormViewModel.Multiset.AliasGAMS = item.Cells[2] != null ? HttpUtility.HtmlDecode(item.Cells[2].Text) : null;
                            multisetFormViewModel.Multiset.Dimensiones = item.Cells[3] != null ? HttpUtility.HtmlDecode(item.Cells[3].Text) : null;
                            //multisetFormViewModel.Multiset.Fecha_Creacion = DateTime.Now;
                            multisetFormViewModel.Multiset.Usuario_Creacion = "iarias";
                            multisetFormViewModel.Multiset.Activa = "1";

                            multisetFormViewModel.Multiset.Cantidad_Dimensiones = multisetFormViewModel.Multiset.Dimensiones.Split('.').Length; 
                            multisetFormViewModel.Multiset.Dimension = multisetFormViewModel.Multiset.Dimensiones; //TMP
                            //multisetFormViewModel.Multiset.Fecha_UltMod = DateTime.Now; //TMP
                            //multisetFormViewModel.Multiset.Usuario_UltMod = "iarias"; //TMP

                            multisetService.Create(multisetFormViewModel.Multiset);

                            spnEstado.Attributes["class"] = "label label-success";
                            lblEstado.Text = "Cargado";
                        }
                        catch (Exception ex)
                        {
                            errorCargue = ex;
                            spnEstado.Attributes["class"] = "label label-danger";
                            lblEstado.Text = "Error";
                            lblEstado.ToolTip = ex.Message;
                        }
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

                CargarMultisets();
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
                    CargarMultisetsExcel();

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
            CargarMultisets();
            CargarModelos();
            CargarVersiones(this.ddlFiltroVersiones, this.ddlFiltroModelos);
            CargarSets();

            this.grvCargueMasivo.DataSource = new List<string>();
            this.grvCargueMasivo.DataBind();

            this.grvExportar.DataSource = new List<string>();
            this.grvExportar.DataBind();
        }

        private void CargarMultisets()
        {
            multisetFormViewModel.Multisets = multisetService.GetAll().ToList();

            if (this.ddlFiltroVersiones.SelectedValue != "0"
                && this.ddlFiltroVersiones.SelectedValue != "")
            {
                long idVersion = Convert.ToInt64(this.ddlFiltroVersiones.SelectedValue);
                multisetFormViewModel.Multisets = multisetFormViewModel.Multisets.Where(pv => pv.IdVersion == idVersion).ToList();
            }

            this.grvDatos.DataSource = multisetFormViewModel.Multisets;
            this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void CargarModelos()
        {
            multisetFormViewModel.Modelos = ModeloService.GetAll().ToList();

            this.ddlFiltroModelos.DataSource = multisetFormViewModel.Modelos;
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
                multisetFormViewModel.Versiones = VersionService.GetAll().ToList();
                int idModelo = Convert.ToInt32(ddlPadre.SelectedValue);
                multisetFormViewModel.Versiones = multisetFormViewModel.Versiones.Where(par => par.IDSubModelo == idModelo).ToList();
            }
            else
            {
                ddlDestino.Items.Clear();
            }

            ddlDestino.DataValueField = "IDVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = multisetFormViewModel.Versiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }

        private void CargarSets()
        {
            multisetFormViewModel.Sets = SetService.GetAll().ToList();

            this.ddlDimension.DataSource = multisetFormViewModel.Sets;
            this.ddlDimension.DataTextField = "Nombre";
            this.ddlDimension.DataValueField = "IDSet";
            this.ddlDimension.DataBind();
            this.ddlDimension.Items.Insert(0, new ListItem { Value = "", Text = "(Set)" });
        }

        private void CargarMultiset(long idMultiSet)
        {
            multisetFormViewModel.Multiset = multisetService.GetById(Convert.ToInt32(idMultiSet));

            if (multisetFormViewModel.Multiset != null)
            {
                this.lblIdMultiset.Text = multisetFormViewModel.Multiset.IDMultiSet.ToString();
                this.txtNombre.Text = multisetFormViewModel.Multiset.Nombre;
                this.txtDescripcion.Text = multisetFormViewModel.Multiset.Descripcion;
                this.txtAlias.Text = multisetFormViewModel.Multiset.AliasGAMS;
                this.txtDimension.Text = multisetFormViewModel.Multiset.Cantidad_Dimensiones.ToString();
                this.hddDimensionVals.Value = multisetFormViewModel.Multiset.Dimension;
                this.chkActivo.Checked = multisetFormViewModel.Multiset.Activa == "1";

                var esUpdate = false;
                if (multisetFormViewModel.Multiset.Cantidad_Dimensiones.ToString() == "0") {
                    esUpdate = true;
                }
                this.lblTextboxDimension.Attributes["class"] = esUpdate ? "input state-disabled" : "input";
                this.lblSelectDimension.Attributes["class"] = esUpdate ? "select state-disabled" : "select";

            }
        }

        private void CargarMultisetsExcel()
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

            multisetFormViewModel.Multisets = new List<Multiset>();

            foreach (DataRow dr in tbl.Rows)
            {
                multisetFormViewModel.Multiset = new Multiset();
                multisetFormViewModel.Multiset.Nombre = HttpUtility.HtmlDecode(dr[0].ToString());
                multisetFormViewModel.Multiset.Descripcion = dr[1] != null ? HttpUtility.HtmlDecode(dr[1].ToString()) : null;
                multisetFormViewModel.Multiset.AliasGAMS = dr[2] != null ? HttpUtility.HtmlDecode(dr[2].ToString()) : null;
                multisetFormViewModel.Multiset.Dimensiones = dr[3] != null ? HttpUtility.HtmlDecode(dr[3].ToString()) : null;

                multisetFormViewModel.Multisets.Add(multisetFormViewModel.Multiset);
            }

            this.grvCargueMasivo.DataSource = multisetFormViewModel.Multisets;
            this.grvCargueMasivo.DataBind();
            this.grvCargueMasivo.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (multisetFormViewModel.Multisets.Count > 0)
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
            workSheet.Cells[1, 1].Value = HttpUtility.HtmlDecode(GetCellByName(this.grvExportar.HeaderRow, "Nombre").ContainingField.ToString());
            workSheet.Cells[1, 2].Value = HttpUtility.HtmlDecode(GetCellByName(this.grvExportar.HeaderRow, "Descripcion").ContainingField.ToString());
            workSheet.Cells[1, 3].Value = HttpUtility.HtmlDecode(GetCellByName(this.grvExportar.HeaderRow, "AliasGAMS").ContainingField.ToString());
            workSheet.Cells[1, 4].Value = HttpUtility.HtmlDecode(GetCellByName(this.grvExportar.HeaderRow, "Dimensiones").ContainingField.ToString());
            //workSheet.Cells[1, 5].Value = HttpUtility.HtmlDecode(GetCellByName(this.grvExportar.HeaderRow, "Cantidad_Dimensiones").ContainingField.ToString());

            int recordIndex = 2;

            foreach (GridViewRow row in this.grvExportar.Rows)
            {
                CheckBox chkActivo = (CheckBox)row.FindControl("chkActivo");

                if (chkActivo.Checked)
                {
                    workSheet.Cells[recordIndex, 1].Value = HttpUtility.HtmlDecode(GetCellByName(row, "Nombre").Text);
                    workSheet.Cells[recordIndex, 2].Value = HttpUtility.HtmlDecode(GetCellByName(row, "Descripcion").Text);
                    workSheet.Cells[recordIndex, 3].Value = HttpUtility.HtmlDecode(GetCellByName(row, "AliasGAMS").Text);
                    workSheet.Cells[recordIndex, 4].Value = HttpUtility.HtmlDecode(GetCellByName(row, "Dimensiones").Text);
                    //workSheet.Cells[recordIndex, 5].Value = HttpUtility.HtmlDecode(GetCellByName(row, "Cantidad_Dimensiones").Text);
                    recordIndex++;
                }
            }

            if (recordIndex > 2)
            {
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();

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

        private Multiset LlenarMultiset(Multiset estructura)
        {
            estructura.Nombre = this.txtNombre.Text.Trim();
            estructura.Descripcion = this.txtDescripcion.Text;
            estructura.AliasGAMS = this.txtAlias.Text;

            return estructura;
        }

        private void LimpiarCampos(bool esUpdate)
        {
            this.pnlIdMultiset.Visible = esUpdate;
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.txtAlias.Text = "";
            this.txtDimension.Text = "";
            this.txtDimension.Enabled = true;//!esUpdate;
            this.ddlDimension.Enabled = true;//!esUpdate;
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