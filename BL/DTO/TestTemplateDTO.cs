using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class TestTemplateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Duration in minutes")]
        [Range(1, 100)]
        public int Time { get; set; }

        [Required]
        [Display(Name = "Student group")]
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "Opens at")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Closes at")]
        public DateTime DateTo { get; set; }

        [Required]
        [Display(Name = "Number of questions")]
        [Range(1, 100)]
        public int NumQuestions { get; set; }

        [Required]
        public List<QuestionCategoryDTO> QuestionCategories { get; set; }

        [Display(Name = "Attempts(0 - infinite)")]
        public int Attempts { get; set; }
        public int OwnerId { get; set; }
        public TestTemplateDTO()
        {
            QuestionCategories = new List<QuestionCategoryDTO>();
        }

        public bool CanTake()
        {
            return DateFrom <= DateTime.Now && DateTo.AddDays(1) > DateTime.Now;
        }
    }
}
