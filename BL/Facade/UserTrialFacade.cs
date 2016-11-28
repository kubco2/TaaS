using AutoMapper;
using BL.DTO;
using BL.Query;
using BL.Repository;
using DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facade
{
    public class UserTrialFacade : AppFacadeBase
    {
        public UserTrialRepository Repository { get; set; }
        public UserTrialQuery UserTrialQuery { get; set; }
        public TestTemplateFacade TestTemplateFacade { get; set; }
        public UserFacade UserFacade { get; set; }
        public QuestionCategoryFacade CategoryFacade { get; set; }
        public QuestionFacade QuestionFacade { get; set; }
        public AnswerFacade AnswerFacade { get; set; }

        protected IQuery<UserTrialDTO> CreateQuery(UserTrialFilter filter)
        {
            var query = UserTrialQuery;
            query.Filter = filter;
            return query;
        }

        public UserTrialDTO GetUserTrialById(int userTrialId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUserTrial = Repository.GetById(userTrialId);
                return Mapper.Map<UserTrialDTO>(appUserTrial);
            }
        }

        public List<UserTrialDTO> GetAllUserTrials()
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new UserTrialFilter() { }).Execute().ToList();
            }
        }

        public List<UserTrialDTO> GetTrialsByTemplate(int templateId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new UserTrialFilter() { TemplateId = templateId }).Execute().ToList();
            }
        }

        public List<UserTrialDTO> GetMyTrialsByTemplate(int templateId, int userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new UserTrialFilter() { TemplateId = templateId, UserId = userId }).Execute().ToList();
            }
        }

        public UserTrialDTO CreateUserTrial(int templateId, int userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var tpl = TestTemplateFacade.Repository.GetById(templateId);
                var trial = new UserTrial()
                {
                    Test = tpl,
                    User = UserFacade.Repository.GetById(userId)
                };
                List<int> sumCats = new List<int>();
                List<int> tmpCats = new List<int>();
                tmpCats = tpl.QuestionCategories.Select(s => s.Id).ToList();
                while (tmpCats.Count > 0)
                {
                    var c = tmpCats[0];
                    tmpCats.RemoveAt(0);
                    sumCats.Add(c);
                    foreach (var cat in CategoryFacade.GetQuestionCategoriesByParent(c, tpl.OwnerId))
                    {
                        tmpCats.Add(cat.Id);
                    }
                }
                var qIds = QuestionFacade.GetQuestionIdsByCategories(sumCats);
                int[] shuffled = qIds.OrderBy(n => Guid.NewGuid()).ToArray();
                var selectedQuestions = shuffled.Take(Math.Min(trial.Test.NumQuestions, qIds.Count)).ToArray();
                trial.Questions = QuestionFacade.Repository.GetByIds(selectedQuestions).ToList();

                Repository.Insert(trial);
                uow.Commit();
                return Mapper.Map<UserTrialDTO>(trial);
            }
        }

        public void UpdateUserTrialAnswers(int userTrialId, int[] answers)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUserTrial = Repository.GetById(userTrialId);
                var user = appUserTrial.User;
                var test = appUserTrial.Test;
                appUserTrial.Answers = AnswerFacade.Repository.GetByIds(answers).ToList();
                foreach(var ans in appUserTrial.Answers)
                {
                    appUserTrial.Score += ans.Score;
                }
                Repository.Update(appUserTrial);
                uow.Commit();
            }
        }

        public DateTime takeUserTrial(int userTrialId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUserTrial = Repository.GetById(userTrialId);
                var user = appUserTrial.User;
                var test = appUserTrial.Test;
                appUserTrial.Taken = DateTime.Now;
                Repository.Update(appUserTrial);
                uow.Commit();
                return (DateTime) appUserTrial.Taken;
            }
        }

        public DateTime closeUserTrial(int userTrialId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUserTrial = Repository.GetById(userTrialId);
                var user = appUserTrial.User;
                var test = appUserTrial.Test;
                appUserTrial.Closed = DateTime.Now;
                Repository.Update(appUserTrial);
                uow.Commit();
                return (DateTime) appUserTrial.Closed;
            }
        }

        public bool CanCreateTrial(int templateId, int userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var user = UserFacade.Repository.GetById(userId);
                var tpl = TestTemplateFacade.Repository.GetById(templateId);
                return tpl.IsValidTime() && (tpl.Attempts == 0 || tpl.Trials.Where(s => s.User.Id == userId).Count() < tpl.Attempts) && tpl.Group.Users.Contains(user);
            }
        }
    }
}
