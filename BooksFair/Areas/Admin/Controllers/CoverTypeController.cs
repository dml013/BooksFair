using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksFair.Areas.Admin.Controllers {
    [Area("Admin")]
    public class CoverTypeController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Upsert(int? id) {
            var coverType = new CoverType();
            if ( id == null )
                return View(coverType);

            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            if ( coverType == null )
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType) {
            if ( !ModelState.IsValid ) {
                return View(coverType);
            }

            if ( coverType.Id == 0 )
                _unitOfWork.CoverType.Add(coverType);
            else
                _unitOfWork.CoverType.Update(coverType);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            var allObj = _unitOfWork.CoverType.GetAll();
            return Ok(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            if ( id == 0 )
                return NotFound(new { success = false, message = "Id can't be null" });

            var objFromDb = _unitOfWork.CoverType.Get(id);

            if ( objFromDb == null )
                return NotFound(new { success = false, message = "Object for delete doesn't found" });

            _unitOfWork.CoverType.Remove(objFromDb);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
