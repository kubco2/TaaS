using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QuestionCategoryViewEditModel
    {
        public QuestionCategoryDTO Category { get; set; }
        public List<QuestionCategoryDTO> Categories { get; set; }
    }
}