using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RouteModel;

namespace Route_MGMT.Controllers
{
    public class LineStylesController : Controller
    {
        private readonly Routes _context;

        public LineStylesController(Routes context)
        {
            _context = context;
        }

        // GET: LineStyles

        public IActionResult Index()

        {
            var routeContext = _context.Styles;
            return View(routeContext);
        }

        // GET: LineStyles/Details/5

        public IActionResult Details(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var lineStyle = _context.Styles
                .FirstOrDefault(m => m.Id == id);
            if (lineStyle == null)
            {
                return NotFound();
            }

            return View(lineStyle);
        }

        // GET: LineStyles/Create
        public IActionResult Create()
        {
            SetViewData(null);
            return View();
        }

        // POST: LineStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,StyleMapId,Width,Style")] LineStyle lineStyle)
        {
            if (ModelState.IsValid)
            {
                lineStyle.StyleMap = _context.StyleMaps.FirstOrDefault(x => x.Id == lineStyle.StyleMapId);
                
                lineStyle.Id = _context.Styles.Count();
                _context.Styles.Add(lineStyle);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            SetViewData(lineStyle.StyleMapId);
            return View(lineStyle);
        }

        // GET: LineStyles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineStyle = _context.Styles.FirstOrDefault(x => x.Id == id);
            if (lineStyle == null)
            {
                return NotFound();
            }
            SetViewData(lineStyle.StyleMapId);
            return View(lineStyle);
        }

        // POST: LineStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,StyleMapId,Width,Style")] LineStyle lineStyle)
        {
            if (id != lineStyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                lineStyle.StyleMap = _context.StyleMaps.FirstOrDefault(x => x.Id == lineStyle.StyleMapId);
                _context.Update(lineStyle);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            SetViewData(lineStyle.StyleMapId);
            //ViewData["StyleMapId"] = new SelectList(_context.StyleMaps, "Id", "StyleMapName", lineStyle.StyleMapId);
            return View(lineStyle);
        }

        // GET: LineStyles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineStyle = _context.Styles
                .FirstOrDefault(m => m.Id == id);
            if (lineStyle == null)
            {
                return NotFound();
            }

            return View(lineStyle);
        }

        // POST: LineStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lineStyle = _context.Styles.FirstOrDefault(x => x.Id == id);
            _context.Styles.Remove(lineStyle);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LineStyleExists(int id)
        {
            return _context.Styles.Any(e => e.Id == id);
        }
        private void SetViewData( int? StyleMapId)
        {
            ViewData["StyleMapId"] = new SelectList(_context.StyleMaps.OrderBy(r => r.Id), "Id", "Name", StyleMapId);
        }
    }
}
