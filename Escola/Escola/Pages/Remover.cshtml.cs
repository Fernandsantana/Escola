using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Escola.Pages
{
    public class RemoverModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        private readonly Contexto _contexto;

        public RemoverModel(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void OnGet()
        {
            Usuario usuario = _contexto.Usuarios.AsNoTracking().FirstOrDefault(x => x.UsuarioId == Id);
            Nome = usuario.Nome;
        }

        public IActionResult OnPost()
        {
            Usuario usuario = _contexto.Usuarios.FirstOrDefault(x => x.UsuarioId == Id);

            _contexto.Remove(usuario);
            _contexto.SaveChanges();

            return new JsonResult(new { Msg = "Usuário excluído com sucesso" });
        }
    }
}
