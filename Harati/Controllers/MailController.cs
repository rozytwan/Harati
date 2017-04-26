using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Harati.Models;
using System.Web.UI;
using System.Net.Mime;

namespace Harati.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail

        public ActionResult Index(Mail _objModelMail)
        {
            ViewBag.IsEmailSent = false;
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();

                mail.To.Add("krennovatest@gmail.com");
                mail.From = new MailAddress(_objModelMail.Email);

                mail.Subject = "Enquiry";
                mail.Body = "Hello <b>Admin</b>,<br/><br/>A new Mail has been Received on Harati.<br/><br/><b>" + mail.Subject + ".</b><br/><br/>" + "From:&nbsp;" + _objModelMail.Name + ".<br/>Email:&nbsp; " + _objModelMail.Email + ".<br/>Contact #:&nbsp; " + _objModelMail.Mobile + ".<br/><br/>" + _objModelMail.Body + "<br/><br/>Thanks.<br/>";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("krennovatest@gmail.com", "test@123#");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ModelState.Clear();
                ViewBag.IsEmailSent = true;
                return View("../Home/Contact");

            }
            else
            {
                ViewBag.IsEmailSent = false;
                return View("../Home/Contact");
            }

        }

        private AlternateView getEmbeddedImage(string filePath, string body)
        {

            //LinkedResource inline = new LinkedResource(filePath);
            filePath = "~/" + filePath;
            LinkedResource inline = new LinkedResource(Server.MapPath(filePath));
            inline.ContentId = Guid.NewGuid().ToString();
            string htmlBody = body + @"<img src='cid:" + inline.ContentId + @"' height='300' width='200'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }

        public ActionResult Order(int Id, string Name, string color, string mobile, string email, string Image)
        {
            ViewBag.IsEmailSent = false;
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();

                mail.To.Add("krennovatest@gmail.com");
                mail.From = new MailAddress(email);

                mail.Subject = "Enquiry";
                mail.Body = "Hello <b>Admin</b>,<br/><br/>A new Mail has been Received on Harati for .<br/><br/><b>" + Name + ".</b> of " + color + " Color. <br/><br/>" + "From:&nbsp;Email:&nbsp; " + email + ".<br/>Contact #:&nbsp; " + mobile + ".<br/><br/>Thanks.<br/>";
                ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                //mail.AlternateViews.Add(getEmbeddedImage(Image));
                mail.IsBodyHtml = true;
                //AlternateView alternate = AlternateView.CreateAlternateViewFromString(mail.Body, mimeType);
                mail.AlternateViews.Add(getEmbeddedImage(Image, mail.Body));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("krennovatest@gmail.com", "test@123#");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                ViewBag.IsEmailSent = true;
                ViewData["IsEmailSent"] = true;
                ModelState.Clear();
                return RedirectToAction("ViewItem", "Home", new { id = Id });
            }
            else
            {
                ViewBag.IsEmailSent = false;
                ViewData["IsEmailSent"] = false;
                return RedirectToAction("ViewItem", "Home", new { id = Id });


            }

        }


    }

}