using System.IO;

namespace Dynamo.Business.Shared.Cyber.Scanner
{
    public class FolderScanner
    {
        public static string ScanFolder(string folder, string start, string end)
        {
            //return string.Empty;
            //var message
            var dirInfo = new DirectoryInfo(folder);
            foreach (var file in dirInfo.GetFiles())
            {
                using (var stream = file.OpenRead())
                using (var reader = new StreamReader(stream))
                {
                    string fileContents = reader.ReadToEnd();
                    if (fileContents.Contains(start) && fileContents.Contains(end))
                    {
                        return FindMessage(fileContents, start, end);
                    }
                }
            }
            return string.Empty;
        }

        public static int CountTheChar(string strToAnalyze, char charToCount)
        {
            var count = 0;
            foreach (var currentChar in strToAnalyze.ToCharArray())
            {
                if (currentChar == charToCount)
                    count++;
            }
            return count;
        }


        public static string FindMessage(string textToSearch, string start, string end)
        {
            var startIndex = textToSearch.IndexOf(start) + start.Length;
            var endIndex = textToSearch.IndexOf(end);
            return textToSearch.Substring(startIndex, endIndex - startIndex);
        }
    }
}
