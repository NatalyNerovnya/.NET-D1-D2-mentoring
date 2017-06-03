namespace SiteCopy.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlPath = "https://translate.google.com/";
            var folderPath = @"D:\Nana\";
            CustomWget.Download(urlPath, folderPath, 2);
        }
    }
}
