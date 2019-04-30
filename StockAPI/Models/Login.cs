using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Login : ILogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
