using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class QuestionCategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Parent category")]
        public int? ParentId { get; set; }
        [Display(Name = "Parent category name")]
        public string ParentName { get; set; }
        public int OwnerId { get; set; }
    }
}
