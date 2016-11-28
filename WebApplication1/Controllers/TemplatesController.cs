using BL.DTO;
using BL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{

    [Authorize]
    public class TemplatesController : Controller
    {
        private GroupFacade groupFacade;
        private TestTemplateFacade templateFacade;
        private QuestionCategoryFacade categoryFacade;
        private UserTrialFacade trialFacade;

        public TemplatesController(UserFacade uf, TestTemplateFacade templateFacade, GroupFacade groupFacade, QuestionCategoryFacade categoryFacade, UserTrialFacade trialFacade) : base(uf)
        {
            this.groupFacade = groupFacade;
            this.templateFacade = templateFacade;
            this.categoryFacade = categoryFacade;
            this.trialFacade = trialFacade;
        }

        // GET: Templates
        public ActionResult Index(int? groupId)
        {
            var templateViewModel = new TemplateViewModel()
            {
                Templates = groupId == null ? templateFacade.GetAllTestTemplates(userId) : templateFacade.GetTestTemplatesByGroup((int)groupId, userId),
                Groups = groupFacade.GetAllGroups(userId)
            };
            return View(templateViewModel);
        }

        public ActionResult New()
        {
            var template = new TestTemplateDTO();
            template.DateFrom = DateTime.Now.Date; 
            template.DateTo = DateTime.Now.Date.AddDays(1);
            var templateViewEditModel = new TemplateViewEditModel()
            {
                Template = template,
                Groups = groupFacade.GetAllGroups(userId),
                Categories = categoryFacade.GetAllQuestionCategorys(userId)
            };
            return View(templateViewEditModel);
        }

        [HttpPost]
        public ActionResult New(TemplateViewEditModel model)
        {
            model.Template.OwnerId = userId;
            templateFacade.CreateTestTemplate(model.Template, model.SelectedCategories);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var template = templateFacade.GetTestTemplateById(id);
            if(template.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            var templateViewEditModel = new TemplateViewEditModel()
            {
                Template = template,
                Groups = groupFacade.GetAllGroups(userId),
                Categories = categoryFacade.GetAllQuestionCategorys(userId),
                SelectedCategories = template.QuestionCategories.Select(s => s.Id).ToArray()
            };

            return View(templateViewEditModel);
        }

        [HttpPost]
        public ActionResult Edit(TemplateViewEditModel model)
        {
            var template = templateFacade.GetTestTemplateById(model.Template.Id);
            if (template.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            model.Template.OwnerId = userId;
            templateFacade.UpdateTestTemplate(model.Template, model.SelectedCategories);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var template = templateFacade.GetTestTemplateById(id);
            if (template.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            var templateViewEditModel = new TemplateViewEditModel()
            {
                Template = template
            };
            return View(templateViewEditModel);
        }

        [HttpPost]
        public ActionResult Delete(TemplateViewEditModel model)
        {
            var template = templateFacade.GetTestTemplateById(model.Template.Id);
            if (template.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            templateFacade.DeleteTemplate(model.Template.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            var template = templateFacade.GetTestTemplateById(id);

            if (template.OwnerId != userId && !groupFacade.IsUserInGroup(template.GroupId, userId))
            {
                throw new UnauthorizedAccessException();
            }

            var model = new TemplateShowModel()
            {
                Template = template,
                CanCreateNew = trialFacade.CanCreateTrial(id, userId),
                Trials = template.OwnerId == userId ? trialFacade.GetTrialsByTemplate(id).OrderByDescending(e => e.Generated).ToList() : trialFacade.GetMyTrialsByTemplate(id, userId).OrderByDescending(e => e.Generated).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Show(TemplateShowModel model)
        {
            return RedirectToAction("Generate", "Trial", new { test = model.Template.Id });
        }
    }
}