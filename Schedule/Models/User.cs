using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class User : IdentityUser
    {
        public string FIO { get; set; }
        public string EducationalInstitutions { get; set; }
        public string Faculty{ get; set; }
        //public string Faculty { get; set; }
    }
}
