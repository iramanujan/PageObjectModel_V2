using Automation.Common.Log;
using Automation.Common.Wait;
using System;
using System.Diagnostics;
using System.Linq;

namespace Automation.Common.Utils
{
    public static class ProcessUtils
    {
        /// <summary>
        /// Gets processes of the current session by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Process[] GetCurrentSessionProcessesByName(string name)
        {
            var currentSessionId = Process.GetCurrentProcess().SessionId;
            return Process.GetProcessesByName(name).Where(x => x.SessionId == currentSessionId).ToArray();
        }

        /// <summary>
        /// Kill the process
        /// </summary>
        public static void KillProcesses(string processName)
        {
            Logger.LogExecute("Kill processes if any by name {0}", processName);
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);
            Logger.LogExecute("{0} {1} processes found", processes.Length, processName);
            if (processes.Length == 0)
            {
                return;
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    //do nothing 
                }
            }
            WaitForProcessNotRunning(processName);
        }

        /// <summary>
        /// Waits for process closed. Polling System and gets process by name to track that it is not exists.
        /// As original process.WaitForExit is sometimes not enough
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        public static void WaitForProcessNotRunning(string processName)
        {
            Waiter.SpinWaitEnsureSatisfied(
                () => System.Diagnostics.Process.GetProcessesByName(processName).Length == 0, TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(1), "The process '" + processName + "' still running");
        }

        /// <summary>
        /// Waits for process opened. Polling System and gets process by name to track that it is exists
        /// </summary>
        /// <param name="processName">
        /// Name of the process
        /// </param>
        public static void WaitForProcessRunning(string processName)
        {
            Waiter.SpinWaitEnsureSatisfied(
                () => Process.GetProcessesByName(processName).Length > 0, TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(1), "The process '" + processName + "' still not running");
        }

    }
}
