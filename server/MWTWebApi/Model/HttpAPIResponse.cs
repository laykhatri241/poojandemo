using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{
    public class HttpAPIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public long Size { get; set; }
        public object Content { get; set; }
    }
}
