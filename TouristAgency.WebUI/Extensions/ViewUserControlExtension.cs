using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace TouristAgency.WebUI.ClassesForViews
{
    public static class ViewUserControlExtension
    {
        public static bool IsAdmin(this WebPageRenderingBase pg)
        {
            return pg.Page.User.IsInRole("admin");
        }
    }
}