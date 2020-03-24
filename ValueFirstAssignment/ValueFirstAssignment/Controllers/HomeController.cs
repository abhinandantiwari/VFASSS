using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ValueFirstAssignment.DataAccess;

namespace ValueFirstAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var userList = new List<User>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                       
                        string[] headers = sreader.ReadLine().Split(',');
                      
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            userList.Add(new User
                            {
                                FullName = rows[0].ToString(),
                                Email = rows[1].ToString(),
                                Phone = rows[2].ToString(),
                                CommunicationAddress = rows[3].ToString(),
                                IsActive=true,
                                Password= Membership.GeneratePassword(12, 2),
                                RolesName= rows[4].ToString()
                            });
                        }
                    }

                    return View("View", userList);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }



        [NonAction]
        public bool SendRegisterEmail(User user)
        {
            var url = string.Format("/Account/Login");
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("abhinandantiwari13@gmail.com", "ABHI");
            var toEmail = new MailAddress(user.Email);

            var fromEmailPassword = "******************";
            string subject = "Successfuly Register !";
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<b>Hi Dear, <b/><br/>");
            sb.AppendFormat("<p>Please click on the following link  to login your account" + "<br/><a href='" + link + "'> Login ! </a>. </p><br/>");

            sb.AppendFormat("<p>Thanks<p/>");
            sb.AppendFormat("<p>ValueFirst<p/><br/>");

            string htmlBody = sb.ToString();
           
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true

                })

                    smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}