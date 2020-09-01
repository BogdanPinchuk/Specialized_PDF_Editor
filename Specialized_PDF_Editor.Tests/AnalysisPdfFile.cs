using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using System.Runtime.InteropServices;

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
            int actual = analysis.PageCount;
            Debug.WriteLine($"\nNumber of page: {actual}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing pdf file on size of page
        /// </summary>
        [TestMethod]
        public void PageRotate_0degree()
        {
            // arrange
            int expected = 0;

            // act
            analysis.ExtractData();
            var actual = analysis.Pages;

            // assert
            foreach (var p in actual)
                Assert.AreEqual(expected, p.Rotation);
        }

        /// <summary>
        /// Testing pdf size in "mm"
        /// </summary>
        [TestMethod]
        public void PageSize_297x210()
        {
            // arrange
            double expected_w = 297,
                expected_h = 210;

            // act
            analysis.ExtractData();
            var actual = analysis.Pages;
            double height, winth;

            // assert
            for (int i = 0; i < actual.Length; i++)
            {
                height = Utilities.PointsToMillimeters(actual[i].Size.GetHeight());
                winth = Utilities.PointsToMillimeters(actual[0].Size.GetWidth());
                Assert.AreEqual(expected_h, height, 1);
                Assert.AreEqual(expected_w, winth, 1);

                Debug.WriteLine($"\nPage {i + 1}:\n");
                Debug.WriteLine($"\theight: {height:G4}");
                Debug.WriteLine($"\twinth: {winth:G4}");
            }
        }

        /// <summary>
        /// Method for other review of document
        /// </summary>
        [TestMethod]
        public void Testing()
        {
            analysis.ExtractData();

            foreach (var s in analysis.Pages)
                Debug.WriteLine("\n" + s.ToString() + "\n");
        }
    }
}

//TODO: 1. Orientation of page
//TODO: 2. Size of page
//TODO: 3. Size of rectangle or board
//TODO: 4. Metadata of file
//TODO: 4.1. Date of create
//TODO: 4.2. Creator
//TODO: 4.3. Version of PDF
//TODO: 4.4. Font in file ???
