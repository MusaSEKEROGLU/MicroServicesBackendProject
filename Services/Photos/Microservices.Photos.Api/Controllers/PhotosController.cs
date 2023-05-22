using Microservices.Photos.Api.Dtos;
using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Photos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos",photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream,cancellationToken);
                var returnPath = "photos/" + photo.FileName;
                
                PhotoDto photoDto = new () { Url = returnPath};
                return CreatedAtActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreatedAtActionResultInstance(Response<NoContent>.Fail("Photo is empty", 400));
        }

        //GET-POSTMAN= http://localhost:5012/api/photos?photoURL=IMG_0835.JPG
        public IActionResult PhotoDelete(string photoURL)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos",photoURL);
            if (!System.IO.File.Exists(path))
            {
                return CreatedAtActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);
            return CreatedAtActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
