using AutoMapper;
using GOF.Domain.Entities;
using GOF.Domain.Models.GameModel.Request;
using GOF.Domain.Models.GameModel.Response;

namespace GOF.Domain.Mapping
{
    public class GameMapping : Profile
    {
        public GameMapping()
        {
            CreateMap<PostGameRequest, GameEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Stages, opt => opt.Ignore());
            CreateMap<GameEntity, PostGameResponse>();
            CreateMap<GameEntity, GetGameResponse>();
        }
    }
}