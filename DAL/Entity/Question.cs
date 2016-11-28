using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Question : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool Multiple { get; set; } = false;        
        [Required]
        public virtual QuestionCategory QuestionCategory { get; set; }

        public virtual List<Answer> Answers { get; set; }
        public virtual List<UserTrial> UsedInTrials { get; set; }
        public int OwnerId { get; set; }
        public Question()
        {
            Answers = new List<Answer>();
            UsedInTrials = new List<UserTrial>();
        }
    }
}
