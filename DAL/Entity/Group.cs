using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthCode { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<TestTemplate> Templates { get; set; }
        public int OwnerId { get; set; }
        public Group()
        {
            Users = new List<User>();
            Templates = new List<TestTemplate>();
        }
    }
}
