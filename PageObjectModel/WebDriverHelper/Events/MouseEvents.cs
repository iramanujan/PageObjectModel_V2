using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverHelper.BrowserFactory;

namespace WebDriverHelper.Events
{
    public class MouseEvents
    {
        private readonly IWebDriver ObjWebDriver;
        private readonly Actions ObjActions;
        public MouseEvents(IWebDriver webDriver)
        {
            this.ObjWebDriver = webDriver;
            this.ObjActions = new Actions(this.ObjWebDriver);
        }
        public MouseEvents() : this(BrowserObjectContext.browser.webDriver)
        {

        }

        public void Click(IWebElement onElement)
        {
            this.ObjActions.Click(onElement).Build().Perform();
        }

        public void ClickAndHold(IWebElement onElement)
        {
            this.ObjActions.ClickAndHold(onElement).Build().Perform();
        }

        public void ContextClick(IWebElement onElement)
        {
            this.ObjActions.ContextClick(onElement).Build().Perform();
        }

        public void DoubleClick(IWebElement onElement)
        {
            this.ObjActions.DoubleClick(onElement).Build().Perform();
        }

        public void DragAndDrop(IWebElement source, IWebElement target)
        {
            this.ObjActions.DragAndDrop(source, target).Build().Perform();
        }

        public void DragAndDropToOffset(IWebElement source, int offsetX, int offsetY)
        {
            this.ObjActions.DragAndDropToOffset(source, offsetX, offsetY).Build().Perform();
        }

        public void MoveByOffset(int offsetX, int offsetY)
        {
            this.ObjActions.MoveByOffset(offsetX, offsetY).Build().Perform();
        }

        public void MoveToElement(IWebElement toElement, int offsetX, int offsetY)
        {
            this.ObjActions.MoveToElement(toElement, offsetX, offsetY).Build().Perform();
        }

        public void MoveToElement(IWebElement toElement)
        {
            this.ObjActions.MoveToElement(toElement).Build().Perform();
        }

        public void MoveToElementAndClick(IWebElement toElement)
        {
            this.ObjActions.MoveToElement(toElement).Click().Build().Perform();
        }

        public void Release(IWebElement toElement)
        {
            this.ObjActions.Release(toElement).Build().Perform();
        }

    }
}