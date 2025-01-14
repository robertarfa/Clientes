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

        public IActionResult Login()
        {
            return View();
        }

        //public IActionResult Login(string returnUrl = null)
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _apiService.Login(model);
                if (token != null)
                {
                    HttpContext.Session.SetString("JWTToken", token);

                    return RedirectToAction("Index", "Clientes");
                }
                ModelState.AddModelError("", "Login inválido");
            }
            return View(model);
        }

    }

}
