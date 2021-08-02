using barter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace barter.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        [Route("api/Image/UploadImage")]
        public IHttpActionResult UploadImage([FromBody] Img image)
        {
            //create the response object
            ImgRes res = new ImgRes();

            try
            {
                //path
                string path = HttpContext.Current.Server.MapPath(@"~/uploads/" + image.Folder);

                //create directory if not exists
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //create the image data
                string imageName = image.Name + ".jpg";
                string imagePath = Path.Combine(path, imageName);
                byte[] imageBytes = Convert.FromBase64String(image.Base64);

                //write the image and save it
                File.WriteAllBytes(imagePath, imageBytes);

                //update the resposne object    
                res.Path = $"{Server.GetServerUrl()}/uploads/{image.Folder}/{imageName}";
                res.IsOk = true;

                return Ok(res);
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                res.IsOk = false;
                return Content(HttpStatusCode.BadRequest, res);
            }
        }
    }
}
