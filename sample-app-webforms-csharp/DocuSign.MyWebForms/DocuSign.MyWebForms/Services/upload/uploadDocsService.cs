using System;
using System.Collections.Generic;
using System.IO;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using DocuSign.MyWebForms.Models;
using DocuSign.MyWebForms.Models.docsupload;
using DocuSign.MyWebForms.Services.upload;

namespace DocuSign.MyWebForms.Services.Upload
{
    public class UploadDocsService: IuploadDocsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadDocsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SendEnvelopeAsync(UploadRequest request)
        {
            var accessToken = _httpContextAccessor.HttpContext.User.FindFirst("access_token").Value;
            var accountId = _httpContextAccessor.HttpContext.User.FindFirst("account_id").Value;
            var basePath = _httpContextAccessor.HttpContext.User.FindFirst("base_uri").Value;

            var apiClient = new ApiClient(basePath + "/restapi");
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);

            using var stream = new MemoryStream();
            await request.File.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            var envelope = new EnvelopeDefinition
            {
                EmailSubject = "Please sign this document",
                Status = "sent",
                Documents = new List<Document>
                {
                    new Document
                    {
                        DocumentBase64 = Convert.ToBase64String(fileBytes),
                        Name = request.File.FileName,
                        FileExtension = Path.GetExtension(request.File.FileName).TrimStart('.'),
                        DocumentId = "1"
                    }
                },
                Recipients = new Recipients
                {
                    Signers = new List<Signer>
                    {
                        new Signer
                        {
                            Email = request.Email,
                            Name = request.Name,
                            RecipientId = "1",
                            RoutingOrder = "1",
                            Tabs = new Tabs
                            {
                                SignHereTabs = new List<SignHere>
                                {
                                    new SignHere
                                    {
                                        AnchorString = "/sn1/",
                                        AnchorUnits = "pixels",
                                        AnchorYOffset = "10",
                                        AnchorXOffset = "20"
                                    }
                                }
                            }
                        }
                    },
                    CarbonCopies = string.IsNullOrEmpty(request.Cc) ? null : new List<CarbonCopy>
                    {
                        new CarbonCopy
                        {
                            Email = request.Cc,
                            Name = "CC Recipient",
                            RecipientId = "2",
                            RoutingOrder = "2"
                        }
                    }
                }
            };

            var envelopesApi = new EnvelopesApi(new DocuSignClient(apiClient.Configuration));
            var result = await envelopesApi.CreateEnvelopeAsync(accountId, envelope);
            return result.EnvelopeId;
        }
    }
}
