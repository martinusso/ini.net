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

        private const int SIZE = 255;
        private const string DATETIME_MASK = "yyyy/MM/dd HH:mm:ss";
        private const string DATE_MASK = "yyyy/MM/dd";
        
        public string FileName { get; private set; }

        public IniFile(string fileName)
        {
            this.FileName = fileName;
        }

        public string ReadString(string section, string key)
        {
            var temp = new StringBuilder(SIZE);
            GetPrivateProfileString(section, key, null, temp, SIZE, this.FileName);
            return temp.ToString();
        }

        public bool ReadBoolean(string section, string key)
        {
            string value = ReadString(section, key);
            return value.ToUpper().Equals("TRUE");
        }

        public int ReadInteger(string section, string key)
        {
            string value = ReadString(section, key);
            return Convert.ToInt32(value.Trim());
        }

        public DateTime ReadDateTime(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATETIME_MASK,
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public DateTime ReadDate(string section, string key)
        {
            string value = ReadString(section, key);
            return DateTime.ParseExact(value, DATE_MASK,
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public bool WriteString(string section, string key, string value)
        {
            long l = WritePrivateProfileString(section, key, value, this.FileName);
            return l > 0;
        }
        
        public bool WriteBoolean(string section, string key, bool value)
        {
            string str = value.ToString().ToUpper();
            return WriteString(section, key, str);
        }

        public bool WriteInteger(string section, string key, int value)
        {
            return WriteString(section, key, value.ToString());
        }

        public bool WriteDateTime(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATETIME_MASK));
        }

        public bool WriteDate(string section, string key, DateTime value)
        {
            return WriteString(section, key, value.ToString(DATE_MASK));
        }

        public bool SectionExists(string section)
        {
            int i = GetPrivateProfileString(section, null, null, new StringBuilder(SIZE), SIZE, this.FileName);
            return i > 0;
        }
    }
}
