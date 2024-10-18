using AutoMapper;
using GOF.Domain.Entities;
using GOF.Domain.Models.GameModel.Request;
using GOF.Domain.Models.GameModel.Response;
using GOF.Domain.Models.GameStageModel.Response;

namespace GOF.Domain.Mapping
{
    /// <summary>
    /// GameMapping class
    /// </summary>
    public class GameStageMapping : Profile
    {
        public GameStageMapping()
        {
            CreateMap<GameStageEntity, GetGameStageResponse>().PreserveReferences();
        }
    }
}