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
        private string FileName;
        private IniFile IniFile;

        public IniFileTest()
        {
            string basePath = System.IO.Directory.GetCurrentDirectory();
            this.FileName = Path.Combine(basePath, "Test.ini"); 
        }

        [TestInitialize()]
        public void Initialize()
        {
            this.IniFile = new IniFile(this.FileName);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete(this.FileName);
        }
        
        [TestMethod]
        public void TestFileNameShouldBeTestini()
        {
            Assert.AreEqual(FileName, this.IniFile.FileName);
        }

        [TestMethod]
        public void TestIniFileShouldExist()
        {
            IniFile.WriteString("section", "key", "value");
            bool fileExists = File.Exists(this.FileName);
            Assert.IsTrue(fileExists);
        }

        [TestMethod]
        public void TestWriteString()
        {
            IniFile.WriteString("section", "key", "value");

            string gotText = System.IO.File.ReadAllText(this.FileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[section]");
            sb.AppendLine("key=value");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);

        }

        [TestMethod]
        public void TestRewriteString()
        {
            this.IniFile.WriteString("section", "key", "value");
            this.IniFile.WriteString("section", "key", "value2");
            string gotText = System.IO.File.ReadAllText(this.FileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[section]");
            sb.AppendLine("key=value2");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);

        }

        [TestMethod]
        public void TestReadString()
        {
            const string section = "section";
            const string key = "key";
            const string value = "value";

            this.IniFile.WriteString(section, key, value);
            string gotValue = this.IniFile.ReadString(section, key);
            Assert.AreEqual(value, gotValue);
        }
    }
}