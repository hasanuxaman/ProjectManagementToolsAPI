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
