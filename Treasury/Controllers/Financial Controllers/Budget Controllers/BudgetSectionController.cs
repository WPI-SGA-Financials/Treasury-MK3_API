﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models.Budget_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Financial_Controllers.Budget_Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BudgetSectionController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public BudgetSectionController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all Budgets by Sections
        /// </summary>
        /// <returns>List of Budget Sections</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/budgetsection")]
        public IEnumerable<BudgetSection> Get()
        {
            return _dbContext.OrgBudgetSections;
        }

        /// <summary>
        /// Get all Budgets by Sections for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Budget Sections</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/budgetsection/{fy}")]
        public IEnumerable<BudgetSection> Get(int fy)
        {
            return _dbContext.OrgBudgetSections.Where(b => b.FiscalYear.Contains("" + fy));
        }
    }
}