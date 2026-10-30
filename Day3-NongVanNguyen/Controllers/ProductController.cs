using Day3_NongVanNguyen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day3_NongVanNguyen.Controllers
{
    public class ProductController : Controller
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Product 1", Price = 100000, CategoryId = 1 },
            new Product { Id = 2, Name = "Product 2", Price = 200000, CategoryId = 2 },
            new Product { Id = 3, Name = "Product 3", Price = 300000, CategoryId = 3 },
            
        };

        [HttpGet]
        public ActionResult Index()
        {
            foreach (var product in _products) LoadCategory(product);
            return View(_products);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadCategories();
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm, Bind("Name,Price,Description,CategoryId")] Product product)
        {
            ValidateCategory(product);
            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }
            product.Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
            LoadCategory(product);
            _products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategories(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id,
            [FromForm, Bind("Id,Name,Price,Description,CategoryId")] Product product)
        {
            if (id != product.Id) return BadRequest();
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return NotFound();
            ValidateCategory(product);
            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            LoadCategory(existingProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([FromRoute] int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(CategoryData.Items, "Id", "Name", selectedId);
        }

        private void ValidateCategory(Product product)
        {
            if (!CategoryData.Items.Any(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Vui lòng chọn danh mục đang tồn tại.");
        }

        private static void LoadCategory(Product product)
        {
            product.Category = CategoryData.Items.FirstOrDefault(c => c.Id == product.CategoryId);
        }
    }
}
