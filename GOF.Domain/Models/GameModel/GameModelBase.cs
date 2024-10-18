#nullable enable

using System.Collections.Generic;

namespace GOF.Domain.Models.GameModel
{
    public class GameModelBase
    {
        public int SquareSideSize { get; set; }
        public List<List<int>>? InitialState { get; set; }
        public int maxGenerations { get; set; }
    }
}