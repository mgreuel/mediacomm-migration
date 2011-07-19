namespace MediaCommMVC.Core.Infrastructure.DependencyResolution
{
    #region Using Directives

    using System;

    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    #endregion

    public class NhRepositoryConvention : IRegistrationConvention
    {
        #region Implemented Interfaces

        #region IRegistrationConvention

        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract || !type.IsClass || type.GetInterface(type.Name.Replace("Nh", "I")) == null)
            {
                return;
            }

            Type interfaceType = type.GetInterface(type.Name.Replace("Nh", "I"));
            registry.AddType(interfaceType, type);
        }

        #endregion

        #endregion
    }
}