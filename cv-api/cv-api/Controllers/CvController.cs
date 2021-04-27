using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv_api.Models;
using cv_api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using cv_api.DocxCreate;

namespace cv_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ILogger<CvController> _logger;
        private readonly IMongoRepository<CVTemplate> _cvRepository;
        //private readonly IMongoRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public CvController(ILogger<CvController> logger, IMongoRepository<CVTemplate> cvRepository, IConfiguration configuration)
        {
            _logger = logger;
            _cvRepository = cvRepository;
            //_userRepository = userRepository;
            _configuration = configuration;

        }


        //Temporary, upload cv from local
        [HttpPost]
        public async Task<IActionResult> PostCvTemplate(CVTemplate test)
        {
            //id++;
            CVTemplate cvTemplate = new CVTemplate();
            cvTemplate.Name = "TestMedUpload2";

            string filePath = @"C:\Users\glenn\source\repos\WordProcessing\WordProcessing\upload.docx";



            //byte[] hej = new byte[8];

            cvTemplate.FileByte = System.IO.File.ReadAllBytes(filePath);

            //using MemoryStream memorystream = new MemoryStream(file)
            //{

            //};

            //await _cvRepository.InsertOneAsync(cvTemplate);
            await _cvRepository.InsertOneAsync(cvTemplate);

            return Ok();
        }

        //Test, download cv template
        [HttpGet("GetCvTemplate")]
        public async Task<IActionResult> GetCvTemplate()
        {
            //CVTemplate cvTemplate = new CVTemplate();

            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload2",
            projection => projection.FileByte).FirstOrDefault();

            using (var net = new System.Net.WebClient())
            {
                //Problem med return OK()
                return new FileContentResult(cv, "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
            }

            //Testkod

            //cvTemplate.FileByte = cv;

            //byte[] test = new byte[8];
            //byte[] test = cv;

            //cvTemplate.FileByte = cv;

            //cvTemplate.FileByte = System.IO.File.();
            //var cv=null;

            //using MemoryStream stream = new MemoryStream(cv);
            //{
            //    return Ok(stream);
            //}

            //using MemoryStream memoryStream = new MemoryStream(cv);
            //{
            //using WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(memoryStream, true);
            //{
            //    //Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

            //    //string txt = "Hello World";

            //    //// Add new text.
            //    //Paragraph para = body.AppendChild(new Paragraph());
            //    //Run run = para.AppendChild(new Run());
            //    //run.AppendChild(new Text(txt));


            //    wordprocessingDocument.Save();
            //    wordprocessingDocument.Close();
            //}
            //}

            //using MemoryStream memoryStream = new MemoryStream(cv);
            //{
            //    WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(memoryStream, true);
            //    Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            //    string txt = "Hello World";
            //    // Add new text.
            //    Paragraph para = body.AppendChild(new Paragraph());
            //    Run run = para.AppendChild(new Run());
            //    run.AppendChild(new Text(txt));
            //    wordprocessingDocument.Save();
            //    wordprocessingDocument.Close();
            //    //return Ok(wordprocessingDocument);
            //}
            //System.IO.File.ReadAllBytes(wordprocessingDocument);

            //return Ok(File(memoryStream.ToArray(),"application/docx","test.docx"));

            //using MemoryStream stream = new MemoryStream()
            //{
            // stream= _cvRepository.FilterBy(
            //filter => filter.Name == "TestMedUpload2",
            //projection => projection.FileByte)
            //};

            //using System.IO.FileStream filestream = new FileStream("test.docx")
            //{

            //};

            //var test=Convert.ToBase64String(cv);


            //var testtt = File( "name.docx");


        }

        //Test, download populate cv template
        [HttpGet("GetCvDocx")]
        public async Task<IActionResult> GetCvDocx()
        {
            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload2",
            projection => projection.FileByte).FirstOrDefault();


            DocxCreator docxCreate = new DocxCreator();

            using MemoryStream memoryStream = new MemoryStream(cv);
            {
                docxCreate.CreateDocx(memoryStream);

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
            }

            using (var net = new System.Net.WebClient())
            {
                //Problem med return Ok()
                return new FileContentResult(memoryStream.ToArray(), "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };

                //FileContentResult file = new FileContentResult(memoryStream.ToArray(), "application/docx")
                //{
                //    FileDownloadName = DateTime.Now.ToString() + ".docx"
                //};

                //return Ok(file);
            }

        }

        //Test, download populate cv template, pdf
        [HttpGet("GetCvPdf")]
        public async Task<IActionResult> GetCvPdf()
        {
            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload2",
            projection => projection.FileByte).FirstOrDefault();

            using MemoryStream memoryStream = new MemoryStream(cv);
            {
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
            }

            //Testa gör om till pdf

            using (var net = new System.Net.WebClient())
            {
                //Problem med return Ok()
                return new FileContentResult(memoryStream.ToArray(), "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
            }

        }

    }
    
}
