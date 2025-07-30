﻿using Newtonsoft.Json;

namespace DocuSign.MyWebForms.Models.Errors;

public class ErrorDetails
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}