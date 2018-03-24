namespace SiteCopy.Console
{
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var urlPath = "https://translate.google.com/";
            var folderPath = @"D:\Nana\";
   

            var callTask = Task.Run(() => CustomWget.DownloadAsync(urlPath, folderPath, 2, DomainRestriction.NoRestriction, true));
            // Wait for it to finish
            callTask.Wait();
        }
    }
}
