using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //Hiçbir zaman bir katman diğer katmanın somutunu onlar dışında bağlantı kuramazsın 
    [Route("api/[controller]")] //Route:Bu isteği yaparken insanlar nasıl ulaşsın
    [ApiController]   // ATTRIBUTE - JAVA'DA ANNOTATION - Bir class ile ilgili bilgi verme onu imzalama
    public class ProductsController : ControllerBase
    {
        //naming convention
        //IoC Container -- Inversion of control -- değişimin kontrolü 
        IProductService _productService;//injection


        public ProductsController(IProductService productService)//sen bir IProductService bağımlısısın ama gevşek bit bağımlılık - Loosely coupled
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public  IActionResult GetAll()
        {//productservice i ctor da yazdığımız için erişemiyourz , field yapıyoruz 
            //Dependecy chain

            //swagger
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);//GetRequestlerde genelde 200 ile çalışırız
            }
            return BadRequest(result);
        
        }
        [HttpGet("getbyid")]//içine id alıcam diyebilirsin veya alians verebilirsin
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")] //data göndericez
        //PostRequestlerde : ben sana data vericem onu sistemine ekle 
        //Zarf ın body sine göndereceğimiz datayı ekliyoruz
        public IActionResult Add(Product product)//controllerın bildiği yer burası
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
    }
}
