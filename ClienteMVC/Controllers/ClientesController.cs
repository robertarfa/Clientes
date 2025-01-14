using ClienteMVC.Models;
using ClienteMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IApiService _apiService;

        public ClientesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var clientes = await _apiService.GetClientesAsync();
            return View(clientes);
        }

        
        public async Task<IActionResult> Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Cliente model)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateCliente(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Clientes/Edit/5
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
  
            var cliente = await _apiService.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
  
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateCliente(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Clientes/Delete/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
      
            var cliente = await _apiService.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteCliente(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
