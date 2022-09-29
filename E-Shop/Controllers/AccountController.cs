using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Shop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //link uzerinden admin/index yazarak izinsiz admin tarafına gidişi engellemek amacıyla asagıdaki kod yazılır böylelikle admi/indek yazıldıgında role göre giriş yapması amacıyla kullanıcı
        //direk olarak giriş sayfasına tönlendirilir
        DataContext db =new DataContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        //kayıt ol
        public ActionResult Login(User data )
        {
            var bilgisler=db.users.FirstOrDefault(x => x.Email==data.Email && x.Password==data.Password);
            if (bilgisler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgisler.Email, false);
                Session["Mail"]=bilgisler.Email.ToString();
                Session["Ad"]=bilgisler.Name.ToString();
                Session["Soyad"]=bilgisler.SurName.ToString();
                Session["UserID"]=bilgisler.Id.ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                 ViewBag.hata = "E-mail veya Şifreniz hatalı";
              

            }
           
            return View(data);
        }
        [HttpPost]

        public ActionResult Register(User data)
        {
            if (ModelState.IsValid)

            {

                db.users.Add(data);
                data.Role = "User";
                db.SaveChanges();
                return RedirectToAction("Login");


            }
            ModelState.AddModelError("", "Hatalı giriş");
            return View("Login",data);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    

    }
}