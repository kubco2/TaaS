using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TemplateViewModel
    {
        public List<TestTemplateDTO> Templates { get; set; }
        public List<GroupDTO> Groups { get; set; }

        public TemplateViewModel()
        {
            Templates = new List<TestTemplateDTO>();
            Groups = new List<GroupDTO>();
        }
    }
}