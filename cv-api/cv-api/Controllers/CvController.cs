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

        [HttpPost("PostCvTemplate/")]
        public async Task<IActionResult> PostCvTemplate(CvUpload cvUpload)
        {
            try
            {
                CVTemplate cvTemplate = new CVTemplate();
                cvTemplate.Name = cvUpload.Name;
                var splitString = cvUpload.Base64String.Split(",");
                cvTemplate.FileByte = Convert.FromBase64String(splitString[1]);
                cvTemplate.Active = true;

                await _cvRepository.InsertOneAsync(cvTemplate);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("RemoveCvTemp/{id}")]
        public async Task<IActionResult> RemoveCvTemplate(string id)
        {
            try
            {
                var cvTemp = await _cvRepository.FindByIdAsync(id);

                cvTemp.Active = false;

                await _cvRepository.ReplaceOneAsync(cvTemp);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
           
        }

        [HttpGet("GetCvTemplate/{id}")]
        public async Task<IActionResult> GetCvTemplate(string id)
        {
            try
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

                using (var net = new System.Net.WebClient())
                {                   
                    return new FileContentResult(cv.FileByte, "application/docx")
                    {
                        FileDownloadName = DateTime.Now.ToString() + ".docx"
                    };
                }
            }

            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("GetCvDocx")]
        public async Task<IActionResult> GetCvDocx(CvDocxDTO cvDocxDto)
        {
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

        [HttpGet("GetAllCvTemplate")]
        public async Task<IActionResult> GetAllCvTemplate()
        {
            try
            {
                var cvTemplates = _cvRepository.FilterBy(
                filter => filter.Active == true).ToList();

                for (int i = 0; i < cvTemplates.Count; i++)
                {
                    cvTemplates[i].StringId = cvTemplates[i].Id.ToString();
                    cvTemplates[i].FileByte = null;
                }

                return Ok(cvTemplates);
            }
            catch 
            {
                return BadRequest();
            }

        }
    }    
}
