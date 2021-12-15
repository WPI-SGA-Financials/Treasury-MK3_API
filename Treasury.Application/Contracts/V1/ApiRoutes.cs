namespace Treasury.Application.Contracts.V1;

public class ApiRoutes
{
    private const string Root = "api";

    private const string Version = "v1";

    private const string Base = Root + "/" + Version;

    private const string FinancialsBase = Base + "/" + "financials";

    private const string OrganizationBase = Base + "/" + "organization";

    public static class FundingRequest
    {
        public const string GetAll = FinancialsBase + "/frs";

        public const string Get = FinancialsBase + "/fr/{id}";

        public const string GetByFy = FinancialsBase + "/frs/{fy}";

        public const string GetByOrg = OrganizationBase + "/{name}/frs";

        public const string GetByOrgFy = OrganizationBase + "/{name}/frs/{fy}";
    }

    public static class Budget
    {
        public const string GetAll = FinancialsBase + "/budgets";

        public const string Get = FinancialsBase + "/budget/{id}";

        public const string GetByFy = FinancialsBase + "/budgets/{fy}";

        public const string GetByOrg = OrganizationBase + "/{name}/budgets";

        public const string GetByOrgFy = OrganizationBase + "/{name}/budgets/{fy}";
    }

    public static class ReallocationRequest
    {
        public const string GetAll = FinancialsBase + "/reallocs";

        public const string Get = FinancialsBase + "/realloc/{id}";

        public const string GetByFy = FinancialsBase + "/reallocs/{fy}";

        public const string GetByOrg = OrganizationBase + "/{name}/reallocs";

        public const string GetByOrgFy = OrganizationBase + "/{name}/reallocs/{fy}";
    }

    public static class Organization
    {
        public const string Get = OrganizationBase + "/{name}";
    }

    public static class Organizations
    {
        public const string GetAll = Base + "/organizations";

        public const string FilterByName = Base + "/organizations/{name}";
    }

    public static class StudentLifeFee
    {
        public const string GetAll = FinancialsBase + "/slf";

        public const string GetByFy = FinancialsBase + "/slf/{fy}";
    }

    public static class Metadata
    {
        public const string All = Base + "/metadata/all";

        public const string Classification = Base + "/metadata/classification";

        public const string ClubTypes = Base + "/metadata/clubtypes";

        public const string FiVizClubTypes = Base + "/metadata/fiviz-clubtypes";

        public const string FiscalClass = Base + "/metadata/fiscalclass";

        public const string FiscalYear = Base + "/metadata/fiscalyear";
    }
}