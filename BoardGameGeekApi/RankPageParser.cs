using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameGeekApi
{
    internal class RankPageParser
    {
        const string URL = "http://boardgamegeek.com";
        const string RANK_SUFIX = "/browse/boardgame/page/";

        public IEnumerable<BoardGameRank> Parse(string url)
        {
            return null;
        }
    }
}
