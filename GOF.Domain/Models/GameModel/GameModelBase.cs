#nullable enable

using System.Collections.Generic;

namespace GOF.Domain.Models.GameModel
{
    /// <summary>
    /// GameModelBase class to be used as base for GameModel classes
    /// </summary>
    public class GameModelBase
    {
        public int SquareSideSize { get; set; }
        public List<List<int>>? InitialState { get; set; }
        public int MaxGenerations { get; set; }
    }
}