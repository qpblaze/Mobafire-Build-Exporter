using System.Collections.Generic;

namespace LoLSets.Core.Entities
{
    public class Block
    {
        public Block()
        {
            Items = new List<BlockItem>();
        }

        public string Type { get; set; }
        public List<BlockItem> Items { get; set; }
    }
}