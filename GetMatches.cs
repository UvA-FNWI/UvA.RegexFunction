using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace UvA.RegexFunctions
{
    public static class GetMatches
    {
        [FunctionName("GetMatches")]
        public static async Task<IEnumerable<string>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            using var reader = new StreamReader(req.Body);
            var body = await reader.ReadToEndAsync();
            var msg = JsonConvert.DeserializeObject<Message>(body);
            return Regex.Matches(msg.Input, msg.Pattern).Select(m => m.Value);
        }
    }

    class Message
    {
        public string Input {get;set;}
        public string Pattern {get;set;}
    }
}
