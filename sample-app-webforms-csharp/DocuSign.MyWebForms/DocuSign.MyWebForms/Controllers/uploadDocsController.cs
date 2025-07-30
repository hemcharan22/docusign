using DocuSign.MyWebForms.Services.Upload;
using DocuSign.MyWebForms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DocuSign.MyWebForms.Models.docsupload;
using DocuSign.MyWebForms.Services.upload;

namespace DocuSign.MyWebForms.Controllers
{
    [Route("api/status")]
    [ApiController]
    [Authorize]
    public class UploadDocsController : ControllerBase
    {
        private readonly IuploadDocsService _uploadDocsService;

        public UploadDocsController(IuploadDocsService uploadDocsService)
        {
            _uploadDocsService = uploadDocsService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadRequest request)
        {
            var envelopeId = await _uploadDocsService.SendEnvelopeAsync(request);
            return Ok(new { EnvelopeId = envelopeId });
        }
    }
}