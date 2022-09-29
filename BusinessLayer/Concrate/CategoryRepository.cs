using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    //burada miras alma  uygulayarak ekle sil güncelleyi kategori tablosunda işleme alıyoruz
    //burada işleme başlamadan önce referans olarak businueslayer e DataAccessLayer ve EntityFramework ekledik  daha sonra <Category> altı ciziliydi  bununda kutuphanesini ekledik
    public class CategoryRepository:GenericRepository<Category>
    {
        DataContext db = new DataContext();

        public List<Product> categooryDetails(int id)
        {
            return db.products.Where(x => x.CategoryId == id).ToList();
        }

    }
}
