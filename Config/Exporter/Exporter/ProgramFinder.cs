using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exporter
{
    internal class ProgramFinder
    {
        public static string? FindConfigFileFromCurrent(string currentDirectory, string folderName= "AR_Project_Data")
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);

            // Traverse up the directory tree
            while (directoryInfo != null)
            {
                // Check if the desired folder exists in the current directory
                var targetDirectory = Path.Combine(directoryInfo.FullName, folderName);
                if (Directory.Exists(targetDirectory))
                {
                    return Path.Combine(targetDirectory, "StreamingAssets", "config.json");
                }

                // If the current directory is the root, stop the loop
                if (directoryInfo.Parent == null)
                {
                    break;
                }

                // Move up to the parent directory
                directoryInfo = directoryInfo.Parent;
            }

            return null; // Return null if the folder was not found
        }

    }
}
