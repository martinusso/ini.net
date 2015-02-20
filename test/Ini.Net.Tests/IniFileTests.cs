using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ini.Net;

namespace Ini.Net.Tests
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
        public void TestReadDecimal()
        {
            const string section = "martinusso";
            const string key = "decimal";
            decimal value = 3.14m;

            this.iniFile.WriteDecimal(section, key, value);
            decimal gotValue = this.iniFile.ReadDecimal(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestReadDouble()
        {
            const string section = "martinusso";
            const string key = "double";
            double value = 3.1415926535;

            this.iniFile.WriteDouble(section, key, value);
            double gotValue = this.iniFile.ReadDouble(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestReadFloat()
        {
            const string section = "martinusso";
            const string key = "float";
            float value = 3.14F;

            this.iniFile.WriteFloat(section, key, value);
            float gotValue = this.iniFile.ReadFloat(section, key);
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
        public void TestReadDateTime()
        {
            const string section = "section";
            const string key = "datetime";
            var value = new DateTime(1984, 9, 11, 17, 40, 01);

            this.iniFile.WriteDateTime(section, key, value);
            var gotValue = this.iniFile.ReadDateTime(section, key);
            Assert.AreEqual(value, gotValue);
        }

        [TestMethod]
        public void TestReadDate()
        {
            const string section = "section";
            const string key = "date";
            var value = new DateTime(1984, 9, 11);

            this.iniFile.WriteDate(section, key, value);
            var gotValue = this.iniFile.ReadDate(section, key);
            Assert.AreEqual(value, gotValue);
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
        public void TestWriteDecimal()
        {
            iniFile.WriteDecimal("breno", "decimal", 300.5m);
            
            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("decimal=300.5");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestWriteDouble()
        {
            iniFile.WriteDouble("breno", "double", 3.14159265359);

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("double=3.14159265359");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestWriteFloat()
        {
            iniFile.WriteFloat("breno", "float", 3.14F);

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("float=3.14");
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
        public void TestWriteDateTime()
        {
            iniFile.WriteDateTime("breno", "somedatetime", new DateTime(1984, 9, 11, 17, 40, 01));

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("somedatetime=1984/09/11 17:40:01");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestWriteDate()
        {
            iniFile.WriteDate("breno", "birthdate", new DateTime(1984, 9, 11));

            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[breno]");
            sb.AppendLine("birthdate=1984/09/11");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
        }

        [TestMethod]
        public void TestRewriteString()
        {
            this.iniFile.WriteString("section", "key", "value");
            this.iniFile.WriteString("section", "key", "value 2");
            string gotText = System.IO.File.ReadAllText(this.fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[section]");
            sb.AppendLine("key=value 2");
            string expectedText = sb.ToString();

            Assert.AreEqual(expectedText, gotText);
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