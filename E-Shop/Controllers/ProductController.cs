using BusinessLayer.Concrate;
using DataAccessLayer.Context;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace E_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductRepository ProductRepository = new ProductRepository();
        DataContext db=new DataContext();
        public PartialViewResult PopularProduct()
        {
            var product = ProductRepository.GEtPopularProduct();
            ViewBag.popular = product;
            return PartialView(ProductRepository.GEtPopularProduct());
        }
        [Route("product/productdetails/{id}/{name}")]
        public ActionResult ProductDetails(int id)
        {
            var details = ProductRepository.GetById(id);
          
           
            var yorum = db.comments.Where(x=>x.ProductId==id).OrderByDescending(x=>x.Id).ToList();
            ViewBag.yorum = yorum;
            return View(details);
        }
        [HttpPost]
        public ActionResult Comment(Comment date)
        {
            

            if (User.Identity.IsAuthenticated)
            {
                db.comments.Add(date);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");


            }
            return View();
        }
    }
}