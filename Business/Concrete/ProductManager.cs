using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;

        public ProductManager(IProductDal productDal)
        {
            _ProductDal = productDal;
        }

        public List<Product> GetAll()
        {
            // iş kodları ..
            // yetkisi var mı ?
            // bütün kurallara uyduktan sonra listeyi ekrana döndürebiliriz.
            return _ProductDal.GettAll();
        }
    }
}
