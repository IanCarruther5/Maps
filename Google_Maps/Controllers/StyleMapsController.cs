using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouteModel;

namespace Route_MGMT.Controllers
{
    public class StyleMapsController : Controller
    {
        private readonly Routes _context;

        public StyleMapsController(Routes context)
        {
            _context = context;
        }

        // GET: StyleMaps
        public  ActionResult Index()
        {
            return View( _context.StyleMaps);
        }

        // GET: StyleMaps/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styleMap =  _context.StyleMaps
                .FirstOrDefault(m => m.Id == id);
            if (styleMap == null)
            {
                return NotFound();
            }

            return View(styleMap);
        }

        // GET: StyleMaps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StyleMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Color,Name,DocumentId")] StyleMap styleMap)
        {
            if (ModelState.IsValid)
            {
                styleMap.Id = _context.StyleMaps.Count();
                _context.StyleMaps.Add(styleMap);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(styleMap);
        }

        // GET: StyleMaps/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styleMap =  _context.StyleMaps.FirstOrDefault(x=>x.Id==id);
            if (styleMap == null)
            {
                return NotFound();
            }
            return View(styleMap);
        }

        // POST: StyleMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Color,Name,DocumentId")] StyleMap styleMap)
        {
            if (id != styleMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    _context.Update(styleMap);
                    _context.SaveChanges();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(styleMap);
        }

        // GET: StyleMaps/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styleMap =  _context.StyleMaps
                .FirstOrDefault(m => m.Id == id);
            if (styleMap == null)
            {
                return NotFound();
            }

            return View(styleMap);
        }

        // POST: StyleMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var styleMap =  _context.StyleMaps.FirstOrDefault(x=>x.Id==id);
            _context.StyleMaps.Remove(styleMap);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool StyleMapExists(int id)
        {
            return _context.StyleMaps.Any(e => e.Id == id);
        }
    }
}
