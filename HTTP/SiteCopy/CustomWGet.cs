namespace SiteCopy
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
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
            if (traicingMode)
                Console.WriteLine($"Analise on level {analysisnLevel}");
            string result;
            Uri uri;
            try
            {
                uri = new Uri(urlPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Some problems with path ({urlPath}).");
                Console.WriteLine(e.Message + "\n");
                
                return;
            }


            using (var client = new HttpClient())
            {
                try
                {
                    if (uri.Scheme != "http" && uri.Scheme != "https")
                    {
                        throw new ArgumentException($"Not HTTP or HTTPS schema. (Actualy it's {uri.Scheme})");
                    }
                    result = Task<string>.Run(() => client.GetAsync(uri)).Result.Content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Some problems while loading from {uri} are occured.");
                    Console.WriteLine(e.Message + "\n");
                    return;
                }
            }

            if (!folderPath.Contains(uri.Host))
            {
                folderPath += @"\" + uri.Host;
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                if (traicingMode)
                    Console.WriteLine($"Create new directory: {folderPath}");
            }

            var name = uri.AbsolutePath == @"\" || uri.AbsolutePath == "/" ? "main" : uri.AbsolutePath;
            var fileName = name + "_copy.html";
            fileName = fileName.Replace("/", string.Empty);
            fileName = fileName.Replace(@"\", string.Empty);
            var filePath = $@"{folderPath}\{fileName}";
            while (File.Exists(filePath))
            {
                filePath = NewFileName(folderPath, ref fileName);
            }

            using (var file = File.Create(filePath))
            {
                if(traicingMode)
                    Console.WriteLine($"Write to {filePath}");
            }

            using (var writer = new StreamWriter(filePath))
            {
                Task.Run(() => writer.WriteAsync(result)).Wait();
            }

            if (analysisnLevel <= 0)
            {
                return;
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(result);
            var nodes = doc.DocumentNode.SelectNodes("//a")?.ToArray();

            if (nodes == null)
            {
                return;
            }
            foreach (var node in nodes)
            {
                var reference = node.GetAttributeValue("href", string.Empty);
                if (reference != string.Empty)
                {
                    Download(reference, folderPath, analysisnLevel - 1);
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
