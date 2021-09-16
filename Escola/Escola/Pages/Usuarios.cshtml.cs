using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Escola.Pages
{
    public class UsuariosModel : PageModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }

        public async Task OnGet()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM usuarios";

            SqlDataReader reader = sqlCommand.ExecuteReader();

            Usuarios = new List<UsuarioViewModel>();

            while (await reader.ReadAsync())
            {
                Usuarios.Add(new UsuarioViewModel
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                }); 
            }

            await sqlConnection.CloseAsync();
        }
    }

    public class UsuarioViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
