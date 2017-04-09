using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using LightInject;
using System.Reflection;
using Thinktecture.IdentityModel.Owin;
using System.Security.Claims;
using System.Collections.Generic;
using Tibox.UnitOfWork;

[assembly: OwinStartup(typeof(Tibox.WebAPi.Startup))]

namespace Tibox.WebAPi
{
    public class Startup
    {
        private readonly IUnitOfWork _unit;

        public Startup()
        {
            _unit = new TiboxUnitOfWork();
        }
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
         


            app.UseBasicAuthentication(new BasicAuthenticationOptions("TiboxSecure",
               async (username, password) => await Authenticate(username, password))
                );



            app.UseWebApi(configuration);


            //inyeccion a dependencias
            ConfigureInjector(configuration);
        }

        private async Task<IEnumerable<Claim>> Authenticate(string username, string password) {

            var user = _unit.Users.ValidateUser(username, password);

            if (user == null) return null;

            return new List<Claim>
            {
                new Claim("name",user.Email)
            };

        }

        private void ConfigureInjector(HttpConfiguration configuration)
        {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("Tibox.Repository*.dll");
            container.RegisterAssembly("Tibox.UnitOfWork*.dll");
            container.RegisterApiControllers();
            container.EnableWebApi(configuration);



        }
    }
}
