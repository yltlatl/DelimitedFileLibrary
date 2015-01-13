using System;
using System.Linq;
using System.Text;
using DelimitedFileLibrary;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelimitedFileLibrary.DelimitedFileUnitTests
{
    [TestClass]
    public class UnitTests
    {
        //#region Properties
        

        //#endregion

        //[TestInitialize]
        //public static void InitializeUnitTests()
        //{
        //    var df = new DelimitedFile("C:\\temp\\DelimitedFileLibrary\test.csv", "ASCII");
        //}

        [TestMethod]
        public void ParseLine_WellFormed01_FieldCountCorrect()
        {
            const string line = "\"data\",\"data\",\"data\"";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char) 44, (char) 34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed01 correctly.");
        }

        [TestMethod]
        public void ParseLine_WellFormed02_FieldCountCorrect()
        {
            const string line = "data,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char) 44, (char) 34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed02 correctly.");
        }

        [TestMethod]
        public void ParseLine_WellFormed03_FieldCountCorrect()
        {
            const string line = "data,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed03 correctly.");
        }

        [TestMethod]
        public void ParseLine_WellFormed04_FieldCountCorrect()
        {
            const string line = "\"da,ta\",data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed04 correctly.");
        }

        [TestMethod]
        public void ParseLine_MalFormed02_FieldCountCorrect()
        {
            const string line = "\"da\"ta\",data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line MalFormed02 correctly.");
        }

        [TestMethod]
        public void ParseLine_MalFormed03_FieldCountCorrect()
        {
            const string line = "da\"ta,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line MalFormed02 correctly.");
        }

    }
}
