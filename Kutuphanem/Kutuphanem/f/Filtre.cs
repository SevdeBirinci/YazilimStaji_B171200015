using Kutuphanem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Kutuphanem.f
{
    public class Filtre : ActionFilterAttribute
    {
        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            Kullanici kullanici = (Kullanici)filterContext.HttpContext.Session["giris"];
            string controller = filterContext.RouteData.Values["controller"].ToString();
            if (kullanici == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Kullanicilar",
                    action = "GirisYap"
                }));
            }
            else
            {
                
            }
       
           
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
