﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
         //validation : eklemek istediğin nesnenin yapısal olark doğru olup olmadığını kontrol ediyor , Business code 'u  ile karıştıtılmamalı 
        
        
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            //business codes : iş ihtiyaçlarımıza uygunluk 
          
            _productDal.Add(product);

            // bunu yapabilmek için ctor eklenmeli
            return new SuccessResult(Messages.ProductAdded);//IResultı impelemente ettiği için tutabiliyor

        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //If - yetkiisi  var mı vb kurallardan geçiiriyor
            if (DateTime.Now.Hour == 22)
            {
                //sadece mesj döndürüyorum
                //default ı = null,ürün listemiz null ,neden? frontendci ona göre karşılicak
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);//datayı döndürmüşüm sistem diyor ki List döndürmen normal ama onun için bir data döndürmen lazım

            
          
            //iş kodlarını geçiyorsa veri erişimini çağırmam lazım
            //InMemoryProductDal inMemoryProductDal = new InMemoryProductDal(); Böyle yaparsan senin tüm kodların bellekte çalışır.
            //Veritabanına geçeceğin zaman bunun gibi(getAll) projede hepsini değiştirmen gerekir 
            //İş sınıfı başka sınıfı new leyemez




        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)//filtrelendi
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p=> p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDTO>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
        }
    }
}
