using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverHelper.Interfaces.Events;

namespace WebDriverHelper.Events
{
    class MouseEvents : IMouseEvents
    {
        private readonly IWebDriver ObjWebDriver;
        private readonly Actions ObjActions;
        public MouseEvents(IWebDriver webDriver)
        {
            this.ObjWebDriver = webDriver;
            this.ObjActions = new Actions(this.ObjWebDriver);
        }

        public void Click()
        {
            this.ObjActions.Click().Build().Perform();
        }

        public void Click(IWebElement onElement)
        {
            this.ObjActions.Click(onElement).Build().Perform();
        }

        public void ClickAndHold(IWebElement onElement)
        {
            this.ObjActions.ClickAndHold(onElement).Build().Perform();
        }

        public void ClickAndHold()
        {
            this.ObjActions.ClickAndHold().Build().Perform();
        }

        public void ContextClick()
        {
            this.ObjActions.ContextClick().Build().Perform();
        }

        public void ContextClick(IWebElement onElement)
        {
            this.ObjActions.ContextClick(onElement).Build().Perform();
        }

        public void DoubleClick()
        {
            this.ObjActions.DoubleClick().Build().Perform();
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
            this.ObjActions.MoveByOffset(offsetX, offsetY);
        }

        public void MoveToElement(IWebElement toElement, int offsetX, int offsetY)
        {
            this.ObjActions.MoveToElement(toElement, offsetX, offsetY);
        }

        public void MoveToElement(IWebElement toElement)
        {
            this.ObjActions.MoveToElement(toElement);
        }

        public void Release()
        {
            this.ObjActions.Release();
        }

        public void Release(IWebElement toElement)
        {
            this.ObjActions.Release(toElement);
        }

    }
}