using AutoMapper.QueryableExtensions;
using BL.DTO;
using DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Query
{
    public class AnswerQuery : AppQuery<AnswerDTO>
    {
        public AnswerFilter Filter { get; set; }

        public AnswerQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<AnswerDTO> GetQueryable()
        {
            IQueryable<Answer> query = Context.Answers;

            if (Filter.QuestionId > 0)
            {
                query = Context.Answers.Where(s => s.Question.Id == Filter.QuestionId);
            }

            return query.Project().To<AnswerDTO>();
        }
    }
}
