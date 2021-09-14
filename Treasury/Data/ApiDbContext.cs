using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Models;
using Treasury.Models.Financial_Models;
using Treasury.Models.Financial_Models.Budget_Models;
using Treasury.Models.Financial_Models.Funding_Request_Models;

namespace Treasury.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<StudentLifeFee> StudentLifeFees { get; set; }
        public DbSet<Budget> OrgBudgets { get; set; }
        public DbSet<BudgetSection> OrgBudgetSections { get; set; }
        public DbSet<FundingRequest> OrgFundingRequests { get; set; }
        public DbSet<Reallocation> OrgReallocations { get; set; }
    }
}
