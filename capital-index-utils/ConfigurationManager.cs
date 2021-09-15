using System;

namespace capital_index_utils
{
    public class ConfigurationManager
    {
        public T GetValue<T>(string key)
        {
            string stringValue = System.Configuration.ConfigurationManager.AppSettings[key];

            if (Convert.ChangeType(stringValue, typeof(T)) is T value)
            {
                return value;
            }

            return default(T);
        }
    }
}
