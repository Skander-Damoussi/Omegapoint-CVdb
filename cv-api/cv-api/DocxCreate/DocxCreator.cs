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
        private List<Assignment> Assignments = new List<Assignment>();
        private List<Assignment> Educations = new List<Assignment>();
        private List<string> Techniques = new List<string>();

        public async Task CreateDocx(MemoryStream memoryStream, ApplicationUser user)
        {
            await BuildData(user);
            await ReplaceFields(memoryStream, user.CV);

            await AddTechniques(memoryStream, "techniques");
            await AddAssignments(memoryStream, "assignments", user);
            await AddLanguage(memoryStream, "language");
            await AddEducation(memoryStream, "education");
            await ReplaceInternalImage(memoryStream, "profilePicture", user.CV.consult_picture);


        }

        public async Task BuildData(ApplicationUser user)
        {

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
                        assignment.Title = experience.Title;
                        assignment.StartDate = CreateDateTime(experience.StartDate);
                        assignment.EndDate = CreateDateTime(experience.EndDate);

                        assignment.Experiences = new List<string>();

                        assignment.Experiences.AddRange(experience.Language.Where(x => !assignment.Experiences.Any(y => y == x)));
                        assignment.Experiences.AddRange(experience.Software.Where(x => !assignment.Experiences.Any(y => y == x)));

                        Techniques.AddRange(experience.Language.Where(x => !Techniques.Any(y => y == x)));
                        Techniques.AddRange(experience.Software.Where(x => !Techniques.Any(y => y == x)));
                    }                    
                }
                Assignments.Add(assignment);
            }
            Assignments = Assignments.OrderBy(x => x.EndDate)
                                    .ThenBy(x => x.StartDate)
                                    .ToList();

            Educations = Assignments.Select(x => x).Where(x=> x.Role=="Student").ToList();
            //foreach (var item in Assignments)
            //{
            //    if (item.Role == "Student")
            //    {
            //        Educations.Add(item);
            //    }
            //}
        }

        public DateTime CreateDateTime(string date)
        {
            DateTime dateTime = new DateTime(9999, 01, 01);
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
            await ReplaceTextBookmarks(memoryStream, "userName", cv.consult_name);
            await ReplaceTextBookmarks(memoryStream, "userRole", cv.consult_role);
            await ReplaceTextBookmarks(memoryStream, "story", cv.consult_presentations);
            await ReplaceTextBookmarks(memoryStream, "contactName", cv.sale_name);
            await ReplaceTextBookmarks(memoryStream, "contactEmail", cv.sale_email);
            await ReplaceTextBookmarks(memoryStream, "contactPhone", cv.sale_phone);
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
                        //Last first
                        bookmarkText.GetFirstChild<Text>().Text = input[input.Count - 1];
                    }
                }

                if (bookmark != null)
                {
                    //Loop backwards
                    for (int i = input.Count - 2; i >= 0; i--)
                    {
                        Run run = bookmark.NextSibling<Run>();

                        Run bookmarkText = bookmark.NextSibling<Run>();

                        //Clone bookmarkparent
                        var p = bookmark.Parent.CloneNode(true);
                        Run bookmarkText2 = bookmark.NextSibling<Run>();

                        //Find bookmark in clone
                        var res2 = from bm in p.Descendants<BookmarkStart>()
                                   where bm.Name == bookmarkName
                                   select bm;

                        var bookmark3 = res2.FirstOrDefault();
                        //Find text in bookmark-clone
                        Run bookmarkText3 = bookmark.NextSibling<Run>();

                        if (bookmarkText != null)
                        {
                            bookmarkText3.GetFirstChild<Text>().Text = input[i];
                        }
                        bookmark.Parent.InsertAfterSelf(p);
                    }
                }
                
            }
        }

        public async Task ReplaceInternalImage(MemoryStream memoryStream, string oldImagesPlaceholderText, string image)
        {
            if (image != null)
            {
                string[] splitStringImage = image.Split(",");

                var newImageBytes = Convert.FromBase64String(splitStringImage[1]);

                using (WordprocessingDocument document = WordprocessingDocument.Open(memoryStream, true))
                {
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
                    document.Close();
                }
            }


        }

        public async Task AddTechniques(MemoryStream memoryStream, string bookmarkName)
        {

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

                            //Find bookmark in clone
                            var res2 = from bm in p.Descendants<BookmarkStart>()
                                       where bm.Name == bookmarkName
                                       select bm;
                            var bookmark3 = res2.FirstOrDefault();
                            //Find text in bookmark-clone
                            Run bookmarkText3 = bookmark.NextSibling<Run>();
                            bookmarkText3.GetFirstChild<Text>().Text = Techniques[i];

                            //Add element
                            bookmark.Parent.InsertAfterSelf(p);
                        }

                    }
                    document.Save();
                    document.Close();
                }
            }
        }

        public async Task AddAssignments(MemoryStream memoryStream, string bookmarkName, ApplicationUser user)
        {
            await AddTable(memoryStream, bookmarkName, user);
        }

        public async Task AddTable(MemoryStream memoryStream, string bookmarkName, ApplicationUser user)
        {   
            
            for (int i = 0; i < Assignments.Count; i++)
            {            
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
                        //tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text("Location"))));
                        tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 13, true), new Text(Assignments[i].Role))));
                        tc2.Append(new Paragraph(new Run(FontStyle("Calibri", 11), new Text(Assignments[i].Description))));

                        //Create Paragraph, Run
                        var p = new Paragraph();
                        var r = new Run(FontStyle("Calibri", 13, true));
                        //Text color
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
        public async Task AddEducation(MemoryStream memoryStream, string bookmarkName)
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

                Shading shading = new Shading() { Color = "auto", Fill = "F7F7F7", Val = ShadingPatternValues.Clear };
                tcp.Append(shading);

                int bgcCount=0;
                //Loopa backwards
                for (int i = Educations.Count-1; i >= 0; i--)
                {
                    //Clone, reuse shading
                    var useTcp1 = tcp.CloneNode(true);
                    var useTcp2 = tcp.CloneNode(true);

                    //Create row, CantSplit row between pages
                    var tr = new TableRow(new TableRowProperties(new CantSplit()));

                    //Create tc
                    var tc1 = new TableCell(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "7000" }));

                    Paragraph p1 = new Paragraph();
                    ParagraphProperties paraProperties = new ParagraphProperties();
                    Justification justification = new Justification() { Val = JustificationValues.Right };
                    paraProperties.Append(justification);
                    p1.Append(paraProperties);

                    var tc2 = new TableCell();

                    //Data 
                    p1.Append((new Run(FontStyle("Calibri", 11), new Text(DateStringBuilderEducation(Educations[i].StartDate, Educations[i].EndDate)))));
                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11, true), new Text(Educations[i].Title))));
                    tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 11), new Text(Educations[i].Description))));
                    //tc1.Append(new Paragraph(new Run(FontStyle("Calibri", 9), new Text("Medieinstitutet Malmö, Sverige"))));

                    //Backgroundcolor
                    if (bgcCount % 2 == 0)
                    {
                        tc1.Append(useTcp1);
                        tc2.Append(useTcp2);
                    }

                    tc2.Append(p1);

                    tr.Append(tc1, tc2);

                    table.Append(tr);

                    bgcCount++;
                }

                if (bookmark != null)
                {
                    var parent = bookmark.Parent;

                    parent.InsertAfterSelf(table);
                }

                document.Close();
            }
        }

        public async Task AddLanguage(MemoryStream memoryStream, string bookmarkName)
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

                Shading shading = new Shading() { Color = "auto", Fill = "F7F7F7", Val = ShadingPatternValues.Clear };

                tcp.Append(shading);

                for (int i = 0; i < 3; i++)
                {
                    //Clone shading
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

        public string DateStringBuilderEducation(DateTime fromDate, DateTime toDate)
        {
            string dateString;

            if (toDate.Year == 9999)
            {
                dateString = fromDate.ToString("yyyy") + " - pågående";
            }
            else if (fromDate.Year == 9999 && toDate.Year == 9999)
            {
                dateString = "N/A" + " - pågående";
            }
            else if (fromDate.Year == 9999)
            {
                dateString = "N/A" + " - " + toDate.ToString("yyyy");
            }
            else
            {
                dateString = fromDate.ToString("yyyy") + " - " + toDate.ToString("yyyy");
            }

            return dateString;
        }

        public string DateStringBuilder(DateTime fromDate, DateTime toDate)
        {
            string dateString;

            if (toDate.Year == 9999)
            {
                dateString = fromDate.ToString("MMMM yyyy") + " - pågående";
            }
            else if (fromDate.Year == 9999 && toDate.Year == 9999)
            {
                dateString = "N/A" + " - pågående";
            }
            else if (fromDate.Year == 9999)
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
