using LoLSets.Core.Entities;
using System.Threading.Tasks;

namespace LoLSets.Core.Interfaces
{
    public interface IMobafireService
    {
        Task<ItemSet> GetItemSetAsync(string link, string title);
    }
}