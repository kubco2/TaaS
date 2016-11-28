using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QuestionViewEditModel
    {
        public QuestionDTO Question { get; set; }
        public List<AnswerDTO> Answers { get; set; }
        public List<QuestionCategoryDTO> Categories { get; set; }
    }
}