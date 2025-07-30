using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DocuSign.MyWebForms.Models.docsupload;

namespace DocuSign.MyWebForms.Services.upload;

public interface IuploadDocsService
{
    Task<string> SendEnvelopeAsync(UploadRequest request);
}