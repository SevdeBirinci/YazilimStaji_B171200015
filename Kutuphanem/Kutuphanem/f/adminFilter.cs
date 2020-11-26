using Kutuphanem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kutuphanem.f
{
    public class adminFilter : ActionFilterAttribute
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
                if (kullanici.Tip == true)
                {
                    //Admin

                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Kullanicilar",
                        action = "GirisYap"
                    }));
                }
            }


        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

    }
}