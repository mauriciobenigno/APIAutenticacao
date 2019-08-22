using System;
using System.Collections.Generic;

namespace APIAutenticacao.Models
{
    public partial class DbConta
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string SaltCript { get; set; }
    }
}
