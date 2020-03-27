using Newtonsoft.Json;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Notes.BLL;
using Notes.BLL.DTOModels;
using Notes.DI;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Notes.App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs args)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                LoggedUser user = JsonConvert.DeserializeObject<LoggedUser>(ticket.UserData);
                UserPrinciple userPrincipal = new UserPrinciple(user.Login)
                {
                    Id = user.Id,
                    Login = user.Login,                    
                    Email = user.Email,
                    Name = user.Name,
                    Roles = user.Roles
                };
                HttpContext.Current.User = userPrincipal;
            }
        }
    }
}
