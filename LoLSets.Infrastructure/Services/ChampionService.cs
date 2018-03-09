using LoLSets.Core.Entities;
using LoLSets.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoLSets.Infrastructure.Services
{
    public class ChampionService : IChampionService
    {
        private readonly Dictionary<string, Champion> _items;

        public ChampionService()
        {
            using (StreamReader r = new StreamReader("champions.json"))
            {
                string json = r.ReadToEnd();
                _items = JsonConvert.DeserializeObject<Dictionary<string, Champion>>(json);
            }
        }

        public Champion GetChampionByName(string name)
        {
            return _items.FirstOrDefault(x => x.Value.Name.Trim().ToLower().Contains(name.Trim().ToLower())).Value;
        }

        public int GetChampionId(string name)
        {
            return GetChampionByName(name).ID;
        }
    }
}