using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksFair.DataAccess.Repository;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BooksFair.Areas.Admin.Controllers {
    [Area("Admin")]
    public class CategoryController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Upsert(int? id) {
            Category category = new Category();
            if ( id == null )
                return View(category);

            category = _unitOfWork.Category.Get(id.GetValueOrDefault());//protect from null

            if ( category == null )
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category) {
            if (!ModelState.IsValid) {
                return View(category);
            }

            if ( category.Id == 0 )
                _unitOfWork.Category.Add(category);
            else
                _unitOfWork.Category.Update(category);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            if(id == 0)
                return NotFound(new { success = false, message = "Id can't be null" });

            var objFromDb = _unitOfWork.Category.Get(id);

            if ( objFromDb == null ) {
                return NotFound(new { success = false, message = "Object for delete isn't found" });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
