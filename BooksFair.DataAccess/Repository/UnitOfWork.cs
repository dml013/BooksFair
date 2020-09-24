using System;
using System.Collections.Generic;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;

namespace BooksFair.DataAccess.Repository {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _db;
        private bool disposedValue;
              

        public UnitOfWork(ApplicationDbContext db) {
            _db = db;
            Category = new CategoryRepository(_db);
            SPCall = new SPCall(_db);
        }

        public ICategoryRepository Category  {get; private set;}

        public ISPCall SPCall { get; private set; }

        public void Save() {
            _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing) {
            if ( !disposedValue ) {
                if ( disposing ) {
                    _db.Dispose();
                    SPCall.Dispose();
                }              
                disposedValue = true;
            }
        }      

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
