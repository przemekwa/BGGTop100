using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameGeekApi.Dto;
using HtmlAgilityPack;

namespace BoardGameGeekApi
{
    internal class RankPageParser
    {
        const string URL = "http://boardgamegeek.com";
        const string RANK_SUFIX = "/browse/boardgame/page/";

        public IEnumerable<BoardGameDto> Parse(short page)
        {
            var url = $"{URL}{RANK_SUFIX}{page}";

            var htmlWeb = new HtmlWeb
            {
                AutoDetectEncoding = true
            };

           var htmlDoc = htmlWeb.Load(url);

            var htmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//div[@id='main_content']/div[@id='collection']/table[@class='collection_table']/tr[@id='row_']");

            var result = new List<BoardGameDto>();

            foreach (HtmlNode trTag in htmlNodeCollection)
            {
                var rank = trTag.SelectSingleNode("td[@class='collection_rank']/a").Attributes["name"].Value;

                var imgUrl = trTag.SelectSingleNode("td[@class='collection_thumbnail']/a/img").Attributes["src"].Value;

                var name = trTag.SelectNodes("td")[2].SelectSingleNode("div/a").InnerText;

                var year = trTag.SelectNodes("td")[2].SelectSingleNode("div/span").InnerText;

                result.Add(new BoardGameDto
                {
                    Rank = short.Parse(rank),
                    Name = name,
                    Year = year.Substring(1,year.Length-2),
                    ImageUrl = $"http:{imgUrl}" 
                });
            }

            return result;
        }

        
    }
}
