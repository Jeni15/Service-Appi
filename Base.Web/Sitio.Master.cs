using Base.Service.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SeedProject
{
    public partial class Sitio : System.Web.UI.MasterPage
    {
        public IMenuService MenuService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMenusPadres();
            }
        }
            
        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<Base.Model.Models.Menu> menus = MenuService.Execute("GetParents", null).ToList();
                Base.Model.Models.Menu subMenu = (Base.Model.Models.Menu)e.Item.DataItem;
                int ID = Convert.ToInt32(subMenu.IdMenu.ToString());
                StringBuilder sb = new StringBuilder();

                List<Base.Model.Models.Menu> subMenus = MenuService.Execute("GetChildren", new Base.Model.Models.Menu { IdMenuPadre = subMenu.IdMenu }).ToList();

                if (subMenus.Count > 0)
                {
                    try
                    {
                        sb = AgregarSubMenu(subMenu, sb);
                    }
                    catch (Exception ex)
                    {
                        string errMes = ex.Message;
                    }
                }

                (e.Item.FindControl("lblSubMenu") as Literal).Text = sb.ToString();
            }
        }

        private void CargarMenusPadres()
        {
            //List<Base.Model.Models.Menu> menus = MenuService.GetAll().OrderBy(men => men.Orden).Where(men => men.IdMenuPadre == null).ToList();
            List<Base.Model.Models.Menu> menus = MenuService.Execute("GetParents", null).ToList();
            this.rptMenu.DataSource = menus;
            this.rptMenu.DataBind();
        }

        private StringBuilder AgregarSubMenu(Base.Model.Models.Menu subMenu, StringBuilder sb)
        {
            //List<Base.Model.Models.Menu> childItems = MenuService.GetAll().OrderBy(men => men.Orden).Where(men => men.IdMenuPadre == subMenu.IdMenu).ToList();
            List<Base.Model.Models.Menu> childItems = MenuService.Execute("GetChildren", new Base.Model.Models.Menu { IdMenuPadre = subMenu.IdMenu }).ToList();

            if (childItems.Count > 0)
            {
                sb.Append("<ul>");

                foreach (Base.Model.Models.Menu cItem in childItems)
                {
                    string menu = "<li><a href='%RUTA%'>%ICONO%%MENU%</a>";

                    menu = menu.Replace("%RUTA%", cItem.Url != null && cItem.Url != "" ? ResolveUrl(cItem.Url) : "#");
                    menu = menu.Replace("%ICONO%", cItem.Icono != null && cItem.Icono != "" ? "<i class='fa fa-lg fa-fw " + cItem.Icono + "'></i>" : "");
                    menu = menu.Replace("%MENU%", cItem.Nombre != null && cItem.Nombre != "" ? cItem.Nombre : "");

                    sb.Append(menu);

                    List<Base.Model.Models.Menu> subChilds = MenuService.GetAll().Where(men => men.IdMenuPadre == subMenu.IdMenu).ToList();

                    if (subChilds.Count > 0)
                    {
                        AgregarSubMenu(cItem, sb);
                    }

                    sb.Append("</li>");
                }

                sb.Append("</ul>");
            }

            return sb;
        }
    }
}