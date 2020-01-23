using Automation.Common.Log;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WebDriverHelper.HtmlAttributes;
using WebDriverHelper.HtmlElement;

namespace OrangeHrmLive.Page.Admin.SystemUsers
{
    public class SystemUsersPage : BasePage
    {

        public enum OrderBy
        {
            [Description("Ascending")]
            ASC = 0,

            [Description("Descending ")]
            DESC = 1,
        }

        public enum CellPosition
        {
            [Description("Last")]
            LAST = 0,

            [Description("First")]
            FIRST = 1,

            [Description("VALUE_BASE")]
            VALUE_BASE = 2,
        }

        private IWebDriver webDriver = new BasePage().browser.webDriver;
        private WebElementHelper webElementHelper = new WebElementHelper();
        public IWebElement UserName => this.webDriver.FindElement(By.CssSelector("#searchSystemUser_userName"));
        public IWebElement UserRole => this.webDriver.FindElement(By.CssSelector("#searchSystemUser_userType"));
        public IWebElement EmployeeName => this.webDriver.FindElement(By.CssSelector("#searchSystemUser_employeeName_empName"));
        public IWebElement Status => this.webDriver.FindElement(By.CssSelector("#searchSystemUser_status"));
        public IWebElement Search => this.webDriver.FindElement(By.CssSelector("#searchBtn"));
        public IWebElement Reset => this.webDriver.FindElement(By.CssSelector("#resetBtn"));
        public IWebElement Add => this.webDriver.FindElement(By.CssSelector("#btnAdd"));
        public IWebElement Delete => this.webDriver.FindElement(By.CssSelector("#btnDelete"));

        public ReadOnlyCollection<IWebElement> TableHeader => this.webDriver.FindElements(By.CssSelector("#resultTable > thead > tr > th > a"));


        public IWebElement GetSystemUserInformationFromTable(string ColumnName, CellPosition cellPosition = CellPosition.VALUE_BASE, string value = "", string Attribute = TagAttributes.Value, bool IsHyperLink = false)
        {
            IWebElement cell = null;
            int ColumnIndex = webElementHelper.GetColumnIndex(TableHeader, ColumnName, ColumnIndex = 2);
            string ColumnCssSelector = "#resultTable > tbody > trCHILD > td:nth-child(" + ColumnIndex + ")";
            if (IsHyperLink) ColumnCssSelector = ColumnCssSelector + " > a";

            if (cellPosition.Equals(CellPosition.VALUE_BASE))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", "");
                ReadOnlyCollection<IWebElement> cells = this.webDriver.FindElements(By.CssSelector(ColumnCssSelector));
                foreach (var item in cells)
                {
                    try
                    {
                        if (item.GetAttribute(Attribute).Trim().ToLower() == value.Trim().ToLower())
                        {
                            cell = item;
                            break;
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (item.Text.Trim().ToLower() == value.Trim().ToLower())
                            {
                                cell = item;
                                break;
                            }
                        }
                        catch (Exception ObjException2)
                        {
                            Logger.Error("Error Occured At Column" + ObjException2.Message);
                        }
                    }
                }
            }
            if (cellPosition.Equals(CellPosition.LAST))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", ":last-child");
                cell = this.webDriver.FindElement(By.CssSelector(ColumnCssSelector));
            }
            if (cellPosition.Equals(CellPosition.FIRST))
            {
                ColumnCssSelector = ColumnCssSelector.Replace("CHILD", ":first-child");
                cell = this.webDriver.FindElement(By.CssSelector(ColumnCssSelector));
            }
            return cell;
        }
    }
}
