namespace MediaCommMVC.Core.Data.Nh.Mapping
{
    #region Using Directives

    using FluentNHibernate.Automapping;

    #endregion

    public interface IAutoMapGenerator
    {
        #region Public Methods

        AutoPersistenceModel Generate();

        #endregion
    }
}