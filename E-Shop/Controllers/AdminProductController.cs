using BusinessLayer.Concrate;
using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace E_Shop.Controllers
{

    //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek yazıldıgında
    //role göre giriş yapması amacıyla kullanıcı direk olarak giriş sayfasına tönlendirilir
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        // GET: AdminProduct
        ProductRepository productRepository = new ProductRepository();  
        DataContext db= new DataContext();
        public ActionResult Index(int sayfa=1)
        {
            return View(productRepository.List().ToPagedList(sayfa, 3));
        }

        //ekleme işlemi
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> deger1 = (from i in db.categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString(),
                                           }).ToList();
            //ViewBag==controller dan view e veri taşımamızı saglar
            ViewBag.kgtr = deger1;
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase File)
        {
            //resim işlemi için de ayreten -- HttpPostedFileBase File-- kısmını paranteze ekledik
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata oluştu.");
            }

            string path= Path.Combine("~/Content/Image/" + File.FileName);//resim klasörü için
            File.SaveAs(Server.MapPath(path));//resmi kaydet 
            data.Image=File.FileName.ToString();//resmin dosya yoluyla al
            productRepository.insert(data);//datadan gelen verileri ekle
            return RedirectToAction("Index");
        }

        //silme işlemi kısmı

        public ActionResult Delete(int id)
        {
            var delete = productRepository.GetById(id);
            productRepository.Delete(delete);
            return RedirectToAction("Index");

        }

        //güncelleme işlemi
        [HttpGet]
        public ActionResult Update(int id)
        {
            List<SelectListItem> deger1 = (from i in db.categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString(),
                                           }).ToList();
            //ViewBag==controller dan view e veri taşımamızı saglar
            ViewBag.kgtr = deger1;
            var Update = productRepository.GetById(id);
            return View(Update);
        }
        [ValidateAntiForgeryToken]//boş geçilemez komutu
        [HttpPost]
        public ActionResult Update(Product data,HttpApplicationStateBase File)
        {
            var update = productRepository.GetById(data.Id);

            if (!ModelState.IsValid)
            {

                if (File == null)
                {

                    update.Description = data.Description;
                    update.Name = data.Name;
                    update.IsApproved = data.IsApproved;
                    update.Popular = data.Popular;
                    update.Price = data.Price;
                    update.Stock = data.Stock;


                    update.CategoryId = data.CategoryId;
                    productRepository.update(update);
                    return RedirectToAction("Index");

                }
                else
                {

                    update.Description = data.Description;
                    update.Name = data.Name;
                    update.IsApproved = data.IsApproved;
                    update.Popular = data.Popular;
                    update.Price = data.Price;
                    update.Stock = data.Stock;

                    update.Image = data.Image;
                    update.CategoryId = data.CategoryId;
                    productRepository.update(update);
                    return RedirectToAction("Index");
                }

            }


          
            return View(update);

        }

        //kiritik stok kısmı

        public ActionResult CriticalStock()
        {
            var kiritik = db.products.Where(x => x.Stock <= 50).ToList();
            return View(kiritik);
        }
        public PartialViewResult StockCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.products.Where(x => x.Stock < 50).Count();
                ViewBag.count=count;
                var azalan = db.products.Where(x => x.Stock == 50).Count();
                ViewBag.azalan = azalan;
            }
            
            return PartialView();

        }
       
    }
    
}