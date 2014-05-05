using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ini.Net;

namespace IniFile.Tests
{
    [TestClass]
    public class IniFileTests
    {
        private string fileName;
        private Ini.Net.IniFile iniFile;

        public IniFileTests()
        {
            string basePath = System.IO.Directory.GetCurrentDirectory();
            this.fileName = Path.Combine(basePath, "Test.ini"); 
        }

        [TestInitialize()]
        public void Initialize()
        {
            this.iniFile = new Ini.Net.IniFile(this.fileName);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete(this.fileName);
        }
        
        [TestMethod]
        public void TestFileNameShouldBeTestini()
        {
            Assert.AreEqual(fileName, this.iniFile.FileName);
        }

        [TestMethod]
        public void TestIniFileShouldExist()
        {
            iniFile.WriteString("section", "key", "value");
            bool fileExists = File.Exists(this.fileName);
            Assert.IsTrue(fileExists);
        }

        [TestMethod]
        public void TestWriteString()
        {
            iniFile.WriteString("section", "key", "value");

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[section]");
            sb.AppendLine("key=value");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestWriteInteger()
        {
            iniFile.WriteInteger("breno", "birthyear", 1984);

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("birthyear=1984");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestWriteBoolean()
        {
            iniFile.WriteBoolean("breno", "is_alive", true);

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("is_alive=TRUE");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestRewriteString()
        {
            this.iniFile.WriteString("section", "key", "value");
            this.iniFile.WriteString("section", "key", "value2");
            string gotText = System.IO.File.ReadAllText(this.fileName);

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

            this.iniFile.WriteString(section, key, value);
            string gotValue = this.iniFile.ReadString(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestReadInteger()
        {
            const string section = "martinusso";
            const string key = "birthyear";
            const int value = 1984;

            this.iniFile.WriteInteger(section, key, value);
            int gotValue = this.iniFile.ReadInteger(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestReadBoolean()
        {
            const string section = "section";
            const string key = "is_dead";
            const bool value = false;

            this.iniFile.WriteBoolean(section, key, value);
            bool gotValue = this.iniFile.ReadBoolean(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestSectionShouldExists()
        {
            iniFile.WriteString("section", "key", "value");
            bool sectionExists = this.iniFile.SectionExists("section");
            Assert.IsTrue(sectionExists);
        }

        [TestMethod]
        public void TestSectionShouldNotExists()
        {
            iniFile.WriteString("section", "key", "value");
            bool sectionExists = this.iniFile.SectionExists("nonexistent_section");
            Assert.IsFalse(sectionExists);
        }
    }
}