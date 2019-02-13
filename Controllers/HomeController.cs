using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pogoda.Models;


namespace Pogoda.Controllers
{
    public class HomeController : Controller
    {
       public ViewResult Index() {
            
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            //https://openweathermap.org/current
            String baseurl = "http://api.openweathermap.org/data/2.5/forecast?q=" + Globalne.city + "," + Globalne.countryCode + "&mode=xml&appid=1f7cff01e11e11b2533f9497da9ee055";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl);
            Console.WriteLine(baseurl);
            request.Method = "POST";
            request.Accept = "application/json";
            //request.Credentials = new NetworkCredential(username, password);
            request.UserAgent = "curl/7.37.0";
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string data = "browser=Win7x64-C1|Chrome32|1024x768&url=http://www.google.com";

                streamWriter.Write(data);
            }

            var response = request.GetResponse();
            string text;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
                //Console.WriteLine(text);
            }            
            return View("MyView");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
