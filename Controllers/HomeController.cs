using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WandaWebAdmin.Data;
using WandaWebAdmin.Helpers;
using WandaWebAdmin.Models;
using WandaWebAdmin.Models.ViewModels;
using WandaWebAdmin.Services;

namespace WandaWebAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVimeoService _vimeoService;
        public HomeController(IVimeoService vimeoService)
        {
            _vimeoService = vimeoService;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Study()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Prayer()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult SyncVideos()
        //{

        //    return RedirectToAction("About");
        //}

        //[EnableCors("AllowMyOrigin")]
        //[HttpGet]
        //public JsonResult GetAlbums()
        //{
        //    var albums = _vimeoService.GetAlbumsWithVideos();
        //    return new JsonResult(albums.ToJson());
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Agenda()
        {
            ViewData["Message"] = "Your agenda page.";

            return View();
        }

        public IActionResult Donner()
        {
            ViewData["Message"] = "Your give page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
