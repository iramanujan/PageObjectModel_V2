using Automation.Common.Log;
using Automation.Common.Report;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.IO;

namespace OrangeHrmLive.Test
{
    [SetUpFixture]
    class OneTimeAssemblySetUp
    {
        //public static ExtentReports extentReports = null;
        //public static ExtentTest test;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //try
            //{
            //    string pth =System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            //    string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            //    string projectPath = new Uri(actualPath).LocalPath; // project path 
            //    string reportPath = projectPath + "Reports\\testreport.html";


            //    extentReports = new ExtentReports();
            //    //var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug","");
            //    //DirectoryInfo di = Directory.CreateDirectory(dir + "Test_Execution_Reports");
            //    var htmlReporter = new ExtentHtmlReporter(reportPath);//new ExtentHtmlReporter(dir + "Test_Execution_Reports"+ "\\Automation_Report" + ".html");
            //    extentReports.AddSystemInfo("Environment", "Journey of Quality");
            //    extentReports.AddSystemInfo("User Name", Environment.UserName);
            //    extentReports.AddSystemInfo("Machine Name", Environment.MachineName);
            //    extentReports.AttachReporter(htmlReporter);
            //    htmlReporter.LoadConfig(projectPath + "Extent-config.xml");
            //}
            //catch (Exception ObjException)
            //{
            //    Logger.Error(ObjException.Message);
            //    throw (ObjException);
            //}

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportsUtils.ExtentReportsTearDown();
        }
    }
}
