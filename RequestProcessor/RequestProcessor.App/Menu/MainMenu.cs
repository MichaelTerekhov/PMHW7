using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer,
            IOptionsSource options,
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }
        private IRequestPerformer _performer;
        private IOptionsSource _options;
        private ILogger _logger;
        public async Task<int> StartAsync()
        {
            Hello();
            try
            {
                var optionsReq = await _options.GetOptionsAsync();

                var tasks = optionsReq
                    .Where(k => (k.Item1.IsValid || k.Item2.IsValid) == true)
                    .Select(k => _performer.PerformRequestAsync(k.Item1, k.Item2)).ToArray();
                await Task.WhenAll(tasks);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void Hello()
        {
            Console.WriteLine("You`re welcome to the http request processor\n" +
                "(c)Michael Terekhov");
        }
    }
}
