using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Product.Data;
using Product.Models;

namespace Product.Controllers
{
    public class ProducttsController : Controller
    {
        private AppDbContext db;
        public ProducttsController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index(int? id)
        {
            if (id != null && id != -1)
            {
                ViewBag.categoriesFilter = new SelectList(db.Categories, "CategoryId", "Name");
                return View(db.Products.Where(x => x.CategoryId == id).OrderBy(x => x.ProducttId));
            }
            ViewBag.categoriesFilter = new SelectList(db.Categories, "CategoryId", "Name");
            return View(db.Products.OrderBy(x=>x.ProducttId));

        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.allCategories = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Productt product)
        {
            if(product.CategoryId != -1)
            {
                product.IsActive = true;
                db.Products.Add(product);
                db.SaveChanges();
            }
            

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var data = db.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Productt product)
        {
            var data = db.Products.Find(product.ProducttId);
            if (data != null)
            {
                db.Products.Remove(data);
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
            var prodData = db.Products.Find(id);
            if (prodData != null)
            {
                ViewBag.allCategories = new SelectList(db.Categories, "CategoryId", "Name");
                return View(prodData);

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Edit(Productt product)
        {
            if(product.CategoryId != -1)
            {
                db.Products.Update(product);
                db.SaveChanges();
            }
            

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var prodData = db.Products.Find(id);
            if (prodData != null)
            {
                return View(prodData);

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult categoriesFilterSelected(int id)
        {
            return RedirectToAction(nameof(Index),new { id = id });
        }

        [HttpPost]
        //public IActionResult searchData(string str)
        //{
        //    return RedirectToAction
        //}
    }
}
