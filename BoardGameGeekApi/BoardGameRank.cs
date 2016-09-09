using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameGeekApi.Dto;

namespace BoardGameGeekApi
{
    public class BoardGameRank
    {
        private RankPageBilder rankPageBilder;

        public BoardGameRank()
        {
            this.rankPageBilder = new RankPageBilder(new RankPageParser());
        }

        public IEnumerable<BoardGameDto> GetRank(short first, short last)
        {
            return this.rankPageBilder.Get(first, last);
        }
     }
}
