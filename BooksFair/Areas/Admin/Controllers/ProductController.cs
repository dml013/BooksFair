using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BooksFair.DataAccess.Repository;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using BooksFair.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;


namespace BooksFair.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ProductController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment; //for upload images

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Upsert(int? id) {
            ProductVM productVM = new ProductVM() {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem {
                    Text = c.Name, //convert item to option in HTML
                    Value = c.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem {
                    Text = c.Name, //convert item to option in HTML
                    Value = c.Id.ToString()
                })
            };

            if ( id == null ) {
                productVM.Product.ImageUrl = "empty";
                return View(productVM);
            }

            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());

            if ( productVM.Product == null )
                return NotFound();
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM) {
            if ( !ModelState.IsValid ) {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem {
                    Text = c.Name, //convert item to option in HTML
                    Value = c.Id.ToString()
                });
                productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem {
                    Text = c.Name, //convert item to option in HTML
                    Value = c.Id.ToString()
                });
                return View(productVM);
            }

            string webRootPath = _hostEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if ( files.Count > 0 ) {
                string fileName = Guid.NewGuid().ToString(); //Globally Unique Identifier
                var uploads = Path.Combine(webRootPath, @"images\products\");
                var extenstion = Path.GetExtension(files[0].FileName);

                if ( productVM.Product.ImageUrl != "empty" ) { //don't fulling with file attach
                    //this is an edit and we need to remove old image
                    var objFromDb = _unitOfWork.Product.Get(productVM.Product.Id); //to protect from fake file path

                    DeleteIMGfromProduct(objFromDb);
                }

                using ( var filesStream = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create) ) {
                    files[0].CopyTo(filesStream);
                }

                productVM.Product.ImageUrl = @"\images\products\" + fileName + extenstion;

            } else if ( productVM.Product.Id != 0 ) {
                var objFromDb = _unitOfWork.Product.Get(productVM.Product.Id);

                productVM.Product.ImageUrl = objFromDb.ImageUrl; //to protect from fake file path
            }

            if ( productVM.Product.Id == 0 )
                _unitOfWork.Product.Add(productVM.Product);
            else
                _unitOfWork.Product.Update(productVM.Product);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        // GET: Admin/Product/GetAll
        [HttpGet]
        public IActionResult GetAll() {
            var allObj = _unitOfWork.Product.GetAll(includeProperties: $"{nameof(Category)},{nameof(CoverType)}");
            return Ok(new { data = allObj });
        }

        // POST: Admin/Product/Delete
        [HttpDelete]
        public IActionResult Delete(int id) {
            if ( id == 0 )
                return NotFound(new { success = false, message = "Id can't be null" });

            var objFromDb = _unitOfWork.Product.Get(id);

            if ( objFromDb == null ) {
                return NotFound(new { success = false, message = "Object for delete doesn't found" });
            }

            DeleteIMGfromProduct(objFromDb);

            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Delete Successful" });
        }

        #endregion

        private void DeleteIMGfromProduct(Product product) {
            var imagePath = Path.Combine(_hostEnviroment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if ( System.IO.File.Exists(imagePath) ) {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
