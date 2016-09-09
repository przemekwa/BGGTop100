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
                api.GetRank(1, 200).Select(b => new BoardGameModel {Name = b.Name, Rank = b.Rank, Year = b.Year, ImgUrl = b.ImageUrl, GameUrl = b.GameUrl});
            
            return View(result);
        }
    }
}