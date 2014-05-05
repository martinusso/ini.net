using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Ini.Net
{
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string FileName { get; private set; }
        //private string fileName;
        //public string GetFileName()
        //{
        //    return this.fileName;
        //}

        public IniFile(string fileName)
        {
            this.FileName = fileName;
        }

        public void WriteString(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.FileName);
        }

        public string ReadString(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.FileName);
            return temp.ToString();
        }
    }
}
