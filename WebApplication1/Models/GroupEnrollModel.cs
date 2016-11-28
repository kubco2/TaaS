using BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeModel
    {
        [Required]
        [Display(Name = "Authorization code")]
        public string AuthCode { get; set; }
        public List<GroupDTO> Groups { get; set; }

        public HomeModel()
        {
            Groups = new List<GroupDTO>();
        }
    }
}