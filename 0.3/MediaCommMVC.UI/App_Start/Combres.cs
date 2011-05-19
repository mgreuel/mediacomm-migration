[assembly: WebActivator.PreApplicationStartMethod(typeof(MediaCommMVC.Web.App_Start.CombresHook), "PreStart")]
namespace MediaCommMVC.Web.App_Start {
	using System.Web.Routing;
	using Combres;
	
    public static class CombresHook {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}