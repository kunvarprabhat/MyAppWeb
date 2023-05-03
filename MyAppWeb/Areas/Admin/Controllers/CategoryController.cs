using Microsoft.AspNetCore.Mvc;

using MyApp.Models;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccessLayer;
using MyApp.DataAccessLayer.Infrastructure.IRepository;

namespace MyAppWeb.Areas.Admin.Controllers
{
      //[Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofWork;
           
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitofWork.Category.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            // ServerSideValidation
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Add(category);
                _unitofWork.Save();
                //tempdata- single request mai work karta hai
                TempData["success"] = "Category Created Done!";
                return RedirectToAction("Index");
            }
            return View(category);

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofWork.Category.GetT(x => x.id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            // ServerSideValidation
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Update(category);
                _unitofWork.Save();
                TempData["success"] = "Category Update Done!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }




        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofWork.Category.GetT(x => x.id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            // Delete 
            var category = _unitofWork.Category.GetT(x => x.id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitofWork.Category.Delete(category);
            _unitofWork.Save();
            TempData["success"] = "Category Deleted Done!";
            return RedirectToAction("Index");

        }


    }
}
