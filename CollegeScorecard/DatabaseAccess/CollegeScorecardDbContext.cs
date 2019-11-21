using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CollegeScorecard.Models;

namespace CollegeScorecard.DatabaseAccess
{

    public class CollegeScorecardDbContext : DbContext
    {
        public CollegeScorecardDbContext(DbContextOptions<CollegeScorecardDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<StudentBody> StudentBody { get; set; }
        public DbSet<CostAidEarnings> CostAidEarnings { get; set; }

    }    
    
}
