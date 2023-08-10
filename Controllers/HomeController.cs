using Microsoft.AspNetCore.Mvc;
using MSIT150Site.Models;
using System.Diagnostics;

namespace MSIT150Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult First()
        {
            return View();
        }

        //旅遊景點介紹:作業1
        public IActionResult Travel()
        {
            return View();
        }

        public IActionResult GetDemo()
        {
            return View();
        }

        //註冊頁面
        public IActionResult Register()
        {
            return View();
        }

        //練習載入縣市資料的頁面
        public IActionResult Address()
        {
            return View();
        }

        //練習Promise物件的頁面
        public IActionResult Promise()
        {
            return View();
        }

        //練習fetch的頁面
        public IActionResult Fetch()
        {
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