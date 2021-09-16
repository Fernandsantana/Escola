using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Escola.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { Msg = "Usuário já logado" });
            }

            return View();
        }

        public async Task<IActionResult> Logar(string nome, string senha, bool manterLogado)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=EscolaDB;");
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM usuarios WHERE nome = '{nome}' AND senha = '{senha}'";

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {

                int usuarioId = reader.GetInt32(0);

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString())
                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = manterLogado,
                            ExpiresUtc = DateTime.Now.AddHours(1)
                        }                    
                    );

                return Json(new { Msg = "Usuário logado com sucesso" });
            }

            await sqlConnection.CloseAsync();

            return Json(new { Msg = "Usuário não encontrado" });
           
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
