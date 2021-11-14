using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Groups
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public SubGroups subGroups { get; set; }
    }
}
