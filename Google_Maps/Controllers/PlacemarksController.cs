using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RouteModel;

namespace Route_MGMT.Controllers
{
    public class PlacemarksController : Controller
    {
        private readonly Routes _context;

        public PlacemarksController(Routes context)
        {
            _context = context;
        }

        // GET: Placemarks
        public IActionResult Index()
        {
            var routeContext = _context.Placemarks.OrderBy(x => x.PlacemarkName);
            return View(routeContext);
        }

        // GET: Placemarks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placemark = _context.Placemarks
                .FirstOrDefault(m => m.Id == id);
            if (placemark == null)
            {
                return NotFound();
            }

            return View(placemark);
        }

        // GET: Placemarks/Create
        public IActionResult Create()
        {
            SetViewData(null);
            return View(new Placemark());
        }

        // POST: Placemarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,PlacemarkName,Description,StyleMapId,LineStringId,Tessellate,Coordinates")] Placemark placemark)
        {
            if (ModelState.IsValid)
            {
                placemark.Id = _context.Placemarks.Count();
                placemark.StyleMap = _context.StyleMaps.FirstOrDefault(x => x.Id == placemark.StyleMapId);
                _context.Placemarks.Add(placemark);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            SetViewData(placemark.StyleMapId);
            return View(placemark);
        }

        // GET: Placemarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placemark =  _context.Placemarks.FirstOrDefault(x=>x.Id==id);
            if (placemark == null)
            {
                return NotFound();
            }
            SetViewData(placemark.StyleMapId);
            return View(placemark);
        }

        // POST: Placemarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PlacemarkName,Description,StyleMapId,LineStringId,Tessellate,Coordinates")] Placemark placemark)
        {
            if (id != placemark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                placemark.StyleMap = _context.StyleMaps.FirstOrDefault(x => x.Id == placemark.StyleMapId);
                _context.Update(placemark);
                     _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            SetViewData(placemark.StyleMapId);
            //ViewData["StyleMapId"] = new SelectList(_context.StyleMaps, "Id", "StyleMapName", placemark.StyleMapId);
            return View(placemark);
        }

        // GET: Placemarks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placemark =  _context.Placemarks
                .FirstOrDefault(m => m.Id == id);
            if (placemark == null)
            {
                return NotFound();
            }

            return View(placemark);
        }

        // POST: Placemarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var placemark =  _context.Placemarks.FirstOrDefault(x=>x.Id==id);
            _context.Placemarks.Remove(placemark);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacemarkExists(int id)
        {
            return _context.Placemarks.Any(e => e.Id == id);
        }
        private void SetViewData(int? StyleMapId)
        {
            ViewData["StyleMapId"] = new SelectList(_context.StyleMaps.OrderBy(r => r.Id), "Id", "Name", StyleMapId);
        }
    }
}
