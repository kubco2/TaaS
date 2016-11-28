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
    public class TestTemplateQuery : AppQuery<TestTemplateDTO>
    {
        public TestTemplateFilter Filter;

        public TestTemplateQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<TestTemplateDTO> GetQueryable()
        {
            IQueryable<TestTemplate> query = Context.TestTemplates;

            if (Filter.GroupId > 0)
            {
                query = query.Where(e => e.Group.Id == Filter.GroupId);
            }
            if (Filter.OwnerId > 0)
            {
                query = query.Where(e => e.OwnerId == Filter.OwnerId);
            }

            return query.Project().To<TestTemplateDTO>();
        }
    }
}
