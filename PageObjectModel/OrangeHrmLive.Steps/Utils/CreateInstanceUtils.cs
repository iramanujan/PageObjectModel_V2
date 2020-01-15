using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverHelper.BrowserFactory;

namespace OrangeHrmLive.Steps.Utils
{
    public static class CreateInstanceUtils
    {
        public static TBaseSteps CreateOUWSteps<TBaseSteps>(Browser browser, params object[] additionalArgs)where TBaseSteps : BaseSteps
        {
            var args = MergeArguments(new object[] { browser }, additionalArgs);
            return CreateInstance<TBaseSteps>(args);
        }
    
    public static T CreateInstance<T>(object[] args)
    {
        return (T)Activator.CreateInstance(typeof(T), args);
    }
    private static object[] MergeArguments(object[] requiredArgs, params object[] optionalArgs)
    {
        var result = new List<object>();
        if (requiredArgs != null && requiredArgs.Length != 0)
        {
            result.AddRange(requiredArgs);
        }

        if (optionalArgs != null && optionalArgs.Length != 0)
        {
            result.AddRange(optionalArgs);
        }
        return result.ToArray();
    }
}
}
