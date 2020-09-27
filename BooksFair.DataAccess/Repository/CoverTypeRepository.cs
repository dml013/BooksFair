using System;
using System.Collections.Generic;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksFair.DataAccess.Repository {
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository {
        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db){
            _db = db;
        }

        public void Update(CoverType coverType) {
            var objFromDb = Get(coverType.Id);

            if(objFromDb == null ) {
                throw new ArgumentNullException("object not found");
            }

            objFromDb.Name = coverType.Name;
        }
    }
}
