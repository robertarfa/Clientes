using ClienteMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClienteMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

        public AccountController(IApiService apiService)
        {
            _apiService = apiService;
        }

      
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

   
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var token = await _apiService.Login(model);
                if (token != null)
                {
                    HttpContext.Session.SetString("JWTToken", token);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Clientes");
                }
                ModelState.AddModelError("", "Login inválido");
            }
            return View(model);
        }

    }

}
