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
    public class EditarModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }


        public async Task OnGetAsync()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM usuarios WHERE Id = '{Id}'";

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                Nome = reader.GetString(1);
            }

            await sqlConnection.CloseAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"UPDATE usuarios SET nome = '{Nome}' WHERE id = '{Id}'";

            await sqlCommand.ExecuteReaderAsync();

            await sqlConnection.CloseAsync();

            return new JsonResult(new { Msg = "Usuário editado com sucesso!" });
        }

        public async Task<IActionResult> OnGetRemoverAsync()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"DELETE FROM usuarios WHERE id = '{Id}'";

            await sqlCommand.ExecuteReaderAsync();

            await sqlConnection.CloseAsync();

            return new JsonResult(new { Msg = "Usuário excluído com sucesso!" });
        }
    }
}
