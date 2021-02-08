using System;
using System.Threading.Tasks;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Models.Impl;
using System.Collections.Generic;
using RequestProcessor.App.Services.Impl;
using RequestProcessor.App.Logging.Impl;
using RequestProcessor.App.Services;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            try
            {
                // ToDo: Null arguments should be replaced with correct implementations.

                var mainMenu = new MainMenu(new RequestPerformer(), new OptionsSource(), new Logger());
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
