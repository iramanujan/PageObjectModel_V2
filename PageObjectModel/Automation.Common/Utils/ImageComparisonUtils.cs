using System.Drawing;
using XnaFan.ImageComparison;

namespace Automation.Common.Utils
{
    public class ImageComparison
    {
        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="image1Path">Path to image 1</param>
        /// <param name="image2">Image 2</param>
        /// <param name="threshold">accuracy</param>
        /// <returns>Match percent</returns>
        public static float CompareImages(string image1Path, Image image2, byte threshold = 3)
        {
            return image2.PercentageDifference(Image.FromFile(image1Path), threshold);
        }

        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="image1Path">Path to image 1</param>
        /// <param name="image2">Path to image 2</param>
        /// <param name="threshold">accuracy</param>
        /// <returns>Match percent</returns>
        public static float CompareImages(string image1Path, string image2Path, byte threshold = 3)
        {
            return ImageTool.GetPercentageDifference(image1Path, image2Path, threshold);
        }

        /// <summary>
        /// Compare images
        /// </summary>
        /// <param name="image1Path">Image 1</param>
        /// <param name="image2">Image 2</param>
        /// <param name="threshold">accuracy</param>
        /// <returns>Match percent</returns>
        public static float CompareImages(Image image1, Image image2, byte threshold = 3)
        {
            return image1.PercentageDifference(image2, threshold);
        }
    }
}
