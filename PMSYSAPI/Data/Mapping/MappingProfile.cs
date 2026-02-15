using AutoMapper;
using PMSYSAPI.DTOs.Company;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // CreateProjectDto -> tbProjList
            CreateMap<CreateProjectDto, tbProjList>()
                .ForMember(dest => dest.ProjName, opt => opt.MapFrom(src => src.ProjName))
                .ForMember(dest => dest.ProjShortname, opt => opt.MapFrom(src => src.ProjShortname))
                .ForMember(dest => dest.ProjDesc, opt => opt.MapFrom(src => src.ProjDesc))
                .ForMember(dest => dest.ProjCompCod, opt => opt.MapFrom(src => src.ProjCompCod))
                .ForMember(dest => dest.ProjPhaseCod, opt => opt.MapFrom(src => src.ProjPhaseCod))
                .ForMember(dest => dest.ProjStsCod, opt => opt.MapFrom(src => src.ProjStsCod))
                .ForMember(dest => dest.ProjInitDate, opt => opt.MapFrom(src => src.ProjInitDate))
                .ForMember(dest => dest.ProjStrtPlndt, opt => opt.MapFrom(src => src.ProjStrtPlndt))
                .ForMember(dest => dest.ProjEndPlandt, opt => opt.MapFrom(src => src.ProjEndPlandt))
                .ForMember(dest => dest.ProjEstAmount, opt => opt.MapFrom(src => src.ProjEstAmount));

            // tbProjList -> ProjectDto
            CreateMap<tbProjList, ProjectDto>()
                .ForMember(dest => dest.ProjName, opt => opt.MapFrom(src => src.ProjName))
                .ForMember(dest => dest.ProjShortname, opt => opt.MapFrom(src => src.ProjShortname))
                .ForMember(dest => dest.ProjDesc, opt => opt.MapFrom(src => src.ProjDesc))
                .ForMember(dest => dest.CompCod, opt => opt.MapFrom(src => src.ProjCompCod))
                .ForMember(dest => dest.PhaseCod, opt => opt.MapFrom(src => src.ProjPhaseCod))
                .ForMember(dest => dest.StatusCod, opt => opt.MapFrom(src => src.ProjStsCod))
                .ForMember(dest => dest.InitDate, opt => opt.MapFrom(src => src.ProjInitDate))
                .ForMember(dest => dest.StartPlannedDate, opt => opt.MapFrom(src => src.ProjStrtPlndt))
                .ForMember(dest => dest.EndPlannedDate, opt => opt.MapFrom(src => src.ProjEndPlandt))
                .ForMember(dest => dest.EstimatedAmount, opt => opt.MapFrom(src => src.ProjEstAmount));
                // Include optional navigation names
                //.ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.CompName : null))
                //.ForMember(dest => dest.PhaseCod, opt => opt.MapFrom(src => src.Phase != null ? src.Phase.PhaseName : null))
                //.ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status != null ? src.Status.ProjStatus : null));



            // CreateProjectStatusDto -> tbStatus
            CreateMap<CreateProjectStatusDto, tbStatus>()
                .ForMember(dest => dest.ProjStatus, opt => opt.MapFrom(src => src.StatusName))
                .ForMember(dest => dest.ProjStsDtl, opt => opt.MapFrom(src => src.StatusShortname));

            // UpdateProjectStatusDto -> tbStatus
            CreateMap<UpdateProjectStatusDto, tbStatus>()
                .ForMember(dest => dest.ProjStatus, opt => opt.MapFrom(src => src.StatusName))
                .ForMember(dest => dest.ProjStsDtl, opt => opt.MapFrom(src => src.StatusShortname));

            // tbStatus -> ProjectStatusDto
            CreateMap<tbStatus, ProjectStatusDto>()
                .ForMember(dest => dest.ProjStatusCod, opt => opt.MapFrom(src => src.ProjStatusCod))
                .ForMember(dest => dest.ProjStatus, opt => opt.MapFrom(src => src.ProjStatus))
                .ForMember(dest => dest.ProjStsDtl, opt => opt.MapFrom(src => src.ProjStsDtl));


            // ====== CreateProjectPhaseDto Mappings ======
            CreateMap<CreateProjectPhaseDto, tbProjPhase>();
            CreateMap<UpdateProjectPhaseDto, tbProjPhase>();
            CreateMap<tbProjPhase, ProjectPhaseDto>();
            // ====== CompanyGroup Mappings ======

            // Create DTO -> Entity
            CreateMap<CreateCompanyGroupDto, tbCompGrp>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.GroupShortname, opt => opt.MapFrom(src => src.GroupShortname)); // DB column name

            // Update DTO -> Entity
            CreateMap<UpdateCompanyGroupDto, tbCompGrp>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.GroupShortname, opt => opt.MapFrom(src => src.GroupShortname));

            // Entity -> DTO
            CreateMap<tbCompGrp, CompanyGroupDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.GroupShortname, opt => opt.MapFrom(src => src.GroupShortname));

            // ====== Project Mappings ======
            CreateMap<tbProjList, ProjectDto>();
            CreateMap<tbProjPhase, ProjectPhaseDto>();

            // ====== Company Mappings ======
            CreateMap<CreateCompanyDto, tbComp>();
            CreateMap<tbComp, CompanyDto>();
        }
    }
}
