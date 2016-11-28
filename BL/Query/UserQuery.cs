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
    public class UserQuery : AppQuery<UserDTO>
    {
        public UserFilter Filter;

        public UserQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<UserDTO> GetQueryable()
        {
            IQueryable<User> query = Context.Users;

            if(!string.IsNullOrEmpty(Filter.SystemId))
            {
                query = query.Where(s => s.UserId == Filter.SystemId);
            }

            return query.Project().To<UserDTO>();
        }

        
    }
}
