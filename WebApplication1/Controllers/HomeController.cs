using BL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using BL.DTO;
using System.Security.Principal;

namespace WebApplication1.Controllers
{
    
    public class HomeController : Controller
    {
        private GroupFacade groupFacade;
        private UserTrialFacade trialFacade;

        public HomeController(UserFacade uf, GroupFacade groupFacade, UserTrialFacade trialFacade) : base(uf)
        {
            this.groupFacade = groupFacade;
            this.trialFacade = trialFacade;
        }

        public ActionResult Index()
        {
            var model = new HomeModel()
            {
                Groups = groupFacade.GetGroupsByUserId(userId)
            };
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            groupFacade.EnrollToGroup(userId, model.AuthCode);
            return RedirectToAction("Index");
        }

        
    }
}