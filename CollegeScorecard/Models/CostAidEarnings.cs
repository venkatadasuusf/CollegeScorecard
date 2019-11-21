using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CollegeScorecard.Models
{

    public class CostAidEarningsData
    {
        public Metadata metadata { get; set; }
        public CostAidEarnings[] results { get; set; }

    }

    public class CostAidEarnings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "integer")]
        public int financialsid { get; set; }

        [JsonProperty("latest.cost.tuition.in_state")]
        public int latestcosttuitionin_state { get; set; }

        [JsonProperty("latest.earnings.10_yrs_after_entry.median")]
        public int latestearnings10_yrs_after_entrymedian { get; set; }

        [JsonProperty("latest.cost.tuition.out_of_state")]
        public int latestcosttuitionout_of_state { get; set; }

        [JsonProperty("latest.aid.federal_loan_rate")]
        public float latestaidfederal_loan_rate { get; set; }

        [JsonProperty("latest.cost.avg_net_price.public")]
        public int latestcostavg_net_pricepublic { get; set; }

        [JsonProperty("latest.aid.median_debt.completers.overall")]
        public float latestaidmedian_debtcompletersoverall { get; set; }

        [JsonProperty("latest.aid.pell_grant_rate")]
        public float latestaidpell_grant_rate { get; set; }
        public string datayear { get; set; } = "Latest";
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey("School")]
        public int id { get; set; }
        public virtual School School { get; set; }       
    }

}
