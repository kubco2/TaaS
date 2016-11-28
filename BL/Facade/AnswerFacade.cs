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
    public class AnswerFacade : AppFacadeBase
    {
        public AnswerRepository Repository { get; set; }
        public QuestionRepository QuestionRepository { get; set; }
        public AnswerQuery AnswerQuery { get; set; }

        protected IQuery<AnswerDTO> CreateQuery(AnswerFilter answerFilter)
        {
            var query = AnswerQuery;
            query.Filter = answerFilter;
            return query;
        }

        public AnswerDTO GetAnswerById(int answerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appAnswer = Repository.GetById(answerId);
                return Mapper.Map<AnswerDTO>(appAnswer);
            }
        }

        public List<AnswerDTO> GetAnswersByQuestion(int questionId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new AnswerFilter { QuestionId = questionId }).Execute().ToList();
            }
        }
        
        public void CreateAnswer(AnswerDTO answer, int questionId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appAnswer = Mapper.Map<Answer>(answer);
                appAnswer.Question = QuestionRepository.GetById(questionId);
                Repository.Insert(appAnswer);
                uow.Commit();
            }
        }

        public void UpdateAnswer(AnswerDTO answer, int questionId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appAnswer = Repository.GetById(answer.Id);
                var question = QuestionRepository.GetById(questionId);
                Mapper.Map(answer, appAnswer);
                appAnswer.Question = question;
                Repository.Update(appAnswer);
                uow.Commit();
            }
        }

        public void DeleteAnswer(int id)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var ans = Repository.GetById(id);
                ans.Question.Answers.Remove(ans);
                Repository.Delete(ans);
                uow.Commit();
            }
        }
    }
}
