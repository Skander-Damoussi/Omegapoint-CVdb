using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.DocxCreate
{
    public class DocxCreator
    {

        public void CreateDocx(MemoryStream memoryStream)
        {
            ReplaceFields(memoryStream);
            AddAssignments(memoryStream);


            //WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(memoryStream, true);
            //Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            //string txt = "Hello World";
            //// Add new text.
            //Paragraph para = body.AppendChild(new Paragraph());
            //Run run = para.AppendChild(new Run());
            //run.AppendChild(new Text(txt));
            //run.AppendChild(new Text(txt));
            //wordprocessingDocument.Save();
            //wordprocessingDocument.Close();
            ////return Ok(wordprocessingDocument);

            ////return memoryStream;
        }

        public void ReplaceFields(MemoryStream memoryStream)
        {
            string story = @"Som många utvecklare trivs Andreas bäst när han får lösa" +
            "komplexa problem och växer med utmaningar. Att ta stort" +
            "ansvar som ensam utvecklare i projekt eller vara en dynamisk" +
            "del av ett större team gör Michal bra vilket som. Med stor" +
            "nyikenhet, prestigelöshet och en positiv attityd lyssnar Andreas" +
            "gärna för att inte bara utveckla saker rätt, utan också rätt saker";

            ReplaceTextBookmarks(memoryStream, "userName", "Andreas Andreasson");
            ReplaceTextBookmarks(memoryStream, "userNameFooter", "Andreas Andreasson");
            ReplaceTextBookmarks(memoryStream, "userRole", "Utvecklare");
            ReplaceTextBookmarks(memoryStream, "story", story);
            ReplaceTextBookmarks(memoryStream, "contactName", "Per Persson");
            ReplaceTextBookmarks(memoryStream, "contactEmail", "per.persson@omegapoint.se");
            ReplaceTextBookmarks(memoryStream, "contactPhone", "070-22 33 44");
        }

        public void ReplaceTextBookmarks(MemoryStream memoryStream, string bookmarkName, string input)
        {

            using (var document = WordprocessingDocument.Open(memoryStream, true))
            {
                var mainPart = document.MainDocumentPart;

                var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                          where bm.Name == bookmarkName
                          select bm;
                var bookmark = res.FirstOrDefault();

                if (bookmark != null)
                {
                    Run bookmarkText = bookmark.NextSibling<Run>();

                    if (bookmarkText != null)
                    {
                        bookmarkText.GetFirstChild<Text>().Text = input;
                    }
                }
            }
        }

        public void AddAssignments(MemoryStream memoryStream)
        {
            for (int i = 0; i < 10; i++)
            {
                AddTable(memoryStream);
            }
        }

        public void AddTable(MemoryStream memoryStream)
        {
            using (var document = WordprocessingDocument.Open(memoryStream, true))
            {
                var doc = document.MainDocumentPart.Document;

                //Data
                string company = "Big Easy Self Storage AB";
                string time = "mar 2021 - pågående";
                //Test Datetime
                DateTime fromDate = new DateTime(2021, 02, 24);
                DateTime toDate = new DateTime(2021, 03, 21);
                string city = "Älvsjö";
                string role = "Utvecklare";
                string text = "Andreas hoppade in i projekten för att åtgärda design buggar som uppstod i olika skärmanpassningar. " +
                    "Han har också byggd vidare ansöknings formuläret och designad slutliga steget i formuläret vilket var BankID. " +
                    "Hans uppgift var att skapa en design som skulle matcha Big Easys nuvarande design.";

                //Create table
                Table table = new Table();
                //Create row, CantSplit row between pages
                var tr = new TableRow(new TableRowProperties(new CantSplit()));
                //Create tc
                var tc1 = new TableCell(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2150" }));
                var tc2 = new TableCell();

                //Append data-para-run to tc, use FontStyle method (Font,Style,bool)
                tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11, true), new Text(company))));
                tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text(DateStringBuilder(fromDate, toDate)))));
                tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text(city))));
                tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 13, true), new Text(role))));
                tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 11), new Text(text))));
                //Append tc to tr
                tr.Append(tc1, tc2);
                //Append tr to table
                table.Append(tr);

                var mainPart = document.MainDocumentPart;
                var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                          where bm.Name == "assignments"
                          select bm;
                var bookmark = res.SingleOrDefault();

                foreach (BookmarkStart item in res)
                {
                    Console.WriteLine("Bookmark name" + item.Name);
                }

                if (bookmark != null)
                {
                    var parent = bookmark.Parent;
                    parent.InsertAfterSelf(table);

                }

                document.Close();
            }
        }
        public static string DateStringBuilder(DateTime fromDate, DateTime toDate)
        {
            string dateString;

            if (toDate.Year == 0001)
            {
                dateString = fromDate.ToString("MMMM yyyy") + " - pågående";
            }
            else
            {
                dateString = fromDate.ToString("MMMM yyyy") + " - " + toDate.ToString("MMMM yyyy");
            }

            return dateString;
        }

        public static RunProperties FontStyle(string font, int size, bool boldState)
        {
            //Recalculate size
            size = size * 2;

            RunProperties rp = new RunProperties();
            RunFonts runFont = new RunFonts() { Ascii = font };
            FontSize runFontSize = new FontSize { Val = new StringValue(size.ToString()) };
            Bold bold = new Bold { Val = boldState };
            rp.Append(runFont, runFontSize, bold);

            return rp;
        }

        public static RunProperties FontStyle(string font, int size)
        {
            //Recalculate size
            size = size * 2;

            RunProperties rp = new RunProperties();
            RunFonts runFont = new RunFonts() { Ascii = font };
            FontSize runFontSize = new FontSize { Val = new StringValue(size.ToString()) };
            rp.Append(runFont, runFontSize);

            return rp;
        }

    }

}
