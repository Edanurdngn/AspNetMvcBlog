using AspNetMvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AspNetMvcBlog.Extensions
{
    public class EmailExtension
    {
        public static ResultEmail EmailRegex(string email)
        {
            var results = new ResultEmail();
            const string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+.(com|org|net|edu|gov|mil|biz|info|mobi)(.[A-Z]{2})?$";
            
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var result = regex.IsMatch(email);
                if (result)
                {
                results.ResultEmailList.Add(email);
                }
                else
                {
                results.ResultErrorList.Add(email);
            }
            return results;
        }
    }
}