using Automation.Common.Config;
using Automation.Common.Interfaces.Screenshot;
using Automation.Common.Log;
using System;

namespace Automation.Common.Utils
{
    public class ScreenshotManager
    {
        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private IScreenshotable screenshotable;

        public ScreenshotManager(IScreenshotable screenshotable)
        {
            this.screenshotable = screenshotable;
        }

        public void MakeScreenshotAndUpload(string className, string methodName)
        {
            try
            {
                Logger.Log("Generating of screenshot started.");
                var screenshotName = this.GenerateScreenshotName(className, methodName);
                screenshotable.MakeAndSaveScreenshot(screenshotName);
                Logger.Log("Generating of screenshot finished.");
            }
            catch (Exception)
            {
                Logger.Error("Failed to capture screenshot. Skip logging this exception");
            }

        }

        private string GenerateScreenshotName(string className, string methodName)
        {
            return methodName + string.Format("{0:yy_MM-dd_hh-mm-ss-fff}", DateTime.Now) + ".jpeg";
        }
    }
}
