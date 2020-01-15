using Automation.Common.Log;
using Automation.Common.Wait;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using TestStack.White.Configuration;
using TestStack.White.UIItems.WindowItems;

namespace Automation.Common.Utils
{
    public static class WindowUtils
    {
        /// <summary>
        /// Waits for common window by condition identified by predicate.
        /// </summary>
        /// <param name="condition">The condition</param>
        /// <param name="waitTimeout">Time out wait time</param>
        /// <param name="errorMessage">The error message</param>
        /// <param name="includeModalSubWindows">If modal window is sub window</param>
        public static Window WaitForCommonWindowByCondition(Func<Window, bool> condition, TimeSpan waitTimeout, string errorMessage, bool includeModalSubWindows = false)
        {
            Window window = null;
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                var parentWindows = TestStack.White.Desktop.Instance.Windows();
                window = parentWindows.FirstOrDefault(condition);
                //if window not found, then iterate through child modal windows.
                if (window == null && includeModalSubWindows)
                {
                    foreach (var parentWindow in parentWindows)
                    {
                        window = parentWindow.ModalWindows().FirstOrDefault(condition);
                        if (window != null) break; ;
                    }
                }
                return window != null;
            },
                waitTimeout,
                TimeSpan.FromSeconds(1),
                errorMessage
            );
            return window;
        }

        /// <summary>
        /// Waits for common window by condition identified by predicate.
        /// </summary>
        /// <param name="condition">The condition</param>
        /// <param name="waitTimeout">Time out wait time</param>
        /// <param name="errorMessage">The error message</param>
        /// <param name="includeModalSubWindows">If modal window is sub window</param>
        public static Window WaitForCommonWindowByConditionNoCrash(Func<Window, bool> condition, TimeSpan waitTimeout, bool includeModalSubWindows = false)
        {
            Window window = null;
            Waiter.SpinWait(() =>
            {
                var parentWindows = TestStack.White.Desktop.Instance.Windows();
                window = parentWindows.FirstOrDefault(condition);
                //if window not found, then iterate through child modal windows.
                if (window == null && includeModalSubWindows)
                {
                    foreach (var parentWindow in parentWindows)
                    {
                        window = parentWindow.ModalWindows().FirstOrDefault(condition);
                        if (window != null) break; ;
                    }
                }
                return window != null;
            },
                waitTimeout,
                TimeSpan.FromSeconds(1)
            );
            return window;
        }

        /// <summary>
        /// Waits for common window by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="includeModalSubWindows">If modal window is sub window</param>
        public static Window WaitForCommonWindowByTitle(string title, bool includeModalSubWindows = false)
        {
            return WaitForCommonWindowByTitle(title, TimeSpan.FromSeconds(30), includeModalSubWindows);
        }

        /// <summary>
        /// Gets the window by exact title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        /// <param name="includeModalSubWindows">If modal window is sub window</param>
        public static Window WaitForCommonWindowByTitle(string title, TimeSpan waitTimeout, bool includeModalSubWindows = false)
        {
            Logger.LogExecute("Wait for window with title '{0}' to be displayed", title);
            var equalByTitleCondition = new Func<Window, bool>(w => w.Title.Equals(title));
            return WaitForCommonWindowByCondition(equalByTitleCondition, waitTimeout,
                "Window with title '" + title + "' is not found", includeModalSubWindows);
        }

        /// <summary>
        /// Waits for common window by title contains.
        /// </summary>
        /// <param name="title">The title.</param>
        public static Window WaitForCommonWindowByTitleContains(string title)
        {
            return WaitForCommonWindowByTitleContains(title, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Waits for common window by title regex.
        /// </summary>
        /// <param name="regex">The title.</param>
        public static Window WaitForCommonWindowByTitleRegex(string regex)
        {
            return WaitForCommonWindowByTitleRegex(regex, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Gets the window by part title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        public static Window WaitForCommonWindowByTitleContains(string title, TimeSpan waitTimeout)
        {
            Logger.LogExecute("Wait for window that contains '{0}' in title to be displayed", title);
            var containsTitleCondition = new Func<Window, bool>(w => w.Title.Contains(title));
            return WaitForCommonWindowByCondition(containsTitleCondition, waitTimeout,
                "Window with title that contains words '" + title + "' is not found");
        }


        /// <summary>
        /// Gets the window by regex title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        public static Window WaitForCommonWindowByTitleRegex(string regex, TimeSpan waitTimeout)
        {
            Logger.LogExecute("Wait for window that match '{0}' in title to be displayed", regex);
            var containsTitleCondition = new Func<Window, bool>(w => Regex.IsMatch(w.Title, regex));
            return WaitForCommonWindowByCondition(containsTitleCondition, waitTimeout,
                "Window with title that match regex '" + regex + "' is not found");
        }

        /// <summary>
        /// Gets the window by part title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        public static Window WaitForCommonWindowByClassAndTitleContains(string title, TimeSpan waitTimeout)
        {
            Logger.LogExecute("Wait for window that contains '{0}' in title to be displayed", title);
            var containsTitleCondition = new Func<Window, bool>(w => w.Title.Contains(title));
            return WaitForCommonWindowByCondition(containsTitleCondition, waitTimeout,
                "Window with title that contains words '" + title + "' is not found");
        }

        /// <summary>
        /// Wait until window is closed
        /// </summary>
        /// <param name="title">Window title</param>
        /// <param name="waitTimeout">Time to wait</param>
        public static void WaitForWindowNotExist(string title, TimeSpan waitTimeout)
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                var Windows = TestStack.White.Desktop.Instance.Windows();
                return Windows.Find(win => win.NameMatches(title)) == null;
            },
                waitTimeout,
                TimeSpan.FromSeconds(1),
                String.Format("Window {0} still exists", title)
            );
        }

        /// <summary>
        /// Do some action in window with waiting for only 1 sec for window.
        /// If Window is not present - error will be thrown
        /// </summary>
        /// <param name="action"></param>
        public static void DoActionWithoutWaitingForWindow(Action action)
        {
            var oldTimeOut = CoreAppXmlConfiguration.Instance.FindWindowTimeout;
            try
            {
                CoreAppXmlConfiguration.Instance.FindWindowTimeout = 1000;
                action();
            }
            finally
            {
                CoreAppXmlConfiguration.Instance.FindWindowTimeout = oldTimeOut;
            }
        }

        public static T DoActionWithoutWaitingForWindow<T>(Func<T> action)
        {
            var oldTimeOut = CoreAppXmlConfiguration.Instance.FindWindowTimeout;
            try
            {
                CoreAppXmlConfiguration.Instance.FindWindowTimeout = 1000;
                return action();
            }
            finally
            {
                CoreAppXmlConfiguration.Instance.FindWindowTimeout = oldTimeOut;
            }
        }
    }
}
