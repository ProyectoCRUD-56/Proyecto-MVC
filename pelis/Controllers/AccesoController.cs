using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pelis.Data;
using pelis.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace pelis.Controllers
{
    public class AccesoController : Controller
    {
        private readonly pelisContext _context;

        public AccesoController(pelisContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuarios oUsuario)
        {
            var usuarios = _context.Usuarios.Include(m => m.Perfil).AsNoTracking();
            var usuario = usuarios.Where(item => item.NombreUsuario == oUsuario.NombreUsuario && item.Contrasena == oUsuario.Contrasena).FirstOrDefault();
            if (usuario != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim("Correo", usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil.Nombre),
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Registro()
        {
            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }

    }
}
