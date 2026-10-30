using Day3_NongVanNguyen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day3_NongVanNguyen.Controllers
{
    public class CategoryController : Controller
    {
        // gia lap csdl
        private static readonly List<Category> _categories = CategoryData.Items;
        // GET: CategoryController
        public ActionResult Index()
        {
            return View(_categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController      /Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                // Sinh ID mới; nếu danh sách rỗng thì bắt đầu từ 1
                category.Id = _categories.Count == 0
                    ? 1
                    : _categories.Max(c => c.Id) + 1;

                // Thêm danh mục vào danh sách
                _categories.Add(category);

                // Chuyển về trang danh sách
                return RedirectToAction(nameof(Index));
            }

            // Dữ liệu không hợp lệ: hiển thị lại form cùng dữ liệu đã nhập
            return View(category);
        }
        // GET: CategoryController1/Edit/5
        public ActionResult Edit([FromRoute] int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, [FromForm] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                var existingCategory = _categories.FirstOrDefault(c => c.Id == id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.Name = category.Name;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Category/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([FromRoute] int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            // Xóa danh mục sau khi người dùng xác nhận.
            _categories.Remove(category);
            return RedirectToAction(nameof(Index));
        }
    }
}

