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
        public void TestWriteInteger()
        {
            IniFile.WriteInteger("breno", "birthyear", 1984);

            string gotText = System.IO.File.ReadAllText(this.FileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("birthyear=1984");
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

        [TestMethod]
        public void TestReadInteger()
        {
            const string section = "martinusso";
            const string key = "birthyear";
            const int value = 1984;

            this.IniFile.WriteInteger(section, key, value);
            int gotValue = this.IniFile.ReadInteger(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestSectionShouldExists()
        {
            IniFile.WriteString("section", "key", "value");
            bool sectionExists = this.IniFile.SectionExists("section");
            Assert.IsTrue(sectionExists);
        }

        [TestMethod]
        public void TestSectionShouldNotExists()
        {
            IniFile.WriteString("section", "key", "value");
            bool sectionExists = this.IniFile.SectionExists("nonexistent_section");
            Assert.IsFalse(sectionExists);
        }
    }
}