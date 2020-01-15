using Automation.Common.Config;
using Automation.Common.Log;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Common.Report
{
    public class ExtentReportsUtils
    {
        public static ExtentReports extentReports = null;
        public static ExtentTest test;
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        public void ExtentReportsSetup()
        {
            try
            {
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath; // project path 
                string reportPath = toolConfigMember.AutomationReportPath + "AutomationReportReport.html";


                extentReports = new ExtentReports();
                //var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug","");
                //DirectoryInfo di = Directory.CreateDirectory(dir + "Test_Execution_Reports");
                var htmlReporter = new ExtentHtmlReporter(reportPath);//new ExtentHtmlReporter(dir + "Test_Execution_Reports"+ "\\Automation_Report" + ".html");
                extentReports.AddSystemInfo("Environment", "Journey of Quality");
                extentReports.AddSystemInfo("User Name", Environment.UserName);
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
    }
}
