using Automation.Common.Config;
using Automation.Common.Interfaces.Screenshot;
using Automation.Common.Log;
using OpenQA.Selenium;
using System.IO;

namespace Automation.Common.Utils
{

    public abstract class WebDriverScreenshotable : IScreenshotable
    {
        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private readonly string filePath = toolConfigMember.AutomationReportPath;
        public static string ScreenshotImagPath { get; set; }
        public abstract ITakesScreenshot TakesScreenshot { get; }

        public void  MakeAndSaveScreenshot(string fileName)
        {
            ScreenshotImagPath = "";
            if (TakesScreenshot == null)
            {
                Logger.LogExecute("No screenshot would be taken as browser is not opened");
                return;
            }
            var screenshot = TakesScreenshot.GetScreenshot();
            ScreenshotImagPath = Path.Combine(filePath, fileName + ".png");
            screenshot.SaveAsFile(ScreenshotImagPath, ScreenshotImageFormat.Png);
            
        }


    }
}
