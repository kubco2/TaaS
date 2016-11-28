using BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TemplateViewEditModel
    {
        public TestTemplateDTO Template { get; set; }
        public List<GroupDTO> Groups { get; set; }
        public List<QuestionCategoryDTO> Categories { get; set; }
        [Required]
        public int[] SelectedCategories { get; set; }

        public TemplateViewEditModel()
        {
            Groups = new List<GroupDTO>();
            Categories = new List<QuestionCategoryDTO>();
        }
    }
}