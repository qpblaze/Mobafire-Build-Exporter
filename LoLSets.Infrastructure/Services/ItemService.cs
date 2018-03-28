using LoLSets.Core.Entities;
using LoLSets.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoLSets.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly Dictionary<int, Item> _items;

        public ItemService()
        {
            using (StreamReader r = new StreamReader("items.json"))
            {
                string json = r.ReadToEnd();
                _items = JsonConvert.DeserializeObject<Dictionary<int, Item>>(json);
            }
        }

        public Item GetItemByName(string name)
        {
            return _items.FirstOrDefault(x => x.Value.InStore == true &&
                            x.Value.Name.Trim().ToLower().Contains(name.Trim().ToLower())).Value;
        }

        public int GetItemId(string name)
        {
            return GetItemByName(name).ID;
        }
    }
}