using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotoController : ControllerBase
{
    [HttpPost("UploadImage")]
    public async Task UploadImage(IFormFile file)
    {

    }
}
