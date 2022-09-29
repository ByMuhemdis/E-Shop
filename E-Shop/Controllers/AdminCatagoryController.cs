using BusinessLayer.Concrate;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{

    //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek
    //yazıldıgında role göre giriş yapması amacıyla kullanıcı direk olarak giriş sayfasına tönlendirilir
    [Authorize(Roles = "Admin")]
    public class AdminCatagoryController : Controller
    {

        // GET: AdminCatagory
        CategoryRepository categoryRepository = new CategoryRepository();
       
        public ActionResult Index()
        {
          
            return View(categoryRepository.List());
        }
        //ekleme işlemi
       
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]// güvenlik kontrolu kullanıcı siteye girdiginde kişi ekle butununa bastıgında  bizim textlerşimize deger göndermesi lazım
        [HttpPost]

        public ActionResult Create(Category p)
        {
            if (ModelState.IsValid)
            {

                categoryRepository.insert(p);
                //brada db.savachange demedik çümkü bunu GenericRepository de yaptık

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "bir hata oluştu");

            return View();
           
        }

        public ActionResult Delete(int id)
        {
            var delete = categoryRepository.GetById(id);
            categoryRepository.Delete(delete);
            return RedirectToAction("Index");
            
        }

        public ActionResult update(int id)
        {
            var update = categoryRepository.GetById(id);
            return View(update);
        }

        [ValidateAntiForgeryToken]// güvenlik kontrolu kullanıcı siteye girdiginde kişi ekle butununa bastıgında  bizim textlerşimize deger göndermesi lazım
        [HttpPost]


        public ActionResult update(Category p)
        {
            if (ModelState.IsValid)
            {
                var update = categoryRepository.GetById(p.Id);
                update.Name = p.Name;
                update.Description = p.Description;
                categoryRepository.update(update);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "bir hata oluştu");

            return View();

        }
    }
}