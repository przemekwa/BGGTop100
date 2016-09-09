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
            short firstPage = (short)(Math.Round(first/100d, MidpointRounding.AwayFromZero) + 1);
            short lastPage = (short)Math.Round(last / 100d, MidpointRounding.AwayFromZero);

            var result = new List<BoardGameDto>();

            for (short i = firstPage ; i < lastPage; i++)
            {
                result.AddRange(this.rankPageParser.Parse(i));
            }

            return result.Skip(first-1).Take(last+1);
        }
    }
}
