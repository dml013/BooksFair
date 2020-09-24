using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository {

    public class CategoryRepository : Repository<Category>, ICategoryRepository {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(Category category) {
            var objFromDb = Get(category.Id);

            if ( objFromDb == null ) {
                throw new ArgumentNullException("object not found");
            }

            objFromDb.Name = category.Name;            
        }
    }
}
