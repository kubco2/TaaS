using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Answer : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = "";
        public string Note { get; set; }
        [Required]
        public double Score { get; set; } = 0;
        [Required]
        public virtual Question Question { get; set; }
        public virtual List<UserTrial> UsedInTrials { get; set; }
        public int OwnerId { get; set; }
        public Answer()
        {
            UsedInTrials = new List<UserTrial>();
        }
    }
}
