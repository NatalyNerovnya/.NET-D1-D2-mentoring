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
        private static readonly INotifier notifier = new ConsoleNotifier(); 

        public static async Task  DownloadAsync(
            string urlPath,
            string folderPath,
            int analysisnLevel = 1,
            DomainRestriction domainRestriction = DomainRestriction.NoRestriction,
            bool traicingMode = false)
        {
            if (traicingMode)
                notifier.Notify($"\nAnalise on level {analysisnLevel}");

            string result;
            Uri uri;
            try
            {
                uri = new Uri(urlPath);
            }
            catch (Exception e)
            {
                notifier.Notify($"----Some problems with path ({urlPath}).");
                notifier.Notify($"----{e.Message}");
                return;
            }

            try
            {
                if (traicingMode) notifier.Notify($"Processing {uri}");
                result = await GetDataAsync(uri);
            }
            catch (Exception e)
            {
                notifier.Notify($"----Some problems while loading from {uri} are occured.");
                notifier.Notify($"----{e.Message}");
                return;
            }

            if (traicingMode)
                notifier.Notify($"Create new directory: {folderPath}");
            folderPath = CreateFolder(folderPath, uri.Host);
            
            var filePath = CreateAndFillFile(uri, folderPath, result);
            if (traicingMode)
                notifier.Notify($"Write to {filePath}");

            if (analysisnLevel <= 0)
            {
                return;
            }

            var nodes = GetHtmlNodes(result);
            if (nodes == null)
            {
                return;
            }

            foreach (var node in nodes)
            {
                var reference = node.GetAttributeValue("href", string.Empty);
                Uri newUri;
                try
                {
                    newUri = new Uri(reference);
                }
                catch (Exception e)
                {
                    notifier.Notify($"----Some problems with path ({reference}).");
                    notifier.Notify($"----{e.Message}");
                    return;
                }
                
                if (domainRestriction == DomainRestriction.InInitialURLOnly && newUri.Host != uri.Host)
                {
                    if (traicingMode)
                        notifier.Notify($"The url isn't in initial url: {reference}");
                    return;
                }

                if (reference != string.Empty)
                {
                    await DownloadAsync(reference, folderPath, analysisnLevel - 1, domainRestriction, traicingMode);
                }
            }
        }

        private static HtmlNode[] GetHtmlNodes(string result)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(result);
            var nodes = doc.DocumentNode.SelectNodes("//a")?.ToArray();
            return nodes;
        }

        private static string CreateAndFillFile(Uri uri, string folderPath, string result)
        {
            var name = uri.AbsolutePath == Path.DirectorySeparatorChar.ToString() || uri.AbsolutePath == Path.AltDirectorySeparatorChar.ToString() ? "main" : uri.AbsolutePath;
            var fileName = name + "_copy.html";
            fileName = fileName.Replace(Path.AltDirectorySeparatorChar.ToString(), string.Empty);
            fileName = fileName.Replace(Path.DirectorySeparatorChar.ToString(), string.Empty);
            var filePath = $@"{folderPath}{Path.DirectorySeparatorChar}{fileName}";
            while (File.Exists(filePath))
            {
                filePath = NewFileName(folderPath, ref fileName);
            }

            File.Create(filePath).Close();

            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(result);
            }

            return filePath;
        }

        private static string CreateFolder(string folderPath, string Host)
        {
            if (!folderPath.Contains(Host))
            {
                folderPath += Path.DirectorySeparatorChar + Host;
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        private static async Task<string> GetDataAsync(Uri uri)
        {
            string result;
            using (var client = new HttpClient())
            {
                if (uri.Scheme != "http" && uri.Scheme != "https")
                {
                    throw new ArgumentException($"Not HTTP or HTTPS schema. (Actualy it's {uri.Scheme})");
                }
                result = await client.GetStringAsync(uri);
            }

            return result;
        }

        private static string NewFileName(string folderPath, ref string fileName)
        {
            fileName = "New" + fileName;
            return $@"{folderPath}{Path.DirectorySeparatorChar}{fileName}";
        }
    }
}
