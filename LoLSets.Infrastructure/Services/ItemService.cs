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
        private readonly List<Item> _items;

        public ItemService()
        {
            using (StreamReader r = new StreamReader("items.json"))
            {
                string json = r.ReadToEnd();
                _items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }

        public Item GetItemByName(string name)
        {
            return _items.FirstOrDefault(x => x.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
        }

        public int GetItemId(string name)
        {
            return GetItemByName(name).ID;
        }
    }
}