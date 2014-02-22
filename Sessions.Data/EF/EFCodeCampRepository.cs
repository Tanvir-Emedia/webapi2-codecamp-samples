using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions.Data.EF
{
    public class EFCodeCampRepository: ICodeCampRepository
    {
        private readonly CodeCampContext _ctx;
        public EFCodeCampRepository()
        {
            _ctx = new CodeCampContext();
        }
        public IEnumerable<Session> GetSessions()
        {
            return _ctx.Sessions.ToList();
        }

        public Session FindByID(int id)
        {
            return _ctx.Sessions.SingleOrDefault(s => s.ID == id);
        }
    }
}
