using Automation.Common.Log;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;
using NInternal = NUnit.Framework.Internal;
namespace Automation.Common.Wait
{
    public class Waiter
    {
        #region Fields
        private readonly TimeSpan _checkInterval;
        private readonly Stopwatch _stopwatch;
        private readonly TimeSpan _timeout;
        private bool _isSatisfied = true;
        private Exception lastException;
        #endregion

        #region Constructors and Destructors

        private Waiter(TimeSpan timeout): this(timeout, TimeSpan.FromSeconds(1))
        {
        }

        private Waiter(TimeSpan timeout, TimeSpan checkInterval)
        {
            this._timeout = timeout;
            this._checkInterval = checkInterval;
            this._stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Properties

        public bool IsSatisfied => _isSatisfied;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Wait for specific condition with timeout and pollingInterval = 1 second
        /// </summary>
        /// <param name="condition">The condition to wait.</param>
        /// <param name="timeout">The Timeout. If null, then 1 sec</param>
        /// <returns>True if condition is reached</returns>
        public static bool SpinWait(Func<bool> condition, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(1);
            return SpinWait(condition, (TimeSpan)timeout, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Wait for specific condition with timeout and pollingInterval
        /// </summary>
        /// <param name="condition">The condition to wait.</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="pollingInterval">The PollingInterval.</param>
        /// <returns>True if condition is reached</returns>
        public static bool SpinWait(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            return WithTimeout(timeout, pollingInterval).WaitFor(condition).IsSatisfied;
        }

        /// <summary>
        /// Wait for specific condition with timeout and pollingInterval and throw Exception is the condition is not satisfied
        /// </summary>
        /// <param name="condition">The condition to wait</param>
        /// <param name="timeOutSeconds">The Timeout in seconds</param>
        /// <param name="pollIntervalSeconds">The PollingInterval in seconds</param>
        /// <param name="exceptionMessage">Failing message</param>
        public static void SpinWaitEnsureSatisfied(Func<bool> condition, string exceptionMessage, int timeOutSeconds = 30, int pollIntervalSeconds = 1)
        {
            WithTimeout(TimeSpan.FromSeconds(timeOutSeconds), TimeSpan.FromSeconds(pollIntervalSeconds)).WaitFor(condition).EnsureSatisfied(exceptionMessage);
        }

        /// <summary>
        /// Wait for specific condition with timeout and pollingInterval and throw Exception is the condition is not satisfied
        /// </summary>
        /// <param name="condition">The condition to wait.</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="pollingInterval">The PollingInterval.</param>
        /// <param name="exceptionMessage">Failing message.</param>
        public static void SpinWaitEnsureSatisfied(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval,
            string exceptionMessage)
        {
            WithTimeout(timeout, pollingInterval).WaitFor(condition).EnsureSatisfied(exceptionMessage);
        }

        /// <summary>
        /// Create <see cref="Waiter"/> with certain timeout and pollingInterval
        /// </summary>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="pollingInterval">The PollingInterval.</param>
        /// <returns></returns>
        public static Waiter WithTimeout(TimeSpan timeout, TimeSpan pollingInterval)
        {
            return new Waiter(timeout, pollingInterval);
        }

        /// <summary>
        /// Create <see cref="Waiter"/> with the certain timeout
        /// </summary>
        /// <param name="timeout">The Timeout.</param>
        /// <returns></returns>
        public static Waiter WithTimeout(TimeSpan timeout)
        {
            return new Waiter(timeout);
        }

        /// <summary>
        /// Throw <see cref="TimeoutException"/> if <see cref="_isSatisfied"/> == false
        /// </summary>
        public void EnsureSatisfied()
        {
            if (!this._isSatisfied)
            {
                string message = string.Empty;
                if (this.lastException != null)
                {
                    message = "Check inner waiter exception.";
                }
                throw new TimeoutException(message, this.lastException);
            }
        }

        /// <summary>
        /// Throw <see cref="TimeoutException"/> if <see cref="_isSatisfied"/> == false and specify message
        /// </summary>
        /// <param name="message">The message.</param>
        public void EnsureSatisfied(string message)
        {
            if (!this._isSatisfied)
            {
                if (this.lastException != null)
                {
                    message += " |Check inner waiter exception.";
                }
                throw new TimeoutException(message, this.lastException);
            }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Logger.LogExecute("Wait for '{0}'", timeSpan);
            Thread.Sleep(timeSpan);
        }

        public static WebDriverWait Wait(IWebDriver webDriver,int numberOfSeconds)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(numberOfSeconds));
        }

        /// <summary>
        /// Base Wait which is used by other methods
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Waiter WaitFor(Func<bool> condition)
        {
            using (new NInternal.TestExecutionContext.IsolatedContext())
            {
                if (!this._isSatisfied)
                {
                    return this;
                }

                while (!this.Try(condition))
                {
                    var sleepAmount = Min(this._timeout - this._stopwatch.Elapsed, this._checkInterval);

                    if (sleepAmount < TimeSpan.Zero)
                    {
                        this._isSatisfied = false;
                        break;
                    }
                    Thread.Sleep(sleepAmount);
                }

                return this;
            }
        }

        public static bool WaitTillPageLoad(IWebDriver webDriver, int numberOfSeconds)
        {
            try
            {
                Wait(webDriver, numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Contains("complete");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout, then page is not loaded. For all other exceptions, do not catch.
            }
            return false;
        }

        #endregion

        #region Methods

        private bool Try(Func<bool> condition)
        {
            try
            {
                return condition();
            }
            catch (Exception ex)
            {
                this.lastException = ex;
                return false;
            }
        }

        private static T Min<T>(T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) < 0 ? left : right;
        }

        #endregion
    }
}
