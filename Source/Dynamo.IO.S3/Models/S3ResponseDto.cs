﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.IO.S3.Models
{
    public class S3ResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
