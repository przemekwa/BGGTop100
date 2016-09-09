using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameGeekApi.Dto
{
    public class BoardGameDto
    {
        public string Name { get; set; }
        public short Rank { get; set; }
        public string Year { get; set; }
        public string ImageUrl { get; set; }
    }
}
