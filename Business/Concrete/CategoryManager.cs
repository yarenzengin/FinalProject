using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    // Business veri erişime bağlı
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;//bağımlılığımızı minimize ediyoruz

        //ben categotyManager olarak veri erişim katmanına bağımlıyım ama zayıf bir bağımlılık var çünkü interface(ref.)üzerinden bağımlıyım o yüzden DataAccess katmanında istediğini
        //yapabilirsin yeter ki kuralllara uy
        public CategoryManager(ICategoryDal categoryDal)//bağımlılığımı const. ile yaptım , injection yaptık
        {
            _categoryDal = categoryDal;
        }
        
        public List<Category> GetAll()
        {
            //iş kodları
            return _categoryDal.GetAll();
        }

        //select * from Categories where CategoryId = 3
        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(c => c.CategoryId == categoryId);
        }
    }
}
