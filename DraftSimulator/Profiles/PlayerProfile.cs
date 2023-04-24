using AutoMapper;
using DraftSimulator;

public class PlayerMappingProfile : Profile
{
    public PlayerMappingProfile()
    {
        CreateMap<Player, PlayerEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PrimaryNumber, opt => opt.MapFrom(src => src.PrimaryNumber))
            .ForMember(dest => dest.CurrentAge, opt => opt.MapFrom(src => src.CurrentAge))
            .ForMember(dest => dest.ShootsCatches, opt => opt.MapFrom(src => src.ShootsCatches))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.PrimaryPosition))
            .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.CurrentTeam));

        CreateMap<PlayerEntity, Player>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PrimaryNumber, opt => opt.MapFrom(src => src.PrimaryNumber))
            .ForMember(dest => dest.CurrentAge, opt => opt.MapFrom(src => src.CurrentAge))
            .ForMember(dest => dest.ShootsCatches, opt => opt.MapFrom(src => src.ShootsCatches))
            .ForMember(dest => dest.PrimaryPosition, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.CurrentTeam, opt => opt.MapFrom(src => src.Team));
    }
}

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, TeamEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationName));

        CreateMap<TeamEntity, Team>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationName));
    }
}

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<Position, PositionEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<PositionEntity, Position>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
    }
}