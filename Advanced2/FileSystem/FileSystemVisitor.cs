using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public class FileSystemVisitor : IEnumerable<string>
    {
        private string path; 

        public FileSystemVisitor() : this(@"C:\") { }

        public FileSystemVisitor(string path)
        {
            if(ReferenceEquals(path, null))
                throw new ArgumentNullException();
            this.path = path;
        }


        public IEnumerator<string> GetEnumerator()
        {
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var s in dirs)
                {
                    yield return s;
                }
                var files = Directory.GetFiles(path);
                foreach (var s in files)
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
