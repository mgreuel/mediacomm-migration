using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Linq;

using MediaCommMVC.Web.Core.Infrastructure;

using MvcMiniProfiler;
using MvcMiniProfiler.MVCHelpers;
using Microsoft.Web.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using MvcMiniProfiler.Data.EntityFramework;
//using MvcMiniProfiler.Data.Linq2Sql;

[assembly: WebActivator.PreApplicationStartMethod(
	typeof(MediaCommMVC.Web.App_Start.MiniProfilerPackage), "PreStart")]

[assembly: WebActivator.PostApplicationStartMethod(
	typeof(MediaCommMVC.Web.App_Start.MiniProfilerPackage), "PostStart")]


namespace MediaCommMVC.Web.App_Start 
{
    public static class MiniProfilerPackage
    {
        public static void PreStart()
        {

            // Be sure to restart you ASP.NET Developement server, this code will not run until you do that. 

            //TODO: See - _MINIPROFILER UPDATED Layout.cshtml
            //      For profiling to display in the UI you will have to include the line @MvcMiniProfiler.MiniProfiler.RenderIncludes() 
            //      in your master layout

            //TODO: Non SQL Server based installs can use other formatters like: new MvcMiniProfiler.SqlFormatters.InlineFormatter()
            MiniProfiler.Settings.SqlFormatter = new MvcMiniProfiler.SqlFormatters.SqlServerFormatter();

			//TODO: To profile a standard DbConnection: 
			// var profiled = new ProfiledDbConnection(cnn, MiniProfiler.Current);

            //TODO: If you are profiling EF code first try: 
			// MiniProfilerEF.Initialize();

            //Make sure the MiniProfiler handles BeginRequest and EndRequest
            DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));

            //Setup profiler for Controllers via a Global ActionFilter
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
        }

        public static void PostStart()
        {
            // Intercept ViewEngines to profile all partial views and regular views.
            // If you prefer to insert your profiling blocks manually you can comment this out
            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }
        }
    }

    public class MiniProfilerStartupModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => MiniProfiler.Start();

            // Roles are set in PostAuthenticate
            context.AuthorizeRequest += (sender, e) =>
            {
                if (!CurrentUserIsAllowedToSeeProfiler(((HttpApplication)sender).User))
                {
                    MiniProfiler.Stop(discardResults: true);
                }
            };

            context.EndRequest += (sender, e) => MiniProfiler.Stop();
        }

        private bool CurrentUserIsAllowedToSeeProfiler(IPrincipal user)
        {
            return user != null && user.IsInRole("Administrators");
        }

        public void Dispose() { }
    }
}

