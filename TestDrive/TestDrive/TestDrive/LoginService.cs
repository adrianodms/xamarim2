using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {
            using (var cliente = new HttpClient())
            {

                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", login.Email),
                    new KeyValuePair<string, string>("senha", login.Senha)
                });

                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");
                HttpResponseMessage resultado = null;
                try {
                    resultado = await cliente.PostAsync("/login", camposFormulario);
                    if (resultado.IsSuccessStatusCode)
                    {
                        string conteudoResultado = await resultado.Content.ReadAsStringAsync();
                        ResultadoLogin resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudoResultado);
                        MessagingCenter.Send<Usuario>(resultadoLogin.usuario, "SucessoLogin");
                    }
                    else
                    {
                        MessagingCenter.Send<LoginException>(new LoginException("Usuário ou senha incorretos"), "FalhaLogin");
                    }
                }
                catch (Exception exc)
                {
                    MessagingCenter.Send<LoginException>(new LoginException(
@"Ocorreu um erro de comunicação com o servidor 
Por favor, verifique sua conexão e tente novamente mais tarde"),
                    "FalhaLogin");
                }

               

            };
        }
    }

    internal class LoginException: Exception
    {
        public LoginException(string message): base(message)
        {

        }
    }
}
