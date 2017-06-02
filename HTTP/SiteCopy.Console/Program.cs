namespace SiteCopy.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlPath = "https://www.tut.by/";
            var folderPath = @"D:\Nana\";
            CustomWget.Download(urlPath, folderPath);
        }
    }
}
