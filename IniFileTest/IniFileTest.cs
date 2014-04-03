using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ini.Net;

namespace IniFileTest
{
    [TestClass]
    public class IniFileTest
    {
        string filename = "";

        public IniFileTest()
        {
            string basePath = System.IO.Directory.GetCurrentDirectory();
            this.filename = Path.Combine(basePath, "Test.ini"); 
        }
        
        [TestMethod]
        public void TestFileNameShouldBeTestini()
        {
            var inifile = new IniFile(this.filename);
            try
            {
                Assert.AreEqual(filename, inifile.GetFileName());
            }
            finally
            {
                File.Delete(this.filename);
            }
        }

        [TestMethod]
        public void TestIniFileShouldExist()
        {
            var inifile = new IniFile(this.filename);
            try
            {
                inifile.WriteString("section", "key", "value");
                bool fileExists = File.Exists(this.filename);
                Assert.IsTrue(fileExists);
            }
            finally
            {
                File.Delete(this.filename);
            }
        }

        [TestMethod]
        public void TestWriteString()
        {
            var inifile = new IniFile(this.filename);
            try
            {
                inifile.WriteString("section", "key", "value");

                string gotText = System.IO.File.ReadAllText(this.filename);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[section]");
                sb.AppendLine("key=value");
                string expectedText = sb.ToString();

                Assert.AreEqual(expectedText, gotText);
            }
            finally
            {
                File.Delete(this.filename);
            }
        }

        [TestMethod]
        public void TestRewriteString()
        {
            var inifile = new IniFile(this.filename);
            try
            {
                inifile.WriteString("section", "key", "value");

                inifile.WriteString("section", "key", "value2");
                string gotText = System.IO.File.ReadAllText(this.filename);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[section]");
                sb.AppendLine("key=value2");
                string expectedText = sb.ToString();

                Assert.AreEqual(expectedText, gotText);
            }
            finally
            {
                File.Delete(this.filename);
            }
        }

        [TestMethod]
        public void TestReadString()
        {
            const string section = "section";
            const string key = "key";
            const string value = "value";

            var inifile = new IniFile(this.filename);
            try
            {
                inifile.WriteString(section, key, value);

                string gotValue = inifile.ReadString(section, key);

                Assert.AreEqual(value, gotValue);
            }
            finally
            {
                File.Delete(this.filename);
            }
        }
    }
}