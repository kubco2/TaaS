using BL.DTO;
using BL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GroupsController : Controller
    {
        private GroupFacade groupFacade;

        public GroupsController(UserFacade uf, GroupFacade groupFacade) : base(uf)
        {
            this.groupFacade = groupFacade;
        }

        
        public ActionResult Index()
        {
            var groupViewModel = new GroupViewModel()
            {
                Groups = groupFacade.GetAllGroups(userId)
            };
            return View(groupViewModel);
        }

        public ActionResult New()
        {
            var groupViewEditModel = new GroupViewEditModel()
            {
                Group = new GroupDTO()
            };
            return View(groupViewEditModel);
        }

        [HttpPost]
        public ActionResult New(GroupViewEditModel model)
        {
            model.Group.OwnerId = userId;
            groupFacade.CreateGroup(model.Group);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var group = groupFacade.GetGroupById(id);
            if(userId != group.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var groupViewEditModel = new GroupViewEditModel()
            {
                Group = group
            };
            return View(groupViewEditModel);
        }

        [HttpPost]
        public ActionResult Edit(GroupViewEditModel model)
        {
            var group = groupFacade.GetGroupById(model.Group.Id);
            if (userId != group.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            model.Group.OwnerId = userId;
            groupFacade.UpdateGroup(model.Group);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var group = groupFacade.GetGroupById(id);
            if (userId != group.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var groupViewEditModel = new GroupViewEditModel()
            {
                Group = group
            };
            return View(groupViewEditModel);
        }

        [HttpPost]
        public ActionResult Delete(GroupViewEditModel model)
        {
            var group = groupFacade.GetGroupById(model.Group.Id);
            if (userId != group.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            groupFacade.DeleteGroup(model.Group.Id);
            return RedirectToAction("Index");
        }
    }
}