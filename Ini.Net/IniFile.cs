using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Ini.Net
{
    public class IniFile
    {
        /// <summary>
        /// Copies a string into the specified section of an initialization file.
        /// </summary>
        /// <returns>
        /// If the function successfully copies the string to the initialization file, the return value is nonzero.
        /// If the function fails, or if it flushes the cached version of the most recently accessed initialization file, the return value is zero. 
        /// </returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <returns>
        /// The return value is the number of characters copied to the buffer, not including the terminating null character.
        /// </returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private static int size = 255;

        public string FileName { get; private set; }

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
            StringBuilder temp = new StringBuilder(size);
            GetPrivateProfileString(section, key, null, temp, size, this.FileName);
            return temp.ToString();
        }

        public bool SectionExists(string sectionName)
        {
            int i = GetPrivateProfileString(sectionName, null, null, new StringBuilder(size), size, this.FileName);
            return i > 0;
        }
    }
}
