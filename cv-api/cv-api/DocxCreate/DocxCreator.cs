using cv_api.Areas.Identity.Data;
using cv_api.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using A = DocumentFormat.OpenXml.Drawing;

namespace cv_api.DocxCreate
{
    public class DocxCreator
    {
        private List<Assignment> Assignments= new List<Assignment>();
        private List<string> Techniques = new List<string>();

        public async Task CreateDocx(MemoryStream memoryStream, ApplicationUser user)
        {
            await BuildData(user);
            //Ta emot datan
            await ReplaceFields(memoryStream, user.CV);

            AddTechniques(memoryStream, "techniques");//Fixa lista för techniques
            await AddAssignments(memoryStream, "assignments", user);//Skicka in assignmentslistan
            AddLanguage(memoryStream, "language");//Skicka in languagelista
            AddEducation(memoryStream, "education");//Skicka in utbildningslistan
            //ReplaceInternalImage(memoryStream, "profilePicture");//Skicka in bilden
            ReplaceInternalImage(memoryStream, "profilePicture", user.CV.consult_picture);//Skicka in bilden


        }

        public async Task BuildData(ApplicationUser user)
        {
            //Assignments = new List<Assignment>();

            foreach (var item in user.CV.consult_experience_other_list)
            {
                Assignment assignment = new Assignment();
                assignment.Id = item.id;
                assignment.Role = item.role;
                assignment.Description = item.description;

                foreach (var experience in user.Experiences)
                {
                    if(assignment.Id==experience.id)
                    {
                        //assignment.Location = ;
                        assignment.Title = experience.Title;
                        assignment.StartDate = CreateDateTime(experience.StartDate);
                        assignment.EndDate = CreateDateTime(experience.EndDate);

                        assignment.Experiences = new List<string>();

                        assignment.Experiences.AddRange(experience.Language.Where(x => !assignment.Experiences.Any(y => y == x)));
                        assignment.Experiences.AddRange(experience.Software.Where(x => !assignment.Experiences.Any(y => y == x)));

                        Techniques.AddRange(experience.Language.Where(x => !Techniques.Any(y => y == x)));
                        Techniques.AddRange(experience.Software.Where(x => !Techniques.Any(y => y == x)));


                        //foreach (var language in experience.Language)
                        //{
                        //    assignment.Experiences.Add(language);
                        //    //Bygg techniques listan
                        //    Techniques.Add(language);
                        //}

                        //foreach (var software in experience.Software)
                        //{
                        //    assignment.Experiences.Add(software);
                        //    //Bygg techniques listan
                        //    Techniques.Add(software);
                        //}
                        //foreach (var assign in experience.Assignments)
                        //{
                        //    assignment.Experiences.Add(assign);
                        //}
                    }                    
                }
                Assignments.Add(assignment);
            }
            Assignments = Assignments.OrderBy(x => x.EndDate)
                                    .ThenBy(x => x.StartDate)
                                    .ToList();
        }

        public DateTime CreateDateTime(string date)
        {
            DateTime dateTime= dateTime = new DateTime(9999, 01, 01);
            string [] dateSplit;

            if (date != null)
            {
                dateSplit=date.Split("-");
                try
                {
                    dateTime = new DateTime(int.Parse(dateSplit[0]), int.Parse(dateSplit[1]), int.Parse(dateSplit[2]));
                }
                catch (Exception ex)
                { 
                
                }
            }

            return dateTime;
        }

