namespace PMSYSAPI.DTOs.Company

{
    public class CompanyDto
    {
        public int CompCod { get; set; }
        public string Compname { get; set; } = string.Empty;
        public string CompShortname { get; set; } = string.Empty;
        public int CmpGroupCod { get; set; }
        public string? GroupName { get; set; }
    }

    public class CreateCompanyDto
    {
        public string Compname { get; set; } = string.Empty;
        public string CompShortname { get; set; } = string.Empty;
        public int CmpGroupCod { get; set; }
    }

    public class UpdateCompanyDto
    {
        public string Compname { get; set; } = string.Empty;
        public string CompShortname { get; set; } = string.Empty;
        public int CmpGroupCod { get; set; }
    }

    public class CompanyGroupDto
    {
        public int GroupCod { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string GroupShortname { get; set; } = string.Empty;
    }
    public class CreateCompanyGroupDto
    {
        public string GroupName { get; set; } = string.Empty;
        public string? GroupShortname { get; set; }
    }
    public class UpdateCompanyGroupDto
    {
        public string GroupName { get; set; } = string.Empty;
        public string? GroupShortname { get; set; }
    }

    public class CompanySummaryDto
    {
        public int TotalCompanies { get; set; }
        public List<CompanyProjectSummaryDto> TopCompanies { get; set; } = new();
    }

    public class CompanyProjectSummaryDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
