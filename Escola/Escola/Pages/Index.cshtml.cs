using Escola.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Escola.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Contexto _contexto;

        public IndexModel(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("Usuarios");
            }

            return Page();
        }

        public IActionResult OnPostLogar(string nome, string senha, bool manterLogado)
        {
            Usuario usuario = _contexto.Usuarios.AsNoTracking().FirstOrDefault(x => x.Nome == nome && x.Senha == senha);

            if (usuario != null)
            {

                int usuarioId = usuario.UsuarioId;

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString())
                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                HttpContext.SignInAsync(userPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = manterLogado,
                            ExpiresUtc = DateTime.Now.AddHours(1)
                        }
                    );

                return RedirectToPage("Usuarios");

            }
            else
            {
                return new JsonResult(new { Msg = "Usuário não encontrado" });
            }
        }

        public IActionResult OnGetLogout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }

            return Redirect("Index");
        }
    }
}
