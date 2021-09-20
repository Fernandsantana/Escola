using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Escola.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Escola.Pages
{
    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage ="O campo {0} é obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        private readonly Contexto _contexto;

        public CadastrarModel(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            _contexto.Add(new Usuario { Nome = Nome, Senha = Senha });
            _contexto.SaveChanges();

            return new JsonResult(new { Msg = "Usuário cadastrado com sucesso!", Url = Url.Page("Index") });
        }
    }
}
