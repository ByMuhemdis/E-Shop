using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace E_Shop.Controllers
{
    //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek yazıldıgında role göre giriş yapması amacıyla kullanıcı
    //direk olarak giriş sayfasına tönlendirilir

    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
       
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Comment(int sayfa=1)
        {
            return View(db.comments.ToList().ToPagedList(sayfa,10));
        }
        public ActionResult Delete(int id)
        {
            var delete = db.comments.Where(x => x.Id == id).FirstOrDefault();
            db.comments.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Comment");
        }

        public ActionResult UserList()
        {
            var user=db.users.Where(x=>x.Role=="User").ToList();


            return View(user);
        }
        public ActionResult UserDelete(int id)
        {

            var usrid = db.users.Where(x => x.Id == id).FirstOrDefault();
            db.users.Remove(usrid);
            db.SaveChanges();

            return RedirectToAction("UserList");
        }

    }
}