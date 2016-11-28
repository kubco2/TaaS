using BL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class QuestionCategoriesController : Controller
    {
        private QuestionCategoryFacade categoryFacade;

        public QuestionCategoriesController(UserFacade uf, QuestionCategoryFacade categoryFacade) : base(uf)
        {
            this.categoryFacade = categoryFacade;
        }

        public ActionResult Index()
        {
            var categoriesViewModel = new QuestionCategoryViewModel()
            {
                Categories = categoryFacade.GetAllQuestionCategorys(userId)
            };
            return View(categoriesViewModel);
        }

        public ActionResult New()
        {
            var categories = categoryFacade.GetAllQuestionCategorys(userId);
            categories.Insert(0, null);
            var categoryViewEditModel = new QuestionCategoryViewEditModel()
            {
                Category = new BL.DTO.QuestionCategoryDTO(),
                Categories = categories
            };
            return View(categoryViewEditModel);
        }

        [HttpPost]
        public ActionResult New(QuestionCategoryViewEditModel model)
        {
            model.Category.OwnerId = userId;
            categoryFacade.CreateQuestionCategory(model.Category);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var category = categoryFacade.GetQuestionCategoryById(id);
            if (userId != category.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var categories = categoryFacade.GetAllQuestionCategorys(userId);
            categories.Insert(0, null);
            var categoryViewEditModel = new QuestionCategoryViewEditModel()
            {
                Category = category,
                Categories = categories
            };
            return View(categoryViewEditModel);
        }

        [HttpPost]
        public ActionResult Edit(QuestionCategoryViewEditModel model)
        {
            var category = categoryFacade.GetQuestionCategoryById(model.Category.Id);
            if (userId != category.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            model.Category.OwnerId = userId;
            categoryFacade.UpdateQuestionCategory(model.Category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var category = categoryFacade.GetQuestionCategoryById(id);
            if (userId != category.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var categoryViewEditModel = new QuestionCategoryViewEditModel()
            {
                Category = category
            };
            return View(categoryViewEditModel);
        }

        [HttpPost]
        public ActionResult Delete(QuestionCategoryViewEditModel model)
        {
            var category = categoryFacade.GetQuestionCategoryById(model.Category.Id);
            if (userId != category.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            categoryFacade.DeleteQuestionCategory(model.Category.Id);
            return RedirectToAction("Index");
        }
    }
}
