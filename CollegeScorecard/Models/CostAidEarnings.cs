using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeScorecard.Models
{
    public class CostAidEarnings
    {
        [Key]
        public int financialsid { get; set; }        
        public int latestcosttuitionin_state { get; set; }        
        public int latestearnings10_yrs_after_entrymedian { get; set; }        
        public int latestcosttuitionout_of_state { get; set; }
        public float latestaidfederal_loan_rate { get; set; }        
        public int latestcostavg_net_pricepublic { get; set; }        
        public float latestaidmedian_debtcompletersoverall { get; set; }        
        public float latestaidpell_grant_rate { get; set; }
        public string datayear { get; set; } = "Latest";
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey("School")]
        public int id { get; set; }
        public virtual School School { get; set; }       
    }

}
