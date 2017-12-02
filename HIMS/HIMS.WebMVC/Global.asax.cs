using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.WebMVC.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace HIMS.WebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.Initialize();

            // Remove data annotations validation provider 
            ModelValidatorProviders.Providers.Remove(
                ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().First());

            // dependency injection
            NinjectModule dependencesModule = new DependencesModule();
            NinjectModule serviceModule = new ServiceModule("HimsDbConnection", "HimsIdentityConnection");
            var kernel = new StandardKernel(dependencesModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            var userObject = new UserObject();

            userObject.IsAdmin = User.IsInRole("admin");
                
            HttpContext.Current.Session.Add("__userObject", userObject);
        }
    }
}
