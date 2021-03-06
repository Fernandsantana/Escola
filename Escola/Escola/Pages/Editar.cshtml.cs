using Escola.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Pages
{
    public class EditarModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} ? obrigat?rio!")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        private readonly Contexto _contexto;

        public EditarModel(Contexto contexto)
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

            usuario.Nome = Nome;

            _contexto.Update(usuario);
            _contexto.SaveChanges();

            return new JsonResult(new { Msg = "Usu?rio editado com sucesso!"});
        }
    }
}
