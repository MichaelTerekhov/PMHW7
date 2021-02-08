using System;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Logging.Impl;
using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;
using RequestProcessor.App.Services.Impl;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer()
        {
            requestHandler = new RequestHandler(new System.Net.Http.HttpClient());
            responseHandler = new ResponseHandler();
            logger = new Logger();
        }
        public RequestPerformer(
            IRequestHandler _requestHandler,
            IResponseHandler _responseHandler,
            ILogger _logger)
        {
            requestHandler = _requestHandler;
            responseHandler = _responseHandler;
            logger = _logger;
        }
        public IRequestHandler requestHandler;
        public IResponseHandler responseHandler;
        public ILogger logger;

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            IResponse responce = null;

            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));
            try
            {
                try
                {
                    responce = await requestHandler.HandleRequestAsync(requestOptions);
                    logger.Log(responce.Content);
                }
                catch (TimeoutException ex)
                {
                    responce = new Response(false, 408, null);
                    logger.Log(ex, ex.Message);
                }
            }
            catch (Exception varietyException)
            {
                logger.Log(varietyException, varietyException.Message);
                throw new PerformException("SOrry, but we have some problems with performing this task!!!");
            }
            responseHandler.HandleResponseAsync(responce, requestOptions, responseOptions).Wait();
            return true;
        }
    }
}