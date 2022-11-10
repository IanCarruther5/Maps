using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using RouteModel;

namespace Google_Maps.Controllers
{
    public class DocumentController : Controller
    {
        private readonly Routes _context;

        public DocumentController(Routes context)
        {
            _context = context;
        }

        // GET: Documents
        public IActionResult Index()
        {
            return View(_context.Documents.OrderBy(x => x.Name));
        }

        // GET: Documents/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents

                .FirstOrDefault(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {

            SetViewData(null, null, null);
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description, SelectedPlacemarks, SelectedLineStyles, SelectedStyleMaps")] Document document)
        {
            if (ModelState.IsValid)
            {
                if (document.SelectedLineStyles != null)
                    foreach (int item in document.SelectedLineStyles)
                    {
                        document.DocumentLineStyles.Add(_context.Styles.FirstOrDefault(x => x.Id == item));
                    }
                //document.DocumentLineStyles.AddRange(_context.Styles.Where(document.SelectedLineStyles.Contains(x => x.Id)));
                    
                if (document.SelectedPlacemarks != null)
                    foreach (int item in document.SelectedPlacemarks)
                    {
                        document.DocumentPlacemarks.Add(_context.Placemarks.FirstOrDefault(x => x.Id == item));
                    }
                if (document.SelectedStyleMaps != null)
                    foreach (int item in document.SelectedStyleMaps)
                    {
                        document.DocumentStyleMaps.Add(_context.StyleMaps.FirstOrDefault(x => x.Id == item));
                    }

                document.Id = _context.Documents.Count();
                _context.Documents.Add(document);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            SetViewData(document.SelectedLineStyles, document.SelectedStyleMaps, document.SelectedPlacemarks);
            return View(document);
        }

        // GET: Documents/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document =  _context.Documents.FirstOrDefault(x => x.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            document.SelectedPlacemarks = document.DocumentPlacemarks.Select(x => x.Id).ToArray();
            document.SelectedLineStyles = document.DocumentLineStyles.Select(x => x.Id).ToArray();
            document.SelectedStyleMaps = document.DocumentStyleMaps.Select(x => x.Id).ToArray();
            SetViewData(document.SelectedLineStyles, document.SelectedStyleMaps, document.SelectedPlacemarks);

            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description, SelectedPlacemarks, SelectedLineStyles, SelectedStyleMaps")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (document.SelectedLineStyles != null)
                    foreach (int item in document.SelectedLineStyles)
                    {
                        document.DocumentLineStyles.Add(_context.Styles.FirstOrDefault(x => x.Id == item));
                    }
                //document.DocumentLineStyles.AddRange(_context.Styles.Where(document.SelectedLineStyles.Contains(x => x.Id)));

                if (document.SelectedPlacemarks != null)
                    foreach (int item in document.SelectedPlacemarks)
                    {
                        document.DocumentPlacemarks.Add(_context.Placemarks.FirstOrDefault(x => x.Id == item));
                    }
                if (document.SelectedStyleMaps != null)
                    foreach (int item in document.SelectedStyleMaps)
                    {
                        document.DocumentStyleMaps.Add(_context.StyleMaps.FirstOrDefault(x => x.Id == item));
                    }

                _context.Update(document);
                     _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents
                .FirstOrDefault(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var document =  _context.Documents.FirstOrDefault(x=>x.Id==id);
            _context.Documents.Remove(document);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }

        private void SetViewData(int[] LineStyleId, int[] StyleMapId, int[] PlacemarkId)
        {
            ViewData["SelectedLineStyles"] = new SelectList(_context.Styles.OrderBy(r => r.Id), "Id", "LineStyleName", LineStyleId);
            ViewData["SelectedStyleMaps"] = new SelectList(_context.StyleMaps.OrderBy(r => r.Id), "Id", "Name", StyleMapId);
            ViewData["SelectedPlacemarks"] = new SelectList(_context.Placemarks.OrderBy(r => r.PlacemarkName), "Id", "PlacemarkName", PlacemarkId);
        }
    }
}
