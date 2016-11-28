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
    public class QuestionCategoryFacade : AppFacadeBase
    {
        public QuestionCategoryRepository Repository { get; set; }
        public QuestionRepository QuestionRepository { get; set; }
        public QuestionCategoryQuery QuestionCategoryQuery { get; set; }

        protected IQuery<QuestionCategoryDTO> CreateQuery(QuestionCategoryFilter filter)
        {
            var query = QuestionCategoryQuery;
            query.Filter = filter;
            return query;
        }

        public QuestionCategoryDTO GetQuestionCategoryById(int questionCategoryId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appQuestionCategory = Repository.GetById(questionCategoryId);
                return Mapper.Map<QuestionCategoryDTO>(appQuestionCategory);
            }
        }

        public List<QuestionCategoryDTO> GetAllQuestionCategorys(int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new QuestionCategoryFilter() { OwnerId = ownerId}).Execute().ToList();
            }
        }

        public List<QuestionCategoryDTO> GetQuestionCategoriesByParent(int parentId, int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new QuestionCategoryFilter() { ParentId = parentId, OwnerId = ownerId }).Execute().ToList();
            }
        }

        public void CreateQuestionCategory(QuestionCategoryDTO category)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appQuestionCategory = Mapper.Map<QuestionCategory>(category);
                if(category.ParentId > 0)
                {
                    appQuestionCategory.Parent = Repository.GetById((int)category.ParentId);
                }
                Repository.Insert(appQuestionCategory);
                uow.Commit();
            }
        }

        public void UpdateQuestionCategory(QuestionCategoryDTO category)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appQuestionCategory = Repository.GetById(category.Id);
                Mapper.Map(category, appQuestionCategory);
                if (category.ParentId > 0)
                {
                    appQuestionCategory.Parent = Repository.GetById((int)category.ParentId);
                }
                Repository.Update(appQuestionCategory);
                uow.Commit();
            }
        }

        public void DeleteQuestionCategory(int id)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var cat = Repository.GetById(id);
                var subcats = GetQuestionCategoriesByParent(id, cat.OwnerId);

                subcats.ForEach(c => c.ParentId = null);
                subcats.ForEach(c => UpdateQuestionCategory(c));
                Repository.Delete(cat);
                uow.Commit();
            }
        }
    }
}
