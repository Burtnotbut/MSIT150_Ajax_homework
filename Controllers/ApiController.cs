using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MSIT150Site.Models;

namespace MSIT150Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;


        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;

        }

        public IActionResult Index()
        {
            /*
            System.Threading.Thread.Sleep(5000); 
            //睡5秒鐘，再往下執行
            */

            return Content("Hello fetch!!");
            //Content()是用來回傳字串
        }

        public IActionResult GetDemo(UserViewModel user) 
        {
            if ( string.IsNullOrEmpty(user.name) )
            {
                user.name = "guest";
            }

            return Content($"Hello {user.name}, you are {user.age} years old.");
            //Content()是用來回傳字串
        }

        public IActionResult Register(Members member, IFormFile file) 
        {
          //_context.Members.Add(member);
          //_context.SaveChanges();

          //  return Content("新增成功");


            string filePath = Path.Combine(_host.WebRootPath, "uploads", file.FileName);
            //C:\shared\Ajax\MSIT150Site\wwwroot\uploads\walk.gif

            //上傳檔案到uploads資料夾中
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            //將圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }

            //return Content($"上傳檔案到 {filePath}");

            member.FileName = file.FileName;
            member.FileData = imgByte;
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功!!");
        }

        //作業三 檢查帳號是否存在的CheckAccount Action
        public IActionResult CheckAccount(Members member)
        {
            string abc = member.Name;

            var answer = _context.Members.Where(a => a.Name == abc);
            if ( answer.Any())
            {
                return Content("此帳戶已被使用");
            }
            else
            {
                return Content("此帳戶尚未註冊");
            }

        }

        //拿到照片回傳的方法
        public IActionResult GetImageByte(int id = 1)
        {
            Members? member = _context.Members.Find(id);
            byte[]? img = member.FileData;
            return File(img, "image/jpeg");

        }

        //回傳城市的JSON資料
        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            //去DemoContext找到Address資料表，撈取Select所有城市City欄位
            //用Distinct()去除掉重複的部分

            /*
            var cities = _context.Address.Select(c => new
            {
                c.City
            }).Distinct();
            */

            return Json(cities);
            //回傳Json格式
        }

        //根據城市名稱，回傳城市的鄉鎮區JSON資料
        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(c => c.SiteId).Distinct();
            //去DemoContext找到Address資料表，撈取Select所有城市City欄位
            //用Distinct()去除掉重複的部分

            return Json(districts);
            //回傳Json格式
        }

        //根據鄉鎮區名稱，回傳鄉鎮區的路名JSON資料
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(c => c.Road).Distinct();

            return Json(roads);
            //回傳Json格式
        }

    }
}
