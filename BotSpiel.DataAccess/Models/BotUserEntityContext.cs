using System;
using System.Collections.Generic;
using System.Text;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Models
{
    public class BotUserEntityContext : IBotUserEntityContext
    {
        public string module { get; set; }
        public string entity { get; set; }
        public string entityIntent { get; set; }
    }
}

