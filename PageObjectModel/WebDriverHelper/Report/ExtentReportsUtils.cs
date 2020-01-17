using Automation.Common.Config;
using Automation.Common.Log;
using Automation.Common.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;
using WebDriverHelper.BrowserFactory;

namespace WebDriverHelper.Report
{
    public class ExtentReportsUtils
    {
        public static ExtentReports extentReports = null;
        public static ExtentTest test;
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private static string ScreenshotImagPath = "";
        private static readonly string filePath = toolConfigMember.AutomationReportPath;

        public void ExtentReportsSetup()
        {
            try
            {
                string Is64BitOperatingSystem = " 32 bit";
                if (System.Environment.Is64BitOperatingSystem)
                    Is64BitOperatingSystem = " 64 bit";

                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath; // project path 
                string reportPath = toolConfigMember.AutomationReportPath + "AutomationReportReport.html";
                string subKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey skey = key.OpenSubKey(subKey);
                string name = skey.GetValue("ProductName").ToString();
                extentReports = new ExtentReports();
                var htmlReporter = new ExtentHtmlReporter(reportPath);
                
                htmlReporter.Config.Theme = Theme.Standard;
                extentReports.AddSystemInfo("Environment", toolConfigMember.Environment);
                extentReports.AddSystemInfo("User Name", Environment.UserName);
                extentReports.AddSystemInfo("Window", name + Is64BitOperatingSystem);
                extentReports.AddSystemInfo("Machine Name", Environment.MachineName);
                
                extentReports.AttachReporter(htmlReporter);
                htmlReporter.LoadConfig(projectPath + "Extent-config.xml");
            }
            catch (Exception ObjException)
            {
                Logger.Error(ObjException.Message);
                throw (ObjException);
            }

        }

        public static ExtentTest CreateTest(string name, string description = "")
        {
            test = extentReports.CreateTest(name, description);
            return test;
        }

        public static void ExtentReportsTearDown()
        {
            extentReports.Flush();
        }

        public static MediaEntityModelProvider GetMediaEntityModelProvider()
        {
            MakeAndSaveScreenshot(TestContext.CurrentContext.Test.Name.Replace(" ", string.Empty)+ "_"+ RandomUtils.GetTimestamp());
            return MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotImagPath).Build();
        }

        private static void MakeAndSaveScreenshot(string fileName)
        {
            Screenshot screenshot = ((ITakesScreenshot)BrowserObjectContext.browser.webDriver).GetScreenshot();
            ScreenshotImagPath = Path.Combine(ExtentReportsUtils.filePath, fileName + ".png");
            screenshot.SaveAsFile(ScreenshotImagPath, ScreenshotImageFormat.Png);
        }

    }
}
