using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeepCart
{
    public static class Utility
    {
        public static byte[] ImageToByteArray(string path)
        {
            byte[] imageData = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Image/" + path));
            return imageData;
        }
    }
}