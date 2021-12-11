using Microsoft.EntityFrameworkCore;
using Treasury.Domain.Models;
using Treasury.Domain.Models.Tables;
using Treasury.Domain.Models.Views;

#nullable disable

namespace Treasury.Application.Contexts
{
    public partial class sgadbContext : DbContext
    {
        public sgadbContext(DbContextOptions<sgadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllOperatingExpense> AllOperatingExpenses { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<BudgetByFy> BudgetByFys { get; set; }
        public virtual DbSet<BudgetBySection> BudgetBySections { get; set; }
        public virtual DbSet<BudgetLegacy> BudgetLegacies { get; set; }
        public virtual DbSet<BudgetLineItem> BudgetLineItems { get; set; }
        public virtual DbSet<BudgetLineItemsReadable> BudgetLineItemsReadables { get; set; }
        public virtual DbSet<BudgetSection> BudgetSections { get; set; }
        public virtual DbSet<CategoriesClubMembership> CategoriesClubMemberships { get; set; }
        public virtual DbSet<CategoriesTotalBudget> CategoriesTotalBudgets { get; set; }
        public virtual DbSet<ClubClassification> ClubClassifications { get; set; }
        public virtual DbSet<CompleteFundingRequest> CompleteFundingRequests { get; set; }
        public virtual DbSet<Frappeal> Frappeals { get; set; }
        public virtual DbSet<Frminute> Frminutes { get; set; }
        public virtual DbSet<FrreportForm> FrreportForms { get; set; }
        public virtual DbSet<Frsupplemental> Frsupplementals { get; set; }
        public virtual DbSet<FrworkdayIdt> FrworkdayIdts { get; set; }
        public virtual DbSet<FundingAccount> FundingAccounts { get; set; }
        public virtual DbSet<FundingRequest> FundingRequests { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }
        public virtual DbSet<MandatoryTransfer> MandatoryTransfers { get; set; }
        public virtual DbSet<MandatoryTransfersTotalBudget> MandatoryTransfersTotalBudgets { get; set; }
        public virtual DbSet<MtlineItem> MtlineItems { get; set; }
        public virtual DbSet<OperatingExpense> OperatingExpenses { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationComment> OrganizationComments { get; set; }
        public virtual DbSet<OrganizationMembershipNumber> OrganizationMembershipNumbers { get; set; }
        public virtual DbSet<OrganizationContactInfo> OrganizationsContactInfos { get; set; }
        public virtual DbSet<ReallocMinute> ReallocMinutes { get; set; }
        public virtual DbSet<Reallocation> Reallocations { get; set; }
        public virtual DbSet<ReclassMinute> ReclassMinutes { get; set; }
        public virtual DbSet<Reclassification> Reclassifications { get; set; }
        public virtual DbSet<Soc> Socs { get; set; }
        public virtual DbSet<StudentLifeFee> StudentLifeFees { get; set; }
        public virtual DbSet<Techsync> TechsyncNames { get; set; }
        public virtual DbSet<TotalBudget> TotalBudgets { get; set; }
        public virtual DbSet<TotalLifeFeeBudget> TotalLifeFeeBudgets { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<AllOperatingExpense>(entity =>
            {
                entity.ToView("All Operating Expenses");

                entity.Property(e => e.Description)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.LineItem)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Notes)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.PaymentType)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Person)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Spent).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Notes).HasDefaultValueSql("''");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.NameOfClub)
                    .HasConstraintName("Budget_ibfk_1");
            });

            modelBuilder.Entity<BudgetByFy>(entity =>
            {
                entity.ToView("Budget By FY");

                entity.Property(e => e.AmountApproved).HasPrecision(55, 2);

                entity.Property(e => e.AmountProposed).HasPrecision(54, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(54, 2);

                entity.Property(e => e.AmountSpent).HasPrecision(54, 2);

                entity.Property(e => e.Appealed).HasPrecision(1);

                entity.Property(e => e.ApprovedAppeal).HasPrecision(54, 2);

                entity.Property(e => e.FiscalYear)
                    .HasDefaultValueSql("''")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .HasDefaultValueSql("''")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NumOfItems).HasPrecision(42);

                entity.Property(e => e.RequestedAppeal).HasPrecision(54, 2);
            });

            modelBuilder.Entity<BudgetBySection>(entity =>
            {
                entity.ToView("Budget By Section");

                entity.Property(e => e.AmountApproved).HasPrecision(33, 2);

                entity.Property(e => e.AmountProposed).HasPrecision(32, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(32, 2);

                entity.Property(e => e.AmountSpent).HasPrecision(32, 2);

                entity.Property(e => e.ApprovedAppeal).HasPrecision(32, 2);

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.RequestedAppeal).HasPrecision(32, 2);

                entity.Property(e => e.SectionName)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<BudgetLegacy>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AmountProposed).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(10, 2);

                entity.Property(e => e.AmountSpent).HasPrecision(10, 2);

                entity.Property(e => e.AppealAmount).HasPrecision(10, 2);

                entity.Property(e => e.AppealDecision).HasDefaultValueSql("''");

                entity.Property(e => e.Appealed).HasDefaultValueSql("b'0'");

                entity.Property(e => e.ApprovedAppeal).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.BudgetLegacies)
                    .HasForeignKey(d => d.BId)
                    .HasConstraintName("BudgetLegacy_Budget_ID_fk");
            });

            modelBuilder.Entity<BudgetLineItem>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AmountProposed).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequest).HasPrecision(10, 2);

                entity.Property(e => e.AmountSpent).HasPrecision(10, 2);

                entity.Property(e => e.AppealAmount).HasPrecision(10, 2);

                entity.Property(e => e.AppealDecision).HasDefaultValueSql("''");

                entity.Property(e => e.Appealed).HasDefaultValueSql("b'0'");

                entity.Property(e => e.ApprovedAppeal).HasPrecision(10, 2);

                entity.Property(e => e.Notes).HasDefaultValueSql("''");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Bs)
                    .WithMany(p => p.BudgetLineItems)
                    .HasForeignKey(d => d.BsId)
                    .HasConstraintName("BudgetLineItem_BudgetSection_ID_fk");
            });

            modelBuilder.Entity<BudgetLineItemsReadable>(entity =>
            {
                entity.ToView("Budget Line Items Readable");

                entity.Property(e => e.AmountProposed).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(10, 2);

                entity.Property(e => e.AmountSpent).HasPrecision(10, 2);

                entity.Property(e => e.ApprovedAppeal).HasPrecision(10, 2);

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.LineItemName)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.SectionName)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<BudgetSection>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.BudgetSections)
                    .HasForeignKey(d => d.BId)
                    .HasConstraintName("BudgetSection_Budget_ID_fk");
            });

            modelBuilder.Entity<CategoriesClubMembership>(entity =>
            {
                entity.ToView("Categories Club Membership");

                entity.Property(e => e.ActiveMembers)
                    .HasDefaultValueSql("'Not Provided'")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Category)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<CategoriesTotalBudget>(entity =>
            {
                entity.ToView("Categories Total Budget");

                entity.HasComment("View 'sgadb.Categories Total Budget' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them");
            });

            modelBuilder.Entity<ClubClassification>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.ClubCategory)
                    .HasForeignKey<ClubClassification>(d => d.Name)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Club Classifications_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<CompleteFundingRequest>(entity =>
            {
                entity.ToView("Complete Funding Request");

                entity.Property(e => e.AgendaNumber)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.AppealAmount).HasPrecision(10, 2);

                entity.Property(e => e.AppealDecision)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.AppealMinutes)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Appealed).HasDefaultValueSql("''");

                entity.Property(e => e.Approved).HasPrecision(10, 2);

                entity.Property(e => e.ApprovedAppeal).HasPrecision(10, 2);

                entity.Property(e => e.Decision)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Description)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.DotNumber)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.IdtSubmitted).HasDefaultValueSql("b'0'");

                entity.Property(e => e.MinutesLink)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Notes)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.ReportFormApprovedAmount).HasPrecision(10, 2);

                entity.Property(e => e.ReportFormStatus)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Requested).HasPrecision(10, 2);

                entity.Property(e => e.SpentAmount).HasPrecision(10, 2);

                entity.Property(e => e.WorkdayApproved)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<Frappeal>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AppealAmount).HasPrecision(10, 2);

                entity.Property(e => e.ApprovedAppeal).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.Frappeal)
                    .HasForeignKey<Frappeal>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRAppeals_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<Frminute>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.Frminute)
                    .HasForeignKey<Frminute>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Minutes_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<FrreportForm>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.ApprovedAmount).HasPrecision(10, 2);

                entity.Property(e => e.SpentAmount).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.FrreportForm)
                    .HasForeignKey<FrreportForm>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRReportForms_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<Frsupplemental>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AmendedAmount).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithMany(p => p.Frsupplementals)
                    .HasForeignKey(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRSupplemental_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<FrworkdayIdt>(entity =>
            {
                entity.HasComment("Workday and IDT Info for Funding Requests")
                    .HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.IdtSubmitted).HasDefaultValueSql("b'0'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.FrworkdayIdt)
                    .HasForeignKey<FrworkdayIdt>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRWorkdayIDT_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<FundingAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccountName, e.FiscalYear })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.FallTransfer).HasPrecision(10, 2);

                entity.Property(e => e.SpringTransfer).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<FundingRequest>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AmountApproved).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.FundingRequests)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Funding Requests_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<Liability>(entity =>
            {
                entity.ToView("Liability");

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.RfApprovedAmt).HasPrecision(32, 2);

                entity.Property(e => e.TotalApproved).HasPrecision(32, 2);

                entity.Property(e => e.TotalLiability).HasPrecision(32, 2);

                entity.Property(e => e.TotalWorkdayLiability).HasPrecision(32, 2);

                entity.Property(e => e.WorkdayApprovedRequests).HasPrecision(23);
            });

            modelBuilder.Entity<MandatoryTransfer>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AmountApproved).HasPrecision(10, 2);

                entity.Property(e => e.AmountProposed).HasPrecision(10, 2);

                entity.Property(e => e.AmountRequested).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.ParentOrganizationNavigation)
                    .WithMany(p => p.MandatoryTransfers)
                    .HasForeignKey(d => d.ParentOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mandatory Transfers_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<MandatoryTransfersTotalBudget>(entity =>
            {
                entity.ToView("Mandatory Transfers Total Budget");

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FundName)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.ParentOrganization)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.TotalBudget).HasPrecision(10, 2);
            });

            modelBuilder.Entity<MtlineItem>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Amount).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Mt)
                    .WithMany(p => p.MtlineItems)
                    .HasForeignKey(d => d.MtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MT_LineItems_Mandatory Transfers_ID_fk");
            });

            modelBuilder.Entity<OperatingExpense>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Spent).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Mtli)
                    .WithMany(p => p.OperatingExpenses)
                    .HasForeignKey(d => d.MtliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MT_Expenses_MT_LineItems_ID_fk");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.NameOfClub)
                    .HasName("PRIMARY");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<OrganizationComment>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.OrganizationComments)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgCommentName");
            });

            modelBuilder.Entity<OrganizationMembershipNumber>(entity =>
            {
                entity.HasKey(e => new { e.NameOfOrganization, e.FiscalYear })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.ActiveMembers).HasDefaultValueSql("'Not Provided'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfOrganizationNavigation)
                    .WithMany(p => p.OrganizationMembershipNumbers)
                    .HasForeignKey(d => d.NameOfOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgMemName");
            });

            modelBuilder.Entity<OrganizationContactInfo>(entity =>
            {
                entity.HasKey(e => e.NameOfClub)
                    .HasName("PRIMARY");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.ContactInfo)
                    .HasForeignKey<OrganizationContactInfo>(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Organizations Contact Info_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<ReallocMinute>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Realloc)
                    .WithMany(p => p.ReallocMinutes)
                    .HasForeignKey(d => d.ReallocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReallocMinutes_Reallocations_ID_fk");
            });

            modelBuilder.Entity<Reallocation>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AllocationAmount).HasPrecision(10, 2);

                entity.Property(e => e.AmountApproved).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.Reallocations)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgRealName");
            });

            modelBuilder.Entity<ReclassMinute>(entity =>
            {
                entity.HasComment("Reclassification Minutes")
                    .HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Reclass)
                    .WithMany(p => p.ReclassMinutes)
                    .HasForeignKey(d => d.ReclassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReclassMinutes_Reclassifications_ID_fk");
            });

            modelBuilder.Entity<Reclassification>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.Reclassifications)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgReclName");
            });

            modelBuilder.Entity<Soc>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<StudentLifeFee>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.SlfAmount).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Techsync>(entity =>
            {
                entity.HasKey(e => e.NameOfClub)
                    .HasName("PRIMARY");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.TechsyncInfo)
                    .HasForeignKey<Techsync>(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgTechName");
            });

            modelBuilder.Entity<TotalBudget>(entity =>
            {
                entity.ToView("Total Budget");

                entity.HasComment("View 'sgadb.Total Budget' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them");
            });

            modelBuilder.Entity<TotalLifeFeeBudget>(entity =>
            {
                entity.ToView("Total Life Fee Budget");

                entity.Property(e => e.FiscalYear)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Total).HasPrecision(20, 2);
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => new { e.FiscalYear, e.DateOfTransfer })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Amount).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
