using Newtonsoft.Json;
using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services.Impl
{
    internal class OptionsSource : IOptionsSource
    {
        private string optionsJson;
        


        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            List<RequestOption> options =  new List<RequestOption>();
            IEnumerable<(IRequestOptions, IResponseOptions)> resultedOptions;
            try
            {
                Console.WriteLine("Reading options from file...");
                optionsJson = await File.ReadAllTextAsync("options.json");
                options = JsonConvert.DeserializeObject<List<RequestOption>>(optionsJson);
                resultedOptions = options.Select(x => ((IRequestOptions)x, (IResponseOptions)x));
                return resultedOptions;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot find file options.json");
                options.Add(new RequestOption(null,null,RequestMethod.Undefined,null,null,null));
                resultedOptions = options.Select(x => ((IRequestOptions)x, (IResponseOptions)x));
                return resultedOptions;
            }
            catch (JsonException)
            {
                Console.WriteLine("Cannot deserialize from options.json\n" +
                    "because json corrupted!");
                options.Add(new RequestOption(null, null, RequestMethod.Undefined, null, null, null));
                resultedOptions = options.Select(x => ((IRequestOptions)x, (IResponseOptions)x));
                return resultedOptions;
            }
            catch (Exception)
            {
                Console.WriteLine("Smth happened wrong!");
                options.Add(new RequestOption(null, null, RequestMethod.Undefined, null, null, null));
                resultedOptions = options.Select(x => ((IRequestOptions)x, (IResponseOptions)x));
                return resultedOptions;
            }
        }
    }
}
