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
    public class TestTemplateFacade : AppFacadeBase
    {
        public TestTemplateRepository Repository { get; set; }
        public TestTemplateQuery TestTemplateQuery { get; set; }
        public QuestionCategoryFacade categoryFacade { get; set; }
        public GroupFacade groupFacade { get; set; }

        protected IQuery<TestTemplateDTO> CreateQuery(TestTemplateFilter filter)
        {
            var query = TestTemplateQuery;
            query.Filter = filter;
            return query;
        }

        public TestTemplateDTO GetTestTemplateById(int testTemplateId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appTestTemplate = Repository.GetById(testTemplateId);
                return Mapper.Map<TestTemplateDTO>(appTestTemplate);
            }
        }

        public List<TestTemplateDTO> GetAllTestTemplates(int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new TestTemplateFilter() { OwnerId = ownerId }).Execute().ToList();
            }
        }

        public List<TestTemplateDTO> GetTestTemplatesByGroup(int groupId, int ownerId)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                return CreateQuery(new TestTemplateFilter() { GroupId = groupId, OwnerId = ownerId }).Execute().ToList();
            }
        }

        public void CreateTestTemplate(TestTemplateDTO testTemplate, int[] selectedCategories)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appTestTemplate = Mapper.Map<TestTemplate>(testTemplate);
                appTestTemplate.QuestionCategories = categoryFacade.Repository.GetByIds(selectedCategories ?? new int[0]).ToList();
                appTestTemplate.Group = groupFacade.Repository.GetById(testTemplate.GroupId);
                Repository.Insert(appTestTemplate);
                uow.Commit();
            }
        }

        public void UpdateTestTemplate(TestTemplateDTO testTemplate, int[] selectedCategories)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var appTestTemplate = Repository.GetById(testTemplate.Id);
                Mapper.Map(testTemplate, appTestTemplate);
                appTestTemplate.QuestionCategories = categoryFacade.Repository.GetByIds(selectedCategories ?? new int[0]).ToList();
                appTestTemplate.Group = groupFacade.Repository.GetById(testTemplate.GroupId);
                Repository.Update(appTestTemplate);
                uow.Commit();
            }
        }

        public void DeleteTemplate(int id)
        {
            using (var uow = AppUnitOfWorkProvider.Create())
            {
                var template = Repository.GetById(id);
                template.Group.Templates.Remove(template);
                Repository.Delete(template);
                uow.Commit();
            }
        }
    }
}
