using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameGeekApi.Dto;

namespace BoardGameGeekApi
{
    internal class RankPageBilder
    {
        const short GAME_ON_PAGE = 100;

        private RankPageParser rankPageParser;

        public RankPageBilder(RankPageParser rankPageParser)
        {
            this.rankPageParser = rankPageParser;
        }

        public IEnumerable<BoardGameDto> Get(short first, short last)
        {
           return  this.rankPageParser.Parse(first);
        }
    }
}
