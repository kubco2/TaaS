using DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository
{
    public class TestTemplateRepository : EntityFrameworkRepository<TestTemplate, int>
    {
        public TestTemplateRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
