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
            /* Moved to new function GetSchoolsData
            APIHandler webHandler = new APIHandler();
            Schools schoolsdata = webHandler.GetSchoolsDataAPI();
            
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
            */
            Schools schoolsdata = GetSchoolsDatafromAPI();
            return View(schoolsdata);
        }

        public IActionResult CollegeScorecard(int? id)
        {
            CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();

            // Start: User accessing the CollegeScorecard link directly, not via Collesgeslist page
            if (id == null)
            {
                ViewBag.collegeselected = 0;
                //CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();
                List<School> schoolslist = dbContext.Schools.ToList();

                //check if the count is 0 (no data available in database), then call the API
                if(schoolslist.Count != 0)
                {                    
                    Schools schoolsdata = GetSchoolsDatafromAPI();            

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
            // End: User accessing the CollegeScorecard link directly, not via Collesgeslist page

            // Start: User accessing the CollegeScorecard from Collesgeslist page
            else
            {
                ViewBag.collegeselected = id;

                collegescorecarddata = GetStudentBodyandCostData(id);

                /*
                StudentBodyData studentbodydata = new StudentBodyData();
                StudentBody studentbodyfromDB = new StudentBody();
                CostAidEarningsData costaidearningsdata = new CostAidEarningsData();
                CostAidEarnings costaidearningsfromDB = new CostAidEarnings();
                //CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();

                //Retrieving the list of colleges from Database
                List<School> schoolslist = dbContext.Schools.ToList();
                //Retrieving the studentbody data from Database for the selected college
                studentbodyfromDB = dbContext.StudentBody.Where(s => s.id == id).FirstOrDefault();
                //Retrieving the studentbody data from Database for the selected college
                costaidearningsfromDB = dbContext.CostAidEarnings.Where(s => s.id == id).FirstOrDefault();                       
                  
                //check if the count is 0 (no data available in database), then call the API                 
                if (schoolslist.Count != 0)
                {
                    Schools schoolsdata = GetSchoolsData();

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

                // Checking the last saved data time for studentbody and costairearnings records
                int studentbodylastSaveddataTime = (DateTime.Now.Date - studentbodyfromDB.CreatedOn.Date).Days;
                int costaidearningslastSaveddataTime = (DateTime.Now.Date - costaidearningsfromDB.CreatedOn.Date).Days;

                //check if the studentbodyfromDB or costaidearningsfromDB is null (no data available in database), then call the API
                //move to a different function

                APIHandler webHandler = new APIHandler();
                string studentbodyandcostdata = webHandler.GetStudentBodyandCostDataAPI(id);

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
                    
                    //studentbodyfromDB.CreatedOn = DateTime.Now;
                    //studentbodyfromDB.latestadmissionsadmission_rateoverall = studentbody.latestadmissionsadmission_rateoverall;
                    //dbContext.Entry(studentbodyfromDB).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        
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
             */                   
                return View(collegescorecarddata);
            }
            // End: User accessing the CollegeScorecard from Collesgeslist page

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

        /****
         * Returns the Collges List from API.
         ****/
        public Schools GetSchoolsDatafromAPI()
        {
            APIHandler webHandler = new APIHandler();
            Schools schoolsdata = webHandler.GetSchoolsDataAPI();

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

            return schoolsdata;
        }

        public CollegeScorecardViewModel GetStudentBodyandCostData(int? id)
        {
            StudentBodyData studentbodydata = new StudentBodyData();
            StudentBody studentbodyfromDB = new StudentBody();
            CostAidEarningsData costaidearningsdata = new CostAidEarningsData();
            CostAidEarnings costaidearningsfromDB = new CostAidEarnings();
            CollegeScorecardViewModel collegescorecarddata = new CollegeScorecardViewModel();
            string studentbodyandcostdata = "";

            //Retrieving the list of colleges from Database
            List<School> schoolslist = dbContext.Schools.ToList();
            //Retrieving the studentbody data from Database for the selected college
            studentbodyfromDB = dbContext.StudentBody.Where(s => s.id == id).FirstOrDefault();
            //Retrieving the studentbody data from Database for the selected college
            costaidearningsfromDB = dbContext.CostAidEarnings.Where(s => s.id == id).FirstOrDefault();

            //check if the count is 0 (no data available in database), then call the API                 
            if (schoolslist.Count == 0)
            {
                Schools schoolsdata = GetSchoolsDatafromAPI();
                foreach (School school in schoolsdata.results)
                {
                    schoolslist.Add(school);
                }                
            }
            
            //collegescorecarddata.SchoolsList = schoolslist;
            //collegescorecarddata.StudentBody = studentbodyfromDB;
            //collegescorecarddata.CostAidEarnings = costaidearningsfromDB;
            //return collegescorecarddata;

            //check if the studentbodyfromDB or costaidearningsfromDB is null (no data available in database), then call the API

            if (studentbodyfromDB == null || costaidearningsfromDB == null)
            {
                APIHandler webHandler = new APIHandler();
                studentbodyandcostdata = webHandler.GetStudentBodyandCostDataAPI(id);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
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
                collegescorecarddata.SchoolsList = schoolslist;
            }

            else if (studentbodyfromDB != null && costaidearningsfromDB != null)
            {
                // Checking the last saved data time for studentbody and costaidrearnings records
                int studentbodylastSaveddataTime = (DateTime.Now.Date - studentbodyfromDB.CreatedOn.Date).Days;
                int costaidearningslastSaveddataTime = (DateTime.Now.Date - costaidearningsfromDB.CreatedOn.Date).Days;
                if (studentbodylastSaveddataTime > 30 || costaidearningslastSaveddataTime > 30)
                {
                    APIHandler webHandler = new APIHandler();
                    studentbodyandcostdata = webHandler.GetStudentBodyandCostDataAPI(id);
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Include,
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
                        else
                        {
                            studentbodyfromDB.CreatedOn = DateTime.Now;
                            studentbodyfromDB.latestadmissionsadmission_rateoverall = studentbody.latestadmissionsadmission_rateoverall;
                            studentbodyfromDB.latestcompletioncompletion_rate_4yr_150nt = studentbody.latestcompletioncompletion_rate_4yr_150nt;
                            studentbodyfromDB.lateststudentdemographicsfemale_share = studentbody.lateststudentdemographicsfemale_share;
                            studentbodyfromDB.lateststudentgrad_students = studentbody.lateststudentgrad_students;
                            studentbodyfromDB.lateststudentpart_time_share = studentbody.lateststudentpart_time_share;
                            studentbodyfromDB.lateststudentretention_ratefour_yearfull_time = studentbody.lateststudentretention_ratefour_yearfull_time;
                            studentbodyfromDB.lateststudentsize = studentbody.lateststudentsize;
                            dbContext.Entry(studentbodyfromDB).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                        else
                        {
                            costaidearningsfromDB.CreatedOn = DateTime.Now;
                            costaidearningsfromDB.latestaidfederal_loan_rate = costaidearnings.latestaidfederal_loan_rate;
                            costaidearningsfromDB.latestaidmedian_debtcompletersoverall = costaidearnings.latestaidmedian_debtcompletersoverall;
                            costaidearningsfromDB.latestaidpell_grant_rate = costaidearnings.latestaidpell_grant_rate;
                            costaidearningsfromDB.latestcostavg_net_pricepublic = costaidearnings.latestcostavg_net_pricepublic;
                            costaidearningsfromDB.latestcosttuitionin_state = costaidearnings.latestcosttuitionin_state;
                            costaidearningsfromDB.latestcosttuitionout_of_state = costaidearnings.latestcosttuitionout_of_state;
                            costaidearningsfromDB.latestearnings10_yrs_after_entrymedian = costaidearnings.latestearnings10_yrs_after_entrymedian;                            
                        }
                        collegescorecarddata.CostAidEarnings = costaidearnings;
                    }

                    dbContext.SaveChanges();
                    collegescorecarddata.SchoolsList = schoolslist;                    
                }

                else
                {
                    collegescorecarddata.SchoolsList = schoolslist;
                    collegescorecarddata.StudentBody = studentbodyfromDB;
                    collegescorecarddata.CostAidEarnings = costaidearningsfromDB;
                }                                
            }

            /*
            APIHandler webHandler = new APIHandler();
            studentbodyandcostdata = webHandler.GetStudentBodyandCostDataAPI(id);
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

                //studentbodyfromDB.CreatedOn = DateTime.Now;
                //studentbodyfromDB.latestadmissionsadmission_rateoverall = studentbody.latestadmissionsadmission_rateoverall;
                //dbContext.Entry(studentbodyfromDB).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
            */
            return collegescorecarddata;
        }        

    }
}