using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameGeekApi;
using Xunit;

namespace Tests
{
    public class BoardGameGeekApi_Test
    {
        [Fact]
        public void Get_Main_Page_Rank_Test()
        {
            var api = new BoardGameRank();

            var result = api.GetRank(1, 100);

            Assert.Equal(100, result.Count());
        }

        [Fact]
        public void Get_Board_Game_Dto_Data_Test()
        {
            var api = new BoardGameRank();

            var result = api.GetRank(1, 200).FirstOrDefault();

            Assert.NotEmpty(result.Name);
            Assert.NotEmpty(result.Year);
            Assert.NotEmpty(result.ImageUrl);
            Assert.NotEmpty(result.GameUrl);
            Assert.Equal(1, result.Rank);
        }

        [Theory]
        [InlineData(1,300)]
        [InlineData(100,400)]
        [InlineData(150,450)]
        [InlineData(175,450)]
        [InlineData(99,450)]
        public void Get_Range_Rank_Test(short first, short last)
        {
            var api = new BoardGameRank();

            var result = api.GetRank((short)first, (short)last);

            Assert.Equal(first, result.First().Rank);

          //  Assert.Equal(last, result.Last().Rank);

        }
    }
}
