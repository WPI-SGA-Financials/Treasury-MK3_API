﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SLFController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public SLFController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all Student Life Fee Data
        /// </summary>
        /// <returns>List of Student Life Fees</returns>
        [SwaggerOperation(Tags = new [] {"Financial Data"})]
        [HttpGet]
        public IEnumerable<StudentLifeFee> Get()
        {
            return _dbContext.StudentLifeFees;
        }

        /// <summary>
        /// Gets a particular fiscal year by database ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student Life Fee data for the year</returns>
        [SwaggerOperation(Tags = new [] {"Financial Data"})]
        [HttpGet("{id}")]
        public StudentLifeFee Get(int id)
        {
            return _dbContext.StudentLifeFees.Find(id);
        }
    }
}