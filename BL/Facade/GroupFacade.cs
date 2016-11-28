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
    public class GroupFacade : AppFacadeBase
    {
        public GroupRepository Repository { get; set; }
        public GroupQuery GroupQuery { get; set; }
        public UserFacade UserFacade { get; set; }

        protected IQuery<GroupDTO> CreateQuery(GroupFilter groupFilter)
        {
            var query = GroupQuery;
            query.Filter = groupFilter;
            return query;
        }

        public GroupDTO GetGroupById(int groupId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appGroup = Repository.GetById(groupId);
                return Mapper.Map<GroupDTO>(appGroup);
            }
        }

        public GroupDTO GetGroupByAuthCode(string authCode)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new GroupFilter { AuthCode = authCode }).Execute().Single();
            }
        }

        public List<GroupDTO> GetGroupsByUserId(int userId)
        {
            
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var user = UserFacade.Repository.GetById(userId);
                return Mapper.Map<List<Group>, List<GroupDTO>>(user.Groups);
            }
        }

        public List<GroupDTO> GetAllGroups(int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new GroupFilter() { UserId = ownerId }).Execute().ToList();
            }
        }

        public bool EnrollToGroup(int userId, string authCode)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var group = GetGroupByAuthCode(authCode);
                if(group != null)
                {
                    var appGroup = Repository.GetById(group.Id);
                    appGroup.Users.Add(UserFacade.Repository.GetById(userId));
                    Repository.Update(appGroup);
                    uow.Commit();
                    return true;
                }
                return false;
            }
        }

        public void CreateGroup(GroupDTO group)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appGroup = Mapper.Map<Group>(group);
                Repository.Insert(appGroup);
                uow.Commit();
            }
        }

        public void UpdateGroup(GroupDTO group)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appGroup = Repository.GetById(group.Id);
                Mapper.Map(group, appGroup);

                Repository.Update(appGroup);
                uow.Commit();
            }
        }

        public void DeleteGroup(int id)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                Repository.Delete(id);
                uow.Commit();
            }
        }

        public bool IsUserInGroup(int groupId, int userId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appGroup = Repository.GetById(groupId);
                var user = UserFacade.Repository.GetById(userId);
                return appGroup.Users.Contains(user);
            }
        }
    }
}
