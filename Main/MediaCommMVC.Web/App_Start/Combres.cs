using MediaCommMVC.Web.App_Start;

using WebActivator;

[assembly: PreApplicationStartMethod(typeof(CombresHook), "PreStart")]

namespace MediaCommMVC.Web.App_Start
{
    using System.Web.Routing;

    using Combres;

    public static class CombresHook
    {
        public static void PreStart()
        {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}