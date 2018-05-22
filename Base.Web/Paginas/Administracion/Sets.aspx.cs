﻿using Base.Model.Models;
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

            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            this.hddIdSet.Value = "0";

            LimpiarCampos(false);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.hddIdSet.Value != "0")
            {
                long idSet = Convert.ToInt64(this.hddIdSet.Value);
                setFormViewModel.Set = SetService.GetById(Convert.ToInt32(idSet));

                if (setFormViewModel.Set != null)
                {
                    setFormViewModel.Set = LlenarSet(setFormViewModel.Set);
                    setFormViewModel.Set.Fecha_UltMod = DateTime.Now;
                    setFormViewModel.Set.Usuario_UltMod = "iarias";
                    setFormViewModel.Set.Activa = this.chkActivo.Checked ? "1" : "0";

                    SetService.Update(setFormViewModel.Set);
                }
            }
            else
            {
                setFormViewModel.Set = LlenarSet(new Set());
                setFormViewModel.Set.Fecha_Creacion = DateTime.Now;
                setFormViewModel.Set.Fecha_UltMod = DateTime.Now;
                setFormViewModel.Set.Usuario_Creacion = "iarias";
                setFormViewModel.Set.Usuario_UltMod = "iarias";
                setFormViewModel.Set.Activa = "1";
                SetService.Create(setFormViewModel.Set);
            }

            CargarSets();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnCerrarModal').click();", true);
        }

        protected void grvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.hddIdSet.Value = e.CommandArgument.ToString();

            if (e.CommandName == "EditarSet")
            {
                this.lblModalTitulo.Text = "Editar Set";
                
                LimpiarCampos(true);
                CargarSet(Convert.ToInt64(this.hddIdSet.Value));

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "$('#btnAgregarModal').click();", true);
            }
            else if (e.CommandName == "EliminarSet")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "AlertMessage", "mostrarConfirm('eliminar', '#" + this.btnEliminar.ClientID + "');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.hddIdSet.Value != "0")
            {
                long idSet = Convert.ToInt64(this.hddIdSet.Value);

                setFormViewModel.Set = new Set();
                setFormViewModel.Set.IDSet = idSet;
                setFormViewModel.Set.Fecha_UltMod = DateTime.Now;
                setFormViewModel.Set.Usuario_UltMod = "iarias";

                SetService.Delete(setFormViewModel.Set);

                CargarSets();
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

            setFormViewModel.Sets = SetService.GetAll().ToList();

            this.grvExportar.DataSource = setFormViewModel.Sets;
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
                        setFormViewModel.Set = new Set();
                        setFormViewModel.Set.Nombre = item.Cells[0].Text;
                        setFormViewModel.Set.Descripcion = item.Cells[1] != null ? item.Cells[1].Text : null;
                        setFormViewModel.Set.AliasGAMS = item.Cells[2] != null ? item.Cells[2].Text : null;
                        setFormViewModel.Set.Fecha_Creacion = DateTime.Now;
                        setFormViewModel.Set.Fecha_UltMod = DateTime.Now;
                        setFormViewModel.Set.Usuario_Creacion = "iarias";
                        setFormViewModel.Set.Usuario_UltMod = "iarias";
                        setFormViewModel.Set.Activa = "1";

                        SetService.Create(setFormViewModel.Set);

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

            CargarSets();
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            if (this.upfArchivo.PostedFile != null
                && (Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xlsx"
                    || Path.GetExtension(this.upfArchivo.PostedFile.FileName) == ".xls"))
            {
                CargarSetsExcel();
            }
        }

        protected void btnExportarDescargar_Click(object sender, EventArgs e)
        {
            if (this.txtNombreArchivo.Text.Trim() != "")
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
                    //this.pnlMensajeExportar.Visible = false;

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
                //else
                //{
                //    this.pnlMensajeExportar.Visible = true;
                //}
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

            this.grvDatos.DataSource = setFormViewModel.Sets;
            this.grvDatos.DataBind();
            this.grvDatos.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            else
            {
                ddlDestino.Items.Clear();
            }

            ddlDestino.DataValueField = "IDVersion";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataSource = setFormViewModel.Versiones;
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem { Value = "0", Text = "(VERSION)" });
        }

        private void CargarSet(long idSet)
        {
            setFormViewModel.Set = SetService.GetById(Convert.ToInt32(idSet));

            if (setFormViewModel.Set != null)
            {
                this.lblIdSet.Text = setFormViewModel.Set.IDSet.ToString();
                this.txtNombre.Text = setFormViewModel.Set.Nombre;
                this.txtDescripcion.Text = setFormViewModel.Set.Descripcion;
                this.txtAlias.Text = setFormViewModel.Set.AliasGAMS;
                this.chkActivo.Checked = setFormViewModel.Set.Activa == "1";
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

            if (setFormViewModel.Sets.Count > 0)
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
            

            this.grvCargueMasivo.DataSource = setFormViewModel.Sets;
            this.grvCargueMasivo.DataBind();
        }

        private Set LlenarSet(Set set)
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