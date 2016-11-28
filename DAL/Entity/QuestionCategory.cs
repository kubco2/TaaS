using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class QuestionCategory : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual QuestionCategory Parent { get; set; }
        public virtual List<TestTemplate> Templates { get; set; }
        public int OwnerId { get; set; }
        public QuestionCategory()
        {
            Templates = new List<TestTemplate>();
        }
    }
}
