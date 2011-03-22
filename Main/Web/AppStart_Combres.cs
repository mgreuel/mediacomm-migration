namespace MediaCommMVC
{
    #region Using Directives

    using System.Web.Routing;

    using Combres;

    #endregion

    public static class AppStart_Combres
    {
        #region Public Methods

        public static void Start()
        {
            RouteTable.Routes.AddCombresRoute("Combres");
        }

        #endregion
    }
}