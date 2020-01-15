using System;
using System.Threading;
using TestStack.White.InputDevices;
using System.Windows;
namespace Automation.Common.Extensions
{
    public static class MouseExtensions
    {
        /// <summary>
        /// Initiate Mouse Move from start point to end point
        /// </summary>
        /// <param name="mouse">Mouse instance</param>
        /// <param name="startPosition">Start position point</param>
        /// <param name="endPosition">End position point</param>
        /// <param name="pause">Pause time</param>
        public static void MouseMove(this Mouse mouse, Point startPosition, Point endPosition, int pause = 1)
        {
            mouse.Location = startPosition;

            var distanceX = endPosition.X - startPosition.X;
            var distanceY = endPosition.Y - startPosition.Y;

            var distance = Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
            var stepCount = Math.Ceiling(distance / 1);

            var stepLengthX = distanceX / stepCount;
            var stepLengthY = distanceY / stepCount;

            var currentX = startPosition.X;
            var currentY = startPosition.Y;

            for (var i = 0; i < stepCount; i++)
            {
                currentX += (int)stepLengthX;
                currentY += (int)stepLengthY;
                mouse.Location = new Point((int)currentX, (int)currentY);
                Thread.Sleep(pause);
            }
        }
    }
}
