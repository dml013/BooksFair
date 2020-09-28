using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using BooksFair.Utility;
using Dapper;
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

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            coverType = _unitOfWork.SPCall.OneRecord<CoverType>(SD.ProcCoverTypeGet, parameter);

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

            var parameter = new DynamicParameters();
            parameter.Add("@Name", coverType.Name);

            if ( coverType.Id == 0 ) {
                _unitOfWork.SPCall.Execute(SD.ProcCoverTypeCreate, parameter);
            } else {
                parameter.Add("@Id", coverType.Id);
                _unitOfWork.SPCall.Execute(SD.ProcCoverTypeUpdate, parameter);
            }

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            var allObj = _unitOfWork.SPCall.List<CoverType>(SD.ProcCoverTypeGetAll);
            return Ok(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            if ( id == 0 )
                return NotFound(new { success = false, message = "Id can't be null" });

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            object test = new { id }; //test anonymous type

            var objFromDb = _unitOfWork.SPCall.OneRecord<CoverType>(SD.ProcCoverTypeGet, parameter);

            if ( objFromDb == null )
                return NotFound(new { success = false, message = "Object for delete doesn't found" });

            _unitOfWork.SPCall.Execute(SD.ProcCoverTypeDelete, test);

            return Ok(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
