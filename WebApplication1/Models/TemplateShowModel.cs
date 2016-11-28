using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TemplateShowModel
    {
        public TestTemplateDTO Template { get; set; }
        public bool CanCreateNew { get; set; }
        public List<UserTrialDTO> Trials { get; set; }

        public TemplateShowModel()
        {
            Trials = new List<UserTrialDTO>();
        }
    }
}