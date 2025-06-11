using System;
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

        public static string FindMessage(string text, string delimiter)
        {
            if (delimiter is null) throw new ArgumentNullException(nameof(delimiter));
            if (delimiter.Length == 0) throw new ArgumentException("Delimiter cannot be empty.", nameof(delimiter));
            if (text is null) throw new ArgumentNullException(nameof(text));

            // first occurrence (opening delimiter)
            int first = text.IndexOf(delimiter, StringComparison.Ordinal);
            if (first < 0) return null;                  // or throw

            // second occurrence (closing delimiter) – search *after* the first one
            int second = text.IndexOf(delimiter, first + delimiter.Length, StringComparison.Ordinal);
            if (second < 0) return null;                  // or throw

            int start = first + delimiter.Length;
            return text.Substring(start, second - start);
        }

    }
}
