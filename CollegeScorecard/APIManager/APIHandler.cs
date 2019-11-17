﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using CollegeScorecard.Models;

namespace CollegeScorecard.APIManager
{
    public class APIHandler
    {
        static string BASE_URL = "https://api.data.gov/ed/collegescorecard/v1/schools.json?";
        static string API_KEY = "DdRtYjTmssGhBJUm2xAoIjGpgxumbWuQXKEe2498"; //Add your API key here inside ""

        HttpClient httpClient;

        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>
        public APIHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Method to receive data from API end point as a collection of objects
        /// 
        /// JsonConvert parses the JSON string into classes
        /// </summary>
        /// <returns></returns>
        public Schools GetSchoolsData()
        {
            string COLLEGE_SCORECARD_API_PATH = BASE_URL + "&school.state=FL&school.ownership=1&school.degrees_awarded.predominant=3&_fields=id,school.name,school.city,school.state,school.zip,school.accreditor,school.school_url,school.price_calculator_url,school.main_campus,school.branches,school.degrees_awarded.highest,school.ownership,school.online_only,latest.student.size,latest.student.grad_students,latest.student.part_time_share,latest.student.demographics.female_share,latest.student.retention_rate.four_year.full_time,latest.admissions.admission_rate.overall,latest.completion.completion_rate_4yr_150nt,latest.earnings.10_yrs_after_entry.median,latest.cost.avg_net_price.public,latest.cost.tuition.in_state,latest.cost.tuition.out_of_state,latest.aid.pell_grant_rate,latest.aid.federal_loan_rate,latest.aid.median_debt.completers.overall&_per_page=100";
            string SchoolsData = "";

            Schools schools = null;

            httpClient.BaseAddress = new Uri(COLLEGE_SCORECARD_API_PATH);

            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(COLLEGE_SCORECARD_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    SchoolsData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!SchoolsData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    schools = JsonConvert.DeserializeObject<Schools>(SchoolsData);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return schools;
        }
    }
}