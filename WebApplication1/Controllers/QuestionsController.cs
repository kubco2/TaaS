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
    public class QuestionsController : Controller
    {
        private QuestionFacade questionFacade;
        private AnswerFacade answerFacade;
        private QuestionCategoryFacade categoryFacade;

        public QuestionsController(UserFacade uf, QuestionFacade questionFacade, AnswerFacade answerFacade, QuestionCategoryFacade categoryFacade) : base(uf)
        {
            this.questionFacade = questionFacade;
            this.answerFacade = answerFacade;
            this.categoryFacade = categoryFacade;
        }

        // GET: Questions
        public ActionResult Index(int? categoryId)
        {
            var questionViewModel = new QuestionViewModel()
            {
                Questions = categoryId == null ? questionFacade.GetAllQuestions(userId) : questionFacade.GetQuestionsByCategory((int)categoryId, userId),
                Categories = categoryFacade.GetAllQuestionCategorys(userId)
            };
            return View(questionViewModel);
        }

        public ActionResult Edit(int id)
        {
            var question = questionFacade.GetQuestionById(id);
            if (userId != question.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }

            var questionViewEditModel = new QuestionViewEditModel()
            {
                Question = question,
                Answers = answerFacade.GetAnswersByQuestion(id),
                Categories = categoryFacade.GetAllQuestionCategorys(userId)
            };

            return View(questionViewEditModel);
        }

        [HttpPost]
        public ActionResult Edit(QuestionViewEditModel model)
        {
            var question = questionFacade.GetQuestionById(model.Question.Id);
            if (userId != question.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            model.Question.OwnerId = userId;
            questionFacade.UpdateQuestion(model.Question);
            return RedirectToAction("Edit", new { id = model.Question.Id });
        }

        public ActionResult New()
        {
            var questionViewEditModel = new QuestionViewEditModel()
            {
                Question = new QuestionDTO(),
                Categories = categoryFacade.GetAllQuestionCategorys(userId)
            };
            return View(questionViewEditModel);
        }

        [HttpPost]
        public ActionResult New(QuestionViewEditModel model)
        {
            model.Question.OwnerId = userId;
            questionFacade.CreateQuestion(model.Question);
            return RedirectToAction("Edit", new { id = model.Question.Id });
        }

        public ActionResult Delete(int id)
        {
            var question = questionFacade.GetQuestionById(id);
            if (userId != question.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var questionViewEditModel = new QuestionViewEditModel()
            {
                Question = question
            };
            return View(questionViewEditModel);
        }

        [HttpPost]
        public ActionResult Delete(QuestionViewEditModel model)
        {
            var question = questionFacade.GetQuestionById(model.Question.Id);
            if (userId != question.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            questionFacade.DeleteQuestion(model.Question.Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditAnswer(int id, int questionId)
        {
            var answer = answerFacade.GetAnswerById(id);
            if (userId != answer.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var answerViewEditModel = new AnswerViewEditModel()
            {
                Answer = answer
            };

            return View(answerViewEditModel);
        }

        [HttpPost]
        public ActionResult EditAnswer(AnswerViewEditModel model, int questionId)
        {
            var answer = answerFacade.GetAnswerById(model.Answer.Id);
            if (userId != answer.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            model.Answer.OwnerId = userId;
            answerFacade.UpdateAnswer(model.Answer, questionId);
            return RedirectToAction("Edit", new { id = questionId });
        }

        public ActionResult NewAnswer(int questionId)
        {
            var answerViewEditModel = new AnswerViewEditModel()
            {
                Answer = new AnswerDTO()
            };

            return View(answerViewEditModel);
        }

        [HttpPost]
        public ActionResult NewAnswer(AnswerViewEditModel model, int questionId)
        {
            model.Answer.OwnerId = userId;
            answerFacade.CreateAnswer(model.Answer, questionId);
            return RedirectToAction("Edit", new { id = questionId });
        }

        public ActionResult DeleteAnswer(int id, int questionId)
        {
            var answer = answerFacade.GetAnswerById(id);
            if (userId != answer.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            var answerViewEditModel = new AnswerViewEditModel()
            {
                Answer = answer
            };

            return View(answerViewEditModel);
        }

        [HttpPost]
        public ActionResult DeleteAnswer(AnswerViewEditModel model, int questionId)
        {
            var answer = answerFacade.GetAnswerById(model.Answer.Id);
            if (userId != answer.OwnerId)
            {
                throw new UnauthorizedAccessException();
            }
            answerFacade.DeleteAnswer(model.Answer.Id);
            return RedirectToAction("Edit", new { id = questionId });
        }
    }
}