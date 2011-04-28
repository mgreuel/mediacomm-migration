namespace MediaCommMVC.Core.Data.Nh.Config
{
    #region Using Directives

    using FluentNHibernate.Cfg;

    #endregion

    public interface IConfigurationGenerator
    {
        #region Public Methods

        FluentConfiguration Generate();

        #endregion
    }
}