using kimyatesti.Data;
using kimyatesti.identity;
using kimyatesti.Identity;
using kimyatesti.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace kimyatesti.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private kimyatestiDataContext dB = new kimyatestiDataContext();
        public AccountController()
        {
            userManager = new UserManager<AppUser>(new UserStore<AppUser>(new IdentityDataContext()));
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 6
            };

            userManager.UserValidator = new UserValidator<AppUser>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };
        }
        // GET: Account
        public ActionResult Index(string alertMsg)
        {
            if (alertMsg != null)
            {
                ViewBag.alertMsg = alertMsg;
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["loggedInError"] = "Giriş yapmış durumdasınız. ";
                return RedirectToAction("index", "Home");
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var user = userManager.Find(model.Username, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı veya parola girdiniz.");
                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    userManager.Update(user);
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };
                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["loggedInError"] = "Giriş yapmış durumdasınız. ";
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser();
                user.UserName = model.Email;
                user.Email = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.RegistrationDate = DateTime.Now;
                user.LastLogin = DateTime.Now;
                var hashTxt = EasyEncryption.MD5.ComputeMD5Hash(user.Email + DateTime.Now);
                user.PhoneNumber = hashTxt;
                var result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    var mailText = "https://localhost:44395/account/verifymail?uN=" + user.UserName + "&uHt=" + hashTxt;
                    SendMail(user.Email, "Mail adresinizi aşağıdaki linke tıklayarak doğrulayabilirsiniz.", mailText);
                    userManager.AddToRole(user.Id, "User");

                    // öğrenci avatarı oluşturulur
                    var ogrenciAvatar = new OgrenciAvatar();
                    ogrenciAvatar.OgrEmail = user.Email;
                    ogrenciAvatar.OgrName = user.Name;
                    dB.OgrenciAvatars.Add(ogrenciAvatar);
                    dB.SaveChanges();

                    return RedirectToAction("Login", new { alertMsg="Hoşgeldiniz. Kullanıcı adı ve parolanızı kullanarak giriş yapabilirsiniz. E-posta adresinize gönderilen linki tıklayarak hesabınızı aktive etmeyi unutmayın."});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("/Login");
        }

        //[HttpPost]
        public void SendMail(string receiver, string subject, string message, string senderMail="info@kimyatesti.com")
        {

            var senderEmail = new MailAddress(senderMail, "Taner OZEL");
            var receiverEmail = new MailAddress(receiver, "Receiver");
            var password = "8hvt1mqr";
            var body = message;
            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(mess);
            }
        }

        public ActionResult VerifyMail(string uN, string uHt)
        {
            var user = userManager.Users.First(i => i.UserName == uN);

            //var user = userManager.Find(model.Username, model.Password);

            user.LastLogin = DateTime.Now;
            userManager.Update(user);
            var authManager = HttpContext.GetOwinContext().Authentication;
            var identity = userManager.CreateIdentity(user, "ApplicationCookie");
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            authManager.SignOut();
            authManager.SignIn(authProperties, identity);

            if (user.PhoneNumber == uHt)
            {
                user.EmailConfirmed = true;
                user.PhoneNumber = null;
                userManager.Update(user);

                ViewBag.testData = "her şey yolunda.";
                return RedirectToAction("index", "dashboard");
            }
            else
            {
                ViewBag.testData = "birşeyler ters gitti.";
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Users.First(i => i.UserName == model.Username);
                var hashTxt = EasyEncryption.MD5.ComputeMD5Hash(user.Email + DateTime.Now);
                user.PhoneNumber = hashTxt;
                userManager.Update(user);
                var mailText = "Aşağıdaki linke tıklayarak şifrenizi sıfırlayabilirsiniz. https://localhost:44395/account/ForgotPassword2?uN=" + CryptoService.Sifrele(user.UserName) + "&uHt=" + hashTxt;

                SendMail(user.UserName, "Şifre sıfırlama talebi", mailText);
                return RedirectToAction("Login");
            }

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword2(string uN, string uHt)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword2(ForgotPassword2 model, string uN, string uHt)
        {

            if (ModelState.IsValid)
            {
                var UserName = CryptoService.SifreCoz(uN);
                var user = userManager.Users.First(i => i.UserName == UserName);
                Console.WriteLine("test");
                if (user.PhoneNumber == uHt)
                {
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(model.Password);
                    userManager.Update(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.testData = "birşeyler ters gitti.";
                }

                return View();
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Contact(ContactViewModel userMsg)
        {
            var mailText = userMsg.UserMessage;
            if (User.Identity.IsAuthenticated)
            {
                mailText += (" " + User.Identity.Name);
            }
            if (ModelState.IsValid)
            {
                SendMail("tanerozel@live.com", userMsg.Konu, mailText);
            }
            return RedirectToAction("index", "dashboard", new { alertMsg = "Mesajınız alınmıştır."});
        }
    }
}