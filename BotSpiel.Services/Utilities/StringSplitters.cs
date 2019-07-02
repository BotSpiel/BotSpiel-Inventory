using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace BotSpiel.Services.Utilities
{
    static public class StringSplitters
    {
        static public string[] SplitCamelCase(string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
    }

}
