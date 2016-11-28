using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Multiple choice")]
        public bool Multiple { get; set; } = false;
        [Required]
        [Display(Name = "Category")]
        public int QuestionCategoryId { get; set; }
        public string QuestionCategoryName { get; set; }
        public int SelectedAnswer { get; set; }
        public List<AnswerDTO> Answers { get; set; }
        public int OwnerId { get; set; }
    }
}
