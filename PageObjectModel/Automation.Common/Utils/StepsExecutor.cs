using Automation.Common.Interfaces.Screenshot;
using Automation.Common.Log;
using NUnit.Framework;
using System;
using NInternal = NUnit.Framework.Internal;

namespace Automation.Common.Utils
{
    public static class StepsExecutor
    {
        public static Exception ExecuteSafely(Action steps)
        {
            return Execute(steps, null, false, false);
        }
        public static Exception ExecuteSafelyCheckScreenshotOnFailure(Action steps, IScreenshotable screenshotable, bool onErrorTakeScreenshot)
        {
            return Execute(steps, screenshotable, onErrorTakeScreenshot, false);
        }
        /// <summary>
        /// Executes the step.
        /// </summary>
        /// <param name="steps">The steps.</param>
        /// <param name="screenshotable">The screenshotable.</param>
        /// <param name="onErrorTakeScreenshot">if set to <c>true</c> [make screenshot on failure].</param>
        /// <param name="rethrowExceptionAfterReporting">if set to <c>true</c> [rethrow exception after reporting].</param>
        private static Exception Execute(Action steps, IScreenshotable screenshotable, bool onErrorTakeScreenshot, bool rethrowExceptionAfterReporting)
        {
            using (new NInternal.TestExecutionContext.IsolatedContext())
            {
                try
                {
                    steps();
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    if (screenshotable != null && onErrorTakeScreenshot)
                    {
                        new ScreenshotManager(screenshotable).MakeScreenshotAndUpload(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.MethodName);
                    }
                    if (rethrowExceptionAfterReporting)
                    {
                        throw;
                    }
                    return exception;
                }
                return null;
            }
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static void LogException(Exception exception)
        {
            var className = TestContext.CurrentContext.Test.ClassName;
            var testName = TestContext.CurrentContext.Test.MethodName;
            Logger.Debug("The exception Message: " + exception.Message);
            Logger.Debug("The exception Stack Trace:{0}{1} ", Environment.NewLine, exception.StackTrace);
            Logger.Error("ERROR: Failed to complete steps for method {0}.{1}", className, testName);
        }
    }
}
