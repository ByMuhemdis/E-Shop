using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Context;
using PagedList;
using PagedList.Mvc;


namespace E_Shop.Controllers
{
    //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek yazıldıgında role göre giriş yapması amacıyla kullanıcı
    //direk olarak giriş sayfasına tönlendirilir
    [Authorize(Roles = "Admin")]
    public class AdminSalesController : Controller
    {
        // GET: AdminSales

        //yapılan satışlar 

        DataContext db = new DataContext();
        public ActionResult Index(int sayfa=1)
        {

            return View(db.sales.ToList().ToPagedList(sayfa,5));
        }
    }
}