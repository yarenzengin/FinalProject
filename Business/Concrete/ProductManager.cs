using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{//business da sadece arayüzden ihtiyacın olan şeyleri gönderebilirsin
    public class ProductManager : IProductService
    {
        IProductDal _productDal; // soyut nesneyle bağlantı kurduk
        ICategoryService _categoryService;

        //productmanager new lendiğinde constructor bana bir tane IProductDal referansı ver(InMemeory olabilir, yarın entityFramework Olabilir VB.) 
        public ProductManager(IProductDal productDal, ICategoryService categoryService)//injection  
        {
            _productDal = productDal;
            _categoryService = categoryService;
           
        }
        //validation : eklemek istediğin nesnenin yapısal olark doğru olup olmadığını kontrol ediyor , Business code 'u  ile karıştıtılmamalı 

        //claim : iddia etmek , bu kullanıcının admin veya pro.add claimlerinden birine sahip olm.gerekiyor
       
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes : iş ihtiyaçlarımıza uygunluk 
            //iş motoru
            //hatalı kural geldi diyelim  tek bir result döner o yüzden resulta attık ,ya null ya da doludur
          IResult result =   BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
               CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());

            if (result!=null)//result kurala uymayan bir durum oluşmuşsa
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
           

          




        }
        [CacheAspect]//key,value
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
        [CacheAspect]
        [PerformanceAspect(1)]//bu metotun çalışması 5 sn yi geçerse beni uyar
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

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]//ıpr.service içerisinde tüm getleri sil
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)//bunu yapabilmek için category i de göndermek gerekir
        {
            //1 kategoride max 10 ürün olabilir, önce kaç tane ürün olacağına bakmalıyız
          //select count(*) from products where categoryId = 1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        // aynı isimde ürün eklenemez
        private IResult CheckIfProductNameExists(string productName)
        {
            //any var  mı demek
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        //eğer bunu categorymanager da yazmış olsaydık bu tek başına bir servis demektir
        private IResult CheckIfCategoryLimitExceded()//Product için CategoryService nasıl yorumlanıyor
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 210)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
    }

