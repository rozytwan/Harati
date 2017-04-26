using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harati.Models;

namespace Harati.Controllers
{
    public class HomeController : Controller
    {
        krennovaEntities db;
        public ActionResult Index()
        {
            db = new krennovaEntities();
            objItm.itemList = db.Items.ToList();
            objItemSlider.itemList = db.Items.ToList();
            objItemSlider.sliderList = db.Sliders.ToList();
            objItm.itemList = db.Items.ToList();
            objItemSlider.itemList = db.Items.ToList();
            return View(objItemSlider);
        }
        Item objItm = new Item();
        ItemSliderWrap objItemSlider = new ItemSliderWrap();
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Carpet()
        {
            db = new krennovaEntities();
            objItm.itemList = db.Items.Where(i => i.Category == 1).ToList();
            return View(objItm.itemList);

        }

        public ActionResult Pashmina()
        {
            db = new krennovaEntities();
            objItm.itemList = db.Items.Where(i => i.Category == 2).ToList();
            return View(objItm.itemList);
        }

        public ActionResult Items()
        {
            db = new krennovaEntities();
            objItm.itemList = db.Items.ToList();
            return View(objItm.itemList);
        }

        public ActionResult ViewItem(int Id)
        {
            if (!string.IsNullOrEmpty(Id.ToString()))
            {
                List<ItemCategoryWrap> model = new List<ItemCategoryWrap>();
                try
                {
                    db = new krennovaEntities();
                    //Item itm = db.Items.Find(Id);            

                    var data = (from i in db.Items
                                join c in db.Categories on i.Category equals c.Id
                                where i.Id == Id
                                select new { i.Id, c.CatName, i.ItemName, i.Price, i.Image, i.Description });
                    //retrieve each item and assign to model
                    foreach (var item in data)
                    {
                        model.Add(new ItemCategoryWrap()
                        {
                            ItemId = item.Id,
                            ItemName = item.ItemName,
                            Category = item.CatName,
                            Image = item.Image,
                            Price = item.Price,
                            Description = item.Description
                        });
                    }

                }
                catch
                {

                }
                return View(model);
            }
            else
            {
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
        }

    }
}