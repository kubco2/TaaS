using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class UserTrial : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public virtual TestTemplate Test { get; set; }
        public virtual User User { get; set; }
        [Required]
        public virtual List<Question> Questions { get; set; }

        public virtual List<Answer> Answers { get; set; }
        [Required]
        public DateTime Generated { get; set; }
        public DateTime? Taken { get; set; }
        public DateTime? Closed { get; set; }
        public double Score { get; set; }

        public UserTrial()
        {
            Generated = DateTime.Now;
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }
    }
}
