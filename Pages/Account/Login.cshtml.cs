using Autenticacion.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (User.Email == "correo@gmail.com" && User.Password == "12345")
            {
                //se crea los Claim,datos a almacenar en la Cookie
                var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email,User.Email ),

            };
                //se asocia los Claims creados a un nombre de Cookie
                var indetity = new ClaimsIdentity(Claims, "MyCookieAuth");

                //se agrega la indtidad creada al ClaimsPrincipal de la Aplicacion
                ClaimsPrincipal ClaimsPrincipal = new ClaimsPrincipal(indetity);

                //se registra exitosamente la autenticacion y se crea la Cookie en el navegador
                await HttpContext.SignInAsync("MyCookieAuth", ClaimsPrincipal);
                return RedirectToPage("/index");
            }
            return Page();
        }
    }

}



