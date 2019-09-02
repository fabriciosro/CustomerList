using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerList.Model.Dtos;
using CustomerList.Model.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomerList.Controllers
{
  public class LoginController : Controller
  {
    private readonly IUserBusiness _userBusiness;

    public LoginController(IUserBusiness userBusiness)
    {
      this._userBusiness = userBusiness;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
      var model = new LoginDto();
      return View(model);
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model)
    {
      if (ModelState.IsValid)
      {
        var user = this._userBusiness.Authenticate(model);

        if (user != null)
        {
          var claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.GivenName, user.Login)
          };

          var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

          var principal = new ClaimsPrincipal(identity);
          var authProperties = new AuthenticationProperties
          {
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(2)
          };

          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

          return RedirectToAction("Index", "Home");
        }
        else
        {
          model = new LoginDto();

          TempData["ErroAutenticacao"] = "Usuário ou senha inválido";
          return View(model);
        }
      }

      return View(model);
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return RedirectToAction("Login", "Secure");
    }

  }
}
