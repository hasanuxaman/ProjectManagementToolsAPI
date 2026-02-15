using PMSYSAPI.DTOs.Company;
using PMSYSAPI.DTOs.Project;

namespace PMSYSAPI.DTOs.Project
{
    public class ProjectDto
    {
        public int ProjCod { get; set; }
        public string ProjName { get; set; } = string.Empty;
        public string ProjShortname { get; set; } = string.Empty;
        public string ProjDesc { get; set; } = string.Empty;
        public int CompCod { get; set; }
        public int PhaseCod { get; set; }
        public int StatusCod { get; set; }
        public DateTime? InitDate { get; set; }
        public DateTime? StartPlannedDate { get; set; }
        public DateTime? EndPlannedDate { get; set; }
        public decimal? EstimatedAmount { get; set; }
    }

    public class CreateProjectDto
    {
        public string ProjName { get; set; } = string.Empty;
        public string ProjShortname { get; set; } = string.Empty;
        public string ProjDesc { get; set; } = string.Empty;
        public int ProjCompCod { get; set; }
        public int ProjPhaseCod { get; set; }
        public int ProjStsCod { get; set; }
        public DateTime ProjInitDate { get; set; }
        public DateTime ProjStrtPlndt { get; set; }
        public DateTime ProjEndPlandt { get; set; }
        public decimal ProjEstAmount { get; set; }
    }

    public class UpdateProjectDto
    {
        public string ProjName { get; set; } = string.Empty;
        public string ProjShortname { get; set; } = string.Empty;
        public string ProjDesc { get; set; } = string.Empty;
        public int ProjCompCod { get; set; }
        public int ProjPhaseCod { get; set; }
        public int ProjStsCod { get; set; }
        public DateTime ProjInitDate { get; set; }
        public DateTime ProjStrtPlndt { get; set; }
        public DateTime ProjEndPlandt { get; set; }
        public decimal ProjEstAmount { get; set; }
    }

    public class ProjectPhaseDto
    {
        public int PhaseCod { get; set; }
        public string PhaseName { get; set; } = string.Empty;
        public string PhaseDesc { get; set; } = string.Empty;
    }
    public class CreateProjectPhaseDto
    {
        public string PhaseName { get; set; } = string.Empty;
        public string? PhaseDesc { get; set; }
    }
    public class UpdateProjectPhaseDto
    {
        public string PhaseName { get; set; } = string.Empty;
        public string? PhaseDesc { get; set; }
    }


    public class ProjectStatusDto
    {
        public int ProjStatusCod { get; set; }
        public string ProjStatus { get; set; } = string.Empty;
        public string ProjStsDtl { get; set; } = string.Empty;
    }
    

    public class CreateProjectStatusDto
    {
        public string StatusName { get; set; } = string.Empty;
        public string? StatusShortname { get; set; }
    }

    public class UpdateProjectStatusDto
    {
        public string StatusName { get; set; } = string.Empty;
        public string? StatusShortname { get; set; }
    }

    public class ProjectSummaryDto
    {
        public int TotalProjects { get; set; }
        public decimal? TotalEstimatedAmount { get; set; }
        public decimal? AverageProjectAmount { get; set; }
        public List<ProjectStatusSummaryDto>? StatusSummary { get; set; }
        public List<ProjectPhaseSummaryDto>? PhaseSummary { get; set; }
        public List<CompanyProjectSummaryDto>? CompanySummary { get; set; }
    }

    public class ProjectStatusSummaryDto
    {
        public int StatusCod { get; set; }
        public int Count { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class ProjectPhaseSummaryDto
    {
        public string PhaseName { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal? TotalAmount { get; set; }
    }
    //public class CompanyProjectSummaryDto
    //{
    //    public string CompanyName { get; set; } = string.Empty;
    //    public int ProjectCount { get; set; }
    //    public decimal? TotalAmount { get; set; }
    //}
}
