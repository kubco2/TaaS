using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QuestionViewModel
    {
        public List<QuestionDTO> Questions { get; set; }
        public List<QuestionCategoryDTO> Categories { get; set; }
        public QuestionViewModel()
        {
            Questions = new List<QuestionDTO>();
            Categories = new List<QuestionCategoryDTO>();
        }
    }
}