using System;
using System.Collections.Generic;
using System.Text;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Models
{
    public class IBotUserEntityContext
    {

        string module { get; set; }
        string entity { get; set; }
        string entityIntent { get; set; }

    }
}

