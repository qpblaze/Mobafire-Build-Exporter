using LoLSets.Core.Entities;

namespace LoLSets.Core.Interfaces
{
    public interface IItemService
    {
        Item GetItemByName(string name);

        int GetItemId(string name);
    }
}