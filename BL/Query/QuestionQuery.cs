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
    public class QuestionQuery : AppQuery<QuestionDTO>
    {
        public QuestionFilter Filter { get; set; }

        public QuestionQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<QuestionDTO> GetQueryable()
        {
            IQueryable<Question> query = Context.Questions;

            if(Filter.CategoryId > 0)
            {
                query = query.Where(s => s.QuestionCategory.Id == Filter.CategoryId);
            }
            if(Filter.OwnerId > 0)
            {
                query = query.Where(s => s.OwnerId == Filter.OwnerId);
            }
            return query.Project().To<QuestionDTO>();
        }
    }
}
