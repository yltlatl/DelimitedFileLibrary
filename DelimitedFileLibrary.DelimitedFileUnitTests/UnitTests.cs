using System;
using System.IO;
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

        #region LineParsing

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_WellFormed01_FieldCountCorrect()
        {
            const string line = "\"data\",\"data\",\"data\"";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char) 44, (char) 34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed01 correctly.");
        }

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_WellFormed02_FieldCountCorrect()
        {
            const string line = "data,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char) 44, (char) 34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed02 correctly.");
        }

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_WellFormed03_FieldCountCorrect()
        {
            const string line = "data,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed03 correctly.");
        }

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_WellFormed04_FieldCountCorrect()
        {
            const string line = "\"da,ta\",data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line WellFormed04 correctly.");
        }

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_MalFormed02_FieldCountCorrect()
        {
            const string line = "\"da\"ta\",data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line MalFormed02 correctly.");
        }

        [TestMethod]
        [TestCategory("Line Parsing")]
        public void ParseLine_MalFormed03_FieldCountCorrect()
        {
            const string line = "da\"ta,data,data";
            const int fieldCount = 3;
            var resultFieldCount = DelimitedFile.ParseLine(line, (char)44, (char)34, fieldCount).Count();
            Assert.IsTrue(resultFieldCount == 3, "Failed to parse line MalFormed02 correctly.");
        }

        #endregion

        #region QuoteRemoval

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8BomQuotes_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8+_quotes.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8", '\n', '\u0014', ';', '"');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes.");
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8BomCarats_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8+_carats.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8", '\n', '\u0014', ';', '^');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8BomThorns_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8+_thorns.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8");
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8Carats_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8_carats.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8", '\n', '\u0014', ';', '^');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8Quotes_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8_quotes.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8", '\n', '\u0014', ';', '"');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_Utf8Thorns_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8_thorns.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-8");
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1200Carats_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1200_carats.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-16", '\n', '\u0014', ';', '^');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1200Quotes_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1200_quotes.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-16", '\n', '\u0014', ';', '"');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1200Thorns_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1200_thorns.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "utf-16");
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1252Carats_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1252_carats.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "windows-1252", '\n', '\u0014', ';', '^');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1252Quotes_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1252_quotes.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "windows-1252", '\n', '\u0014', ';', '"');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_1252Thorns_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\1252_thorns.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "windows-1252");
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_UsAsciiCarats_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\US-ASCII_carats.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "us-ascii", '\n', '\u0014', ';', '^');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_UsAsciiQuotes_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\US-ASCII_quotes.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "us-ascii", '\n', '\u0014', ';', '"');
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }

        [TestMethod]
        [TestCategory("Quote Removal")]
        public void ReplaceQuotes_UsAsciiThorns_QuotesAccuratelyReplaced()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\US-ASCII_thorns.txt");
            var df = new DelimitedFile(cwdStrB.ToString(), "us-ascii");
            var success = CheckForBadQuoteReplacement(df);
            Assert.IsTrue(success, "At least one row failed to properly replace quotes: {0}", df.GetFieldByPosition(0));
        }
        #endregion

        #region Encoding

        [TestMethod]
        [TestCategory("Encoding")]
        public void EncodingValidation_CommonEncodings_EncodingValidationSucceeds()
        {
            var cwdStrB = new StringBuilder(Directory.GetCurrentDirectory());
            //Encoding of the underlying file is irrelevant for this test
            cwdStrB.Replace(@"bin\Debug", @"TestData\QuoteRemoval\UTF8+_quotes.txt");
            var commonEncodings = new[]
            {
                "utf-16",
                "utf-16BE",
                "Windows-1252",
                "us-ascii",
                "utf-7",
                "utf-8"
            };
            var success = true;
            foreach (var e in commonEncodings)
            {
                try
                {
                    var df = new DelimitedFile(cwdStrB.ToString(), e);
                }
                catch (ArgumentException exception)
                {
                    if (exception.ParamName.Equals("encoding"))
                    {
                        success = false;
                    }
                    else
                    {
                        throw new Exception("Unknown exception encountered.", exception);
                    }
                }
            }
            Assert.IsTrue(success, "At least one encoding failed to validate.");
        }

        #endregion
        #region HelperMethods

        private bool CheckForBadQuoteReplacement(DelimitedFile df)
        {
            while (!df.EndOfFile)
            {
                if (df.GetFieldByPosition(0).Length != 1)
                {
                    return false;
                }
                df.GetNextRecord();
            }
            return true;
        }

        #endregion

    }
}
