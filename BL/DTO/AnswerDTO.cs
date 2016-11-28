using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = "";
        public string Note { get; set; }
        [Required]
        public double Score { get; set; } = 0;
        [Required]
        public int QuestionId { get; set; }
        public int OwnerId { get; set; }
    }
}
