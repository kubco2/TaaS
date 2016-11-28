using AutoMapper;
using BL.DTO;
using BL.Query;
using BL.Repository;
using DAL;
using DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facade
{
    public class QuestionFacade : AppFacadeBase
    {
        public QuestionRepository Repository { get; set; }
        public QuestionCategoryFacade CategoryFacade { get; set; }
        public QuestionQuery QuestionQuery { get; set; }
        public AppDbContext Context { get; set; }


        protected IQuery<QuestionDTO> CreateQuery(QuestionFilter filter)
        {
            var query = QuestionQuery;
            query.Filter = filter;
            return query;
        }

        public QuestionDTO GetQuestionById(int questionId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appQuestion = Repository.GetById(questionId);
                return Mapper.Map<QuestionDTO>(appQuestion);
            }
        }

        public List<QuestionDTO> GetAllQuestions(int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new QuestionFilter() { OwnerId = ownerId}).Execute().ToList();
            }
        }

        public List<QuestionDTO> GetQuestionsByCategory(int categoryId, int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new QuestionFilter() { CategoryId = categoryId, OwnerId = ownerId }).Execute().ToList();
            }
        }

        public List<int> GetQuestionIdsByCategories(List<int> catIds)
        {
            using (var context = new AppDbContext())
            {
                return context.Questions.Where(q => catIds.Contains(q.QuestionCategory.Id)).Select(s => s.Id).ToList();
            }
            
        }

        public void CreateQuestion(QuestionDTO question)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var cat = CategoryFacade.Repository.GetById(question.QuestionCategoryId);
                var appQuestion = Mapper.Map<Question>(question);
                appQuestion.QuestionCategory = cat;
                Repository.Insert(appQuestion);
                uow.Commit();
                question.Id = appQuestion.Id;
            }
        }

        public void UpdateQuestion(QuestionDTO question)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var cat = CategoryFacade.Repository.GetById(question.QuestionCategoryId);
                var appQuestion = Repository.GetById(question.Id);
                appQuestion.Text = question.Text;
                appQuestion.Multiple = question.Multiple;
                appQuestion.QuestionCategory = cat;
                Repository.Update(appQuestion);
                uow.Commit();
            }
        }

        public void DeleteQuestion(int id)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var question = Repository.GetById(id);
                var cat = CategoryFacade.Repository.GetById(question.QuestionCategory.Id);
                //cat.Questions.Remove(question);
                Repository.Delete(question);
                uow.Commit();
            }
        }
    }
}
