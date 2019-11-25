using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

//This model is a databse entity CostAidEarnings. This gets the data from API and stores in DB

namespace CollegeScorecard.Models
{

    public class CostAidEarningsData
    {
        public Metadata metadata { get; set; }
        public CostAidEarnings[] results { get; set; }

    }

    public class CostAidEarnings
    {
        //Database generated Primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "integer")]
        public int financialsid { get; set; }

        [JsonProperty("latest.cost.tuition.in_state")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? latestcosttuitionin_state { get; set; }

        [JsonProperty("latest.earnings.10_yrs_after_entry.median")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? latestearnings10_yrs_after_entrymedian { get; set; }

        [JsonProperty("latest.cost.tuition.out_of_state")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? latestcosttuitionout_of_state { get; set; }

        [JsonProperty("latest.aid.federal_loan_rate")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? latestaidfederal_loan_rate { get; set; }

        [JsonProperty("latest.cost.avg_net_price.public")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? latestcostavg_net_pricepublic { get; set; }
        
        [JsonProperty("latest.aid.median_debt.completers.overall")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public float? latestaidmedian_debtcompletersoverall { get; set; }

        [JsonProperty("latest.aid.pell_grant_rate")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public float? latestaidpell_grant_rate { get; set; }

        public string datayear { get; set; } = "Latest";
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //ID from Schools table is the Foreign key
        [ForeignKey("School")]
        public int id { get; set; }
        public virtual School School { get; set; }       
    }

}
