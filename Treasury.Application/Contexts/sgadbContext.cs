using Microsoft.EntityFrameworkCore;
using Treasury.Domain.Models.Tables;
using Treasury.Domain.Models.Views;

#nullable disable

namespace Treasury.Application.Contexts
{
    public partial class sgadbContext : DbContext
    {
        public sgadbContext()
        {
        }

        public sgadbContext(DbContextOptions<sgadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllOperatingExpense> AllOperatingExpenses { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<BudgetByFy> BudgetByFies { get; set; }
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
        public virtual DbSet<OrganizationsContactInfo> OrganizationsContactInfos { get; set; }
        public virtual DbSet<ReallocMinute> ReallocMinutes { get; set; }
        public virtual DbSet<Reallocation> Reallocations { get; set; }
        public virtual DbSet<ReclassMinute> ReclassMinutes { get; set; }
        public virtual DbSet<Reclassification> Reclassifications { get; set; }
        public virtual DbSet<Soc> Socs { get; set; }
        public virtual DbSet<StudentLifeFee> StudentLifeFees { get; set; }
        public virtual DbSet<TechsyncName> TechsyncNames { get; set; }
        public virtual DbSet<TotalBudget> TotalBudgets { get; set; }
        public virtual DbSet<TotalLifeFeeBudget> TotalLifeFeeBudgets { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<AllOperatingExpense>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("All Operating Expenses");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FiscalYear)
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.LineItem)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Line Item")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.PaymentType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Payment Type")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Person)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Spent).HasPrecision(10, 2);

                entity.Property(e => e.WorkdayApprovalDate)
                    .HasColumnType("date")
                    .HasColumnName("Workday Approval Date");

                entity.Property(e => e.WorkdayApproved)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Workday Approved");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.NameOfClub, "Name of Club");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.NameOfClub)
                    .HasConstraintName("Budget_ibfk_1");
            });

            modelBuilder.Entity<BudgetByFy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Budget By FY");

                entity.Property(e => e.AmountApproved)
                    .HasPrecision(55, 2)
                    .HasColumnName("Amount Approved");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(54, 2)
                    .HasColumnName("Amount Proposed");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(54, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.AmountSpent)
                    .HasPrecision(54, 2)
                    .HasColumnName("Amount Spent");

                entity.Property(e => e.Appealed).HasPrecision(1);

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(54, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.BudgetId).HasColumnName("Budget ID");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year")
                    .HasDefaultValueSql("''")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name of Club")
                    .HasDefaultValueSql("''")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NumOfItems)
                    .HasPrecision(42)
                    .HasColumnName("Num of Items");

                entity.Property(e => e.RequestedAppeal)
                    .HasPrecision(54, 2)
                    .HasColumnName("Requested Appeal");
            });

            modelBuilder.Entity<BudgetBySection>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Budget By Section");

                entity.Property(e => e.AmountApproved)
                    .HasPrecision(33, 2)
                    .HasColumnName("Amount Approved");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(32, 2)
                    .HasColumnName("Amount Proposed");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(32, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.AmountSpent)
                    .HasPrecision(32, 2)
                    .HasColumnName("Amount Spent");

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(32, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.BudgetId).HasColumnName("Budget ID");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name of Club")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NumOfItems).HasColumnName("Num of Items");

                entity.Property(e => e.RequestedAppeal)
                    .HasPrecision(32, 2)
                    .HasColumnName("Requested Appeal");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Section Name")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<BudgetLegacy>(entity =>
            {
                entity.ToTable("BudgetLegacy");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.BId, "BudgetLegacy_Budget_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Proposed");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.AmountSpent)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Spent");

                entity.Property(e => e.AppealAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Appeal Amount");

                entity.Property(e => e.AppealDecision)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Appeal Decision")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Appealed)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.BId).HasColumnName("B_ID");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.BudgetLegacies)
                    .HasForeignKey(d => d.BId)
                    .HasConstraintName("BudgetLegacy_Budget_ID_fk");
            });

            modelBuilder.Entity<BudgetLineItem>(entity =>
            {
                entity.ToTable("BudgetLineItem");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.BsId, "BudgetLineItem_BudgetSection_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount_Proposed");

                entity.Property(e => e.AmountRequest)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount_Request");

                entity.Property(e => e.AmountSpent)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount_Spent");

                entity.Property(e => e.AppealAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Appeal_Amount");

                entity.Property(e => e.AppealDecision)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Appeal_Decision")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Appealed)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved_Appeal");

                entity.Property(e => e.BsId).HasColumnName("BS_ID");

                entity.Property(e => e.LineItemName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Line_Item_Name");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Bs)
                    .WithMany(p => p.BudgetLineItems)
                    .HasForeignKey(d => d.BsId)
                    .HasConstraintName("BudgetLineItem_BudgetSection_ID_fk");
            });

            modelBuilder.Entity<BudgetLineItemsReadable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Budget Line Items Readable");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Proposed");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.AmountSpent)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Spent");

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.LineItemName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Line Item Name")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name of Club")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Section Name")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<BudgetSection>(entity =>
            {
                entity.ToTable("BudgetSection");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.BId, "BudgetSection_Budget_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BId).HasColumnName("B_ID");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Section_Name");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.BudgetSections)
                    .HasForeignKey(d => d.BId)
                    .HasConstraintName("BudgetSection_Budget_ID_fk");
            });

            modelBuilder.Entity<CategoriesClubMembership>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Categories Club Membership");

                entity.Property(e => e.ActiveMembers)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Active Members")
                    .HasDefaultValueSql("'Not Provided'")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<CategoriesTotalBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Categories Total Budget");

                entity.HasComment("View 'sgadb.Categories Total Budget' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them");
            });

            modelBuilder.Entity<ClubClassification>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("Club Classifications");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.NameNavigation)
                    .WithOne(p => p.ClubClassification)
                    .HasForeignKey<ClubClassification>(d => d.Name)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Club Classifications_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<CompleteFundingRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Complete Funding Request");

                entity.Property(e => e.AgendaNumber)
                    .HasMaxLength(9)
                    .HasColumnName("Agenda Number")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.AppealAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Appeal Amount");

                entity.Property(e => e.AppealDecision)
                    .HasMaxLength(20)
                    .HasColumnName("Appeal Decision")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.AppealMinutes)
                    .HasMaxLength(255)
                    .HasColumnName("Appeal Minutes")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Appealed)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Approved).HasPrecision(10, 2);

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.DateOfEvent)
                    .HasColumnType("date")
                    .HasColumnName("Date of Event");

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(20)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.DotNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Dot Number")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FiscalYear)
                    .HasMaxLength(5)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.HearingDate)
                    .HasColumnType("date")
                    .HasColumnName("Hearing Date");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdtSubmitted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("IDT Submitted")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.MinutesLink)
                    .HasMaxLength(255)
                    .HasColumnName("Minutes Link")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name of Club")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Notes)
                    .HasMaxLength(512)
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.ReportFormApprovedAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Report Form Approved Amount");

                entity.Property(e => e.ReportFormStatus)
                    .HasMaxLength(25)
                    .HasColumnName("Report Form Status")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Requested).HasPrecision(10, 2);

                entity.Property(e => e.SpentAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Spent Amount");

                entity.Property(e => e.WorkdayApprovalDate)
                    .HasColumnType("date")
                    .HasColumnName("Workday Approval Date");

                entity.Property(e => e.WorkdayApproved)
                    .HasMaxLength(15)
                    .HasColumnName("Workday Approved")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<Frappeal>(entity =>
            {
                entity.ToTable("FRAppeals");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.FrId, "FRAppeals_FR_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppealAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Appeal Amount");

                entity.Property(e => e.AppealDate)
                    .HasColumnType("date")
                    .HasColumnName("Appeal Date");

                entity.Property(e => e.ApprovedAppeal)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved Appeal");

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FrId).HasColumnName("FR_ID");

                entity.Property(e => e.MinutesLink)
                    .HasMaxLength(255)
                    .HasColumnName("Minutes Link");

                entity.Property(e => e.NewDotNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("New Dot Number");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.Frappeal)
                    .HasForeignKey<Frappeal>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRAppeals_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<Frminute>(entity =>
            {
                entity.ToTable("FRMinutes");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.FrId, "FR_ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgendaNumber)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("Agenda Number");

                entity.Property(e => e.FrId).HasColumnName("FR_ID");

                entity.Property(e => e.MinutesLink)
                    .HasMaxLength(255)
                    .HasColumnName("Minutes Link");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.Frminute)
                    .HasForeignKey<Frminute>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Minutes_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<FrreportForm>(entity =>
            {
                entity.ToTable("FRReportForms");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.FrId, "FRReportForms_FR_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Approved Amount");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("date")
                    .HasColumnName("Approved Date");

                entity.Property(e => e.FrId).HasColumnName("FR_ID");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.SpentAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Spent Amount");

                entity.Property(e => e.Status).HasMaxLength(25);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithOne(p => p.FrreportForm)
                    .HasForeignKey<FrreportForm>(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRReportForms_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<Frsupplemental>(entity =>
            {
                entity.ToTable("FRSupplemental");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.FrId, "FRSupplemental_Funding Requests_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amended).HasColumnType("bit(1)");

                entity.Property(e => e.AmendedAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amended Amount");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.FrId).HasColumnName("FR_ID");

                entity.Property(e => e.ItemType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Item Type");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OtherType)
                    .HasMaxLength(100)
                    .HasColumnName("Other Type");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Fr)
                    .WithMany(p => p.Frsupplementals)
                    .HasForeignKey(d => d.FrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FRSupplemental_Funding Requests_ID_fk");
            });

            modelBuilder.Entity<FrworkdayIdt>(entity =>
            {
                entity.ToTable("FRWorkdayIDT");

                entity.HasComment("Workday and IDT Info for Funding Requests")
                    .HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.FrId, "FRWorkdayIDT_FR_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FrId).HasColumnName("FR_ID");

                entity.Property(e => e.IdtSubmitted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("IDT Submitted")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.WorkdayApprovalDate)
                    .HasColumnType("date")
                    .HasColumnName("Workday Approval Date");

                entity.Property(e => e.WorkdayApproved)
                    .HasMaxLength(15)
                    .HasColumnName("Workday Approved");

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

                entity.ToTable("Funding Accounts");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.AccountName).HasColumnName("Account Name");

                entity.Property(e => e.FiscalYear).HasColumnName("Fiscal Year");

                entity.Property(e => e.FallTransfer)
                    .HasPrecision(10, 2)
                    .HasColumnName("Fall Transfer");

                entity.Property(e => e.SpringTransfer)
                    .HasPrecision(10, 2)
                    .HasColumnName("Spring Transfer");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.WorkDayCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("Work Day Code");
            });

            modelBuilder.Entity<FundingRequest>(entity =>
            {
                entity.ToTable("Funding Requests");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.NameOfClub, "Funding Requests_Organizations_Name of Club_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountApproved)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Approved");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.DateOfEvent)
                    .HasColumnType("date")
                    .HasColumnName("Date of Event");

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DotNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Dot Number");

                entity.Property(e => e.FiscalYear)
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.FundingDate)
                    .HasColumnType("date")
                    .HasColumnName("Funding Date");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Notes).HasMaxLength(512);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.FundingRequests)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Funding Requests_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<Liability>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Liability");

                entity.Property(e => e.FiscalYear)
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.HeardRequests).HasColumnName("Heard Requests");

                entity.Property(e => e.RfApprovedAmt)
                    .HasPrecision(32, 2)
                    .HasColumnName("RF Approved Amt");

                entity.Property(e => e.TotalApproved)
                    .HasPrecision(32, 2)
                    .HasColumnName("Total Approved");

                entity.Property(e => e.TotalLiability)
                    .HasPrecision(32, 2)
                    .HasColumnName("Total Liability");

                entity.Property(e => e.TotalWorkdayLiability)
                    .HasPrecision(32, 2)
                    .HasColumnName("Total Workday Liability");

                entity.Property(e => e.WorkdayApprovedRequests)
                    .HasPrecision(23)
                    .HasColumnName("Workday Approved Requests");
            });

            modelBuilder.Entity<MandatoryTransfer>(entity =>
            {
                entity.ToTable("Mandatory Transfers");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.ParentOrganization, "Mandatory Transfers_Organizations_Name of Club_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountApproved)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Approved");

                entity.Property(e => e.AmountProposed)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Proposed");

                entity.Property(e => e.AmountRequested)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Requested");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.FundName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fund Name");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.ParentOrganization)
                    .IsRequired()
                    .HasColumnName("Parent Organization");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Worktag)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ParentOrganizationNavigation)
                    .WithMany(p => p.MandatoryTransfers)
                    .HasForeignKey(d => d.ParentOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mandatory Transfers_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<MandatoryTransfersTotalBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Mandatory Transfers Total Budget");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.FundName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fund Name")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.ParentOrganization)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Parent Organization")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.TotalBudget)
                    .HasPrecision(10, 2)
                    .HasColumnName("Total Budget");
            });

            modelBuilder.Entity<MtlineItem>(entity =>
            {
                entity.ToTable("MTLineItems");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.MtId, "MT_LineItems_MT_ID_index");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasPrecision(10, 2);

                entity.Property(e => e.LineItem)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Line Item");

                entity.Property(e => e.MtId).HasColumnName("MT_ID");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Mt)
                    .WithMany(p => p.MtlineItems)
                    .HasForeignKey(d => d.MtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MT_LineItems_Mandatory Transfers_ID_fk");
            });

            modelBuilder.Entity<OperatingExpense>(entity =>
            {
                entity.ToTable("Operating Expenses");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.MtliId, "MT_Expenses_MT_LineItems_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MtliId).HasColumnName("MTLI_ID");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PaymentType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Payment Type");

                entity.Property(e => e.Person)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Spent).HasPrecision(10, 2);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.WorkdayApprovalDate)
                    .HasColumnType("date")
                    .HasColumnName("Workday Approval Date");

                entity.Property(e => e.WorkdayApproved)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Workday Approved");

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

                entity.Property(e => e.NameOfClub).HasColumnName("Name of Club");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(8)
                    .HasColumnName("Account Number");

                entity.Property(e => e.Acronym1)
                    .HasMaxLength(50)
                    .HasColumnName("Acronym 1");

                entity.Property(e => e.Classification).HasMaxLength(100);

                entity.Property(e => e.Inactive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Inactive?");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TypeOfClub)
                    .HasMaxLength(100)
                    .HasColumnName("Type of Club");
            });

            modelBuilder.Entity<OrganizationComment>(entity =>
            {
                entity.ToTable("Organization Comments");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.NameOfClub, "orgCommentName_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CommentDate)
                    .HasColumnType("date")
                    .HasColumnName("Comment Date");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.ToTable("Organization Membership Numbers");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.NameOfOrganization).HasColumnName("Name of Organization");

                entity.Property(e => e.FiscalYear).HasColumnName("Fiscal Year");

                entity.Property(e => e.ActiveMembers)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Active Members")
                    .HasDefaultValueSql("'Not Provided'");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfOrganizationNavigation)
                    .WithMany(p => p.OrganizationMembershipNumbers)
                    .HasForeignKey(d => d.NameOfOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgMemName");
            });

            modelBuilder.Entity<OrganizationsContactInfo>(entity =>
            {
                entity.HasKey(e => e.NameOfClub)
                    .HasName("PRIMARY");

                entity.ToTable("Organizations Contact Info");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.NameOfClub).HasColumnName("Name of Club");

                entity.Property(e => e.PresidentEmail)
                    .HasMaxLength(255)
                    .HasColumnName("President Email");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TreasurerEmail)
                    .HasMaxLength(255)
                    .HasColumnName("Treasurer Email");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithOne(p => p.OrganizationsContactInfo)
                    .HasForeignKey<OrganizationsContactInfo>(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Organizations Contact Info_Organizations_Name of Club_fk");
            });

            modelBuilder.Entity<ReallocMinute>(entity =>
            {
                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.ReallocId, "ReallocMinutes_Reallocations_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgendaNumber)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("Agenda Number");

                entity.Property(e => e.MinutesLink)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Minutes Link");

                entity.Property(e => e.ReallocId).HasColumnName("Realloc_ID");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.HasIndex(e => e.NameOfClub, "orgRealName_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AllocatedFrom)
                    .HasMaxLength(255)
                    .HasColumnName("Allocated From");

                entity.Property(e => e.AllocatedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Allocated To");

                entity.Property(e => e.AllocationAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("Allocation Amount");

                entity.Property(e => e.AmountApproved)
                    .HasPrecision(10, 2)
                    .HasColumnName("Amount Approved");

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DotNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Dot Number");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.HearingDate)
                    .HasColumnType("date")
                    .HasColumnName("Hearing Date");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.HasIndex(e => e.ReclassId, "ReclassMinutes_Reclassifications_ID_fk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgendaNumber)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("Agenda Number");

                entity.Property(e => e.MinutesLink)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Minutes Link");

                entity.Property(e => e.ReclassId).HasColumnName("Reclass_ID");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.HasIndex(e => e.NameOfClub, "orgReclName_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Approved Class");

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DotNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Dot Number");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.HearingDate)
                    .HasColumnType("date")
                    .HasColumnName("Hearing Date");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OriginalClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Original Class");

                entity.Property(e => e.RequestedClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Requested Class");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithMany(p => p.Reclassifications)
                    .HasForeignKey(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgReclName");
            });

            modelBuilder.Entity<Soc>(entity =>
            {
                entity.ToTable("SOC");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Acronym).HasMaxLength(255);

                entity.Property(e => e.Decision)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.HearingDate)
                    .HasColumnType("date")
                    .HasColumnName("Hearing Date");

                entity.Property(e => e.NameOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name of Club");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.PresidentEmail)
                    .HasMaxLength(255)
                    .HasColumnName("President Email");

                entity.Property(e => e.ProjectedActiveMembers).HasColumnName("Projected Active Members");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TreasurerEmail)
                    .HasMaxLength(255)
                    .HasColumnName("Treasurer Email");

                entity.Property(e => e.TypeOfClub)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Type of Club");
            });

            modelBuilder.Entity<StudentLifeFee>(entity =>
            {
                entity.ToTable("Student Life Fee");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FallStudentAmount).HasColumnName("Fall Student Amount");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year");

                entity.Property(e => e.SlfAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("SLF Amount");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<TechsyncName>(entity =>
            {
                entity.HasKey(e => e.NameOfClub)
                    .HasName("PRIMARY");

                entity.ToTable("Techsync Names");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.NameOfClub).HasColumnName("Name of Club");

                entity.Property(e => e.TechsyncName1)
                    .HasMaxLength(255)
                    .HasColumnName("Techsync Name");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.NameOfClubNavigation)
                    .WithOne(p => p.TechsyncName)
                    .HasForeignKey<TechsyncName>(d => d.NameOfClub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orgTechName");
            });

            modelBuilder.Entity<TotalBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Total Budget");

                entity.HasComment("View 'sgadb.Total Budget' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them");
            });

            modelBuilder.Entity<TotalLifeFeeBudget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Total Life Fee Budget");

                entity.Property(e => e.FiscalYear)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Fiscal Year")
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

                entity.HasIndex(e => e.From, "transferFrom");

                entity.HasIndex(e => e.To, "transferTo_idx");

                entity.Property(e => e.FiscalYear).HasColumnName("Fiscal Year");

                entity.Property(e => e.DateOfTransfer)
                    .HasColumnType("date")
                    .HasColumnName("Date of Transfer");

                entity.Property(e => e.Amount).HasPrecision(10, 2);

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
