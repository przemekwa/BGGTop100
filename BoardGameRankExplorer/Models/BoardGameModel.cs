using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameRankExplorer.Models
{
    public class BoardGameModel
    {
        public short FirstRank { get; set; }
        public short LastRank { get; set; }
        public IEnumerable<BoardGameDto> BoardGameCollection { get; set; }
    }
}