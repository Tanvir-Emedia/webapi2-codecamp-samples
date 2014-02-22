using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions.Data
{
    public interface ICodeCampRepository
    {
        IEnumerable<Session> GetSessions();

        Session FindByID(int id);
    }
}
