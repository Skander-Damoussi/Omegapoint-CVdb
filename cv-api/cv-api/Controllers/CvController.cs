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
using Microsoft.AspNetCore.Identity;
using cv_api.Areas.Identity.Data;


namespace cv_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ILogger<CvController> _logger;
        private readonly IMongoRepository<CVTemplate> _cvRepository;
        private readonly IMongoRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public CvController(ILogger<CvController> logger, 
            IMongoRepository<CVTemplate> cvRepository, 
            IMongoRepository<User> userRepository, 
            UserManager<ApplicationUser> userManager, 
            IConfiguration configuration)
        {
            _logger = logger;
            _cvRepository = cvRepository;
            _userRepository = userRepository;
            this.userManager = userManager;
            _configuration = configuration;

        }


        //Temporary, upload cv from local
        [HttpPost]
        public async Task<IActionResult> PostCvTemplate(CVTemplate test)
        {

            CVTemplate cvTemplate = new CVTemplate();
            cvTemplate.Name = "TestMedUpload6";
            cvTemplate.Active = true;

            string filePath = @"C:\Users\glenn\source\repos\WordProcessing\WordProcessing\testmedupload6.docx";

            cvTemplate.FileByte = System.IO.File.ReadAllBytes(filePath);

            await _cvRepository.InsertOneAsync(cvTemplate);

            return Ok();
        }

        //Test, download cv template
        [HttpGet("GetCvTemplate")]
        public async Task<IActionResult> GetCvTemplate()
        {
            //CVTemplate cvTemplate = new CVTemplate();

            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload6",
            projection => projection.FileByte).FirstOrDefault();

            

            using (var net = new System.Net.WebClient())
            {
                //Problem med return OK()
                return new FileContentResult(cv, "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
            } 

        }

        //Test, download populate cv template
        [HttpGet("GetCvDocx/{userId}")]
        public async Task<IActionResult> GetCvDocx(string userId)
        {
            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload6",
            projection => projection.FileByte).FirstOrDefault();

            //Where active == true

            //var user = _userRepository.FilterBy(
            //    filter => filter.FirstName == "konsult",
            //    projection => projection.LastName).FirstOrDefault();
 
            var user = await userManager.FindByIdAsync(userId);

            DocxCreator docxCreate = new DocxCreator();

           
            //Memorystream, byte array, make memorystream resizeable
            using MemoryStream memoryStream = new MemoryStream(0);
            {
                
                memoryStream.Write(cv,0,cv.Length);
                //cv.CopyTo(memoryStream);
                await docxCreate.CreateDocx(memoryStream, user.CV);
                cv = memoryStream.ToArray();

            }

            using (var net = new System.Net.WebClient())
            {
                //Problem med return Ok()
                return new FileContentResult(cv, "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
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
                //Pdf convert.
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
