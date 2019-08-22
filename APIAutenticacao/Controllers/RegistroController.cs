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
    public class RegistroController : Controller
    {
        NetCoreAutenticacaoContext netCoreAutenticacaoContext = new NetCoreAutenticacaoContext();
        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]DbConta value)
        {
            // Verifica se o usuário já existe no banco de dados
            if(!netCoreAutenticacaoContext.DbConta.Any (usuario => usuario.Usuario.Equals(value.Usuario)))
            {
                DbConta conta = new DbConta();
                conta.Usuario = value.Usuario; // Atribunidno um valor para o POST de usuário
                conta.SaltCript = Convert.ToBase64String(Common.getRandomSalt(16)); // gerando um valor aleatorio para Salt
                conta.Senha = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(value.Senha),
                    Convert.FromBase64String(conta.SaltCript)));

                // Colando no banco de dados
                try
                {
                    netCoreAutenticacaoContext.Add(conta);
                    netCoreAutenticacaoContext.SaveChanges();
                    // Usuário Registrado com sucesso
                    //return JsonConvert.SerializeObject("Registrado com sucesso.");
                    return JsonConvert.SerializeObject("REGTRUE");
                }
                catch(Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Nome de usuário já registrado.");
            }

        }

    }
}
