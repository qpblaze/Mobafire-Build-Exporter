using LoLSets.Core.Entities;

namespace LoLSets.Core.Interfaces
{
    public interface IChampionService
    {
        Champion GetChampionByName(string name);

        int GetChampionId(string name);
    }
}