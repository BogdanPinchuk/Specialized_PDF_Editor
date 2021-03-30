using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Specialized_PDF_Editor.Tests
{
    /// <summary>
    /// Summary description for AnalysisPdfFile
    /// </summary>
    [TestClass]
    public class AnalysisPdfFile
    {
        private Analysis analysis;
        private static MemoryStream streamL;

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
            streamL = Visual.StreamL;
        }

        /// <summary>
        /// Reset data before every testing
        /// </summary>
        [TestInitialize]
        public void CleanBeforeTesting()
        {
            analysis = new Analysis(streamL);
            analysis.ExtractMetaData();
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
            analysis.ExtractMetaData();
            int actual = analysis.PageCount;
            Trace.WriteLine($"\nNumber of page: {actual}");

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
            analysis.ExtractMetaData();
            var actual = analysis.Pages;

            // assert
            foreach (var p in actual)
                Assert.AreEqual(expected, p.Rotation);
        }

        /// <summary>
        /// Testing pdf size in "mm"
        /// </summary>
        [TestMethod]
        public void PageSize_297x210mm()
        {
            // arrange
            double expected_w = 297,
                expected_h = 210;

            // act
            var actual = analysis.Pages;

            // assert
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected_h, actual[i].Size.HeightUU, 1);
                Assert.AreEqual(expected_w, actual[i].Size.WidthUU, 1);

                Trace.WriteLine($"\nPage {i + 1}:\n");
                Trace.WriteLine($"\theight: {actual[i].Size.HeightUU:G4}");
                Trace.WriteLine($"\twinth: {actual[i].Size.WidthUU:G4}");
            }
        }

        /// <summary>
        /// Testing pdf border size in "mm"
        /// </summary>
        [TestMethod]
        public void Margin_36()
        {
            // arrange
            double expected = 36;

            // act
            analysis.ExtractMetaData();
            var actual = analysis.Margin;

            // assert
            Assert.AreEqual(expected, actual.Top, 1);
            Assert.AreEqual(expected, actual.Bottom, 1);
            Assert.AreEqual(expected, actual.Left, 1);
            Assert.AreEqual(expected, actual.Right, 1);

            Trace.WriteLine("\n" + actual);
        }

        /// <summary>
        /// Testing create and upload pdf-file into RAM memory
        /// </summary>
        [TestMethod]
        public void TestSpeed_LoadToRamMemory()
        {
            Stopwatch time = new Stopwatch();
            var str = new StringBuilder("\n");

            time.Start();
            Visual.CreateLocalFile_Testing();
            time.Stop();
            str.Append($"CreateLocalFile_Testing: {time.Elapsed.TotalMilliseconds} ms\n");

            time.Restart();
            Visual.CreateRAMFile_Testing();
            time.Stop();
            str.Append($"CreateRAMFile_Testing: {time.Elapsed.TotalMilliseconds} ms\n");

            time.Restart();
            Visual.CreateRAMData_Testing();
            time.Stop();
            str.Append($"CreateRAMData_Testing: {time.Elapsed.TotalMilliseconds} ms\n");

            Trace.WriteLine(str.ToString());
            // My result:
            // CreateLocalFile_Testing: 7933.13 ms
            // CreateRAMFile_Testing:   39.78 ms
            // CreateRAMData_Testing:   2.1 ms
        }


        /// <summary>
        /// Method for other review of document
        /// </summary>
        [TestMethod]
        public void Testing()
        {
            analysis.ExtractMetaData();

            Trace.WriteLine("\n" + analysis.Pages[0].ToString());
            Trace.WriteLine("\n" + analysis.Metadata);

        }
    }
}

//TODO: 4.4. Font in file ???
//TODO: 4.4. Size of font
//TODO: 5. Size of picture
//TODO: 5. Size of tables

