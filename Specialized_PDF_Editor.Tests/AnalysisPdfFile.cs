using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;

namespace Specialized_PDF_Editor.Tests
{
    /// <summary>
    /// Summary description for AnalysisPdfFile
    /// </summary>
    [TestClass]
    public class AnalysisPdfFile
    {
        private Analysis analysis;
        private static MemoryStream streamC;

        /// <summary>
        /// Initialize class before testing
        /// </summary>
        [ClassInitialize]
        public static void InitializeClass(TestContext _)
        {
            MainForm form = new MainForm(new string[] 
            { 
                new FileInfo(@"Resources\File_3_pages.pdf").FullName 
            });
            form.LoadPdfForRead();
            streamC = form.streamC;
        }

        /// <summary>
        /// Reset data before every testing
        /// </summary>
        [TestInitialize]
        public void CleanBeforeTesting()
        {
            analysis = new Analysis(streamC);
        }

        /// <summary>
        /// Testing pdf file with 3 pages
        /// </summary>
        [TestMethod]
        public void PageCount_3pages()
        {
            // arrange
            int expected = 3;

            // act
            analysis.ExtractData();
            var actual = analysis.PageCount;
            Debug.WriteLine($"\nNumber of page: {actual}");

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
