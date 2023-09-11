using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Core.Dtos
{
    public class ApiResponse<T>
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }


        public ApiResponse()
        {
            StatusCode = HttpStatusCode.OK;
            Sucess = true;
            Message = string.Empty;
            
        }
    }
}
