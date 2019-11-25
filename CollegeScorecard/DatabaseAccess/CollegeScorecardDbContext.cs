using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CollegeScorecard.Models;

//This class has the Database context and the 3 DB tables that will be created

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
