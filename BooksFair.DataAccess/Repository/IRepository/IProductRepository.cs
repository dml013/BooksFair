using System;
using System.Collections.Generic;
using System.Text;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository.IRepository {
    public interface IProductRepository: IRepository<Product> {
        void Update(Product product);
    }
}
