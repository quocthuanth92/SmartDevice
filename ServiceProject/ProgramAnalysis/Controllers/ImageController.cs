using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProgramAnalysis.Helper;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProgramAnalysis.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        public async Task<HttpResponseMessage> UploadImage(string id)
        {
            DateTime today = DateTime.Today;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = Utility.PathImage;
            string path = root + "/" + today.ToString("MM-dd-yyyy");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var provider = new MultipartFormDataStreamProvider(path);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string fileName = "";
                    if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                    {
                        fileName = Guid.NewGuid().ToString();
                    }
                    fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    File.Move(file.LocalFileName, Path.Combine(path, fileName + ".jpg"));
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
