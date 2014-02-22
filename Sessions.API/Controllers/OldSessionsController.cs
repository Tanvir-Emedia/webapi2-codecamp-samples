using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Sessions.Data;

namespace Sessions.API.Controllers
{
    public class OldSessionsController: ApiController
    {
         private readonly ICodeCampRepository _repo;

         public OldSessionsController(ICodeCampRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]       
        public IHttpActionResult GetAllSessions()
        {
            var sessions = _repo.GetSessions();
            if (sessions.Any())
                return Ok(sessions);
            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("groupByTime")]
        public IHttpActionResult GetSessionsByTime()
        {
            var sessions = _repo.GetSessions();
            if (sessions.Any())
                return Ok(sessions.GroupBy(s => s.StartTime).Select(s => new{Key = s.Key.ToString("HH:mm tt"), Value = s}));
            else
                return NotFound();
        }

        [HttpGet]      
        public IHttpActionResult GetSession(int id)
        {
            var session = _repo.FindByID(id);
            if (session != null)
                return Ok(session);
            else
                return NotFound();         
        }

        [HttpGet]
        [ActionName("presenters")]
        public IHttpActionResult GetPresenters()
        {
            var presenters = _repo.GetSessions().Select(s => s.Presenter).Distinct();
            if (presenters.Any())
                return Ok(presenters);
            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("presentersessions")]
        public IHttpActionResult GetPresenterSessions(string name)
        {
            var presenters = _repo.GetSessions().Where(s => s.Presenter.StartsWith(name,StringComparison.OrdinalIgnoreCase));
            if (presenters.Any())
                return Ok(presenters);
            else
                return NotFound();
        }
    }
}
