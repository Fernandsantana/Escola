using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Escola.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Escola.Pages
{
    public class UsuariosModel : PageModel
    {
        public List<Usuario> Usuarios { get; set; }

        private readonly Contexto _contexto;

        public UsuariosModel(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
               return Redirect("Index");
            }

            Usuarios = _contexto.Usuarios.ToList();

            return Page();
        }
    }
}
