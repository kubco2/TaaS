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
    public class AnswerRepository : EntityFrameworkRepository<Answer, int>
    {
        public AnswerRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
