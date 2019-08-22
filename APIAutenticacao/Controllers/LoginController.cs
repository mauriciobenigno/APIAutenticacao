using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIAutenticacao.Models;
using APIAutenticacao.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIAutenticacao.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        NetCoreAutenticacaoContext netCoreAutenticacaoContext = new NetCoreAutenticacaoContext();
        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]DbConta value)
        {
            // Verificiando se a conta existe
            if (netCoreAutenticacaoContext.DbConta.Any(usuario => usuario.Usuario.Equals(value.Usuario)))
            {
                DbConta conta = netCoreAutenticacaoContext.DbConta.Where(usuario => usuario.Usuario.Equals(value.Usuario)).First();

                // Calcula o Hash da senha do cliente  e compara com o Hash da senha do servidor
                var cliente_post_hash_senha = Convert.ToBase64String(
                    Common.SaltHashPassword(
                        Encoding.ASCII.GetBytes(value.Senha),
                        Convert.FromBase64String(conta.SaltCript)));

                if(cliente_post_hash_senha.Equals(conta.Senha))
                {
                    if(conta.Usuario.Equals("admin"))
                        return JsonConvert.SerializeObject(conta +"LOGTRUE" +  "admin");
                    return JsonConvert.SerializeObject(conta + "LOGTRUE");
                }
                else
                {
                    return JsonConvert.SerializeObject("Senha Incorreta");
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Usuário não encontrado.");
            }
        }

    }
}
