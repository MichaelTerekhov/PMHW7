using RequestProcessor.App.Models;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services.Impl
{
    internal class ResponseHandler : IResponseHandler
    {
        private static object marker = new object();
        public ResponseHandler()
        {
        }
        public Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            System.Console.WriteLine("Building files...");
            var text = new StringBuilder();
            text.AppendLine("<------------------->\n" +
                $"[HEADER: {requestOptions.Name}] Requsted to : {requestOptions.Address} [Handeled: {response.Handled}]\n" +
                $"[STATUS CODE: {response.Code}]\n" +
                $"MAIN CONTENT:\n" +
                $"{response.Content}");
            return WriteAllText($"{responseOptions.Path}.txt", text.ToString());
        }

        private static async Task WriteAllText(string path, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
            {
                await stream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}
