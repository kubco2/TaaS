using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthCode { get; set; }
        
        public List<TestTemplateDTO> Templates { get; set; }
        public int OwnerId { get; set; }
        public GroupDTO()
        {
            Templates = new List<TestTemplateDTO>();
        }
    }
}
