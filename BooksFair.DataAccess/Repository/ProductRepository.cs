using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository {

    public class ProductRepository : Repository<Product>, IProductRepository {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(Product product) {
            var objFromDb = Get(product.Id);

            if ( objFromDb == null ) 
                throw new ArgumentNullException("object not found");

            if ( product.ImageUrl != "empty" )
                objFromDb.ImageUrl = product.ImageUrl;

            objFromDb.Title = product.Title;
            objFromDb.ISBN = product.ISBN;
            objFromDb.Description = product.Description;
            objFromDb.Author = product.Author;
            objFromDb.Price = product.Price;
            objFromDb.ListPrice = product.ListPrice;
            objFromDb.Price50 = product.Price50;
            objFromDb.Price100 = product.Price100;
            objFromDb.CoverTypeId = product.CoverTypeId;
            objFromDb.CategoryId = product.CategoryId;      
        }
    }
}
