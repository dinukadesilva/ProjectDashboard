﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProDashBoard.Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Diagnostics;
using ProDashBoard.Model.Repository;

namespace ProDashBoard.Data
{
    public class CD_CustomerSatisfactionRepository : ICD_CustomerSatisfactionRepository
    {
        private readonly IDbConnection _db;
        //private AuthorizationRepository authRepo;

        public CD_CustomerSatisfactionRepository()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DashBoard1"].ConnectionString);
            //authRepo = new AuthorizationRepository();
        }
        public List<CD_SatisfactionData> Get()
        {
            List<CD_SatisfactionData> resultsArray = this._db.Query("select top 7 Year,Quarter,Rating,Completion FROM CD_CustomerSatisfaction order by Year desc, Quarter desc").Select(d => new CD_SatisfactionData { Year = d.Year, Quarter = d.Quarter, Rating = d.Rating, Completion = d.Completion }).ToList();

            return resultsArray;
        }

        public List<CD_ProjectSatisfactionData> GetProjects(int year, int quarter)
        {
            List<CD_ProjectSatisfactionData> resultsArray = this._db.Query("SELECT  pj.Name, sumr.Rating FROM CustomerSatisfactionSummary sumr join Project pj on sumr.ProjectId = pj.Id where sumr.Year = " + year + " and sumr.Quarter =" + quarter + "; ").Select(d => new CD_ProjectSatisfactionData { Name = d.Name, Rating = d.Rating }).ToList();

            return resultsArray;
        }
    }
}