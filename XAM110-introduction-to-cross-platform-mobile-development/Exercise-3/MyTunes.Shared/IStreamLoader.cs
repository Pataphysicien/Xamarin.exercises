using System;
using System.IO;

namespace MyTunes.Shared
{
    public interface IStreamLoader
    {
        Stream GetStreamForFilename(string filename);
    }
}

