using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.App.Models.Impl
{
    internal class Response : IResponse
    {
        public Response(bool handled, int code, string content)
        {
            Handled = handled;
            Code = code;
            Content = content;
        }
        public bool Handled { get; set; }

        public int Code { get; set; }

        public string Content { get; set; }
    }
}
