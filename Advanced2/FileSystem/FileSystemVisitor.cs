using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileSystem
{
    public class FileSystemVisitor : IEnumerable<string>
    {
        private string path;
        private readonly Comparison<string> comparer;
        private delegate void VisitHandler(string message);
        private event VisitHandler VisitorEvent = delegate {};

        public FileSystemVisitor() : this(@"C:\", null) { }

        public FileSystemVisitor(string path): this(path, null){}

        public FileSystemVisitor(string path, Comparison<string> comparer)
        {
            this.path = path;
            this.comparer = comparer;

            if (ReferenceEquals(path, null))
                this.path = @"C:\";
            if (ReferenceEquals(comparer, null))
                this.comparer = default(Comparison<string>);

            this.VisitorEvent += WriteToDebugConsole;
        }

        public IEnumerator<string> GetEnumerator()
        {
            if (Directory.Exists(path))
            {
                VisitorEvent("-------START-------");
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                var elements = dirs.ToList().Union(files);

                if(! ReferenceEquals(comparer, null))
                    Array.Sort(dirs, comparer);

                foreach (var s in elements)
                {
                    VisitorEvent(s);
                    yield return s;
                }
                VisitorEvent("-------Finish-------");
            }
            else
                yield return "Such path doesn't exist!";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void WriteToDebugConsole(string message)
        {
            Debug.Print(message);
        }
    }

}
