using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameGeekApi;
using BoardGameRankExplorer.Models;

namespace BoardGameRankExplorer.Controllers
{
    public class ExploreController : Controller
    {
        // GET: Explore
        public ActionResult Index()
        {
            var api = new BoardGameRank();

            var result =
                api.GetRank(1, 100).Select(b => new BoardGameModel
                {
                    Name = b.Name,
                    Rank = b.Rank,
                    Year = b.Year,
                    ImgUrl = b.ImageUrl,
                    GameUrl = b.GameUrl
                });
            
            return View(result);
        }

        public ActionResult Range(string first, string last)
        {
            if (string.IsNullOrEmpty(first))
            {
                first = "1";
            }

            if (string.IsNullOrEmpty(last))
            {
                last = "100";
            }

            var api = new BoardGameRank();

            var result =
                api.GetRank(short.Parse(first), short.Parse(last)).Select(b => new BoardGameModel
                {
                    Name = b.Name,
                    Rank = b.Rank,
                    Year = b.Year,
                    ImgUrl = b.ImageUrl,
                    GameUrl = b.GameUrl
                });
            
            return View("Index", result);
        }


    }
}