        public async Task ReplaceFields(MemoryStream memoryStream, CV cv)
        {
            //string story = @"Som många utvecklare trivs Andreas bäst när han får lösa" +
            //" komplexa problem och växer med utmaningar. Att ta stort" +
            //" ansvar som ensam utvecklare i projekt eller vara en dynamisk" +
            //" del av ett större team gör Michal bra vilket som. Med stor" +
            //" nyikenhet, prestigelöshet och en positiv attityd lyssnar Andreas" +
            //" gärna för att inte bara utveckla saker rätt, utan också rätt saker";

            //Bygg sträng
            string userStory = null;
            foreach (var item in cv.consult_presentations)
            {
                userStory += item;
            }

            await ReplaceTextBookmarks(memoryStream, "userName", cv.consult_name);
            await ReplaceTextBookmarks(memoryStream, "userRole", cv.consult_role);
            //await ReplaceTextBookmarks(memoryStream, "story", userStory);
            await ReplaceTextBookmarks(memoryStream, "story", cv.consult_presentations);
            await ReplaceTextBookmarks(memoryStream, "contactName", cv.sale_name);
            await ReplaceTextBookmarks(memoryStream, "contactEmail", cv.sale_email);
            await ReplaceTextBookmarks(memoryStream, "contactPhone", cv.sale_phone);
            //focusassignment
            await ReplaceTextBookmarks(memoryStream, "focusAssignmentRole", cv.consult_experience_focus_role);
            await ReplaceTextBookmarks(memoryStream, "focusAssignmentCompany", cv.consult_experience_focus_title);
            await ReplaceTextBookmarks(memoryStream, "focusAssignmentText", cv.consult_experience_focus_description);
        }

        public async Task ReplaceTextBookmarks(MemoryStream memoryStream, string bookmarkName, string input)
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

