using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository {

    public class CompanyRepository : Repository<Company>, ICompanyRepository {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(Company company) {
            var objFromDb = Get(company.Id);

            if ( objFromDb == null ) 
                throw new ArgumentNullException("object not found");         

            objFromDb.Name = company.Name;
            objFromDb.StreetAddress = company.StreetAddress;
            objFromDb.City = company.City;
            objFromDb.State = company.State;
            objFromDb.PostalCode = company.PostalCode;
            objFromDb.PhoneNumber = company.PhoneNumber;
            objFromDb.IsAuthorizedCompany = company.IsAuthorizedCompany;           
        }
    }
}
