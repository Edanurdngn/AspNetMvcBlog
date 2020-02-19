
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AspNetMvcBlog.Extensions;
using AspNetMvcBlog.Models;

namespace AspNetMvcBlog.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contect
        SendEmailEntities1 db = new SendEmailEntities1();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Narail | İletişim";
            List <SelectListItem> ulkeadi = (from i in db.TelKodu.ToList()
                                            select new SelectListItem
                                            {
                                                
                                                Text =i.UlkeAd,
                                                Value = i.Id.ToString()
                                            }).ToList();
            ViewBag.bag = ulkeadi;
            return View();
        }
        [HttpGet]
        public JsonResult GetJsonResult()
        {
            var result = db.TelKodu.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        ResultEmail results;
        [HttpPost]
        public ActionResult Index(Send Savemail)
        {
         
            

            List<SelectListItem> ulkekodu = (from i in db.TelKodu.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.UlkeKodu,
                                                 Value = i.Id.ToString()
                                             }).ToList();
            ViewBag.bag = ulkekodu;

            try
            {
                try
                {
                    string[] email = Savemail.Kime.Split(';');
                  
                    for (int i = 0; i < email.Count(); i++)
                    {
                        results = EmailExtension.EmailRegex(email[i]);
                    }
                    if (results.ResultErrorList.Count > 0)
                    {
                        ModelState.AddModelError("Kime", "Email girişlerinde hatalı bir email mevcut");
                        return View(Savemail);
                    }
                    else
                    {
                        foreach (var item in email)
                        {
                            var senderEmail = new MailAddress("edanurdingin@gmail.com", "Edanur");

                            var receiverEmail = new MailAddress(item, "Receiver");

                            var password = "sifre";
                            var sub = Savemail.Konu;
                            var body = Savemail.Mesaj + "  " + Savemail.Kim + " " + Savemail.Telefon;
                            var smtp = new SmtpClient()
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(senderEmail.Address, password)
                            };
                            using (var mess = new MailMessage(senderEmail, receiverEmail)
                            {
                                Subject = Savemail.Konu,
                                Body = body

                            })
                                smtp.Send(mess);
                            db.Send.Add(Savemail);
                            db.SaveChanges();
                        }
                    }
                    return View();

                }
                catch (NullReferenceException exception)
                {
                    ModelState.AddModelError("Kime", "Email alanı boş olamaz");
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public JsonResult TelefonKoduGetir(int id)
        {
            var result = db.TelKodu.FirstOrDefault(x => x.Id == id).UlkeKodu;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}


