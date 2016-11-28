using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TakeTrialModel
    {
        public TestTemplateDTO Template { get; set; }
        public List<UserTrialDTO> GeneratedTrials { get; set; }
        public UserTrialDTO Trial { get; set; }
        public Dictionary<int, double> QuestionScore { get; set; }
        public Dictionary<int, int> AnswerChecks { get; set; }
    }
}