using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WandaWebAdmin.Models;
using WandaWebAdmin.Models.ViewModels;
using WandaWebAdmin.Services;
using WandaWebAdmin.Services.Contracts;

namespace WandaWebAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVimeoService _vimeoService;
        private readonly IEmailService _emailService;
        public HomeController(IVimeoService vimeoService, IEmailService emailService)
        {
            _vimeoService = vimeoService;
            _emailService = emailService;
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

        [HttpGet]
        public IActionResult Prayer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prayer(PrayerRequestViewModel model)
        {
            if (model.IsValid)
            {
                model.Body = $"<p>Sender: {model.From}</p>" + model.Body;
                _emailService.SendEmail(model.Subject, model.Body, EmailAddresses.Prayer);
            }
            return View(model);
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
            ViewData["Message"] = "Pour nous contacter:";

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
