using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    //bellek üzerinde veri erişim kodlarının yazılacağı yer
    {
        //global değişken 
        List<Product> _products;//vt
        
        //veri varmış gibi düşünüyoruz 
        public InMemoryProductDal()//bellekte referans aldığı zaman çalışacak olan blok
        {//new lendiği zaman, projeyi çalıştırdığım zaman bellekte ürün oluşturdu
            //Oracle,Sql Server ,Postgres,MongoDb den geliyormuş gibi simüle ediyoruz
            _products = new List<Product> {
            new Product{ProductId = 1, CategoryId =1, ProductName= "Bardak", UnitPrice = 15, UnitsInStock = 15 },
            new Product{ProductId = 2, CategoryId =1, ProductName= "Kamera", UnitPrice = 500, UnitsInStock = 3 },
            new Product{ProductId = 3, CategoryId =2, ProductName= "Telefon", UnitPrice = 1500, UnitsInStock = 2 },
            new Product{ProductId = 4, CategoryId =2, ProductName= "Klavye", UnitPrice = 150, UnitsInStock = 65 },
            new Product{ProductId = 5, CategoryId =2, ProductName= "Fare", UnitPrice = 85, UnitsInStock = 1 }


            };
                
        }
        //businessten aldık vt ye ekledik
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Normalde bir listeden elemanı böyle siliyoruz ama burada çalışmaz çünkü  HEAP te  bir adres tutuyor sen bir ürün new leyip gönderdiğinde adres aynı kalmıyor 
            //: _products.Remove(product);Referans tipi silemezsin böyle


            //Primary Key önemli
            //Product productToDelete = null;
            //foreach (var p in _products) // listeyi tek tek dolaştım
            //{
            //    if (product.ProductId == p.ProductId)//şart koyduk
            //    {
            //        productToDelete = p;
            //        //ref no atadık
            //    }
            //}

            //ıd bazlı yapılarda single or default kullanabilirz
            //Lambda p=> her P için demek
                                        //foreach yaptı         //takma isim-------- Kuralını yaz
             Product productToDelete = _products.SingleOrDefault(p=> p.ProductId == product.ProductId);//tek bir eleman bulmayı sağlar


            _products.Remove(productToDelete);

        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()//business ürün listesi
        {
            return _products;//vt nin tümünü döndür
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //where içindeki şarta uyan bütün elemanları yeni bir liste haline getirir ve onu döndürür
          return  _products.Where(p => p.CategoryId == categoryId).ToList()
                ;
        }

        public List<ProductDetailDTO> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)//ekrandan gelen data
        {
            //gönderdiğim ürün id 'sine sahip olan listedeki ürünü bul
            //veri kaynağında güncellenecek referansı bulmam lazım
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;//ekrandan gelen data ama ref nosunu getirdiğim için güncellendi demektir
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            


        }  
    }
}
