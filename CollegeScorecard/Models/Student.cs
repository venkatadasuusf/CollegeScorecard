using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CollegeScorecard.Models
{
    public class Student
    {
        [Key]
        public int studentdataid { get; set; }
        public float latestcompletioncompletion_rate_4yr_150nt { get; set; }   
        public float lateststudentdemographicsfemale_share { get; set; }        
        public float lateststudentretention_ratefour_yearfull_time { get; set; }

        [JsonProperty("latest.student.size")]
        public int lateststudentsize { get; set; }
        public int lateststudentgrad_students { get; set; }        
        public float latestadmissionsadmission_rateoverall { get; set; }
        public float lateststudentpart_time_share { get; set; }
        public string datayear { get; set; } = "Latest";
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey("School")]
        public int id { get; set; }
        public virtual School School { get; set; }

    }

}
