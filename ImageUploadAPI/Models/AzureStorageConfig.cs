using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadAPI.Models
{
    public class AzureStorageConfig
    {

        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string QueueName { get; set; }
        public string ImageContainer { get; set; }
    }
}
