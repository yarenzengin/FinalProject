using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    // iş katmanında kullanacağımız servis operasyonları
  public  interface IProductService
    {               //<T> işlem sonucu ve mesajı da döndürmek istiyorum
        IDataResult<List<Product>> GetAll();//datayı nasıl döndürcez?
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);//şu fiyat aralığında olanları getir

        IDataResult<Product> GetById(int productId);
        IDataResult<List<ProductDetailDTO>> GetProductDetails();
        IResult Add(Product product);


    }
}
