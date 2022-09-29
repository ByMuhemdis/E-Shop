using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        //kullanıcı bilgileri güncelleme ve şifre degiştirme işlemi

        DataContext db = new DataContext();

        [HttpGet]
        public ActionResult Update()
        {
            var usename = (string)Session["Mail"];
            var degerler=db.users.FirstOrDefault(x=>x.Email == usename);

            return View(degerler);
        }

        [ValidateAntiForgeryToken]//siteye dısardan gelecek olan saldırılara yada site içindeki degişiklikleri engelleyen kod
        [HttpPost]

        public ActionResult Update(User data) 
        {
            var usename = (string)Session["Mail"];
            var user = db.users.Where(x => x.Email == usename).FirstOrDefault();
            user.Name = data.Name;
            user.SurName = data.SurName;
            user.UserName = data.UserName;
            user.Email = data.Email;
            user.Password = data.Password;
            user.RePassword = data.RePassword;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(string eposta)
        {
            var mail = db.users.Where(x=>x.Email==eposta).SingleOrDefault();

            if (mail!=null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                User sifre = new User();
                mail.Password = (Convert.ToString(yenisifre));
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "geltestgel@gmail.com";
                WebMail.Password = "QwEaSd49.";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.Send(eposta, "Giriş şifreniz", "şifreniz" + yenisifre);
                ViewBag.uyarı = "şifreniz başarıyla gönderilmiştir";

                //MailMessage _mail = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                //smtp.Credentials = new NetworkCredential("geltestgel@gmail.com", "QwEaSd49."); // Gönderici bilgilerini giriyoruz
                //smtp.Port = 587; // Mail uzantınıza göre bu değişebilir
                //smtp.Host = "smtp.gmail.com"; // Gmail veya hotmail ise onların host bilgisini almanız gerekiyor 
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //_mail.IsBodyHtml = true;// HTML tagleri kullanarak mail gönderebilmek için aktif ediyoruz
                //_mail.From = new MailAddress("geltestgel@gmail.com"); // Gönderen mail adresini yazıyoruz
                //_mail.To.Add(eposta); // Gönderilecek mail adresini ekliyoruz
                //_mail.Subject = "Giriş şifreniz"; // Mesaja konuyu ekliyoruz
                //_mail.Body = "şifreniz" + yenisifre; // Mesajın içeriğini ekliyoruz
                //_mail.Priority = MailPriority.High;

                ////bir tane sıfır mail olusturup denesene gmail izin vermiyor 
                //smtp.Send(_mail); // Mesajı gönderiyoruz

            }
            else
            {
                ViewBag.uyarı = "hata oluştu tekrar deneyin";
            }
            return View();
        }
    }
}