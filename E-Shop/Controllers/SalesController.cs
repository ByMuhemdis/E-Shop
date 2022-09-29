using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EntityLayer.Entites;

namespace E_Shop.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales

        DataContext db = new DataContext();
        public ActionResult Index(int sayfa = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.users.FirstOrDefault(x => x.Email == kullaniciadi);
                var model = db.sales.Where(x => x.UserId == kullanici.Id).ToList().ToPagedList(sayfa, 5);
                return View(model);
            }
            return HttpNotFound();
        }

        public ActionResult Buy(int id)
        {
            var model = db.Carts.FirstOrDefault(x => x.ID == id);

            return View(model);
        }
        [HttpPost]
        public ActionResult buy2(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.Carts.FirstOrDefault(x => x.ID == id);
                    var satis = new Sales
                    {
                        UserId = model.UserId,
                        ProductID = model.ProductID,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        Image=model.Image,
                        date=DateTime.Now,



                    };
                    db.Carts.Remove(model);
                    db.sales.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir.";

                }

            }
            catch (Exception)
            {

                ViewBag.islem = "Satın alma işlemi başarısız oldu!";
            }
            return View("islem");
        }

        //tümünü stıl al kısmı
        //tumunu satın la tıklayınca urunlerin listelenmesi kısmı
        [HttpGet]
        public ActionResult BuyAll(decimal? tutar )
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.users.FirstOrDefault(x => x.Email == kullaniciadi);
                var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
                var kid=db.Carts.FirstOrDefault(x => x.UserId == kullanici.Id);
                if (model!=null)
                {
                    if (kid==null)
                    {
                        ViewBag.tutar = "Sepetinizde ürün bulunmamaktadır";

                    }
                    else if (kid!=null)
                    {

                        tutar = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.Tutar = "toplam tutar " + tutar + " TL";
                      
                    }
                    return View(model);
                }

                return View();

            }
            return HttpNotFound();

        }

        //tumunu satın la tıklayınca onay verince urunlerin satın alınan kısmı  kısmı

        [HttpPost]

        public ActionResult BuyAll2()
        {
            var username=User.Identity.Name;
            var kullanici = db.users.FirstOrDefault(x => x.Email == username);
            var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
            int row=0;

            foreach (var satınal in model)
            {
                var satis = new Sales
                {
                    UserId = model[row].UserId,
                    ProductID = model[row].ProductID,
                    Quantity = model[row].Quantity,
                    Price = model[row].Price,
                    Image = model[row].Image,
                    date = DateTime.Now,


                };
                db.sales.Add(satis);
                db.SaveChanges();
                row++;
                ViewBag.tutar = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir.";
            }
            db.Carts.RemoveRange(model);
            db.SaveChanges();


            return View("tümsatinalma");
        }

    }
}