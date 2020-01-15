using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using Automation.Common.Log;
using TestStack.White;

namespace Automation.Common.Utils
{
    public class OfficeUtils
    {
        private static string officeVersionRegistryPath = @"HKEY_CLASSES_ROOT\Word.Application\CurVer";
        public enum OfficeAppType
        {
            WINWORD,
            POWERPNT,
            EXCEL
        }

        /// <summary>
        /// Office app version
        /// </summary>
        public enum OfficeVersion
        {
            Unknown = 0,
            Office2000 = 10,
            Office2003 = 11,
            Office2007 = 12,
            Office2010 = 14,
            Office2013 = 15,
            Office2016 = 16
        }

        /// <summary>
        /// Get bersion of installed MS Office
        /// </summary>
        /// <returns></returns>
        public static OfficeVersion GetOfficeVersion()
        {
            var version = Registry.GetValue(officeVersionRegistryPath, null, "0") as string;
            int versionNumber = 0;
            int.TryParse(version.Substring(version.LastIndexOf(".") + 1), out versionNumber);
            return (OfficeVersion)versionNumber;
        }

        /// <summary>
        /// Close all office applications
        /// </summary>
        public static void CloseOfficeApps()
        {
            Logger.LogExecute("Close all office apps");
            foreach (OfficeAppType type in Enum.GetValues(typeof(OfficeAppType)))
            {
                Process[] processesByName = Process.GetProcessesByName(type.ToString());
                foreach (var process in processesByName)
                {
                    process.Kill();
                }
            }
        }

        /// <summary>
        /// Open MS Office file
        /// </summary>
        /// <param name="type">Type of office application</param>
        /// <param name="path">Path to file</param>
        /// <param name="cleanRecovery">Should recoverable documents be deleted?</param>
        /// <returns>Office application</returns>
        public static Application OpenOfficeFile(OfficeAppType type, string path, bool cleanRecovery = true, bool fixStartUp = true)
        {
            string arg;
            if (cleanRecovery)
            {
                ClearRecovery(type);
            }
            if (fixStartUp)
            {
                FixStartUp(type);
            }
            switch (type)
            {
                case OfficeAppType.EXCEL:
                    arg = String.Format("\"{0}\"", path);
                    break;
                case OfficeAppType.WINWORD:
                    arg = String.Format("/t \"{0}\"", path);
                    break;
                case OfficeAppType.POWERPNT:
                    arg = String.Format("\"{0}\"", path);
                    break;
                default:
                    arg = String.Format("\"{0}\"", path);
                    break;
            }
            var process = Process.Start(type.ToString(), arg);
            return Application.Attach(process);
        }

        private static string GetOfficeTypeRegistryName(OfficeAppType type)
        {
            switch (type)
            {
                case OfficeAppType.EXCEL:
                    return "Excel";
                case OfficeAppType.POWERPNT:
                    return "PowerPoint";
                case OfficeAppType.WINWORD:
                    return "Word";
                default:
                    throw new ArgumentException("Unsupported app type");
            }
        }

        /// <summary>
        /// Remove all recoverable files for selected application
        /// </summary>
        /// <param name="type">Type of office application</param>
        public static void ClearRecovery(OfficeAppType type)
        {
            Logger.LogExecute("Clear recovery files for {0}", GetOfficeTypeRegistryName(type));
            var regPath = String.Format(@"Software\Microsoft\Office\{0}.0\{1}\Resiliency", (int)GetOfficeVersion(), GetOfficeTypeRegistryName(type));
            var reg = Registry.CurrentUser.OpenSubKey(regPath, true);
            if (reg != null)
            {
                reg.DeleteSubKeyTree("DocumentRecovery", false);
            }
        }

        /// <summary>
        /// Remove startup failures
        /// </summary>
        /// <param name="type">Type of office application</param>
        public static void FixStartUp(OfficeAppType type)
        {
            Logger.LogExecute("Fix {0} startup after crash", GetOfficeTypeRegistryName(type));
            var regPath = String.Format(@"Software\Microsoft\Office\{0}.0\{1}\Resiliency", (int)GetOfficeVersion(), GetOfficeTypeRegistryName(type));
            var reg = Registry.CurrentUser.OpenSubKey(regPath, true);
            if (reg != null)
            {
                reg.DeleteSubKeyTree("StartupItems", false);
            }
        }

        /// <summary>
        /// Disable Backstage when opening or saving file
        /// </summary>
        public static void DisableBackstageOnOpening()
        {
            Logger.LogExecute("Disable Backstage when opening or saving file on Office");
            var regPath = String.Format(@"Software\Microsoft\Office\{0}.0\Common\General", (int)GetOfficeVersion());
            var reg = Registry.CurrentUser.OpenSubKey(regPath, true);
            reg.SetValue("SkipOpenAndSaveAsPlace", 1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Method will try to understand office app type by extension
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        public static OfficeAppType GetOfficeAppType(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension.Contains("do") || extension.Contains("rtf")) return OfficeAppType.WINWORD;
            if (extension.Contains("pp") || extension.Contains("pot") || extension.Contains("sld")) return OfficeAppType.POWERPNT;
            if (extension.Contains("xl") || extension.Contains("csv")) return OfficeAppType.EXCEL;
            throw new Exception($"Unknown office file type: {extension}");
        }
    }
}
