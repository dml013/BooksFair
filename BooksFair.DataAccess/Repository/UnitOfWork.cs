using System;
using System.Collections.Generic;
using System.Text;
using BooksFair.DataAccess.Data;
using BooksFair.DataAccess.Repository.IRepository;
using BooksFair.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksFair.DataAccess.Repository {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _db;
        private bool disposedValue;
              
        public UnitOfWork(ApplicationDbContext db) {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            SPCall = new SPCall(_db);
        }
        public ISPCall SPCall { get; private set; }

        public ICategoryRepository Category  {get; private set;}

        public ICoverTypeRepository CoverType { get; private set; }

        public void Save() {
            try {
                _db.SaveChanges();
            } catch ( DbUpdateException e ) {
                Console.WriteLine(e.Message);                
            }            
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
