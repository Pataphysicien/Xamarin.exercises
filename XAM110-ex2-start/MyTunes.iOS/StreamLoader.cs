using MyTunes.Shared;
using System.IO;

namespace MyTunes
{
    class StreamLoader : IStreamLoader
    {
        public Stream GetStreamForFilename(string pFileName)
        {
            return System.IO.File.OpenRead(pFileName);
        }
    }
}