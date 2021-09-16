using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"INSERT INTO usuarios (nome,senha) VALUES ('{Nome}','{Senha}')";

            await sqlCommand.ExecuteReaderAsync();

            await sqlConnection.CloseAsync();

            return new JsonResult(new { Msg = "Usuário cadastrado com sucesso!" });
        }
    }
}
