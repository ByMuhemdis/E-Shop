
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessLayer.Concrate
{

    //burada miras alma  uygulayarak ekle sil güncelleyi urun tablosunda işleme alıyoruz
    public class ProductRepository:GenericRepository<Product>
    {
        DataContext db = new DataContext();
        public List<Product> GEtPopularProduct()
        {
            return db.products.Where(x=>x.Popular==true).Take(6).ToList();
        }
        
        
    }
}
