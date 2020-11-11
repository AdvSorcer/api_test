using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                var saveStream = new MemoryStream(buffer);
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + filename);
                using (var fileStream = File.Create(filePath))
                {
                    saveStream.CopyTo(fileStream);
                }
            }

            return Ok();
        }
        [HttpGet]
        public string test()
        {
            return "ok";
        }
    }
}
