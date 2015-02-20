using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var iniFile = new Ini.Net.IniFile("Test.ini");
            iniFile.WriteString("section", "key", "value");
        }
    }
}
