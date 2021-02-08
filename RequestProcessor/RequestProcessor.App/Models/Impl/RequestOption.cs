using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RequestProcessor.App.Models.Impl
{
    internal class RequestOption : IRequestOptions, IResponseOptions
    {
        public RequestOption()
        {
        }
        public RequestOption(string name, string address, RequestMethod method,
            string contentType, string body, string path)
        {
            Name = name;
            Address = address;
            Method = method;
            ContentType = contentType;
            Body = body;
            Path = path;
        }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("method")]
        public RequestMethod Method { get; set; }
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (String.IsNullOrEmpty(Address) || Method == RequestMethod.Undefined || String.IsNullOrEmpty(Path))
                {
                    return false;
                }
                else  return true;
            }
        }
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
