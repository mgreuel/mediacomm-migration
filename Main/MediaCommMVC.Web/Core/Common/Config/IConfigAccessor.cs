namespace MediaCommMVC.Web.Core.Common.Config
{
    using System.Collections.Generic;

    public interface IConfigAccessor
    {
        string GetConfigValue(string key);

        IEnumerable<string> GetConfigValues(string key);

        void SaveConfigValue(string key, string value);

        void SaveConfigValues(string key, IEnumerable<string> values);
    }
}