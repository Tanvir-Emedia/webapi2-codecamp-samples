using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions.Data
{
    public class Session
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Presenter { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }              
    }
}
