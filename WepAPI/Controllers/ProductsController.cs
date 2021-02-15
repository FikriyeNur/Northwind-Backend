﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")] // contoller'a istek gönderirken bunu nasıl yapacağımızı söyler.
    [ApiController] // Attribute --Bir class ile ilgili bilgi verme yöntemidir.
    public class ProductsController : ControllerBase
    {
        // Bir katman bir katmanın somut class'ına bağımlılık kuramaz!! Her zaman soyuta bağımlılık olmalı.

        // Loosely Coupled -- Gevşek bağımlılık. Soyut nesneye bağımlılık var.

        // C# da constructor'a verdiğimiz değeri aşağılarda kullanamayız! (kısaca productSevice'i aşağı'da çağıramayız.)
        // Bunun için IoC Container denilen yapıyı oluştururuz. (Inversion of Controlller -- IoC)

        IProductService _productService;

        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // Dependency Chain -- Bağımlılık zinciri oldu! 
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
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
