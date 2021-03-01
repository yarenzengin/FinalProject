using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interface in operasyonları public
    //IEntityRepository i Product için yapılandırdın
    public interface IProductDal: IEntityRepository<Product>
        //productla ilgili vt de yapacağım operasyonları  içeren interface
    {
        List<ProductDetailDTO> GetProductDetails();

      //ürünleri kategoriye göre filtrele
    }
}
//code refactoring