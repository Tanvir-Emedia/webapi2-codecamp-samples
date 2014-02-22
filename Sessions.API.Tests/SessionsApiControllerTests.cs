using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FakeItEasy;
using Sessions.API.Controllers;
using Sessions.Data;
using Xunit;

namespace Sessions.API.Tests
{
    public class SessionsApiControllerTests
    {
        [Fact]
        public void GetAllSessions_Returns_Ok_When_Sessions_Found()
        {
            //Arrange
            var repo = A.Fake<ICodeCampRepository>();
            A.CallTo(() => repo.GetSessions()).Returns(new List<Session>
            {
                new Session(),new Session(),new Session()
            });

            var controller = new SessionsApiController(repo);

            //Act
            IHttpActionResult result = controller.GetAllSessions();

            //Assert
            Assert.IsType<OkNegotiatedContentResult<IEnumerable<Session>>>(result);
            var nResult = result as OkNegotiatedContentResult<IEnumerable<Session>>;

            Assert.Equal(3, nResult.Content.Count());
        }

        [Fact]
        public void GetPresenterSessions_Only_Returns_Sessions_For_Matching_Presenters()
        {
            //Arrange
            var repo = A.Fake<ICodeCampRepository>();
            A.CallTo(() => repo.GetSessions()).Returns(new List<Session>
            {
                 new Session{Presenter = "Me",Title = "My session 1"},
                 new Session{Presenter = "You",Title = "Your session 1"},
                 new Session{Presenter = "Me",Title = "My session 2"},
                 new Session{Presenter = "You",Title = "Your session 2"},
                 new Session{Presenter = "Me",Title = "My session 3"},
            });
            var controller = new SessionsApiController(repo);

            //Act
            IHttpActionResult result = controller.GetPresenterSessions("Me");

            //Assert
            var content = (result as OkNegotiatedContentResult<IEnumerable<Session>>).Content;
            var presenterName = content.Select(s => s.Presenter).Distinct();
            Assert.Equal(1, presenterName.Count());
            Assert.Equal("Me", presenterName.Single());
            Assert.Equal(3, content.Count());
        }

        [Fact]
        public void GetSession_Returns_NotFound_When_No_Sessions_Matches()
        {
            //Arrange
            var repo = A.Fake<ICodeCampRepository>();
            A.CallTo(() => repo.FindByID(A<int>._)).Returns(null);
            var controller = new SessionsApiController(repo);
            
            //Act
            var result = controller.GetSession(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
