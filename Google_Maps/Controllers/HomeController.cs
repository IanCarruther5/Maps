using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google_Maps.Models;
using RouteModel;
using System.Text.Json;
using Model;

namespace Google_Maps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Routes _context;

        public HomeController(ILogger<HomeController> logger, Routes context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Import()
        {

            var json = System.IO.File.ReadAllText($"test/routes.json");

            var r = JsonSerializer.Deserialize<Import>(json);
            _context.Placemarks = new List<RouteModel.Placemark>();
            foreach (var item in r.Document.Folder.Placemark)
            {
                RouteModel.Placemark d = item;
                d.Id = _context.Placemarks.Count();
                var sm= _context.StyleMaps.FirstOrDefault(x => x.StyleMapName == item.styleUrl.Replace("#",""));
                d.StyleMap = sm;
                d.StyleMapId = sm?.Id??0;
                _context.Placemarks.Add(d);
            }
            _context.SaveChanges();
            var m = new HomeView { Message = JsonSerializer.Serialize(_context.Placemarks) };
            return View(m);
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
