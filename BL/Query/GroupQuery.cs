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
    public class GroupQuery : AppQuery<GroupDTO>
    {
        public GroupFilter Filter { get; set; }

        public GroupQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<GroupDTO> GetQueryable()
        {
            IQueryable<Group> query = Context.Groups;

            if(!string.IsNullOrEmpty(Filter.AuthCode))
            {
                query = query.Where(s => s.AuthCode == Filter.AuthCode);
            }
            if(Filter.UserId > 0)
            {
                query = query.Where(s => s.OwnerId == Filter.UserId);
            }
            return query.Project().To<GroupDTO>();
        }
    }
}