                //document.Close();
            }
        }

        public async Task ReplaceTextBookmarks(MemoryStream memoryStream, string bookmarkName, List<string> input)
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
                        //Sista först för rätt ordning
                        bookmarkText.GetFirstChild<Text>().Text = input[input.Count-1];
                    }
                }
                //Försöka med delade stycke, loopa baklänges för rätt ordning
                for (int i = input.Count-2; i >= 0; i--)
                {
                    Run run = bookmark.NextSibling<Run>();

                    Run bookmarkText = bookmark.NextSibling<Run>();

                    //Clone bookmarkparent
                    var p = bookmark.Parent.CloneNode(true);
                    Run bookmarkText2 = bookmark.NextSibling<Run>();

                    //Leta upp bookmark i clone
                    var res2 = from bm in p.Descendants<BookmarkStart>()
                               where bm.Name == bookmarkName
                               select bm;

                    var bookmark3 = res2.FirstOrDefault();
                    //Leta upp Texten i bookmark-clone
                    Run bookmarkText3 = bookmark.NextSibling<Run>();

                    if (bookmarkText != null)
                    {
                        bookmarkText3.GetFirstChild<Text>().Text = input[i];
                    }
                    bookmark.Parent.InsertAfterSelf(p);
                }
                //document.Close();
            }
        }

        public void ReplaceInternalImage(MemoryStream memoryStream, string oldImagesPlaceholderText, string image)
        {
            //Gör en fil till bytearray
            //Tillfälligt innan det finns på DB
            //string imageFilename = @"C:\Users\glenn\source\repos\WordProcessing\WordProcessing\profilepicture\profilepicture2.jpg";

            //var newImageBytes = File.ReadAllBytes(imageFilename);
            //newImageBytes = null;
            string [] splitStringImage = image.Split(",");

            var newImageBytes = Convert.FromBase64String(splitStringImage[1]);

            using (WordprocessingDocument document = WordprocessingDocument.Open(memoryStream, true)) 
            {
                //var imagesToRemove = new List<Drawing>();

                IEnumerable<Drawing> drawings = document.MainDocumentPart.Document.Descendants<Drawing>().ToList();
                foreach (Drawing drawing in drawings)
                {
                    DocProperties dpr = drawing.Descendants<DocProperties>().FirstOrDefault();
                    if (dpr != null && dpr.Name == oldImagesPlaceholderText)
                    {
                        foreach (A.Blip b in drawing.Descendants<A.Blip>().ToList())
                        {
                            OpenXmlPart imagePart = document.MainDocumentPart.GetPartById(b.Embed);

                            if (newImageBytes != null)
                            {
                                using (var writer = new BinaryWriter(imagePart.GetStream()))
                                {
                                    writer.Write(newImageBytes);
                                }
                            }
                        }
                    }
                }

                //Stäng streamen
                document.Close();
            }
        }

        public void AddTechniques(MemoryStream memoryStream, string bookmarkName)
        {
            //List<string> techniques = new List<string>(new string[] { "C#", "JavaScript", "CSS", "Node.js", "Express.JS", "PHP", "Redux", "HTML5", "WooCommerce" });

            //Loopa baklänges
            for (int i = Techniques.Count - 1; i >= 0; i--)
            {

                using (var document = WordprocessingDocument.Open(memoryStream, true))
                {

                    var mainPart = document.MainDocumentPart;


                    var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                              where bm.Name == bookmarkName
                              select bm;
                    var bookmark = res.FirstOrDefault();

                    if (i == Techniques.Count - 1)
                    {
                        if (bookmark != null)
                        {
                            Run bookmarkText = bookmark.NextSibling<Run>();

                            if (bookmarkText != null)
                            {
                                bookmarkText.GetFirstChild<Text>().Text = Techniques[i];                                
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Bookmark not found, null, " + bookmarkName);
                        }
                    }

                    else
                    {
                        if (bookmark != null)
                        {
                            Run run = bookmark.NextSibling<Run>();

                            Run bookmarkText = bookmark.NextSibling<Run>();

                            //Clone bookmarkparent
                            var p = bookmark.Parent.CloneNode(true);
                            Run bookmarkText2 = bookmark.NextSibling<Run>();

                            //Leta upp bookmarkclone i clone
                            var res2 = from bm in p.Descendants<BookmarkStart>()
                                       where bm.Name == bookmarkName
                                       select bm;
                            var bookmark3 = res2.FirstOrDefault();
                            //Leta upp Texten i bookmark-clone
                            Run bookmarkText3 = bookmark.NextSibling<Run>();
                            bookmarkText3.GetFirstChild<Text>().Text = Techniques[i];

                            //Lägg till Elementet
                            bookmark.Parent.InsertAfterSelf(p);
                        }

                        else
                        {
                            //Console.WriteLine("Bookmark not found, null, " + bookmarkName);
                        }
                    }


                    document.Save();
                    document.Close();

                }
            }


        }

        public async Task AddAssignments(MemoryStream memoryStream, string bookmarkName, ApplicationUser user)
        {
            //Assignmentslistan
            //for (int i = 0; i < 10; i++)
            //{
            //    await AddTable(memoryStream, bookmarkName);
            //}

            await AddTable(memoryStream, bookmarkName, user);
        }

        public async Task AddTable(MemoryStream memoryStream, string bookmarkName, ApplicationUser user)
        {
            //var exList = user.CV.consult_experience_other_list;           
            
            for (int i = 0; i < Assignments.Count; i++)
            {
                ////Data
                //string company = "Big Easy Self Storage AB";
                //string time = "mar 2021 - pågående";
                ////Test Datetime
                //DateTime fromDate = new DateTime(2021, 02, 24);
                //DateTime toDate = new DateTime(2021, 03, 21);
                //string city = "Älvsjö";
                //string role = "Utvecklare";
                //string text = "Andreas hoppade in i projekten för att åtgärda design buggar som uppstod i olika skärmanpassningar. " +
                //    "Han har också byggd vidare ansöknings formuläret och designad slutliga steget i formuläret vilket var BankID. " +
                //    "Hans uppgift var att skapa en design som skulle matcha Big Easys nuvarande design.";

                //Experience experience = (Experience)from ex in user.Experiences
                //                        where ex.id == exList[i].id
                //                        select ex;

                //Experience x = from ex in user.Experiences
                //         where ex.id == exList[i].id
                //        select ex;

                //foreach (Experience item in user.Experiences)
                //{
                //    if (item.id==exList[i].id)
                //    {
                //        var y = item;

                //    }
                //}                



                //string company = experience.Title;
                //string role = exList[i].role;
                //string text = exList[i].description;
                //string city = "N/A";//Ska användas till city, location
                //string fromDate = experience.StartDate;
                //string toDate =experience.EndDate;

                //string techniques = "";

                //foreach (var item in experience.Language )
                //{
                //    techniques += item + "   ";
                //}
                //foreach (var item in experience.Software)
                //{
                //    techniques += item + "   ";
                //}
                //foreach (var item in experience.Assignments)
                //{
                //    techniques += item + "   ";
                //}               

                string techniques="";
                for (int y = 0; y < Assignments[i].Experiences.Count; y++)
                {
                    if (y==Assignments[i].Experiences.Count-1)
                    {
                        techniques += Assignments[i].Experiences[y];
                    }
                    else
                    {
                        techniques += Assignments[i].Experiences[y] + "    ";
                    }
                }

                    using (var document = WordprocessingDocument.Open(memoryStream, true))
                    {
                        var doc = document.MainDocumentPart.Document;

                        //Create table
                        Table table = new Table();
                        //Create row, CantSplit row between pages
                        var tr = new TableRow(new TableRowProperties(new CantSplit()));
                        //Create tc
                        var tc1 = new TableCell(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2150" }));
                        var tc2 = new TableCell();

                        //Append data-para-run to tc, use FontStyle method (Font,Style,bool)
                        tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11, true), new Text(Assignments[i].Title))));
                        tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text(DateStringBuilder(Assignments[i].StartDate, Assignments[i].EndDate)))));
                        tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text("N/A"))));
                        tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 13, true), new Text(Assignments[i].Role))));
                        tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 11), new Text(Assignments[i].Description))));

                        //Lägg till tekniker
                        //List<string> techniques = new List<string>(new string[] { "C#", "JavaScript", "CSS", "Node.js", "Express.JS", "PHP", "Redux", "HTML5", "WooCommerce" });
                        var p = new Paragraph();
                        var r = new Run(FontStyle("Calibri", 13, true));
                        //Ändra färg på text
                        var rp = new RunProperties();
                        Color c = new Color();
                        c.Val = "777777";
                        rp.Append(c);
                        r.Append(rp);

                        r.Append(new Text(techniques));
                        p.Append(r);
                        tc2.Append(p);

                        //Append tc to tr
                        tr.Append(tc1, tc2);
                        //Append tr to table
                        table.Append(tr);

                        var mainPart = document.MainDocumentPart;
                        var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                                  where bm.Name == bookmarkName
                                  select bm;
                        var bookmark = res.SingleOrDefault();

                        if (bookmark != null)
                        {
                            var parent = bookmark.Parent;
                            parent.InsertAfterSelf(table);
                        }

                        document.Close();
                    }
                
            }
        }
        public void AddEducation(MemoryStream memoryStream, string bookmarkName)
        {
            using (var document = WordprocessingDocument.Open(memoryStream, true))
            {
                var doc = document.MainDocumentPart.Document;

                var mainPart = document.MainDocumentPart;
                var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                          where bm.Name == bookmarkName
                          select bm;
                var bookmark = res.SingleOrDefault();

                //Create table
                Table table = new Table();
                //Shading
                TableCellProperties tcp = new TableCellProperties();
                //TableRowProperties trp = new TableRowProperties();
                Shading shading = new Shading() { Color = "auto", Fill = "F7F7F7", Val = ShadingPatternValues.Clear };
                tcp.Append(shading);

                //Loopa listan
                for (int i = 0; i < 5; i++)
                {
                    //Workaround för att använda shading flera gånger
                    var useTcp1 = tcp.CloneNode(true);
                    var useTcp2 = tcp.CloneNode(true);

                    //Create row, CantSplit row between pages
                    var tr = new TableRow(new TableRowProperties(new CantSplit()));
                    //if (i==0 || i==2)
                    //{
                    //    tr.Append(trp);
                    //}
                    //Create tc
                    var tc1 = new TableCell(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "7500" }));

                    Paragraph p1 = new Paragraph();
                    ParagraphProperties paraProperties = new ParagraphProperties();
                    Justification justification = new Justification() { Val = JustificationValues.Right };
                    paraProperties.Append(justification);
                    p1.Append(paraProperties);

                    var tc2 = new TableCell();

                    //Data
                    p1.Append((new Run(FontStyle("Calibri", 11), new Text("2018 - 2020"))));
                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11, true), new Text("Webbutvecklare - CMS"))));
                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text("Yrkeshögskola"))));
                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text("Medieinstitutet Malmö, Sverige"))));

                    //Workaround för bakgrundsfärg
                    if (i % 2 == 0)
                    {
                        tc1.Append(useTcp1);
                        tc2.Append(useTcp2);
                    }

                    tc2.Append(p1);

                    tr.Append(tc1, tc2);

                    table.Append(tr);
                }

                if (bookmark != null)
                {
                    var parent = bookmark.Parent;

                    parent.InsertAfterSelf(table);
                }

                document.Close();
            }
        }

        //Test Språk
        public void AddLanguage(MemoryStream memoryStream, string bookmarkName)
        {
            //Skicka in språklistan
            using (var document = WordprocessingDocument.Open(memoryStream, true))
            {
                var doc = document.MainDocumentPart.Document;

                var mainPart = document.MainDocumentPart;
                var res = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
                          where bm.Name == bookmarkName
                          select bm;
                var bookmark = res.SingleOrDefault();

                //Create table
                Table table = new Table();
                //Shading
                TableCellProperties tcp = new TableCellProperties();

                Shading shading = new Shading() { Color = "auto", Fill = "F7F7F7", Val = ShadingPatternValues.Clear };

                tcp.Append(shading);

                for (int i = 0; i < 3; i++)
                {
                    //Workaround för att använda shading flera gånger
                    var useTcp1 = tcp.CloneNode(true);
                    var useTcp2 = tcp.CloneNode(true);


                    var tr = new TableRow(new TableRowProperties(new CantSplit()));
                    var tc1 = new TableCell(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "7500" }));
                    var tc2 = new TableCell();

                    //Språk, nivå, varannan
                    string language = "";
                    string level = "";
                    //Tillfällig, data
                    if (i % 2 == 0)
                    {
                        //Workaround för shading
                        tc1.Append(useTcp1);
                        tc2.Append(useTcp2);
                        language = "Svenska";
                        level = "Modersmål";
                    }
                    else
                    {
                        language = "Engelska";
                        level = "Flytande";
                    }

                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11), new Text(language))));

                    Paragraph p1 = new Paragraph();
                    ParagraphProperties paraProperties = new ParagraphProperties();
                    Justification justification = new Justification() { Val = JustificationValues.Right };
                    paraProperties.Append(justification);
                    p1.Append(paraProperties);
                    p1.Append(new Run(FontStyle("Calibri", 11), new Text(level)));

                    tc2.Append(p1);

                    tr.Append(tc1, tc2);

                    table.Append(tr);
                }

                if (bookmark != null)
                {
                    var parent = bookmark.Parent;

                    parent.InsertAfterSelf(table);
                }

                document.Close();
            }
        }

        //public string DateStringBuilder(string fromDate, string toDate)
        //{
        //    string dateString;

        //    DateTime FDate = new DateTime(0001, 01, 01);
        //    if (fromDate!=null)
        //    {
        //        try
        //        {
        //            string[] splitFD = fromDate.Split("-");
        //            FDate = new DateTime(int.Parse(splitFD[0]), int.Parse(splitFD[1]), int.Parse(splitFD[2]));
        //        }

        //        catch (Exception exeption)
        //        { 
        //        }

        //    }

        //    DateTime TDate = new DateTime(0001, 01, 01);
        //    if (toDate != null)
        //    {
        //        try
        //        {
        //            string[] splitTD = toDate.Split("-");
        //            TDate = new DateTime(int.Parse(splitTD[0]), int.Parse(splitTD[1]), int.Parse(splitTD[2]));
        //        }

        //        catch (Exception exeption)
        //        {
        //        }
        //    }

        //    dateString = DateStringBuilder(FDate,TDate);

        //        return dateString;
        //}

        public string DateStringBuilder(DateTime fromDate, DateTime toDate)
        {
            string dateString;

            if (toDate.Year == 9999)
            {
                dateString = fromDate.ToString("MMMM yyyy") + " - pågående";
            }
            else if (fromDate.Year == 0001 && toDate.Year == 0001)
            {
                dateString = "N/A" + " - pågående";
            }
            else if (fromDate.Year == 0001)
            {
                dateString = "N/A" + " - " + toDate.ToString("MMMM yyyy");
            }
            else
            {
                dateString = fromDate.ToString("MMMM yyyy") + " - " + toDate.ToString("MMMM yyyy");
            }

            return dateString;
        }

        public RunProperties FontStyle(string font, int size, bool boldState)
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

        public RunProperties FontStyle(string font, int size)
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
