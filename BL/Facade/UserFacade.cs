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
    public class UserFacade : AppFacadeBase
    {
        public UserRepository Repository { get; set; }
        public UserQuery UserQuery { get; set; }

        protected IQuery<UserDTO> CreateQuery(UserFilter filter)
        {
            var query = UserQuery;
            query.Filter = filter;
            return query;
        }

        public UserDTO GetUserById(int userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUser = Repository.GetById(userId);
                return Mapper.Map<UserDTO>(appUser);
            }
        }

        public UserDTO GetUserBySystemId(string userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new UserFilter() { SystemId = userId }).Execute().Single();
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new UserFilter() { }).Execute().ToList();
            }
        }

        public void CreateUser(UserDTO user)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUser = Mapper.Map<User>(user);
                Repository.Insert(appUser);
                uow.Commit();
                if(appUser.Id == 1)
                {
                    DataInitializer.Seed(UserQuery.Context, appUser);
                }
            }
        }

        public void UpdateUser(UserDTO user)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appUser = Repository.GetById(user.Id);
                Mapper.Map(user, appUser);
                
                Repository.Update(appUser);
                uow.Commit();
            }
        }
    }
}
