using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class LoggingEvents
    {
        public const int Login = 3000;
        public const int LoginNotFound = 3001;
        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;
        
    }
}
