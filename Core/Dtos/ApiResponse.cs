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
        bool Sucess { get; set; }
        string Message { get; set; }
        T Data { get; set; }
        HttpStatusCode StatusCode { get; set; }


        public ApiResponse()
        {
            StatusCode = HttpStatusCode.OK;
            Sucess = true;
            Message = string.Empty;
            
        }
    }
}
