using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebApplicationCoreLogin.Models;
using WebApplicationCoreLogin.Models.ViewModel;

namespace WebApplicationCoreLogin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private DatabaseContext db;
        private IConfiguration _configuration;

        public AccountController(DatabaseContext dbcontext,IConfiguration configuration)
        {
            db = dbcontext;
            _configuration = configuration;
        }
        //dependency injection araştır

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string sifre = _configuration.GetValue<string>("Appsettings:sifre");
                sifre = model.Password + sifre;
                string md5sifre = sifre.MD5();

                User user = db.Users.FirstOrDefault(x => x.UserName.ToLower() == model.UserName.ToLower() && x.Password == md5sifre);

                if (user!=null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Name", user.Name?? string.Empty));
                    claims.Add(new Claim("UserName", user.UserName));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                    //HttpContext.SignInAsync("Cookies",);böyle dekullanılır yukardakinin aynısı

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı yada şifre hatalı");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x=>x.UserName.ToLower()==model.UserName.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.UserName), "Bu kullanıcı adı sistemde zaten kayıtlı");
                    return View(model);
                }
                string sifre = _configuration.GetValue<string>("Appsettings:sifre");
                sifre = model.Password + sifre;
                string md5sifre = sifre.MD5();
                
                User user=new User()
                {
                    UserName=model.UserName,
                    Password=md5sifre,//md5 şifreleme ve çözme bak
                };
                db.Users.Add(user);
                if (db.SaveChanges()==0)
                {
                    ModelState.AddModelError("", "Kayıt Eklenemedi");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        public IActionResult Profil()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
