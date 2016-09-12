﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
            var result = new List<BoardGameDto>();

            var f =(short) Math.Ceiling(first/100m);
            

            for (int i = f; i <= 3  ; i++)
            {
                result.AddRange(this.rankPageParser.Parse((short)i));
            }

            var count = (first-1)-(((f-1)*100)); 
            return result.Skip(count).Take(last+1);
        }
    }
}
