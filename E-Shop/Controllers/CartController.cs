using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db = new DataContext();

        //sepete unun ekleyip fiat belirleme

        public ActionResult Index(decimal? Tutar )
        {
            //liste ve toplam tutar hesaplama

          
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var kullanici = db.users.FirstOrDefault(x => x.Email == username);
                var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
                var kid = db.Carts.FirstOrDefault(x => x.UserId == kullanici.Id);
               


                if (model != null)
                {

                    if (kid == null)
                    {

                        ViewBag.Tutar = "sepetinizde urun bulunamadı.";

                    }
                    else if (kid != null)
                    {

                        Tutar = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.Tutar = "toplam tutar " + Tutar + " TL";

                    }
                    return View(model);


                }

            }
            return HttpNotFound();
        }

        //sepete urun ekleme işlemi

        public ActionResult AddCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var model=db.users.FirstOrDefault(x => x.Email == kullaniciadi);
                var u = db.products.Find(id);
                var sepet=db.Carts.FirstOrDefault(x => x.UserId == model.Id && x.ProductID==id);

                if (model!=null)
                {
                    if (sepet != null)
                    {
                        sepet.Quantity++;
                        sepet.Price = u.Price*sepet.Quantity;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }

                    var s = new Cart
                    {
                        UserId = model.Id,
                        ProductID = u.Id,
                        Quantity = 1,
                        Price = u.Price,
                        date = DateTime.Now,
                    };

                    db.Carts.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");

                }
             
            }
            return View();
        }

        //sepektteki urun sayınını gösteren metot
      
        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = db.users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = db.Carts.Where(x => x.UserId == model.Id).Count();
                ViewBag.count=count;
                if (count==0)
                {
                    ViewBag.count = null;
                }
            }
            return PartialView();
        }

        public void DinamikMiktar(int id,int miktari)
        {
            var model = db.Carts.Find(id);
            model.Quantity = miktari;
            model.Price=model.Price*model.Quantity;
            db.SaveChanges();


        }
        //sepetteki ürünü Azaltma işlemi

        public ActionResult azalt(int id)
        {
            var model = db.Carts.Find(id);
            if (model.Quantity==1)
            {

                db.Carts.Remove(model);
                        

            }
            model.Quantity--;
            model.Price=model.Price*model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //sepetteki ürünü arttırma işlemi

        public ActionResult arttir(int id)
        {
            var model = db.Carts.Find(id);
            model.Quantity++;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //sepetteki urunleri tek tek silme

        public ActionResult Delete(int id)
        {
            var Teksil = db.Carts.Find(id);
            db.Carts.Remove(Teksil);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        //sepetteki urunleri tek serferde silme

        public ActionResult DeleteRange()
        {
            if(User.Identity.IsAuthenticated)
            {
                var kullan = User.Identity.Name;
                var model=db.users.FirstOrDefault(x => x.Email == kullan);
                var sil = db.Carts.Where(x => x.UserId == model.Id);
                db.Carts.RemoveRange(sil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();

        }
    }
}