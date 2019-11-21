using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CollegeScorecard.Models;
using CollegeScorecard.APIManager;
using CollegeScorecard.DatabaseAccess;
using System.Net.Http;
using Newtonsoft.Json;

namespace CollegeScorecard.Controllers
{
    public class HomeController : Controller
    {
        public CollegeScorecardDbContext dbContext;

        public HomeController(CollegeScorecardDbContext context)
        {
            dbContext = context;
        }          

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CollegesList()
        {
            APIHandler webHandler = new APIHandler();
            Schools schoolsdata = webHandler.GetSchoolsData();
            
            foreach (School school in schoolsdata.results)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add School only if it doesnt exist, check existence using symbol (PK)
                //if ( (dbContext.Schools.Where(c => c.id.Equals(school.id)).Count() == 0) || (DateTime.Now.Date - Convert.ToDateTime(dbContext.Schools.Where(c => c.id.Equals(school.id))?.Select(c => c.CreatedOn.Date))).Days > 30 )
                if (dbContext.Schools.Where(c => c.id.Equals(school.id)).Count() == 0)
                {
                    dbContext.Schools.Add(school);            
                    
                }
            }
            
            dbContext.SaveChanges();
            
            return View(schoolsdata);
        }

        public IActionResult CollegeScorecard(int? id)
        {
            
            if (id == null)
            {
                ViewBag.collegeselected = 0;
                CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();

                List<School> schoolslist = dbContext.Schools.ToList();

                //check if the count is 0 (no data available in database), then call the API
                if(schoolslist.Count == 0)
                {
                    APIHandler webHandler = new APIHandler();
                    Schools schoolsdata = webHandler.GetSchoolsData();
                    foreach (School school in schoolsdata.results)
                    {
                        schoolslist.Add(school);
                    }
                    collegescorecarddata.SchoolsList = schoolslist;
                }
                else
                {
                    collegescorecarddata.SchoolsList = schoolslist;
                }                
                
                return View(collegescorecarddata);
            }

            else
            {
                ViewBag.collegeselected = id;
                APIHandler webHandler = new APIHandler();
                string studentbodyandcostdata = webHandler.GetStudentBodyandCostData(id);
                
                StudentBodyData studentbodydata = new StudentBodyData();
                CostAidEarningsData costaidearningsdata = new CostAidEarningsData();
                CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();

                List<School> schoolslist = dbContext.Schools.ToList();

                /*check if the count is 0 (no data available in database), then call the API
                 * move to a different function
                if (schoolslist.Count == 0)
                {
                    APIHandler webHandler = new APIHandler();
                    Schools schoolsdata = webHandler.GetSchoolsData();
                    foreach (School school in schoolsdata.results)
                    {
                        schoolslist.Add(school);
                    }
                    collegescorecarddata.SchoolsList = schoolslist;
                }
                else
                {
                    collegescorecarddata.SchoolsList = schoolslist;
                }
                */

                collegescorecarddata.SchoolsList = schoolslist;

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (!studentbodyandcostdata.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    studentbodydata = JsonConvert.DeserializeObject<StudentBodyData>(studentbodyandcostdata, settings);
                    costaidearningsdata = JsonConvert.DeserializeObject<CostAidEarningsData>(studentbodyandcostdata, settings);                  

                }

                foreach (StudentBody studentbody in studentbodydata.results)
                {
                    //Database will give PK constraint violation error when trying to insert record with existing PK.
                    //So add company only if it doesnt exist, check existence using symbol (PK)
                    
                    if (dbContext.StudentBody.Where(c => c.id.Equals(studentbody.id)).Count() == 0)
                    {
                        dbContext.StudentBody.Add(studentbody);                        
                    }
                                       
                    collegescorecarddata.StudentBody = studentbody;
                }

                foreach (CostAidEarnings costaidearnings in costaidearningsdata.results)
                {
                    //Database will give PK constraint violation error when trying to insert record with existing PK.
                    //So add company only if it doesnt exist, check existence using symbol (PK)

                    if (dbContext.CostAidEarnings.Where(c => c.id.Equals(costaidearnings.id)).Count() == 0)
                    {
                        dbContext.CostAidEarnings.Add(costaidearnings);
                    }

                    collegescorecarddata.CostAidEarnings = costaidearnings;
                }

                dbContext.SaveChanges();
                                
                return View(collegescorecarddata);
            }

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
