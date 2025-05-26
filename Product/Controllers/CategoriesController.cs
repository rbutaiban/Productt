using Microsoft.AspNetCore.Mvc;
using Product.Data;
using Product.Models;

namespace Product.Controllers
{
    
    public class CategoriesController : Controller
    {
        private AppDbContext db;
        public CategoriesController(AppDbContext _db)
        {
            db = _db;
        }
        [Route("Productts/Categories")]
        [Route("Categories")]
        public IActionResult Index()
        {
            return View(db.Categories.OrderBy(x => x.CategoryId));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var data = db.Categories.Find(id);
            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Category category)
        {
            var data = db.Categories.Find(category.CategoryId);
            if (data != null)
            {
                db.Categories.Remove(data);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var catData = db.Categories.Find(id);
            if (catData != null)
            {
                return View(catData);

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
