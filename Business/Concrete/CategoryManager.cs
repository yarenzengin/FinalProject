using Business.Abstract;
using Core.Utilities.Results;
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
        
        public IDataResult<List<Category>> GetAll()
        {
          
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
           
        }

        //select * from Categories where CategoryId = 3
        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
