using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Sessions.Data;

namespace Sessions.API.HttpActions
{
    public class ApiResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly Session[] _sessions;


        public ApiResult(HttpRequestMessage request, params Session[] sessions)
        {
            _request = request;
            _sessions = sessions;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            if (_sessions != null && _sessions.Any() && !_sessions.Contains(null))
            {
                var apiData = new ApiResultData
                {
                    Count = _sessions.Count(),
                    RetrivalDate = DateTime.UtcNow,
                    Version = "v1",
                    Results = _sessions
                };
                response = _request.CreateResponse(HttpStatusCode.OK, apiData);
            }
            else
            {
                var httpErr = new HttpError();
                httpErr["message"] = "No Sessions were found that meet your criteria";
                response = _request.CreateErrorResponse(HttpStatusCode.NotFound, httpErr);
            }

            return Task.FromResult(response);
        }
    }
}
