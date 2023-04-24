using AutoMapper;
using DraftSimulator;

public class DraftTeamMappingProfile : Profile
{
    public DraftTeamMappingProfile()
    {
        // Mapping from DraftTeam to DraftTeamEntity
        CreateMap<DraftTeam, DraftTeamEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TeamId))
            .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NumPlayers, opt => opt.MapFrom(src => src.NumPlayers));

        // Mapping from DraftTeamEntity to DraftTeam
        CreateMap<DraftTeamEntity, DraftTeam>()
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players))
            .ForMember(dest => dest.NumPlayers, opt => opt.MapFrom(src => src.NumPlayers));
    }
}
