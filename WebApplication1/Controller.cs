using BL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebApplication1
{
    [Authorize]
    public class Controller: System.Web.Mvc.Controller
    {
        protected UserFacade userFacade;
        protected int userId;

        public Controller(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            try
            {
                userId = userFacade.GetUserBySystemId(User.Identity.GetUserId()).Id;
            } catch(InvalidOperationException e)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                filterContext.Result = new RedirectResult("/");
                return;
            }

        }
    }
}