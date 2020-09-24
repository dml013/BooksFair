using System;
using System.Collections.Generic;
using System.Text;
using BooksFair.Models;

namespace BooksFair.DataAccess.Repository.IRepository {
    public interface ICategoryRepository: IRepository<Category> {
        void Update(Category category);
    }
}
