using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;

namespace A02_Repos
{
    public interface ICategoriesRepo
    {
        public List<Category> getAllCategories();


        public Category GetCategoryId(short id);

        public void AddCategory(Category a);

        public void UpdateCategory(Category a);

        public void DeleteCategory(short id);

        public List<object> GetCategoryList();
    }
}
