using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{//business da sadece arayüzden ihtiyacın olan şeyleri gönderebilirsin
    public class ProductManager : IProductService
    {
        IProductDal _productDal; // soyut nesneyle bağlantı kurduk


        //productmanager new lendiğinde constructor bana bir tane IProductDal referansı ver(InMemeory olabilir, yarın entityFramework Olabilir VB.) 
        public ProductManager(IProductDal productDal)//injection  
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //iş kodları
            //If - yetkiisi  var mı vb kurallardan geçiiriyor
            return _productDal.GetAll();

            
          
            //iş kodlarını geçiyorsa veri erişimini çağırmam lazım
            //InMemoryProductDal inMemoryProductDal = new InMemoryProductDal(); Böyle yaparsan senin tüm kodların bellekte çalışır.
            //Veritabanına geçeceğin zaman bunun gibi(getAll) projede hepsini değiştirmen gerekir 
            //İş sınıfı başka sınıfı new leyemez




        }

        public List<Product> GetAllByCategoryId(int id)//filtrelendi
        {
            return _productDal.GetAll(p => p.CategoryId == id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDTO> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
