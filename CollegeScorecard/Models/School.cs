using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

//This model is a databse entity School. This gets the data from API and stores in DB

namespace CollegeScorecard.Models
{
    public class Schools
    {
        public Metadata metadata { get; set; }
        public School[] results { get; set; }   

    }

    public class Metadata
    {
        public int total { get; set; }
        public int page { get; set; }
        public int per_page { get; set; }
    }

    public class School
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("school.name")]
        public string schoolname { get; set; }

        [JsonProperty("school.city")]
        public string schoolcity { get; set; }

        [JsonProperty("school.state")]
        public string schoolstate { get; set; }

        [JsonProperty("school.zip")]
        public string schoolzip { get; set; }

        [JsonProperty("school.accreditor")]
        public string schoolaccreditor { get; set; }

        [JsonProperty("school.ownership")]
        public int? schoolownership { get; set; }

        [JsonProperty("school.school_url")]
        public string schoolschool_url { get; set; }

        [JsonProperty("school.price_calculator_url")]
        public string schoolprice_calculator_url { get; set; }

        [JsonProperty("school.main_campus")]
        public int? schoolmain_campus { get; set; }

        [JsonProperty("school.branches")]
        public int? schoolbranches { get; set; }

        [JsonProperty("school.degrees_awarded.highest")]
        public int? schooldegrees_awardedhighest { get; set; }

        [JsonProperty("school.online_only")]
        public int? schoolonline_only { get; set; }  
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        

    }

    

}
