using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystem
{
    public class FileSystemVisitor : IEnumerable<string>
    {
        private string path;
        private readonly Comparison<string> comparer;

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
        }

        // TODO: Check dirs on NULL
        public IEnumerator<string> GetEnumerator()
        {
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                var elements = dirs.Union(files);


                if(! ReferenceEquals(comparer, null))
                    Array.Sort(dirs, comparer);

                foreach (var s in elements)
                {
                    yield return s;
                }

            }
            else
            {
                yield return "Such path doesn't exist!";
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
