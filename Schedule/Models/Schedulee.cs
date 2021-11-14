using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Schedulee
    {
        [Key]
        public long Id { get; set; }
        public string Star { get; set; }
        public Groups Group { get; set; }
        public Lecture Lecture { get; set; }
        public Time Time { get; set; }
        public DateTime Day { get; set; }
        public Teachers Teachers { get; set; }

        public EducationalInstitutions EducationalInstitutions {get;set;}
        //Дистанционно/Очно
        public string TrainingFormat { get; set; }
        public int Room { get; set; }
        public string Korpus { get; set; }
        public string Faculty { get; set; }
        public Levels Level { get; set; }
        public Specialization Specialization { get; set; }
        public string FormOfTraining { get; set; }
        public int Course { get; set; }

    }
}
