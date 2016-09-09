using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameRankExplorer.Models
{
    public class BoardGameModel
    {
        public string Name { get; set; }
        public short Rank { get; set; }
        public string Year { get; set; }
        public string ImgUrl { get; set; }
        public string GameUrl { get; set; }
    }
}