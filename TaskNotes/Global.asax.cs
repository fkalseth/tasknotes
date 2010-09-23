using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Cqrs.Configuration;
using Ninject;
using Ninject.Activation;
using TaskNotes.Infrastructure;

namespace TaskNotes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionFilter(kernel));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{taskId}",
                new { controller = "Tasks", action = "All", taskId = UrlParameter.Optional }
            );
        }

        private IKernel _kernel;

        protected void Application_Start()
        {
            _kernel = BuildKernel();
            MvcServiceLocator.SetCurrent(new NinjectMvcServiceLocator(_kernel));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters, _kernel);
            RegisterRoutes(RouteTable.Routes);
        }

        public static IKernel BuildKernel()
        {
            var kernel = new StandardKernel(
                new CqrsAspNetModule(HostingEnvironment.MapPath("~/App_Data/")),
                new GuiModule(), 
                new DomainModule());
            
            SetupMvc(kernel);

            return kernel;
        }

        private static void SetupMvc(IKernel kernel)
        {
            kernel.Bind<IControllerFactory>().To<DefaultControllerFactory>().InSingletonScope();
        }
    }
}