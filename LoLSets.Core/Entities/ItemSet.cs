using System.Collections.Generic;

namespace LoLSets.Core.Entities
{
    public class ItemSet
    {
        public ItemSet()
        {
            Blocks = new List<Block>();
            AssociatedMaps = new List<int>();
            AssociatedChampions = new List<int>();
        }

        public string Title { get; set; }

        public List<int> AssociatedMaps { get; set; }
        public List<int> AssociatedChampions { get; set; }

        public List<Block> Blocks { get; set; }
    }
}