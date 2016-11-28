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
    public class UserTrialQuery : AppQuery<UserTrialDTO>
    {
        public UserTrialFilter Filter;

        public UserTrialQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<UserTrialDTO> GetQueryable()
        {
            IQueryable<UserTrial> query = Context.UserTrials;

            if(Filter.TemplateId > 0)
            {
                query = query.Where(e => e.Test.Id == Filter.TemplateId);
            }
            if(Filter.UserId > 0)
            {
                query = query.Where(e => e.User.Id == Filter.UserId);
            }

            return query.Project().To<UserTrialDTO>();
        }
    }
}
