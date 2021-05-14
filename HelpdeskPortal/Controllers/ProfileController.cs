using HelpdeskPortal.Interfaces;
using HelpdeskPortal.Models.Profile;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpdeskPortal.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileInterface _repository;
        public ProfileController(IProfileInterface repository)
        {
            this._repository = repository;
        }
        [Authorize]
        public IActionResult Index()
        {
            var claim = User.Claims.ToList();
            return View(_repository.GetProfile(Convert.ToInt32(claim[0].Value)));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            ProfileModel profile = new ProfileModel();
            if (ModelState.IsValid)
            {
                profile = _repository.Login(login, password);
                if (profile.Login != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, profile.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name , profile.Login));
                    identity.AddClaim(new Claim(ClaimTypes.Role , profile.PositionToken));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName , profile.Name + " " + profile.Surname));
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return this.RedirectToAction("Index", "Question");
                }
                ViewBag.Error = "Неверный логин или пароль!";
            }
            return this.View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public string UpdateProfile(string phone,string firstName,string lastName,string email)
        {
            var claim = User.Claims.ToList();
            _repository.UpdateProfile(phone, firstName, lastName, email, Convert.ToInt32(claim[0].Value));
            return "true";
        }
        [HttpPost]
        public string ChangePassword(string password)
        {
            _repository.ChangePassword(Convert.ToInt32(User.Claims.ToList()[0].Value), password);
            return "true";
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
        public IActionResult AddUser()
        {
            return View();
        }
        [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
        [HttpPost]
        public string AddUser(string login, string password, string phone, string firstName, string lastName, string email, int positionId)
        {
            try
            {
                return _repository.AddUser(login, password, phone, firstName, lastName, email, positionId);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public IActionResult Positions(int? profileId = null)
        {
            ViewBag.ProfileId = profileId;
            return View(_repository.GetPositions());
        }
        [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
        public IActionResult Users()
        {
            return View(_repository.GetUsers());
        }
        [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
        public IActionResult ChangeUser(int profileId)
        {
            return View(_repository.GetProfile(profileId));
        }
        [Authorize(Roles = "B26101AC-4284-41D7-B400-6443C159BB53")]
        public string UpdateUser(int profileId, string phone, string firstName, string lastName, string email, int positionId)
        {
            try
            {
                _repository.UpdateUser(phone, firstName, lastName, email, positionId, profileId);
                return "true";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
