using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CollegeScorecard.Models
{
    public class CollegeScorecardViewModel
    {
        public List<School> SchoolsList { get; set; }
        public StudentBody StudentBody { get; set; }
        public CostAidEarnings CostAidEarnings { get; set; }
        
    }
}
