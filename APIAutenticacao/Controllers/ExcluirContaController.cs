using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAutenticacao.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIAutenticacao.Controllers
{
    [Route("api/[controller]")]
    public class ExcluirContaController : Controller
    {
        NetCoreAutenticacaoContext netCoreAutenticacaoContext = new NetCoreAutenticacaoContext();
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete([FromBody]DbConta value)
        {
            // Verificiando se a conta existe
            if (netCoreAutenticacaoContext.DbConta.Any(usuario => usuario.Usuario.Equals(value.Usuario)))
            {
                DbConta conta = netCoreAutenticacaoContext.DbConta.Where(usuario => usuario.Usuario.Equals(value.Usuario)).First();
                netCoreAutenticacaoContext.DbConta.Remove(conta);
                return JsonConvert.SerializeObject("DELCONTA");
            }
            else
            {
                return JsonConvert.SerializeObject("Usuário não encontrado.");
            }
        }
    }
}
