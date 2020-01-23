using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvcBlog.Models
{
    public class ResultEmail
    {
        public List<string> ResultEmailList { get; set; } = new List<string>();
        public List<string> ResultErrorList { get; set; } = new List<string>();

    }
}