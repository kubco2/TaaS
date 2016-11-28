using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class TestTemplate : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Time { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        [Required]
        public int NumQuestions { get; set; }
        public virtual List<QuestionCategory> QuestionCategories { get; set; }
        public virtual List<UserTrial> Trials { get; set; }
        public int Attempts { get; set; }
        public int OwnerId { get; set; }
        public TestTemplate()
        {
            QuestionCategories = new List<QuestionCategory>();
            Trials = new List<UserTrial>();
        }

        public bool IsValidTime()
        {
            var now = DateTime.Now;
            return DateFrom <= now && DateTo > now;
        }
    }
}
