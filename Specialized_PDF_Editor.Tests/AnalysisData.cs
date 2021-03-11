using iText.Kernel.Pdf;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Specialized_PDF_Editor.Tests
{
    /// <summary>
    /// Summary description for AnalysisData
    /// </summary>
    [TestClass]
    public class AnalysisData
    {
        private Analysis analysis;
        private static MemoryStream streamL;
        private PdfPage[] pages;

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
        /// Testing read header info 1-st page
        /// </summary>
        [TestMethod]
        public void ParsingHeader_1st_page()
        {
            // arrange
            string expected = "Серийный номер термотестера:  8196   Название измерения:  \r\nПериод измерения:  15 мин\r\nВыбран период с  09:00 30.06.2020  по  17:59 01.07.2020   Количество измерений:  132\r\nЗадан диапазон температур между:  2°C и 8°C  Количество нарушений: 79\r\n+ - нарушение температурного режима\r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingHeader(pages[0])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nHeader information:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read header info last page
        /// </summary>
        [TestMethod]
        public void ParsingHeader_last_page()
        {
            // arrange
            string expected = "Серийный номер термотестера:  8196   Название измерения:  \r\nПериод измерения:  15 мин\r\nВыбран период с  09:00 30.06.2020  по  17:59 01.07.2020   Количество измерений:  132\r\nЗадан диапазон температур между:  2°C и 8°C  Количество нарушений: 79\r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingHeader(pages[pageCount - 1])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nHeader information:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read names of columns into 1-st page
        /// </summary>
        [TestMethod]
        public void ParsingColumns_1st_page()
        {
            // arrange
            string expected = "Номер Дата Время Т, °C Нар.\r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingColumns(pages[0])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nName of columns:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read values from table into 1-st page
        /// </summary>
        [TestMethod]
        public void ParsingTables_1st_page()
        {
            // arrange
            string expected = "1 30.06.2020 09:12 26.0 + \r\n2 30.06.2020 09:27 26.1 + \r\n3 30.06.2020 09:42 26.2 + \r\n4 30.06.2020 09:57 26.4 + \r\n5 30.06.2020 10:12 26.5 + \r\n6 30.06.2020 10:27 26.7 + \r\n7 30.06.2020 10:42 26.8 + \r\n8 30.06.2020 10:57 27.0 + \r\n9 30.06.2020 11:12 27.1 + \r\n10 30.06.2020 11:27 27.3 + \r\n11 30.06.2020 11:42 27.4 + \r\n12 30.06.2020 11:57 27.6 + \r\n13 30.06.2020 12:12 27.7 + \r\n14 30.06.2020 12:27 27.8 + \r\n15 30.06.2020 12:42 27.9 + \r\n16 30.06.2020 12:57 28.0 + \r\n17 30.06.2020 13:12 28.1 + \r\n18 30.06.2020 13:27 32.9 + \r\n19 30.06.2020 13:42 34.4 + \r\n20 30.06.2020 13:57 34.8 + \r\n21 30.06.2020 14:12 35.3 + \r\n22 30.06.2020 14:27 35.3 + \r\n23 30.06.2020 14:42 32.7 + \r\n24 30.06.2020 14:57 31.8 + \r\n25 30.06.2020 15:12 31.3 + \r\n26 30.06.2020 15:27 30.9 + \r\n27 30.06.2020 15:42 30.5 + \r\n28 30.06.2020 15:57 30.6 + \r\n29 30.06.2020 16:12 19.3 + \r\n30 30.06.2020 16:27 15.1 + \r\n31 30.06.2020 16:42 10.2 + \r\n32 30.06.2020 16:57 8.1 + \r\n33 30.06.2020 17:12 9.3 + \r\n34 30.06.2020 17:27 8.0 + \r\n35 30.06.2020 17:42 9.0 + \r\n36 30.06.2020 17:57 8.1 + \r\n37 30.06.2020 18:12 8.2 + \r\n38 30.06.2020 18:27 8.3 + \r\n39 30.06.2020 18:42 7.8\r\n40 30.06.2020 18:57 7.7\r\n41 30.06.2020 19:12 8.2 + \r\n42 30.06.2020 19:27 8.4 + \r\n43 30.06.2020 19:42 8.3 + \r\n44 30.06.2020 19:57 7.9\r\n45 30.06.2020 20:12 7.6\r\n46 30.06.2020 20:27 7.5\r\n47 30.06.2020 20:42 8.1 + \r\n48 30.06.2020 20:57 7.6\r\n49 30.06.2020 21:12 7.9\r\n50 30.06.2020 21:27 7.8\r\n51 30.06.2020 21:42 8.4 + \r\n52 30.06.2020 21:57 7.7\r\n53 30.06.2020 22:12 7.4\r\n54 30.06.2020 22:27 7.8\r\n55 30.06.2020 22:42 8.2 + \r\n56 30.06.2020 22:57 7.6\r\n57 30.06.2020 23:12 8.1 + \r\n58 30.06.2020 23:27 7.5\r\n59 30.06.2020 23:42 8.0 + \r\n60 30.06.2020 23:57 7.4\r\n61 01.07.2020 00:12 7.9\r\n62 01.07.2020 00:27 7.4\r\n63 01.07.2020 00:42 7.8\r\n64 01.07.2020 00:57 8.0 + \r\n65 01.07.2020 01:12 7.6\r\n66 01.07.2020 01:27 7.1\r\n67 01.07.2020 01:42 7.6\r\n68 01.07.2020 01:57 7.6\r\n69 01.07.2020 02:12 7.5\r\n70 01.07.2020 02:27 7.8\r\n71 01.07.2020 02:42 7.2\r\n72 01.07.2020 02:57 7.6\r\n73 01.07.2020 03:12 7.0\r\n74 01.07.2020 03:27 7.4\r\n75 01.07.2020 03:42 7.3\r\n76 01.07.2020 03:57 7.2\r\n77 01.07.2020 04:12 7.6\r\n78 01.07.2020 04:27 7.0\r\n79 01.07.2020 04:42 7.4\r\n80 01.07.2020 04:57 6.8\r\n81 01.07.2020 05:12 7.3\r\n82 01.07.2020 05:27 7.1\r\n83 01.07.2020 05:42 7.0\r\n84 01.07.2020 05:57 7.4\r\n85 01.07.2020 06:12 6.8\r\n86 01.07.2020 06:27 7.2\r\n87 01.07.2020 06:42 6.7\r\n88 01.07.2020 06:57 7.1\r\n89 01.07.2020 07:12 6.6\r\n90 01.07.2020 07:27 7.1\r\n91 01.07.2020 07:42 6.7\r\n92 01.07.2020 07:57 7.2\r\n93 01.07.2020 08:12 6.8\r\n94 01.07.2020 08:27 6.9\r\n95 01.07.2020 08:42 7.0\r\n96 01.07.2020 08:57 6.7\r\n97 01.07.2020 09:12 6.6\r\n98 01.07.2020 09:27 7.0\r\n99 01.07.2020 09:42 7.0\r\n100 01.07.2020 09:57 7.4\r\n101 01.07.2020 10:12 9.8 + \r\n102 01.07.2020 10:27 12.1 + \r\n103 01.07.2020 10:42 18.3 + \r\n104 01.07.2020 10:57 19.4 + \r\n105 01.07.2020 11:12 19.6 + \r\n106 01.07.2020 11:27 19.7 + \r\n107 01.07.2020 11:42 19.7 + \r\n108 01.07.2020 11:57 19.7 + \r\n109 01.07.2020 12:12 25.4 + \r\n110 01.07.2020 12:27 29.0 + \r\n111 01.07.2020 12:42 28.1 + \r\n112 01.07.2020 12:57 27.9 + \r\n113 01.07.2020 13:12 27.9 + \r\n114 01.07.2020 13:27 28.1 + \r\n115 01.07.2020 13:42 28.4 + \r\n116 01.07.2020 13:57 28.5 + \r\n117 01.07.2020 14:12 28.8 + \r\n118 01.07.2020 14:27 28.8 + \r\n119 01.07.2020 14:42 28.5 + \r\n120 01.07.2020 14:57 27.8 + \r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingTables(pages[0])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nTable data:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read values from table into 2-nd page
        /// </summary>
        [TestMethod]
        public void ParsingTables_2nd_page()
        {
            // arrange
            string expected = "121 01.07.2020 15:12 27.8 + \r\n122 01.07.2020 15:27 27.4 + \r\n123 01.07.2020 15:42 26.7 + \r\n124 01.07.2020 15:57 26.4 + \r\n125 01.07.2020 16:12 26.1 + \r\n126 01.07.2020 16:27 26.3 + \r\n127 01.07.2020 16:42 26.0 + \r\n128 01.07.2020 16:57 26.1 + \r\n129 01.07.2020 17:12 26.2 + \r\n130 01.07.2020 17:27 26.2 + \r\n131 01.07.2020 17:42 26.4 + \r\n132 01.07.2020 17:57 26.7 + \r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingTables(pages[1])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nTable data:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read Oy axis info last page
        /// </summary>
        [TestMethod]
        public void ParsingOyAxis_ofChart_last_page()
        {
            // arrange
            string expected = "48\r\n42\r\n36\r\n30\r\n24\r\n18\r\n12\r\n6\r\n0\r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingOyAxis(pages[pageCount - 1])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nOy axis:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing read Ox axis info last page
        /// </summary>
        [TestMethod]
        public void ParsingOxAxis_ofChart_last_page()
        {
            // arrange
            string expected = "12:40 16:50 21:00 01:10 05:20 09:30 13:40\r\n30.06.2020 30.06.2020 30.06.2020 01.07.2020 01.07.2020 01.07.2020 01.07.2020\r\n";

            // act
            string actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                actual = analysis
                    .ParsingOxAxis(pages[pageCount - 1])
                    .ToString();

                pdf.Close();
            }

            Debug.WriteLine($"\nOx axis:");
            Debug.WriteLine($"\nActual: \n{actual}");
            Debug.WriteLine($"\nExpected: \n{expected}");

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing extract data from table into 1-st page
        /// </summary>
        [TestMethod]
        public void ExtractTableData_1st_pages()
        {
            // arrange
            KeyValuePairTable<int, DateTime, float, bool>[] expected =
            {
                new KeyValuePairTable<int, DateTime, float, bool>(01, new DateTime(2020, 06, 30, 09, 12, 00), 26.0f, true),
                new KeyValuePairTable<int, DateTime, float, bool>(39, new DateTime(2020, 06, 30, 18, 42, 00), 07.8f, false),
            };

            // act
            KeyValuePairTable<int, DateTime, float, bool>[] actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                var dataString = analysis
                    .ParsingTables(pages[0]);

                var data = analysis
                    .ExtractTableData(new StringBuilder[] { dataString });

                actual = new KeyValuePairTable<int, DateTime, float, bool>[]
                {
                    data[0],
                    data[38],
                };

                pdf.Close();
            }

            Debug.WriteLine($"\nTable data:");
            for (int i = 0; i < expected.Length; i++)
            {
                Debug.WriteLine($"\nActual: \n{actual[i]}");
                Debug.WriteLine($"\nExpected: \n{expected[i]}");
            }

            // assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Key, actual[i].Key);
                Assert.AreEqual(expected[i].DateTime, actual[i].DateTime);
                Assert.AreEqual(expected[i].Value, actual[i].Value);
                Assert.AreEqual(expected[i].OOR, actual[i].OOR);
            }
        }

        /// <summary>
        /// Testing extract data from Oy axis into last page
        /// </summary>
        [TestMethod]
        public void ExtractOyAxis_last_pages()
        {
            // arrange
            int[] expected = { 48, 42, 36, 30, 24, 18, 12, 6, 0 };

            // act
            int[] actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                var dataString = analysis
                    .ParsingOyAxis(pages[pageCount - 1]);

                actual = analysis
                    .ExtractOyAxis(dataString);

                pdf.Close();
            }

            Debug.WriteLine($"\nOy axis:");
            for (int i = 0; i < expected.Length; i++)
            {
                Debug.WriteLine($"\nActual: {actual[i]}");
                Debug.WriteLine($"Expected: {expected[i]}");
            }

            // assert
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        /// <summary>
        /// Testing extract data from Ox axis into last page
        /// </summary>
        [TestMethod]
        public void ExtractOxAxis_last_pages()
        {
            // arrange
            DateTime[] expected = new DateTime[]
            {
                new DateTime(2020, 06, 30, 12, 40, 00),
                new DateTime(2020, 06, 30, 16, 50, 00),
                new DateTime(2020, 06, 30, 21, 00, 00),
                new DateTime(2020, 07, 01, 01, 10, 00),
                new DateTime(2020, 07, 01, 05, 20, 00),
                new DateTime(2020, 07, 01, 09, 30, 00),
                new DateTime(2020, 07, 01, 13, 40, 00),
            };

            // act
            DateTime[] actual;

            // extract pages
            streamL.Position = 0;
            using (var reader = new PdfReader(streamL))
            using (var pdf = new PdfDocument(reader))
            {
                int pageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                pages = Enumerable
                    .Range(0, pageCount)
                    .Select(t => pdf.GetPage(t + 1))
                    .ToArray();

                var dataString = analysis
                    .ParsingOxAxis(pages[pageCount - 1]);

                actual = analysis
                    .ExtractOxAxis(dataString);

                pdf.Close();
            }

            Debug.WriteLine($"\nOx axis:");
            for (int i = 0; i < expected.Length; i++)
            {
                Debug.WriteLine($"\nActual: {actual[i]}");
                Debug.WriteLine($"Expected: {expected[i]}");
            }

            // assert
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

    }
}
