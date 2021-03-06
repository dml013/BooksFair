﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BooksFair.Models.ViewModels;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;

namespace BooksFair.Areas.Customer.Controllers {
    [Area("Customer")]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: $"{nameof(Category)},{nameof(CoverType)}");
            return View(productList);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
