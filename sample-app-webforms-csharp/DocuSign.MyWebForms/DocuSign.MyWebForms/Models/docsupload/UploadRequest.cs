using Microsoft.AspNetCore.Http;

namespace DocuSign.MyWebForms.Models.docsupload
{
    public class UploadRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cc { get; set; }
        public IFormFile File { get; set; }
    }
}