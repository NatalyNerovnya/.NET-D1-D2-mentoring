namespace MvcMusicStore
{
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.WebPages;

    using Logging;

    using MvcMusicStore.Infrastructure;

    using PerformanceCounterHelper;

    public class MvcApplication : System.Web.HttpApplication
    {
        private ILogger logger;
        public MvcApplication()
        {
            bool enableLogging = ConfigurationManager.AppSettings["Logging"].AsBool();
            if (enableLogging)
            {
                this.logger = new Logger();
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("MvcMusicStore"))
            {
                counterHelper.RawValue(Counters.Login, 0);
            }
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            this.logger?.LogError(error, error.Message);
            this.logger?.LogError(error, error.Message);
        }
    }
}
