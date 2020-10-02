using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository {

    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }


    }
}
