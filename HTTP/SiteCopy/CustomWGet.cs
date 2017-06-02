namespace SiteCopy
{
    using System;
    using System.CodeDom;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Policy;
    using System.Threading.Tasks;

    using HtmlAgilityPack;

    public static class CustomWget
    {
        public static void Download(
            string urlPath,
            string folderPath,
            int analysisnLevel = 1,
            DomainRestriction domainRestriction = DomainRestriction.NoRestriction,
            string[] extensionRestriction = null,
            bool traicingMode = false)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(urlPath);
                var result = Task<HtmlDocument>.Run(() => client.GetAsync(uri)).Result.Content.ReadAsStringAsync().Result;
                folderPath += uri.Host;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var name = uri.AbsolutePath == @"\" || uri.AbsolutePath == "/" ? "main" : uri.AbsolutePath;
                var fileName = name + "_copy.html";
                var filePath = $@"{folderPath}\{fileName}";
                while (File.Exists(filePath))
                {
                    filePath = NewFileName(folderPath, ref fileName);
                }
                using (var file = File.Create(filePath))
                {
                }

                using (var writer = new StreamWriter(filePath))
                {
                    Task.Run(() => writer.WriteAsync(result)).Wait();
                }


            }
        }

        private static string NewFileName(string folderPath, ref string fileName)
        {
            fileName = "New" + fileName;
            return $@"{folderPath}\{fileName}";
        }

    }
}
