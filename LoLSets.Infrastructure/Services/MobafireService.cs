﻿using HtmlAgilityPack;
using LoLSets.Core.Entities;
using LoLSets.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoLSets.Infrastructure.Services
{
    public class MobafireService : IMobafireService
    {
        private readonly IItemService _itemService;
        private readonly IChampionService _championService;
        private HtmlDocument document;

        public MobafireService(
           IItemService itemService,
           IChampionService championService)
        {
            _itemService = itemService;
            _championService = championService;
        }

        private async Task LoadDocument(string link)
        {
            HttpClient client = new HttpClient();

            using (var response = await client.GetAsync(link))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception("The link is not valid.");

                using (var content = response.Content)
                {
                    var result = await content.ReadAsStringAsync();
                    document = new HtmlDocument();
                    document.LoadHtml(result);
                }
            }
        }

        private IEnumerable<HtmlNode> GetBlocks()
        {
            var node = document.DocumentNode
                            .Descendants("div")
                            .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("build-box"));

            var node2 = node.Descendants("div")
                            .Where(o => o.GetAttributeValue("class", "").Contains("item-wrap self-clear float-left"));

            return node2;
        }

        private string GetBlockName(HtmlNode node)
        {
            return node.SelectSingleNode("./h2").InnerText.Trim();
        }

        private string GetTitle()
        {
            return document.DocumentNode
                            .Descendants("h2")
                            .Where(o => o.GetAttributeValue("class", "")
                            .Contains("guide-main-title"))
                            .FirstOrDefault()
                            .InnerText.Trim();
        }

        private IEnumerable<HtmlNode> GetItems(HtmlNode node)
        {
            return node.Descendants("div")
                        .Where(o => o.GetAttributeValue("class", "")
                        .Contains("main-items"));
        }

        private string GetBlockItemName(HtmlNode node)
        {
            string name = node.Descendants("span")
                        .FirstOrDefault(o => o.GetAttributeValue("class", "")
                        .Contains("ajax-tooltip")).InnerText.Trim();

            if(name.Contains(" - "))
            {
                int startIndex = name.IndexOf(" - ") + 3;
                int endIndex = name.Length - startIndex;

                string newName = name.Substring(startIndex, endIndex);
                newName = "Enchantment: " + newName;

                return newName;
            }

            return name;
        }

        private int GetBlockItemCount(HtmlNode node)
        {
            var countNode = node.SelectSingleNode(".//span[contains(@class, 'item-count')]");

            if (countNode != null)
            {
                return int.Parse(countNode.InnerText.Replace('x', ' ').Trim());
            }

            return 1;
        }

        private List<BlockItem> GetBlockItems(HtmlNode node)
        {
            List<BlockItem> blockItems = new List<BlockItem>();

            foreach (var itemNode in GetItems(node))
            {
                BlockItem blockItem = new BlockItem()
                {
                    ID = _itemService.GetItemId(GetBlockItemName(itemNode)).ToString(),
                    Count = GetBlockItemCount(itemNode)
                };

                blockItems.Add(blockItem);
            }

            return blockItems;
        }

        private List<int> GetMaps()
        {
            return new List<int> { 10, 11, 12 };
        }

        private List<int> GetChampions()
        {
            List<int> champions = new List<int>();

            var champNode = document.DocumentNode.SelectSingleNode("//*[@id=\"sidebar-similar-builds\"]/div[1]/div[1]/img");

            string champName = champNode.Attributes["title"].Value.Trim();

            champions.Add(_championService.GetChampionId(champName));

            return champions;
        }

        public async Task<ItemSet> GetItemSetAsync(string link, string title = null)
        {
            ItemSet itemSet = new ItemSet();

            await LoadDocument(link);

            if (title == null)
            {
                itemSet.Title = GetTitle();
            }
            else
            {
                itemSet.Title = title;
            }
            itemSet.AssociatedMaps = GetMaps();
            itemSet.AssociatedChampions = GetChampions();

            var nodes = GetBlocks();
            foreach (var node in nodes)
            {
                Block block = new Block
                {
                    Type = GetBlockName(node),
                    Items = GetBlockItems(node)
                };

                itemSet.Blocks.Add(block);
            }

            return itemSet;
        }

        public bool IsLinkValid(string link)
        {
            if(!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return false;

            Uri uri = new Uri(link);
            if(uri.Host.ToLower() != "www.mobafire.com")
                return false;

            if (!uri.LocalPath.ToLower().Contains("/league-of-legends/build/"))
                return false;

            return true;
        }
    }
}