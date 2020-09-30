using System;
using System.Collections.Generic;
using System.Text;

namespace BooksFair.DataAccess.Repository.IRepository {
    public interface IUnitOfWork : IDisposable {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ISPCall SPCall { get; }

        void Save();
    }
}
