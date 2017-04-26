using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harati.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Harati.Areas.Admin.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: Admin/Item
        krennovaEntities db;

        Item objItm = new Item();
        public ActionResult Index()
        {

            db = new krennovaEntities();
            objItm.itemList = db.Items.ToList();

            return View(objItm.itemList);

        }

        public ActionResult AddItem()
        {
            db = new krennovaEntities();
            ViewBag.Category = new SelectList(db.Categories, "Id", "CatName", objItm.Category);
            return View();
        }

        [HttpPost]
        public ActionResult Save(Item objItem, HttpPostedFileBase file)
        {
            ViewBag.IsSaved = false;
            db = new krennovaEntities();
            if (ModelState.IsValid)
            {
                try
                {

                    var fileName = "";
                    if (file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        string[] file_name = fileName.Split('.');
                        file.SaveAs(path);
                    }
                    objItm.ItemName = objItem.ItemName;
                    objItm.Price = objItem.Price;
                    objItm.Category = objItem.Category;
                    objItm.Image = "Content/Images/" + fileName;
                    objItm.Description = objItem.Description;
                    db.Items.Add(objItm);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.IsSaved = true;
                }
                catch (Exception)
                {

                }
            }
            //objItm.itemList = db.Items.ToList();
            ViewBag.Category = new SelectList(db.Categories, "Id", "CatName", objItm.Category);

            return View("AddItem");

        }
        public ActionResult EditItem(Item objItem, int id)
        {
            db = new krennovaEntities();
            Item itm = db.Items.Find(id);
            ViewBag.Category = new SelectList(db.Categories, "Id", "CatName", itm.Category);

            return View(itm);
        }

        public ActionResult Update(Item objItem, int id, HttpPostedFileBase file)
        {
            try
            {
                db = new krennovaEntities();
                Item objItm = db.Items.Find(id);
                var fileName = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        string[] file_name = fileName.Split('.');
                        file.SaveAs(path);
                        objItm.Image = "Content/Images/" + fileName;
                    }
                }
                else
                {
                    objItm.Image = objItm.Image;
                }
                objItm.ItemName = objItem.ItemName;
                objItm.Price = objItem.Price;
                objItm.Category = objItem.Category;
                objItm.Description = objItem.Description;
                db.Entry(objItm);
                db.SaveChanges();
            }
            catch
            {

            }
            objItm.itemList = db.Items.ToList();
            return View("Index", objItm.itemList);

        }

        public ActionResult DeleteItem(int Id)
        {
            db = new krennovaEntities();
            Item objItm = db.Items.Find(Id);

            return View(objItm);
        }

        public ActionResult Delete(int id)
        {
            db = new krennovaEntities();
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(id)))
                {
                    Item objItm = db.Items.Find(id);
                    db.Items.Remove(objItm);
                    db.SaveChanges();
                }
            }
            catch
            {

            }
            objItm.itemList = db.Items.ToList();
            return View("Index", objItm.itemList);
        }

        public ActionResult ViewItem(int Id)
        {
            db = new krennovaEntities();
            Item objItm = db.Items.Find(Id);

            return View(objItm);
        }
    }
}