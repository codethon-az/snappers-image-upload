using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadAPI.Response
{
    public class UploadResponse
    {
        public string Status { get; set; }

        public UploadResponse(string status)
        {
            Status = status;
        }
    }
}
