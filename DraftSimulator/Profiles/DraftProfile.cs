using AutoMapper;

namespace DraftSimulator;


public class DraftMappingProfile : Profile
{
    public DraftMappingProfile()
    {
        CreateMap<Draft, DraftEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DraftId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.NumPlayers, opt => opt.MapFrom(src => src.NumPlayers))
            .ForMember(dest => dest.DraftTeams, opt => opt.MapFrom(src => src.DraftTeams));

        CreateMap<DraftEntity, Draft>()
            .ForMember(dest => dest.DraftId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.NumPlayers, opt => opt.MapFrom(src => src.NumPlayers))
            .ForMember(dest => dest.DraftTeams, opt => opt.MapFrom(src => src.DraftTeams));
    }
}
