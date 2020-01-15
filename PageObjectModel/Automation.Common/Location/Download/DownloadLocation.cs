using Automation.Common.Log;
using System;
using System.IO;
namespace Automation.Common.Location.Download
{
    public class DownloadLocation
    {
        public string FullPath { get; }
        private readonly bool isLocal;

        public DownloadLocation(string fullPath, bool isLocal)
        {
            this.FullPath = fullPath;
            this.isLocal = isLocal;
        }

        public string PathWithLoadedFilesFromTestAssembly(string relativeToAssemblyPath)
        {
            var localResourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeToAssemblyPath);
            if (isLocal)
            {
                return localResourcePath;
            }
            var destinationPath = Path.Combine(FullPath, relativeToAssemblyPath);
            Directory.CreateDirectory(destinationPath);
            CopyFolderContent(localResourcePath, destinationPath);
            return destinationPath;
        }

        private void CopyFolderContent(string sourcePath, string destinationPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
            }
            foreach (string newPath in Directory.GetFiles(sourcePath, "*",
                SearchOption.AllDirectories))
            {
                var sourceFilePath = newPath;
                var destFilePath = newPath.Replace(sourcePath, destinationPath);
                if (!File.Exists(destFilePath))
                {
                    File.Copy(sourceFilePath, destFilePath, false);
                }
            }
        }

        internal static DownloadLocation Create(string nameIdentifier, bool isLocal, string rootUploadLocation)
        {
            var path = CreateWebDriverDirectory(nameIdentifier, rootUploadLocation);
            return new DownloadLocation(path, isLocal);
        }

        public static string CreateWebDriverDirectory(string browserName, string rootDownloadLocation)
        {
            var donwloadFolderName = $"{browserName}-{Guid.NewGuid():N}";
            var uniqueFolderLocation = Path.Combine(rootDownloadLocation, donwloadFolderName);
            Logger.LogExecute($"Create folder for webdriver with path {uniqueFolderLocation}");
            if (!Directory.Exists(uniqueFolderLocation))
            {
                try
                {
                    Directory.CreateDirectory(uniqueFolderLocation);
                }
                catch (Exception)
                {
                    Logger.Error($"Could not create folder with path {uniqueFolderLocation}");
                    throw;
                }
            }
            return uniqueFolderLocation;
        }
    }
}
