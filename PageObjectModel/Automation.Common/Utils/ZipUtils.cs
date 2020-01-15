using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Automation.Common.Utils
{
    /// <summary>Methods to work with .zip archives</summary>
    public class ZipUtils
    {
        /// <summary>Get FileNames of files in Archive</summary>
        /// <param name="pathToZip">path to archive</param>
        /// <returns>List of FileNames</returns>
        public static List<String> GetContentFileNames(string pathToZip)
        {
            List<String> files = new List<String>();
            using (ZipArchive archive = ZipFile.OpenRead(pathToZip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    files.Add(entry.FullName);
                }
            }
            return files;
        }

        /// <summary>Get FileName of files with initial size = 0 in archive</summary>
        /// <param name="pathToZip">Path to archive</param>
        /// <returns>List of empty files</returns>
        public static IList<String> GetEmptyFiles(string pathToZip)
        {
            List<String> emptyFiles = new List<string>();
            using (ZipArchive archive = ZipFile.OpenRead(pathToZip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.Length == 0)
                    {
                        emptyFiles.Add(entry.Name);
                    }
                }
            }
            return emptyFiles;
        }

        /// <summary>Adds the file.</summary>
        /// <param name="zipPath">The zip path.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="compression">The compression.</param>
        public static void AddFile(string zipPath, string filePath, CompressionLevel compression = CompressionLevel.Optimal)
        {
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath), compression);
            }
        }
    }
}