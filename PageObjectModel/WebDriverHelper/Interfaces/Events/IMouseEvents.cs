using OpenQA.Selenium;

namespace WebDriverHelper.Interfaces.Events
{
    public interface IMouseEvents
    {
        void DragAndDrop(IWebElement source, IWebElement target);
        void DragAndDropToOffset(IWebElement source, int offsetX, int offsetY);
        void MoveToElement(IWebElement toElement, int offsetX, int offsetY);
        void MoveToElement(IWebElement toElement);
        void MoveByOffset(int offsetX, int offsetY);

        void Click();

        void ClickAndHold();

        void ContextClick();

        void DoubleClick();

        void Release();

    }
}
