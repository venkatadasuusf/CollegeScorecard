using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

//This Model is used for View purpose. The data on CollegeScorecard page is displayed from this model.

namespace CollegeScorecard.Models
{
    public class CollegeScorecardViewModel
    {
        public List<School> SchoolsList { get; set; }
        public StudentBody StudentBody { get; set; }
        public CostAidEarnings CostAidEarnings { get; set; }
        
    }
}
