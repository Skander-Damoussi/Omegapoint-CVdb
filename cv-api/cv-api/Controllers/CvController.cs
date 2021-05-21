﻿using System;
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
using MongoDB.Driver;

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
        [HttpPost("PostCvTemplate/")]
        public async Task<IActionResult> PostCvTemplate(CvUpload cvUpload)
        {
            CVTemplate cvTemplate = new CVTemplate();
            cvTemplate.Name = cvUpload.Name;
            var splitString = cvUpload.Base64String.Split(",");
            cvTemplate.FileByte = Convert.FromBase64String(splitString[1]);
            cvTemplate.Active = true;
            //cvTemplate.Name = "TestMedUpload6";
            //cvTemplate.Active = true;

            //string filePath = @"C:\Users\glenn\source\repos\WordProcessing\WordProcessing\testmedupload6.docx";

            //cvTemplate.FileByte = System.IO.File.ReadAllBytes(filePath);

            await _cvRepository.InsertOneAsync(cvTemplate);

            return Ok();
        }

        //Test, download cv template
        [HttpGet("GetCvTemplate/{id}")]
        public async Task<IActionResult> GetCvTemplate(string id)
        {
            CVTemplate cv = new CVTemplate();

            var cvTemplates = _cvRepository.FilterBy(
            filter => filter.Name != "").ToList();


            foreach (var item in cvTemplates)
            {
                if (item.Id.ToString() == id)
                {
                    cv = item;
                }
            }

            //var cv = _cvRepository.FilterBy(
            //filter => filter.Id.ToString() == id).FirstOrDefault();

            //var cv = _cvRepository.FilterBy(
            //filter => filter.Id.ToString() == id,
            //projection => projection.FileByte).FirstOrDefault();

            using (var net = new System.Net.WebClient())
            {
                //Problem med return OK()
                return new FileContentResult(cv.FileByte, "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
            } 

        }

        //Test, download cv template
        [HttpPost("PostGetCvTemplate/{id}")]
        public async Task<IActionResult> PostGetCvTemplate(string id)
        {
            //CVTemplate cvTemplate = new CVTemplate();

            var cv = _cvRepository.FilterBy(
            filter => filter.Name == "TestMedUpload6",
            projection => projection.FileByte).FirstOrDefault();

            //var cv = _cvRepository.FilterBy(
            //filter => filter.Id.ToString() == id,
            //projection => projection.FileByte).FirstOrDefault();

            using (var net = new System.Net.WebClient())
            {
                //Problem med return OK()
                return new FileContentResult(cv, "application/docx")
                {
                    FileDownloadName = DateTime.Now.ToString() + ".docx"
                };
            }

        }

        ////Test, download populate cv template
        //[HttpGet("GetCvDocx/{userId}")]
        //public async Task<IActionResult> GetCvDocx(string userId)
        //{
        //    var cv = _cvRepository.FilterBy(
        //    filter => filter.Name == "TestMedUpload6",
        //    projection => projection.FileByte).FirstOrDefault();

        //    //Where active == true

        //    //var user = _userRepository.FilterBy(
        //    //    filter => filter.FirstName == "konsult",
        //    //    projection => projection.LastName).FirstOrDefault();
 
        //    var user = await userManager.FindByIdAsync(userId);

        //    DocxCreator docxCreate = new DocxCreator();

        //    //Memorystream, byte array, make memorystream resizeable
        //    using MemoryStream memoryStream = new MemoryStream(0);
        //    {
                
        //        memoryStream.Write(cv,0,cv.Length);
        //        //cv.CopyTo(memoryStream);
        //        await docxCreate.CreateDocx(memoryStream, user);
        //        cv = memoryStream.ToArray();

        //    }

        //    using (var net = new System.Net.WebClient())
        //    {
        //        //Problem med return Ok()
        //        return new FileContentResult(cv, "application/docx")
        //        {
        //            FileDownloadName = DateTime.Now.ToString() + ".docx"
        //        };
        //    }
        //}

        //Test, download populate cv template
        [HttpPost("GetCvDocx")]
        public async Task<IActionResult> GetCvDocx(CvDocxDTO cvDocxDto)
        {
            //var cv = _cvRepository.FilterBy(
            //filter => filter.Name == "TestMedUpload6",
            //projection => projection.FileByte).FirstOrDefault();

            try
            {
                CVTemplate cv = new CVTemplate();

                var cvTemplates = _cvRepository.FilterBy(
                filter => filter.Name != "").ToList();

                foreach (var item in cvTemplates)
                {
                    if (item.Id.ToString() == cvDocxDto.TempId)
                    {
                        cv = item;
                    }
                }

                var user = await userManager.FindByIdAsync(cvDocxDto.UserId);

                DocxCreator docxCreate = new DocxCreator();

                if (cv.FileByte != null)
                {
                    //Memorystream, byte array, make memorystream resizeable
                    using MemoryStream memoryStream = new MemoryStream(0);
                    {

                        memoryStream.Write(cv.FileByte, 0, cv.FileByte.Length);
                        //cv.CopyTo(memoryStream);
                        await docxCreate.CreateDocx(memoryStream, user);
                        cv.FileByte = memoryStream.ToArray();

                    }

                    using (var net = new System.Net.WebClient())
                    {
                        //Problem med return Ok()
                        

                        return new FileContentResult(cv.FileByte, "application/docx")
                        {
                            FileDownloadName = DateTime.Now.ToString() + ".docx"
                        };
                        //return Ok(returnfile);
                    }
                    
                }

                else
                {
                    return StatusCode(409, "Cv kunde inte skapas, mall saknas");
                }
            }

            catch {
                return BadRequest();
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

        [HttpGet("GetAllCvTemplate")]
        public async Task<IActionResult> GetAllCvTemplate()
        {

            //var cvTemplates = _cvRepository.AsQueryable();

            var cvTemplates = _cvRepository.FilterBy(
            filter => filter.Id != null).ToList();

            //FÖrsökte med flera projections, null:ade istället filebyte i loopen

            //foreach (var item in cvTemplates)
            //{
            //    //item.StringId = item.Id.ToString();

            //    item.StringId = "Hej";

            //}

            for (int i = 0; i < cvTemplates.Count; i++)
            {
                cvTemplates[i].StringId = cvTemplates[i].Id.ToString();
                cvTemplates[i].FileByte = null;
            }

            return Ok(cvTemplates);
        }
    }    
}
