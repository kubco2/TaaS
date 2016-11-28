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
    public class TrialController : Controller
    {
        private UserTrialFacade trialFacade;

        public TrialController(UserFacade uf, UserTrialFacade trialFacade) : base(uf)
        {
            this.trialFacade = trialFacade;
        }
        
        public ActionResult Generate(int test)
        {
            if (trialFacade.CanCreateTrial(test, userId))
            {
                var trial = trialFacade.CreateUserTrial(test, userId);
                return RedirectToAction("Take", new { id = trial.Id });
            }

            return RedirectToAction("Show", "Templates", new { id = test });
        }

        public ActionResult Take(int id)
        {
            var trial = trialFacade.GetUserTrialById(id);
            if(trial.User.Id != userId)
            {
                throw new UnauthorizedAccessException();
            }

            var model = new TakeTrialModel()
            {
                Trial = trial
            };

            if (trial.Taken != null)
            {
                return View("AlreadyTaken", model);
            }
            else
            {
                trial.Taken = trialFacade.takeUserTrial(trial.Id);
            }

            
            return View(model);
        }

        [HttpPost]
        public ActionResult Take(TakeTrialModel model)
        {
            var trial = trialFacade.GetUserTrialById(model.Trial.Id);
            if (trial.User.Id != userId)
            {
                throw new UnauthorizedAccessException();
            }

            if (trial.Taken != null && trial.Closed == null && ((DateTime)trial.Taken).AddMinutes(trial.Test.Time + 2) >= DateTime.Now)
            {
                var answers = new List<int>();
                foreach (var q in trial.Questions)
                {
                    if (q.Multiple)
                    {
                        foreach (var a in q.Answers)
                        {
                            if (Request.Form.AllKeys.Contains("multipleChoice_" + q.Id + "_" + a.Id) && Request.Form["multipleChoice_" + q.Id + "_" + a.Id] != "false")
                            {
                                answers.Add(a.Id);
                            }
                        }
                    }
                    else
                    {
                        if (Request.Form.AllKeys.Contains("singleChoice_" + q.Id))
                        {
                            answers.Add(int.Parse(Request.Form["singleChoice_" + q.Id]));
                        }
                    }
                }
                trialFacade.UpdateUserTrialAnswers(trial.Id, answers.ToArray());
                trialFacade.closeUserTrial(trial.Id);
            }
            
            return RedirectToAction("Show", "Templates", new { id = trial.Test.Id });
        }

        public ActionResult Show(int id)
        {
            var trial = trialFacade.GetUserTrialById(id);
            if (trial.User.Id != userId && trial.Test.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }

            var model = new TakeTrialModel()
            {
                Trial = trial
            };

            if (trial.Taken == null || 
                (
                    trial.Closed == null &&
                    trial.Taken != null && 
                    ((DateTime)trial.Taken).AddMinutes(trial.Test.Time + 2) >= DateTime.Now
                ))
            {
                
                return View("NotClosed", model);
            }
            else
            {
                var aChecks = new Dictionary<int, int>();
                var qSums = new Dictionary<int, double>();

                foreach (var question in trial.Questions)
                {
                    qSums[question.Id] = 0;
                    foreach (var answer in question.Answers)
                    {
                        aChecks[answer.Id] = trial.CheckAnswer(answer);
                        if (aChecks[answer.Id] != 0)
                        {
                            qSums[question.Id] += answer.Score;
                        }
                    }
                }
                model.AnswerChecks = aChecks;
                model.QuestionScore = qSums;
            }
            
            return View(model);
        }
    }
}