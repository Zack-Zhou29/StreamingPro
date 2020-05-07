using StreamingProcess.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StreamingProcess.Controllers
{
    public class GroupController : ApiController
    {

        public HttpResponseMessage Get()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            var appDataPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
            var input = File.ReadAllText(appDataPath + "/input.txt");
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || !input.Contains("{"))
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("0") };
            var parser = new Parser(input.Trim());
            var group = parser.ParseGroup();
            responseMessage.Content = new StringContent(group.GetScore(0).ToString());
            return responseMessage;

        }
    }

}