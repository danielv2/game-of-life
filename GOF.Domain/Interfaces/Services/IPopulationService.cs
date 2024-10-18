
namespace GOF.Domain.Interfaces.Services
{
    public interface IPopulationService
    {
        List<List<int>> GeneratePopulationBoardAsync(int squareSideSize, List<List<int>>? initialState);
        List<List<int>> NextGeneration(List<List<int>> board, int squareSideSize);
    }
}