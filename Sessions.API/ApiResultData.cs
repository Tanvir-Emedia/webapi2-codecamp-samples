using System;
using System.Collections.Generic;
using Sessions.Data;

namespace Sessions.API
{
    public class ApiResultData
    {
        public DateTime RetrivalDate { get; set; }
        public int Count { get; set; }
        public string Version { get; set; }
        public IEnumerable<Session> Results { get; set; }
    }
}