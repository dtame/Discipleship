using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
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
            var album = _vimeoService.GetAlbumsWithVideos()[1];
            return View(album);
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
                _emailService.SendEmail(model.Subject, model.Body, model.From, EmailAddresses.Infos);
            }
            return View(model);
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

        [EnableCors("AllowMyOrigin")]
        public IActionResult Play(string code)
        {
            ViewData["Code"] = code;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
