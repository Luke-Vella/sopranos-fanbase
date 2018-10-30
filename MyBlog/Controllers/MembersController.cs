using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class MembersController : Controller
    {
        private MyBlogContext db = new MyBlogContext();


        // GET: Members/Create
        public ActionResult Registration()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] Member member)
        {
            bool Status = false;
            string Message = "";

            #region Model Validation
            if (ModelState.IsValid)
            {
                #region EmailExists
                //Email already exists
                var isExist = IsEmailExist(member.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exists");
                    return View(member);
                }
                #endregion

                #region Generate Activate Code
                member.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                member.Password = Crypto.Hash(member.Password);
                member.ConfirmPassword = Crypto.Hash(member.ConfirmPassword);
                #endregion
                member.IsEmailVerified = false;

                #region Save to Database
                db.Members.Add(member);
                db.SaveChanges();

                //Send Email To User
                SendVerificationLinkEmail(member.Email, member.ActivationCode.ToString());
                Message = "Registration successfully completed. Account activation link has been sent to your Email Account:" + member.Email;
                Status = true;

                #endregion


            }
            else
            {
                Message = "Invalid Request";
            }

            ViewBag.message = Message;
            ViewBag.Status = Status;


            #endregion//Model Validation




            return View(member);
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (MyBlogContext dc = new MyBlogContext())
            {
                var v = dc.Members.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            if (String.IsNullOrEmpty(emailID))
                return;
            try
            {
                var verifyUrl = "/User/VerifyAccount/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                var fromEmail = new MailAddress("luke.vella.0697@gmail.com", "Luke Vella");
                var toEmail = new MailAddress(emailID);
                var fromEmailPassword = "echoes_68";
                string subject = "Your Account is successfully created!";
                string body = "<br/><br/> We are exited to tell you that your account is successfully created. Please click on the below link to verify your account." +
                    "<br/><br/><a href='" + link + "'>" + link + "</a> ";

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
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                    ViewBag.Error ="Exception in sendEmail:" + ex.Message;
            }

        }

      
    }
}
