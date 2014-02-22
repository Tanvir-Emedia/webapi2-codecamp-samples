using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Web.Http;
using Sessions.API.Attributes;
using Sessions.API.Providers;
using Sessions.Data;
using Sessions.Data.EF;

namespace Sessions.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SetupIOC();
            SetupAuth(app);
            SetupWebApi(app, container);
        }

        private static void SetupAuth(IAppBuilder app)
        {
            const string publicClientId = "self";
           
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(publicClientId),             
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

           app.UseCookieAuthentication(new CookieAuthenticationOptions());
           

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        private static void SetupWebApi(IAppBuilder app, IContainer container)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var cors = new EnableCorsAttribute(origins: "*", methods: "*", headers: "*");
            config.EnableCors(cors);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
                );

            app.UseWebApi(config);
        }

        private IContainer SetupIOC()
        {
            var builder = new ContainerBuilder();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToArray();
           
            builder.RegisterApiControllers(assemblies);
            builder.RegisterType<EFCodeCampRepository>().As<ICodeCampRepository>().InstancePerApiRequest();

            return builder.Build();
        }
    }
}
