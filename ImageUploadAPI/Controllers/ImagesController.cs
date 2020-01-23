using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadAPI.Helpers;
using ImageUploadAPI.Models;
using ImageUploadAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // make sure that appsettings.json is filled with the necessary details of the azure storage
        private readonly AzureStorageConfig storageConfig = null;

        public ImagesController(IOptions<AzureStorageConfig> config)
        {
            storageConfig = config.Value;
        }
        // GET: api/Images
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Images/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/images/upload
        [HttpPost("[action]")]
        public async Task<UploadResponse> Upload(ICollection<IFormFile> requestFiles)
        {
            bool isUploaded = false;
            string uri = "";

            //try
            //{
            //    var formFile = Request.Form.Files[0];

            //    if (StorageHelper.IsImage(formFile))
            //    {
            //        if (formFile.Length > 0)
            //        {
            //            using (Stream stream = formFile.OpenReadStream())
            //            {
            //                isUploaded = await StorageHelper.UploadFileToStorage(stream, formFile.FileName, storageConfig);
            //            }
            //        }
            //    }
            //    return new UploadResponse("woohoo");
            //}
            //catch (Exception ex)
            //{
            //    return new UploadResponse("Failzor");
            //}

            try
            {
                var files = Request.Form.Files;
                if (files.Count == 0)

                    return new UploadResponse("No files received from the upload");

                if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)

                    return new UploadResponse("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (storageConfig.ImageContainer == string.Empty)

                    return new UploadResponse("Please provide a name for your image container in the azure blob storage");

                foreach (var formFile in files)
                {
                    if (StorageHelper.IsImage(formFile))
                    {
                        if (formFile.Length > 0)
                        {
                            using (Stream stream = formFile.OpenReadStream())
                            {
                                uri = await StorageHelper.UploadFileToStorage(stream, formFile.FileName, storageConfig);
                            }
                        }
                    }
                    else
                    {
                        return new UploadResponse("Upload failed");
                    }
                }

                if (!string.IsNullOrWhiteSpace(uri))
                {
                   return new UploadResponse("success", uri);
                }
                else

                    return new UploadResponse("Upload failed");


            }
            catch (Exception ex)
            {
                return new UploadResponse(ex.Message);

            }
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
