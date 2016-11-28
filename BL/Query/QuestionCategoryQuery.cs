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
    public class QuestionCategoryQuery : AppQuery<QuestionCategoryDTO>
    {
        public QuestionCategoryFilter Filter { get; set; }

        public QuestionCategoryQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<QuestionCategoryDTO> GetQueryable()
        {
            IQueryable<QuestionCategory> query = Context.QuestionCategories;

            if(Filter.ParentId > 0)
            {
                query = query.Where(s => s.Parent.Id == Filter.ParentId);
            }
            if(Filter.OwnerId > 0)
            {
                query = query.Where(s => s.OwnerId == Filter.OwnerId);
            }
            return query.Project().To<QuestionCategoryDTO>();
        }
    }
}
