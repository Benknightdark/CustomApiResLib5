using System;
using System.Collections.Generic;
using System.Text;

namespace CutomApiLib.Models
{
    public class CustomResponseModel
    {
        public object errors { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public object data { get; set; }
    }
}
