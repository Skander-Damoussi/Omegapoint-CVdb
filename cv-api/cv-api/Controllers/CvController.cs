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

namespace cv_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ILogger<CvController> _logger;
        private readonly IMongoRepository<CVTemplate> _cvRepository;
        private readonly IConfiguration _configuration;

        public CvController(ILogger<CvController> logger, IMongoRepository<CVTemplate> cvRepository, IConfiguration configuration)
        {
            _logger = logger;
            _cvRepository = cvRepository;
            _configuration = configuration;

            
        }


        //Test för att ladda upp cv, från lokalt
        [HttpPost]
        public async Task<IActionResult> PostCvTemplate(CVTemplate test)
        {
            //id++;
            CVTemplate cvTemplate = new CVTemplate();
            cvTemplate.Name = "TestMedUpload2";

            string filePath = @"C:\Users\glenn\source\repos\WordProcessing\WordProcessing\upload.docx";



            byte[] hej = new byte[8];

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
            CVTemplate cvTemplate = new CVTemplate();

            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload2",
            projection => projection.FileByte);

            //byte[] test = new byte[8];
            //byte[] test = cv;

            //cvTemplate.FileByte = cv;

            //cvTemplate.FileByte = System.IO.File.();

            //using MemoryStream stream = new MemoryStream()
            //{
                
            //};

            //using System.IO.FileStream filestream = new FileStream("test.docx")
            //{

            //};



            //var testtt = File( "name.docx");

            return Ok(cv);
        }

    }
}
