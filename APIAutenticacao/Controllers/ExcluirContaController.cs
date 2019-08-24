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
        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]DbConta value)
        {
            // Verificiando se a conta existe
            if (netCoreAutenticacaoContext.DbConta.Find(value.Usuario)!=null)// Usuario existe
            {
                DbConta conta = netCoreAutenticacaoContext.DbConta.Find(value.Usuario);
                netCoreAutenticacaoContext.DbConta.Remove(conta);
                netCoreAutenticacaoContext.SaveChanges();
                return JsonConvert.SerializeObject("DELCONTA");
            }
            else
            {
                return JsonConvert.SerializeObject("Usuário não encontrado.");
            }
        }
    }
}
