using BusinessLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryRepository categoryRepository = new CategoryRepository();
        public PartialViewResult CategoryList()
        {
            return PartialView(categoryRepository.List());
        }

        public ActionResult Details(int id)
        {
            var category = categoryRepository.categooryDetails(id);
            return View(category);
        }
    }
}