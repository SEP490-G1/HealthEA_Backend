using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Program
{
    public class ServiceResult
    {
        public string? userMsg { get; set; }
        public string? devMsg { get; set; }
        public object? data { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
