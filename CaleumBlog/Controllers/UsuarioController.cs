using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaelumBlog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CaelumBlog.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autentica(LoginViewModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (ValidaLogin(model))
            {
                // Vamos gravar um cookie com dados da sessão

                // Dados para serem gravados
                var claims = new List<Claim>
                    {
                        new Claim("usuario", model.LoginName),
                        new Claim("role", "Member")
                    };

                // novo ClaimsIdentity passando a lista de claims com a qual popular o ClaimsIdentity
                var principal = new ClaimsIdentity(
                    claims: claims, 
                    authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
                    nameType: "usuario",
                    roleType: "role"
                    );

                // 
                await HttpContext.SignInAsync(new ClaimsPrincipal(principal));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            return RedirectToAction(Login);
        }

        private IActionResult RedirectToAction(Func<IActionResult> login)
        {
            throw new NotImplementedException();
        }

        private bool ValidaLogin(LoginViewModel model)
        {
            return true;
        }
    }
}
