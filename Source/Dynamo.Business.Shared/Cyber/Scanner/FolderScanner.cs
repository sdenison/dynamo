namespace Dynamo.Business.Shared.Cyber.Scanner
{
    public class FolderScanner
    {
        public static string ScanFolder(string folder, string start, string end)
        {
            return string.Empty;
        }

        public static string FindMessage(string textToSearch, string start, string end)
        {
            var startIndex = textToSearch.IndexOf(start) + start.Length;
            var endIndex = textToSearch.IndexOf(end);
            return textToSearch.Substring(startIndex, endIndex - startIndex);
        }
    }
}
