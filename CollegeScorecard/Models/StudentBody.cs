using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

//This model is a databse entity StudentBody. This gets the data from API and stores in DB

namespace CollegeScorecard.Models
{
    public class StudentBodyData
    {
        public Metadata metadata { get; set; }
        public StudentBody[] results { get; set; }

    }
  
    public class StudentBody
    {
        //Database generated Primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "integer")]
        public int studentbodyid { get; set; }

        [JsonProperty("latest.completion.completion_rate_4yr_150nt")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? latestcompletioncompletion_rate_4yr_150nt { get; set; }

        [JsonProperty("latest.student.demographics.female_share")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? lateststudentdemographicsfemale_share { get; set; }

        [JsonProperty("latest.student.retention_rate.four_year.full_time")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? lateststudentretention_ratefour_yearfull_time { get; set; }
        
        [JsonProperty("latest.student.size")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int? lateststudentsize { get; set; }

        [JsonProperty("latest.student.grad_students")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int? lateststudentgrad_students { get; set; }

        [JsonProperty("latest.admissions.admission_rate.overall")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? latestadmissionsadmission_rateoverall { get; set; }

        [JsonProperty("latest.student.part_time_share")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? lateststudentpart_time_share { get; set; }
        
        public string datayear { get; set; } = "Latest";

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //ID from Schools table is the Foreign key
        [ForeignKey("School")]
        public int id { get; set; }
        public virtual School School { get; set; }

    }

}
