using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek yazıldıgında role göre giriş yapması amacıyla kullanıcı
    //direk olarak giriş sayfasına tönlendirilir
    [Authorize(Roles ="Admin")]
    public class AdminİstatisticController : Controller
    {

        // GET: Adminİstatistic
       
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var satis = db.sales.Count();
            ViewBag.satıs = satis;

            var urun = db.products.Count();
            ViewBag.urun = urun;


            var kategori = db.categories.Count();
            ViewBag.kategori = kategori;

            var sapet = db.Carts.Count();
            ViewBag.sapet = sapet;

            var user = db.users.Count();
            ViewBag.user = user;

          
            return View();
        }
    }
}